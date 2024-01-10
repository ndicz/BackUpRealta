using System.Data;
using System.Data.Common;
using LMM03700Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM03700Back;

public class LMM03700Cls : R_BusinessObject<LMM03700DTO>
{
    protected override LMM03700DTO R_Display(LMM03700DTO poEntity)
    {
        R_Exception loException = new R_Exception();
        LMM03700DTO loReturn = null;
        R_Db loDb;
        DbCommand loCommand;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            var lcQuery = @"RSP_LM_GET_TENANT_CLASS_GRP_DETAIL";
            loCommand.CommandType = System.Data.CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 10, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 10,
                poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_GROUP_ID", System.Data.DbType.String, 20,
                poEntity.CTENANT_CLASSIFICATION_GROUP_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

            loReturn = R_Utility.R_ConvertTo<LMM03700DTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }

    protected override void R_Deleting(LMM03700DTO poEntity)
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

            lcQuery = "RSP_LM_MAINTAIN_TENANT_CLASS";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 10, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, 60,
                poEntity.CTENANT_CLASSIFICATION_GROUP_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_GROUP_NAME", DbType.String, 10,
                poEntity.CTENANT_CLASSIFICATION_GROUP_NAME);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
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
        }

        loException.ThrowExceptionIfErrors();
    }


    protected override void R_Saving(LMM03700DTO poNewEntity, eCRUDMode poCRUDMode)
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

            lcQuery = "RSP_LM_MAINTAIN_TENANT_CLASS_GRP";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 10, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, 60,
                poNewEntity.CTENANT_CLASSIFICATION_GROUP_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_GROUP_NAME", DbType.String, 10,
                poNewEntity.CTENANT_CLASSIFICATION_GROUP_NAME);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
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


    public List<LMM03700InitialProcessDTO> GetInitialPropertyList(LMM03700DBPamater poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM03700InitialProcessDTO> loReturn = null;
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

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50,
                poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50, poParameter.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<LMM03700InitialProcessDTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }


    public List<LMM03700DTO> GetTenanClasificationList(LMM03700DBPamater poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM03700DTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = "RSP_LM_GET_TENANT_CLASS_GRP_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<LMM03700DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
}