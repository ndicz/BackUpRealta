using GSM00700Common.DTO.Upload_DTO;
using GSM00700Model.Model;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Excel;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GSM00700Common;


namespace GSM00700Model
{
    public class GSM00710UploadViewModel : R_ViewModel<GSM00710UploadCashFlowDTO>, R_IProcessProgressStatus
    {
        private GSM00710UploadModel loModel = new GSM00710UploadModel();
        public ObservableCollection<GSM00710UploadCashFlowDTO> loUploadCashFlowDisplayList = new ObservableCollection<GSM00710UploadCashFlowDTO>();

        public List<GSM00710UploadCashFlowDTO> loUploadCashFlowList = new List<GSM00710UploadCashFlowDTO>();

        public GSM00710UploadCashFlowResultDTO loRtn = new GSM00710UploadCashFlowResultDTO();

        public GSM00710UploadCashFlowCheckUsedDTO loCheckIsCashFlowUsed = new GSM00710UploadCashFlowCheckUsedDTO();

        public GSM00710UploadCashFlowCheckRessultDTO loCheckIsCashFlowUsedRtn = new GSM00710UploadCashFlowCheckRessultDTO();

        public GSM00710UploadCashFlowParameterDTO loParameter = new GSM00710UploadCashFlowParameterDTO();

        public string SelectedCompanyId = "";
        public string SelectedUserId = "";

        public bool IsOverWrite = false;

        public string CashFlowGroupCode = "";
        public string CashFlowGroupName = "";
        public string Message = "";
        public int Percentage = 0;
        public bool OverwriteData = false;
        public byte[] fileByte = null;

        public bool VisibleError = false;
        public bool IsErrorEmptyFile = false;

        public void ReadExcelFile()
        {
            var loEx = new R_Exception();
            List<GSM00710UploadCashFlowExcelDTO> loExtract = new List<GSM00710UploadCashFlowExcelDTO>();
            try
            {
                //add filebyte to DTO
                var loExcel = new R_Excel();

                var loDataSet = loExcel.R_ReadFromExcel(fileByte, new string[] { "CashFlow" });

                var loResult = R_FrontUtility.R_ConvertTo<GSM00710UploadCashFlowExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<GSM00710UploadCashFlowExcelDTO>(loResult);

                loUploadCashFlowList = loExtract.Select(x => new GSM00710UploadCashFlowDTO
                {
                    CSEQ = x.DisplaySeq,
                    CCASHFLOW_CODE = x.CashFlowCode,
                    CCASHFLOW_GROUP_CODE = CashFlowGroupCode,
                    CCASH_FLOW_NAME = x.CashFlowName,
                    CCASHFLOW_TYPE = x.CashFlowType,
                    NOTES_ = "",
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
            List<GSM00710UploadCashFlowDTO> Bigobject;
            List<R_KeyValue> loUserParam;

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
                loUploadPar.ClassName = "GSM00700Back.GSM00710UploadCashFlowCls";
                loUploadPar.BigObject = loUploadCashFlowList;

                await loCls.R_AttachFile<List<GSM00710UploadCashFlowDTO>>(loUploadPar);

                VisibleError = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);

                if (loEx.ErrorList.Count > 0)
                {
                    var DeserializeList = JsonSerializer.Deserialize<List<GSM00710UploadCashFlowErrorDTO>>(loEx.ErrorList[0].ErrDescp);

                    loUploadCashFlowList = DeserializeList.Select(x => new GSM00710UploadCashFlowDTO
                    {
                        CCASH_FLOW_NAME = x.CCASH_FLOW_NAME,
                        CCASHFLOW_TYPE = x.CCASHFLOW_TYPE,
                        CCASHFLOW_CODE = x.CCASHFLOW_CODE,
                        CSEQ = x.CSEQ,
                        CCASHFLOW_GROUP_CODE = x.CCASHFLOW_GROUP_CODE,
                        NOTES_ = x.ErrorMessage,
                        LOVER_WRITE = false,
                        CCOMPANY_ID = SelectedCompanyId,
                    }).ToList();

                    loUploadCashFlowDisplayList = new ObservableCollection<GSM00710UploadCashFlowDTO>(loUploadCashFlowList);
                }

                VisibleError = true;
            }
        }

        public async Task GetUploadCashFlowListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.UPLOAD_CASHFLOW_STREAMING_CONTEXT, loUploadCashFlowList);
                loRtn = await loModel.GetUploadList();
                loUploadCashFlowDisplayList = new ObservableCollection<GSM00710UploadCashFlowDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task CheckIsCashFlowUsedAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loCheckIsCashFlowUsedRtn = await loModel.CheckResultUpload();
                loCheckIsCashFlowUsed = loCheckIsCashFlowUsedRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task SaveUploadCashFlowAsync()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<GSM00710UploadCashFlowDTO> Bigobject;
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
                if (loUploadCashFlowDisplayList.Count == 0)
                    return;

                Bigobject = loUploadCashFlowDisplayList.ToList<GSM00710UploadCashFlowDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "GSM00700Back.GSM00710UploadCashFlowCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                lcGuid = await loCls.R_BatchProcess<List<GSM00710UploadCashFlowDTO>>(loBatchPar, Bigobject.Count);
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
    }
}
