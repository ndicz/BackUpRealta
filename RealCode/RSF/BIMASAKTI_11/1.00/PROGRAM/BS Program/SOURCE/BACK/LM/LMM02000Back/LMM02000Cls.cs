using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LMM02000Common;
using LMM02000Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02000Back
{
    public class LMM02000Cls : R_BusinessObject<LMM02000DTO>
    {
        private LogLMM02000Common _logger;
        public LMM02000Cls()
        {
            _logger = LogLMM02000Common.R_GetInstanceLogger();
        }
        public void RSP_GS_ACTIVE_INACTIVE_LMM02000(LMM02000DBParameter poEntity)
        {
           R_Exception loex = new R_Exception();
           R_Db loDb;
           DbCommand loCmd;
           DbConnection loConn;
           try
            {
                loDb = new R_Db(); 
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();
                string lcQuery = "RSP_LM_ACTIVE_INACTIVE_SALESMAN";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSALESMAN_ID", DbType.String, 20, poEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 2, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "CCOMPANY_ID" ||
                        x.ParameterName == "CPROPERTY_ID" ||
                        x.ParameterName == "CSALESMAN_ID" ||
                        x.ParameterName == "LACTIVE" ||
                        x.ParameterName == "CUSER_ID").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || Salesman(Cls) ", lcQuery, poEntity);


                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loex.Add(ex);
                _logger.LogError(ex);
            }

            EndBlock:
            loex.ThrowExceptionIfErrors();
        }




        public List<LMM02000GenderTypeDTO> GetGender(LMM02000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM02000GenderTypeDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuerry = @"SELECT * FROM RFT_GET_GSB_CODE_INFO ('SIAPP',@CCOMPANY_ID, '_GENDER', '', 'en')";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuerry;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "CCOMPANY_ID").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || Salesman(Cls) ", lcQuerry, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<LMM02000GenderTypeDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }
            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;

        }

        public List<LMM02000SalesmanTypeDTO> GetSalesmanType(LMM02000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM02000SalesmanTypeDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuerry = @"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', 'CCOMPANY_ID', '_BS_SALESMAN_TYPE', '', 'en')";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuerry;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);
                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "CCOMPANY_ID").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || Salesman(Cls) ", lcQuerry, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<LMM02000SalesmanTypeDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;

        }

        public List<LMM02000DTO> GetListSalesman(LMM02000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM02000DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_LM_GET_SALESMAN_LIST";
                loCmd.CommandType = System.Data.CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", System.Data.DbType.String, 10, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", System.Data.DbType.String, 50, poParameter.CPROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "CCOMPANY_ID" ||
                        x.ParameterName == "CUSER_ID" ||
                        x.ParameterName == "CPROPERTY_ID").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || Salesman(Cls) ", lcQuery, poParameter);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<LMM02000DTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;

        }

        public List<LMM02010DTO> GetListSalesmenDetail(LMM02000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM02010DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_LM_GET_SALESMAN_DETAIL";
                loCommand.CommandType = System.Data.CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 10, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", System.Data.DbType.String, 20, poParameter.CSALESMAN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "CCOMPANY_ID" ||
                        x.ParameterName == "CUSER_ID" ||
                        x.ParameterName == "CPROPERTY_ID" ||
                        x.ParameterName == "CSALESMAN_ID").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || Salesman(Cls) ", lcQuery, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMM02010DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loReturn;

        }

        public List<LMM02000PropertyDTO> GetAllPropertyList(LMM02000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<LMM02000PropertyDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
                loCommand.CommandType = System.Data.CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50, poParameter.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "CCOMPANY_ID" ||
                        x.ParameterName == "CUSER_ID").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || Salesman(Cls) ", lcQuery, poParameter);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LMM02000PropertyDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        protected override LMM02000DTO R_Display(LMM02000DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM02000DTO loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_LM_GET_SALESMAN_DETAIL";
                loCommand.CommandType = System.Data.CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 10, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", System.Data.DbType.String, 20, poEntity.CSALESMAN_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "CCOMPANY_ID" ||
                        x.ParameterName == "CUSER_ID" ||
                        x.ParameterName == "CPROPERTY_ID" ||
                        x.ParameterName == "CSALESMAN_ID").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || Salesman(Cls) ", lcQuery, poEntity);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<LMM02000DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {

                loException.Add(ex);
                _logger.LogError(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }

        protected override void R_Deleting(LMM02000DTO poEntity)
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

                lcQuery = "RSP_LM_MAINTAIN_SALESMAN";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", System.Data.DbType.String, 20, poEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_NAME", System.Data.DbType.String, 100, poEntity.CSALESMAN_NAME);
                loDb.R_AddCommandParameter(loCommand, "@LACTIVE", System.Data.DbType.Boolean, 1, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCommand, "@CADDRESS", System.Data.DbType.String, 255, poEntity.CADDRESS);
                loDb.R_AddCommandParameter(loCommand, "@CEMAIL", System.Data.DbType.String, 100, poEntity.CEMAIL);
                loDb.R_AddCommandParameter(loCommand, "@CMOBILE_PHONE1", System.Data.DbType.String, 30, poEntity.CMOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CMOBILE_PHONE2", System.Data.DbType.String, 30, poEntity.CMOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCommand, "@CID_NO", System.Data.DbType.String, 40, poEntity.CID_NO);
                loDb.R_AddCommandParameter(loCommand, "@CGENDER", System.Data.DbType.String, 50, poEntity.CGENDER);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_TYPE", System.Data.DbType.String, 8, poEntity.CSALESMAN_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CEXT_COMPANY_NAME", System.Data.DbType.String, 100, poEntity.CEXT_COMPANY_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "CCOMPANY_ID" ||
                        x.ParameterName == "CPROPERTY_ID" ||
                        x.ParameterName == "CSALESMAN_ID" ||
                        x.ParameterName == "CSALESMAN_NAME" ||
                        x.ParameterName == "LACTIVE" ||
                        x.ParameterName == "CADDRESS" ||
                        x.ParameterName == "CEMAIL" ||
                        x.ParameterName == "CMOBILE_PHONE1" ||
                        x.ParameterName == "CMOBILE_PHONE2" ||
                        x.ParameterName == "CID_NO" ||
                        x.ParameterName == "CGENDER" ||
                        x.ParameterName == "CSALESMAN_TYPE" ||
                        x.ParameterName == "CEXT_COMPANY_NAME" ||
                        x.ParameterName == "CACTION" ||
                        x.ParameterName == "CUSER_ID").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || Salesman(Cls) ", lcQuery, poEntity);
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
                _logger.LogError(ex);
            }

            loException.ThrowExceptionIfErrors();
        }




        protected override void R_Saving(LMM02000DTO poEntity, eCRUDMode poCRUDMode)
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
                lcQuery = "RSP_LM_MAINTAIN_SALESMAN";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_ID", System.Data.DbType.String, 20, poEntity.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_NAME", System.Data.DbType.String, 100, poEntity.CSALESMAN_NAME);
                loDb.R_AddCommandParameter(loCommand, "@LACTIVE", System.Data.DbType.Boolean, 1, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCommand, "@CADDRESS", System.Data.DbType.String, 255, poEntity.CADDRESS);
                loDb.R_AddCommandParameter(loCommand, "@CEMAIL", System.Data.DbType.String, 100, poEntity.CEMAIL);
                loDb.R_AddCommandParameter(loCommand, "@CMOBILE_PHONE1", System.Data.DbType.String, 30, poEntity.CMOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCommand, "@CMOBILE_PHONE2", System.Data.DbType.String, 30, poEntity.CMOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCommand, "@CID_NO", System.Data.DbType.String, 40, poEntity.CID_NO);
                loDb.R_AddCommandParameter(loCommand, "@CGENDER", System.Data.DbType.String, 50, poEntity.CGENDER);
                loDb.R_AddCommandParameter(loCommand, "@CSALESMAN_TYPE", System.Data.DbType.String, 8, poEntity.CSALESMAN_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CEXT_COMPANY_NAME", System.Data.DbType.String, 100, poEntity.CEXT_COMPANY_NAME);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "CCOMPANY_ID" ||
                        x.ParameterName == "CPROPERTY_ID" ||
                        x.ParameterName == "CSALESMAN_ID" ||
                        x.ParameterName == "CSALESMAN_NAME" ||
                        x.ParameterName == "LACTIVE" ||
                        x.ParameterName == "CADDRESS" ||
                        x.ParameterName == "CEMAIL" ||
                        x.ParameterName == "CMOBILE_PHONE1" ||
                        x.ParameterName == "CMOBILE_PHONE2" ||
                        x.ParameterName == "CID_NO" ||
                        x.ParameterName == "CGENDER" ||
                        x.ParameterName == "CSALESMAN_TYPE" ||
                        x.ParameterName == "CEXT_COMPANY_NAME" ||
                        x.ParameterName == "CACTION" ||
                        x.ParameterName == "CUSER_ID").
                    Select(x => x.Value);
                _logger.R_LogDebug("EXEC {Query} {@Parameters} || Salesman(Cls) ", lcQuery, poEntity);
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
                _logger.LogError(ex);
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
