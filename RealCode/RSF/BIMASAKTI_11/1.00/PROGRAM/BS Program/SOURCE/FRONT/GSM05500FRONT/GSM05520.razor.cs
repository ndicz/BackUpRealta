
using GSM05500Common.DTO;
using GSM05500Model;
using GSM05500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Controls.MessageBox;
using System.Diagnostics.Tracing;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls.Popup;
using BlazorClientHelper;
using GFF00900COMMON.DTOs;

namespace GSM05500Front
{
    public partial class GSM05520 : R_Page
    {
        private GSM05520ViewModel GSM05520ViewModel = new();
        private R_ConductorGrid _conGridGSM05520Ref;
        private R_Grid<GSM05520DTO> _gridRef5520;
        private R_Conductor _conductorRef;
        private bool enableRateTime;

        [Inject] private IClientHelper _clientHelper { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await GSM05520ViewModel.GetLcCurrency();
                await GSM05520ViewModel.GetRateListP();
                enableRateTime = true;
                await RateTypeGet(null);

                await _gridRef5520.R_RefreshGrid(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region GSM05200
        // private void R_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        // {
        //     eventArgs.Parameter = "GSM5502";
        //     eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        //
        // }


        // private async Task R_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        // {
        //     R_Exception loException = new R_Exception();
        //     try
        //     {
        //         if (eventArgs.Success == false)
        //         {
        //             return;
        //         }
        //         bool result = (bool)eventArgs.Result;
        //         if (result == true)
        //
        //         await _gridRef5520.R_RefreshGrid(null);
        //     }
        //     catch (Exception ex)
        //     {
        //         loException.Add(ex);
        //     }
        //     loException.ThrowExceptionIfErrors();
        //     await _gridRef5520.R_RefreshGrid(null);
        //
        // }
        [Inject] public R_PopupService PopupService { get; set; }


        public async Task Conductor_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;
            enableRateTime = false;
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM05502"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    
                    eventArgs.Cancel = false;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    loParam = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM05502" //Uabh Approval Code sesuai Spec masing masing
                    };
                    loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                    eventArgs.Cancel = !(bool)loResult.Result;
                }

                //loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), "GSM05502");
                //eventArgs.Cancel = !(bool)loResult.Result;
                enableRateTime = true;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }



        public async Task Conductor_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;
            enableRateTime = false;
            try
            {
                if (GSM05520ViewModel.CrateTime < DateTime.Today)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM05501"; //Uabh Approval Code sesuai Spec masing masing
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                    //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        eventArgs.Cancel = false;
                    }
                    else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                    {
                        loParam = new GFF00900ParameterDTO()
                        {
                            
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "GSM05501" //Uabh Approval Code sesuai Spec masing masing
                            
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                        eventArgs.Cancel = !(bool)loResult.Result;
                    }

                }

                enableRateTime = true;
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
            string lsCrated = (string)poParam;
            try
            {
                GSM05520ViewModel.CreateCode = lsCrated;

                //await GSM05520ViewModel.GetLcCurrency();
                await _gridRef5520.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnChangedDate(DateTime poParam)
        {
            var loEx = new R_Exception();
            try
            {
                GSM05520ViewModel.CrateTime = poParam;

                string formattedDate = poParam.ToString("yyyyMMdd");
                GSM05520ViewModel.CrateDate = formattedDate;

                if (_conGridGSM05520Ref.R_ConductorMode == R_eConductorMode.Normal)
                {
                    await _gridRef5520.R_RefreshGrid(null);
                }
                else
                {
                    var loData = (GSM05520DTO)_conGridGSM05520Ref.R_GetCurrentData();
                    loData.CRATE_DATE = formattedDate;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task RateTypeGet(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await GSM05520ViewModel.GetRateListP();
                //eventArgs.ListEntityResult = _viewModel.loGridListProperty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region Lookup Button

        private R_AddButton R_AddBtn;
        private R_Button R_ActiveInActiveBtn;
        private R_Lookup R_LookupBtn;

        private void Before_Open_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var param = new GSL00300ParameterDTO
            {
                CCOMPANY_ID = "",
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00300);
        }

        private void After_Open_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GSL00300DTO)eventArgs.Result;
            if (loTempResult == null)
                return;
            var loGetData = (GSM05520DTO)eventArgs.ColumnData;
            loGetData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            loGetData.CCURRENCY_NAME = loTempResult.CCURRENCY_NAME;

        }

        // private void R_SetAdd(R_SetEventArgs eventArgs)
        // {
        //     if (R_LookupBtn != null)
        //         R_LookupBtn.Enabled = eventArgs.Enable;
        // }
        //
        // private void R_SetEdit(R_SetEventArgs eventArgs)
        // {
        //     if (R_LookupBtn != null)
        //         R_LookupBtn.Enabled = eventArgs.Enable;
        // }

        #endregion

        private async Task Grid_R_ServiceGetListRecordRate(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                await GSM05520ViewModel.GetRateList();
                eventArgs.ListEntityResult = GSM05520ViewModel.loGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        // public async Task Conductor_AfterDelete()
        // {
        //     await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        // }
        private async Task Grid_ServiceGetRecordRate(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM05520DTO)eventArgs.Data;
                await GSM05520ViewModel.GetRateId(loParam.CCURRENCY_CODE, loParam.CRATETYPE_CODE, loParam.CRATE_DATE);
                eventArgs.Result = GSM05520ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveRate(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var loParam = (GSM05520DTO)eventArgs.Data;
                loParam.CRATETYPE_CODE = GSM05520ViewModel.CreateCode;
                //loParam.CCURRENCY_CODE = GSM05520ViewModel.CurrencyCode;
                loParam.CRATE_DATE = GSM05520ViewModel.CrateTime.ToString("yyyyMMdd");
                await GSM05520ViewModel.SaveRateType(loParam, eventArgs.ConductorMode);

                eventArgs.Result = GSM05520ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_ServiceDeleteRate(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05520DTO)eventArgs.Data;
                await GSM05520ViewModel.DeleteRate(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //private void R_Before_Open_Popup_ActivateInactive(R_BeforeEditEventArgs eventArgs)
        //{
        //    eventArgs.Parameter = "LMM02001";
        //    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        //}

        [Inject] public R_PopupService PopupServiceAdd { get; set; }

        public async Task Conductor_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            enableRateTime = false;
            var loEntity = (GSM05520DTO)eventArgs.Data;
            loEntity.CRATE_DATE = GSM05520ViewModel.CrateDate;
            //GSM05520ViewModel.RateTypeCode = "";
            //GSM05520ViewModel.CrateTime = DateTime.Now;


        }


        private async Task Conductor_Display(R_AfterSaveEventArgs eventArgs)
        {
            enableRateTime = true;
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var data = (GSM05520DTO)eventArgs.Data;
                data.CRATE_DATE = GSM05520ViewModel.CrateTime.ToString("yyyyMMdd");
                GSM05520ViewModel.CrateTime = DateTime.ParseExact(data.CRATE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                await _gridRef5520.R_RefreshGrid((GSM05520DTO)eventArgs.Data);
            }
        }

        public void Conductor_Saving(R_SavingEventArgs eventArgs)
        {
            ((GSM05520DTO)eventArgs.Data).CRATE_DATE = GSM05520ViewModel.CrateTime.ToString("yyyyMMdd");
        }
        
        public void Conductor_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            enableRateTime = true;
        }


        public async Task Grid_AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        #endregion
        //private void R_Before_Open_Form(R_BeforeOpenDetailEventArgs eventArgs)
        //{
        //    eventArgs.TargetPageType = typeof(GSM05510);
        //    eventArgs.Parameter = "ea";
        //}


        public async Task R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var loParam = (GSM05520DTO)eventArgs.Data;
            var emptyFieldCondition = loParam.NLBASE_RATE_AMOUNT == 0 || loParam.NLCURRENCY_RATE_AMOUNT == 0 ||
                                      loParam.NBBASE_RATE_AMOUNT == 0 || loParam.NBCURRENCY_RATE_AMOUNT == 0 ||
                                      loParam.CCURRENCY_CODE == null;


            if (emptyFieldCondition)
            {
                await R_MessageBox.Show("Error", "You Must Fill Empty Field", R_eMessageBoxButtonType.OK);
                eventArgs.Cancel = true;
                return;
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}

