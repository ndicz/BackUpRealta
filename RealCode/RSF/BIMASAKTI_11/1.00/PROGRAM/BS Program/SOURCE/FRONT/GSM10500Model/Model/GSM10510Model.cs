using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM10500Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM10500Model.Model
{
    public class GSM10510Model : R_BusinessObjectServiceClientBase<GSM10510DTO>, IGSM10510
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM10510";
        private const string DEFAULT_MODULE = "GS";
        public GSM10510Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }
        public async Task<GSM10510ListDTO> GetAllAgeingDTStreamAsync()
        {
            var loEx = new R_Exception();
            GSM10510ListDTO loResult = new GSM10510ListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM10510DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM10510.GetAllAgeingDTStream),
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
        public IAsyncEnumerable<GSM10510DTO> GetAllAgeingDTStream()
        {
            throw new System.NotImplementedException();
        }
    }
}