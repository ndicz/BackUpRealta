using GSM00700Common.DTO;
using GSM00700Model.Model;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using GSM00700Common;

namespace GSM00700Model
{
    public class GSM00720ViewModel : R_ViewModel<GSM00720DTO>
    {
        private GSM00720Model _GSM00720Model = new GSM00720Model();
        public ObservableCollection<GSM00720DTO> loGridList = new ObservableCollection<GSM00720DTO>();
        public ObservableCollection<GSM00720YearDTO> loYearList = new ObservableCollection<GSM00720YearDTO>();
        public GSM00720DTO loEntity = new GSM00720DTO();
        public ObservableCollection<GSM00720CopyFromYearDTO> loCopyFromList = new ObservableCollection<GSM00720CopyFromYearDTO>();
        public GSM00720CopyFromYearDTO loCopyFromEntity = new GSM00720CopyFromYearDTO();
        public ObservableCollection<GSM00720CopyBaseLocalAmountDTO> loCopyBaseAmountList = new ObservableCollection<GSM00720CopyBaseLocalAmountDTO>();
        public GSM00720CopyBaseLocalAmountDTO loCopyBaseAmountEntity = new GSM00720CopyBaseLocalAmountDTO();

        public List<GSM00720CopyFromYearDTO> RadioButtonCopyFrom { get; set; } = new List<GSM00720CopyFromYearDTO>()
        {
            new GSM00720CopyFromYearDTO() { Code = "01", Desc = "Same"},
            new GSM00720CopyFromYearDTO() {  Code = "00", Desc = "Other" }
        };


        public string CashFlowPlanCode = ""; // for filter
        public string CashFlowPlanName = ""; // for filter  
        public string Year = ""; // for year
        //Copy From Year
        public string CFromCashFlowFlag = ""; // for coptfrom
        public string CFromCashFlowCode = ""; // for filter
        public string CFromCashFlowName = ""; // for filter
        public string CFromYear = ""; // for year
        public string CToCashFlowCode = ""; // for filter
        public string CToYear = ""; // for year
        //Local Amount 
        public string CurrencyCode = "";
        public string CurrencyRate = "";
        public string PeriodFrom = "";
        public string PeriodTo = "";


        public async Task GetCashFlowPlanList(string CashFlowGroupCode, string CashFlowPlanCode)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE, CashFlowGroupCode);

                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_CODE, CashFlowPlanCode);

                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CYEAR, Year);
                var loReturn = await _GSM00720Model.GetCashFlowPlanAsync();
                loGridList = new ObservableCollection<GSM00720DTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetYearList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM00720Model.GetYearListAsync();
                loYearList = new ObservableCollection<GSM00720YearDTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        //testing copyfrom 
        public async Task CopyFrom()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CFROM_CASH_FOW_FLAG, CFromCashFlowFlag);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CFROM_CASH_FLOW_CODE, CFromCashFlowCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CFROM_YEAR, CFromYear);

                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CTO_CASH_FLOW_CODE, CToCashFlowCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CTO_YEAR, CToYear);
                var loReturn = await _GSM00720Model.GetCopyFromYearListAsync();

                loCopyFromList = new ObservableCollection<GSM00720CopyFromYearDTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //LocalBaseAmount 

        public async Task LocalAmount(string CashFlowGroupCode, string CashFlowPlanCode)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_GROUP, CashFlowGroupCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_CODE, CashFlowPlanCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CYEAR, Year);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCURRENCY_CODE, CurrencyCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCURRENCY_RATE, CurrencyRate);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.INO_PERIOD_FROM, PeriodFrom);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.INO_PERIOD_TO, PeriodTo);
                var loReturn = await _GSM00720Model.GetCopyLocalAmountListAsync();

                loCopyBaseAmountList = new ObservableCollection<GSM00720CopyBaseLocalAmountDTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task BaseAmount(string CashFlowGroupCode, string CashFlowPlanCode)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_GROUP, CashFlowGroupCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_CODE, CashFlowPlanCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CYEAR, Year);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCURRENCY_CODE, CurrencyCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCURRENCY_RATE, CurrencyRate);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.INO_PERIOD_FROM, PeriodFrom);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.INO_PERIOD_TO, PeriodTo);
                var loReturn = await _GSM00720Model.GetCopyBaseAmountListAsync();

                loCopyBaseAmountList = new ObservableCollection<GSM00720CopyBaseLocalAmountDTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}

