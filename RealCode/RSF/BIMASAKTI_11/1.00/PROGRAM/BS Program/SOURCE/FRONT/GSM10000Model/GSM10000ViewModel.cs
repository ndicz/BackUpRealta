using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using GSM10000Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM10000Model
{
    public class GSM10000ViewModel : R_ViewModel<GSM10000DTO>
    {
        private Model.GSM10000Model _GSM10000Model = new Model.GSM10000Model();
        public ObservableCollection<GSM10000DTO> loGridList = new ObservableCollection<GSM10000DTO>();
        public GSM10000DTO loEntity = new GSM10000DTO();
        public string holidayDate = ""; // for r_display

        public async Task GetHolidayList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM10000Model.GetAllStreamAsync();
                loGridList = new ObservableCollection<GSM10000DTO>(loReturn.Data);
                foreach (var list in loGridList)
                {
                    list.DHOLIDAY_DATE = DateTime.ParseExact(list.CHOLIDAY_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetHolidayId(string holidayCode)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new GSM10000DTO() { CHOLIDAY_DATE = holidayCode };

                var loResult = await _GSM10000Model.R_ServiceGetRecordAsync(loParam);
                // Assuming DHOLIDAY_DATE is of type DateTime in loResult
                // Convert DateTime to string using a specific format
                // loParam.CHOLIDAY_DATE = loResult.DHOLIDAY_DATE.ToString("yyyy-MM-dd"); 

                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task DeleteHoliday(GSM10000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM10000Model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task SaveHoliday(GSM10000DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM10000DTO loResult = null;
            try
            {
                loResult = await _GSM10000Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task ActiveInactiveProcessAsync()
        {
            R_Exception loException = new R_Exception();

            try
            {
                var poParam = new LMM02000ActiveInactiveParam();
                poParam.CHOLIDAY_DATE = loEntity.CHOLIDAY_DATE;
                poParam.LACTIVE = !loEntity.LACTIVE;

                var loreturn = await _GSM10000Model.GetActiveInactiveS(poParam);

      }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}