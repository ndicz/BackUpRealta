using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common.DTO;
using GSM00700Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace GSM00700Front
{
    public partial class GSM00720LocalAmount : R_Page
    {
        private GSM00720ViewModel _GSM00720ViewModel = new();
        private GSM00710ViewModel _GSM00710ViewModel = new();
        private R_Grid<GSM00710DTO> _gridRef00710;
        private R_Conductor _conductorRef;
        private R_Conductor R_conduct;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _GSM00720ViewModel.loCopyBaseAmountEntity = (GSM00720CopyBaseLocalAmountDTO)poParameter;
                _GSM00710ViewModel.CashFlowGroupCode = _GSM00720ViewModel.loCopyBaseAmountEntity.CCASH_FLOW_CODE;
                _GSM00710ViewModel.CashFlowGroupName = _GSM00720ViewModel.loCopyBaseAmountEntity.CCASH_FLOW_NAME;
                _GSM00720ViewModel.Year = _GSM00720ViewModel.loCopyBaseAmountEntity.CYEAR;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeLookupReplacement(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {       
                eventArgs.Parameter = new GSL01700DTOParameter()
             
                {
                    
                    CCOMPANY_ID = _GSM00720ViewModel.loCopyBaseAmountEntity.CCOMPANY_ID,
                    CUSER_ID = _GSM00720ViewModel.loCopyBaseAmountEntity.CUSER_ID,
                 
                };
                eventArgs.TargetPageType = typeof(GSL01700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
