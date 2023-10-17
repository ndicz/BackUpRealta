using LMM03700Common;
using LMM03700Common.DTO_s;
using LMM03700Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace LMM03700Front
{
    public partial class LMM03700Tab2 : R_ITabPage
    {
        private LMM03700ViewModel _viewTCGModel = new(); //ViewModel TenantClassGrp
        private LMM03710ViewModel _viewTCModel = new();//viewModel TenantClass
        private R_Conductor _conT2_TCGRef; //conductor grid TenantClassGrp tab 2
        private R_ConductorGrid _conTCRef; //conductor grid TenantClass tab 2
        private R_ConductorGrid _conTRef; //conductor grid Tenant tab 2
        private R_Grid<TenantClassificationGroupDTO> _gridT2_TCGRef; //gridref TenantClassGrp tab 2
        private R_Grid<TenantClassificationDTO> _gridTCRef; //gridref TenantClass tab 2
        private R_Grid<TenantDTO> _gridTRef; //gridref Tenant tab 2
        private R_Popup R_PopupCheck;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewTCGModel._propertyId = (string)poParameter;
                _viewTCModel._propertyId = (string)poParameter;
                await _gridT2_TCGRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task RefreshTabPageAsync(object poParam)
        {
            _viewTCGModel._propertyId = (string)poParam;
            _viewTCModel._propertyId = (string)poParam;
            await _gridT2_TCGRef.R_RefreshGrid(null);
        }

        #region Tab2-TenantClassificationGrp
        private async Task T2_TCG_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewTCModel.GetTenantClassGroupList();
                eventArgs.ListEntityResult = _viewTCModel.TenantClassGrpList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private async Task T2_TCG_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(eventArgs.Data);
                await _viewTCModel.GetTenantClassGroupRecord(loParam);
                eventArgs.Result = _viewTCModel.TenantClassiGrp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task T2_TCG_ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);
                _viewTCModel._tenantClassificationGroupId = loParam.CTENANT_CLASSIFICATION_GROUP_ID;
                await _gridTCRef.R_RefreshGrid(null);
                //await _gridTCRef.AutoFitAllColumnsAsync();
                //_viewTCModel.AssignedTenantList = null;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                throw;
            }
        }
        #endregion

        #region Tab2-TenantClassification
        private async Task T2_TC_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewTCModel.GetTenantClassList();
                eventArgs.ListEntityResult = _viewTCModel.TenantClassList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private async Task T2_TC_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);
                await _viewTCModel.GetTenantClassRecord(loParam);
                eventArgs.Result = _viewTCModel.TenantClass;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task T2_TC_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);
                await _viewTCModel.DeleteTenantClass(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task T2_TC_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);
                await _viewTCModel.SaveTenantClass(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewTCModel.TenantClass;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task T2_TC_ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loEntity = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);
                _viewTCModel._tenantClassificationId = loEntity.CTENANT_CLASSIFICATION_ID;
                await _gridTRef.R_RefreshGrid(null);
                //await _gridTRef.AutoFitAllColumnsAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Tab2-TenantList
        private async Task T2_T_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //_viewTCModel._tenantClassificationId = (string)eventArgs.Parameter;
                await _viewTCModel.GetAssignedTenantList();
                eventArgs.ListEntityResult = _viewTCModel.AssignedTenantList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void T2_T_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Result = R_FrontUtility.ConvertObjectToObject<TenantDTO>(_gridTRef.GetCurrentData());
                if (eventArgs.Result == null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Tab2-Assign Tenant
        private void R_Before_Open_PopupAssignTenant(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = new TenantGridPopupDTO()
            {
                CPROPERTY_ID = _viewTCModel._propertyId,
                CTENANT_CLASSIFICATION_GROUP_ID = _viewTCModel._tenantClassificationGroupId,
                CTENANT_CLASSIFICATION_ID = _viewTCModel._tenantClassificationId
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(PopupAssignTenant);
        }
        private async Task R_After_Open_PopupAssignTenant(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result == null)
                {
                    return;
                }
                var loResult = (List<SelectedTenantGridPopupDTO>)eventArgs.Result;
                var loSelectedResult = loResult.Where(obj => (bool)obj.GetType().GetProperty("LSELECTED").GetValue(obj)).ToList();

                var loAssignTenantParam = R_FrontUtility.ConvertCollectionToCollection<TenantGridPopupDTO>(loSelectedResult);
                if (loAssignTenantParam.Count > 0)
                {
                    await _viewTCModel.AssignTenantCategory(loAssignTenantParam.ToList());
                    await _gridTRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Tab2-Move Tenant
        private void R_Before_Open_Popup_MoveTenant(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = new TenantGridPopupDTO()
            {
                CPROPERTY_ID = _viewTCModel._propertyId,
                CTENANT_CLASSIFICATION_GROUP_ID = _viewTCModel._tenantClassificationGroupId,
                CTENANT_CLASSIFICATION_ID = _viewTCModel._tenantClassificationId,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(PopupMoveTenant);
        }
        private async Task R_After_Open_Popup_MoveTenant(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result == null)
                {
                    return;
                }
                _viewTCModel._tenantClassificationId = (string)eventArgs.Result;
                await _gridTCRef.R_RefreshGrid(null);
                //await _gridTCRef.AutoFitAllColumnsAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
