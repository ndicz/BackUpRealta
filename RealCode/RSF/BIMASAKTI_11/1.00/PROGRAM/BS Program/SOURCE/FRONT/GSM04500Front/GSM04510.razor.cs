using GSM04500Common.DTOs;
using GSM04500Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM04500Front;

public partial class GSM04510 : R_Page
{
    private GSM04510ViewModel _GSM4510ViewModel = new();
    private R_ConductorGrid _conGridGSM04510Ref;
    private R_Grid<GSM04510DTO> _gridRefGSM04510;
    
    private GSM04520ViewModel _GSM4520ViewModel = new();
    private R_ConductorGrid _conGridGSM04520Ref;
    private R_Grid<GSM04520DTO> _gridRefGSM04520;
    
    private R_Conductor _conductorRef;
    
    private bool _enableFlagAcc;
    private bool _enableFlagDept;

    
    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM4510ViewModel.Init(poParam);
            await _gridRefGSM04510.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task GetListRecordAccount(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM4510ViewModel.GetAccountSettingGridList();
            eventArgs.ListEntityResult = _GSM4510ViewModel.loGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private void BeforeOpenLookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
    {
        var loParam = new GSL00520ParameterDTO();
        loParam.CGOA_CODE = _GSM4510ViewModel.loEntity.CGOA_CODE;
        eventArgs.Parameter = loParam;
        eventArgs.TargetPageType = typeof(GSL00520);
    }

    private void AfterOpenLookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.Result == null) return;

            var loResult = (GSL00520DTO)eventArgs.Result;
            var loGetData = (GSM04510DTO)eventArgs.ColumnData;
            loGetData.CGLACCOUNT_NO = loResult.CGLACCOUNT_NO;
            loGetData.CGLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private void SetOther(R_SetEventArgs eventArgs)
    {
        _enableFlagDept = eventArgs.Enable;
    }
    
        private async Task R_Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (GSM04510DTO)eventArgs.Data;
            _enableFlagDept = loData.LDEPARTMENT_MODE;
            
            await _GSM4520ViewModel.Init(loData);
            await _gridRefGSM04520.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
        
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM04510DTO>(eventArgs.Data);

                await _GSM4510ViewModel.GetAccountSettingId(loParam);
                eventArgs.Result = _GSM4510ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM04510DTO>(eventArgs.Data);
                loParam.CGLACCOUNT_NO ??= string.Empty;
                await _GSM4510ViewModel.SaveJournalGroup(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _GSM4510ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        
        
        private async Task GetListRecordDept(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM4520ViewModel.GetDeptGridList();
                eventArgs.ListEntityResult = _GSM4520ViewModel.loGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        private void BeforeLookupDept(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            if (eventArgs.ColumnName == nameof(GSM04520DTO.CDEPT_CODE))
            {
                var loParam = new GSL00700ParameterDTO();
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00700);
            }
            else if (eventArgs.ColumnName == nameof(GSM04520DTO.CGL_ACCOUNT_NO))
            {
                var loParam = new GSL00520ParameterDTO();
                loParam.CGOA_CODE = _GSM4520ViewModel.loParentEntity.CGOA_CODE;
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00520);
            }
        }
    
        private void AfterLookupDept(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Result == null) return;

                if (eventArgs.ColumnName == nameof(GSM04520DTO.CDEPT_CODE))
                {
                    var loResult = (GSL00700DTO)eventArgs.Result;
                    var loGetData = (GSM04520DTO)eventArgs.ColumnData;
                    loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
                else if (eventArgs.ColumnName == nameof(GSM04520DTO.CGL_ACCOUNT_NO))
                {
                    var loResult = (GSL00520DTO)eventArgs.Result;
                    var loGetData = (GSM04520DTO)eventArgs.ColumnData;
                    loGetData.CGL_ACCOUNT_NO = loResult.CGLACCOUNT_NO;  
                    loGetData.CGLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        private void SetOtherDept(R_SetEventArgs eventArgs)
        {
            _enableFlagAcc = eventArgs.Enable;
        }
        
        private async Task ServiceGetRecordDept(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM04520DTO>(eventArgs.Data);
                await _GSM4520ViewModel.GetDeptID(loParam);
                eventArgs.Result = _GSM4520ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        private async Task ServiceSaveDept(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM04520DTO>(eventArgs.Data);
                loParam.CGL_ACCOUNT_NO ??= string.Empty;
                await _GSM4520ViewModel.SaveDept(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _GSM4520ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
            
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM04520DTO>(eventArgs.Data);
                await _GSM4520ViewModel.DeleteDept(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
}