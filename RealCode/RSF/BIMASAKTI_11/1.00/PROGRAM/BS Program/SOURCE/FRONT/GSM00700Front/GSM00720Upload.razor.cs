using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GSM00700Common.DTO;
using GSM00700Common.DTO.Upload_DTO_GSM00720;
using GSM00700Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
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
    public partial class GSM00720Upload : R_Page
    {
        private GSM00720UploadViewModel _viewModel = new GSM00720UploadViewModel();
        private GSM00720ViewModel _gsm00720ViewModel = new GSM00720ViewModel();
        private R_Grid<GSM00720UploadErrorValidateDTO> _CashFlowPlanMoveDetail_gridRef;

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
        public async Task ShowSuccessInvoke()
        {
            var loValidate = await R_MessageBox.Show("", "Upload Successfully", R_eMessageBoxButtonType.OK);
            if (loValidate == R_eMessageBoxResult.OK)
            {
                await this.Close(true, true);
            }
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                //var Param = (GSM00720UploadErrorValidateParamDTO)poParameter;

                ////Assign Company, User and others value for Parameters
                var poParam = (GSM00720UploadDTO)poParameter;

                _viewModel.CashFlowGroupCode = poParam.CCASHFLOW_GROUP_CODE;
                _viewModel.CashFlowGroupName = poParam.CCASHFLOW_GROUP_NAME;
                _viewModel.CashFlowCode = poParam.CCASH_FLOW_CODE;
                _viewModel.CashFlowName = poParam.CCASH_FLOW_NAME;
                _viewModel.Year = poParam.CCYEAR;


                _viewModel.CompanyId = ClientHelper.CompanyId;
                _viewModel.UserId = ClientHelper.UserId;
                //_viewModel.PropertyValue = Param.CPROPERTY_ID;
                //_viewModel.PropertyName = Param.CPROPERTY_NAME;
                //_viewModel.JournalGroupTypeValue = Param.CJRNGRP_TYPE;

                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
                _viewModel.DisplayErrorAction = DisplayErrorInvoke;
                _viewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
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

                var loDataSet = loExcel.R_ReadFromExcel(fileByte, new[] { "CashFlowPlan" });
                var loResult = R_FrontUtility.R_ConvertTo<GSM00720UploadExcelDTO>(loDataSet.Tables[0]);

                FileHasData = loResult.Count > 0 ? true : false;

                await _CashFlowPlanMoveDetail_gridRef.R_RefreshGrid(loResult);
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
                List<GSM00720UploadExcelDTO> loData = (List<GSM00720UploadExcelDTO>)eventArgs.Parameter;

                await _viewModel.ConvertGrid(loData);
                eventArgs.ListEntityResult = _viewModel.CashFlowPlanValidateUploadError;
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
            var lcName = $"Cash Flow Plan{_viewModel.CashFlowGroupCode}" + ".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }

        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }

    }
}