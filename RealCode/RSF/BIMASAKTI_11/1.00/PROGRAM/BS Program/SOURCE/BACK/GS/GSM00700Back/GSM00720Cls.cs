using GSM00700Common.DTO;
using R_BackEnd;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Reflection.Metadata;
using System.Windows.Input;
using GSM00700Common;

namespace GSM00700Back
{
    public class GSM00720Cls : R_BusinessObject<GSM00720DTO>
    {
        private LogGSM00700Common _logger;
        public GSM00720Cls()
        {
            _logger = LogGSM00700Common.R_GetInstanceLogger();
        }

        public List<GSM00720InitialProsesDTO> InitialProses(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720InitialProsesDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {

                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"SELECT TOP 1 1 AS NUM FROM GSM_COMPANY (NOLOCK)
                                 WHERE CCOMPANY_ID = @CCOMPANY_ID
                                 AND CBASE_CURRENCY_CODE <> CLOCAL_CURRENCY_CODE";

                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID").
                    Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowPlan(Cls)", lcQuery, poParameter);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00720InitialProsesDTO>(loReturnTemp).ToList();
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

        public List<GSM00720DTO> GetCashFlowPlan(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CASHFLOW_PLAN_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 10, poParameter.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", DbType.String, 10, poParameter.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 10, poParameter.CCYEAR);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CCASH_FLOW_CODE" ||
                        x.ParameterName == "@CYEAR" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP_CODE").
                    Select(x => x.Value);


                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowPlan(Cls)", lcQuery, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM00720DTO>(loReturnTemp).ToList();
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

        public List<GSM00720YearDTO> GetYearList(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720YearDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PERIOD_YEAR_LIST";
                loCmd.CommandType = System.Data.CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID").
                    Select(x => x.Value);

                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowPlan(Cls)", lcQuery, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00720YearDTO>(loReturnTemp).ToList();
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

        public List<GSM00720CopyFromYearDTO> CopyFromYear(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720CopyFromYearDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_COPY_FROM_CASHFLOW";
                loCmd.CommandType = System.Data.CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_CASH_FOW_FLAG", System.Data.DbType.String, 2, poParameter.CFROM_CASH_FOW_FLAG);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_CASH_FLOW_CODE", System.Data.DbType.String, 20, poParameter.CFROM_CASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_YEAR", System.Data.DbType.String, 4, poParameter.CFROM_YEAR);
                loDb.R_AddCommandParameter(loCmd, "@CTO_CASH_FLOW_CODE", System.Data.DbType.String, 20, poParameter.CTO_CASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_YEAR", System.Data.DbType.String, 4, poParameter.CTO_YEAR);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", System.Data.DbType.String, 10, poParameter.CUSER_ID);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CFROM_CASH_FOW_FLAG" ||
                        x.ParameterName == "@CFROM_CASH_FLOW_CODE" ||
                        x.ParameterName == "@CFROM_YEAR" ||
                        x.ParameterName == "@CTO_CASH_FLOW_CODE" ||
                        x.ParameterName == "@CTO_YEAR" ||
                        x.ParameterName == "@CUSER_LOGIN_ID").
                    Select(x => x.Value);

                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowPlan(Cls)", lcQuery, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00720CopyFromYearDTO>(loReturnTemp).ToList();
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

        public List<GSM00720CopyBaseLocalAmountDTO> UpdateBaseAmount(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720CopyBaseLocalAmountDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_UPDATE_BASE_AMOUNT_CASHFLOW_PLAN";
                loCmd.CommandType = System.Data.CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP", System.Data.DbType.String, 20, poParameter.CCASH_FLOW_GROUP);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", System.Data.DbType.String, 20, poParameter.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", System.Data.DbType.String, 4, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", System.Data.DbType.String, 20, poParameter.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_RATE", System.Data.DbType.String, 20, poParameter.CCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@INO_PERIOD_FROM", System.Data.DbType.Int32, 10, poParameter.INO_PERIOD_FROM);
                loDb.R_AddCommandParameter(loCmd, "@INO_PERIOD_TO", System.Data.DbType.Int32, 10, poParameter.INO_PERIOD_TO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", System.Data.DbType.String, 10, poParameter.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x => 
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP" ||
                        x.ParameterName == "@CCASH_FLOW_CODE" ||
                        x.ParameterName == "@CYEAR" ||
                        x.ParameterName == "@CCURRENCY_CODE" ||
                        x.ParameterName == "@CCURRENCY_RATE" ||
                        x.ParameterName == "@INO_PERIOD_FROM" ||
                        x.ParameterName == "@INO_PERIOD_TO" ||
                        x.ParameterName == "@CUSER_LOGIN_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowPlan(Cls)", lcQuery, poParameter);   

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00720CopyBaseLocalAmountDTO>(loReturnTemp).ToList();
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


        public List<GSM00720CopyBaseLocalAmountDTO> UpdateLocalAmount(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720CopyBaseLocalAmountDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_UPDATE_LOCAL_AMOUNT_CASHFLOW_PLAN";
                loCmd.CommandType = System.Data.CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP", System.Data.DbType.String, 20, poParameter.CCASH_FLOW_GROUP);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", System.Data.DbType.String, 20, poParameter.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", System.Data.DbType.String, 4, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", System.Data.DbType.String, 20, poParameter.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_RATE", System.Data.DbType.String, 20, poParameter.CCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@INO_PERIOD_FROM", System.Data.DbType.Int32, 10, poParameter.INO_PERIOD_FROM);
                loDb.R_AddCommandParameter(loCmd, "@INO_PERIOD_TO", System.Data.DbType.Int32, 10, poParameter.INO_PERIOD_TO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", System.Data.DbType.String, 10, poParameter.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x => 
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP" ||
                        x.ParameterName == "@CCASH_FLOW_CODE" ||
                        x.ParameterName == "@CYEAR" ||
                        x.ParameterName == "@CCURRENCY_CODE" ||
                        x.ParameterName == "@CCURRENCY_RATE" ||
                        x.ParameterName == "@INO_PERIOD_FROM" ||
                        x.ParameterName == "@INO_PERIOD_TO" ||
                        x.ParameterName == "@CUSER_LOGIN_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowPlan(Cls)", lcQuery, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00720CopyBaseLocalAmountDTO>(loReturnTemp).ToList();
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
        public GSM00720CurrencyDTO GetCurrency(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            GSM00720CurrencyDTO loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandType = System.Data.CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowPlan(Cls)", lcQuery, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM00720CurrencyDTO>(loReturnTemp).FirstOrDefault();

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


        //public List<GSM00720DTO> GetCashFlowPlanList(GSM00700DBParameter poParameter)
        //{
        //    R_Exception loException = new R_Exception();
        //    List<GSM00720DTO> loReturn = null;
        //    R_Db loDb;
        //    DbCommand loCommand;

        //    try
        //    {
        //        loDb = new R_Db();
        //        var loConn = loDb.GetConnection();

        //        loCommand = loDb.GetCommand();
        //        var lcQuery = @"RSP_GS_GET_CASHFLOW_GRP_DT";
        //        loCommand.CommandType = CommandType.StoredProcedure;
        //        loCommand.CommandText = lcQuery;
        //        loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
        //        loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
        //        loDb.R_AddCommandParameter(loCommand, "CCASH_FLOW_GROUP_CODE", DbType.String, 20, poParameter.CCASH_FLOW_GROUP_CODE
        //        );
        //        var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
        //        loReturn = R_Utility.R_ConvertTo<GSM00720DTO>(loReturnTemp).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }

        //    EndBlock:
        //    loException.ThrowExceptionIfErrors();

        //    return loReturn;
        //}
        protected override GSM00720DTO R_Display(GSM00720DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM00720DTO loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_CASHFLOW_PLAN_DT";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "CCASH_FLOW_GROUP_CODE", DbType.String, 20, poEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCommand, "CCASH_FLOW_CODE", DbType.String, 20, poEntity.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCommand, "CYEAR", DbType.String, 20, poEntity.CCYEAR);
                loDb.R_AddCommandParameter(loCommand, "CPERIOD_NO", DbType.String, 20, poEntity.CPERIOD_NO);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CCASH_FLOW_GROUP_CODE" ||
                        x.ParameterName == "@CCASH_FLOW_CODE" ||
                        x.ParameterName == "@CYEAR" ||
                        x.ParameterName == "@CPERIOD_NO").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowPlan(Cls)", lcQuery, poEntity);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM00720DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        protected override void R_Saving(GSM00720DTO poNewEntity, eCRUDMode poCRUDMode)
        {
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

                lcQuery = "RSP_GS_MAINTAIN_CASHFLOW_PLAN";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_GROUP_CODE", DbType.String, 20, poNewEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_CODE", DbType.String, 20, poNewEntity.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 100, poNewEntity.CCYEAR);
                loDb.R_AddCommandParameter(loCommand, "@CPERIOD_NO", DbType.String, 60, poNewEntity.CPERIOD_NO);
                loDb.R_AddCommandParameter(loCommand, "@NBASE_AMOUNT", DbType.String, 60, poNewEntity.NBASE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NLOCAL_AMOUNT", DbType.String, 60, poNewEntity.NLOCAL_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);

               var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                       x.ParameterName == "@CCOMPANY_ID" ||
                       x.ParameterName == "@CCASH_FLOW_GROUP_CODE" ||
                       x.ParameterName == "@CCASH_FLOW_CODE" ||
                       x.ParameterName == "@CYEAR" ||
                       x.ParameterName == "@CPERIOD_NO" ||
                       x.ParameterName == "@NBASE_AMOUNT" ||
                       x.ParameterName == "@NLOCAL_AMOUNT" ||
                       x.ParameterName == "@CUSER_ID").
                    Select(x => x.Value);

               _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowPlan(Cls) ", lcQuery, poNewEntity);

                //loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
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

        protected override void R_Deleting(GSM00720DTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}
