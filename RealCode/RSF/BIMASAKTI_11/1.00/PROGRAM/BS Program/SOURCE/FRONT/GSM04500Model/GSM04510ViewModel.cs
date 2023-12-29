using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GSM04500Common;
using GSM04500Common.DTOs;
using GSM04500Model.Model;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM04500Model
{
    public class GSM04510ViewModel : R_ViewModel<GSM04510DTO>
    {
        private GSM04510Model _GSM04510Model = new GSM04510Model();
        public ObservableCollection<GSM04510DTO> loGridList = new ObservableCollection<GSM04510DTO>();
        public GSM04500ViewModel _GSM04500ViewModel = new GSM04500ViewModel();
        public GSM04500DTO loParentEntity = new GSM04500DTO();
        public GSM04510DTO loEntity = new GSM04510DTO();

        public async Task Init(object poParam)
        {
            loParentEntity = (GSM04500DTO)poParam;
        }

        public async Task GetAccountSettingGridList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstanGSM04500.CPROPERTY_ID, loParentEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstanGSM04500.CJOURNAL_GROUP_TYPE, loParentEntity.CJRNGRP_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstanGSM04500.CJOURNAL_GROUP_CODE, loParentEntity.CJRNGRP_CODE);
                var loReturn = await _GSM04510Model.GetallJournalGroupGOAListStreamAsync();
                loGridList = new ObservableCollection<GSM04510DTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetAccountSettingId( GSM04510DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04510Model.R_ServiceGetRecordAsync(poParam);

                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task SaveJournalGroup(GSM04510DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CPROPERTY_ID = loParentEntity.CPROPERTY_ID;
                poNewEntity.CJOURNAL_GROUP_TYPE = loParentEntity.CJRNGRP_CODE;
                loEntity = await _GSM04510Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}