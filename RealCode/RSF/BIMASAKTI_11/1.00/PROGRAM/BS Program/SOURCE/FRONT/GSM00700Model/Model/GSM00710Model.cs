using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common;
using GSM00700Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM00700Model.Model
{
    public class GSM00710Model : R_BusinessObjectServiceClientBase<GSM00710DTO>, IGSM00710
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM00710";
        private const string DEFAULT_MODULE = "GS";

        public GSM00710Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        //public async Task<GSM00710ListDTO> GetAllCashFlowListAsync()
        //{
        //    var loEx = new R_Exception();
        //    GSM00710ListDTO loResult = null;
        //    try
        //    {
        //        R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
        //        loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00710ListDTO>(
        //            _RequestServiceEndPoint,
        //            nameof(IGSM00710.GetAllCashFlowList), DEFAULT_MODULE,
        //            _SendWithContext,
        //            _SendWithToken);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();

        //    return loResult;
        //}

        public async Task<GSM00710ListDTO> GetAllCashFlowStreamAsync()
        {
            var loEx = new R_Exception();
            GSM00710ListDTO loResult = new GSM00710ListDTO();

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM00710DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00710.GetAllCashFlowStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }


        public async Task<GSM00710CashFlowTypeListDTO> GetListCashFlowTypeAsync()
        {
            var loEx = new R_Exception();
            GSM00710CashFlowTypeListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00710CashFlowTypeListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00710.GetListCashFlowType), DEFAULT_MODULE,
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
        public GSM00710ListDTO GetAllCashFlowList()
        {
            throw new NotImplementedException();
        }

        public GSM00710CashFlowTypeListDTO GetListCashFlowType()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM00710DTO> GetAllCashFlowStream()
        {
            throw new NotImplementedException();
        }
    }
}
