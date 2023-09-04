using GSM00700Common.DTO.Upload_DTO;
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

namespace GSM00700Model
{
    public class GSM00720UploadViewModel : R_ViewModel<GSM00720UploadCashFlowPlanDTO>, R_IProcessProgressStatus
    {
        private GSM00720UploadModel loModel = new GSM00720UploadModel();
        private GSM00720Model loCashflowPlanModel = new GSM00720Model();

        public GSM00720UploadCashFlowPlanErrorResultDTO loErrorRtn = new GSM00720UploadCashFlowPlanErrorResultDTO();
        public GSM00720UploadCashFlowPlanResultDTO loRtn = new GSM00720UploadCashFlowPlanResultDTO();
        public GSM00720UploadCashFlowPlanCheckUsedDTO loCheckIsCashFlowPlanUsed = new GSM00720UploadCashFlowPlanCheckUsedDTO();
        public GSM00720UploadCashFlowPlanCheckResultDTO loCheckIsCashFlowPlanUsedRtn = new GSM00720UploadCashFlowPlanCheckResultDTO();
        public GSM00720UploadCashFlowPlanParameterDTO loParameter = new GSM00720UploadCashFlowPlanParameterDTO();

        public ObservableCollection<GSM00720UploadCashFlowPlanDTO> loUploadCashFlowPlanDisplayList = new ObservableCollection<GSM00720UploadCashFlowPlanDTO>();

        public List<GSM00720UploadCashFlowPlanDTO> loUploadCashFlowPlanList = new List<GSM00720UploadCashFlowPlanDTO>();
        public List<GSM00720UploadCashFlowPlanErrorDTO> loErrorList = new List<GSM00720UploadCashFlowPlanErrorDTO>();

        public string SelectedCompanyId = "";
        public string SelectedUserId = "";

        public bool IsOverWrite = false;

        public string CashFlowPlanGroupCode = "";
        public string CashFlowPlanGroupName = "";
        public string CashFlowPlanCode = "";
        public string CashFlowPlanName = "";
        public string CashFlowPlanYear = "";

        public bool IsUploadSuccesful = true;
        public string Message = "";
        public int Percentage = 0;
        public bool OverwriteData = false;
        public byte[] fileByte = null;

        public bool VisibleError = false;
        public bool IsErrorEmptyFile = false;

        public async Task ValidateUpload()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchValidatePar;
            R_ProcessAndUploadClient loCls;
            List<GSM00720UploadCashFlowPlanErrorDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();
                loUserParam.Add(new R_KeyValue() { Key = ContextConstantGSM00700.IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loBatchValidatePar = new R_BatchParameter();
                loBatchValidatePar.COMPANY_ID = SelectedCompanyId;
                loBatchValidatePar.USER_ID = SelectedUserId;
                loBatchValidatePar.UserParameters = loUserParam;
                loBatchValidatePar.ClassName = "GSM00700Back.GSM00720UploadCashFlowPlanValidateCls";
                loBatchValidatePar.BigObject = loUploadCashFlowPlanList;

                await loCls.R_BatchProcess<List<GSM00720UploadCashFlowPlanDTO>>(loBatchValidatePar, 6);

                VisibleError = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        public void ReadExcelFile()
        {
            var loEx = new R_Exception();
            List<GSM00720UploadCashFlowPlanExcelDTO> loExtract = new List<GSM00720UploadCashFlowPlanExcelDTO>();
            try
            {
                //add filebyte to DTO
                var loExcel = new R_Excel();

                var loDataSet = loExcel.R_ReadFromExcel(fileByte, new string[] { "CashFlowPlan" });

                var loResult = R_FrontUtility.R_ConvertTo<GSM00720UploadCashFlowPlanExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<GSM00720UploadCashFlowPlanExcelDTO>(loResult);

                loUploadCashFlowPlanList = loExtract.Select(x => new GSM00720UploadCashFlowPlanDTO
                {
                    CPERIOD_NO = x.PeriodNo,
                    NLOCAL_AMOUNT = x.LocalAmount,
                    NBASE_AMOUNT = x.BaseAmount,
                    CCASHFLOW_GROUP_CODE = CashFlowPlanGroupCode,
                    CCASH_FLOW_CODE = CashFlowPlanCode,
                    CCYEAR = CashFlowPlanYear,
                    CNOTES = "",
                    LEXIST = false,

                }).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task AttachFile()
        {
            var loEx = new R_Exception();
            R_UploadPar loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<GSM00720UploadCashFlowPlanDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            R_Exception loException = new R_Exception();
            try
            {
                loUserParam = new List<R_KeyValue>();
                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loUploadPar = new R_UploadPar();
                loUploadPar.COMPANY_ID = SelectedCompanyId;
                loUploadPar.USER_ID = SelectedUserId;
                loUploadPar.UserParameters = loUserParam;
                loUploadPar.ClassName = "GSM00700Back.GSM00720UploadCashFlowPlanPlanCls";
                loUploadPar.BigObject = loUploadCashFlowPlanList;

                await loCls.R_AttachFile<List<GSM00720UploadCashFlowPlanDTO>>(loUploadPar);

                VisibleError = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);

                if (loEx.ErrorList.Count > 0)
                {
                    var DeserializeList = JsonSerializer.Deserialize<List<GSM00720UploadCashFlowPlanErrorDTO>>(loEx.ErrorList[0].ErrDescp);

                    loUploadCashFlowPlanList = DeserializeList.Select(x => new GSM00720UploadCashFlowPlanDTO
                    {
                        CCASHFLOW_GROUP_CODE = x.CCASH_FLOW_CODE,
                        CCASH_FLOW_CODE = x.CCASH_FLOW_GROUP_CODE,
                        CCYEAR = x.CYEAR,
                        CPERIOD_NO = x.CPERIOD_NO,
                        NLOCAL_AMOUNT = x.NLOCAL_AMOUNT,
                        CNOTES = x.ErrorMessage,
                        NBASE_AMOUNT = x.NBASE_AMOUNT,
                        LOVERWRITE = false,
                        CCOMPANY_ID = SelectedCompanyId,
                    }).ToList();

                    loUploadCashFlowPlanDisplayList = new ObservableCollection<GSM00720UploadCashFlowPlanDTO>(loUploadCashFlowPlanList);
                }

                VisibleError = true;
                loException.ThrowExceptionIfErrors();
            }
        }

        public async Task GetUploadCashFlowPlanListStreamAsync() //fiks
        {
            R_Exception loException = new R_Exception();
            try
            {

                //R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.UPLOAD_CashFlowPlan_STREAMING_CONTEXT, loUploadCashFlowPlanList);
                //loRtn = await loModel.GetUploadList();
                //loUploadCashFlowPlanDisplayList = new ObservableCollection<GSM00720UploadCashFlowPlanDTO>(loRtn.Data);

                GSM00720ListDTO loResult = new GSM00720ListDTO();
                loResult = await loCashflowPlanModel.GetCashFlowPlanStreamAsync();
                List<GSM00720UploadCashFlowPlanDTO> loTemp = new List<GSM00720UploadCashFlowPlanDTO>();
                loTemp = loUploadCashFlowPlanList.Select(x => new GSM00720UploadCashFlowPlanDTO()
                {
                    CCOMPANY_ID = x.CCOMPANY_ID,
                    CCASHFLOW_GROUP_CODE = x.CCASHFLOW_GROUP_CODE,
                    CCASHFLOW_GROUP_NAME = x.CCASHFLOW_GROUP_NAME,
                    CCASH_FLOW_NAME = x.CCASH_FLOW_NAME,
                    CCASH_FLOW_CODE = x.CCASH_FLOW_CODE,
                    CCYEAR = x.CCYEAR,
                    CPERIOD_NO = x.CPERIOD_NO,
                    NLOCAL_AMOUNT = x.NLOCAL_AMOUNT,
                    NBASE_AMOUNT = x.NBASE_AMOUNT,
                    CNOTES = x.CNOTES,
                    LOVERWRITE = x.LOVERWRITE,
                    LEXIST = loResult.Data.Any(y => x.CCASH_FLOW_CODE == y.CCASH_FLOW_CODE),
                }).ToList();

                loUploadCashFlowPlanDisplayList = new ObservableCollection<GSM00720UploadCashFlowPlanDTO>(loTemp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task CheckIsCashFlowPlanUsedAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loCheckIsCashFlowPlanUsedRtn = await loModel.CheckResultUpload();
                loCheckIsCashFlowPlanUsed = loCheckIsCashFlowPlanUsedRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task SaveUploadCashFlowPlanAsync()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<GSM00720UploadCashFlowPlanDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();


                loUserParam.Add(new R_KeyValue() { Key = ContextConstantGSM00700.IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //Check Data
                if (loUploadCashFlowPlanDisplayList.Count == 0)
                    return;

                Bigobject = loUploadCashFlowPlanDisplayList.ToList<GSM00720UploadCashFlowPlanDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "GSM00700Back.GSM00720UploadCashFlowPlanPlanCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                lcGuid = await loCls.R_BatchProcess<List<GSM00720UploadCashFlowPlanDTO>>(loBatchPar, 6);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            if (poProcessResultMode == eProcessResultMode.Success)
            {
                Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                await GetUploadCashFlowPlanListStreamAsync();
            }

            if (poProcessResultMode == eProcessResultMode.Fail)
            {
                Message = string.Format("Process Complete but fail with GUID {0}", pcKeyGuid);
            }
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            await Task.CompletedTask;
        }
        private async Task GetError(string pcKeyGuid)
        {
            //R_APIException loException;
            //R_ProcessAndUploadClient loCls;
            //List<R_ErrorStatusReturn> loErrRtn;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.UPLOAD_CENTER_ERROR_GUID_STREAMING_CONTEXT, pcKeyGuid);

                loErrorRtn = await loModel.GetErrorProcessListAsync();
                loErrorList = loErrorRtn.Data;


                loUploadCashFlowPlanList = loErrorList.Select(x => new GSM00720UploadCashFlowPlanDTO()
                {
                    CCASHFLOW_GROUP_CODE = x.CCASH_FLOW_GROUP_CODE,
                    CCASH_FLOW_CODE = x.CCASH_FLOW_CODE,
                    CCASH_FLOW_NAME = x.CYEAR,
                    CPERIOD_NO = x.CPERIOD_NO,
                    NLOCAL_AMOUNT = x.NLOCAL_AMOUNT,
                    NBASE_AMOUNT = x.NBASE_AMOUNT,
                    CNOTES = x.ErrorMessage,
                    LEXIST = x.LEXIST,
                    CCOMPANY_ID = SelectedCompanyId
                }).ToList();

                loUploadCashFlowPlanDisplayList = new ObservableCollection<GSM00720UploadCashFlowPlanDTO>(loUploadCashFlowPlanList);

                VisibleError = true;
                IsUploadSuccesful = !VisibleError;

                //DO SOMETHING WITH ERROR

                //loErrRtn = await loCls.R_GetErrorProcess(new R_UploadAndProcessKey() { KEY_GUID = pcKeyGuid });
                //foreach (R_ErrorStatusReturn item in loErrRtn)
                //{
                //    Message = $"Error SeqNo {item.SeqNo} with msg {item.ErrorMessage}";
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
    }
}
