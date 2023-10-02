using GSM00700Common.DTO;
using GSM00700Common;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_APIClient;
using GSM00700Common.DTO.Report_DTO_GSM00700;

namespace GSM00700Model.Model
{
    public class GSM00700Model : R_BusinessObjectServiceClientBase<GSM00700DTO>, IGSM00700
    {

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM00700";
        private const string DEFAULT_MODULE = "GS";

        public GSM00700Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        //public async Task<GSM00700ListDTO> GetAllAsync()
        //{
        //    var loEx = new R_Exception();
        //    GSM00700ListDTO loResult = null;
        //    try
        //    {
        //        R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
        //        loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00700ListDTO>(
        //            _RequestServiceEndPoint,
        //            nameof(IGSM00700.GetAllCashFlowGroupList), DEFAULT_MODULE,
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

        public async Task<GSM00700CashFlowGroupTypeListDTO> GetCashFlowGroupTypeAsync()
        {
            var loEx = new R_Exception();
            GSM00700CashFlowGroupTypeListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00700CashFlowGroupTypeListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00700.GetListCashFlowGroupType), DEFAULT_MODULE,
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

        public async Task<GSM00700ListDTO> GetAllCashFlowGroupStreamAsync()
        {
            var loEx = new R_Exception();
            GSM00700ListDTO loResult = new GSM00700ListDTO();

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM00700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00700.GetAllCashFlowGroupStream),
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

        public async Task<GSM00700ListDTO> GetPrint(GSM00700PrintCashFlowParameterDTo poParamDto)
        {
            var loEx = new R_Exception();
            GSM00700ListDTO loResult = new GSM00700ListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00700ListDTO, GSM00700PrintCashFlowParameterDTo>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00700.GetPrintCashFlow), 
                    poParamDto, 
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

        public async Task<GSM00700ListDTO> GetYearFromPrintAsync()
        {
            var loEx = new R_Exception();
            GSM00700ListDTO loResult = new GSM00700ListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00700ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00700.GetYearFromPrint),
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

        public async Task<GSM00700ListDTO> GetYearToPrintAsync()
        {
            var loEx = new R_Exception();
            GSM00700ListDTO loResult = new GSM00700ListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00700ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00700.GetYearToPrint),
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



        //Not Used
        public IAsyncEnumerable<GSM00700DTO> GetAllCashFlowGroupStream()
        {
            throw new NotImplementedException();
        }

        //public GSM00700ListDTO GetAllCashFlowGroupList()
        //{
        //    throw new NotImplementedException();
        //}

        public GSM00700CashFlowGroupTypeListDTO GetListCashFlowGroupType()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM00700DTO> GetPrintCashFlow(GSM00700PrintCashFlowParameterDTo poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM00700DTO> GetYearFromPrint()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM00700DTO> GetYearToPrint()
        {
            throw new NotImplementedException();
        }
    }
}
