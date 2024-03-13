using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GSM10500Back.Activity;
using GSM10500Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM10500Back;

public class GSM10500Cls : R_BusinessObject<GSM10500DTO>
{
    RSP_GS_MAINTAIN_AGEING_HDResources.Resources_Dummy_Class ResourcesDummyClass = new();
    RSP_GS_MAINTAIN_AGEING_DTResources.Resources_Dummy_Class ResourcesDummyClass2 = new();
    private LogGSM10500Common _logger;
    private readonly ActivitySource _activitySource;

    public GSM10500Cls()
    {
        _logger = LogGSM10500Common.R_GetInstanceLogger();
        _activitySource= GSM10500Activity.R_GetInstanceActivitySource();
    }
    
    protected override GSM10500DTO R_Display(GSM10500DTO poEntity)
    {
        using var activity = _activitySource.StartActivity(nameof(R_Display));
        R_Exception loException = new R_Exception();
        GSM10500DTO loReturn = null;
        R_Db loDb;
        DbCommand loCommand;

        try
        {
            // poEntity.CHOLIDAY_DATE = poEntity.DHOLIDAY_DATE.ToString("yyyyMMdd");
            loDb = new R_Db();
            var loConn = loDb.GetConnection();

            loCommand = loDb.GetCommand();

            var lcQuery = @"RSP_GS_GET_HOLIDAY_HD";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 100, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "CAGEING_CODE", DbType.String, 8, poEntity.CAGEING_CODE);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CUSER_ID" ||
                    x.ParameterName == "@CAGEING_CODE").
                Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || Ageing(Cls) ", lcQuery, poEntity);

            var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

            loReturn = R_Utility.R_ConvertTo<GSM10500DTO>(loDataTable).FirstOrDefault();
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

    protected override void R_Deleting(GSM10500DTO poEntity)
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
        
            lcQuery = "RSP_GS_MAINTAIN_AGEING_HD";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;
        
            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CAGEING_CODE", DbType.String, 8, poEntity.CAGEING_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CAGEING_NAME", DbType.String, 100, poEntity.CAGEING_NAME);
            loDb.R_AddCommandParameter(loCommand, "@CROUNDING_MODE", DbType.String, 10, poEntity.CROUNDING_MODE);
            loDb.R_AddCommandParameter(loCommand, "@IROUNDING", DbType.Int32, 100, poEntity.IROUNDING);
            loDb.R_AddCommandParameter(loCommand, "@IDIGIT_LENGTH", DbType.Int32, 10, poEntity.IDIGIT_LENGTH);
            loDb.R_AddCommandParameter(loCommand, "@IDECIMAL_POINT", DbType.Int32, 10, poEntity.IDECIMAL_POINT);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
            var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CAGEING_CODE" ||
                    x.ParameterName == "@CAGEING_NAME" ||
                    x.ParameterName == "@CROUNDING_MODE" ||
                    x.ParameterName == "@IROUNDING" ||
                    x.ParameterName == "@IDIGIT_LENGTH" ||
                    x.ParameterName == "@IDECIMAL_POINT" ||
                    x.ParameterName == "@CACTION" ||
                    x.ParameterName == "@CUSER_ID").
                Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || Ageing(Cls) ", lcQuery, poEntity);
        
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

    protected override void R_Saving(GSM10500DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        var loEx = new R_Exception();
        string lcQuery = "";
        var loDb = new R_Db();
        DbConnection loConn = null;
        DbCommand loCmd = null;
        string lcAction = "";
        
        try
        {
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();


                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    lcAction = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    lcAction = "EDIT";
                }

                lcQuery = "EXEC RSP_GS_MAINTAIN_AGEING_HD @CCOMPANY_ID, @CAGEING_CODE, @CAGEING_NAME, @CROUNDING_MODE, @IROUNDING, @IDIGIT_LENGTH, @IDECIMAL_POINT, @CACTION, @CUSER_ID";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CAGEING_CODE", DbType.String, 8, poNewEntity.CAGEING_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CAGEING_NAME", DbType.String, 100, poNewEntity.CAGEING_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CROUNDING_MODE", DbType.String, 10, poNewEntity.CROUNDING_MODE);
                loDb.R_AddCommandParameter(loCmd, "@IROUNDING", DbType.Int32, 100, poNewEntity.IROUNDING);
                loDb.R_AddCommandParameter(loCmd, "@IDIGIT_LENGTH", DbType.Int32, 10, poNewEntity.IDIGIT_LENGTH);
                loDb.R_AddCommandParameter(loCmd, "@IDECIMAL_POINT", DbType.Int32, 10, poNewEntity.IDECIMAL_POINT);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug(lcQuery,  loDbParam);
                
                R_ExternalException.R_SP_Init_Exception(loConn);
                
                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(ex);
            }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != System.Data.ConnectionState.Closed)
                    loConn.Close();

                loConn.Dispose();
                loConn = null;
            }
            if (loCmd != null)
            {
                loCmd.Dispose();
                loCmd = null;
            }
            if (loDb != null)
            {
                loDb = null;
            }
        }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
    }
    
    public List<GSM10500DTO> GetAllAgeingHD(GSM10500DBParameter poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(GetAllAgeingHD));
        R_Exception loException = new R_Exception();
        List<GSM10500DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();

            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_GS_GET_AGEING_HD_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 100, poParameter.CUSER_ID);
            var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID"||
                    x.ParameterName == "@CUSER_ID").
                Select(x => x.Value);
            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GSM10500DTO>(loReturnTemp).ToList();
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
    public List<GSM10500RoundingModeDTO> GetAllRoundingMode(GSM10500DBParameter poParameter)
    {
        using var Activity = _activitySource.StartActivity(nameof(GetAllRoundingMode));
        R_Exception loException = new R_Exception();
        List<GSM10500RoundingModeDTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = @"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', @CCOMPANY_ID, '_ROUNDING_MODE', '', @CULTURE)";
            loCmd.CommandType = System.Data.CommandType.Text;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CULTURE", System.Data.DbType.String, 100, poParameter.CULTURE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CULTURE").
                Select(x => x.Value);


            _logger.LogDebug("EXEC {Query} {@Parameters} || CashFlowGroupType(Cls) ", lcQuery, poParameter);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
            loReturn = R_Utility.R_ConvertTo<GSM10500RoundingModeDTO>(loReturnTemp).ToList();
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
}