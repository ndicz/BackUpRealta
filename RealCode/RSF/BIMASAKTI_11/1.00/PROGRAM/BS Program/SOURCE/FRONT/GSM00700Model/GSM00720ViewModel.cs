using GSM00700Common.DTO;
using GSM00700Model.Model;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using GSM00700Common;
using R_BlazorFrontEnd.Enums;
using R_CommonFrontBackAPI;

namespace GSM00700Model
{
    public class GSM00720ViewModel : R_ViewModel<GSM00720YearDTO>
    {
        private GSM00720Model _GSM00720Model = new GSM00720Model();
        public GSM00710Model _GSM00710Model = new GSM00710Model();
        public Model.GSM00700Model _GSM00700Model = new Model.GSM00700Model();
        public ObservableCollection<GSM00720DTO> loGridList = new ObservableCollection<GSM00720DTO>();
        public ObservableCollection<GSM00720YearDTO> loYearList = new ObservableCollection<GSM00720YearDTO>();
        public ObservableCollection<GSM00720CopyFromYearDTO> loCopyFromList = new ObservableCollection<GSM00720CopyFromYearDTO>();
        public ObservableCollection<GSM00720CopyBaseLocalAmountDTO> loCopyBaseAmountList = new ObservableCollection<GSM00720CopyBaseLocalAmountDTO>();
        public ObservableCollection<GSM00720CurrencyDTO> loCurrencyList = new ObservableCollection<GSM00720CurrencyDTO>();
        public ObservableCollection<GSM00720InitialProsesDTO> loInitialProsesList = new ObservableCollection<GSM00720InitialProsesDTO>();


        public GSM00720InitialProsesListDTO LoInitialProses = new GSM00720InitialProsesListDTO();
        public GSM00720CurrencyDTO loCurrency = new GSM00720CurrencyDTO();
        public GSM00720DTO loEntity = new GSM00720DTO();
        public GSM00720CopyFromYearDTO loCopyFromEntity = new GSM00720CopyFromYearDTO();
        public GSM00720CopyBaseLocalAmountDTO loCopyBaseAmountEntity = new GSM00720CopyBaseLocalAmountDTO();


        public List<GSM00720YearDTO> YearComboBox { get; set; } = new List<GSM00720YearDTO>();
        public List<GSM00720DTO> loGridListCashFlowPlan { get; set; } = new List<GSM00720DTO>();

        public List<GSM00720CopyFromYearDTO> RadioButtonCopyFrom { get; set; } = new List<GSM00720CopyFromYearDTO>()
        {
            new GSM00720CopyFromYearDTO() { Code = "00", Desc = "Same"},
            new GSM00720CopyFromYearDTO() {  Code = "01", Desc = "Other" }
        };

        public List<GSM00720CopyBaseLocalAmountDTO> YearDropDown { get; set; } = new List<GSM00720CopyBaseLocalAmountDTO>()
        {
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 01 , Desc = "01"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 02 , Desc = "02"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 03 , Desc = "03"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 04 , Desc = "04"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 05 , Desc = "05"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 06 , Desc = "06"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 07 , Desc = "07"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 08 , Desc = "08"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 09 , Desc = "09"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 10 , Desc = "10"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 11 , Desc = "11"},
            new GSM00720CopyBaseLocalAmountDTO() {  Code = 12 , Desc = "12"}
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
        public Int32 PeriodFrom;
        public Int32 PeriodTo;
        //currency

        public string CashFlowGroupCode = ""; // for filter


        public async Task InitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM00720Model.GetInitialProsesAsync();
                loInitialProsesList = new ObservableCollection<GSM00720InitialProsesDTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetCashFlowPlanList(string CashFlowGroupCode, string CashFlowPlanCode)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE, CashFlowGroupCode);

                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_CODE, CashFlowPlanCode);

                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CYEAR, Year);
                var loReturn = await _GSM00720Model.GetCashFlowPlanStreamAsync();
                // Menggunakan OrderByDescending dengan int.Parse()
                loGridListCashFlowPlan = loReturn.Data
                    .OrderBy(x => int.Parse(x.CPERIOD_NO))
                    .ToList();



                loGridList = new ObservableCollection<GSM00720DTO>(loGridListCashFlowPlan);



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
                var loReturn = await _GSM00720Model.GetYearStreamAsync();
                YearComboBox = loReturn.Data.OrderByDescending(x => x.CYEAR).ToList();
                loYearList = new ObservableCollection<GSM00720YearDTO>(YearComboBox);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetYearForCopyFrom()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GSM00720Model.GetYearStreamAsync();
                // loYearList = new ObservableCollection<GSM00720YearDTO>(YearComboBox);
                //
                // YearComboBox = loYearList[0].CYEAR;
                YearComboBox = loResult.Data.OrderByDescending(x => x.CYEAR).ToList(); // Urutkan dan simpan ke dalam list
                loYearList = new ObservableCollection<GSM00720YearDTO>(YearComboBox);
                CFromYear = YearComboBox[0].CYEAR;
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
                var poParam = new GSM00700ParameterDTO();
                poParam.CFROM_CASH_FOW_FLAG = loCopyFromEntity.CFROM_CASH_FLOW_FLAG;
                poParam.CFROM_CASH_FLOW_CODE = loCopyFromEntity.CFROM_CASH_FLOW_CODE;
                poParam.CFROM_YEAR = CFromYear;
                poParam.CTO_CASH_FLOW_CODE = CashFlowPlanCode;
                poParam.CTO_YEAR = Year;
                var loReturn = await _GSM00720Model.GetCopyFromYearListAsync(poParam);

                loCopyFromList = new ObservableCollection<GSM00720CopyFromYearDTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //LocalBaseAmount 

        public async Task LocalAmount()
        {
            var loEx = new R_Exception();
            try
            {
                var poParam = new GSM00700ParameterDTO();
                poParam.CCASH_FLOW_GROUP = CashFlowGroupCode;
                poParam.CCASH_FLOW_CODE = CashFlowPlanCode;
                poParam.CYEAR = Year;
                poParam.CCURRENCY_CODE = CurrencyCode;
                poParam.CCURRENCY_RATE = CurrencyRate;
                poParam.INO_PERIOD_FROM = loCopyBaseAmountEntity.INO_PERIOD_FROM;
                poParam.INO_PERIOD_TO = loCopyBaseAmountEntity.INO_PERIOD_TO;
                var loReturn = await _GSM00720Model.GetCopyLocalAmountListAsync(poParam);

                loCopyBaseAmountList = new ObservableCollection<GSM00720CopyBaseLocalAmountDTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task BaseAmount()
        {
            var loEx = new R_Exception();
            try
            {
                var poParam = new GSM00700ParameterDTO();
                poParam.CCASH_FLOW_GROUP = CashFlowGroupCode;
                poParam.CCASH_FLOW_CODE = CashFlowPlanCode;
                poParam.CYEAR = Year;
                poParam.CCURRENCY_CODE = CurrencyCode;
                poParam.CCURRENCY_RATE = CurrencyRate;
                poParam.INO_PERIOD_FROM = loCopyBaseAmountEntity.INO_PERIOD_FROM;
                poParam.INO_PERIOD_TO = loCopyBaseAmountEntity.INO_PERIOD_TO;
                var loReturn = await _GSM00720Model.GetCopyBaseAmountListAsync(poParam);

                loCopyBaseAmountList = new ObservableCollection<GSM00720CopyBaseLocalAmountDTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCurrencyList()
        {
            var loEx = new R_Exception();

            try
            {
                loCurrency = await _GSM00720Model.GetCurrencyListAsync();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSM00710TemplateCashFlowUserInterface> DownloadTemplate()
        {
            var loEx = new R_Exception();
            GSM00710TemplateCashFlowUserInterface loResult = null;

            try
            {
                loResult = await _GSM00720Model.TemplateGSM00710CashFlowPlan();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM00720TemplateCashFlowPlan> DownloadTemplate720()
        {
            var loEx = new R_Exception();
            GSM00720TemplateCashFlowPlan loResult = null;

            try
            {
                loResult = await _GSM00720Model.TemplatCashFlowInterface();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task GetCashFlowPlanId(string cashFlowCode, string cashFlowGroupCode, string cYear, string periodNo)
        {
            var loEx = new R_Exception();
            try
            {

                //R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_CODE, cashFlowCode);  

                var loParam = new GSM00720DTO() { CCASH_FLOW_CODE = cashFlowCode, CCASH_FLOW_GROUP_CODE = cashFlowGroupCode, CCYEAR = cYear, CPERIOD_NO = periodNo};
                var loResult = await _GSM00720Model.R_ServiceGetRecordAsync(loParam);
                loEntity = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveCashFlowPlan(GSM00720DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM00720DTO loResult = null;
            try
            {
                loResult = await _GSM00720Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}

