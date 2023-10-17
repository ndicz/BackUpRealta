using LMM03700Common.DTO_s;
using LMM03700Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace LMM03700Front
{
    public partial class PopupMoveTenant : R_Page
    {
        private R_ConductorGrid _conTenantToMoveRef;
        private R_Grid<SelectedTenantGridPopupDTO> _Grid;

        private LMM03710ViewModel _viewModelTC = new LMM03710ViewModel();
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await TenantClassDetail(poParameter);//get detail
                await TenantClassForMoveTenant_GetList(poParameter);
                await _Grid.R_RefreshGrid(poParameter);//refresh grid param
                //get list tc for drowdown
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task TenantClassForMoveTenant_GetList(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (TenantGridPopupDTO)poParam;
                _viewModelTC._propertyId = loParam.CPROPERTY_ID;
                _viewModelTC._tenantClassificationGroupId = loParam.CTENANT_CLASSIFICATION_GROUP_ID;
                await _viewModelTC.GetTenantClassListForMove();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task TenantClassDetail(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(poParam);
                _viewModelTC._propertyId = loParam.CPROPERTY_ID;
                _viewModelTC._tenantClassificationGroupId = loParam.CTENANT_CLASSIFICATION_GROUP_ID;
                await _viewModelTC.GetTenantClassRecordForMove(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantGridPopupDTO>(eventArgs.Parameter);
                await _viewModelTC.GetTenantListToMove(loParam);
                eventArgs.ListEntityResult = _viewModelTC.TenantToMoveList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        
        #region ProcesButton
        public async Task Button_OnClickOkAsync()
        {
            await _conTenantToMoveRef.R_SaveBatch();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
        #endregion
        #region Save Batch
        private void R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            if (_viewModelTC._toTenantClassificationId == "" || _viewModelTC._toTenantClassificationId == null)
            {
                R_MessageBox.Show("", "Please select Tennat Classification Destination", R_eMessageBoxButtonType.OK);
                return;
            }
        }
        private async Task R_ServiceSaveBatchAsync(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (List<SelectedTenantGridPopupDTO>)eventArgs.Data;
                var loListIdTenantString = loData.Where(dto => dto.LSELECTED).Select(dto => dto.CTENANT_ID).ToList();
                _viewModelTC._fromTenantClassificationId = _viewModelTC.TenantClassForMoveTenant.CTENANT_CLASSIFICATION_ID;
                await _viewModelTC.MoveTenant(loListIdTenantString);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_AfterSaveBatchAsync(R_AfterSaveBatchEventArgs eventArgs)
        {
            await this.Close(true, _viewModelTC._toTenantClassificationId);
        }
        #endregion

        
    }
}
