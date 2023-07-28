using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using R_BlazorFrontEnd.Controls.Popup;
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




    

        [Inject] private IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef5500.R_RefreshGrid(null);
             ;
              
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

        public async Task R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05500DTO)eventArgs.Data;
                var Condition1 = loParam.CCURRENCY_CODE == null;
                var Condition2 = loParam.CCURRENCY_NAME == null;
                var Condition3 = loParam.CCURRENCY_CODE == null && loParam.CCURRENCY_NAME == null;
                //check loparam currency code = null or empty

                if (Condition1)
                {
                    await R_MessageBox.Show("Error", "You Must Fill Empty Field", R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }
                if (Condition2)
                {
                    await R_MessageBox.Show("Error", "You Must Fill Empty Field", R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }
                if (Condition3)
                {
                    await R_MessageBox.Show("Error", "You Must Fill Empty Field", R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }
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

        private async Task BeforeOpenTabDetailRateType(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM05510);
            //eventArgs.Parameter = R_FrontUtility.ConvertObjectToObject<GSM05500DTO>(GSM05500ViewModel.loEntity);
        }

        private async Task BeforeOpenTabDetailCurrencyRate(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM05520);
            //eventArgs.Parameter = R_FrontUtility.ConvertObjectToObject<GSM05500DTO>(GSM05500ViewModel.loEntity);
        }


        #endregion


        #region GSM05100

      


        #endregion


    }
}
