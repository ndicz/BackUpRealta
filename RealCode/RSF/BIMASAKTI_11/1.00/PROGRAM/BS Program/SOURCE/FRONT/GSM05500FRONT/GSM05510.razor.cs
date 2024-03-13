using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GSM05500Common.DTO;
using GSM05500Model;
using GSM05500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM05500Front
{
    public partial class GSM05510 : R_Page
    {
        private GSM05510ViewModel GSM05510ViewModel = new();
        private R_ConductorGrid _conGridGSM05510Ref;
        private R_Grid<GSM05510DTO> _gridRef5510;
        [Inject] private IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef5510.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_MODULE_NAME = "GS";

        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (GSM05510DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "GSM05500",
                        Table_Name = "GSM_RATETYPE",
                        Key_Value = string.Join("|", _clientHelper.CompanyId,
                            loData.CRATETYPE_CODE) // Example rcd|ASHMD|SUPP001
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "GSM05500",
                        Table_Name = "GSM_RATETYPE",
                        Key_Value = string.Join("|", _clientHelper.CompanyId,
                            loData.CRATETYPE_CODE) // Example rcd|ASHMD|SUPP001
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

        public async Task Grid_AfterDelete5510()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private async Task Grid_R_ServiceGetListRecordRateType(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await GSM05510ViewModel.GetRateTypeList();
                eventArgs.ListEntityResult = GSM05510ViewModel.loGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecordRateType(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM05510DTO)eventArgs.Data;
                await GSM05510ViewModel.GetRateTypeId(loParam.CRATETYPE_CODE);
                eventArgs.Result = GSM05510ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task Grid_ServiceSaveRateType(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05510DTO)eventArgs.Data;
                await GSM05510ViewModel.SaveRateType(loParam, eventArgs.ConductorMode);

                eventArgs.Result = GSM05510ViewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_ServiceDeleteRateType(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05510DTO)eventArgs.Data;
                await GSM05510ViewModel.DeleteRateType(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05510DTO)eventArgs.Data;
                var Condition1 = loParam.CRATETYPE_CODE == null;
                var Condition2 = loParam.CRATETYPE_DESCRIPTION == null;
                var Condition3 = loParam.CRATETYPE_CODE == null && loParam.CRATETYPE_DESCRIPTION == null;
                //check loparam currency code = null or emptyCCURRENCY_CODE

                if (Condition1)
                {
                    await R_MessageBox.Show("Error", "You Must Fill Empty Field", R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }

                if (Condition2)
                {
                    await R_MessageBox.Show("Error", "You Must Fill Empty Field", R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }

                if (Condition3)
                {
                    await R_MessageBox.Show("Error", "You Must Fill Empty Field", R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}