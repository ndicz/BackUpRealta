using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM04500Model
{
    public class GSM04500ViewModel : R_ViewModel<GSM04500DTO>
    {
        private Model.GSM04500Model _GSM04500Model = new Model.GSM04500Model();
        
        public ObservableCollection<GSM04500DTO> loGridList = new ObservableCollection<GSM04500DTO>();
        public ObservableCollection<GSM04500PropertyDTO> loGridProperty = new ObservableCollection<GSM04500PropertyDTO>();
        public ObservableCollection<GSM04500JournalGroupTypeDTO> loGridJournalType = new ObservableCollection<GSM04500JournalGroupTypeDTO>();

        public List<GSM04500JournalGroupTypeDTO> JournalTypeList { get; set; } = new List<GSM04500JournalGroupTypeDTO>(); 
        public List<GSM04500PropertyDTO> PropertyList { get; set; } = new List<GSM04500PropertyDTO>();

        public GSM04500DTO loEntity = new GSM04500DTO();
        public GSM04500PropertyDTO loPropertyEntity = new GSM04500PropertyDTO();
        public GSM04500JournalGroupTypeDTO loJournalTypeEntity = new GSM04500JournalGroupTypeDTO();
        public string propertyValue = "";
        public string journalTypeValue = "";
        public async Task GetJournalGroupListStream()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstanGSM04500.CPROPERTY_ID, propertyValue);
                R_FrontContext.R_SetStreamingContext(ContextConstanGSM04500.CJOURNAL_GROUP_TYPE, journalTypeValue);
                var loReturn = await _GSM04500Model.GetallJournalGroupListStreamAsync();
                loGridList = new ObservableCollection<GSM04500DTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyStream()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _GSM04500Model.GetPropertyStreamAsync();
                PropertyList = loReturn.Data;
                propertyValue = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        
        public async Task GetJournalTypeListStream()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _GSM04500Model.GetJournalGroupTypeStreamAsync();
                JournalTypeList = loReturn.Data;
                journalTypeValue = JournalTypeList[0].CCODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalGroupId( GSM04500DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04500Model.R_ServiceGetRecordAsync(poParam);

                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task SaveJournalGroup(GSM04500DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CPROPERTY_ID = propertyValue;
                poNewEntity.CJRNGRP_TYPE = journalTypeValue;
                loEntity = await _GSM04500Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task DeleteJournalGroup(GSM04500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM04500Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
    }
}