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
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 10, poParameter.CCYEAR);
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

        public List<GSM00720CopyFromYearDTO> CopyFromYear(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720CopyFromYearDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuerry = @"RSP_GS_COPY_FROM_CASHFLOW
	                              @CCOMPANY_ID				
	                            , @CFROM_CASH_FOW_FLAG		
	                            , @CFROM_CASH_FLOW_CODE		
	                            , @CFROM_YEAR				
	                            , @CTO_CASH_FLOW_CODE		
	                            , @CTO_YEAR					
	                            , @CUSER_LOGIN_ID";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuerry;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_CASH_FOW_FLAG", System.Data.DbType.String, 2, poParameter.CFROM_CASH_FOW_FLAG);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_CASH_FLOW_CODE", System.Data.DbType.String, 20, poParameter.CFROM_CASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_YEAR", System.Data.DbType.String, 4, poParameter.CFROM_YEAR);
                loDb.R_AddCommandParameter(loCmd, "@CTO_CASH_FLOW_CODE", System.Data.DbType.String, 20, poParameter.CTO_CASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_YEAR", System.Data.DbType.String, 4, poParameter.CTO_YEAR);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", System.Data.DbType.String, 10, poParameter.CUSER_LOGIN_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00720CopyFromYearDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<GSM00720CopyBaseLocalAmountDTO> UpdateBaseAmount(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720CopyBaseLocalAmountDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuerry = @"RSP_GS_UPDATE_BASE_AMOUNT_CASHFLOW_PLAN 
                                  CCOMPANY_ID
                                , CCASH_FLOW_GROUP
                                , CCASH_FLOW_CODE 
                                , CYEAR
                                , CCURRENCY_CODE
                                , CCURRENCY_RATE
                                , INO_PERIOD_FROM
                                , INO_PERIOD_TO
                                , CUSER_LOGIN_ID
                                ";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuerry;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP", System.Data.DbType.String, 20, poParameter.CCASH_FLOW_GROUP);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", System.Data.DbType.String, 20, poParameter.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", System.Data.DbType.String, 4, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", System.Data.DbType.String, 20, poParameter.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_RATE", System.Data.DbType.String, 20, poParameter.CCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@INO_PERIOD_FROM", System.Data.DbType.String, 10, poParameter.INO_PERIOD_FROM);
                loDb.R_AddCommandParameter(loCmd, "@INO_PERIOD_TO", System.Data.DbType.String, 10, poParameter.INO_PERIOD_TO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", System.Data.DbType.String, 10, poParameter.CUSER_LOGIN_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00720CopyBaseLocalAmountDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }


        public List<GSM00720CopyBaseLocalAmountDTO> UpdateLocalAmount(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720CopyBaseLocalAmountDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuerry = @"RSP_GS_UPDATE_LOCAL_AMOUNT_CASHFLOW_PLAN 
                                  CCOMPANY_ID
                                , CCASH_FLOW_GROUP
                                , CCASH_FLOW_CODE 
                                , CYEAR
                                , CCURRENCY_CODE
                                , CCURRENCY_RATE
                                , INO_PERIOD_FROM
                                , INO_PERIOD_TO
                                , CUSER_LOGIN_ID
                                ";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuerry;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP", System.Data.DbType.String, 20, poParameter.CCASH_FLOW_GROUP);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_CODE", System.Data.DbType.String, 20, poParameter.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", System.Data.DbType.String, 4, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", System.Data.DbType.String, 20, poParameter.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_RATE", System.Data.DbType.String, 20, poParameter.CCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@INO_PERIOD_FROM", System.Data.DbType.String, 10, poParameter.INO_PERIOD_FROM);
                loDb.R_AddCommandParameter(loCmd, "@INO_PERIOD_TO", System.Data.DbType.String, 10, poParameter.INO_PERIOD_TO);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", System.Data.DbType.String, 10, poParameter.CUSER_LOGIN_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00720CopyBaseLocalAmountDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;


        }


        public List<GSM00720CurrencyDTO> GetCurrency(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00720CurrencyDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuerry = @"SELECT CLOCAL_CURRENCY, CBASE_CURRENCY FROM SAM_COMPANIES (NOLOCK)
                                    WHERE CCOMPANY_ID = @CCOMPANY_ID";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuerry;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);

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
