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
    public class LMM02000UploadSalesmanCls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
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
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";
            List<LMM02000UploadErrorValidateDTO> loTempObject = new();
            IList<LMM02000UploadRequestDTO> loObject = new List<LMM02000UploadRequestDTO>();
            object loVar = "";
            //object loTempVar = "";
            var LcGroupCode = "";


            try
            {
                await Task.Delay(1000);

                loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<LMM02000UploadErrorValidateDTO>>(poBatchProcessPar.BigObject);
                loObject = R_Utility.R_ConvertCollectionToCollection<LMM02000UploadErrorValidateDTO, LMM02000UploadRequestDTO>(loTempObject);

                loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstantLMM02000.CPROPERTY_ID)).FirstOrDefault().Value;
                var lcCodeGroup = ((System.Text.Json.JsonElement)loVar).GetString();

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery += "  CREATE TABLE #SALESMAN" +
                           "  (NO INT" +
                           ", SalesmanId VARCHAR(100)" +
                           ", SalesmanName NVARCHAR(100)" +
                           ", Active BIT" +
                           ", Address  NVARCHAR(255)" +
                           ", EmailAddress VARCHAR(100)" +
                           ", MobileNo1 VARCHAR(100)" +
                           ", MobileNo2 VARCHAR(100)" +
                           ", NIK NVARCHAR(100)" +
                           ", Gender VARCHAR(10)" +
                           ", SalesmanType VARCHAR(100)" +
                           ", CompanyName VARCHAR(100)" +
                           ", NonActiveDate VARCHAR(100))";


                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                loDb.R_BulkInsert<LMM02000UploadRequestDTO>((SqlConnection)loConn, "#SALESMAN", loObject);


                lcQuery = "EXECUTE RSP_LM_UPLOAD_SALESMAN @CCOMPANY_ID, @CPROPERTY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, lcCodeGroup);
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
