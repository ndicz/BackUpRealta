using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM10500Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM10500Model.Model
{
    public class GSM10500Model : R_BusinessObjectServiceClientBase<GSM10500DTO>, IGSM10500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM10500";
        private const string DEFAULT_MODULE = "GS";
        public GSM10500Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }
    
        public async Task<GSM10500ListDTO> GetAllAgeingHDStreamAsync()
        {
            var loEx = new R_Exception();
            GSM10500ListDTO loResult = new GSM10500ListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM10500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM10500.GetAllAgeingHDStream),
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

        public async Task<GSM10500RoundingModeListDTO> GetRoundingModeAsync()
        {
            var loEx = new R_Exception();
            GSM10500RoundingModeListDTO loResult = new GSM10500RoundingModeListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM10500RoundingModeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM10500.GetRoundingMode),
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



        public IAsyncEnumerable<GSM10500DTO> GetAllAgeingHDStream()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<GSM10500RoundingModeDTO> GetRoundingMode()
        {
            throw new System.NotImplementedException();
        }
    }
}