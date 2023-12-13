using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Text;
using GSM02300Common.DTO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Enums;
using R_CommonFrontBackAPI;

namespace GSM02300Model
{
    public class GSM02300ViewModel : R_ViewModel<GSM02300DTO>
    {
        private Model.GSM02300Model _GSM02300Model = new Model.GSM02300Model();
        public ObservableCollection<GSM02300DTO> loGridList = new ObservableCollection<GSM02300DTO>();
        public ObservableCollection<GSM02300PropertyTypeDTO> loGridListPropertyType = new ObservableCollection<GSM02300PropertyTypeDTO>();

        //public List<GSM00710CashFlowTypeDTO> loCashFlowType { get; set; } = new List<GSM00710CashFlowTypeDTO>();

        public GSM02300DTO loEntity = new GSM02300DTO();


        public async Task GetAllProperty()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM02300Model.GetAllPropertyStreamAsync();
                loGridList = new ObservableCollection<GSM02300DTO>(loReturn.Data);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyType()
        {

            var loEx = new R_Exception();
            try
            {
                var loResult = await _GSM02300Model.GetAllPropertyTypeStreamAsync();
                loGridListPropertyType = new ObservableCollection<GSM02300PropertyTypeDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyId(string propertyCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM02300DTO() { CPROPERTY_TYPE_CODE = propertyCode };
                var loResult = await _GSM02300Model.R_ServiceGetRecordAsync(loParam);

                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveProperty(GSM02300DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM02300DTO loResult = null;
           
            try
            {

                loResult = await _GSM02300Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                loEntity = loResult;
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeletePropertyType(GSM02300DTO poProperty)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new GSM02300DTO
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CPROPERTY_TYPE_NAME = poProperty.CPROPERTY_TYPE_NAME,
                    CPROPERTY_TYPE_CODE = poProperty.CPROPERTY_TYPE_CODE,
                    LSINGLE_UNIT = poProperty.LSINGLE_UNIT,
                    LUSE_PRICE_LIST = poProperty.LUSE_PRICE_LIST,
                    CUSER_ID = poProperty.CUSER_ID,

                };
                await _GSM02300Model.R_ServiceDeleteAsync(poProperty);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
