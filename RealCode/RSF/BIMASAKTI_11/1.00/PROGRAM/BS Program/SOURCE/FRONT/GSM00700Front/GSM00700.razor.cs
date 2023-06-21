﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common.DTO;
using GSM00700Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM00700Front
{
    public partial class GSM00700 : R_Page
    {
        private GSM00700ViewModel GSM00700ViewModel = new();
        private R_ConductorGrid _conGridGSM00700Ref;
        private R_Grid<GSM00700DTO> _gridRef00700;

        private GSM00710ViewModel GSM00710ViewModel = new();
        private R_ConductorGrid _conGridGSM00710Ref;
        private R_Grid<GSM00710DTO> _gridRef00710;
        private R_Conductor R_conduct;

        private GSM00720ViewModel GSM00720ViewModel = new();
        private R_ConductorGrid _conGridGSM00720Ref;
        private R_Grid<GSM00720DTO> _gridRef00720;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await GSM00700ViewModel.GetCashFlowGroupTypeList();
                await GSM00710ViewModel.GetCashFlowTypeList();

                await _gridRef00700.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
     
        private async Task Grid_R_ServiceGetListCashFlowGroup(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await GSM00700ViewModel.GetCashFlowGroupList();
               
                GSM00710ViewModel.csquence = GSM00710ViewModel.loGridList.Count.ToString();
                eventArgs.ListEntityResult = GSM00700ViewModel.loGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_ServiceGetRecordCashFlowGroup(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM00700DTO)eventArgs.Data;
                GSM00710ViewModel.CashFlowGroupCode = loParam.CCASH_FLOW_GROUP_CODE;
                GSM00710ViewModel.CashFlowGroupName = loParam.CCASH_FLOW_GROUP_NAME;
                

                await GSM00700ViewModel.GetCashFlowGroupId(loParam.CCASH_FLOW_GROUP_CODE,loParam.CCASH_FLOW_GROUP_NAME);

                eventArgs.Result = GSM00700ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_ServiceSaveRecordCashFlowGroup(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM00700DTO)eventArgs.Data;
                await GSM00700ViewModel.SaveCashFlowGroup(loParam, eventArgs.ConductorMode);

                eventArgs.Result = GSM00700ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_DeleteRecordCashFlowGroup(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM00700DTO)eventArgs.Data;
                await GSM00700ViewModel.DeleteCashFlowGroup(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_ValidationCashFlowGroup(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (GSM00700DTO)eventArgs.Data;

                if (string.IsNullOrEmpty(loData.CCASH_FLOW_GROUP_CODE))
                    loException.Add("001", "Cash Flow Group Code cannot be Empty.");

                if (string.IsNullOrEmpty(loData.CCASH_FLOW_GROUP_NAME))
                    loException.Add("002", "Cash Flow Group Name cannot be Empty");


            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            eventArgs.Cancel = loException.HasError;

            loException.ThrowExceptionIfErrors();
        }

#region GSM00710


        private async Task ChangeTab(R_TabStripTab arg)
        {
            var loEx = new R_Exception();

            try
            {
                if (arg.Id == "tabCashFlow")
                {
                    await GSM00710ViewModel.GetCashFlowList();
                    await _gridRef00710.R_RefreshGrid(null);
                }
                //else if (arg.Id == "tabCurrencyRate")
                //{

                //    await GSM05520ViewModel.GetRateListP();
                //    await GSM05520ViewModel.GetRateList();
                //    await GSM05520ViewModel.GetLcCurrency();
                //    await _gridRef00700.R_RefreshGrid(null);
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
       
        private async Task Grid_R_ServiceGetListCashFlow(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
              
                await GSM00710ViewModel.GetCashFlowList();
                GSM00710ViewModel.csquence = GSM00710ViewModel.csquence.ToString();
                eventArgs.ListEntityResult = GSM00710ViewModel.loGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_DisplaytListCashFlow(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<GSM00710DTO>(eventArgs.Data);
                    GSM00710ViewModel.CashFlowGroupCode = loParam.CCASH_FLOW_GROUP_CODE;
                    GSM00710ViewModel.CashFlowGroupName = loParam.CCASH_FLOW_GROUP_NAME;
                    //GSM00710ViewModel.csquence = loParam.CSEQUENCE.ToString();

                    await GSM00700ViewModel.GetCashFlowGroupId(loParam.CCASH_FLOW_GROUP_CODE,loParam.CCASH_FLOW_GROUP_NAME);
                
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_ServiceGetRecordCashFlow(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM00710DTO)eventArgs.Data;
                //GSM00710ViewModel.CashFlowGroupCode = loParam.CCASH_FLOW_GROUP_CODE;
                //GSM00710ViewModel.CashFlowGroupName = loParam.CCASH_FLOW_NAME;
                GSM00710ViewModel.csquence = loParam.CSEQUENCE.ToString();
                await GSM00710ViewModel.GetCashFlowId(loParam.CCASH_FLOW_CODE, loParam.CCASH_FLOW_GROUP_CODE); // belum ditest karena belum ada data
                eventArgs.Result = GSM00710ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_ServiceSaveRecordCashFlow(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM00710DTO)eventArgs.Data;
                //GSM00710ViewModel.csquence = loParam.CSEQUENCE.ToString();
                GSM00700ViewModel.GetCashFlowGroupId(loParam.CCASH_FLOW_GROUP_CODE, loParam.CCASH_FLOW_NAME);
                await GSM00710ViewModel.SaveCashFlow(loParam, eventArgs.ConductorMode, loParam.CCASH_FLOW_GROUP_CODE);

                eventArgs.Result = GSM00710ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_DeleteRecordCashFlow(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM00710DTO)eventArgs.Data;
                await GSM00710ViewModel.DeleteCashFlow(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

#endregion
        #region GSM00720

      

        #endregion

    }
}
