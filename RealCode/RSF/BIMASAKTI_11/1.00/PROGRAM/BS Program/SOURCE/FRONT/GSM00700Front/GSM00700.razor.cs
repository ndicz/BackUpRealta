using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common.DTO;
using GSM00700Common.DTO.Report_DTO_GSM00700;
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

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await GSM00700ViewModel.GetCashFlowGroupTypeList();
                await _gridRef00700.R_RefreshGrid(null);
                //await _gridRef00700.AutoFitAllColumnsAsync();

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

            //R_DisplayException(loEx);
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


                await GSM00700ViewModel.GetCashFlowGroupId(loParam.CCASH_FLOW_GROUP_CODE, loParam.CCASH_FLOW_GROUP_NAME);

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
                // await _gridRef00700.R_RefreshGrid(null);
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
                        loException.Add("001", @_localizer["CASH_GRP_CD_EMPT"]);

                if (string.IsNullOrEmpty(loData.CCASH_FLOW_GROUP_NAME))
                    loException.Add("002", @_localizer["CASH_GRP_NM_EMPT"]);


            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            eventArgs.Cancel = loException.HasError;

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_DisplaytListCashFlow(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<GSM00710DTO>(eventArgs.Data);
                    if (loParam != null)
                    {
                        GSM00710ViewModel.CashFlowGroupCode = loParam.CCASH_FLOW_GROUP_CODE;
                        GSM00710ViewModel.CashFlowGroupName = loParam.CCASH_FLOW_GROUP_NAME;
                        //GSM00710ViewModel.csquence = loParam.CSEQUENCE.ToString();

                        await GSM00700ViewModel.GetCashFlowGroupId(loParam.CCASH_FLOW_GROUP_CODE, loParam.CCASH_FLOW_GROUP_NAME);


                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BeforeOpenTabCashFlow(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM00710);
            eventArgs.Parameter = R_FrontUtility.ConvertObjectToObject<GSM00700DTO>(GSM00700ViewModel.loEntity);
        }

        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            var GridType = (GSM00700DTO)eventArgs.Data;

            try
            {
                //GSM00700ViewModel.loEntity.CCASH_FLOW_GROUP_TYPE = "I";
                await GSM00700ViewModel.GetCashFlowGroupTypeList();
                await _gridRef00700.R_RefreshGrid(null);
                //await _gridRef00700.AutoFitAllColumnsAsync();


                GridType.CCASH_FLOW_GROUP_TYPE = GSM00700ViewModel.CashFlowTyp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var GridType = (GSM00700DTO) eventArgs.Data;

            GridType.CCASH_FLOW_GROUP_TYPE = GSM00700ViewModel.loGroupType.FirstOrDefault().ToString();


        }
        private void R_BeforeOpenPrint(R_BeforeOpenPopupEventArgs eventArgs)
        {

            eventArgs.TargetPageType = typeof(GSM00700Print);
            var param = new GSM00700PrintCashFlowDTO()
            {
                CCASH_FLOW_GROUP_CODE = GSM00700ViewModel.CashFlowTyp,
                CCASH_FLOW_GROUP_NAME = GSM00700ViewModel.CashFlowTyp,
                CCASH_FLOW_GROUP_TYPE = GSM00700ViewModel.CashFlowTyp,

            };
            eventArgs.Parameter = param;

        }

        private async Task R_AfterOpenPrint(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                await _gridRef00700.R_RefreshGrid(null);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();


        }
    }
}

