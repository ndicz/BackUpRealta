using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMI00100Common.DTO;
using LMI00100Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace LMI00100Front
{
    public partial class LMI00100 : R_Page
    {

        private LMI00100ViewModel _viewModel = new();
        private R_ConductorGrid _conGridLMI00100Ref;
        private R_Grid<LMI00100DTO> _gridRef00100;
        private R_Conductor _conductorRef;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await PropertyListData(null);
                await _gridRef00100.R_RefreshGrid(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }



        private async Task Grid_R_ServiceGetListRecordRateType(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetGridList();
                eventArgs.ListEntityResult = _viewModel.loGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }



        private async Task PropertyListData(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetProperty();
                //eventArgs.ListEntityResult = _viewModel.loGridListProperty;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task OnChanged(object poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;

            try
            {
                _viewModel.PropertyValue = lsProperty;
                await _gridRef00100.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
    }
}
