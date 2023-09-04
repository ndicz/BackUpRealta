using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM05500Common;
using GSM05500Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using static GSM05500Common.DTO.GSM05510ListDTO;

namespace GSM05500Model.Model
{
    public class GSM05510Model : R_BusinessObjectServiceClientBase<GSM05510DTO>, IGSM05510
    {

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05510";
        private const string DEFAULT_MODULE = "GS";
        public GSM05510Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GSM05510ListDTO> GetAllStreamAsync()
        {

            var loEx = new R_Exception();
            GSM05510ListDTO loResult = new GSM05510ListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05510DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05510.GetAllRateTypeStream),
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


        //public async Task<GSM05510ListDTO> GetAllAsync()
        //{

        //    var loEx = new R_Exception();
        //    GSM05510ListDTO loResult = null;

        //    try
        //    {
        //        R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
        //        loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05510ListDTO>(
        //            _RequestServiceEndPoint,
        //            nameof(IGSM05510.GetAllRateTypeList), DEFAULT_MODULE,
        //            _SendWithContext,
        //            _SendWithToken);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //    return loResult;
        //}

        public IAsyncEnumerable<GSM05510DTO> GetAllRateTypeStream()
        {
            throw new NotImplementedException();
        }

        public GSM05510ListDTO GetAllRateTypeList()
        {
            throw new NotImplementedException();
        }
    }
}
