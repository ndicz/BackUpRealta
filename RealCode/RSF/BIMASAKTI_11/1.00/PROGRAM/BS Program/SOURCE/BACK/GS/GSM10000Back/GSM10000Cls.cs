using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GSM10000Back.Activity;
using GSM10000Common;
using GSM10000Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM10000Back;

public class GSM10000Cls : R_BusinessObject<GSM10000DTO>
{
    RSP_GS_MAINTAIN_HOLIDAYResources.Resources_Dummy_Class ResourcesDummyClass = new();
    private LogGSM10000Common _logger;
    private readonly ActivitySource _activitySource;
    public GSM10000Cls()
    {
        _logger = LogGSM10000Common.R_GetInstanceLogger();
        _activitySource= GSM10000Activity.R_GetInstanceActivitySource();
    }
    protected override GSM10000DTO R_Display(GSM10000DTO poEntity)
    {
        using var activity = _activitySource.StartActivity(nameof(R_Display));
        R_Exception loException = new R_Exception();
        GSM10000DTO loReturn = null;
        R_Db loDb;
        DbCommand loCommand;

        try
        {
            // poEntity.CHOLIDAY_DATE = poEntity.DHOLIDAY_DATE.ToString("yyyyMMdd");
            loDb = new R_Db();
            var loConn = loDb.GetConnection();

            loCommand = loDb.GetCommand();

            var lcQuery = @"RSP_GS_GET_HOLIDAY_DETAIL";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "CHOLIDAY_DATE", DbType.String, 100, poEntity.CHOLIDAY_DATE);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CHOLIDAY_DATE").
                Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || Holidays(Cls) ", lcQuery, poEntity);

            var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

            loReturn = R_Utility.R_ConvertTo<GSM10000DTO>(loDataTable).FirstOrDefault();
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

    protected override void R_Deleting(GSM10000DTO poEntity)
    {
        throw new NotImplementedException();
        // using var activity = _activitySource.StartActivity(nameof(R_Deleting));
        // R_Exception loException = new R_Exception();
        // string lcQuery = null;
        // R_Db loDb;
        // DbCommand loCommand;
        // DbConnection loConn = null;
        // string lcAction = "";
        //
        //
        // try
        // {
        //     loDb = new R_Db();
        //     loConn = loDb.GetConnection();
        //     loCommand = loDb.GetCommand();
        //     R_ExternalException.R_SP_Init_Exception(loConn);
        //
        //     lcQuery = "RSP_GS_MAINTAIN_HOLIDAY";
        //     loCommand.CommandType = CommandType.StoredProcedure;
        //     loCommand.CommandText = lcQuery;
        //
        //     loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
        //     loDb.R_AddCommandParameter(loCommand, "@CHOLIDAY_DATE", DbType.String, 10, poEntity.CHOLIDAY_DATE);
        //     loDb.R_AddCommandParameter(loCommand, "@CHOLIDAY_NAME", DbType.String, 100, poEntity.CHOLIDAY_NAME);
        //     loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
        //     loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
        //     var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
        //             x.ParameterName == "@CCOMPANY_ID" ||
        //             x.ParameterName == "@CHOLIDAY_DATE" ||
        //             x.ParameterName == "@CHOLIDAY_NAME" ||
        //             x.ParameterName == "@CUSER_ID" ||
        //             x.ParameterName == "@CUSER_ID").
        //         Select(x => x.Value);
        //     _logger.LogDebug("EXEC {Query} {@Parameters} || Holidays(Cls) ", lcQuery, poEntity);
        //
        //     //loDb.SqlExecNonQuery(loConn, loCommand, true);
        //     try
        //     {
        //         loDb.SqlExecNonQuery(loConn, loCommand, false);
        //     }
        //     catch (Exception ex)
        //     {
        //         loException.Add(ex);
        //     }
        //
        //     loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
        // }
        // catch (Exception ex)
        // {
        //     loException.Add(ex);
        //     _logger.LogError(ex);
        // }
        //
        // loException.ThrowExceptionIfErrors();
    }

    protected override void R_Saving(GSM10000DTO poNewEntity, eCRUDMode poCRUDMode)
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
            poNewEntity.CHOLIDAY_DATE = poNewEntity.DHOLIDAY_DATE.ToString("yyyyMMdd");

            if (poCRUDMode == eCRUDMode.AddMode)
            {
                lcAction = "ADD";
            }
            else if (poCRUDMode == eCRUDMode.EditMode)
            {
                lcAction = "EDIT";
            }

            lcQuery = "RSP_GS_MAINTAIN_HOLIDAY";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CHOLIDAY_DATE", DbType.String, 10, poNewEntity.CHOLIDAY_DATE);
            loDb.R_AddCommandParameter(loCommand, "@CHOLIDAY_NAME", DbType.String, 100, poNewEntity.CHOLIDAY_NAME);
            // loDb.R_AddCommandParameter(loCommand, "@LACTIVE", DbType.String, 10, poNewEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CHOLIDAY_DATE" ||
                    // x.ParameterName == "@LACTIVE" ||
                    x.ParameterName == "@CHOLIDAY_NAME" ||
                    x.ParameterName == "@CUSER_ID" ||
                    x.ParameterName == "@CACTION").
                Select(x => x.Value);
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

    public List<GSM10000DTO> GetAllHoliday(GSM10000DBParameter poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(GetAllHoliday));
        R_Exception loException = new R_Exception();
        List<GSM10000DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();

            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_GS_GET_HOLIDAY_LIST ";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID").
                Select(x => x.Value);
            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GSM10000DTO>(loReturnTemp).ToList();
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

    public void ActiveInactive(GSM10000DBParameter poEntity)
    {
        using var activity = _activitySource.StartActivity(nameof(GetAllHoliday));
        R_Exception loex = new R_Exception();
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn;
        try
        {
            loDb = new R_Db(); 
            loConn = loDb.GetConnection("R_DefaultConnectionString");
            loCmd = loDb.GetCommand();
            string lcQuery = "RSP_GS_ACTIVE_INACTIVE_HOLIDAY";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CHOLIDAY_DATE", DbType.String, 20, poEntity.CHOLIDAY_DATE);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 2, poEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
            var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "CCOMPANY_ID" ||
                    x.ParameterName == "CHOLIDAY_DATE" ||
                    x.ParameterName == "LACTIVE" ||
                    x.ParameterName == "CUSER_ID").
                Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || Holiday(Cls) ", lcQuery, poEntity);


            loDb.SqlExecNonQuery(loConn, loCmd, true);

        }
        catch (Exception ex)
        {
            loex.Add(ex);
            _logger.LogError(ex);
        }

        EndBlock:
        loex.ThrowExceptionIfErrors();
    }
}