using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GSM00700Common;
using GSM00700Common.DTO;
using GSM00700Common.DTO.Report_DTO_GSM00700;
using GSM00700Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace GSM00700Front
{
    public partial class GSM00700Print : R_Page
    {
        private GSM00720ViewModel _GSM00720ViewModel = new();
        private GSM00700ViewModel _GSM00700ViewModel = new();
        private R_Conductor _conductorRef;
        [Inject] private R_IReport _reportService { get; set; }
        public R_Lookup CashFlowGroup { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                //_GSM00720ViewModel.loCopyFromEntity = (GSM00720CopyFromYearDTO)poParameter;
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

        private Task R_BeforeOpenLookUpFrom(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL01600ParameterDTO()

                {

                    CCOMPANY_ID = _GSM00720ViewModel.loCopyFromEntity.CCOMPANY_ID,
                    CUSER_ID = _GSM00720ViewModel.loCopyFromEntity.CUSER_ID,
                    //CCASH_FLOW_GROUP_CODE = _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_CODE,

                };
                eventArgs.TargetPageType = typeof(GSL01600);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return Task.CompletedTask;
        }

        private Task R_AfterOpenLookUpFrom(R_AfterOpenLookupEventArgs eventArgs)
        {
            if (eventArgs.Result == null)
            {
                return Task.CompletedTask;
            }


            var loData = (GSL01600DTO)eventArgs.Result;
            _GSM00700ViewModel.loPrint.CCASH_FLOW_GROUP_FROM = loData.CCASH_FLOW_GROUP_CODE;
            _GSM00700ViewModel.loPrint.CCASH_FLOW_GROUP_FROM_NAME = loData.CCASH_FLOW_GROUP_NAME;


            return Task.CompletedTask;
        }


        private Task R_BeforeOpenLookUpTo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL01600ParameterDTO()

                {

                    CCOMPANY_ID = _GSM00720ViewModel.loCopyFromEntity.CCOMPANY_ID,
                    CUSER_ID = _GSM00720ViewModel.loCopyFromEntity.CUSER_ID,
                    //CCASH_FLOW_GROUP_CODE = _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_CODE,

                };
                eventArgs.TargetPageType = typeof(GSL01600);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return Task.CompletedTask;
        }

        private Task R_AfterOpenLookUpTo(R_AfterOpenLookupEventArgs eventArgs)
        {
            if (eventArgs.Result == null)
            {
                return Task.CompletedTask;
            }


            var loData = (GSL01600DTO)eventArgs.Result;
            _GSM00700ViewModel.loPrint.CCASH_FLOW_GROUP_TO = loData.CCASH_FLOW_GROUP_CODE;
            _GSM00700ViewModel.loPrint.CCASH_FLOW_GROUP_TO_NAME = loData.CCASH_FLOW_GROUP_NAME;


            return Task.CompletedTask;
        }

        public async Task OnChanged()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _GSM00720ViewModel.RadioButtonCopyFrom;
                if (_GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_FLAG == "01")
                {
                    CashFlowGroup.Enabled = true;


                }

                else
                {
                    CashFlowGroup.Enabled = false;
                    //_GSM00720ViewModel.CashFlowPlanCode = _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_CODE;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //public async Task Button_OnClickProcessAsync()
        //{
        //    var loEx = new R_Exception();
        //    var loData = _GSM00720ViewModel.loCopyFromEntity;
        //    try
        //    {

        //        _GSM00720ViewModel.CFromCashFlowFlag = _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_FLAG;
        //        _GSM00720ViewModel.CFromYear = _GSM00720ViewModel.loCopyFromEntity.CFROM_YEAR;


        //        await _GSM00700ViewModel.GetPrint();
        //        //await this.Close(true, loData);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();



        //    await this.Close(true, loData);
        //}

        [Inject] private IClientHelper _clientHelper { get; set; }

        private async Task Button_OnClickProcessAsync()
        {
            var loEx = new R_Exception();
            GSM00700PrintCashFlowParameterDTo loParam;

            try
            {
                loParam = new GSM00700PrintCashFlowParameterDTo()
                { 
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_LOGIN_ID = _clientHelper.UserId,
                    CCASH_FLOW_GROUP_FROM = _GSM00700ViewModel.loPrint.CCASH_FLOW_GROUP_FROM,
                    CCASH_FLOW_GROUP_TO = _GSM00700ViewModel.loPrint.CCASH_FLOW_GROUP_TO,
                    CYEAR_FROM = _GSM00700ViewModel.loPrint.CYEAR_FROM,
                    CYEAR_TO = _GSM00700ViewModel.loPrint.CYEAR_TO,
                    CPERIOD_FROM = _GSM00700ViewModel.loPrint.CPERIOD_FROM,
                    CPERIOD_TO = _GSM00700ViewModel.loPrint.CPERIOD_TO,
                    CSHORT_BY = _GSM00700ViewModel.loPrint.CSHORT_BY,
                    LPRINT_LOCAL = _GSM00700ViewModel.loPrint.LPRINT_LOCAL,
                    LPRINT_BASE = _GSM00700ViewModel.loPrint.LPRINT_BASE

            };

            await _reportService.GetReport(
                "R_DefaultServiceUrl",
                "GS",
                "api/GSM00700Print/CashFlowPost",
                "api/GSM00700Print/CashFlowGet",
                loParam);
        }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

    loEx.ThrowExceptionIfErrors();
        }


public async Task Button_OnClickCloseAsync()
{
    await this.Close(true, null);
}
    }

}
