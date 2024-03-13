using BlazorClientHelper;
using LMM02500Common;
using LMM02500Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace LMM02500Front;

public partial class LMM02510 : R_Page
{
    private R_Conductor? _conductorFullLMM02500;
    private R_Grid<LMM02510DTO>? _gridRefLMM02510;

    private readonly LMM02510ViewModel _viewModelLMM02510 = new();
    private readonly LMM02511ViewModel _viewModelLMM02511 = new();

    private R_TabStrip? _tabStripRef;

    private bool IsSuccess;
    private bool IsChangedValueLMM02500Profile;
    private bool _flagCombo;

    [Inject] private IClientHelper? clientHelper { get; set; }
    
    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelLMM02510.Init(poParam);
            await _gridRefLMM02510.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private async Task ServiceGetRecordLMM02510(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {

            await _viewModelLMM02510.GetEntity(_viewModelLMM02510.loEntity);
            eventArgs.Result = _viewModelLMM02510.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

}