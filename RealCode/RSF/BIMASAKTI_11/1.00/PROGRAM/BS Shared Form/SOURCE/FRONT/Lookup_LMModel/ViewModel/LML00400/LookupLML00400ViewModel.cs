﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Lookup_LMCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_LMModel.ViewModel.LML00400
{
    public class LookupLML00400ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        public ObservableCollection< LML00400DTO> UtilityChargesList = new ObservableCollection<LML00400DTO>();
        
        public async Task GetUtitlityChargesList(LML00400ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCOMPANY_ID, poParam.CCOMPANY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CUSER_ID, poParam.CUSER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCHARGE_TYPE_ID, poParam.CCHARGE_TYPE_ID);

                var loResult = await _model.LML00400GetUtilityChargesListAsync();
                UtilityChargesList = new ObservableCollection<LML00400DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}