using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM10000Common;
using GSM10000Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM10000Model.Model
{
    public class GSM10000Model : R_BusinessObjectServiceClientBase<GSM10000DTO>, IGSM10000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM10000";
        private const string DEFAULT_MODULE = "GS";
        public GSM10000Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<GSM10000ListDTO> GetAllStreamAsync()
        {
            var loEx = new R_Exception();
            GSM10000ListDTO loResult = new GSM10000ListDTO();

            try
            {
           
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM10000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM10000.GetAllHolidayStream),
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
        public async Task<GSM10000ActiveInactiveDTO> GetActiveInactiveS(LMM02000ActiveInactiveParam poParamDto)
        {
            var loEx = new R_Exception();
            GSM10000ActiveInactiveDTO loRtn = null;
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM10000ActiveInactiveDTO, LMM02000ActiveInactiveParam>(
                    _RequestServiceEndPoint,
                    nameof(IGSM10000.GetActiveInactive), poParamDto, DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        public IAsyncEnumerable<GSM10000DTO> GetAllHolidayStream()
        {
            throw new System.NotImplementedException();
        }

        public GSM10000ActiveInactiveDTO GetActiveInactive(LMM02000ActiveInactiveParam poParamDto)
        {
            throw new NotImplementedException();
        }
    }
}