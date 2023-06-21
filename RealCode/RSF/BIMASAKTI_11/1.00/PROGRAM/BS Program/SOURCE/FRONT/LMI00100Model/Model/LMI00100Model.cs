using R_BusinessObjectFront;
using System;
using System.Threading.Tasks;
using LMI00100Common;
using LMI00100Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;

namespace LMI00100Model.Model
{
    public class LMI00100Model : R_BusinessObjectServiceClientBase<LMI00100DTO>, ILMI00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMI00100";
        private const string DEFAULT_MODULE = "LM";

        public LMI00100Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<LMI00100ListDTO> GetAllBankAccountAsync(string lcPropertyId)
        {
            var loEx = new R_Exception();
            LMI00100ListDTO loResult = null;

            try
            {
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstantLMI00100.CPROPERTY_ID,
                    lcPropertyId);

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMI00100ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMI00100.GetAllLMI00100List), DEFAULT_MODULE,
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

        public async Task<LMI00100ListPropertyDTO> GetAllPropertyAsync()
        {
            var loEx = new R_Exception();
            LMI00100ListPropertyDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMI00100ListPropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMI00100.GetLMI00100Property), DEFAULT_MODULE,
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


        public LMI00100ListDTO GetAllLMI00100List()
        {
            throw new NotImplementedException();
        }

        public LMI00100ListPropertyDTO GetLMI00100Property()
        {
            throw new NotImplementedException();
        }
    }
}
