using GSM00700Common.DTO.Upload_DTO;
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

namespace GSM00700Back
{
    public class GSM00710UploadCashFlowValidateCls : R_IBatchProcess
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
            List<GSM00710UploadCashFlowDTO> loResult = null;
            int count = 1;

            try
            {
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM00710UploadCashFlowDTO>>(poBatchProcessPar.BigObject);


                List<GSM00710UploadCashFlowSaveDTO> loParam = new List<GSM00710UploadCashFlowSaveDTO>();

                foreach (var item in loTempObject) // untuk mendapatkan NO karna di front tidak mengirim NO
                {
                    loParam.Add(new GSM00710UploadCashFlowSaveDTO()
                    {
                        NO = count,
                        CCOMPANY_ID = poBatchProcessPar.Key.COMPANY_ID,
                        CSEQ = item.CSEQ,
                        CCASHFLOW_CODE = item.CCASHFLOW_CODE,
                        CCASH_FLOW_NAME = item.CCASH_FLOW_NAME,
                        CCASHFLOW_TYPE = item.CCASHFLOW_TYPE,
                        CCASHFLOW_GROUP_CODE = item.CCASHFLOW_GROUP_CODE,
                        LEXIST = item.LEXIST
                    });
                    count++;
                };



                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {

                    lcQuery = "CREATE TABLE #CASHFLOW ( " +
                              "    NO INT, " +
                              "    CCOMPANY_ID VARCHAR(8), " +
                              "    CCASHFLOW_GROUP_CODE VARCHAR(100), " +
                              "    CSEQ VARCHAR(3), " +
                              "    CCASHFLOW_CODE VARCHAR(100), " +
                              "    CCASH_FLOW_NAME NVARCHAR(100), " +
                              "    CCASHFLOW_TYPE VARCHAR(10), " +
                              "    LEXIST BIT " +
                              ")";


                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<GSM00710UploadCashFlowSaveDTO>((SqlConnection)loConn, "#CASHFLOW", loParam);

                    lcQuery = $"EXEC RSP_GS_VALIDATE_UPLOAD_CASHFLOW " +
                        $"@CCOMPANY_ID, " +
                        $"@CUSER_ID, " +
                        $"@KEY_GUID";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                    loCmd.CommandText = lcQuery;

                    loDb.SqlExecNonQuery(loConn, loCmd, false);


                    //lcQuery = $"SELECT CCOMPANY_ID FROM GST_XML_RESULT WHERE CCOMPANY_ID = @CCOMPANY_ID AND CUSER_ID = @CUSER_ID AND CKEY_GUID = @KEY_GUID ";
                    //loCmd.CommandText = lcQuery; //untuk check ada error atau tidak

                    //var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    //var loDataErrorValidate = R_Utility.R_ConvertTo<GSM00710UploadCashFlowDTO>(loDataTable).ToList();

                    //if (loDataErrorValidate.Count > 0)
                    //{
                    //    lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @KEY_GUID";
                    //    loCmd.CommandText = lcQuery;

                    //    var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                    //    var loTempResult = R_Utility.R_ConvertTo<GSM00710UploadCashFlowErrorDTO>(loDataTableResult).ToList();

                    //    var loConvertJson = JsonSerializer.Serialize(loTempResult);

                    //    throw new Exception(loConvertJson);
                    //}

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


        public List<GSM00710UploadCashFlowErrorDTO> GetErrorProcess(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            var loEx = new R_Exception();
            var lcQuery = "";
            var loDb = new R_Db();
            List<GSM00710UploadCashFlowErrorDTO> loResult = null;

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

                loResult = R_Utility.R_ConvertTo<GSM00710UploadCashFlowErrorDTO>(loDataTableResult).ToList();
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
