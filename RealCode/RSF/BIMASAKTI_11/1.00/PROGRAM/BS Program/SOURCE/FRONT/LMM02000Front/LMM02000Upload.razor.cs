using BlazorClientHelper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM02000Common.DTO.UPLOAD_DTO_LMM02000;
using LMM02000Model;

namespace LMM02000Front
{
    public partial class LMM02000Upload
    {
        private LMM0200UploadViewModel _viewModel = new LMM0200UploadViewModel();
        private LMM02000ViewModel _LMM02000ViewModel = new LMM02000ViewModel();
        private R_Grid<LMM02000UploadErrorValidateDTO> _SalesmanMoveDetail_gridRef;

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
                //var Param = (LMM02000UploadErrorValidateParamDTO)poParameter;

                ////Assign Company, User and others value for Parameters
                var poParam = (LMM02000UploadDTO)poParameter;

                _viewModel.PropertyId = poParam.CPROPERTY_ID;
                _viewModel.PropertyName = poParam.CPROPERTY_NAME;

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

                var loDataSet = loExcel.R_ReadFromExcel(fileByte, new[] { "Salesman" });
                var loResult = R_FrontUtility.R_ConvertTo<LMM02000UploadExcelDTO>(loDataSet.Tables[0]);

                FileHasData = loResult.Count > 0 ? true : false;

                await _SalesmanMoveDetail_gridRef.R_RefreshGrid(loResult);
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
                List<LMM02000UploadExcelDTO> loData = (List<LMM02000UploadExcelDTO>)eventArgs.Parameter;

                await _viewModel.ConvertGrid(loData);
                eventArgs.ListEntityResult = _viewModel.SalesmanValidateUploadError;
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
                        await R_MessageBox.Show("", " Salesman uploaded successfully!", R_eMessageBoxButtonType.OK);
                        await this.Close(true, false);
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
            var lcName = $"Salesman" + ".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }

        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }

    }
}