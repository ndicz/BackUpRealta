using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GSM10500Back;
using GSM10500Back.Activity;
using GSM10500Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM10510Back;

public class GSM10510Cls : R_BusinessObject<GSM10510DTO>
{
    RSP_GS_MAINTAIN_AGEING_HDResources.Resources_Dummy_Class ResourcesDummyClass = new();
    RSP_GS_MAINTAIN_AGEING_DTResources.Resources_Dummy_Class ResourcesDummyClass2 = new();
     private LogGSM10500Common _logger;
    private readonly ActivitySource _activitySource;

    public GSM10510Cls()
    {
        _logger = LogGSM10500Common.R_GetInstanceLogger();
        _activitySource= GSM10510Activity.R_GetInstanceActivitySource();
    }
    
    protected override GSM10510DTO R_Display(GSM10510DTO poEntity)
    {
        using var activity = _activitySource.StartActivity(nameof(R_Display));
        R_Exception loException = new R_Exception();
        GSM10510DTO loReturn = null;
        R_Db loDb;
        DbCommand loCommand;

        try
        {
            // poEntity.CHOLIDAY_DATE = poEntity.DHOLIDAY_DATE.ToString("yyyyMMdd");
            loDb = new R_Db();
            var loConn = loDb.GetConnection();

            loCommand = loDb.GetCommand();

            var lcQuery = @"RSP_GS_GET_AGEING_DT";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "CAGEING_CODE", DbType.String, 8, poEntity.CAGEING_CODE);
            loDb.R_AddCommandParameter(loCommand, "ICOLUMN_NO", DbType.Int32, 100, poEntity.ICOLUMN_NO);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "CAGEING_CODE" ||
                    x.ParameterName == "@CUSER_ID").
                Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || Holidays(Cls) ", lcQuery, poEntity, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

            loReturn = R_Utility.R_ConvertTo<GSM10510DTO>(loDataTable).FirstOrDefault();
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

    protected override void R_Deleting(GSM10510DTO poEntity)
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
        
            lcQuery = "RSP_GS_MAINTAIN_AGEING_DT";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;
        
            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CAGEING_CODE", DbType.String, 8, poEntity.CAGEING_CODE);
            loDb.R_AddCommandParameter(loCommand, "@ICOLUMN_NO", DbType.Int32, 100, poEntity.ICOLUMN_NO);
            loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, 10, poEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCommand, "@IFROM_DAYS", DbType.Int32, 100, poEntity.IFROM_DAYS);
            loDb.R_AddCommandParameter(loCommand, "@ITO_DAYS", DbType.Int32, 10, poEntity.ITO_DAYS);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
            var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CAGEING_CODE" ||
                    x.ParameterName == "@ICOLUMN_NO" ||
                    x.ParameterName == "@CDESCRIPTION" ||
                    x.ParameterName == "@IFROM_DAYS" ||
                    x.ParameterName == "@ITO_DAYS" ||
                    x.ParameterName == "@CACTION" ||
                    x.ParameterName == "@CUSER_ID").
                Select(x => x.Value);
            _logger.LogDebug("EXEC {Query} {@Parameters} || Holidays(Cls) ", lcQuery, poEntity, loDbParam);
        
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

    protected override void R_Saving(GSM10510DTO poNewEntity, eCRUDMode poCRUDMode)
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

                lcQuery = "RSP_GS_MAINTAIN_AGEING_DT";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CAGEING_CODE", DbType.String, 8, poNewEntity.CAGEING_CODE);
                loDb.R_AddCommandParameter(loCommand, "@ICOLUMN_NO", DbType.Int32, 100, poNewEntity.ICOLUMN_NO);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, 10, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@IFROM_DAYS", DbType.Int32, 100, poNewEntity.IFROM_DAYS);
                loDb.R_AddCommandParameter(loCommand, "@ITO_DAYS", DbType.Int32, 10, poNewEntity.ITO_DAYS);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CAGEING_CODE" ||
                        x.ParameterName == "@ICOLUMN_NO" ||
                        x.ParameterName == "@CDESCRIPTION" ||
                        x.ParameterName == "@IFROM_DAYS" ||
                        x.ParameterName == "@ITO_DAYS" ||
                        x.ParameterName == "@CACTION" ||
                        x.ParameterName == "@CUSER_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || Holidays(Cls) ", lcQuery, poNewEntity, loDbParam);
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
    
    public List<GSM10510DTO> GetAllAgeingDT(GSM10500DBParameter poParameter)
    {
        using var activity = _activitySource.StartActivity(nameof(GetAllAgeingDT));
        R_Exception loException = new R_Exception();
        List<GSM10510DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();

            loCmd = loDb.GetCommand();

            var lcQuery = @"RSP_GS_GET_AGEING_DT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CAGEING_CODE", DbType.String, 100, poParameter.CAGEING_CODE);
            var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                    x.ParameterName == "@CCOMPANY_ID"||
                    x.ParameterName == "@CUSER_ID").
                Select(x => x.Value);
            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GSM10510DTO>(loReturnTemp).ToList();
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