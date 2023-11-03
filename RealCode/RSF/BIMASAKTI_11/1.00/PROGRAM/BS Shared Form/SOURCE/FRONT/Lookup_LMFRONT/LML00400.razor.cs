﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_LMCOMMON.DTOs;
using Lookup_LMModel.ViewModel.LML00400;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_LMFRONT;

public partial class LML00400 : R_Page
{
    private LookupLML00400ViewModel _viewModelLML00400 = new LookupLML00400ViewModel();
    private R_Grid<LML00400DTO> GridRef;
    
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await GridRef.R_RefreshGrid(poParameter);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = (LML00400ParameterDTO)eventArgs.Parameter;
            await _viewModelLML00400.GetUtitlityChargesList(loParam);
            eventArgs.ListEntityResult = _viewModelLML00400.UtilityChargesList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task Button_OnClickOkAsync()
    {
        var loData = GridRef.GetCurrentData();
        await this.Close(true, loData);
    }
    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }
}