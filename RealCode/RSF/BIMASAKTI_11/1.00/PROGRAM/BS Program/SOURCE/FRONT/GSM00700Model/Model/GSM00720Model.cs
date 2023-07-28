using GSM00700Common.DTO;
using GSM00700Common;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;

namespace GSM00700Model.Model
{
    public class GSM00720Model : R_BusinessObjectServiceClientBase<GSM00720DTO>, IGSM00720
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM00720";
        private const string DEFAULT_MODULE = "GS";

        public GSM00720Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GSM00720ListDTO> GetCashFlowPlanAsync()
        {
            var loEx = new R_Exception();
            GSM00720ListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00720ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00720.GetAllCashFlowPlan), DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM00720YearListDTO> GetYearListAsync()
        {
            var loEx = new R_Exception();
            GSM00720YearListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00720YearListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00720.GetYearList), DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM00720CopyFromYearListDTO> GetCopyFromYearListAsync()
        {
            var loEx = new R_Exception();
            GSM00720CopyFromYearListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00720CopyFromYearListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00720.GetCopyFromYearList), DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM00720CopyBaseAmountListDTO> GetCopyBaseAmountListAsync()
        {
            var loEx = new R_Exception();
            GSM00720CopyBaseAmountListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00720CopyBaseAmountListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00720.GetCopyBaseAmountList), DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM00720CopyLocalAmountListDTO> GetCopyLocalAmountListAsync()
        {
            var loEx = new R_Exception();
            GSM00720CopyLocalAmountListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00720CopyLocalAmountListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00720.GetCopyLocalAmountList), DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM00720CurrencyDTO> GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            GSM00720CurrencyDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00720CurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00720.GetCurrencyList), DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }

            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM00720InitialProsesListDTO> GetInitialProsesAsync()
        {
            var loEx = new R_Exception();
            GSM00720InitialProsesListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00720InitialProsesListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00720.GetInitialProses), DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }

            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM00710TemplateCashFlowUserInterface> TemplateGSM00710CashFlowPlan()
        {
            var loEx = new R_Exception();
            GSM00710TemplateCashFlowUserInterface loResult = new GSM00710TemplateCashFlowUserInterface();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00710TemplateCashFlowUserInterface>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00720.GetTemplate),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<GSM00720TemplateCashFlowPlan> TemplatCashFlowInterface()
        {
            var loEx = new R_Exception();
            GSM00720TemplateCashFlowPlan loResult = new GSM00720TemplateCashFlowPlan();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00720TemplateCashFlowPlan>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00720.GetTemplate720),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public GSM00720ListDTO GetAllCashFlowPlan()
        {
            throw new NotImplementedException();
        }

        public GSM00720YearListDTO GetYearList()
        {
            throw new NotImplementedException();
        }

        public GSM00720CopyFromYearListDTO GetCopyFromYearList()
        {
            throw new NotImplementedException();
        }

        public GSM00720CopyBaseAmountListDTO GetCopyBaseAmountList()
        {
            throw new NotImplementedException();
        }

        public GSM00720CopyLocalAmountListDTO GetCopyLocalAmountList()
        {
            throw new NotImplementedException();
        }

        public GSM00720CurrencyDTO GetCurrencyList()
        {
            throw new NotImplementedException();
        }

        public GSM00720InitialProsesListDTO GetInitialProses()
        {
            throw new NotImplementedException();
        }

        public GSM00710TemplateCashFlowUserInterface GetTemplate()
        {
            throw new NotImplementedException();
        }

        public GSM00720TemplateCashFlowPlan GetTemplate720()
        {
            throw new NotImplementedException();
        }
    }

}
