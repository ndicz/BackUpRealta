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
using GSM00700Common.DTO;
using GSM00710Common.DTO.Upload_DTO_GSM00710;
using System.ComponentModel.Design;
using System.Data;


namespace GSM00700Model
{
    public class GSM00710UploadViewModel : R_IProcessProgressStatus
    {
        public string Message = "";
        public int Percentage = 0;
        public Action ShowSuccessAction { get; set; }
        public Action StateChangeAction { get; set; }
        public DataSet ExcelDataSet { get; set; }
        public Func<Task> ActionDataSetExcel { get; set; }
        public Action<R_Exception> DisplayErrorAction { get; set; }
        public string CashFlowGroupCode { get; set; } = "";
        public string CashFlowGroupName { get; set; } = "";
        public string CashflowTypeValue { get; set; } = "";
        public string CompanyId { get; set; }
        public string UserId { get; set; }

        public int SumListExcel { get; set; }
        public bool VisibleError { get; set; } = false;
        public int SumValidDataExcel { get; set; }
        public int SumInvalidDataExcel { get; set; }
        public ObservableCollection<GSM00710UploadErrorValidateDTO> CashflowValidateUploadError { get; set; } = new ObservableCollection<GSM00710UploadErrorValidateDTO>();


        public async Task ConvertGrid(List<GSM00710UploadExcelDTO> poEntity)
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
                List<GSM00710UploadErrorValidateDTO> Data = poEntity.Select((item, i) => new GSM00710UploadErrorValidateDTO
                {
                    NO = i + 1,
                    CSEQ = item.DisplaySeq,
                    CCASHFLOW_CODE = item.CashFlowCode,
                    CCASH_FLOW_NAME = item.CashFlowName,
                    CCASHFLOW_TYPE = item.CashFlowType,
                    CCASHFLOW_GROUP_CODE = CashFlowGroupCode,
                    CCOMPANY_ID = CompanyId
                }).ToList();

                SumListExcel = Data.Count;
                CashflowValidateUploadError = new ObservableCollection<GSM00710UploadErrorValidateDTO>(Data);
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
            List<GSM00710UploadErrorValidateDTO> ListFromExcel;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue()
                    { Key = ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE, Value = CashFlowGroupCode });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //Set Data
                if (CashflowValidateUploadError.Count == 0)
                    return;

                ListFromExcel = CashflowValidateUploadError.ToList();

     
                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = CompanyId;
                loBatchPar.USER_ID = UserId;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "GSM00700Back.GSM00710UploadCashFlowCls";
                loBatchPar.BigObject = ListFromExcel;

                await loCls.R_BatchProcess<List<GSM00710UploadErrorValidateDTO>>(loBatchPar,5);
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
                    ShowSuccessAction();
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
                    RESOURCE_NAME = "RSP_GS_UPLOAD_CASHFLOWResources"
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
                    CashflowValidateUploadError.ToList().ForEach(x =>
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
                    var loExcelData = R_FrontUtility.ConvertCollectionToCollection<GSM00710UploadExcelDTO>(CashflowValidateUploadError);

                    var loDataTable = R_FrontUtility.R_ConvertTo<GSM00710UploadExcelDTO>(loExcelData);
                    loDataTable.TableName = "CashFlow";

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
