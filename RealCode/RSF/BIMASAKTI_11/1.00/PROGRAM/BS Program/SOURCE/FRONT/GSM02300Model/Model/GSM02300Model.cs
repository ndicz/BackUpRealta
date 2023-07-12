using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM02300Common;
using GSM02300Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM02300Model.Model
{
    public class GSM02300Model : R_BusinessObjectServiceClientBase<GSM02300DTO>, IGSM02300
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02300";
        private const string DEFAULT_MODULE = "GS";
        public GSM02300Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GSM02300ListDTO> GetAllPropertyAsync()
        {
            var loEx = new R_Exception();
            GSM02300ListDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM02300ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02300.GetAllProperty), DEFAULT_MODULE,
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

        public async Task<GSM02300ListPropertyTypeDTO> GetAllPropertyTypeAsync()
        {
            var loEx = new R_Exception();
            GSM02300ListPropertyTypeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM02300ListPropertyTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02300.GetPropertyType), DEFAULT_MODULE,
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

        public GSM02300ListDTO GetAllProperty()
        {
            throw new NotImplementedException();
        }

        public GSM02300ListPropertyTypeDTO GetPropertyType()
        {
            throw new NotImplementedException();
        }
    }
}
