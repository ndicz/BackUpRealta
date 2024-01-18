using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using GSM00700Common;
using GSM00710Common.DTO.Upload_DTO_GSM00710;

namespace GSM00700Back
{
    public class GSM00710UploadCashFlowCls : R_IBatchProcess
    {
        RSP_GS_MAINTAIN_CASHFLOW_GROUPResources.Resources_Dummy_Class ResourceDummyClasCashFlowGroup = new();
        RSP_GS_MAINTAIN_CASHFLOWResources.Resources_Dummy_Class ResourcesDummyClassCashFlow = new();
        private readonly ActivitySource _activitySource;

        public GSM00710UploadCashFlowCls()
        {
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        #region NoteUse

        

       
        //    public GSM00710UploadCashFlowCheckUsedDTO CheckIsCashFlowUsed(GSM00710UploadCashFlowCheckUsedParameterDTO poEntity)
        //    {
        //        R_Exception loException = new R_Exception();
        //        GSM00710UploadCashFlowCheckUsedDTO loResult = null;

        //        try
        //        {
        //            R_Db loDb = new R_Db();
        //            DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

        //            string lcQuery = $"DECLARE @Result BIT; " +
        //                             $"IF EXISTS ( " +
        //                             $"SELECT TOP 1 1 FROM GSM_CASH_FLOW (NOLOCK) WHERE NOT EXISTS (SELECT TOP 1 1 FROM GSM_COA " +
        //                             $"(NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID" +
        //                             $" AND CCASH_FLOW_GROUP_CODE = @CCASH_FLOW_GROUP_CODE" +
        //                             $" AND CCASH_FLOW_CODE = @CCASH_FLOW_CODE))" +
        //                             $"SET @Result = 1; -- Condition is true, set @Result to true " +
        //                             $"ELSE " +
        //                             $"SET @Result = 0; -- Condition is false, set @Result to false " +
        //                             $"SELECT @Result AS LRESULT;";

        //            DbCommand loCmd = loDb.GetCommand();
        //            loCmd.CommandText = lcQuery;

        //            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
        //            loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 50, poEntity.CCASH_FLOW_GROUP_CODE);
        //            loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", DbType.String, 50, poEntity.CCASH_FLOW_CODE);


        //            //loDb.SqlExecNonQuery(loConn, loCmd, false);

        //            //lcQuery = $"SELECT @Result AS LRESULT";

        //            //loCmd.CommandText = lcQuery;

        //            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

        //            loResult = R_Utility.R_ConvertTo<GSM00710UploadCashFlowCheckUsedDTO>(loDataTable).FirstOrDefault();
        //        }
        //        catch (Exception ex)
        //        {
        //            loException.Add(ex);
        //        }

        //        loException.ThrowExceptionIfErrors();

        //        return loResult;
        //    }

        //    public List<GSM00710UploadCashFlowDTO> GetGSM00710UploadCashFlowList(List<GSM00710UploadCashFlowDTO> poEntity)
        //    {
        //        R_Exception loException = new R_Exception();
        //        List<GSM00710UploadCashFlowDTO> loResult = null;

        //        try
        //        {
        //            R_Db loDb = new R_Db();
        //            DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

        //            string lcQuery = $"CREATE TABLE #CASHFLOW " +
        //                    $"(CCOMPANY_ID VARCHAR(50), " +
        //                    $"CSEQ VARCHAR(50), " +
        //                    $"CCASHFLOW_GROUP_CODE VARCHAR(50), " +
        //                    $"CCASHFLOW_CODE VARCHAR(20), " +
        //                    $"CCASHFLOW_GROUP_NAME VARCHAR(50)," +
        //                    $"NOTES_ VARCHAR(20)," +
        //                    $"CCASH_FLOW_NAME VARCHAR(100), " +
        //                    $"CCASHFLOW_TYPE NVARCHAR(MAX), " +
        //                    $"LEXIST BIT, " +
        //                    $"LOVER_WRITE BIT);";

        //            loDb.SqlExecNonQuery(lcQuery, loConn, false);

        //            loDb.R_BulkInsert<GSM00710UploadCashFlowDTO>((SqlConnection)loConn, "#CASHFLOW", poEntity);

        //            //lcQuery = $"UPDATE A SET A.LEXIST = 1 " +
        //            //    $"FROM #CASHFLOW A WHERE EXISTS " +
        //            //    $"(SELECT TOP 1 1 FROM GSM_CASH_FLOW B " +
        //            //    $"WHERE B.CCOMPANY_ID = A.CCOMPANY_ID " +
        //            //    $"AND B.CCASH_FLOW_CODE = A.CCASHFLOW_CODE " +


        //            //loDb.SqlExecNonQuery(lcQuery, loConn, false);

        //            //lcQuery = $"SELECT * FROM #CASHFLOW";

        //            DbCommand loCmd = loDb.GetCommand();
        //            loCmd.CommandText = lcQuery;

        //            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

        //            loResult = R_Utility.R_ConvertTo<GSM00710UploadCashFlowDTO>(loDataTable).ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            loException.Add(ex);
        //        }

        //        loException.ThrowExceptionIfErrors();

        //        return loResult;
        //    }

        //    //public void R_AttachFile(R_AttachFilePar poAttachFile)
        //    //{
        //    //    R_Exception loException = new R_Exception();
        //    //    string lcCmd;
        //    //    string lcQuery = "";
        //    //    var loDb = new R_Db();
        //    //    DbConnection loConn = loDb.GetConnection();
        //    //    var loCmd = loDb.GetCommand();
        //    //    Dictionary<string, string> loMapping = new Dictionary<string, string>();
        //    //    List<GSM00710UploadCashFlowDTO> loResult = null;
        //    //    int count = 1;

        //    //    try
        //    //    {
        //    //        var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM00710UploadCashFlowDTO>>(poAttachFile.BigObject);


        //    //        List<GSM00710UploadCashFlowSaveDTO> loParam = new List<GSM00710UploadCashFlowSaveDTO>();

        //    //        foreach (var item in loTempObject)
        //    //        {
        //    //            loParam.Add(new GSM00710UploadCashFlowSaveDTO()
        //    //            {
        //    //                NO = count,
        //    //                CCOMPANY_ID = poAttachFile.Key.COMPANY_ID,
        //    //                CSEQ =item.CSEQ,
        //    //                CCASHFLOW_CODE = item.CCASHFLOW_CODE,
        //    //                CCASH_FLOW_NAME = item.CCASH_FLOW_NAME,
        //    //                CCASHFLOW_TYPE = item.CCASHFLOW_TYPE,
        //    //                CCASHFLOW_GROUP_CODE = item.CCASHFLOW_GROUP_CODE,
        //    //                LEXIST = item.LEXIST
        //    //            });
        //    //            count++;
        //    //        };



        //    //        using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
        //    //        {

        //    //            lcQuery = "CREATE TABLE #CASHFLOW ( " +
        //    //                      "    NO INT, " +
        //    //                      "    CCOMPANY_ID VARCHAR(8), " +
        //    //                      "    CCASHFLOW_GROUP_CODE VARCHAR(100), " +
        //    //                      "    CSEQ VARCHAR(3), " +
        //    //                      "    CCASHFLOW_CODE VARCHAR(100), " +
        //    //                      "    CCASH_FLOW_NAME NVARCHAR(100), " +
        //    //                      "    CCASHFLOW_TYPE VARCHAR(10), " +
        //    //                      "    LEXIST BIT " +
        //    //                      ")";


        //    //            loDb.SqlExecNonQuery(lcQuery, loConn, false);

        //    //            loDb.R_BulkInsert<GSM00710UploadCashFlowSaveDTO>((SqlConnection)loConn, "#CASHFLOW", loParam);

        //    //            lcQuery = $"EXEC RSP_GS_VALIDATE_UPLOAD_CASHFLOW " +
        //    //                $"@CCOMPANY_ID, " +
        //    //                $"@CUSER_ID, " +
        //    //                $"@KEY_GUID";

        //    //            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poAttachFile.Key.COMPANY_ID);
        //    //            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poAttachFile.Key.USER_ID);
        //    //            loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poAttachFile.Key.KEY_GUID);

        //    //            loCmd.CommandText = lcQuery;

        //    //            loDb.SqlExecNonQuery(loConn, loCmd, false);


        //    //            lcQuery = $"SELECT CCOMPANY_ID FROM GST_XML_RESULT WHERE CCOMPANY_ID = @CCOMPANY_ID AND CUSER_ID = @CUSER_ID AND CKEY_GUID = @KEY_GUID ";
        //    //            loCmd.CommandText = lcQuery;

        //    //            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

        //    //            var loDataErrorValidate = R_Utility.R_ConvertTo<GSM00710UploadCashFlowDTO>(loDataTable).ToList();

        //    //            if (loDataErrorValidate.Count > 0)
        //    //            {
        //    //                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @KEY_GUID";
        //    //                loCmd.CommandText = lcQuery;

        //    //                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

        //    //                var loTempResult = R_Utility.R_ConvertTo<GSM00710UploadCashFlowErrorDTO>(loDataTableResult).ToList();

        //    //                var loConvertJson = JsonSerializer.Serialize(loTempResult);

        //    //                throw new Exception(loConvertJson);
        //    //            }

        //    //            TransScope.Complete();
        //    //        }
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        loException.Add(ex);
        //    //    }
        //    //    finally
        //    //    {
        //    //        if (loConn != null)
        //    //        {
        //    //            if (loConn.State != ConnectionState.Closed)
        //    //                loConn.Close();

        //    //            loConn.Dispose();
        //    //            loConn = null;
        //    //        }
        //    //    }

        //    //    loException.ThrowExceptionIfErrors();
        //    //}

        //    public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        //    {
        //        var loDb = new R_Db();
        //        DbConnection loConn = null;
        //        DbCommand loCmd = null;
        //        var loEx = new R_Exception();
        //        var lcQuery = "";
        //        int count = 1;

        //        try
        //        {
        //            using (var transScope = new TransactionScope(TransactionScopeOption.Required))
        //            {
        //                loConn = loDb.GetConnection(); // UNTUK CONNECTION TRASCOPE GUNAKAN DISINI 
        //                loCmd = loDb.GetCommand();
        //                var liFinishFlag = 1; //0=Process, 1=Success, 9=Fail
        //                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM00710UploadCashFlowDTO>>(poBatchProcessPar.BigObject);


        //                var loVar1 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstantGSM00700.IS_OVERWRITE_CONTEXT)).FirstOrDefault().Value;
        //                bool IsOverwrite = ((System.Text.Json.JsonElement)loVar1).GetBoolean();

        //                List<GSM00710UploadCashFlowSaveDTO> loParam = new List<GSM00710UploadCashFlowSaveDTO>();

        //                foreach (var item in loObject)
        //                {
        //                    loParam.Add(new GSM00710UploadCashFlowSaveDTO()
        //                    {
        //                        NO = count,
        //                        CCOMPANY_ID = poBatchProcessPar.Key.COMPANY_ID,
        //                        CSEQ = item.CSEQ,
        //                        CCASHFLOW_CODE = item.CCASHFLOW_CODE,
        //                        CCASH_FLOW_NAME = item.CCASH_FLOW_NAME,
        //                        CCASHFLOW_TYPE = item.CCASHFLOW_TYPE,
        //                        CCASHFLOW_GROUP_CODE = item.CCASHFLOW_GROUP_CODE,
        //                        LEXIST = item.LEXIST
        //                    });
        //                    count++;
        //                };

        //                lcQuery = "CREATE TABLE #CASHFLOW ( " +
        //                          "    NO INT, " +
        //                          "    CCOMPANY_ID VARCHAR(8), " +
        //                          "    CCASHFLOW_GROUP_CODE VARCHAR(100), " +
        //                          "    CSEQ VARCHAR(3), " +
        //                          "    CCASHFLOW_CODE VARCHAR(100), " +
        //                          "    CCASH_FLOW_NAME NVARCHAR(100), " +
        //                          "    CCASHFLOW_TYPE VARCHAR(10), " +
        //                          "    LEXIST BIT " +
        //                          ")";

        //                loDb.SqlExecNonQuery(lcQuery, loConn, false);

        //                loDb.R_BulkInsert<GSM00710UploadCashFlowSaveDTO>((SqlConnection)loConn, "#CASHFLOW", loParam);

        //                lcQuery = $"EXEC RSP_GS_UPLOAD_CASHFLOW " +
        //                    $"@CCOMPANY_ID, " +
        //                    $"@CUSER_ID, " +
        //                    $"@CKEY_GUID, " +
        //                    $"@LOVERWRITE";

        //                /*
        //                                    lcQuery = "RSP_GS_GSM00710Upload_PROPERTY_CashFlow";
        //                                    loCmd.CommandType = CommandType.StoredProcedure;
        //                */

        //                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
        //                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
        //                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
        //                loDb.R_AddCommandParameter(loCmd, "@LOVERWRITE", DbType.Boolean, 50, IsOverwrite);


        //                loCmd.CommandText = lcQuery;
        //                loDb.SqlExecNonQuery(loConn, loCmd, false);

        //                transScope.Complete(); // UNTUK ROLLBACK 
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            loEx.Add(ex);
        //        }
        //        finally
        //        {
        //            if (loConn != null)
        //            {
        //                if (loConn.State != System.Data.ConnectionState.Closed)
        //                    loConn.Close();

        //                loConn.Dispose();
        //                loConn = null;
        //            }

        //            if (loCmd != null)
        //            {
        //                loCmd.Dispose();
        //                loCmd = null;
        //            }
        //        }

        //        loEx.ThrowExceptionIfErrors();
        //    }
        //}
        #endregion
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using var Activity = _activitySource.StartActivity(nameof(R_BatchProcess));
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });

                while (!loTask.IsCompleted)
                {
                    Thread.Sleep(100);
                }

                if (loTask.IsFaulted)
                {
                    loException.Add(loTask.Exception.InnerException != null ?
                        loTask.Exception.InnerException :
                        loTask.Exception);

                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:

            loException.ThrowExceptionIfErrors();
        }

        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using var Activity = _activitySource.StartActivity(nameof(_BatchProcess));
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";
            List<GSM00710UploadErrorValidateDTO> loTempObject = new();
            IList<GSM00710UploadRequestDTO> loObject = new List<GSM00710UploadRequestDTO>();
            object loVar = "";
            //object loTempVar = "";
            var LcGroupCode = "";


            try
            {
                await Task.Delay(1000);

                loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM00710UploadErrorValidateDTO>>(poBatchProcessPar.BigObject);
                loObject = R_Utility.R_ConvertCollectionToCollection<GSM00710UploadErrorValidateDTO, GSM00710UploadRequestDTO>(loTempObject);

                loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE)).FirstOrDefault().Value;
                var lcCodeGroup = ((System.Text.Json.JsonElement)loVar).GetString();

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery += "CREATE TABLE #CASHFLOW (" +
                           "NO INT, " +
                           "CCOMPANY_ID VARCHAR(8)," +
                           "CCASHFLOW_GROUP_CODE VARCHAR(100)," +
                           "CSEQ VARCHAR(3)," +
                           "CCASHFLOW_CODE VARCHAR(100)," +
                           "CCASH_FLOW_NAME NVARCHAR(100)," +
                           "CCASHFLOW_TYPE VARCHAR(10),)";


                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                loDb.R_BulkInsert<GSM00710UploadRequestDTO>((SqlConnection)loConn, "#CASHFLOW", loObject);


                lcQuery = "EXECUTE RSP_GS_UPLOAD_CASHFLOW @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASHFLOW_GROUP_CODE", DbType.String, 20, lcCodeGroup);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loDb.SqlExecNonQuery(loConn, loCmd, false);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (!(loConn.State == ConnectionState.Closed))
                        loConn.Close();
                    loConn.Dispose();
                    loConn = null;
                }

                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }

            if (loException.Haserror)
            {
                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                          $"'{poBatchProcessPar.Key.USER_ID}', " +
                          $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                          $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                loDb.SqlExecNonQuery(lcQuery);
            }
        }
    }
}
