using GSM05500Common.DTO;
using GSM05500Common;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;
using R_BlazorFrontEnd;

namespace GSM05500Model.Model
{
    public class GSM05520Model : R_BusinessObjectServiceClientBase<GSM05520DTO>, IGSM05520
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05520";
        private const string DEFAULT_MODULE = "GS";
        public GSM05520Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GSM05520DTO>> GetAllStreamAsync()
        {

            var loEx = new R_Exception();
            List<GSM05520DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05520DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05520.GetAllRateStream), DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }


        public async Task<GSM05520ListDTO> GetAllAsync(string RateTypeCode, string CrateDate)
        {

            var loEx = new R_Exception();
            GSM05520ListDTO loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM05500.CRATETYPE_CODE, RateTypeCode);
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM05500.CRATE_DATE, CrateDate);
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05520ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05520.GetAllRateList), DEFAULT_MODULE,
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

        public async Task<GSM05520ListDTOGetRateType> GetRateList()
        {

            var loEx = new R_Exception();
            GSM05520ListDTOGetRateType loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05520ListDTOGetRateType>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05520.GetListRateType), DEFAULT_MODULE,
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

        public async Task<GSM05520DTOLocalBaseCurrency> GetLccCurrency()
        {

            var loEx = new R_Exception();
            GSM05520DTOLocalBaseCurrency loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05520DTOLocalBaseCurrency>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05520.GetLcCurrency), DEFAULT_MODULE,
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






        public IAsyncEnumerable<GSM05520DTO> GetAllRateStream()
        {
            throw new NotImplementedException();
        }

        public GSM05520ListDTO GetAllRateList()
        {
            throw new NotImplementedException();
        }

        public GSM05520ListDTOGetRateType GetListRateType()
        {
            throw new NotImplementedException();
        }

        public GSM05520DTOLocalBaseCurrency GetLcCurrency()
        {
            throw new NotImplementedException();
        }
    }
}
