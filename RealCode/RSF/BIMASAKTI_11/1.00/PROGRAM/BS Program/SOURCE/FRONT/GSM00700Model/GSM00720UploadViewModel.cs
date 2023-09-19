
using GSM00700Common;
using GSM00700Model.Model;
using R_APICommonDTO;
using R_BlazorFrontEnd.Excel;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GSM00700Common.DTO.Upload_DTO_GSM00720;
using GSM00700Common.DTO;
using System.ComponentModel.Design;
using System.Data;

namespace GSM00700Model
{
    public class GSM00720UploadViewModel : R_IProcessProgressStatus
    {
        //private GSM00720UploadModel loModel = new GSM00720UploadModel();
        //private GSM00720Model loCashFlowPlanPlanModel = new GSM00720Model();

        //public GSM00720UploadCashFlowPlanPlanErrorResultDTO loErrorRtn = new GSM00720UploadCashFlowPlanPlanErrorResultDTO();
        //public GSM00720UploadCashFlowPlanPlanResultDTO loRtn = new GSM00720UploadCashFlowPlanPlanResultDTO();
        //public GSM00720UploadCashFlowPlanPlanCheckUsedDTO loCheckIsCashFlowPlanPlanUsed = new GSM00720UploadCashFlowPlanPlanCheckUsedDTO();
        //public GSM00720UploadCashFlowPlanPlanCheckResultDTO loCheckIsCashFlowPlanPlanUsedRtn = new GSM00720UploadCashFlowPlanPlanCheckResultDTO();
        //public GSM00720UploadCashFlowPlanPlanParameterDTO loParameter = new GSM00720UploadCashFlowPlanPlanParameterDTO();

        //public ObservableCollection<GSM00720UploadCashFlowPlanPlanDTO> loUploadCashFlowPlanPlanDisplayList = new ObservableCollection<GSM00720UploadCashFlowPlanPlanDTO>();

        //public List<GSM00720UploadCashFlowPlanPlanDTO> loUploadCashFlowPlanPlanList = new List<GSM00720UploadCashFlowPlanPlanDTO>();
        //public List<GSM00720UploadCashFlowPlanPlanErrorDTO> loErrorList = new List<GSM00720UploadCashFlowPlanPlanErrorDTO>();

        //public string SelectedCompanyId = "";
        //public string SelectedUserId = "";

        //public bool IsOverWrite = false;

        //public string CashFlowPlanPlanGroupCode = "";
        //public string CashFlowPlanPlanGroupName = "";
        //public string CashFlowPlanPlanCode = "";
        //public string CashFlowPlanPlanName = "";
        //public string CashFlowPlanPlanYear = "";

        //public bool IsUploadSuccesful = true;
        //public string Message = "";
        //public int Percentage = 0;
        //public bool OverwriteData = false;
        //public byte[] fileByte = null;

        //public bool VisibleError = false;
        //public bool IsErrorEmptyFile = false;

        //public async Task ValidateUpload()
        //{
        //    var loEx = new R_Exception();
        //    R_BatchParameter loBatchValidatePar;
        //    R_ProcessAndUploadClient loCls;
        //    List<GSM00720UploadCashFlowPlanPlanErrorDTO> Bigobject;
        //    List<R_KeyValue> loUserParam;

        //    try
        //    {
        //        loUserParam = new List<R_KeyValue>();
        //        loUserParam.Add(new R_KeyValue() { Key = ContextConstantGSM00700.IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

        //        //Instantiate ProcessClient
        //        loCls = new R_ProcessAndUploadClient(
        //            pcModuleName: "GS",
        //            plSendWithContext: true,
        //            plSendWithToken: true,
        //            pcHttpClientName: "R_DefaultServiceUrl",
        //            poProcessProgressStatus: this);

        //        //preapare Batch Parameter
        //        loBatchValidatePar = new R_BatchParameter();
        //        loBatchValidatePar.COMPANY_ID = SelectedCompanyId;
        //        loBatchValidatePar.USER_ID = SelectedUserId;
        //        loBatchValidatePar.UserParameters = loUserParam;
        //        loBatchValidatePar.ClassName = "GSM00700Back.GSM00720UploadCashFlowPlanPlanValidateCls";
        //        loBatchValidatePar.BigObject = loUploadCashFlowPlanPlanList;

        //        await loCls.R_BatchProcess<List<GSM00720UploadCashFlowPlanPlanDTO>>(loBatchValidatePar, 6);

        //        VisibleError = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //}


        //public void ReadExcelFile()
        //{
        //    var loEx = new R_Exception();
        //    List<GSM00720UploadCashFlowPlanPlanExcelDTO> loExtract = new List<GSM00720UploadCashFlowPlanPlanExcelDTO>();
        //    try
        //    {
        //        //add filebyte to DTO
        //        var loExcel = new R_Excel();

        //        var loDataSet = loExcel.R_ReadFromExcel(fileByte, new string[] { "CashFlowPlanPlan" });

        //        var loResult = R_FrontUtility.R_ConvertTo<GSM00720UploadCashFlowPlanPlanExcelDTO>(loDataSet.Tables[0]);

        //        loExtract = new List<GSM00720UploadCashFlowPlanPlanExcelDTO>(loResult);

        //        loUploadCashFlowPlanPlanList = loExtract.Select(x => new GSM00720UploadCashFlowPlanPlanDTO
        //        {
        //            CPERIOD_NO = x.PeriodNo,
        //            NLOCAL_AMOUNT = x.LocalAmount,
        //            NBASE_AMOUNT = x.BaseAmount,
        //            CCashFlowPlan_GROUP_CODE = CashFlowPlanPlanGroupCode,
        //            CCASH_FLOW_CODE = CashFlowPlanPlanCode,
        //            CCYEAR = CashFlowPlanPlanYear,
        //            CNOTES = "",
        //            LEXIST = false,

        //        }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //        var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
        //        IsErrorEmptyFile = MatchingError != null;
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //}

        //public async Task AttachFile()
        //{
        //    var loEx = new R_Exception();
        //    R_UploadPar loUploadPar;
        //    R_ProcessAndUploadClient loCls;
        //    List<GSM00720UploadCashFlowPlanPlanDTO> Bigobject;
        //    List<R_KeyValue> loUserParam;

        //    R_Exception loException = new R_Exception();
        //    try
        //    {
        //        loUserParam = new List<R_KeyValue>();
        //        //Instantiate ProcessClient
        //        loCls = new R_ProcessAndUploadClient(
        //            pcModuleName: "GS",
        //            plSendWithContext: true,
        //            plSendWithToken: true,
        //            pcHttpClientName: "R_DefaultServiceUrl",
        //            poProcessProgressStatus: this);

        //        //preapare Batch Parameter
        //        loUploadPar = new R_UploadPar();
        //        loUploadPar.COMPANY_ID = SelectedCompanyId;
        //        loUploadPar.USER_ID = SelectedUserId;
        //        loUploadPar.UserParameters = loUserParam;
        //        loUploadPar.ClassName = "GSM00700Back.GSM00720UploadCashFlowPlanPlanPlanCls";
        //        loUploadPar.BigObject = loUploadCashFlowPlanPlanList;

        //        await loCls.R_AttachFile<List<GSM00720UploadCashFlowPlanPlanDTO>>(loUploadPar);

        //        VisibleError = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);

        //        if (loEx.ErrorList.Count > 0)
        //        {
        //            var DeserializeList = JsonSerializer.Deserialize<List<GSM00720UploadCashFlowPlanPlanErrorDTO>>(loEx.ErrorList[0].ErrDescp);

        //            loUploadCashFlowPlanPlanList = DeserializeList.Select(x => new GSM00720UploadCashFlowPlanPlanDTO
        //            {
        //                CCashFlowPlan_GROUP_CODE = x.CCASH_FLOW_CODE,
        //                CCASH_FLOW_CODE = x.CCASH_FLOW_GROUP_CODE,
        //                CCYEAR = x.CYEAR,
        //                CPERIOD_NO = x.CPERIOD_NO,
        //                NLOCAL_AMOUNT = x.NLOCAL_AMOUNT,
        //                CNOTES = x.ErrorMessage,
        //                NBASE_AMOUNT = x.NBASE_AMOUNT,
        //                LOVERWRITE = false,
        //                CCOMPANY_ID = SelectedCompanyId,
        //            }).ToList();

        //            loUploadCashFlowPlanPlanDisplayList = new ObservableCollection<GSM00720UploadCashFlowPlanPlanDTO>(loUploadCashFlowPlanPlanList);
        //        }

        //        VisibleError = true;
        //        loException.ThrowExceptionIfErrors();
        //    }
        //}

        //public async Task GetUploadCashFlowPlanPlanListStreamAsync() //fiks
        //{
        //    R_Exception loException = new R_Exception();
        //    try
        //    {

        //        //R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.UPLOAD_CashFlowPlanPlan_STREAMING_CONTEXT, loUploadCashFlowPlanPlanList);
        //        //loRtn = await loModel.GetUploadList();
        //        //loUploadCashFlowPlanPlanDisplayList = new ObservableCollection<GSM00720UploadCashFlowPlanPlanDTO>(loRtn.Data);

        //        GSM00720ListDTO loResult = new GSM00720ListDTO();
        //        loResult = await loCashFlowPlanPlanModel.GetCashFlowPlanPlanStreamAsync();
        //        List<GSM00720UploadCashFlowPlanPlanDTO> loTemp = new List<GSM00720UploadCashFlowPlanPlanDTO>();
        //        loTemp = loUploadCashFlowPlanPlanList.Select(x => new GSM00720UploadCashFlowPlanPlanDTO()
        //        {
        //            CCOMPANY_ID = x.CCOMPANY_ID,
        //            CCashFlowPlan_GROUP_CODE = x.CCashFlowPlan_GROUP_CODE,
        //            CCashFlowPlan_GROUP_NAME = x.CCashFlowPlan_GROUP_NAME,
        //            CCASH_FLOW_NAME = x.CCASH_FLOW_NAME,
        //            CCASH_FLOW_CODE = x.CCASH_FLOW_CODE,
        //            CCYEAR = x.CCYEAR,
        //            CPERIOD_NO = x.CPERIOD_NO,
        //            NLOCAL_AMOUNT = x.NLOCAL_AMOUNT,
        //            NBASE_AMOUNT = x.NBASE_AMOUNT,
        //            CNOTES = x.CNOTES,
        //            LOVERWRITE = x.LOVERWRITE,
        //            LEXIST = loResult.Data.Any(y => x.CCASH_FLOW_CODE == y.CCASH_FLOW_CODE),
        //        }).ToList();

        //        loUploadCashFlowPlanPlanDisplayList = new ObservableCollection<GSM00720UploadCashFlowPlanPlanDTO>(loTemp);
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }
        //    loException.ThrowExceptionIfErrors();
        //}

        //public async Task CheckIsCashFlowPlanPlanUsedAsync()
        //{
        //    R_Exception loException = new R_Exception();
        //    try
        //    {
        //        loCheckIsCashFlowPlanPlanUsedRtn = await loModel.CheckResultUpload();
        //        loCheckIsCashFlowPlanPlanUsed = loCheckIsCashFlowPlanPlanUsedRtn.Data;
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }
        //    loException.ThrowExceptionIfErrors();
        //}
        //public async Task SaveUploadCashFlowPlanPlanAsync()
        //{
        //    var loEx = new R_Exception();
        //    R_BatchParameter loBatchPar;
        //    R_ProcessAndUploadClient loCls;
        //    string lcGuid = "";
        //    List<GSM00720UploadCashFlowPlanPlanDTO> Bigobject;
        //    List<R_KeyValue> loUserParam;

        //    try
        //    {
        //        loUserParam = new List<R_KeyValue>();


        //        loUserParam.Add(new R_KeyValue() { Key = ContextConstantGSM00700.IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

        //        //Instantiate ProcessClient
        //        loCls = new R_ProcessAndUploadClient(
        //            pcModuleName: "GS",
        //            plSendWithContext: true,
        //            plSendWithToken: true,
        //            pcHttpClientName: "R_DefaultServiceUrl",
        //            poProcessProgressStatus: this);

        //        //Check Data
        //        if (loUploadCashFlowPlanPlanDisplayList.Count == 0)
        //            return;

        //        Bigobject = loUploadCashFlowPlanPlanDisplayList.ToList<GSM00720UploadCashFlowPlanPlanDTO>();

        //        //preapare Batch Parameter
        //        loBatchPar = new R_BatchParameter();

        //        loBatchPar.COMPANY_ID = SelectedCompanyId;
        //        loBatchPar.USER_ID = SelectedUserId;
        //        loBatchPar.ClassName = "GSM00700Back.GSM00720UploadCashFlowPlanPlanPlanCls";
        //        loBatchPar.UserParameters = loUserParam;
        //        loBatchPar.BigObject = Bigobject;

        //        lcGuid = await loCls.R_BatchProcess<List<GSM00720UploadCashFlowPlanPlanDTO>>(loBatchPar, 6);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}

        //public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        //{
        //    if (poProcessResultMode == eProcessResultMode.Success)
        //    {
        //        Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
        //        await GetUploadCashFlowPlanPlanListStreamAsync();
        //    }

        //    if (poProcessResultMode == eProcessResultMode.Fail)
        //    {
        //        Message = string.Format("Process Complete but fail with GUID {0}", pcKeyGuid);
        //    }
        //    await Task.CompletedTask;
        //}

        //public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        //{
        //    Message = string.Format("Process Error with GUID {0}", pcKeyGuid);
        //    await Task.CompletedTask;
        //}

        //public async Task ReportProgress(int pnProgress, string pcStatus)
        //{
        //    Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

        //    Percentage = pnProgress;
        //    Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

        //    await Task.CompletedTask;
        //}
        //private async Task GetError(string pcKeyGuid)
        //{
        //    //R_APIException loException;
        //    //R_ProcessAndUploadClient loCls;
        //    //List<R_ErrorStatusReturn> loErrRtn;

        //    try
        //    {
        //        R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.UPLOAD_CENTER_ERROR_GUID_STREAMING_CONTEXT, pcKeyGuid);

        //        loErrorRtn = await loModel.GetErrorProcessListAsync();
        //        loErrorList = loErrorRtn.Data;


        //        loUploadCashFlowPlanPlanList = loErrorList.Select(x => new GSM00720UploadCashFlowPlanPlanDTO()
        //        {
        //            CCashFlowPlan_GROUP_CODE = x.CCASH_FLOW_GROUP_CODE,
        //            CCASH_FLOW_CODE = x.CCASH_FLOW_CODE,
        //            CCASH_FLOW_NAME = x.CYEAR,
        //            CPERIOD_NO = x.CPERIOD_NO,
        //            NLOCAL_AMOUNT = x.NLOCAL_AMOUNT,
        //            NBASE_AMOUNT = x.NBASE_AMOUNT,
        //            CNOTES = x.ErrorMessage,
        //            LEXIST = x.LEXIST,
        //            CCOMPANY_ID = SelectedCompanyId
        //        }).ToList();

        //        loUploadCashFlowPlanPlanDisplayList = new ObservableCollection<GSM00720UploadCashFlowPlanPlanDTO>(loUploadCashFlowPlanPlanList);

        //        VisibleError = true;
        //        IsUploadSuccesful = !VisibleError;

        //        //DO SOMETHING WITH ERROR

        //        //loErrRtn = await loCls.R_GetErrorProcess(new R_UploadAndProcessKey() { KEY_GUID = pcKeyGuid });
        //        //foreach (R_ErrorStatusReturn item in loErrRtn)
        //        //{
        //        //    Message = $"Error SeqNo {item.SeqNo} with msg {item.ErrorMessage}";
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }


        //}

        public string Message = "";
        public int Percentage = 0;
        public Action StateChangeAction { get; set; }
        public DataSet ExcelDataSet { get; set; }
        public Func<Task> ActionDataSetExcel { get; set; }
        public Action<R_Exception> DisplayErrorAction { get; set; }
        public string CashFlowGroupCode { get; set; } = "";
        public string CashFlowGroupName { get; set; } = "";
        public string CashFlowCode { get; set; } = "";
        public string CashFlowName { get; set; } = "";
        public string Year { get; set; } = "";
        public string CompanyId { get; set; }
        public string UserId { get; set; }

        public int SumListExcel { get; set; }
        public bool VisibleError { get; set; } = false;
        public int SumValidDataExcel { get; set; }
        public int SumInvalidDataExcel { get; set; }
        public ObservableCollection<GSM00720UploadErrorValidateDTO> CashFlowPlanValidateUploadError { get; set; } = new ObservableCollection<GSM00720UploadErrorValidateDTO>();


        public async Task ConvertGrid(List<GSM00720UploadExcelDTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                //Onchanged Visible Error
                VisibleError = false;
                VisibleError = false;
                SumValidDataExcel = 0;
                SumInvalidDataExcel = 0;


                // Convert Excel DTO And Add SeqNo
                List<GSM00720UploadErrorValidateDTO> Data = poEntity.Select((item, i) => new GSM00720UploadErrorValidateDTO
                {
                    NO = i + 1,
                    CPERIOD_NO = item.PeriodNo,
                    NLOCAL_AMOUNT = item.LocalAmount,
                    NBASE_AMOUNT = item.BaseAmount,
                    CCASHFLOW_GROUP_CODE = CashFlowGroupCode,
                    CCOMPANY_ID = CompanyId,
                    CCYEAR = Year,
                    CCASH_FLOW_CODE = CashFlowCode,

                }).ToList();


                SumListExcel = Data.Count;
                CashFlowPlanValidateUploadError = new ObservableCollection<GSM00720UploadErrorValidateDTO>(Data);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveBulkFile()
        {


            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<GSM00720UploadErrorValidateDTO> ListFromExcel;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue()
                { Key = ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE, Value = CashFlowGroupCode });
                loBatchParUserParameters.Add(new R_KeyValue()
                { Key = ContextConstantGSM00700.CCASH_FLOW_CODE, Value = CashFlowCode });
                loBatchParUserParameters.Add(new R_KeyValue()
                { Key = ContextConstantGSM00700.CYEAR, Value = Year });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //Set Data
                if (CashFlowPlanValidateUploadError.Count == 0)
                    return;

                ListFromExcel = CashFlowPlanValidateUploadError.ToList();


                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = CompanyId;
                loBatchPar.USER_ID = UserId;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "GSM00700Back.GSM00720UploadCashFlowPlanPlanCls";
                loBatchPar.BigObject = ListFromExcel;

                await loCls.R_BatchProcess<List<GSM00720UploadErrorValidateDTO>>(loBatchPar, 6);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #region ProgressStatus



        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                    VisibleError = false;
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                    await ServiceGetError(pcKeyGuid);
                    VisibleError = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            //IF ERROR CONNECTION, PROGRAM WILL RUN THIS METHOD
            R_Exception loException = new R_Exception();

            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);
            ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

            DisplayErrorAction.Invoke(loException);
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            StateChangeAction();
            await Task.CompletedTask;
        }

        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyId,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_GS_UPLOAD_CashFlowPlanResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandleEx = loResultData.Where(y => y.SeqNo <= 0).Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    loUnhandleEx.ForEach(x => loException.Add(x));
                }
                else
                {
                    // Display Error Handle if get seq
                    CashFlowPlanValidateUploadError.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, ErrorFlag and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.NO))
                        {
                            x.ErrorMessage = loResultData.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
                            x.ErrorFlag = true;
                            SumInvalidDataExcel++;
                        }
                        else
                        {
                            SumValidDataExcel++;
                        }
                    });

                    ////Set DataSetTable and get error
                    var loExcelData = R_FrontUtility.ConvertCollectionToCollection<GSM00720UploadExcelDTO>(CashFlowPlanValidateUploadError);

                    var loDataTable = R_FrontUtility.R_ConvertTo<GSM00720UploadExcelDTO>(loExcelData);
                    loDataTable.TableName = "CashFlowPlan";

                    var loDataSet = new DataSet();
                    loDataSet.Tables.Add(loDataTable);

                    // Asign Dataset
                    ExcelDataSet = loDataSet;

                    //// Dowload if get Error
                    //await ActionDataSetExcel.Invoke();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #endregion
    }

}
