using System.Data;
using System.Data.Common;
using LMM01500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_Storage;
using R_StorageCommon;

namespace LMM01500Back;

public class LMM01500Cls : R_BusinessObject<LMM01500GeneralInfoDTO>
{
    protected override LMM01500GeneralInfoDTO R_Display(LMM01500GeneralInfoDTO poEntity)
    {
        R_Exception loException = new R_Exception();
        LMM01500GeneralInfoDTO loReturn = null;
        R_Db loDb;
        DbCommand loCommand;

        try
        {
            var lcQuery = "RSP_LM_GET_INVOICE_GROUP";
            loDb = new R_Db();
            loCommand = loDb.GetCommand();
            var loConn = loDb.GetConnection();
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 10, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CINVGRP_CODE", DbType.String, 20, poEntity.CINVGRP_CODE);


            var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
            loReturn = R_Utility.R_ConvertTo<LMM01500GeneralInfoDTO>(loReturnTemp).ToList().FirstOrDefault();
            if (String.IsNullOrEmpty(loReturn.CSTORAGE_ID) == false)
            {
                var loReadParameter = new R_ReadParameter()
                {
                    StorageId = loReturn.CSTORAGE_ID
                };

               var loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                loReturn.OData = loReadResult.Data;
                loReturn.CFileName = loReadResult.FileName;
                loReturn.CFileExtension = loReadResult.FileExtension;
                loReturn.CFileNameExtension = loReadResult.FileName + loReadResult.FileExtension;
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }

    protected override void R_Deleting(LMM01500GeneralInfoDTO poEntity)
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

            lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;


            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CINVGRP_CODE", DbType.String, 8, poEntity.CINVGRP_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CINVGRP_NAME", DbType.String, 100, poEntity.CINVGRP_NAME);
            loDb.R_AddCommandParameter(loCommand, "@CSEQUENCE", DbType.String, 6, poEntity.CSEQUENCE);
            loDb.R_AddCommandParameter(loCommand, "@LACTIVE", DbType.Boolean, 2, poEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCommand, "@CINVOICE_DUE_MODE", DbType.String, 2, poEntity.CINVOICE_DUE_MODE);
            loDb.R_AddCommandParameter(loCommand, "@CINVOICE_GROUP_MODE", DbType.String, 2,
                poEntity.CINVOICE_GROUP_MODE);
            loDb.R_AddCommandParameter(loCommand, "@IDUE_DAYS ", DbType.Int32, 512, poEntity.IDUE_DAYS);
            loDb.R_AddCommandParameter(loCommand, "@IFIXED_DUE_DATE", DbType.Int32, 512, poEntity.IFIXED_DUE_DATE);
            loDb.R_AddCommandParameter(loCommand, "@ILIMIT_INVOICE_DATE ", DbType.Int32, 512,
                poEntity.ILIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCommand, "@IBEFORE_LIMIT_INVOICE_DATE ", DbType.Int32, 512,
                poEntity.IBEFORE_LIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCommand, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, 512,
                poEntity.IAFTER_LIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCommand, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 2,
                poEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
            loDb.R_AddCommandParameter(loCommand, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 2,
                poEntity.LDUE_DATE_TOLERANCE_SATURDAY);
            loDb.R_AddCommandParameter(loCommand, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 2,
                poEntity.LDUE_DATE_TOLERANCE_SUNDAY);
            loDb.R_AddCommandParameter(loCommand, "LUSE_STAMP", DbType.Boolean, 2, poEntity.LUSE_STAMP);
            loDb.R_AddCommandParameter(loCommand, "@CSTAMP_ADD_ID", DbType.String, 20, poEntity.CSTAMP_ADD_ID);
            loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION ", DbType.Int32, Int32.MaxValue,
                poEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCommand, "@LBY_DEPARTMENT", DbType.Boolean, 2, poEntity.LBY_DEPARTMENT);
            loDb.R_AddCommandParameter(loCommand, "@CINVOICE_TEMPLATE", DbType.String, 100, poEntity.CINVOICE_TEMPLATE);
            loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 8, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CBANK_CODE", DbType.String, 8, poEntity.CBANK_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CBANK_ACCOUNT", DbType.String, 20, poEntity.CBANK_ACCOUNT);
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, "DELETE");
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50, poEntity.CUSER_ID);
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
   public List<LMM01500TemplateBankAccountDTO> GetInvoiceDeptList(LMM01500DBParameter poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM01500TemplateBankAccountDTO> loReturn = null;
        R_Db loDb;
        DbCommand loCommand;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            var lcQuery = @"RSP_LM_GET_INVGRP_DEPT_LIST";
            loCommand.CommandType = System.Data.CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CINVGRP_CODE", DbType.String, 50, poParameter.CINVGRP_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<LMM01500TemplateBankAccountDTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }
    public List<LMM01500InitialProcessDTO> GetInitProperty(LMM01500DBParameter poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM01500InitialProcessDTO> loReturn = null;
        R_Db loDb;
        DbCommand loCommand;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<LMM01500InitialProcessDTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public List<LMM01500GeneralInfoDTO> GetInvoiceGroupList(LMM01500DBParameter poParameter)
    {
        R_Exception loException = new R_Exception();
        List<LMM01500GeneralInfoDTO> loReturn = null;
        R_Db loDb;
        DbCommand loCommand;

        try
        {
            loDb = new R_Db();
            var loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            var lcQuery = @"RSP_LM_GET_INVOICE_GROUP_LIST";
            loCommand.CommandType = System.Data.CommandType.StoredProcedure;
            loCommand.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", System.Data.DbType.String, 50,
                poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", System.Data.DbType.String, 50,
                poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", System.Data.DbType.String, 50, poParameter.CUSER_ID);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<LMM01500GeneralInfoDTO>(loReturnTemp).ToList();
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }

  public bool ValidateCheckDataTab2(LMM01500GeneralInfoDTO poNewEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQueryLog = "";
            LMM01500GeneralInfoDTO loResult = null;
            bool llRtn = false;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT TOP 1 1 FROM LMM_INVGRP_BANK_ACC_DEPT (NOLOCK) " +
                   "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                   "AND CPROPERTY_ID = @CPROPERTY_ID " +
                   "AND CINVGRP_CODE = @CINVGRP_CODE ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01500GeneralInfoDTO>(loDataTable).FirstOrDefault();

                //Validasi Add
                if (loResult == null)
                {
                    llRtn = false;
                }
                else
                {
                    llRtn = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                
            }

            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }
    protected override void R_Saving(LMM01500GeneralInfoDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        R_Exception loException = new R_Exception();
        R_Db loDb;
        DbCommand loCommand = null;
        DbConnection loConn = null;
        loDb = new R_Db();
        string lcStorageId = "";
        try
        {
            if (poNewEntity.DeleteAllTabDept)
            {
                var loParam = R_Utility.R_ConvertObjectToObject<LMM01500GeneralInfoDTO, LMM01500TemplateBankAccountDTO>(poNewEntity);
                DeleteAllDataByInvoiceGroupCode(loParam);
            }

            var loGetStorageType = GetStorageType();

            var loValidate = ValidateAlreadyExists(poNewEntity);
           var loResult = SetStorageID(poNewEntity, loGetStorageType, loValidate);

            SaveDataSP(loResult, poCRUDMode);
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
                    loConn.Close();

                loConn.Dispose();
                loConn = null;
            }
            if (loDb != null)
            {
                loDb = null;
            }
        }

        loException.ThrowExceptionIfErrors();
    }
    
   
        #region Saving
        #region Delete All Data Tab Template 
        private List<LMM01500TemplateBankAccountDTO> GetAllTemplateAndBankAccount(LMM01500TemplateBankAccountDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01500TemplateBankAccountDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_INVGRP_DEPT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01500TemplateBankAccountDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        private void DeleteStorageData(LMM01500TemplateBankAccountDTO poEntity)
        {
            var loEx = new R_Exception();
            var loDb = new R_Db();
            DbConnection loConn = null;
            R_DeleteParameter loDeleteParameter;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                if (String.IsNullOrEmpty(poEntity.CSTORAGE_ID) == false)
                {
                    loDeleteParameter = new R_DeleteParameter()
                    {
                        StorageId = poEntity.CSTORAGE_ID,
                        UserId = R_BackGlobalVar.USER_ID
                    };
                    R_StorageUtility.DeleteFile(loDeleteParameter, loConn);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void DeleteDataSP(LMM01500TemplateBankAccountDTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                // set action delete

                lcQuery = "RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 50, poEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 100, poEntity.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, "DELETE");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void DeleteAllDataByInvoiceGroupCode(LMM01500TemplateBankAccountDTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01500TemplateBankAccountDTO loParam = null;

            try
            {
                var loListData = GetAllTemplateAndBankAccount(poEntity);

                if (loListData.Count > 0)
                {

                    foreach (var item in loListData)
                    {
                        DeleteStorageData(item);
                    }

                    DeleteDataSP(poEntity);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Set Storage ID
        private LMM01500StorageType GetStorageType()
        {
            var loEx = new R_Exception();
            LMM01500StorageType loResult = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_STORAGE_TYPE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01500StorageType>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            
            }
            
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        private bool ValidateAlreadyExists(LMM01500GeneralInfoDTO poNewEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQueryLog = "";
            LMM01500GeneralInfoDTO loResult = null;
            bool llRtn = false;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT TOP 1 1 FROM LMM_INVGRP (NOLOCK) " +
                    "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    "AND CPROPERTY_ID = @CPROPERTY_ID " +
                    "AND CINVGRP_CODE = @CINVGRP_CODE ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);

                //Debug Logs
               

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01500GeneralInfoDTO>(loDataTable).FirstOrDefault();

                //Validasi Add
                if (loResult == null)
                {
                    llRtn = true;
                }
                else
                {
                    llRtn = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }
        private LMM01500GeneralInfoDTO SetStorageID(LMM01500GeneralInfoDTO poNewEntity, LMM01500StorageType poStorageType, bool plExists)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            string lcQueryLog = "";
            R_SaveResult loSaveResult;
            R_ConnectionAttribute loConnAttr;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loConnAttr = loDb.GetConnectionAttribute();
                if (poNewEntity.LBY_DEPARTMENT ==  false)
                {
                    if (plExists)
                    {
                        //Set Storage Type
                        R_EStorageType loStorageType;
                        loStorageType = poStorageType.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;

                        R_EProviderForCloudStorage loProvider;
                        loProvider = poStorageType.CSTORAGE_PROVIDER_ID.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;

                        R_AddParameter loAddParameter;
                        loAddParameter = new R_AddParameter()
                        {
                            StorageType = loStorageType,
                            ProviderCloudStorage = loProvider,
                            FileName = poNewEntity.CFileName,
                            FileExtension = poNewEntity.CFileExtension,
                            UploadData = poNewEntity.OData,
                            UserId = poNewEntity.CUSER_ID,
                            BusinessKeyParameter = new R_BusinessKeyParameter()
                            {
                                CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                                CDATA_TYPE = "STORAGE_DATA_TABLE",
                                CKEY01 = poNewEntity.CPROPERTY_ID,
                                CKEY02 = poNewEntity.CINVGRP_CODE,
                            }
                        };
                        loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                        //Set Storage ID CSTORAGE_ID
                        poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(poNewEntity.CSTORAGE_ID) == false)
                        {
                            R_UpdateParameter loUpdateParameter;

                            loUpdateParameter = new R_UpdateParameter()
                            {
                                StorageId = poNewEntity.CSTORAGE_ID,
                                UploadData = poNewEntity.OData,
                                UserId = poNewEntity.CUSER_ID,
                                OptionalSaveAs = new R_UpdateParameter.OptionalSaveAsParameter()
                                {
                                    FileExtension = poNewEntity.CFileExtension,
                                    FileName = poNewEntity.CFileName
                                }
                            };
                            loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, loConn, loConnAttr.Provider);

                            //Set Storage ID CSTORAGE_ID
                            poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;
                        }
                        else
                        {
                            //Set Storage Type
                            R_EStorageType loStorageType;
                            loStorageType = poStorageType.CSTORAGE_TYPE != "1" ? R_EStorageType.OnPremise : R_EStorageType.Cloud;

                            R_EProviderForCloudStorage loProvider;
                            loProvider = poStorageType.CSTORAGE_PROVIDER_ID.ToLower() != "azure" ? R_EProviderForCloudStorage.google : R_EProviderForCloudStorage.azure;

                            R_AddParameter loAddParameter;
                            loAddParameter = new R_AddParameter()
                            {
                                StorageType = loStorageType,
                                ProviderCloudStorage = loProvider,
                                FileName = poNewEntity.CFileName,
                                FileExtension = poNewEntity.CFileExtension,
                                UploadData = poNewEntity.OData,
                                UserId = poNewEntity.CUSER_ID,
                                BusinessKeyParameter = new R_BusinessKeyParameter()
                                {
                                    CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                                    CDATA_TYPE = "STORAGE_DATA_TABLE",
                                    CKEY01 = poNewEntity.CPROPERTY_ID,
                                    CKEY02 = poNewEntity.CINVGRP_CODE,
                                }
                            };
                            loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                            //Set Storage ID CSTORAGE_ID
                            poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;
                        }
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(poNewEntity.CSTORAGE_ID) == false)
                    {
                        var loDeleteParameter = new R_DeleteParameter()
                        {
                            StorageId = poNewEntity.CSTORAGE_ID,
                            UserId = poNewEntity.CUSER_ID
                        };
                        R_StorageUtility.DeleteFile(loDeleteParameter, "R_DefaultConnectionString");

                        //Set Storage ID CSTORAGE_ID
                        poNewEntity.CSTORAGE_ID = "";
                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return poNewEntity;
        }
        #endregion

        #region Save Data
        private void SaveDataSP(LMM01500GeneralInfoDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            LMM01500GeneralInfoDTO loResult = null;
            string lcAction = null;
            try
            {
                //Set Action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    lcAction = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    lcAction = "EDIT";
                }

                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 50, poNewEntity.CINVGRP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 50, poNewEntity.CSEQUENCE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 50, poNewEntity.CINVOICE_DUE_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 50, poNewEntity.CINVOICE_GROUP_MODE);
                loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, 50, poNewEntity.IDUE_DAYS);
                loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, 50, poNewEntity.IFIXED_DUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.ILIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.IBEFORE_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.IAFTER_LIMIT_INVOICE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SUNDAY);
                loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 50, poNewEntity.LUSE_STAMP);
                loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 50, poNewEntity.CSTAMP_ADD_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 50, poNewEntity.LBY_DEPARTMENT);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poNewEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poNewEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 50, poNewEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 100, poNewEntity.CSTORAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #endregion
        
    public void ActiveIanctive(LMM01500GeneralInfoDTO poEntity)
    {
        R_Exception loException = new R_Exception();

        try
        {
            R_Db loDb = new R_Db();
            DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");
            DbCommand loCmd = loDb.GetCommand();

            string lcQuery = "RSP_LM_ACTIVE_INACTIVE_INVGRP";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
            

            loDb.SqlExecQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
    }
}