using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMM02500Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMM02500Model.Model
{
    public class LMM02500Model : R_BusinessObjectServiceClientBase<LMM02500DTO>, ILMM02500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM02500";
        private const string DEFAULT_MODULE = "LM";

        public LMM02500Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<LMM02500InitialProcessListDTO> GetInitialProcessStreamASync()
        {
            var loEx = new R_Exception();
            LMM02500InitialProcessListDTO loResult = new LMM02500InitialProcessListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02500InitialProcessDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500.GetInitialProcessStream),
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
        
        public async Task<LMM02500ListDTO> GetTenantGroupListStreamAsync()
        {
            var loEx = new R_Exception();
            LMM02500ListDTO loResult = new LMM02500ListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02500DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500.GetTenantGroupListStream),
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


        public IAsyncEnumerable<LMM02500InitialProcessDTO> GetInitialProcessStream()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<LMM02500DTO> GetTenantGroupListStream()
        {
            throw new System.NotImplementedException();
        }
    }
}