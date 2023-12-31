﻿using System;
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
using R_BlazorFrontEnd.Controls.MessageBox;
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
                _GSM00720ViewModel.CashFlowPlanCode = _GSM00720ViewModel.loCopyBaseAmountEntity.CCASH_FLOW_CODE;
                _GSM00720ViewModel.CashFlowGroupCode = _GSM00720ViewModel.loCopyBaseAmountEntity.CCASH_FLOW_GROUP;
                _GSM00720ViewModel.CashFlowPlanName = _GSM00720ViewModel.loCopyBaseAmountEntity.CCASH_FLOW_NAME;
                _GSM00720ViewModel.Year = _GSM00720ViewModel.loCopyBaseAmountEntity.CYEAR;

                //await _gridRef00710.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private Task R_BeforeOpenLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL01700DTOParameter()

                {

                    CCOMPANY_ID = _GSM00720ViewModel.loCopyBaseAmountEntity.CCOMPANY_ID,
                    CUSER_ID = _GSM00720ViewModel.loCopyBaseAmountEntity.CUSER_ID,
                    CRATETYPE_CODE = _GSM00720ViewModel.loCurrency.CBASE_CURRENCY_CODE,

                };
                eventArgs.TargetPageType = typeof(GSL01700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);   
            }

            loEx.ThrowExceptionIfErrors();
            return Task.CompletedTask;
        }

        private void R_AfterOpenLookUp(R_AfterOpenLookupEventArgs EventArgs)
        {
            var loData = (GSL01700DTO)EventArgs.Result;
            if (loData == null)
                return;
            _GSM00720ViewModel.loCopyBaseAmountEntity.CCURENCY_RATE = loData.NLCURRENCY_RATE_AMOUNT.ToString();
            _GSM00720ViewModel.loCopyBaseAmountEntity.CCURRENCY_CODE = loData.CCURRENCY_CODE;


            //return Task.CompletedTask;
        }


        public async Task Button_OnClickProcessAsync()
        {
            var loEx = new R_Exception();
            var loData = _GSM00720ViewModel.loCopyBaseAmountEntity;
            try
            {

                _GSM00720ViewModel.CurrencyRate = _GSM00720ViewModel.loCopyBaseAmountEntity.CCURENCY_RATE;
                _GSM00720ViewModel.CurrencyCode = _GSM00720ViewModel.loCopyBaseAmountEntity.CCURRENCY_CODE;


                await _GSM00720ViewModel.LocalAmount();
                //await this.Close(true, loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            await this.Close(true, loData);
        }


        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

        public async Task OnChangedTo()
        {
            var loEx = new R_Exception();

            try
            {


                if (_GSM00720ViewModel.loCopyBaseAmountEntity.INO_PERIOD_FROM > _GSM00720ViewModel.loCopyBaseAmountEntity.INO_PERIOD_TO)
                {

                    await R_MessageBox.Show("", "Period To Must be Greater Than Period From", R_eMessageBoxButtonType.OK);

                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnChangedFrom()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _GSM00720ViewModel.loCopyBaseAmountEntity;
                if (_GSM00720ViewModel.loCopyBaseAmountEntity.INO_PERIOD_FROM != null && _GSM00720ViewModel.loCopyBaseAmountEntity.INO_PERIOD_TO <= 1)
                {
                    // "from" sudah memiliki angka sedangkan "to" belum, maka tidak ada validasi yang dilakukan
                }
                else if (_GSM00720ViewModel.loCopyBaseAmountEntity.INO_PERIOD_FROM != null && _GSM00720ViewModel.loCopyBaseAmountEntity.INO_PERIOD_TO != null)
                {
                    if (_GSM00720ViewModel.loCopyBaseAmountEntity.INO_PERIOD_FROM > _GSM00720ViewModel.loCopyBaseAmountEntity.INO_PERIOD_TO)
                    {
                        await R_MessageBox.Show("", "Period To Must be Greater Than Period From", R_eMessageBoxButtonType.OK);
                    }
                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }







    }
}
