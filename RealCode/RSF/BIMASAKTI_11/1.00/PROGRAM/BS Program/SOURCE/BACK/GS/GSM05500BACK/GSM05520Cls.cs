using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM05500Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05500Back
{
    public class GSM05520Cls : R_BusinessObject<GSM05520DTO>
    {

        public List<GSM05520DTOGetRateType> GetRateType(GSM05500DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM05520DTOGetRateType> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();

                var lcQuery = @"SELECT CRATETYPE_CODE, CRATETYPE_DESCRIPTION FROM GSM_RATETYPE (NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM05520DTOGetRateType>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }




        public GSM05520DTOLocalBaseCurrency GetLocalCurrency(GSM05500DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            GSM05520DTOLocalBaseCurrency loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();

                var lcQuery = @"SELECT A.CLOCAL_CURRENCY, B.CCURRENCY_NAME AS CLOCAL_CURRENCY_NAME,  A.CBASE_CURRENCY, 
                                C.CCURRENCY_NAME AS CBASE_CURRENCY_NAME FROM SAM_COMPANIES A (NOLOCK)
                                LEFT JOIN GSM_CURRENCY B (NOLOCK) ON  A.CLOCAL_CURRENCY = B.CCURRENCY_CODE
                                LEFT JOIN GSM_CURRENCY C (NOLOCK) ON A.CBASE_CURRENCY = C.CCURRENCY_CODE
                                WHERE A.CCOMPANY_ID = @CCOMPANY_ID
";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM05520DTOLocalBaseCurrency>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }



        public List<GSM05520DTO> GetAllCurrencyRate(GSM05500DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM05520DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CURRENCY_RATE_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 10, poParameter.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATE_DATE", DbType.String, 10, poParameter.CRATE_DATE);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM05520DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
        protected override GSM05520DTO R_Display(GSM05520DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM05520DTO loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CURRENCY_RATE_DETAIL";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 10, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_CODE", DbType.String, 10, poEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATE_DATE", DbType.String, 10, poEntity.CRATE_DATE);





                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM05520DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        protected override void R_Saving(GSM05520DTO poNewEntity, eCRUDMode poCRUDMode)
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

                lcQuery = "RSP_GS_MAINTAIN_CURRENCY_RATE";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 8, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_CODE", DbType.String, 10, poNewEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATE_DATE", DbType.String, 10, poNewEntity.CRATE_DATE);
                loDb.R_AddCommandParameter(loCommand, "@NLBASE_RATE_AMOUNT", DbType.Decimal, 10, poNewEntity.NLBASE_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NLCURRENCY_RATE_AMOUNT", DbType.Decimal, 10, poNewEntity.NLCURRENCY_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NBBASE_RATE_AMOUNT", DbType.Decimal, 10, poNewEntity.NBBASE_RATE_AMOUNT);
                //loDb.R_AddCommandParameter(loCommand, "@DCREATE_DATE", DbType.DateTime, 10, DateTime.Now);
                loDb.R_AddCommandParameter(loCommand, "@NBCURRENCY_RATE_AMOUNT", DbType.Decimal, 10, poNewEntity.NBCURRENCY_RATE_AMOUNT);
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


        protected override void R_Deleting(GSM05520DTO poEntity)
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

                lcQuery = "RSP_GS_MAINTAIN_CURRENCY_RATE";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 10, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_CODE", DbType.String, 8, poEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATE_DATE", DbType.String, 8, poEntity.CRATE_DATE);
                loDb.R_AddCommandParameter(loCommand, "@NLBASE_RATE_AMOUNT", DbType.Decimal, 20, poEntity.NLBASE_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NLCURRENCY_RATE_AMOUNT", DbType.Decimal, 20, poEntity.NLCURRENCY_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NBBASE_RATE_AMOUNT", DbType.Decimal, 20, poEntity.NBBASE_RATE_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@NBCURRENCY_RATE_AMOUNT", DbType.Decimal, 20, poEntity.NBCURRENCY_RATE_AMOUNT);
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
    }
}
