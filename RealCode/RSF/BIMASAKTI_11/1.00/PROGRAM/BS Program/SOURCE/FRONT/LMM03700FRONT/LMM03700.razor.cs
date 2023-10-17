using LMM03700Common;
using LMM03700Common.DTO_s;
using LMM03700Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace LMM03700Front
{
    public partial class LMM03700 : R_Page
    {
        private LMM03700ViewModel _viewTCGModel = new();
        private LMM03710ViewModel _viewTCModel = new();

        private R_ConductorGrid _conT1_TCGRef; //conductor grid tenantclassgrp tab 1
        private R_ConductorGrid _conT2_TCGRef; //conductor grid tenantclassgrp tab 2
        private R_ConductorGrid _conTCRef; //conductor grid tenantclass tab 2
        private R_ConductorGrid _conTRef; //conductor grid tenant tab 2

        private R_Grid<TenantClassificationGroupDTO> _gridT1_TCGRef; //gridref  tenantclassgrp tab 1 
        private R_Grid<TenantClassificationGroupDTO> _gridT2_TCGRef; //gridref tenantclassgrp tab 2
        private R_Grid<TenantClassificationDTO> _gridTCRef; //gridref tenantclassgrp tab 2
        private R_Grid<TenantDTO> _gridTRef; //gridref tenantclassgrp tab 2

        private R_Popup R_PopupCheck;

        private R_TabPage _tab2TenantClass; //refTabPageTab2
        private R_TabStrip _tabStrip; //refTabstrip

        public bool _pageTenantClassOnCRUDmode = false; //to disable moving tab while crudmode
        private bool _comboboxPropertyEnabled = true; //to disable combobox while crudmode

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await Property_ServiceGetListRecord(null);
                await _gridT1_TCGRef.R_RefreshGrid(null); //refresh grid tab 1
                //await _gridT2_TCGRef.R_RefreshGrid(null); //refresh grid tab 2
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        #region PropertyDropdown
        private async Task Property_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewTCGModel.GetPropertyList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private async void ComboboxPropertyOnChanged()
        {
            if (_conT1_TCGRef.R_ConductorMode == R_eConductorMode.Normal)
            {
                _viewTCModel._propertyId = _viewTCGModel._propertyId; //assign property_id as param grid
                await _gridT1_TCGRef.R_RefreshGrid(null); //refresh grid tab 1

                if (_tabStrip.ActiveTab.Id == "TC")
                {
                    await _tab2TenantClass.InvokeRefreshTabPageAsync(_viewTCModel._propertyId);
                }
            }
        }
        #endregion

        #region TabPage
        private void R_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LMM03700Tab2);
            eventArgs.Parameter = _viewTCGModel._propertyId;
        }
        private void R_After_Open_TabPage(R_AfterOpenTabPageEventArgs eventArgs)
        {

        }
        private void R_TabEventCallback(object poValue)
        {
            _comboboxPropertyEnabled = (bool)poValue;
            _pageTenantClassOnCRUDmode = !(bool)poValue;
        }
        #endregion

        #region TabSet
        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageTenantClassOnCRUDmode;
        }
        #endregion

        #region Tab1-TenantClassificationGroup
        private async Task TCG_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewTCGModel.GetTenantClassGroupList();
                eventArgs.ListEntityResult = _viewTCGModel.TenantClassificationGroupList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private async Task TCG_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(eventArgs.Data);
                await _viewTCGModel.GetTenantClassGroupRecord(loParam);
                eventArgs.Result = _viewTCGModel.TenantClassificationGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TCG_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(eventArgs.Data);
                await _viewTCGModel.DeleteTenantClassGroup(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task TCG_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(eventArgs.Data);
                await _viewTCGModel.SaveTenantClassGroup(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewTCGModel.TenantClassificationGroup;
                //await _gridT2_TCGRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void TGC_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (TenantClassificationGroupDTO)eventArgs.Data;

                if (string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_GROUP_ID))
                    loEx.Add("", "Please fill Tenant Classification Group Id ");

                if (string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_GROUP_NAME))
                    loEx.Add("", "Please fill Tenant Classification Group Name ");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.HasError)
                eventArgs.Cancel = true;

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

    }

}
