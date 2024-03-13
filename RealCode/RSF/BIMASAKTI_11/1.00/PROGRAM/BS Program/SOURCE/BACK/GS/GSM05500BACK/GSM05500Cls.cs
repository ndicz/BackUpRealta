using GSM05500Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using GSM05500Back.Activity;
using GSM05500Common;
using R_OpenTelemetry;
using static GSM05500Common.DTO.GSM05500ListDTO;

namespace GSM05500Back
{
    public class GSM05500Cls : R_BusinessObject<GSM05500DTO>
    {
        RSP_GS_MAINTAIN_CURRENCYResources.Resources_Dummy_Class ResourcesDummyClass = new();
        RSP_GS_MAINTAIN_RATE_TYPEResources.Resources_Dummy_Class ResourcesDummyClass2 = new();
        RSP_GS_MAINTAIN_CURRENCY_RATEResources.Resources_Dummy_Class ResourcesDummyClass3 = new();
        
        private LogGSM05500Common _logger;
        private readonly ActivitySource _activitySource;
        public GSM05500Cls()
        {
            _logger = LogGSM05500Common.R_GetInstanceLogger();
            _activitySource= GSM05500Activity.R_GetInstanceActivitySource();
        }
        public List<GSM05500DTO> GetAllCurrency(GSM05500DBParameter poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(GetAllCurrency));
            R_Exception loException = new R_Exception();
            List<GSM05500DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                    
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CURRENCY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || Currency(Cls) ", lcQuery, poParameter, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM05500DTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
        protected override GSM05500DTO R_Display(GSM05500DTO poEntity)
        {
            using var activity = _activitySource.StartActivity(nameof(R_Display));
            R_Exception loException = new R_Exception();
            GSM05500DTO loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CURRENCY_DETAIL";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "CCURRENCY_CODE", DbType.String, 3, poEntity.CCURRENCY_CODE);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CCURRENCY_CODE").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || Currency(Cls) ", lcQuery, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM05500DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {

                loException.Add(ex);
                _logger.LogError(ex);
            }
            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        protected override void R_Saving(GSM05500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using var activity = _activitySource.StartActivity(nameof(R_Saving));
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string lcAction = "";
            try
            {
            
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);


                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    lcAction = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    lcAction = "EDIT";
                }

                lcQuery = "RSP_GS_MAINTAIN_CURRENCY";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 10, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_NAME", DbType.String, 60, poNewEntity.CCURRENCY_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCURRENCY_CODE" ||
                        x.ParameterName == "@CCURRENCY_NAME" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CACTION").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || Currency(Cls) ", lcQuery, loDbParam);
                //loDb.SqlExecNonQuery(loConn, loCommand, true);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {   
                    loException.Add(ex);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }

            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                }
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();   
        }

        protected override void R_Deleting(GSM05500DTO poEntity)
        {
            using var activity = _activitySource.StartActivity(nameof(R_Deleting));
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string lcAction = "";
          

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_GS_MAINTAIN_CURRENCY";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 10, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_NAME", DbType.String, 60, poEntity.CCURRENCY_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCURRENCY_CODE" ||
                        x.ParameterName == "@CCURRENCY_NAME" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CACTION").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || Currency(Cls) ", lcQuery, loDbParam);
                //loDb.SqlExecNonQuery(loConn, loCommand, true);
                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
