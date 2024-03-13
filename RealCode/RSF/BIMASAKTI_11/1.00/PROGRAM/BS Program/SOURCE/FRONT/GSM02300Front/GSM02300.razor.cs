using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02300Common.DTO;
using GSM02300Model;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM02300Front
{
    public partial class GSM02300
    {
        private GSM02300ViewModel _GSM02300iewModel = new();
        private R_ConductorGrid _conGridGSM02300Ref;
        private R_Grid<GSM02300DTO> _gridRef02300;

        [Inject] private IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _GSM02300iewModel.GetPropertyType();
                await _gridRef02300.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Get_Record_GSM02300(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM02300iewModel.GetAllProperty();
                eventArgs.ListEntityResult = _GSM02300iewModel.loGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Service_Get_Record_GSM02300(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM02300DTO)eventArgs.Data;
                await _GSM02300iewModel.GetPropertyId(loParam.CPROPERTY_TYPE_CODE);
                eventArgs.Result = _GSM02300iewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_SaveGSM02300(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM02300DTO)eventArgs.Data;
                await _GSM02300iewModel.SaveProperty(loParam, eventArgs.ConductorMode);

                eventArgs.Result = _GSM02300iewModel.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Grid_DeleteProperty(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM02300DTO)eventArgs.Data;
                await _GSM02300iewModel.DeletePropertyType(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_CellValueChanged(R_CellValueChangedEventArgs eventArgs)
        {   
            var loEx = new R_Exception();
            var loData = _GSM02300iewModel.loGridListPropertyType;
            var loDataEntity = _GSM02300iewModel.loEntity;
           
            try
            {
                if (eventArgs.ColumnName == "CPROPERTY_TYPE_CODE")
                {
                    var loPropertyType = loData.FirstOrDefault(x => x.CCODE == eventArgs.Value.ToString());
                    if (loPropertyType != null)
                    {
                        var loContactNameColumn = eventArgs.Columns.FirstOrDefault(x => x.Name == "CPROPERTY_TYPE_NAME");
                        loContactNameColumn.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
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
                var loData = (GSM02300DTO)eventArgs.Data;

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
                        Program_Id = "GSM02300",
                        Table_Name = "GSM_PROPERTY_TYPE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_TYPE_CODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "GSM02300",
                        Table_Name = "GSM_PROPERTY_TYPE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_TYPE_CODE)
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
}