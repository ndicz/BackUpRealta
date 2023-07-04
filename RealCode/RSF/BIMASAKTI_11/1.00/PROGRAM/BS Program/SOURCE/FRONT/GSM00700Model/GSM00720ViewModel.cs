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
        public string CashFlowPlanCode = ""; // for filter
        public string CashFlowPlanName = ""; // for filter  


        public async Task GetCashFlowPlanList(string CashFlowGroupCode) 
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE, CashFlowGroupCode);

                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_CODE, CashFlowPlanCode);

                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CYEAR, CashFlowPlanCode);
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
    }
}
