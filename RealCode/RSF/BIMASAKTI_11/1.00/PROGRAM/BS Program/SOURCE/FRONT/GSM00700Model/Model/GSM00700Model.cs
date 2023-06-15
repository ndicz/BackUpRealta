using GSM00700Common.DTO;
using GSM00700Common;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_APIClient;

namespace GSM00700Model.Model
{
    public class GSM00700Model : R_BusinessObjectServiceClientBase<GSM00700DTO>, IGSM00700
    {

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM00700";
        private const string DEFAULT_MODULE = "GS";

        public GSM00700Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GSM00700ListDTO> GetAllAsync()
        {
            var loEx = new R_Exception();
            GSM00700ListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00700ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00700.GetAllCashFlowGroupList), DEFAULT_MODULE,
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

        public async Task<GSM00700CashFlowGroupTypeListDTO> GetCashFlowGroupTypeAsync()
        {
            var loEx = new R_Exception();
            GSM00700CashFlowGroupTypeListDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00700CashFlowGroupTypeListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00700.GetListCashFlowGroupType), DEFAULT_MODULE,
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
        public IAsyncEnumerable<GSM00700DTO> GetAllCashFlowGroupStream()
        {
            throw new NotImplementedException();
        }

        public GSM00700ListDTO GetAllCashFlowGroupList()
        {
            throw new NotImplementedException();
        }

        public GSM00700CashFlowGroupTypeListDTO GetListCashFlowGroupType()
        {
            throw new NotImplementedException();
        }
    }
}
