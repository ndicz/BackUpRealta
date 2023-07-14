using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common.DTO;
using GSM00700Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace GSM00700Front
{
    public partial class GSM00720CopyFrom : R_Page
    {
        private GSM00720ViewModel _GSM00720ViewModel = new();
        private R_Conductor _conductorRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _GSM00720ViewModel.loCopyFromEntity = (GSM00720CopyFromYearDTO)poParameter;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private Task Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = _GSM00720ViewModel.loCopyFromEntity;
            eventArgs.TargetPageType = typeof(GSM00720CopyFromYearDTO);
            return Task.CompletedTask;
        }

        private Task After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loData = (GSM00720CopyFromYearDTO)eventArgs.Result;
            _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_CODE = loData.CFROM_CASH_FLOW_CODE;
            _GSM00720ViewModel.loCopyFromEntity.CFROM_CASH_FLOW_FLAG = loData.CFROM_CASH_FLOW_FLAG;
            _GSM00720ViewModel.loCopyFromEntity.CFROM_YEAR = loData.CFROM_YEAR;
            _GSM00720ViewModel.loCopyFromEntity.CTO_CASH_FLOW_CODE = loData.CTO_CASH_FLOW_CODE;
            _GSM00720ViewModel.loCopyFromEntity.CTO_YEAR = loData.CTO_YEAR;

            return Task.CompletedTask;
        }


    }

}
