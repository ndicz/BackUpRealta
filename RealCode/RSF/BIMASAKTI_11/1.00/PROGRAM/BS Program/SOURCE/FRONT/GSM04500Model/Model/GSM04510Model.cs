using System.Collections.Generic;
using System.Threading.Tasks;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM04500Model.Model
{
    public class GSM04510Model : R_BusinessObjectServiceClientBase<GSM04510DTO>, IGSM04510
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM04510";
        private const string DEFAULT_MODULE = "GS";
        public GSM04510Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        
        public async Task<GSM04510ListDTO> GetallJournalGroupGOAListStreamAsync()
        {
            var loEx = new R_Exception();
            GSM04510ListDTO loResult = new GSM04510ListDTO();

            try
            {
           
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04510DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04510.GetJournalGroupGOAListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loTemp;
            }
            catch (System.Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public IAsyncEnumerable<GSM04510DTO> GetJournalGroupGOAListStream()
        {
            throw new System.NotImplementedException();
        }
    }
}