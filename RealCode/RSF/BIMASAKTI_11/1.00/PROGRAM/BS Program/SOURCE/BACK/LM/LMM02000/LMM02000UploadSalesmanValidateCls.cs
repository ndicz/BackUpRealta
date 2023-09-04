using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using LMM02000Common.DTO.UPLOAD_DTO_LMM02000;

namespace LMM02000Back
{
    public class LMM02000UploadSalesmanValidateCls
    {

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
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
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<LMM02000UploadSalesmanDTO>>(poBatchProcessPar.BigObject);


                List<LMM02000UploadSalesmanSaveDTO> loParam = new List<LMM02000UploadSalesmanSaveDTO>();

                foreach (var item in loTempObject)
                {
                    loParam.Add(new LMM02000UploadSalesmanSaveDTO()
                    {
                        NO = count,
                        CCOMPANY_ID = poBatchProcessPar.Key.COMPANY_ID,
          
                        LEXIST = item.LEXIST,
                        LOVERWRITE = item.LOVERWRITE,
                    });
                    count++;
                };



                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {

                    lcQuery = $"CREATE TABLE #CASHFLOW_PLAN " +
                              $"(CCOMPANY_ID VARCHAR(50)," +
                              $"NO INT, " +
                              $"CCASHFLOW_GROUP_CODE VARCHAR(50), " +
                              $"CCYEAR VARCHAR(50), " +
                              $"CPERIOD_NO VARCHAR(20), " +
                              $"NLOCAL_AMOUNT NUMERIC(18,2)," +
                              $"NBASE_AMOUNT NUMERIC(18,2), " +
                              $"LEXIST BIT, " +
                              $"LOVERWRITE BIT," +
                              $"CCASH_FLOW_CODE VARCHAR(20));";


                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<LMM02000UploadSalesmanSaveDTO>((SqlConnection)loConn, "#CASHFLOW_PLAN", loParam);

                    lcQuery = $"EXEC RSP_GS_VALIDATE_UPLOAD_CASHFLOW_PLAN " +
                        $"@CCOMPANY_ID, " +
                        $"@CUSER_ID, " +
                        $"@KEY_GUID," +
                        $"@LOVERWRITE";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                    loDb.R_AddCommandParameter(loCmd, "@LOVERWRITE", DbType.Boolean, 50, false);

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
        public List<LMM02000UploadSalesmanErrorDTO> GetErrorProcess(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            var loEx = new R_Exception();
            var lcQuery = "";
            var loDb = new R_Db();
            List<LMM02000UploadSalesmanErrorDTO> loResult = null;

            try
            {
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<LMM02000UploadSalesmanErrorDTO>(loDataTableResult).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}