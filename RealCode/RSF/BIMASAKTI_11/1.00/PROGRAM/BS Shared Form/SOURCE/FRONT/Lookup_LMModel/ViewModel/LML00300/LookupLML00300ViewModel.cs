﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Lookup_LMCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_LMModel.ViewModel.LML00300
{
    public class LookupLML00300ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        public ObservableCollection<LML00300DTO> SupervisorList = new ObservableCollection<LML00300DTO>();
        
        public async Task GetSupervisorList(LML00300ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCOMPANY_ID, poParam.CCOMPANY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CUSER_ID, poParam.CUSER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);

                var loResult = await _model.LML00300SupervisorListAsync();
                SupervisorList = new ObservableCollection<LML00300DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}