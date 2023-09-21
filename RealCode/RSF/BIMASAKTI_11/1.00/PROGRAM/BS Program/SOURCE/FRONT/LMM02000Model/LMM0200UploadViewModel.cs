using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM02000Common;
using LMM02000Common.DTO.UPLOAD_DTO_LMM02000;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace LMM02000Model
{
    public class LMM0200UploadViewModel : R_IProcessProgressStatus
    {
        public string Message = "";
        public int Percentage = 0;

        public Action ShowSuccessAction { get; set; }
        public Action StateChangeAction { get; set; }
        public DataSet ExcelDataSet { get; set; }
        public Func<Task> ActionDataSetExcel { get; set; }
        public Action<R_Exception> DisplayErrorAction { get; set; }
        public string PropertyId { get; set; } = "";
        public string PropertyName { get; set; } = "";
        public string SalesmanTypeValue { get; set; } = "";
        public string CompanyId { get; set; }
        public string UserId { get; set; }

        public int SumListExcel { get; set; }
        public bool VisibleError { get; set; } = false;
        public int SumValidDataExcel { get; set; }
        public int SumInvalidDataExcel { get; set; }
        public ObservableCollection<LMM02000UploadErrorValidateDTO> SalesmanValidateUploadError { get; set; } = new ObservableCollection<LMM02000UploadErrorValidateDTO>();


        public async Task ConvertGrid(List<LMM02000UploadExcelDTO> poEntity)
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
                List<LMM02000UploadErrorValidateDTO> Data = poEntity.Select((item, i) => new LMM02000UploadErrorValidateDTO
                {
                    NO = i + 1,
                    SalesmanId = item.SalesmanId,
                    SalesmanName = item.SalesmanName,
                    Active = item.Active,
                    Address = item.Address,
                    EmailAddress = item.EmailAddress,
                    MobileNo1 = item.MobileNo1,
                    MobileNo2 = item.MobileNo2,
                    NIK = item.NIK,
                    Gender = item.Gender,
                    SalesmanType = item.SalesmanType,
                    CompanyName = item.CompanyName,
                    NonActiveDate = item.NonActiveDate
                }).ToList();

                SumListExcel = Data.Count;
                SalesmanValidateUploadError = new ObservableCollection<LMM02000UploadErrorValidateDTO>(Data);
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
            List<LMM02000UploadErrorValidateDTO> ListFromExcel;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue()
                { Key = ContextConstantLMM02000.CPROPERTY_ID, Value = PropertyId });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM",
                    poProcessProgressStatus: this);

                //Set Data
                if (SalesmanValidateUploadError.Count == 0)
                    return;

                ListFromExcel = SalesmanValidateUploadError.ToList();


                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = CompanyId;
                loBatchPar.USER_ID = UserId;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "LMM02000Back.LMM02000UploadSalesmanCls";
                loBatchPar.BigObject = ListFromExcel;

                await loCls.R_BatchProcess<List<LMM02000UploadErrorValidateDTO>>(loBatchPar, 10);
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
                    RESOURCE_NAME = "RSP_LM_UPLOAD_SALESMANResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM");

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
                    SalesmanValidateUploadError.ToList().ForEach(x =>
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
                    var loExcelData = R_FrontUtility.ConvertCollectionToCollection<LMM02000UploadExcelDTO>(SalesmanValidateUploadError);

                    var loDataTable = R_FrontUtility.R_ConvertTo<LMM02000UploadExcelDTO>(loExcelData);
                    loDataTable.TableName = "Salesman";

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
