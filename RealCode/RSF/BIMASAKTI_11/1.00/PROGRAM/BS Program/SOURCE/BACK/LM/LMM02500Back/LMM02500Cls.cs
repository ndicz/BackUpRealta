using System.Data;
using System.Data.Common;
using LMM02500Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02500Back
{
    public class LMM02500Cls : R_BusinessObject<LMM02500DTO>
    {
         public List<LMM02500InitialProcessDTO> GetInitialProcess(LMM02500DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM02500InitialProcessDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", System.Data.DbType.String, 10, poParameter.CUSER_ID);
                
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<LMM02500InitialProcessDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        public List<LMM02500DTO> GetTenantGroupList(LMM02500DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM02500DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_LM_GET_TENANT_GROUP_LIST";
                loCmd.CommandType = System.Data.CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", System.Data.DbType.String, 10, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", System.Data.DbType.String, 50, poParameter.CPROPERTY_ID);
                
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<LMM02500DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }
        protected override LMM02500DTO R_Display(LMM02500DTO poEntity)
        {
       
            R_Exception loException = new R_Exception();
            LMM02500DTO loReturn = null;
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

                loReturn = R_Utility.R_ConvertTo<LMM02500DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }
            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        protected override void R_Saving(LMM02500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

        protected override void R_Deleting(LMM02500DTO poEntity)
        {
            throw new NotImplementedException();
        }

       
    }
}