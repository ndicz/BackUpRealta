using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GSM05500Back.Activity;
using GSM05500Common;
using GSM05500Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05500Back
{
    public class GSM05520Cls : R_BusinessObject<GSM05520DTO>
    {
        RSP_GS_MAINTAIN_CURRENCYResources.Resources_Dummy_Class ResourcesDummyClass = new();
        RSP_GS_MAINTAIN_RATE_TYPEResources.Resources_Dummy_Class ResourcesDummyClass2 = new();
        RSP_GS_MAINTAIN_CURRENCY_RATEResources.Resources_Dummy_Class ResourcesDummyClass3 = new();
        
        private LogGSM05500Common _logger;
        private readonly ActivitySource _activitySource;
        public GSM05520Cls()
        {
            _logger = LogGSM05500Common.R_GetInstanceLogger();
            _activitySource= GSM05520Activity.R_GetInstanceActivitySource();
        }
        public List<GSM05520DTOGetRateType> GetRateType(GSM05500DBParameter poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(GetRateType));
            R_Exception loException = new R_Exception();
            List<GSM05520DTOGetRateType> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_RATETYPE_LIST ";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CurrencyRate(Cls) ", lcQuery, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM05520DTOGetRateType>(loReturnTemp).ToList();
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

        public GSM05520DTOLocalBaseCurrency GetLocalCurrency(GSM05500DBParameter poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(GetLocalCurrency));
            R_Exception loException = new R_Exception();
            GSM05520DTOLocalBaseCurrency loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CurrencyRate(Cls) ", lcQuery, loDbParam);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM05520DTOLocalBaseCurrency>(loReturnTemp).FirstOrDefault();
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



        public List<GSM05520DTO> GetAllCurrencyRate(GSM05500DBParameter poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(GetAllCurrencyRate));
            R_Exception loException = new R_Exception();
            List<GSM05520DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CURRENCY_RATE_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 10, poParameter.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATE_DATE", DbType.String, 10, poParameter.CRATE_DATE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CRATETYPE_CODE" ||
                        x.ParameterName == "@CRATE_DATE").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CurrencyRate(Cls) ", lcQuery, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM05520DTO>(loReturnTemp).ToList();   
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
        protected override GSM05520DTO R_Display(GSM05520DTO poEntity)
        {
            using var activity = _activitySource.StartActivity(nameof(R_Display));
            R_Exception loException = new R_Exception();
            GSM05520DTO loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CURRENCY_RATE_DETAIL";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 10, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_CODE", DbType.String, 10, poEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATE_DATE", DbType.String, 10, poEntity.CRATE_DATE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CCURRENCY_CODE" ||
                        x.ParameterName == "@CRATETYPE_CODE" ||
                        x.ParameterName == "@CRATE_DATE").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CurrencyRate(Cls) ", lcQuery, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM05520DTO>(loDataTable).FirstOrDefault();
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

        protected override void R_Saving(GSM05520DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using var activity = _activitySource.StartActivity(nameof(R_Saving));
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string lcAction = "ADD";
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn); 
                
                if (poCRUDMode == eCRUDMode.EditMode)
                {
                    lcAction = "EDIT";
                }

                lcQuery = "RSP_GS_MAINTAIN_CURRENCY_RATE";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 8, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_CODE", DbType.String, 10, poNewEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATE_DATE", DbType.String, 10, poNewEntity.CRATE_DATE);
                loDb.R_AddCommandParameter(loCommand, "@NLBASE_RATE_AMOUNT", DbType.Decimal, 10, poNewEntity.NLBASE_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NLCURRENCY_RATE_AMOUNT", DbType.Decimal, 10, poNewEntity.NLCURRENCY_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NBBASE_RATE_AMOUNT", DbType.Decimal, 10, poNewEntity.NBBASE_RATE_AMOUNT);
                //loDb.R_AddCommandParameter(loCommand, "@DCREATE_DATE", DbType.DateTime, 10, DateTime.Now);
                loDb.R_AddCommandParameter(loCommand, "@NBCURRENCY_RATE_AMOUNT", DbType.Decimal, 10, poNewEntity.NBCURRENCY_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCURRENCY_CODE" ||
                        x.ParameterName == "@CRATETYPE_CODE" ||
                        x.ParameterName == "@CRATE_DATE" ||
                        x.ParameterName == "@NLBASE_RATE_AMOUNT" ||
                        x.ParameterName == "@NLCURRENCY_RATE_AMOUNT" ||
                        x.ParameterName == "@NBBASE_RATE_AMOUNT" ||
                        x.ParameterName == "@NBCURRENCY_RATE_AMOUNT" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CACTION").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CurrencyRate(Cls) ", lcQuery, loDbParam);
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


        protected override void R_Deleting(GSM05520DTO poEntity)
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

                lcQuery = "RSP_GS_MAINTAIN_CURRENCY_RATE";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 10, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_CODE", DbType.String, 8, poEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATE_DATE", DbType.String, 8, poEntity.CRATE_DATE);
                loDb.R_AddCommandParameter(loCommand, "@NLBASE_RATE_AMOUNT", DbType.Decimal, 20, poEntity.NLBASE_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NLCURRENCY_RATE_AMOUNT", DbType.Decimal, 20, poEntity.NLCURRENCY_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NBBASE_RATE_AMOUNT", DbType.Decimal, 20, poEntity.NBBASE_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NBCURRENCY_RATE_AMOUNT", DbType.Decimal, 20, poEntity.NBCURRENCY_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");

                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCURRENCY_CODE" ||
                        x.ParameterName == "@CRATETYPE_CODE" ||
                        x.ParameterName == "@CRATE_DATE" ||
                        x.ParameterName == "@NLBASE_RATE_AMOUNT" ||
                        x.ParameterName == "@NLCURRENCY_RATE_AMOUNT" ||
                        x.ParameterName == "@NBBASE_RATE_AMOUNT" ||
                        x.ParameterName == "@NBCURRENCY_RATE_AMOUNT" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CACTION").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || CurrencyRate(Cls) ", lcQuery, loDbParam);
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
