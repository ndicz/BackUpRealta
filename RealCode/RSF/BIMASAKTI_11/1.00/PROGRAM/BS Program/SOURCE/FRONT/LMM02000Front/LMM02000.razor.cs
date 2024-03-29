﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using LMM02000Common.DTO;
using LMM02000Common.DTO.UPLOAD_DTO_LMM02000;
using LMM02000Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace LMM02000Front
{
    public partial class LMM02000 : R_Page
    {
        private LMM02000ViewModel _viewModel = new();
        private R_Conductor _conductorRef;
        private R_ConductorGrid _conductorGrid;
        private R_AddButton R_AddBtn;
        private R_Button R_ActiveInActiveBtn;
        private R_Grid<LMM02000DTO> _gridRef;
        private R_TextBox _salesmanIdRef;
        private R_TextBox _salesmanNameRef;
        private bool enableleGrid;
        [Inject] private IClientHelper ClientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                //await PropertyListData(null);
                await _viewModel.GetProperty();
                await Task.Delay(250);
                await _gridRef.R_RefreshGrid(null);
                //await _gridRef.AutoFitAllColumnsAsync();
                enableleGrid = true;
                // enableProperty = true;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        [Inject] public R_PopupService PopupService { get; set; }

        //private void R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        //{
        //    eventArgs.Parameter = "LMM02001";
        //    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        //}

        //private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        //{
        //    R_Exception loException = new R_Exception();
        //    try
        //    {
        //        bool result = (bool)eventArgs.Result;

        //        if (result)
        //        {
        //            await _viewModel.ActiveInactiveProcessAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }
        //    loException.ThrowExceptionIfErrors();
        //    await _gridRef.R_RefreshGrid(null);

        //}
        public async Task ActiveInactiveSaving(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;
            LMM02000DTO loData = null;

            try
            {
                loData = (LMM02000DTO)eventArgs.Data;
                if (loData.LACTIVE == true && _conductorRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                        "LMM02001"; //Uabh Approval Code sesuai Spec masing masing
                    await loValidateViewModel
                        .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                    //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                        loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        eventArgs.Cancel = false;
                    }
                    else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                    {
                        loParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "LMM02001" //Uabh Approval Code sesuai Spec masing masing
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                        eventArgs.Cancel = !(bool)loResult.Result;
                    }

                    if (loData.CSALESMAN_ID == null || loData.CSALESMAN_NAME == null || loData.CADDRESS == null ||
                        loData.CEMAIL == null || loData.CMOBILE_PHONE1 == null)
                    {
                        await R_MessageBox.Show("Error", "You Must Fill Empty Field", R_eMessageBoxButtonType.OK);
                        eventArgs.Cancel = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                    "LMM02001"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel
                    .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                    loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _viewModel.ActiveInactiveProcessAsync(); //Ganti jadi method ActiveInactive masing masing
                    await _gridRef.R_RefreshGrid(null);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "LMM02001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }

                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _viewModel.ActiveInactiveProcessAsync();
                    await _gridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task R_Before_Open_Upload(R_BeforeOpenPopupEventArgs eventArgs)
        {
            string propertyid = _viewModel.propertyValue;
            LMM02000PropertyDTO loParam = (_viewModel.PropertyList).Find(x => x.CPROPERTY_ID == propertyid);

            eventArgs.TargetPageType = typeof(LMM02000Upload);
            var param = new LMM02000UploadDTO()
            {
                CPROPERTY_ID = loParam.CPROPERTY_ID,
                CPROPERTY_NAME = loParam.CPROPERTY_NAME,
            };
            eventArgs.Parameter = param;
        }

        private async Task R_After_Open_Upload(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task GenderList()
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetGenderList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        //private async Task SalesmanType()
        //{
        //    var loEx = new R_Exception();

        //    try
        //    {
        //        await _viewModel.GetSalesmanType();
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    R_DisplayException(loEx);
        //}

        private async Task PropertyListData(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetProperty();
                //eventArgs.ListEntityResult = _viewModel.loGridListProperty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChanged(object poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                _viewModel.propertyValue = lsProperty;
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }


            R_DisplayException(loEx);
        }

        private string loLabel;
        private bool enableSalesmanType;
        private bool enableActiveInactive;
        private bool enableSalesmanId;

        private async Task OnChangedSalesmanType(string poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                _viewModel.Data.CSALESMAN_TYPE = poParam;
                //enableSalesmanType = lsProperty == "I" ? false : true;
                if (enableSalesmanType = lsProperty == "I")
                {
                    enableSalesmanType = false;
                    _viewModel.Data.CEXT_COMPANY_NAME = null;
                }
                else
                {
                    enableSalesmanType = true;
                    _viewModel.Data.CEXT_COMPANY_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }


            R_DisplayException(loEx);
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetGridList();
                await _viewModel.GetGenderList();
                await _viewModel.GetSalesmanType();
                eventArgs.ListEntityResult = _viewModel.loGridList;
                //await _gridRef.AutoFitAllColumnsAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void R_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            enableSalesmanType = false;
            enableActiveInactive = false;
            enableSalesmanId = false;

            enableleGrid = true;
                    }

        private void R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
           
            var LMMDTO = (LMM02000DTO)eventArgs.Data;

            // LMMDTO.CMOBILE_PHONE2 = "";
            LMMDTO.CSALESMAN_TYPE = "I";
            // LMMDTO.CEXT_COMPANY_NAME = "";
            // LMMDTO.CID_NO = "";
            LMMDTO.CGENDER = "M";

            enableleGrid = false;
        }

        private void R_AfterCancel(R_BeforeCancelEventArgs eventArgs)
        {
            enableSalesmanType = false;
            enableleGrid = true;
        }

        private async Task Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM02000DTO>(eventArgs.Data);

                await _viewModel.GetEntity(loParam);
                eventArgs.Result = _viewModel.loEntity;

                if (loParam.LACTIVE)
                {
                    loLabel = "Inactive";
                    _viewModel.Data.LACTIVE = false;
                }
                else
                {
                    loLabel = "Activate";
                    _viewModel.Data.LACTIVE = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<LMM02000DTO>(eventArgs.Data);
        }

        public async Task Conductor_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM02000DTO>(eventArgs.Data);
                await _viewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    
        private void R_Beforeedit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
            
                enableleGrid = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        private void R_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // enableProperty = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        
        private async Task Conductor_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM02000DTO)eventArgs.Data;
                await _viewModel.Delete(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        [Inject] private IJSRuntime JS { get; set; }

        private async Task DownloadTemplate()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?",
                    R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _viewModel.DownloadTemplate();
                    var saveFileName = $"Salesman-{ClientHelper.CompanyId}.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Display(R_DisplayEventArgs eventArgs)
        {
            switch (eventArgs.ConductorMode)
            {
                case R_eConductorMode.Add:
                    await _salesmanIdRef.FocusAsync();
                    break;
                case R_eConductorMode.Edit:
                    await _salesmanNameRef.FocusAsync();
                    break;
            }
        }
      
    }
}