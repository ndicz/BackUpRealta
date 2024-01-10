using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LMM01500Common;
using LMM01500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMM01500Model
{
    public class LMM01500ViewModel : R_ViewModel<LMM01500GeneralInfoDTO>

    {
        private LMM01500Model _model = new LMM01500Model();
        public List<LMM01500InitialProcessDTO> PropertyList { get; set; } = new List<LMM01500InitialProcessDTO>();
        public ObservableCollection<LMM01500GeneralInfoDTO> GridList { get; set; } = new ObservableCollection<LMM01500GeneralInfoDTO>();
        public LMM01500TabParamDTO InvoiceParam { get; set; } = new LMM01500TabParamDTO();
        public LMM01500GeneralInfoDTO InvoiceGroupDetail { get; set; } = new LMM01500GeneralInfoDTO();
        
        
        public string PropertyType = ""; 
        public string InvoiceGroupValue = "";
        public string _selectedBank="";
        public string _selectedDeptCode = "";
        public List<LMM01500GeneralInfoDTO> InvoiceDueMode { get; set; } = new List<LMM01500GeneralInfoDTO>
        { new LMM01500GeneralInfoDTO { CODE = "01", DESC = "Tenant" },
            new LMM01500GeneralInfoDTO { CODE = "02", DESC = "Invoice Group" } };
        
        public List<LMM01500GeneralInfoDTO> InvoiceGroupMode { get; set; } = new List<LMM01500GeneralInfoDTO>
        { new LMM01500GeneralInfoDTO { CODE = "01", DESC = "Due Days" },
            new LMM01500GeneralInfoDTO { CODE = "02", DESC = "Fix Due Date" },
            new LMM01500GeneralInfoDTO { CODE = "03", DESC = "Range Fix Due Date" } };

        public string InvoiceDueModeValue = "01";
        public string InvoiceGroupModeValue = "01";

        public async Task GetInitialPropertyList()
        {
            var loException = new R_Exception();
            try
            {
                var loResult = await _model.GetInitialProcessPropertyStream();
                PropertyList = loResult.Data;
                PropertyType = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        
        public async Task GetInvoiceGroupList()
        {
            var loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantLMM01500.CPROPERTY_ID, PropertyType);
                var loResult = await _model.GetInvoiceGroupListStreamAsync();
                GridList = new ObservableCollection<LMM01500GeneralInfoDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        
        public async Task<LMM01500GeneralInfoDTO> GetInvoiceGroupDetail(LMM01500GeneralInfoDTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01500GeneralInfoDTO loResult = new LMM01500GeneralInfoDTO();
            try
            {
                var loParam = new LMM01500GeneralInfoDTO()
                {
                    CCOMPANY_ID = poEntity.CCOMPANY_ID,
                    CUSER_ID = poEntity.CUSER_ID,
                    CPROPERTY_ID = PropertyType,
                    CINVGRP_CODE = poEntity.CINVGRP_CODE,
                };
                loResult = await _model.R_ServiceGetRecordAsync(loParam);

                //set value for param
                InvoiceParam.CPROPERTY_ID = loResult.CPROPERTY_ID;
                InvoiceParam.CINVGRP_CODE = loResult.CINVGRP_CODE;

                //set value to enable disable group box on detail
                InvoiceDueModeValue = loResult.CINVOICE_DUE_MODE;

                InvoiceGroupDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task Save_InvoiceGroup(LMM01500GeneralInfoDTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    poEntity.CPROPERTY_ID = PropertyType;
                    poEntity.CINVOICE_DUE_MODE = InvoiceDueModeValue;
                    poEntity.CINVOICE_GROUP_MODE = InvoiceGroupModeValue;
                }

                var loResult = await _model.R_ServiceSaveAsync(poEntity, peCRUDMode);
                InvoiceGroupDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}