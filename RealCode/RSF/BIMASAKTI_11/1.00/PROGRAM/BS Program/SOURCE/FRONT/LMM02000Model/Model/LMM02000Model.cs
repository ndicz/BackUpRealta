using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMM02000Common;
using LMM02000Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMM02000Model.Model
{
    public class LMM02000Model : R_BusinessObjectServiceClientBase<LMM02000DTO>, ILMM02000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM02000";
        private const string DEFAULT_MODULE = "LM";

        public LMM02000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<LMM02000ListDTO> GetAllSalesman(string lcPropertyId)
        {
            var loEx = new R_Exception();
            LMM02000ListDTO loResult = null;

            try
            {
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstantLMM02000.CPROPERTY_ID, lcPropertyId);

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02000ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02000.GetAllLMM02000List), DEFAULT_MODULE,
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

        public async Task<LMM02010ListDTO> GetSalesmanGrid(string lcPropertyId)
        {
            var loEx = new R_Exception();
            LMM02010ListDTO loResult = null;
            try
            {
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstantLMM02000.CPROPERTY_ID, lcPropertyId);
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstantLMM02000.CSALESMAN_ID, lcPropertyId);

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02010ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02000.GetAllLMM02010List), DEFAULT_MODULE,
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

        public async Task<LMM02000ListDTO> GetSalesmanStreamAsync(string lcPropertyId)
        {
            var loEx = new R_Exception();
            LMM02000ListDTO loResult = new LMM02000ListDTO();

            try
            {
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstantLMM02000.CPROPERTY_ID, lcPropertyId);
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02000DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02000.GetAllLMM02000Stream),
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

        public async Task<LMM02000ListPropertyDTO> GetPropertyStreamAsync()
        {
            var loEx = new R_Exception();
            LMM02000ListPropertyDTO loResult = new LMM02000ListPropertyDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02000PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02000.GetAllLMM02000PropertyStream),
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

        public async Task<LMM02000ListPropertyDTO> GetProperty()
        {
            var loEx = new R_Exception();
            LMM02000ListPropertyDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02000ListPropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02000.GetLMM02000Property), DEFAULT_MODULE,
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


        public async Task<LMM02000ListGenderTypeDTO> GetGenderAll()
        {
            var loEx = new R_Exception();
            LMM02000ListGenderTypeDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02000ListGenderTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02000.GetGender), DEFAULT_MODULE,
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

        public async Task<LMM02000ListSalesmanTypeDTO> GetSalesmanTypeAll()
        {
            var loEx = new R_Exception();
            LMM02000ListSalesmanTypeDTO loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02000ListSalesmanTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02000.GetSalesmanType), DEFAULT_MODULE,
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


        public async Task GetActiveInactiveS()
        {
            var loEx = new R_Exception();
            LMM02000ActiveInactiveDTO loRtn = new LMM02000ActiveInactiveDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM02000ActiveInactiveDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02000.GetActiveInactive), DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

        }

        public async Task<LMM02000Template> GetTemplateAsync()
        {
            var loEx = new R_Exception();
            LMM02000Template loResult = new LMM02000Template();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02000Template>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02000.GetTemplate),
                    DEFAULT_MODULE,
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








        public IAsyncEnumerable<LMM02000DTO> GetAllLMM02000Stream()
        {
            throw new NotImplementedException();
        }

        public LMM02000ListDTO GetAllLMM02000List() // done
        {
            throw new NotImplementedException();
        }

        public LMM02010ListDTO GetAllLMM02010List() // done
        {
            throw new NotImplementedException();
        }

        public LMM02000ListPropertyDTO GetLMM02000Property() //done
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM02000PropertyDTO> GetAllLMM02000PropertyStream()
        {
            throw new NotImplementedException();
        }

        public LMM02000ListGenderTypeDTO GetGender()
        {
            throw new NotImplementedException();
        }

        public LMM02000ListSalesmanTypeDTO GetSalesmanType()
        {
            throw new NotImplementedException();
        }

        public LMM02000ActiveInactiveDTO GetActiveInactive()
        {
            throw new NotImplementedException();
        }

        public LMM02000Template GetTemplate()
        {
            throw new NotImplementedException();
        }
    }
}
