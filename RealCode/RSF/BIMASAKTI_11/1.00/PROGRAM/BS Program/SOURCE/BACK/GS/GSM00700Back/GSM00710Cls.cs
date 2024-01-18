using R_BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common.DTO;
using R_CommonFrontBackAPI;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using GSM00700Back.Activity;
using GSM00700Common;

namespace GSM00700Back
{
    public class GSM00710Cls : R_BusinessObject<GSM00710DTO>
    {
        RSP_GS_MAINTAIN_CASHFLOW_GROUPResources.Resources_Dummy_Class ResourceDummyClasCashFlowGroup = new();
        RSP_GS_MAINTAIN_CASHFLOWResources.Resources_Dummy_Class ResourcesDummyClassCashFlow = new();
        private LogGSM00700Common _logger;
        private readonly ActivitySource _activitySource;

        public GSM00710Cls()
        {
            _logger = LogGSM00700Common.R_GetInstanceLogger();
            _activitySource = GSM00710Activity.R_GetInstanceActivitySource();
        }

        public List<GSM00710DTO> GetCashFlowList(GSM00700DBParameter poParameter)
        {
            using var Activity = _activitySource.StartActivity(nameof(GetCashFlowList));
            R_Exception loException = new R_Exception();
            List<GSM00710DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_CASHFLOW_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 20,
                    poParameter.CCASH_FLOW_GROUP_CODE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CUSER_ID" ||
                    x.ParameterName == "@CCASH_FLOW_GROUP_CODE").Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlow(Cls)", lcQuery, poParameter);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM00710DTO>(loReturnTemp).ToList();
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

        public List<GSM00710CashFlowTypeDTO> CashFlowType(GSM00700DBParameter poParameter)
        {
            using var Activity = _activitySource.StartActivity(nameof(CashFlowType));
            R_Exception loException = new R_Exception();
            List<GSM00710CashFlowTypeDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery =
                    @"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO  ('BIMASAKTI', @CCOMPANY_ID, '_CASH_FLOW_TYPE', '', 'Login User Language')";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10,
                    poParameter.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID").Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlow(Cls)", lcQuery, poParameter);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00710CashFlowTypeDTO>(loReturnTemp).ToList();
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


        protected override GSM00710DTO R_Display(GSM00710DTO poEntity)
        {
            using var Activity = _activitySource.StartActivity(nameof(R_Display));
            R_Exception loException = new R_Exception();
            GSM00710DTO loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_CASHFLOW_DT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 20,
                    poEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", DbType.String, 20, poEntity.CCASH_FLOW_CODE);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CUSER_ID" ||
                    x.ParameterName == "@CCASH_FLOW_GROUP_CODE" ||
                    x.ParameterName == "@CCASH_FLOW_CODE").Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlow(Cls)", lcQuery, poEntity);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM00710DTO>(loDataTable).FirstOrDefault();
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

        protected override void R_Saving(GSM00710DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using var Activity = _activitySource.StartActivity(nameof(R_Saving));
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            string lcAction = "";


            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    lcAction = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    lcAction = "EDIT";
                }

                lcQuery = "RSP_GS_MAINTAIN_CASHFLOW";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 20,
                    poNewEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", DbType.String, 20, poNewEntity.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_NAME", DbType.String, 100, poNewEntity.CCASH_FLOW_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_TYPE", DbType.String, 60, poNewEntity.CCASH_FLOW_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 60, poNewEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CUSER_ID" ||
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CCASH_FLOW_GROUP_CODE" ||
                    x.ParameterName == "@CCASH_FLOW_CODE" ||
                    x.ParameterName == "@CCASH_FLOW_NAME" ||
                    x.ParameterName == "@CCASH_FLOW_TYPE" ||
                    x.ParameterName == "@CUSER_ID" ||
                    x.ParameterName == "@CSEQUENCE" ||
                    x.ParameterName == "@CACTION").Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlow(Cls)", lcQuery, lcAction, poNewEntity);
                //loDb.SqlExecNonQuery(loConn, loCmd, true);
                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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

        protected override void R_Deleting(GSM00710DTO poEntity)
        {
            using var Activity = _activitySource.StartActivity(nameof(R_Deleting));
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_GS_MAINTAIN_CASHFLOW";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;


                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 20,
                    poEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", DbType.String, 20, poEntity.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_NAME", DbType.String, 100, poEntity.CCASH_FLOW_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_TYPE", DbType.String, 60, poEntity.CCASH_FLOW_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 10, poEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, "DELETE");

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CUSER_ID" ||
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CCASH_FLOW_GROUP_CODE" ||
                    x.ParameterName == "@CCASH_FLOW_CODE" ||
                    x.ParameterName == "@CCASH_FLOW_NAME" ||
                    x.ParameterName == "@CCASH_FLOW_TYPE" ||
                    x.ParameterName == "@CUSER_ID" ||
                    x.ParameterName == "@CSEQUENCE" ||
                    x.ParameterName == "@CACTION").Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlow(Cls)", lcQuery, poEntity);
                //loDb.SqlExecNonQuery(loConn, loCmd, true);
                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}