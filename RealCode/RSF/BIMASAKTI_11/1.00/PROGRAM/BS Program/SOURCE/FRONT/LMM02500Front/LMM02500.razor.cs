using BlazorClientHelper;
using LMM02500Common;
using LMM02500Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace LMM02500Front;

public partial class LMM02500 : R_Page
{

    private LMM02500ViewModel _viewModel = new();
    private R_Grid<LMM02500DTO> _gridRef;
    private R_Conductor _conductorRef;
    private R_ConductorGrid _conductorGridRef;
    
    private LMM02500TabParamDTO _loTabParameter = new();
    private R_TabStrip? _tabStripRef;
    private R_TabPage? _tabProfileRef;
    private bool _pageContractorOnCRUDmode;
    private bool _flagCombo;
    [Inject] private IClientHelper? _clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetInitialProcess();
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    private async Task OnChanged(object poParam)
    {
        var loEx = new R_Exception();
        string lsProperty = (string)poParam;
        try
        {
            _viewModel.propertyValue = lsProperty;
            await _gridRef.R_RefreshGrid(null);
            
            if (_tabStripRef.ActiveTab.Id == "Profile")
            {
                await _tabProfileRef.InvokeRefreshTabPageAsync(_loTabParameter);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }


        R_DisplayException(loEx);
    }
    
    private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.TenantGroupList(_viewModel.propertyValue);
            eventArgs.ListEntityResult = _viewModel.loGridList;
            //await _gridRef.AutoFitAllColumnsAsync();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }
    
    private async Task R_DisplayGetRecordLMM02500(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            //Untuk Tab - 1
            var loConvertTabParameter = R_FrontUtility.ConvertObjectToObject<LMM02500TabParamDTO>(eventArgs.Data);
            _loTabParameter = new LMM02500TabParamDTO();
            _loTabParameter = loConvertTabParameter;
            if (_clientHelper?.UserId != null) _loTabParameter.CUSER_LOGIN_ID = _clientHelper.UserId;
            await Task.CompletedTask;
            
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
    {
        if (eventArgs.Id == "List")
        {
            await _gridRef.R_RefreshGrid(null);
        }
    }
    
    private void OnChangedTab(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        _flagCombo = eventArgs.TabStripTab.Id == "List";
    }
    
    private void BeforeOpenProfile(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMM02510);
        eventArgs.Parameter = _viewModel.loEntity;
    }
    
}