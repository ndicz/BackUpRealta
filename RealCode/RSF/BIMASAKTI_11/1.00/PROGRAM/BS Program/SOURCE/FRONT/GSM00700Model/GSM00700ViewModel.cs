using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GSM00700Common.DTO;
using GSM00700Common.DTO.Report_DTO_GSM00700;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Enums;
using R_CommonFrontBackAPI;

namespace GSM00700Model
{
    public class GSM00700ViewModel : R_ViewModel<GSM00700DTO>
    {
        private Model.GSM00700Model _GSM00700Model = new Model.GSM00700Model();
        public ObservableCollection<GSM00700DTO> loGridList = new ObservableCollection<GSM00700DTO>();
        public GSM00700DTO loEntity = new GSM00700DTO();
        public GSM00700PrintCashFlowParameterDTo loPrint = new GSM00700PrintCashFlowParameterDTo();

        public List<GSM00700CashFlowGroupTypeDTO> loCashFlowGroupType { get; set; } = new List<GSM00700CashFlowGroupTypeDTO>();
        //{
        //    new GSM00700CashFlowGroupTypeDTO() { CCODE = "I", CDESCRIPTION = "Investing"},
        //    new GSM00700CashFlowGroupTypeDTO() { CCODE = "O", CDESCRIPTION = "Operating"},
        //    new GSM00700CashFlowGroupTypeDTO() { CCODE = "F", CDESCRIPTION = "Financing"}
        // };
        public List<GSM00700PrintCashFlowParameterDTo> Period { get; set; } = new List<GSM00700PrintCashFlowParameterDTo>()
        {
            new GSM00700PrintCashFlowParameterDTo() {Period = "00"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "01"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "02"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "03"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "04"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "05"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "06"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "07"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "08"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "09"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "10"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "11"},
            new GSM00700PrintCashFlowParameterDTo() {Period = "12"}
        };
        //COMBO BOX
        public string CashFlowTyp = ""; // for filter


        public async Task GetCashFlowGroupTypeList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GSM00700Model.GetCashFlowGroupTypeAsync();
                loCashFlowGroupType = loResult.Data;
                CashFlowTyp = loCashFlowGroupType[0].CCODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        //CRUD ALL

        public async Task GetCashFlowGroupList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _GSM00700Model.GetAllCashFlowGroupStreamAsync();
                loGridList = new ObservableCollection<GSM00700DTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCashFlowGroupId(string cashFlowCode, string cashFlowName)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new GSM00700DTO() { CCASH_FLOW_GROUP_CODE = cashFlowCode, CCASH_FLOW_GROUP_NAME = cashFlowName };
                var loResult = await _GSM00700Model.R_ServiceGetRecordAsync(loParam);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveCashFlowGroup(GSM00700DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM00700DTO loResult = null;
            try
            {
                loResult = await _GSM00700Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteCashFlowGroup(GSM00700DTO poProperty)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new GSM00700DTO
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CCASH_FLOW_GROUP_CODE = poProperty.CCASH_FLOW_GROUP_CODE,
                    CCASH_FLOW_GROUP_NAME = poProperty.CCASH_FLOW_GROUP_NAME,
                    CCASH_FLOW_GROUP_TYPE = poProperty.CCASH_FLOW_GROUP_TYPE,
                    CUSER_ID = poProperty.CUSER_ID,

                };
                await _GSM00700Model.R_ServiceDeleteAsync(poProperty);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}