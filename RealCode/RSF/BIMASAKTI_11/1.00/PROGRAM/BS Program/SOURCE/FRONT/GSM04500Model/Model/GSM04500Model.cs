using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM04500Model.Model
{
    public class GSM04500Model : R_BusinessObjectServiceClientBase<GSM04500DTO>, IGSM04500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM04500";
        private const string DEFAULT_MODULE = "GS";
        public GSM04500Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }
        
        public async Task<GSM04500ListDTO> GetallJournalGroupListStreamAsync()
        {
            var loEx = new R_Exception();
            GSM04500ListDTO loResult = new GSM04500ListDTO();

            try
            {
           
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500.GetJournalGroupListStream),
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
        
        public async Task<GSM04500PropertyListDTO> GetPropertyStreamAsync()
        {
            var loEx = new R_Exception();
            GSM04500PropertyListDTO loResult = new GSM04500PropertyListDTO();

            try
            {
           
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04500PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500.GetAllPropertyListStream),
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

        public async Task<GSM04500JournalGroupTypeListDTO> GetJournalGroupTypeStreamAsync()
        {
            var loEx = new R_Exception();
            GSM04500JournalGroupTypeListDTO loResult = new GSM04500JournalGroupTypeListDTO();

            try
            {
           
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04500JournalGroupTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500.GetListJournalGroupTypeStream),
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

        public IAsyncEnumerable<GSM04500DTO> GetJournalGroupListStream()
        {
                throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM04500PropertyDTO> GetAllPropertyListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM04500JournalGroupTypeDTO> GetListJournalGroupTypeStream()
        {
            throw new NotImplementedException();
        }
    }
}