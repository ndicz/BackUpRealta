using BlazorClientHelper;
using GSM10500Common.DTO;
using GSM10500Model;
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

namespace GSM10500Front;

public partial class GSM10500 : R_Page
{
    private GSM10500ViewModel _viewModelGSM10500 = new();
    private R_ConductorGrid _conGridGSM10500Ref;
    private R_Grid<GSM10500DTO> _gridRef10500;

    private GSM10510ViewModel _viewModelGSM10510 = new();
    private R_ConductorGrid _conGridGSM10510Ref;
    private R_Grid<GSM10510DTO> _gridRef10510;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModelGSM10500.GetRoundingMode();
            await _gridRef10500.R_RefreshGrid(null);
            // await _gridRef10510.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region GSM10500

    private async Task Grid_R_ServiceGetListRecordAgeingHD(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelGSM10500.GetAgeingHDList();
            eventArgs.ListEntityResult = _viewModelGSM10500.loGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task Grid_ServiceGetRecordAgeingHD(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM10500DTO)eventArgs.Data;
            await _viewModelGSM10500.GetAgeingHDID(loParam.CAGEING_CODE);
            eventArgs.Result = _viewModelGSM10500.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task Grid_ServiceSaveAgeingHD(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM10500DTO)eventArgs.Data;
            await _viewModelGSM10500.SaveAgeingHD(loParam, eventArgs.ConductorMode);

            eventArgs.Result = _viewModelGSM10500.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task Grid_ServiceDeleteAgeingHD(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM10500DTO)eventArgs.Data;
            await _viewModelGSM10500.DeleteAgeingHD(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_R_DisplaytAgeingHD(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM10500DTO>(eventArgs.Data);
                _viewModelGSM10510.ageingCode = loParam.CAGEING_CODE;
                //await GSM00720ViewModel.GetCashFlowPlanList(GSM00710ViewModel.CashFlowGroupCode);
                await _viewModelGSM10510.GetAgeingDTList();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        var GridType = (GSM10500DTO)eventArgs.Data;
        GridType.CROUNDING_MODE = _viewModelGSM10500.AgeingRound;
    }

    #endregion

    #region GSM10510

    private async Task Grid_R_ServiceGetListRecordAgeingDT(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelGSM10510.GetAgeingDTList();
            eventArgs.ListEntityResult = _viewModelGSM10510.loGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    public async Task Grid_ServiceGetRecordAgeingDT(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM10510DTO)eventArgs.Data;
            await _viewModelGSM10510.GetAgeingDTID(loParam.CAGEING_CODE, loParam.ICOLUMN_NO);
            eventArgs.Result = _viewModelGSM10510.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task Grid_ServiceSaveAgeingDT(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            
            var loParam = (GSM10510DTO)eventArgs.Data;
            loParam.CAGEING_CODE = _viewModelGSM10510.ageingCode;
            await _viewModelGSM10510.SaveAgeingDT(loParam, eventArgs.ConductorMode);

            eventArgs.Result = _viewModelGSM10510.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task Grid_ServiceDeleteAgeingDT(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM10510DTO)eventArgs.Data;
            await _viewModelGSM10510.DeleteAgeingDT(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion
    
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
            var loData = (GSM10500DTO)eventArgs.Data;

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
                    Program_Id = "GSM10500",
                    Table_Name = "GSM_AGEING_HD",
                    Key_Value = string.Join("|", clientHelper.CompanyId, loData.CAGEING_CODE)
                };

                loLockResult = await loCls.R_Lock(loLockPar);
            }
            else
            {
                var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                {
                    Company_Id = clientHelper.CompanyId,
                    User_Id = clientHelper.UserId,
                    Program_Id = "GSM10500",
                    Table_Name = "GSM_AGEING_HD",
                    Key_Value = string.Join("|", clientHelper.CompanyId, loData.CAGEING_CODE)
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