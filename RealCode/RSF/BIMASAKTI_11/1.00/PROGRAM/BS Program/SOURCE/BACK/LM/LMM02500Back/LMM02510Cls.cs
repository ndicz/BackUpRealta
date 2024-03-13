using System.Data;
using System.Data.Common;
using LMM02500Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02500Back;

public class LMM02510Cls : R_BusinessObject<LMM02510DTO>
{
    protected override LMM02510DTO R_Display(LMM02510DTO poEntity)
    {
        R_Exception loException = new R_Exception();
        LMM02510DTO loReturn = null;
        R_Db loDb;
        DbCommand loCommand;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();

            loCommand = loDb.GetCommand();

            var lcQuery = @"RSP_LM_GET_TENANT_GROUP_DETAIL";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;
            loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "CPROPERTY_ID", DbType.String, 10, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "CTENANT_GROUP_ID", DbType.String, 3, poEntity.CTENANT_GROUP_ID);
            loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

            loReturn = R_Utility.R_ConvertTo<LMM02510DTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }

    protected override void R_Saving(LMM02510DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        throw new NotImplementedException();
       
    }

    protected override void R_Deleting(LMM02510DTO poEntity)
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

            lcQuery = "RSP_LM_MAINTAIN_TENANT_GRP";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 10, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_ID", DbType.String, 60, poEntity.CTENANT_GROUP_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTENANT_GROUP_NAME", DbType.String, 60, poEntity.CTENANT_GROUP_NAME);
            loDb.R_AddCommandParameter(loCommand, "@CADDRESS", DbType.String, 60, poEntity.CADDRESS);
            loDb.R_AddCommandParameter(loCommand, "@CPHONE1", DbType.String, 60, poEntity.CPHONE1);
            loDb.R_AddCommandParameter(loCommand, "@CPHONE2", DbType.String, 60, poEntity.CPHONE2);
            loDb.R_AddCommandParameter(loCommand, "@CEMAIL", DbType.String, 60, poEntity.CEMAIL);
            loDb.R_AddCommandParameter(loCommand, "@CPIC_NAME", DbType.String, 60, poEntity.CPIC_NAME);
            loDb.R_AddCommandParameter(loCommand, "@CPIC_POSITION", DbType.String, 60, poEntity.CPIC_POSITION);
            loDb.R_AddCommandParameter(loCommand, "@CPIC_EMAIL", DbType.String, 60, poEntity.CPIC_EMAIL);
            loDb.R_AddCommandParameter(loCommand, "@CPIC_MOBILE_PHONE1", DbType.String, 60, poEntity.CPIC_MOBILE_PHONE1);
            loDb.R_AddCommandParameter(loCommand, "@CPIC_MOBILE_PHONE2", DbType.String, 60, poEntity.CPIC_MOBILE_PHONE2);
            loDb.R_AddCommandParameter(loCommand, "@LUSE_GROUP_TAX", DbType.Boolean, 1, poEntity.LUSE_GROUP_TAX);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
            
            loDb.R_AddCommandParameter(loCommand, "@CTAX_CODE", DbType.String, 2, poEntity.CTAX_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_TYPE", DbType.String, 2, poEntity.CTAX_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_ID", DbType.String, 40, poEntity.CTAX_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_NAME", DbType.String, 100, poEntity.CTAX_NAME);
            loDb.R_AddCommandParameter(loCommand, "@LPPH", DbType.Boolean, 2, poEntity.LPPH);
            loDb.R_AddCommandParameter(loCommand, "@CID_NO", DbType.String, 40, poEntity.CID_NO);
            loDb.R_AddCommandParameter(loCommand, "@CID_TYPE", DbType.String, 2, poEntity.CID_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@CID_EXPIRED_DATE", DbType.String, 20, poEntity.CID_EXPIRED_DATE);
            loDb.R_AddCommandParameter(loCommand, "@NTAX_CODE_LIMIT_AMOUNT", DbType.Decimal, 18, poEntity.NTAX_CODE_LIMIT_AMOUNT);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_ADDRESS", DbType.String, 255, poEntity.CTAX_ADDRESS);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE1", DbType.String, 30, poEntity.CTAX_PHONE1);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_PHONE2", DbType.String, 30, poEntity.CTAX_PHONE2);
            loDb.R_AddCommandParameter(loCommand, "@CTAX_EMAIL", DbType.String, 100, poEntity.CTAX_EMAIL);
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
}