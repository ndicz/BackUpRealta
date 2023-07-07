using R_BackEnd;
using R_CommonFrontBackAPI;
using GSM00700Common.DTO;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Reflection.Metadata;

namespace GSM00700Back
{
    public class GSM00700Cls : R_BusinessObject<GSM00700DTO>    
    {
        public List<GSM00700DTO> GetCashFlowGroupList(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00700DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CASHFLOW_GRP_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM00700DTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<GSM00700CashFlowGroupTypeDTO> CashFlowGroupType(GSM00700DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM00700CashFlowGroupTypeDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuerry =
                    @"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO  ('BIMASAKTI', @CCOMPANY_ID, '_CASH_FLOW_GROUP_TYPE', '', 'Login User Language')";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuerry;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10,
                    poParameter.CCOMPANY_ID);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM00700CashFlowGroupTypeDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

      
        protected override GSM00700DTO R_Display(GSM00700DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM00700DTO loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_CASHFLOW_GRP_DT";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "CCASH_FLOW_GROUP_CODE", DbType.String, 20, poEntity.CCASH_FLOW_GROUP_CODE
                );
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM00700DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        protected override void R_Deleting(GSM00700DTO poEntity)
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


                    lcQuery = "RSP_GS_MAINTAIN_CASHFLOW_GROUP";
                    loCommand.CommandType = CommandType.StoredProcedure;
                    loCommand.CommandText = lcQuery;

                    loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_GROUP_CODE", DbType.String, 10, poEntity.CCASH_FLOW_GROUP_CODE);
                    loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_GROUP_NAME", DbType.String, 60, poEntity.CCASH_FLOW_GROUP_NAME);
                    loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_GROUP_TYPE", DbType.String, 60, poEntity.CCASH_FLOW_GROUP_TYPE);
                    loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                    loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");

                    loDb.SqlExecNonQuery(loConn, loCommand, true);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.ThrowExceptionIfErrors();
            }


        protected override void R_Saving(GSM00700DTO poNewEntity, eCRUDMode poCRUDMode)
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

                lcQuery = "RSP_GS_MAINTAIN_CASHFLOW_GROUP";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;


                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_GROUP_CODE", DbType.String, 10, poNewEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_GROUP_NAME", DbType.String, 60, poNewEntity.CCASH_FLOW_GROUP_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CCASH_FLOW_GROUP_TYPE", DbType.String, 60, poNewEntity.CCASH_FLOW_GROUP_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
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
    }
}
