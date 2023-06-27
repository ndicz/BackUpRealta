
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

namespace GSM05500Front
{
    public partial class GSM05520 : R_Page
    {
        private GSM05520ViewModel GSM05520ViewModel = new();
        private R_ConductorGrid _conGridGSM05520Ref;
        private R_Grid<GSM05520DTO> _gridRef;
        private R_Conductor R_conduct;
    

  

        private string lcPropertyId = "Admin";
        private string lcCompany = "RCD";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef.R_RefreshGrid(null);
                await GSM05520ViewModel.GetRateListP();
                await GSM05520ViewModel.GetLcCurrency();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

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
                loParam.CRATETYPE_CODE = GSM05520ViewModel.Data.CRATETYPE_CODE;
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

        public void Conductor_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEntity = (GSM05520DTO)eventArgs.Data;

            GSM05520ViewModel.CrateTime = DateTime.Now;
        }

        private async Task DropDwonRate (R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await GSM05520ViewModel.GetRateListP();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Conductor_Display(R_AfterSaveEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var data =(GSM05520DTO) eventArgs.Data;
                GSM05520ViewModel.CrateTime =
                    DateTime.ParseExact(data.CRATE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                await _gridRef.R_RefreshGrid((GSM05520DTO)eventArgs.Data);
            }
        }

        public async Task Conductor_Saving(R_SavingEventArgs eventArgs)
        {
            ((GSM05520DTO)eventArgs.Data).CRATE_DATE = GSM05520ViewModel.CrateTime.ToString("yyyyMMdd");
        }

        public async Task Grid_AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }


        public async Task Popup_BeforeEdit(R_BeforeOpenPopupEventArgs eventArgs)
        {
     
        }

        //public asy
    }
}
