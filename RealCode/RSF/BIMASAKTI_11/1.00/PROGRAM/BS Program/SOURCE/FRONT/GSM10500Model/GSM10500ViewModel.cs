using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GSM10500Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM10500Model
{
    public class GSM10500ViewModel  : R_ViewModel<GSM10500DTO>
    {
        private Model.GSM10500Model _GSM10500Model = new Model.GSM10500Model();
        public ObservableCollection<GSM10500DTO> loGridList = new ObservableCollection<GSM10500DTO>();
        public GSM10500DTO loEntity = new GSM10500DTO();
        
        public ObservableCollection<GSM10500RoundingModeDTO>  loRoundingModeList = new ObservableCollection<GSM10500RoundingModeDTO>();
        public List<GSM10500RoundingModeDTO> loRoundingMode = new List<GSM10500RoundingModeDTO>();

        
        public string AgeingRound = ""; // for filter
        public string AgeingCode = ""; // for filter
        public async Task GetAgeingHDList()
        {
            var loReturn = await _GSM10500Model.GetAllAgeingHDStreamAsync();
            loGridList = new ObservableCollection<GSM10500DTO>(loReturn.Data);
        }
        
        public async Task GetRoundingMode()
        {
            
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _GSM10500Model.GetRoundingModeAsync();
                loRoundingMode = loReturn.Data.OrderByDescending(x => x.CCODE).ToList();
                loRoundingModeList = new ObservableCollection<GSM10500RoundingModeDTO>(loRoundingMode);
                AgeingRound = loRoundingMode[0].CCODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetAgeingHDID (string pcAgeingHDCode)
        {
            var loParam = new GSM10500DTO() { CAGEING_CODE = pcAgeingHDCode };
            var loResult = await _GSM10500Model.R_ServiceGetRecordAsync(loParam);
            loEntity = loResult;
        }

        public async Task DeleteAgeingHD(GSM10500DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                await _GSM10500Model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task SaveAgeingHD(GSM10500DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM10500DTO loResult = null;
            try
            {
                loResult = await _GSM10500Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}