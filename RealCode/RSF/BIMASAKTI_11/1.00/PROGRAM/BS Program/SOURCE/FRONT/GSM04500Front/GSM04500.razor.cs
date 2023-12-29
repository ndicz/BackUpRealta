using GSM04500Common.DTOs;
using GSM04500Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM04500Front;

public partial class GSM04500 : R_Page
{
    private GSM04500ViewModel _GSM4500ViewModel = new();
    private R_ConductorGrid _conGridGSM04500Ref;
    private R_Conductor _conductorRef;
    private R_Grid<GSM04500DTO> _gridRefGSM04500;
    private bool _enableFlag;
    private R_TabStripTab _tabAccountSetting;
    private R_TabStrip _tab;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        try
        {
            await _GSM4500ViewModel.GetPropertyStream();
            await _GSM4500ViewModel.GetJournalTypeListStream();
            // await _GSM4500ViewModel.GetJournalGroupListStream();
            await _gridRefGSM04500.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetPropertyListData()
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM4500ViewModel.GetPropertyStream();
            await _GSM4500ViewModel.GetJournalTypeListStream();
            await _gridRefGSM04500.R_RefreshGrid(null);
            //eventArgs.ListEntityResult = _viewModel.loGridListProperty;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetListRecordJournal(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM4500ViewModel.GetJournalGroupListStream();
            eventArgs.ListEntityResult = _GSM4500ViewModel.loGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private void JournalGroupGetEntity(R_DisplayEventArgs eventArgs)
    {
        _GSM4500ViewModel.loEntity = (GSM04500DTO)eventArgs.Data;
    }
    
    private async Task JournalGroupGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500DTO>(eventArgs.Data);

            await _GSM4500ViewModel.GetJournalGroupId(loParam);
            eventArgs.Result = _GSM4500ViewModel.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task JournalGroupSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500DTO>(eventArgs.Data);
            loParam.CJRNGRP_CODE ??= string.Empty;
            loParam.CJRNGRP_NAME ??= string.Empty;
            await _GSM4500ViewModel.SaveJournalGroup(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _GSM4500ViewModel.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task JournalGroupdelete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500DTO>(eventArgs.Data);
            await _GSM4500ViewModel.DeleteJournalGroup(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    
    private void SetOther(R_SetEventArgs eventArgs)
    {
        _tabAccountSetting.Enabled = eventArgs.Enable;
        _enableFlag = eventArgs.Enable;
    }

    private async Task OnChangeParam(object value, string type)
    {
        var loEx = new R_Exception();
        var lcType = (string)value;
        try
        {
            if (type == "property") _GSM4500ViewModel.propertyValue = lcType;
            else if (type == "type") _GSM4500ViewModel.journalTypeValue = lcType;

            await _gridRefGSM04500.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        R_DisplayException(loEx);
    }

    private void AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        var loData = (GSM04500DTO)eventArgs.Data;
        loData.DCREATE_DATE = DateTime.Now;
        loData.DUPDATE_DATE = DateTime.Now;
    }
    
    private void OnChangeTab(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        _enableFlag = eventArgs.TabStripTab.Id == "JournalGroup";
    }

    private void BeforeOpenAccSetting(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSM04510);
        eventArgs.Parameter = _GSM4500ViewModel.loEntity;
    }
}