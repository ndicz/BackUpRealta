using GSM00700Common.DTO;
using GSM00700Model;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.Popup;

namespace GSM00700Front
{
    public partial class GSM00710 : R_Page
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
        private R_Grid<GSM00720YearDTO> _gridRef00720Year;
        private R_Grid<GSM00720DTO> _gridRef00720;


        #region GSM00710

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var param = (GSM00700DTO)poParameter;
                GSM00710ViewModel.CashFlowGroupCode = param.CCASH_FLOW_GROUP_CODE;
                GSM00710ViewModel.CashFlowGroupName = param.CCASH_FLOW_GROUP_NAME;
                await GSM00710ViewModel.GetCashFlowList();
                await GSM00710ViewModel.GetCashFlowTypeList();
        
               

                await _gridRef00710.R_RefreshGrid(null);
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
                await _gridRef00710.AutoFitAllColumnsAsync();
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
        private async Task Grid_R_DisplaytListCashFlowPlan(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<GSM00720DTO>(eventArgs.Data);
                    GSM00720ViewModel.CashFlowPlanCode = loParam.CCASH_FLOW_CODE;
                    GSM00720ViewModel.CashFlowPlanName = loParam.CCASH_FLOW_NAME;


                    await GSM00710ViewModel.GetCashFlowId(loParam.CCASH_FLOW_CODE, loParam.CCASH_FLOW_NAME);

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_R_ServiceGetListRecordCashFlowPlan(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //await GSM00720ViewModel.GetYearList();
                eventArgs.ListEntityResult = GSM00720ViewModel.loGridList;
                await _gridRef00720.AutoFitAllColumnsAsync();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_Display_Year(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<GSM00720YearDTO>(eventArgs.Data);
                    GSM00720ViewModel.Year = loParam.CYEAR;
                    //await GSM00720ViewModel.GetCashFlowPlanList(GSM00710ViewModel.CashFlowGroupCode);
                    await GSM00720ViewModel.GetCashFlowPlanList(GSM00710ViewModel.CashFlowGroupCode, GSM00720ViewModel.CashFlowPlanCode);
                    await GSM00720ViewModel.GetYearList();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ChangeTab(R_TabStripTab arg)
        {
            var loEx = new R_Exception();

            try
            {
                
                if (arg.Id == "tabCashFlowPlan")
                {
                    await GSM00720ViewModel.GetYearList();
                    await GSM00720ViewModel.GetCashFlowPlanList(GSM00710ViewModel.CashFlowGroupCode, GSM00720ViewModel.CashFlowPlanCode);



                    //await _gridRef00720Year.R_RefreshGrid(null);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region POPUP
        [Inject] public R_PopupService PopupService { get; set; }
        private void R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM00720LocalAmount);

            var param = new GSM00720CopyBaseLocalAmountDTO()
            {
                CCASH_FLOW_CODE = GSM00710ViewModel.CashFlowGroupCode,
                CCASH_FLOW_NAME = GSM00710ViewModel.CashFlowGroupName,
                CYEAR = GSM00720ViewModel.Year,
            };

            eventArgs.Parameter = param;
        }

        private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                await _gridRef00720.R_RefreshGrid(null);
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
