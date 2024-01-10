using BlazorClientHelper;
using LMM01500Common.DTOs;
using LMM01500Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace LMM01500Front;

public partial class LMM01500 : R_Page
{
    private LMM01500ViewModel _viewModel = new();
    private R_Grid<LMM01500GeneralInfoDTO> _gridRef;
    private R_Conductor _conductor;
    [Inject] IClientHelper clientHelper { get; set; }
    private bool isInvoiceGroup;
    private bool textBoxDueDays = true;
    private bool textBoxFixedDueDays;
    private bool textBoxRangeFixedDueDays;

    private R_Lookup R_Lookup_Additional;
    private R_Lookup R_Lookup_DEPT;
    private R_Lookup R_Lookup_BANK;
    private R_Lookup R_Lookup_BANKACC;
    private string loLabel = "Activate";

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        try
        {
            await InitProperty(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task InitProperty(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetInitialPropertyList();
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }


    private async Task PropertyOnchanged(string poParam)
    {
        var loException = new R_Exception();
        string lsProperty = (string)poParam;
        try
        {
            _viewModel.PropertyType = lsProperty;
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        R_DisplayException(loException);
    }


    private async Task GetInvoiceGroupList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetInvoiceGroupList();
            eventArgs.ListEntityResult = _viewModel.GridList;
            if (_viewModel.GridList.Count < 1)
            {
                //disable when, unit type didn't have data
                _viewModel.InvoiceGroupValue = "";
                // _viewModel._IsButtonAddEnable = false;
                // await _gridInvoiceGroupValueRef.R_RefreshGrid(null);
            }
            else
            {
                // _viewModel._IsButtonAddEnable = true;
                _viewModel.InvoiceGroupValue = _viewModel.GridList[0].CINVGRP_CODE;
                // await _gridInvoiceGroupRef.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task ServiceRecordInvoiceGroup(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<LMM01500GeneralInfoDTO>(eventArgs.Data);
            await _viewModel.GetInvoiceGroupDetail(loParam);
            eventArgs.Result = _viewModel.InvoiceGroupDetail;

            /*
               //Set ActiveInactive Value
            BillingRuleViewModel.ActiveInactiveEntity.PROPERTY_ID = temp.CPROPERTY_ID;
            BillingRuleViewModel.ActiveInactiveEntity.CUNIT_TYPE_ID = temp.CUNIT_TYPE_ID;
            BillingRuleViewModel.ActiveInactiveEntity.CBILLING_RULE_CODE = temp.CBILLING_RULE_CODE;

            if (loParam.LACTIVE)
            {
                loLabel = "Inactive";
                BillingRuleViewModel.ActiveInactiveEntity.LACTIVE = false;
            }
            else
            {
                loLabel = "Activate";
                BillingRuleViewModel.ActiveInactiveEntity.LACTIVE = true;
            }
             */
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        //if (eventArgs.ConductorMode == R_eConductorMode.Normal)
        //{
        //    //Enable button add, when user click another unit type
        //    //_viewModel._IsButtonAddEnable = true;

        //var loParam = (LMM01500InvoiceGroupDTO)eventArgs.Data;
        //_viewModel.PropertyValueContext = loParam.CINVGRP_CODE;
        //_viewModel.InvoiceGroup = loParam.CINVGRP_CODE;
        //}
    }
    
    private async Task R_ServiceSaveGeneralInfo(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Save_InvoiceGroup((LMM01500GeneralInfoDTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

            eventArgs.Result = _viewModel.InvoiceGroupDetail;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    #region Radio Button


    private async Task OnChangedRadioBtnDueMode(object poParam)
    {
        var loEx = new R_Exception();
        try
        {
            isInvoiceGroup = poParam.ToString() == "02";
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task OnChangedRadioBtnGrpMode(object poParam)
    {
        var loEx = new R_Exception();
        try
        {
            string typeGroupMode = (string)poParam;

            textBoxDueDays = (typeGroupMode == "01");
            textBoxFixedDueDays = (typeGroupMode == "02");
            textBoxRangeFixedDueDays = (typeGroupMode == "03");
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    #endregion

    private void BeforeOpenLookUp_Additional(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL01400ParameterDTO()
        {
            CCOMPANY_ID = clientHelper.CompanyId,
            CPROPERTY_ID = _viewModel.PropertyType,
            CCHARGES_TYPE_ID = "A"
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL01400);
    }

    private void AfterOpenLookUp_Additional(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL01400DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (LMM01500GeneralInfoDTO)_conductor.R_GetCurrentData();

        loGetData.CSTAMP_ADD_ID = loTempResult.CCHARGES_ID;
        loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;

    }
    
    private void Department_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
    {

        var param = new GSL00700ParameterDTO()
        {
            CCOMPANY_ID = clientHelper.CompanyId,
            CUSER_ID = clientHelper.UserId
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00700);
    }
    private void Department_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00700DTO)eventArgs.Result;
        if (loTempResult == null)
        {
            return;
        }

        _viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
        _viewModel._selectedDeptCode = loTempResult.CDEPT_CODE;
        _viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;

        //GeneralButtonEnable = !string.IsNullOrEmpty(_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_viewModel.Data.CBANK_CODE);
    }

    private void BANK_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
    {

        var param = new GSL01200ParameterDTO()
        {
            CCOMPANY_ID = clientHelper.CompanyId,
            CBANK_TYPE = "",
            CCB_TYPE = "B",
            CUSER_ID = clientHelper.UserId
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL01200);
    }
    private void BANK_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL01200DTO)eventArgs.Result;
        if (loTempResult == null)
        {
            return;
        }
        _viewModel.Data.CBANK_CODE = loTempResult.CCB_CODE;
        _viewModel._selectedBank = loTempResult.CCB_CODE;
        _viewModel.Data.CBANK_NAME = loTempResult.CCB_NAME;
        //GeneralButtonEnable = !string.IsNullOrEmpty(_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_viewModel.Data.CBANK_CODE);
    }
    
    private void BANKACCOUNT_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL01300ParameterDTO()
        {
            CCOMPANY_ID = clientHelper.CompanyId,
            CBANK_TYPE = "B",
            CCB_CODE = _viewModel._selectedBank,
            CDEPT_CODE = _viewModel._selectedDeptCode
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL01300);
    }
    private void BANKACCOUNT_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL01300DTO)eventArgs.Result;
        if (loTempResult == null)
        {
            return;
        }
        _viewModel.Data.CBANK_ACCOUNT = loTempResult.CCB_ACCOUNT_NO;
        //GeneralButtonEnable = !string.IsNullOrEmpty(_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_viewModel.Data.CBANK_CODE);
    }
    
    private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Doc };

    private async Task InputFileChangeLMM01500GeneralInfo(InputFileChangeEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (LMM01500GeneralInfoDTO)_conductor.R_GetCurrentData();
            if (loData != null)
            {
                //if TRUE OVERRIDE DATA EXIST

                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loData.OData = loMS.ToArray();
                loData.CFileNameExtension = eventArgs.File.Name;
                loData.CFileExtension = Path.GetExtension(eventArgs.File.Name);
                loData.CFileName = Path.GetFileNameWithoutExtension(eventArgs.File.Name);

                loData.LINVOICE_TEMPLATE = true;
                loData.CINVOICE_TEMPLATE = loData.CFileName + "." + loData.CFileExtension;

            }
        }
        // Set Data
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);

    }
    
    private R_TabPage _tabPageTempalate;
    //CHANGE TAB
    private void Before_Open_TabTemplate(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        // eventArgs.TargetPageType = typeof(LMM01500InvoiceGroupDept);
        // if (_viewModel.GridList.Count > 0)
        // {
        //     eventArgs.Parameter = _viewModel.InvoiceParam;
        // }
        // else
        // {
        //     eventArgs.Parameter = null;
        // }
    }
    private void onTabChange(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
    }

    private R_TabPage _tabPageCharges;
    //CHANGE TAB
    private void Before_Open_TabCharges(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        // eventArgs.TargetPageType = typeof(LMM01500Charges);
        // if (_viewModel.GridList.Count > 0)
        // {
        //     eventArgs.Parameter = _viewModel.InvoiceParam;
        // }
        // else
        // {
        //     eventArgs.Parameter = null;
        // }
    }
    
    private async Task ServiceBeforeEdit(R_BeforeEditEventArgs eventArgs)
    {
        isInvoiceGroup = _viewModel.InvoiceGroupDetail.CINVOICE_DUE_MODE == "02";
    }
    private async Task ServiceBeforeCancel(R_BeforeCancelEventArgs eventArgs)
    {
        isInvoiceGroup = false;
        _viewModel.InvoiceDueModeValue = _viewModel.InvoiceGroupDetail.CINVOICE_DUE_MODE;
    }
}