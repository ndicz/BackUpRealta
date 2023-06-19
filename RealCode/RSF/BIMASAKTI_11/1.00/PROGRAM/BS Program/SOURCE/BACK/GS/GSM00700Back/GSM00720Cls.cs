using GSM00700Common.DTO;
using R_BackEnd;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Reflection.Metadata;
using System.Windows.Input;

namespace GSM00700Back
{
    public class GSM00720Cls : R_BusinessObject<GSM00700DTO>
    {

        public List<GSM00720DTO> GetCashFlowPlan(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CASHFLOW_PLAN_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 10, poParameter.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", DbType.String, 10, poParameter.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 10, poParameter.CYEAR);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM00720DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<GSM00720YearDTO> GetYearList(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720YearDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuerry = @"SELECT CYEAR FROM GSM_PERIOD WHERE CCOMPANY_ID = @CCOMPANY_ID";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuerry;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10,
                    poParameter.CCOMPANY_ID);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00720YearDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        //public List<GSM00720DTO> GetCashFlowPlanList(GSM00700DBParameter poParameter)
        //{
        //    R_Exception loException = new R_Exception();
        //    List<GSM00720DTO> loReturn = null;
        //    R_Db loDb;
        //    DbCommand loCommand;

        //    try
        //    {
        //        loDb = new R_Db();
        //        var loConn = loDb.GetConnection();

        //        loCommand = loDb.GetCommand();
        //        var lcQuery = @"RSP_GS_GET_CASHFLOW_GRP_DT";
        //        loCommand.CommandType = CommandType.StoredProcedure;
        //        loCommand.CommandText = lcQuery;
        //        loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
        //        loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
        //        loDb.R_AddCommandParameter(loCommand, "CCASH_FLOW_GROUP_CODE", DbType.String, 20, poParameter.CCASH_FLOW_GROUP_CODE
        //        );
        //        var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
        //        loReturn = R_Utility.R_ConvertTo<GSM00720DTO>(loReturnTemp).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }

        //    EndBlock:
        //    loException.ThrowExceptionIfErrors();

        //    return loReturn;
        //}
        protected override GSM00700DTO R_Display(GSM00700DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override void R_Saving(GSM00700DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            throw new NotImplementedException();
        }

        protected override void R_Deleting(GSM00700DTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}
