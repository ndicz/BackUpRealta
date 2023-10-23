using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using GSM05500Common;
using GSM05500Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using static GSM05500Common.DTO.GSM05510ListDTO;

namespace GSM05500Back
{
    public class GSM05510Cls : R_BusinessObject<GSM05510DTO>
    {
        private LogGSM05500Common _logger;
        public GSM05510Cls()
        {
            _logger = LogGSM05500Common.R_GetInstanceLogger();
        }
        public List<GSM05510DTO> GetAllRateType(GSM05500DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM05510DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CURRENCY_TYPE_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
               
                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || RateType(Cls) ", lcQuery, poParameter);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM05510DTO>(loReturnTemp).ToList();

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
        protected override GSM05510DTO R_Display(GSM05510DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM05510DTO loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CURRENCY_TYPE_DETAIL";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_CODE", DbType.String, 8, poEntity.CRATETYPE_CODE);
                
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CRATETYPE_CODE").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || RateType(Cls) ", lcQuery, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM05510DTO>(loDataTable).FirstOrDefault();
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

        protected override void R_Saving(GSM05510DTO poNewEntity, eCRUDMode poCRUDMode)
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

                lcQuery = "RSP_GS_MAINTAIN_RATE_TYPE";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_CODE", DbType.String, 8, poNewEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_DESCRIPTION", DbType.String, 80, poNewEntity.CRATETYPE_DESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CRATETYPE_CODE" ||
                        x.ParameterName == "@CRATETYPE_DESCRIPTION" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CACTION").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || RateType(Cls) ", lcQuery, loDbParam);
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

        protected override void R_Deleting(GSM05510DTO poEntity)
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

                lcQuery = "RSP_GS_MAINTAIN_RATE_TYPE";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;


                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_CODE", DbType.String, 8, poEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_DESCRIPTION", DbType.String, 80, poEntity.CRATETYPE_DESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");

                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CRATETYPE_CODE" ||
                        x.ParameterName == "@CRATETYPE_DESCRIPTION" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CACTION").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || RateType(Cls) ", lcQuery, loDbParam);
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
