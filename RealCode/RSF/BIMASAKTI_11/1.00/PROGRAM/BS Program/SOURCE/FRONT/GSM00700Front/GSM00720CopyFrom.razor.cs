using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common.DTO;
using GSM00700Model;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APFRONT;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace GSM00700Front
{
    public partial class GSM00720CopyFrom : R_Page
    {
        private GSM00720ViewModel _GSM00720ViewModel = new();
        private GSM00700ViewModel _GSM00700ViewModel = new();

        private R_Conductor _conductorRef;
        public R_Lookup CashFlow { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _GSM00720ViewModel.loCopyFromEntity = (GSM00720CopyFromYearDTO)poParameter;
                _GSM00720ViewModel.Year = _GSM00720ViewModel.loCopyFromEntity.CTO_YEAR;
                _GSM00720ViewModel.CashFlowPlanName = _GSM00720ViewModel.loCopyFromEntity.CashFlowName;
                _GSM00720ViewModel.CashFlowPlanCode = _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_CODE;
                await _GSM00720ViewModel.GetYearForCopyFrom();


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private Task R_BeforeOpenLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //eventArgs.Parameter = new GSL01500ParameterGroupDTO()

                //{

                //    CCOMPANY_ID = _GSM00720ViewModel.loCopyFromEntity.CCOMPANY_ID,
                //    CUSER_ID = _GSM00720ViewModel.loCopyFromEntity.CUSER_ID,
                //    //CCASH_FLOW_GROUP_CODE = _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_CODE,

                //};
                //eventArgs.TargetPageType = typeof(GSL01500);



                eventArgs.Parameter = new APL00300ParameterDTO()

                {

                    CCOMPANY_ID = "RCD",
                    CPROPERTY_ID = "ASHMD",
                    CCATEGORY_ID = "",
                    CTAXABLE_TYPE = "1",
                    CACTIVE_TYPE = "1",
                    CLANGUAGE_ID = "EN",
                    CTAX_DATE = "",
                    //CCASH_FLOW_GROUP_CODE = _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_CODE,

                };
                eventArgs.TargetPageType = typeof(APL00300);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return Task.CompletedTask;
        }

        private void R_AfterOpenLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSL01500DTO)eventArgs.Result;
                if (loData == null)
                    return;

                _GSM00720ViewModel.loCopyFromEntity.CFROMGOUP = loData.CCASH_FLOW_GROUP_CODE;
                _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_CODE = loData.CCASH_FLOW_CODE;
                _GSM00720ViewModel.loCopyFromEntity.CashFlowName = loData.CCASH_FLOW_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            //return Task.CompletedTask;
        }


        public async Task OnChanged()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _GSM00720ViewModel.RadioButtonCopyFrom;
                if (_GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_FLAG == "01")
                {
                    CashFlow.Enabled = true;


                }

                else
                {
                    CashFlow.Enabled = false;
                    //_GSM00720ViewModel.CashFlowPlanCode = _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_CODE;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickProcessAsync()
        {
            var loEx = new R_Exception();
            var loData = _GSM00720ViewModel.loCopyFromEntity;
            try
            {

                _GSM00720ViewModel.CFromCashFlowFlag = _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_FLAG;
                _GSM00720ViewModel.CFromYear = _GSM00720ViewModel.loCopyFromEntity.CFROM_YEAR;


                await _GSM00720ViewModel.CopyFrom();
                //await this.Close(true, loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();



            await this.Close(true, loData);
        }


        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }

}
