using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GSM05500Common.DTO;
using GSM05500Model;
using GSM05500Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid.Columns;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05500Front
{
    public partial class GSM05500 : R_Page
    {
        private GSM05500ViewModel GSM05500ViewModel = new();
        private R_ConductorGrid _conGridGSM05500Ref;
        private R_Grid<GSM05500DTO> _gridRef5500;

        private GSM05510ViewModel GSM05510ViewModel = new();
        private R_ConductorGrid _conGridGSM05510Ref;
        private R_Grid<GSM05510DTO> _gridRef5510;


        private GSM05520ViewModel GSM05520ViewModel = new();
        private R_ConductorGrid _conGridGSM05520Ref;
        private R_Grid<GSM05520DTO> _gridRef5520;
        private R_Conductor _conductorRef;

        [Inject] private IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef5500.R_RefreshGrid(null);
                await RateTypeGet(null);
                await GSM05520ViewModel.GetLcCurrency();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region GSM05000
        public async Task Grid_AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        private async Task Grid_R_ServiceGetListRecordCurrency(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await GSM05500ViewModel.GetCurrencyList();
                eventArgs.ListEntityResult = GSM05500ViewModel.loGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecordCurrency(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM05500DTO)eventArgs.Data;
                await GSM05500ViewModel.GetCurrencyId(loParam.CCURRENCY_CODE);
                eventArgs.Result = GSM05500ViewModel.loEntity;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }



        private async Task Grid_ServiceSaveCurrency(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var loParam = (GSM05500DTO)eventArgs.Data;
                await GSM05500ViewModel.SaveCurrency(loParam, eventArgs.ConductorMode);

                eventArgs.Result = GSM05500ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_ServiceDeleteCurrency(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05500DTO)eventArgs.Data;
                await GSM05500ViewModel.DeleteCurrency(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }



        #endregion


        #region GSM05100

        private async Task ChangeTab(R_TabStripTab arg)
        {
            var loEx = new R_Exception();

            try
            {
                if (arg.Id == "tabRateType")
                {
                    await GSM05510ViewModel.GetRateTypeList();
                    await _gridRef5510.R_RefreshGrid(null);
                }
                else if (arg.Id == "tabCurrencyRate")
                {
                    await GSM05520ViewModel.GetLcCurrency();
                    //await GSM05520ViewModel.GetRateListP();
                    await GSM05520ViewModel.GetRateList();
                    //await GSM05520ViewModel.GetLcCurrency();
                    await _gridRef5520.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_AfterDelete5510()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        private async Task Grid_R_ServiceGetListRecordRateType(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await GSM05510ViewModel.GetRateTypeList();
                eventArgs.ListEntityResult = GSM05510ViewModel.loGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecordRateType(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM05510DTO)eventArgs.Data;
                await GSM05510ViewModel.GetRateTypeId(loParam.CRATETYPE_CODE);
                eventArgs.Result = GSM05510ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }



        private async Task Grid_ServiceSaveRateType(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var loParam = (GSM05510DTO)eventArgs.Data;
                await GSM05510ViewModel.SaveRateType(loParam, eventArgs.ConductorMode);

                eventArgs.Result = GSM05510ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_ServiceDeleteRateType(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05510DTO)eventArgs.Data;
                await GSM05510ViewModel.DeleteRateType(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region GSM05200

        private async Task OnChanged(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                GSM05520ViewModel.RateTypeCode = poParam.ToString();
                await _gridRef5520.R_RefreshGrid(null);
                await GSM05520ViewModel.GetLcCurrency();


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnChangedDate(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                DateTime dateValue = DateTime.Parse(poParam.ToString());
                string formattedDate = dateValue.ToString("yyyyMMdd");
                GSM05520ViewModel.CrateDate = formattedDate;

                await _gridRef5520.R_RefreshGrid(null);
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
           
        }

        private void R_SetAdd(R_SetEventArgs eventArgs)
        {
            if (R_LookupBtn != null)
                R_LookupBtn.Enabled = eventArgs.Enable;
        }

        private void R_SetEdit(R_SetEventArgs eventArgs)
        {
            if (R_LookupBtn != null)
                R_LookupBtn.Enabled = eventArgs.Enable;
        }

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
        public async Task Conductor_AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
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
                loParam.CRATETYPE_CODE = GSM05520ViewModel.RateTypeCode;
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


        public void Conductor_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEntity = (GSM05520DTO)eventArgs.Data;

            GSM05520ViewModel.CrateTime = DateTime.Now;
        }
        private async Task Conductor_Display(R_AfterSaveEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var data = (GSM05520DTO)eventArgs.Data;
                GSM05520ViewModel.CrateTime =
                    DateTime.ParseExact(data.CRATE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                await _gridRef5520.R_RefreshGrid((GSM05520DTO)eventArgs.Data);
            }
        }

        public async Task Conductor_Saving(R_SavingEventArgs eventArgs)
        {
            ((GSM05520DTO)eventArgs.Data).CRATE_DATE = GSM05520ViewModel.CrateTime.ToString("yyyyMMdd");
        }

        public async Task Grid_AfterDelete5520()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        #endregion
        //private void R_Before_Open_Form(R_BeforeOpenDetailEventArgs eventArgs)
        //{
        //    eventArgs.TargetPageType = typeof(GSM05510);
        //    eventArgs.Parameter = "ea";
        //}

    }
}
