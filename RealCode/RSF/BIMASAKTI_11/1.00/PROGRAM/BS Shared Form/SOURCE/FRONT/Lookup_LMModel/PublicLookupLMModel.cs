﻿using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lookup_LMCOMMON;
using Lookup_LMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_APIClient;

namespace Lookup_LMModel
{
    public class PublicLookupLMModel : R_BusinessObjectServiceClientBase<LML00200DTO>, IPublicLookupLM
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupLM";
        private const string DEFAULT_MODULE = "LM";

        public PublicLookupLMModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region implements INTERFACE
        public IAsyncEnumerable<LML00100DTO> LML00100GetSalesTaxList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00200DTO> LML00200UnitChargesList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML00300DTO> LML00300SupervisorList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00400DTO> LML00400UtilityChargesList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00500DTO> LML00500SalesmanList()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LML00100
        public async Task<LMLGenericList<LML00100DTO>> LML00100GetSalesTaxListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00100DTO> loResult = new LMLGenericList<LML00100DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00100GetSalesTaxList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #endregion
        #region LML00200 

        public async Task<LMLGenericList<LML00200DTO>> LML00200GetUnitChargesListAsync ()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00200DTO> loResult = new LMLGenericList<LML00200DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00200UnitChargesList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00300 

        public async Task<LMLGenericList<LML00300DTO>> LML00300SupervisorListAsync ()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00300DTO> loResult = new LMLGenericList<LML00300DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00300SupervisorList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00400
        public async Task<LMLGenericList<LML00400DTO>> LML00400GetUtilityChargesListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00400DTO> loResult = new LMLGenericList<LML00400DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00400DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00400UtilityChargesList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00500
        public async Task<LMLGenericList<LML00500DTO>> LML00500GetSalesmanListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00500DTO> loResult = new LMLGenericList<LML00500DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00500SalesmanList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion


    }
}
