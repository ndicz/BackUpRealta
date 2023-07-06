using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSM05500Common.DTO;
using GSM05500Model;
using GSM05500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM05500Front
{
    public partial class GSM05510 : R_Page
    {
        private GSM05510ViewModel GSM05510ViewModel = new();
        private R_ConductorGrid _conGridGSM05510Ref;
        private R_Grid<GSM05510DTO> _gridRef5510;

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



    }
}

