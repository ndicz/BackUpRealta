using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM05500Common.DTO;
using GSM05500Model.Model;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;



namespace GSM05500Model
{
    public class GSM05510ViewModel : R_ViewModel<GSM05510DTO>
    {
        private GSM05510Model _GSM05510Model = new GSM05510Model();

        public ObservableCollection<GSM05510DTO> loGridList = new ObservableCollection<GSM05510DTO>();
       

        public GSM05510DTO loEntity = new GSM05510DTO();

          public string PropertyValueContext = "";
        public async Task GetRateTypeList()
        {

            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05510Model.GetAllStreamAsync();
                loGridList = new ObservableCollection<GSM05510DTO>(loReturn.Data);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRateTypeId(string currencyCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM05510DTO() { CRATETYPE_CODE = currencyCode };
                var loResult = await _GSM05510Model.R_ServiceGetRecordAsync(loParam);

                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSM05510DTO> SaveRateType(GSM05510DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM05510DTO loResult = null;

            try
            {
                loResult = await _GSM05510Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task DeleteRateType(GSM05510DTO poProperty)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM05510DTO
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CRATETYPE_CODE = poProperty.CRATETYPE_CODE,
                    CRATETYPE_DESCRIPTION = poProperty.CRATETYPE_CODE,
                    CUSER_ID = poProperty.CUSER_ID,

                };
                await _GSM05510Model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
