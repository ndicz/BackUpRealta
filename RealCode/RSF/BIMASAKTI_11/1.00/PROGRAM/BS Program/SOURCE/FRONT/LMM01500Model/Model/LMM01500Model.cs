using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMM01500Common;
using LMM01500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;


namespace LMM01500Model.Model
{
    public class LMM01500Model : R_BusinessObjectServiceClientBase<LMM01500GeneralInfoDTO>, ILMM01500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM01500";
        private const string DEFAULT_MODULE = "LM";

        public LMM01500Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<LMM01500InitialProcessListDTO> GetInitialProcessPropertyStream()
        {
            var loEx = new R_Exception();
            LMM01500InitialProcessListDTO loResult = new LMM01500InitialProcessListDTO();

            try
            {
            
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01500InitialProcessDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500.GetInitialProcessProperty),
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
        
        public async Task<LMM01500GeneralInfoListDTO> GetInvoiceGroupListStreamAsync()
        {
            var loEx = new R_Exception();
            LMM01500GeneralInfoListDTO loResult = new LMM01500GeneralInfoListDTO();

            try
            {
            
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01500GeneralInfoDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500.GetInvoiceGroupList),
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
        
        public IAsyncEnumerable<LMM01500InitialProcessDTO> GetInitialProcessProperty()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM01500GeneralInfoDTO> GetInvoiceGroupList()
        {
            throw new NotImplementedException();
        }

        public LMM01500GeneralInfoDTO GetActiveInactive(LMM01500GeneralInfoDTO poParamDto)
        {
            throw new NotImplementedException();
        }
        
        
    }
}