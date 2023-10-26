using System.Data.Common;
using LMI00100Common;
using LMI00100Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMI00100Back
{
    public class LMI00100Cls : R_BusinessObject<LMI00100Cls>
    {
        private LogLMI00100Common _logger;
        public LMI00100Cls()
        {
            _logger = LogLMI00100Common.R_GetInstanceLogger();
        }
        public List<LMI00100PropertyDTO> GetAllPropertyList(LMI00100DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMI00100PropertyDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
                loCommand.CommandType = System.Data.CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50, poParameter.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || VaBankChannel(Cls) ", lcQuery, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMI00100PropertyDTO>(loReturnTemp).ToList();
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

        public List<LMI00100DTO> GetAllBankChannelList(LMI00100DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMI00100DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_LM_GET_VA_BANK_CHANNEL_LIST";
                loCommand.CommandType = System.Data.CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 50, poParameter.CPROPERTY_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CPROPERTY_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || VaBankChannel(Cls) ", lcQuery, loDbParam);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMI00100DTO>(loReturnTemp).ToList();
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
        protected override LMI00100Cls R_Display(LMI00100Cls poEntity)
        {
            throw new NotImplementedException();
        }

        protected override void R_Deleting(LMI00100Cls poEntity)
        {
            throw new NotImplementedException();
        }

        protected override void R_Saving(LMI00100Cls poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }
    }
}