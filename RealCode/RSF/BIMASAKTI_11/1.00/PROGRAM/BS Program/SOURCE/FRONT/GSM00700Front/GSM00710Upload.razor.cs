using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GSM00700Common.DTO;
using GSM00700Model;
using GSM00710Common.DTO.Upload_DTO_GSM00710;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GSM00700Front
{
    public partial class GSM00710Upload : R_Page
    {

        private GSM00710UploadViewModel _viewModel = new GSM00710UploadViewModel();
        private GSM00710ViewModel _gsm00710ViewModel = new GSM00710ViewModel();
        private R_Grid<GSM00710UploadErrorValidateDTO> _CashFlowMoveDetail_gridRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        [Inject] R_IExcel ExcelInject { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] private IClientHelper ClientHelper { get; set; }

        private bool FileHasData = false;

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        #region HandleError
        private void DisplayErrorInvoke(R_Exception poException)
        {
            this.R_DisplayException(poException);
        }
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                //var Param = (GSM00710UploadErrorValidateParamDTO)poParameter;

                ////Assign Company, User and others value for Parameters
                var poParam = (GSM00710UploadDTO)poParameter;

                _viewModel.CashFlowGroupCode = poParam.CCASHFLOW_GROUP_CODE;
                _viewModel.CashFlowGroupName = poParam.CCASHFLOW_GROUP_NAME;


                _viewModel.CompanyId = ClientHelper.CompanyId;
                _viewModel.UserId = ClientHelper.UserId;
                //_viewModel.PropertyValue = Param.CPROPERTY_ID;
                //_viewModel.PropertyName = Param.CPROPERTY_NAME;
                //_viewModel.JournalGroupTypeValue = Param.CJRNGRP_TYPE;

                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
                _viewModel.DisplayErrorAction = DisplayErrorInvoke;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task SourceUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //get file name
                // _viewModel.SourceFileName = eventArgs.File.Name;

                //import excel from user
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var fileByte = loMS.ToArray();

                //READ EXCEL
                var loExcel = ExcelInject;

                var loDataSet = loExcel.R_ReadFromExcel(fileByte, new[] { "CashFlow" });
                var loResult = R_FrontUtility.R_ConvertTo<GSM00710UploadExcelDTO>(loDataSet.Tables[0]);

                FileHasData = loResult.Count > 0 ? true : false;

                await _CashFlowMoveDetail_gridRef.R_RefreshGrid(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Upload_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                List<GSM00710UploadExcelDTO> loData = (List<GSM00710UploadExcelDTO>)eventArgs.Parameter;

                await _viewModel.ConvertGrid(loData);
                eventArgs.ListEntityResult = _viewModel.CashflowValidateUploadError;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure want to import data?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SaveBulkFile();

                    if (_viewModel.VisibleError)
                    {
                        await R_MessageBox.Show("", "Journal Group uploaded successfully!", R_eMessageBoxButtonType.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickSaveAsync()
        {
            var loEx = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure want to save to excel again?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await ActionFuncDataSetExcel();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        // Create Method Action For Download Excel if Has Error
        private async Task ActionFuncDataSetExcel()
        {
            var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
            var lcName = $"Cash Flow Plan{_viewModel.CashFlowGroupCode}" +".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }

        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }

    }
}



//public void StateChangeInvoke()
//{
//    StateHasChanged();
//}
//protected override async Task R_Init_From_Master(object poParameter)
//{
//    var loEx = new R_Exception();
//    var param = (GSM00710UploadCashFlowDTO)poParameter;
//    try
//    {
//        _ViewModelUpload.CashFlowGroupCode = param.CCASHFLOW_GROUP_CODE;
//        _ViewModelUpload.CashFlowGroupName = param.CCASHFLOW_GROUP_NAME;
//        _ViewModelUpload.StateChangeAction = StateChangeInvoke;
//        _ViewModelUpload.SelectedCompanyId = loClientHelper.CompanyId;
//        _ViewModelUpload.SelectedUserId = loClientHelper.UserId;
//    }
//    catch (Exception ex)
//    {
//        loEx.Add(ex);
//    }

//    loEx.ThrowExceptionIfErrors();
//}
//private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
//{
//    var loData = (GSM00710UploadCashFlowDTO)eventArgs.Data;

//    if (loData.LEXIST)
//    {
//        eventArgs.RowStyle = new R_GridRowRenderStyle
//        {
//            FontColor = "red"
//        };
//    }
//}

//private async Task OnSaveToExcel()
//{
//    var loEx = new R_Exception();

//    try
//    {
//        List<GSM00710UploadCashFlowExcelDTO> loExcelList = new List<GSM00710UploadCashFlowExcelDTO>();

//        loExcelList = _ViewModelUpload.loUploadCashFlowDisplayList.Select(x => new GSM00710UploadCashFlowExcelDTO()
//        {
//            DisplaySeq = x.CSEQ,
//            CashFlowCode = x.CCASHFLOW_CODE,
//            CashFlowName = x.CCASH_FLOW_NAME,
//            CashFlowType = x.CCASHFLOW_TYPE,
//            Notes = x.NOTES_,
//        }).ToList();

//        var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
//        loDataTable.TableName = "CashFlow";

//        //export to excel
//        var loByteFile = Excel.R_WriteToExcel(loDataTable);
//        var saveFileName = $"Upload Error CashFlow.xlsx";

//        await JS.downloadFileFromStreamHandler(saveFileName, loByteFile);
//        await this.Close(true, true);

//    }
//    catch (Exception ex)
//    {
//        loEx.Add(ex);
//    }
//B:
//    loEx.ThrowExceptionIfErrors();
//}

//public void ReadExcelFile()
//{
//    var loEx = new R_Exception();
//    List<GSM00710UploadCashFlowExcelDTO> loExtract = new List<GSM00710UploadCashFlowExcelDTO>();
//    try
//    {
//        var loDataSet = Excel.R_ReadFromExcel(_ViewModelUpload.fileByte, new string[] { "CashFlow" });

//        var loResult = R_FrontUtility.R_ConvertTo<GSM00710UploadCashFlowExcelDTO>(loDataSet.Tables[0]);

//        loExtract = new List<GSM00710UploadCashFlowExcelDTO>(loResult);

//        _ViewModelUpload.loUploadCashFlowList = loExtract.Select(x => new GSM00710UploadCashFlowDTO
//        {

//            CSEQ = x.DisplaySeq,
//            CCASHFLOW_CODE = x.CashFlowCode,
//            CCASHFLOW_GROUP_CODE = _ViewModelUpload.CashFlowGroupCode,
//            CCASH_FLOW_NAME = x.CashFlowName,
//            CCASHFLOW_TYPE = x.CashFlowType,
//            NOTES_ = "",
//            LEXIST = false,
//        }).ToList();
//    }
//    catch (Exception ex)
//    {
//        loEx.Add(ex);
//        var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
//        _ViewModelUpload.IsErrorEmptyFile = MatchingError != null;
//    }
//    loEx.ThrowExceptionIfErrors();
//}
//private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
//{
//    var loEx = new R_Exception();

//    try
//    {
//        var loMS = new MemoryStream();
//        await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
//        _ViewModelUpload.fileByte = loMS.ToArray();

//        ReadExcelFile();

//        if (eventArgs.File.Name.Contains(".xlsx") == false)
//        {
//            await R_MessageBox.Show("", "File Type must Microsoft Excel .xlsx", R_eMessageBoxButtonType.OK);
//        }
//        if (eventArgs.File.Name.Length > 0)
//        {
//            IsFileExist = true;
//        }
//        else
//        {
//            IsFileExist = false;
//        }
//        await _ConGriedRef.R_RefreshGrid(null);
//    }
//    catch (Exception ex)
//    {
//        if (_ViewModelUpload.IsErrorEmptyFile)
//        {
//            await R_MessageBox.Show("", "File is Empty", R_eMessageBoxButtonType.OK);
//        }
//        else
//        {
//            loEx.Add(ex);
//        }
//    }

//    //loEx.ThrowExceptionIfErrors();
//}

//private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
//{
//    var loEx = new R_Exception();

//    try
//    {
//        //await _ViewModelUpload.AttachFile();
//        await _ViewModelUpload.ValidateUpload();

//        IsUploadSuccesful = !_ViewModelUpload.VisibleError;
//        eventArgs.ListEntityResult = _ViewModelUpload.loUploadCashFlowDisplayList;
//    }
//    catch (Exception ex)
//    {
//        loEx.Add(ex);
//    }

//    //loEx.ThrowExceptionIfErrors();
//}

//private async Task OnProcess()
//{
//    R_Exception loException = new R_Exception();
//    try
//    {
//        //await _ViewModelUpload.CheckIsCashFlowUsedAsync();
//        if (_ViewModelUpload.IsUsed)
//        {
//          return;  
//        }
//        if (_ViewModelUpload.loCheckIsCashFlowUsed.LRESULT == true)
//        {
//            _ViewModelUpload.Message = string.Format("Cannot Load Cash Flow, existing Cash Flow already in use");
//        }
//        else
//        {
//            _ViewModelUpload.Message = string.Format("Cannot Load Cash Flow");
//        }
//        await R_ConductorGrid.R_SaveBatch();
//    }
//    catch (Exception ex)
//    {

//        loException.Add(ex);
//    }
//    loException.ThrowExceptionIfErrors();
//}
//private async Task OnCancel()
//{
//    await this.Close(true, false);
//}
//private async Task R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
//{
//    var loEx = new R_Exception();
//    try
//    {

//        var loTemp = await R_MessageBox.Show("", "Are You sure want process these records? ", R_eMessageBoxButtonType.YesNo);

//        if (loTemp == R_eMessageBoxResult.No)
//        {
//            events.Cancel = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        loEx.Add(ex);
//    }
//    loEx.ThrowExceptionIfErrors();
//}

//private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
//{
//    var loEx = new R_Exception();

//    try
//    {
//        _ViewModelUpload.loUploadCashFlowList = (List<GSM00710UploadCashFlowDTO>)eventArgs.Data;
//        await _ViewModelUpload.SaveUploadCashFlowAsync();



//    }
//    catch (Exception ex)
//    {
//        loEx.Add(ex);
//    }

//    loEx.ThrowExceptionIfErrors();
//}

//private async Task R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
//{
//    var loEx = new R_Exception();

//    try
//    {
//        await this.Close(true, true);

//    }
//    catch (Exception ex)
//    {
//        loEx.Add(ex);
//    }

//    loEx.ThrowExceptionIfErrors();
//}