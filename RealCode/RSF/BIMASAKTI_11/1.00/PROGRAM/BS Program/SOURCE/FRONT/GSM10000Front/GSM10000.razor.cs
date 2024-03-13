using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM10000Common.DTO;
using GSM10000Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM10000Front;

public partial class GSM10000 : R_Page
{
    private GSM10000ViewModel _viewModel = new();
    private R_ConductorGrid _conGridGSM10000Ref;
    private R_Conductor _conductorRef;
    private R_Grid<GSM10000DTO> _gridRef10000;
    private string loLabel;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        try
        {
            await _gridRef10000.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_R_ServiceGetListRecordHoliday(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetHolidayList();
            eventArgs.ListEntityResult = _viewModel.loGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_R_DisplaytListCashFlow(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM10000DTO>(eventArgs.Data);
                if (loParam != null)
                {
                    _viewModel.holidayDate = loParam.CHOLIDAY_DATE;
                    //GSM00710ViewModel.csquence = loParam.CSEQUENCE.ToString();

                    await _viewModel.GetHolidayId(loParam.CHOLIDAY_DATE);

                    if (loParam.LACTIVE)
                    {
                        loLabel = "Inactive";
                        _viewModel.Data.LACTIVE = true;
                    }
                    else
                    {
                        loLabel = "Activate";
                        _viewModel.Data.LACTIVE = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_ServiceGetRecordHoliday(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = (GSM10000DTO)eventArgs.Data;
            await _viewModel.GetHolidayId(loParam.CHOLIDAY_DATE);
            eventArgs.Result = _viewModel.loEntity;
            // if (loParam.LACTIVE)
            // {
            //     loLabel = "Inactive";
            //     _viewModel.Data.LACTIVE = false;
            // }
            // else
            // {
            //     loLabel = "Activate";
            //     _viewModel.Data.LACTIVE = true;
            // }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_ServiceSaveHoliday(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM10000DTO)eventArgs.Data;
            await _viewModel.SaveHoliday(loParam, eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.loEntity;
            // await _gridRef10000.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task Grid_ServiceDeleteHoliday(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM10000DTO)eventArgs.Data;
            await _viewModel.DeleteHoliday(loParam);
            // await _gridRef10000.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task Grid_AfterSaveHoliday(R_AfterSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridRef10000.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
    {
        R_Exception loException = new R_Exception();
        try
        {
            var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
            loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                "LMM02001"; //Uabh Approval Code sesuai Spec masing masing
            await loValidateViewModel
                .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

            //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
            if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
            {
                await _viewModel.ActiveInactiveProcessAsync(); //Ganti jadi method ActiveInactive masing masing
                await _gridRef10000.R_RefreshGrid(null);
                return;
            }
            else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
            {
                eventArgs.Parameter = new GFF00900ParameterDTO()
                {
                    Data = loValidateViewModel.loRspActivityValidityList,
                    IAPPROVAL_CODE = "GSM10001" //Uabh Approval Code sesuai Spec masing masing
                };
                eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }

    private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
    {
        R_Exception loException = new R_Exception();
        try
        {
            if (eventArgs.Success == false)
            {
                return;
            }

            bool result = (bool)eventArgs.Result;
            if (result == true)
            {
                await _viewModel.ActiveInactiveProcessAsync();
                var loData = _gridRef10000.GetCurrentData();
                await _gridRef10000.R_SetCurrentData(loData);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }

    [Inject] private IClientHelper clientHelper { get; set; }
    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
    private const string DEFAULT_MODULE_NAME = "GS";

    protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        var llRtn = false;
        R_LockingFrontResult loLockResult = null;

        try
        {
            var loData = (GSM10000DTO)eventArgs.Data;

            var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);

            if (eventArgs.Mode == R_eLockUnlock.Lock)
            {
                var loLockPar = new R_ServiceLockingLockParameterDTO
                {
                    Company_Id = clientHelper.CompanyId,
                    User_Id = clientHelper.UserId,
                    Program_Id = "GSM10000",
                    Table_Name = "GSM_HOLIDAY",
                    Key_Value = string.Join("|", clientHelper.CompanyId, loData.CHOLIDAY_DATE)
                };

                loLockResult = await loCls.R_Lock(loLockPar);
            }
            else
            {
                var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                {
                    Company_Id = clientHelper.CompanyId,
                    User_Id = clientHelper.UserId,
                    Program_Id = "GSM10000",
                    Table_Name = "GSM_HOLIDAY",
                    Key_Value = string.Join("|", clientHelper.CompanyId, loData.CHOLIDAY_DATE)
                };


                loLockResult = await loCls.R_UnLock(loUnlockPar);
            }

            llRtn = loLockResult.IsSuccess;
            if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                throw loLockResult.Exception;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return llRtn;
    }
}