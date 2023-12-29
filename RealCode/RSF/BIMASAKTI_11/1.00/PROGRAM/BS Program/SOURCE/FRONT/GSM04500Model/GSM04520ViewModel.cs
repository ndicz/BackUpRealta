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
    public class GSM04520ViewModel : R_ViewModel<GSM04520DTO>
    {
        private GSM04520Model _GSM04520Model = new GSM04520Model();
        public ObservableCollection<GSM04520DTO> loGridList = new ObservableCollection<GSM04520DTO>();
        public GSM04510ViewModel _GSM04500ViewModel = new GSM04510ViewModel();
        public GSM04520DTO loEntity = new GSM04520DTO();
        public GSM04510DTO loParentEntity = new GSM04510DTO();
        
        public async Task Init(object poParam)
        {
            loParentEntity = (GSM04510DTO)poParam;
        }
        
        public async Task GetDeptGridList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstanGSM04500.CPROPERTY_ID, loParentEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstanGSM04500.CJOURNAL_GROUP_TYPE, loParentEntity.CJRNGRP_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstanGSM04500.CJOURNAL_GROUP_CODE, loParentEntity.CJRNGRP_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstanGSM04500.CGOA_CODE, loParentEntity.CGOA_CODE);
                var loReturn = await _GSM04520Model.GetallJournalGroupGOADeptListStreamAsync();
                loGridList = new ObservableCollection<GSM04520DTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDeptID(GSM04520DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                
                var loReturn = await _GSM04520Model.R_ServiceGetRecordAsync(poParam);
                loEntity = loReturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveDept(GSM04520DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CPROPERTY_ID = loParentEntity.CPROPERTY_ID;
                poNewEntity.CJRNGRP_TYPE = loParentEntity.CJRNGRP_TYPE;
                poNewEntity.CJRNGRP_CODE = loParentEntity.CJRNGRP_CODE;
                poNewEntity.CGOA_CODE = loParentEntity.CGOA_CODE;
                loEntity = await _GSM04520Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteDept(GSM04520DTO poNewEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM04520Model.R_ServiceDeleteAsync(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}