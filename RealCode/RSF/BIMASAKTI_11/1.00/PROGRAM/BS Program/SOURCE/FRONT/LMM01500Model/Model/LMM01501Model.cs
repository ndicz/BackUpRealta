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
    public class LMM01501Model : R_BusinessObjectServiceClientBase<LMM01500TemplateBankAccountDTO>, ILMM01501
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM01501";
        private const string DEFAULT_MODULE = "LM";

        public LMM01501Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<LMM01500TemplateBankAccountListDTO> GetInvoiceGroupDeptListStreamAsync()
        {
            var loEx = new R_Exception();
            LMM01500TemplateBankAccountListDTO loResult = new LMM01500TemplateBankAccountListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01500TemplateBankAccountDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01501.GetInvoiceGroupDeptList),
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

        public IAsyncEnumerable<LMM01500TemplateBankAccountDTO> GetInvoiceGroupDeptList()
        {
            throw new NotImplementedException();
        }

        public LMM01500TemplateBankAccountListDTO GetInvoiceGroup()
        {
            throw new NotImplementedException();
        }
    }
}