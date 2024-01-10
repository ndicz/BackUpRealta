using System.Data;
using System.Data.Common;
using LMM03700Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM03700Back;

public class LMM03710Cls : R_BusinessObject<LMM03710DTO>
{
    protected override LMM03710DTO R_Display(LMM03710DTO poEntity)
    {
        R_Exception loException = new R_Exception();
        LMM03710DTO loReturn = null;
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

            loReturn = R_Utility.R_ConvertTo<LMM03710DTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }

    protected override void R_Saving(LMM03710DTO poNewEntity, eCRUDMode poCRUDMode)
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

            lcQuery = "RSP_LM_MAINTAIN_TENANT_CLASS";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;


            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, 20,
                poNewEntity.CTENANT_CLASSIFICATION_GROUP_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_ID", DbType.String, 20,
                poNewEntity.CTENANT_CLASSIFICATION_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_NAME", DbType.String, 100,
                poNewEntity.CTENANT_CLASSIFICATION_NAME);
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
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

    protected override void R_Deleting(LMM03710DTO poEntity)
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

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, 50,
                poEntity.CTENANT_CLASSIFICATION_GROUP_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_ID", DbType.String, 50,
                poEntity.CTENANT_CLASSIFICATION_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_CLASSIFICATION_NAME", DbType.String, 50,
                poEntity.CTENANT_CLASSIFICATION_NAME);
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 50, "DELETE");
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
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

    #region GetListAll

    public List<LMM03710DTO> GetTenanClasificationGroupList(LMM03700DBPamater poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM03710DTO> loReturn = null;
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

            loReturn = R_Utility.R_ConvertTo<LMM03710DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public List<LMM03710DTO> GetTenanClasificationList(LMM03700DBPamater poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM03710DTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = "RSP_LM_GET_TENANT_CLASS_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, 10,
                poParameter.CTENANT_CLASSIFICATION_GROUP_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<LMM03710DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public List<LMM03710DTO> GetTenantList(LMM03700DBPamater poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM03710DTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = "RSP_LM_GET_TENANT_CLASS_TENANT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_ID", DbType.String, 10,
                poParameter.CTENANT_CLASSIFICATION_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<LMM03710DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    #endregion

    #region AssignTenant

    public List<LMM03710DTO> AssignAvailableTenantList(LMM03700DBPamater poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM03710DTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = "RSP_LM_GET_ASSIGN_TENANT_CLASS_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, 10,
                poParameter.CTENANT_CLASSIFICATION_GROUP_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_ID", DbType.String, 10,
                poParameter.CTENANT_CLASSIFICATION_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<LMM03710DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public List<LMM03710DTO> AssignSelectedTenantList(LMM03700DBPamater poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM03710DTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = "RSP_LM_GET_TENANT_CLASS_TENANT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_ID", DbType.String, 10,
                poParameter.CTENANT_CLASSIFICATION_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<LMM03710DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
    
     public void AssignTenantProecess(LMM03700ParamDTO poParam)
    {
        R_Exception loException = new R_Exception();
        List<LMM03710DTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn = null;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            R_ExternalException.R_SP_Init_Exception(loConn);
            string lcQuery = "RSP_LM_ASSIGN_TENANT_CLASS";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, Int32.MaxValue, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, Int32.MaxValue, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_ID", DbType.String, Int32.MaxValue, poParam.CTENANT_CLASSIFICATION_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, Int32.MaxValue, poParam.CTENANT_CLASSIFICATION_GROUP_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_LIST", DbType.String, Int32.MaxValue, poParam.CTENANT_ID_LIST_COMMA_SEPARATOR);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, Int32.MaxValue, poParam.CUSER_ID);

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

    #endregion


    #region MoveRegion

    public List<LMM03710DTO> MoveTenantDropown(LMM03700DBPamater poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM03710DTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = "RSP_LM_GET_TENANT_CLASS_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_GRP_ID", DbType.String, 10,
                poParameter.CTENANT_CLASSIFICATION_GROUP_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<LMM03710DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public List<LMM03710DTO> MoveGetTenantList(LMM03700DBPamater poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM03710DTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuery = "RSP_LM_GET_TENANT_LIST_CLASS";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_ID", DbType.String, 10,
                poParameter.CTENANT_CLASSIFICATION_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<LMM03710DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }

    public void MoveTenantProecess(LMM03700ParamDTO poParam)
    {
        R_Exception loException = new R_Exception();
        List<LMM03710DTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn = null;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            R_ExternalException.R_SP_Init_Exception(loConn);
            string lcQuery = "RSP_LM_MOVE_TENANT_CLASS";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, Int32.MaxValue, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, Int32.MaxValue, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_CLASSIFICATION_GROUP_ID", DbType.String, Int32.MaxValue,
                poParam.CTENANT_CLASSIFICATION_GROUP_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_TENANT_CLASSIFICATION_ID", DbType.String, Int32.MaxValue,
                poParam.CFROM_TENANT_CLASSIFICATION_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTO_TENANT_CLASSIFICATION_ID", DbType.String, Int32.MaxValue,
                poParam.CTO_TENANT_CLASSIFICATION_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_LIST", DbType.String, Int32.MaxValue,
                poParam.CTENANT_ID_LIST_COMMA_SEPARATOR);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, Int32.MaxValue, poParam.CUSER_ID);

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
    #endregion
}