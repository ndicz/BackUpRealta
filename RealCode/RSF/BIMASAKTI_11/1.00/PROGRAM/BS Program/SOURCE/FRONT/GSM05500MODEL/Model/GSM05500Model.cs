using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM05500Common;
using GSM05500Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using static GSM05500Common.DTO.GSM05500ListDTO;

namespace GSM05500Model.Model
{
    public class GSM05500Model : R_BusinessObjectServiceClientBase<GSM05500DTO>, IGSM05500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05500";
        private const string DEFAULT_MODULE = "GS";
        public GSM05500Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<GSM05500ListDTO> GetAllStreamAsync()
        {
            var loEx = new R_Exception();
            GSM05500ListDTO loResult = new GSM05500ListDTO();

            try
            {
           
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05500.GetAllCurrencyStream),
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

        //public async Task<GSM05500ListDTO> GetAllAsync()
        //{
        //    var loEx = new R_Exception();
        //    GSM05500ListDTO loResult = null;

        //    try
        //    {
        //        //R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
        //        //loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05500ListDTO>(
        //        //    _RequestServiceEndPoint,
        //        //    nameof(IGSM05500.GetAllCurrencyList), DEFAULT_MODULE,
        //        //    _SendWithContext,
        //        //    _SendWithToken);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();        

        //    return loResult;
        //}


        public IAsyncEnumerable<GSM05500DTO> GetAllCurrencyStream()
        {
            throw new NotImplementedException();
        }

        // public GSM05500ListDTO GetAllCurrencyList()
        // {
        //     throw new NotImplementedException();
        // }
    }
}
