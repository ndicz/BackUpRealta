using System.Data;
using System.Data.Common;
using LMM02500Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02500Back;

public class LMM02511Cls : R_BusinessObject<LMM02511DTO>
{
    protected override LMM02511DTO R_Display(LMM02511DTO poEntity)
    {
        R_Exception loException = new R_Exception();
        LMM02511DTO loReturn = null;
        R_Db loDb;
        DbCommand loCommand;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();

            loCommand = loDb.GetCommand();

            var lcQuery = @"RSP_LM_GET_TENANT_GROUP_TAX_INFO";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "CPROPERTY_ID", DbType.String, 10, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "CTENANT_GROUP_ID", DbType.String, 3, poEntity.CTENANT_GROUP_ID);
            loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

            loReturn = R_Utility.R_ConvertTo<LMM02511DTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }

    protected override void R_Saving(LMM02511DTO poNewEntity, eCRUDMode poCRUDMode)
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

            lcQuery = "RSP_LM_MAINTAIN_TENANT_GRP";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 10, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_ID", DbType.String, 60, poNewEntity.CTENANT_GROUP_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_NAME", DbType.String, 60, poNewEntity.CTENANT_GROUP_NAME);
            loDb.R_AddCommandParameter(loCommand, "@CADDRESS", DbType.String, 60, poNewEntity.CADDRESS);
            loDb.R_AddCommandParameter(loCommand, "@CPHONE1", DbType.String, 60, poNewEntity.CPHONE1);
            loDb.R_AddCommandParameter(loCommand, "@CPHONE2", DbType.String, 60, poNewEntity.CPHONE2);
            loDb.R_AddCommandParameter(loCommand, "@CEMAIL", DbType.String, 60, poNewEntity.CEMAIL);
            loDb.R_AddCommandParameter(loCommand, "@CPIC_NAME", DbType.String, 60, poNewEntity.CPIC_NAME);
            loDb.R_AddCommandParameter(loCommand, "@CPIC_POSITION", DbType.String, 60, poNewEntity.CPIC_POSITION);
            loDb.R_AddCommandParameter(loCommand, "@CPIC_EMAIL", DbType.String, 60, poNewEntity.CPIC_EMAIL);
            loDb.R_AddCommandParameter(loCommand, "@CPIC_MOBILE_PHONE1", DbType.String, 60, poNewEntity.CPIC_MOBILE_PHONE1);
            loDb.R_AddCommandParameter(loCommand, "@CPIC_MOBILE_PHONE2", DbType.String, 60, poNewEntity.CPIC_MOBILE_PHONE2);
            loDb.R_AddCommandParameter(loCommand, "@LUSE_GROUP_TAX", DbType.Boolean, 1, poNewEntity.LUSE_GROUP_TAX);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
            
            loDb.R_AddCommandParameter(loCommand, "@CTAX_CODE", DbType.String, 2, poNewEntity.CTAX_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_TYPE", DbType.String, 2, poNewEntity.CTAX_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_ID", DbType.String, 40, poNewEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_NAME", DbType.String, 100, poNewEntity.CTAX_NAME);
            loDb.R_AddCommandParameter(loCommand, "@LPPH", DbType.Boolean, 2, poNewEntity.LPPH);
            loDb.R_AddCommandParameter(loCommand, "@CID_NO", DbType.String, 40, poNewEntity.CID_NO);
            loDb.R_AddCommandParameter(loCommand, "@CID_TYPE", DbType.String, 2, poNewEntity.CID_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@CID_EXPIRED_DATE", DbType.String, 20, poNewEntity.CID_EXPIRED_DATE);
            loDb.R_AddCommandParameter(loCommand, "@NTAX_CODE_LIMIT_AMOUNT", DbType.Decimal, 18, poNewEntity.NTAX_CODE_LIMIT_AMOUNT);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_ADDRESS", DbType.String, 255, poNewEntity.CTAX_ADDRESS);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE1", DbType.String, 30, poNewEntity.CTAX_PHONE1);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE2", DbType.String, 30, poNewEntity.CTAX_PHONE2);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_EMAIL", DbType.String, 100, poNewEntity.CTAX_EMAIL);
            
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

    protected override void R_Deleting(LMM02511DTO poEntity)
    {
        throw new NotImplementedException();
    }
    
    public List<LMM02511DTO> GetTaxType(LMM02500DBParameter poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM02511DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuerry = @"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '@CCOMPANY_ID', ' _BS_TAX_TYPE', '', 'en')";
            loCmd.CommandType = System.Data.CommandType.Text;
            loCmd.CommandText = lcQuerry;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
            loReturn = R_Utility.R_ConvertTo<LMM02511DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;

    }
    public List<LMM02511DTO> GetIDType(LMM02500DBParameter poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM02511DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuerry = @"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '@CCOMPANY_ID', '_BS_ID_TYPE', '', 'en')";
            loCmd.CommandType = System.Data.CommandType.Text;
            loCmd.CommandText = lcQuerry;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
            loReturn = R_Utility.R_ConvertTo<LMM02511DTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loReturn;

    }
    public List<LMM02511DTO> GetTaxCode(LMM02500DBParameter poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM02511DTO> loReturn = null;
        R_Db loDb;
        DbCommand loCmd;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            var lcQuerry = @"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '@CCOMPANY_ID', ' _BS_TAX_TYPE', '', 'en')";
            loCmd.CommandType = System.Data.CommandType.Text;
            loCmd.CommandText = lcQuerry;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
            loReturn = R_Utility.R_ConvertTo<LMM02511DTO>(loReturnTemp).ToList();
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