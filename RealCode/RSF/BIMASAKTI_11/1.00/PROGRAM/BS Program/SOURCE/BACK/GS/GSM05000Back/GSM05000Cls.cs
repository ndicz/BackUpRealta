using System.Data;
using System.Data.Common;
using GSM05000Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Back;

public class GSM05000Cls : R_BusinessObject<GSM05000SingleDTO>
{
    public List<GSM05000GridDTO> GetAllTransactionList(GSM05000DBParameter poParam)
    {
        R_Exception loException = new R_Exception();
        List<GSM05000GridDTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;
        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            var lcQuery = @"RSP_GS_GET_TRANS_CODE_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParam.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GSM05000GridDTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
    protected override GSM05000SingleDTO R_Display(GSM05000SingleDTO poEntity)
    {
        R_Exception loException = new R_Exception();
        GSM05000SingleDTO loReturn = null;
        R_Db loDb;
        DbCommand loCmd;
        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            var lcQuery = @"RSP_GS_GET_TRANS_CODE_INFO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCmd, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "CHOLIDAY_DATE", DbType.String, 100, poEntity.CTRANS_CODE);
            

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GSM05000SingleDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
        loException.Add(ex);
        }
        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }

    protected override void R_Deleting(GSM05000SingleDTO poEntity)
    {
        throw new NotImplementedException();
    }

    protected override void R_Saving(GSM05000SingleDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}