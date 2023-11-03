using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APModel.ViewModel.APL00100;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_APFRONT
{
    public partial class APL00100 :R_Page
    {
        private LookupAPL00100ViewModel _viewModel = new LookupAPL00100ViewModel();
        private R_Grid<APL00100DTO> GridRef;
        private R_Conductor _conductorRef;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await Supplier_ServiceGetListRecord(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Supplier_ServiceGetListRecord(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (APL00100ParameterDTO)poParameter;
                await _viewModel.GetSupplierList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (APL00100ParameterDTO)eventArgs.Parameter;
                await _viewModel.GetSupplierList(loParam);

                eventArgs.ListEntityResult = _viewModel.SupplierGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickOkAsync()
        {
            var loData = GridRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }
}
