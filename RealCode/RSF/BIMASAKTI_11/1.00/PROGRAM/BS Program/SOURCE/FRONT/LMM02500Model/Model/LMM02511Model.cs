using System;
using System.Threading.Tasks;
using LMM02500Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMM02500Model.Model
{
    public class LMM02511Model : R_BusinessObjectServiceClientBase<LMM02511DTO>, ILMM02511
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM02511";
        private const string DEFAULT_MODULE = "LM";

        public LMM02511Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        
        public async Task<LMM02511ListDTO> GetTaxCodeSteam()
        {
            var loEx = new R_Exception();
            LMM02511ListDTO loResult = new LMM02511ListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02511DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02511.GetTaxCode),
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
        
        public async Task<LMM02511ListDTO> GetIDTypeSteam()
        {
            var loEx = new R_Exception();
            LMM02511ListDTO loResult = new LMM02511ListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02511DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02511.GetIDType),
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
        
        public async Task<LMM02511ListDTO> GetTaxTypeSteam()
        {
            var loEx = new R_Exception();
            LMM02511ListDTO loResult = new LMM02511ListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02511DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02511.GetTaxType),
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
        
        
        
        
        
        public LMM02511ListDTO GetTaxCode()
        {
            throw new System.NotImplementedException();
        }

        public LMM02511ListDTO GetIDType()
        {
            throw new System.NotImplementedException();
        }

        public LMM02511ListDTO GetTaxType()
        {
            throw new System.NotImplementedException();
        }
    }
}