using System.Data;
using System.Data.Common;
using GSM04500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM04500Back;

public class GSM04510Cls : R_BusinessObject<GSM04510DTO>
{
    
    public List<GSM04510DTO> GetJournalGroupGOAList(GSM04500DBParameter poParameter)
    {
        R_Exception loException = new R_Exception();
        List<GSM04510DTO> loReturn = null;
        string lcQuery = null;
        DbConnection loConn;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();

            loCmd = loDb.GetCommand();
            lcQuery = @"RSP_GS_GET_JOURNAL_GRP_GOA_LIST";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.StoredProcedure;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 100, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 25, poParameter.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_TYPE", DbType.String, 10, poParameter.CJOURNAL_GROUP_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_CODE", DbType.String, 30, poParameter.CJOURNAL_GROUP_CODE);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GSM04510DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);

        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;
    }
  
    protected override GSM04510DTO R_Display(GSM04510DTO poEntity)
    {
        R_Exception loException = new R_Exception();
        GSM04510DTO loReturn = null;
        R_Db loDb;
        string lcQuery = null;
        DbCommand loCmd;
        DbConnection loConn;
        string lcActio = "";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();

            loCmd = loDb.GetCommand();
            lcQuery = @"RSP_GS_GET_JOURNAL_GRP_GOA_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 100, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_TYPE", DbType.String, 2, poEntity.CJRNGRP_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_CODE", DbType.String, 30, poEntity.CJRNGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CGOA_CODE", DbType.String, 30, poEntity.CGOA_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 20, poEntity.CUSER_LOGIN_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GSM04510DTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }

    protected override void R_Saving(GSM04510DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        R_Exception loException = new R_Exception();
        string lcQuery = null;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            R_ExternalException.R_SP_Init_Exception(loConn);

            lcAction = "EDIT";

            lcQuery = @"RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 100, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_TYPE", DbType.String, 2, poNewEntity.CJRNGRP_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 30, poNewEntity.CJRNGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CGOA_CODE", DbType.String, 30, poNewEntity.CGOA_CODE);
            loDb.R_AddCommandParameter(loCmd, "@LLDEPARTMENT_MODE", DbType.Boolean, 2, poNewEntity.LDEPARTMENT_MODE);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poNewEntity.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poNewEntity.CUSER_LOGIN_ID);
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
    

    protected override void R_Deleting(GSM04510DTO poEntity)
    {
        throw new NotImplementedException();
    }
}