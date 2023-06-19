using R_BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common.DTO;
using R_CommonFrontBackAPI;
using R_Common;
using System.Data.Common;
using System.Data;

namespace GSM00700Back
{
    public class GSM00710Cls : R_BusinessObject<GSM00710DTO>
    {

        public List<GSM00710DTO> GetCashFlowList(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00710DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_CASHFLOW_LIST";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_GROUP_CODE", DbType.String, 20,
                    poParameter.CCASH_FLOW_GROUP_CODE);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM00710DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }ss
        pub
        public List<GSM00710CashFlowTypeDTO> CashFlowType(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00710CashFlowTypeDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuerry = @"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO  ('BIMASAKTI', @CCOMPANY_ID, '_CASH_FLOW_TYPE', '', 'Login User Language')";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuerry;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10,
                    poParameter.CCOMPANY_ID);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00710CashFlowTypeDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }


        protected override GSM00710DTO R_Display(GSM00710DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM00710DTO loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_CASHFLOW_DT";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "CCASH_FLOW_GROUP_CODE", DbType.String, 20, poEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCommand, "CCASH_FLOW_CODE", DbType.String, 20, poEntity.CCASH_FLOW_CODE);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM00710DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        protected override void R_Saving(GSM00710DTO poNewEntity, eCRUDMode poCRUDMode)
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

                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    lcAction = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    lcAction = "EDIT";
                }

                lcQuery = "RSP_GS_MAINTAIN_CASHFLOW";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_GROUP_CODE", DbType.String, 20, poNewEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_CODE", DbType.String, 20, poNewEntity.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_NAME", DbType.String, 100, poNewEntity.CCASH_FLOW_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_TYPE", DbType.String, 60, poNewEntity.CCASH_FLOW_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSEQUENCE", DbType.String, 60, poNewEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.SqlExecNonQuery(loConn, loCommand, true);
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

        protected override void R_Deleting(GSM00710DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_GS_MAINTAIN_CASHFLOW";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_GROUP_CODE", DbType.String, 20, poEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_CODE", DbType.String, 20, poEntity.CCASH_FLOW_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_NAME", DbType.String, 100, poEntity.CCASH_FLOW_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_TYPE", DbType.String, 60, poEntity.CCASH_FLOW_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSEQUENCE", DbType.String, 10, poEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
                loDb.SqlExecNonQuery(loConn, loCommand, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
