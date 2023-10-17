using LMM00200Common;
using LMM00200Common.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM00200Model
{
    public class LMM00200Model : R_BusinessObjectServiceClientBase<LMM00200DTO>, ILMM00200
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_CHECKPOINT_NAME = "api/LMM00200";
        private const string DEFAULT_MODULE = "LM";

        public LMM00200Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public LMM00200ActiveInactiveParamDTO GetActiveParam()
        {
            throw new NotImplementedException();
        }

        public async Task GetActiveParamAsync()
        {
            R_Exception loEx = new R_Exception();
            LMM00200ActiveInactiveParamDTO loRtn = new LMM00200ActiveInactiveParamDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM00200ActiveInactiveParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM00200.GetActiveParam),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

        }

        public IAsyncEnumerable<LMM00200StreamDTO> GetUserParamList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM00200StreamDTO>> GetUserParamListAsync()

        {
            var loEx = new R_Exception();
            List<LMM00200StreamDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM00200StreamDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM00200.GetUserParamList),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }

    }
}
