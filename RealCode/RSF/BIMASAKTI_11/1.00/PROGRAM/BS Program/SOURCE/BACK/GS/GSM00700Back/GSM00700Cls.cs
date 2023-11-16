using R_BackEnd;
using R_CommonFrontBackAPI;
using GSM00700Common.DTO;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Reflection.Metadata;
using GSM00700Common.DTO.Report_DTO_GSM00700;
using System.Windows.Input;
using GSM00700Common;

namespace GSM00700Back
{
    public class GSM00700Cls : R_BusinessObject<GSM00700DTO>
    {

        private LogGSM00700Common _logger;
        public GSM00700Cls()
        {
            _logger = LogGSM00700Common.R_GetInstanceLogger();
        }


        public List<GSM00700DTO> GetCashFlowGroupList(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00700DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CASHFLOW_GRP_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x => 
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID").
                    Select(x => x.Value);
                      

                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowGroup", lcQuery, poParameter);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM00700DTO>(loReturnTemp).ToList();

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

        public List<GSM00700CashFlowGroupTypeDTO> CashFlowGroupType(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00700CashFlowGroupTypeDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO  ('BIMASAKTI', @CCOMPANY_ID, '_CASH_FLOW_GROUP_TYPE', '', 'Login User Language')";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID").
                    Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowGroupType(Cls) ", lcQuery, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00700CashFlowGroupTypeDTO>(loReturnTemp).ToList();
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


        protected override GSM00700DTO R_Display(GSM00700DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM00700DTO loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_CASHFLOW_GRP_DT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "CCASH_FLOW_GROUP_CODE", DbType.String, 20, poEntity.CCASH_FLOW_GROUP_CODE);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP_CODE").
                    Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowGroup", lcQuery, poEntity);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM00700DTO>(loDataTable).FirstOrDefault();
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

        protected override void R_Deleting(GSM00700DTO poEntity)
        {
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


                lcQuery = "RSP_GS_MAINTAIN_CASHFLOW_GROUP";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 10, poEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_NAME", DbType.String, 60, poEntity.CCASH_FLOW_GROUP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_TYPE", DbType.String, 60, poEntity.CCASH_FLOW_GROUP_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, "DELETE");

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP_CODE" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP_NAME" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP_TYPE" ||
                        x.ParameterName == "@CACTION" ||
                        x.ParameterName == "@CUSER_ID").
                    Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowGroup", lcQuery, poEntity);

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

            loException.ThrowExceptionIfErrors();
        }


        protected override void R_Saving(GSM00700DTO poNewEntity, eCRUDMode poCRUDMode)
        {
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

                lcQuery = "RSP_GS_MAINTAIN_CASHFLOW_GROUP";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;


                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 10, poNewEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_NAME", DbType.String, 60, poNewEntity.CCASH_FLOW_GROUP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_TYPE", DbType.String, 60, poNewEntity.CCASH_FLOW_GROUP_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP_CODE" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP_NAME" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP_TYPE" ||
                        x.ParameterName == "@CACTION" ||
                        x.ParameterName == "@CUSER_ID").
                    Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowGroup(Cls)", lcQuery, poNewEntity,lcAction);

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

        public List<GSM00700DTO> YearFromComboBoxPrint(GSM00700DBParameter poParameter)
        {
            R_Exception loEx = new R_Exception();
            List<GSM00700DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PERIOD_YEAR_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowGroup(Cls)", lcQuery, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM00700DTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<GSM00700DTO> YearToComboBoxPrint(GSM00700DBParameter poParameter)
        {
            R_Exception loEx = new R_Exception();
            List<GSM00700DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PERIOD_YEAR_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID").
                    Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowGroup", lcQuery, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00700DTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(ex);   
            }

            loEx.ThrowExceptionIfErrors();

            return loReturn;
        }



        public List<GSM00700DTO> GetPrintParam(GSM00700PrintCashFlowParameterDTo poParameter)
        {
            R_Exception loEx = new R_Exception();
            List<GSM00700DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_PRINT_CASHFLOW";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_LOGIN_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_FROM", DbType.String, 10, poParameter.CCASH_FLOW_GROUP_FROM);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_TO", DbType.String, 10, poParameter.CCASH_FLOW_GROUP_TO);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR_FROM", DbType.String, 10, poParameter.CYEAR_FROM);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR_TO", DbType.String, 10, poParameter.CYEAR_TO);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_FROM", DbType.String, 10, poParameter.CPERIOD_FROM);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_TO", DbType.String, 10, poParameter.CPERIOD_TO);
                loDb.R_AddCommandParameter(loCmd, "@CSHORT_BY", DbType.String, 10, poParameter.CSHORT_BY);
                loDb.R_AddCommandParameter(loCmd, "@LPRINT_LOCAL", DbType.String, 10, poParameter.LPRINT_LOCAL);
                loDb.R_AddCommandParameter(loCmd, "@LPRINT_BASE", DbType.String, 10, poParameter.LPRINT_BASE);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM00700DTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loReturn;
        }
    }
}
