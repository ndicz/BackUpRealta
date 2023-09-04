﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_LMCOMMON.DTOs;
using Lookup_LMModel.ViewModel;
using Lookup_LMModel.ViewModel.LML00200;
using Lookup_LMModel.ViewModel.LML00300;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_LMFRONT
{
    public partial class LML00300 : R_Page
    {
        private LookupLML00300ViewModel _viewModelLML00300 = new LookupLML00300ViewModel();
        private R_Grid<LML00300DTO> GridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await GridRef.R_RefreshGrid(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LML00300ParameterDTO)eventArgs.Parameter;
                await _viewModelLML00300.GetSupervisorList(loParam);
                eventArgs.ListEntityResult = _viewModelLML00300.SupervisorList;
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