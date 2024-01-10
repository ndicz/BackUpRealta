using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using GSM02300Common;
using GSM02300Common.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM02300Back
{
    public class GSM02300Cls : R_BusinessObject<GSM02300DTO>    
    {
        RSP_GS_MAINTAIN_PROPERTY_TYPEResources.Resources_Dummy_Class ResourceDummyClasCashFlowGroup = new();
       
        private LogGSM02300Common _logger;
        public GSM02300Cls()
        {
            _logger = LogGSM02300Common.R_GetInstanceLogger();
        }
        public List<GSM02300DTO> GetAllProperty(GSM02300DBParaneter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM02300DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;
         
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PROPERTY_TYPE_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || VaBankChannel(Cls) ", lcQuery, poParameter);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<GSM02300DTO>(loReturnTemp).ToList();
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

        public List<GSM02300PropertyTypeDTO> GetPropertyType(GSM02300DBParaneter poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GSM02300PropertyTypeDTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery =
                    @"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', @CCOMPANY_ID, '_PROPERTY_TYPE', '', @CCOMPANY_ID)";
                loCmd.CommandType = System.Data.CommandType.Text;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", System.Data.DbType.String, 10, poParameter.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x =>
                                           x.ParameterName == "@CCOMPANY_ID").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || VaBankChannel(Cls) ", lcQuery, poParameter);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<GSM02300PropertyTypeDTO>(loReturnTemp).ToList();
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


        protected override GSM02300DTO R_Display(GSM02300DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02300DTO loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();

                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_PROPERTY_TYPE_DETAIL";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCommand, "CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "CPROPERTY_TYPE_CODE", DbType.String, 20, poEntity.CPROPERTY_TYPE_CODE);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "CCOMPANY_ID" ||
                        x.ParameterName == "CUSER_ID" ||
                        x.ParameterName == "CPROPERTY_TYPE_CODE").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || VaBankChannel(Cls) ", lcQuery, poEntity);

                 
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                loReturn = R_Utility.R_ConvertTo<GSM02300DTO>(loDataTable).FirstOrDefault();
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

        protected override void R_Deleting(GSM02300DTO poEntity)
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
                R_ExternalException.R_SP_Init_Exception(loConn);


                lcQuery = "RSP_GS_MAINTAIN_PROPERTY_TYPE";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_TYPE_CODE", DbType.String, 10, poEntity.CPROPERTY_TYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_TYPE_NAME", DbType.String, 60, poEntity.CPROPERTY_TYPE_NAME);
                loDb.R_AddCommandParameter(loCommand, "@LSINGLE_UNIT", DbType.String, 60, poEntity.LSINGLE_UNIT);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@LUSE_PRICE_LIST", DbType.String, 10, poEntity.LUSE_PRICE_LIST);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CPROPERTY_TYPE_CODE" ||
                        x.ParameterName == "@CPROPERTY_TYPE_NAME" ||
                        x.ParameterName == "@LSINGLE_UNIT" ||
                        x.ParameterName == "@LUSE_PRICE_LIST" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CACTION").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || VaBankChannel(Cls) ", lcQuery, poEntity);

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

        protected override void R_Saving(GSM02300DTO poNewEntity, eCRUDMode poCRUDMode)
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

                lcQuery = "RSP_GS_MAINTAIN_PROPERTY_TYPE";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_TYPE_CODE", DbType.String, 10, poNewEntity.CPROPERTY_TYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_TYPE_NAME", DbType.String, 60, poNewEntity.CPROPERTY_TYPE_NAME);
                loDb.R_AddCommandParameter(loCommand, "@LSINGLE_UNIT", DbType.String, 60, poNewEntity.LSINGLE_UNIT);
                loDb.R_AddCommandParameter(loCommand, "@LUSE_PRICE_LIST", DbType.String, 10, poNewEntity.LUSE_PRICE_LIST);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>().Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CPROPERTY_TYPE_CODE" ||
                        x.ParameterName == "@CPROPERTY_TYPE_NAME" ||
                        x.ParameterName == "@LSINGLE_UNIT" ||
                        x.ParameterName == "@LUSE_PRICE_LIST" ||
                        x.ParameterName == "@CUSER_ID" ||
                        x.ParameterName == "@CACTION").
                    Select(x => x.Value);
                _logger.LogDebug("EXEC {Query} {@Parameters} || VaBankChannel(Cls) ", lcQuery, poNewEntity);
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
