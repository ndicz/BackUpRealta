using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using LMM02000Common;
using LMM02000Common.DTO.UPLOAD_DTO_LMM02000;

namespace LMM02000Back
{
    public class LMM02000UploadSalesmanCls : R_IBatchProcess, R_IAttachFile
    {
        //ga kepake
        public LMM02000UploadSalesmanCheckUsedDTO CheckIsSalesmanUsed(LMM02000UploadSalesmanCheckUsedParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM02000UploadSalesmanCheckUsedDTO loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"DECLARE @Result BIT; " +
                                 $"IF EXISTS ( " +
                                 $"SELECT TOP 1 1 FROM GSM_CASH_FLOW (NOLOCK) WHERE NOT EXISTS (SELECT TOP 1 1 FROM GSM_COA " +
                                 $"(NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID" +
                                 $" AND CCASH_FLOW_GROUP_CODE = @CCASH_FLOW_GROUP_CODE" +
                                 $" AND CCASH_FLOW_CODE = @CCASH_FLOW_CODE)" +
                                 $"SET @Result = 1; -- Condition is true, set @Result to true " +
                                 $"ELSE " +
                                 $"SET @Result = 0; -- Condition is false, set @Result to false " +
                                 $"SELECT @Result AS LRESULT;";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 50, poEntity.CPROPERTY_ID);



                loDb.SqlExecNonQuery(loConn, loCmd, false);

                lcQuery = $"SELECT @Result AS LRESULT";

                loCmd.CommandText = lcQuery;

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM02000UploadSalesmanCheckUsedDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM02000UploadSalesmanDTO> GetLMM02000UploadSalesmanList(List<LMM02000UploadSalesmanDTO> poEntity)
        {
            R_Exception loException = new R_Exception();
            List<LMM02000UploadSalesmanDTO> loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"CREATE TABLE #SALESMAN ( " +
                           $"NO INT, " +
                          $"SalesmanId  VARCHAR(8)," +
                          $"SalesmanName NVARCHAR(100)," +
                          $"Active BIT," +
                          $"NonActiveDate    VARCHAR(8)," +
                          $"Address NVARCHAR(255)," +
                          $"EmailAddress VARCHAR(100)," +
                          $"MobileNo1 VARCHAR(30)," +
                          $"MobileNo2 VARCHAR(30)," +
                          $"NIK NVARCHAR(40)," +
                          $"Gender VARCHAR(2)," +
                          $"SalesmanType VARCHAR(2)," +
                          $"CompanyName VARCHAR(20)," +
                          $"LEXIST BIT " +
                          $")";


                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<LMM02000UploadSalesmanDTO>((SqlConnection)loConn, "#SALESMAN", poEntity);

                lcQuery = $"UPDATE A SET A.LEXIST = 1 " +
                          $"FROM #SALESMAN A WHERE EXISTS " +
                          $"(SELECT TOP 1 1 FROM LMM_SALESMAN B " +
                          $"WHERE B.CCOMPANY_ID = A.CCOMPANY_ID " +
                          $"AND B.CSALESMAN_ID = A.CSALESMAN_ID " +


                          loDb.SqlExecNonQuery(lcQuery, loConn, false);

                lcQuery = $"SELECT * FROM #SALESMAN";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM02000UploadSalesmanDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public void R_AttachFile(R_AttachFilePar poAttachFile)
        {
            R_Exception loException = new R_Exception();
            string lcCmd;
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = loDb.GetConnection();
            var loCmd = loDb.GetCommand();
            Dictionary<string, string> loMapping = new Dictionary<string, string>();
            List<LMM02000UploadSalesmanDTO> loResult = null;
            int count = 1;

            try
            {
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<LMM02000UploadSalesmanDTO>>(poAttachFile.BigObject);
                var loVar = poAttachFile.UserParameters.Where((x) => x.Key.Equals(ContextConstantLMM02000.UPLOAD_UNIT_PROMOTION_PROPERTY_ID_CONTEXT)).FirstOrDefault().Value;
                string PropertyId = ((System.Text.Json.JsonElement)loVar).GetString();
                var loVar1 = poAttachFile.UserParameters.Where((x) => x.Key.Equals(ContextConstantLMM02000.UPLOAD_UNIT_PROMOTION_IS_OVERWRITE_CONTEXT)).FirstOrDefault().Value;
                bool IsOverwrite = ((System.Text.Json.JsonElement)loVar1).GetBoolean();

                List<LMM02000UploadSalesmanSaveDTO> loParam = new List<LMM02000UploadSalesmanSaveDTO>();

                foreach (var item in loTempObject)
                {
                    loParam.Add(new LMM02000UploadSalesmanSaveDTO()
                    {
                        NO = count,
                        CCOMPANY_ID = poAttachFile.Key.COMPANY_ID,
                        CPROPERTY_ID = item.CPROPERTY_ID,
                        SalesmanId = item.SalesmanId,
                        SalesmanName = item.SalesmanName,
                        Active = item.Active,
                        NonActiveDate = item.NonActiveDate,
                        Address = item.Address,
                        EmailAddress = item.EmailAddress,
                        MobileNo1 = item.MobileNo1,
                        MobileNo2 = item.MobileNo2,
                        NIK = item.NIK,
                        Gender = item.Gender,
                        SalesmanType = item.SalesmanType,
                        CompanyName = item.CompanyName,
                        LEXIST = item.LEXIST
                    });
                    count++;
                };


                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {

                    lcQuery = "CREATE TABLE #SALESMAN ( " +
                              "NO INT, " +
                              "SalesmanId  VARCHAR(8)," +
                              "SalesmanName NVARCHAR(100)," +
                              "Active BIT," +
                              "NonActiveDate    VARCHAR(8)," +
                              "Address NVARCHAR(255)," +
                              "EmailAddress VARCHAR(100)," +
                              "MobileNo1 VARCHAR(30)," +
                              "MobileNo2 VARCHAR(30)," +
                              "NIK NVARCHAR(40)," +
                              "Gender VARCHAR(2)," +
                              "SalesmanType VARCHAR(2)," +
                              "CompanyName VARCHAR(20)" +
                              ")";


                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<LMM02000UploadSalesmanSaveDTO>((SqlConnection)loConn, "#SALESMAN", loParam);

                    lcQuery = $"EXEC RSP_LM_UPLOAD_SALESMAN " +
                        $"@CCOMPANY_ID, " +
                        $"@CUSER_ID, " +
                        $"@CPROPERTY_ID," +
                        $"@KEY_GUID," +
                        $"@Var_Overwrite";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poAttachFile.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poAttachFile.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poAttachFile.Key.KEY_GUID);
                    loDb.R_AddCommandParameter(loCmd, "@Var_Overwrite", DbType.Boolean, 50, IsOverwrite);

                    loCmd.CommandText = lcQuery;

                    loDb.SqlExecNonQuery(loConn, loCmd, false);


                    lcQuery = $"SELECT CCOMPANY_ID FROM GST_XML_RESULT WHERE CCOMPANY_ID = @CCOMPANY_ID AND CUSER_ID = @CUSER_ID AND CKEY_GUID = @KEY_GUID ";
                    loCmd.CommandText = lcQuery;

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    var loDataErrorValidate = R_Utility.R_ConvertTo<LMM02000UploadSalesmanDTO>(loDataTable).ToList();

                    if (loDataErrorValidate.Count > 0)
                    {
                        lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @KEY_GUID";
                        loCmd.CommandText = lcQuery;

                        var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                        var loTempResult = R_Utility.R_ConvertTo<LMM02000UploadSalesmanErrorDTO>(loDataTableResult).ToList();

                        var loConvertJson = JsonSerializer.Serialize(loTempResult);

                        throw new Exception(loConvertJson);
                    }

                    TransScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
            }

            loException.ThrowExceptionIfErrors();
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            var loEx = new R_Exception();
            var lcQuery = "";
            int count = 1;

            try
            {
                using (var transScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    loConn = loDb.GetConnection(); // UNTUK CONNECTION TRASCOPE GUNAKAN DISINI 
                    loCmd = loDb.GetCommand();
                    var liFinishFlag = 1; //0=Process, 1=Success, 9=Fail
                    var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<LMM02000UploadSalesmanDTO>>(poBatchProcessPar.BigObject);


                    var loVar1 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstantLMM02000.IS_OVERWRITE_CONTEXT)).FirstOrDefault().Value;
                    bool IsOverwrite = ((System.Text.Json.JsonElement)loVar1).GetBoolean();

                    List<LMM02000UploadSalesmanSaveDTO> loParam = new List<LMM02000UploadSalesmanSaveDTO>();

                    foreach (var item in loObject)
                    {
                        loParam.Add(new LMM02000UploadSalesmanSaveDTO()
                        {
                            NO = count,
                            CCOMPANY_ID = poBatchProcessPar.Key.COMPANY_ID,
                            //CSEQ = item.CSEQ,
                            //CSalesman_CODE = item.CSalesman_CODE,
                            //CCASH_FLOW_NAME = item.CCASH_FLOW_NAME,
                            //CSalesman_TYPE = item.CSalesman_TYPE,
                            //CSalesman_GROUP_CODE = item.CSalesman_GROUP_CODE,
                            LEXIST = item.LEXIST
                        });
                        count++;
                    };

                    lcQuery = "CREATE TABLE #Salesman ( " +
                              "    NO INT, " +
                              "    CCOMPANY_ID VARCHAR(8), " +
                              "    CSalesman_GROUP_CODE VARCHAR(100), " +
                              "    CSEQ VARCHAR(3), " +
                              "    CSalesman_CODE VARCHAR(100), " +
                              "    CCASH_FLOW_NAME NVARCHAR(100), " +
                              "    CSalesman_TYPE VARCHAR(10), " +
                              "    LEXIST BIT " +
                              ")";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<LMM02000UploadSalesmanSaveDTO>((SqlConnection)loConn, "#Salesman", loParam);

                    lcQuery = $"EXEC RSP_GS_UPLOAD_Salesman " +
                        $"@CCOMPANY_ID, " +
                        $"@CUSER_ID, " +
                        $"@CKEY_GUID, " +
                        $"@LOVERWRITE";

                    /*
                                        lcQuery = "RSP_GS_LMM02000Upload_PROPERTY_Salesman";
                                        loCmd.CommandType = CommandType.StoredProcedure;
                    */

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                    //loDb.R_AddCommandParameter(loCmd, "@LOVERWRITE", DbType.Boolean, 50, IsOverwrite);


                    loCmd.CommandText = lcQuery;
                    loDb.SqlExecNonQuery(loConn, loCmd, false);

                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
