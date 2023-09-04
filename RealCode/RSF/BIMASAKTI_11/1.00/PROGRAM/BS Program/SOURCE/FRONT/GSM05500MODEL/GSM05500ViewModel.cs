using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM05500Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM05500Model.ViewModel
{
    public class GSM05500ViewModel : R_ViewModel<GSM05500DTO>
    {
        private Model.GSM05500Model _GSM05500Model = new Model.GSM05500Model();

        public ObservableCollection<GSM05500DTO> loGridList = new ObservableCollection<GSM05500DTO>();

        public GSM05500DTO loEntity = new GSM05500DTO();


        public async Task GetCurrencyList()
        {
                
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05500Model.GetAllStreamAsync();
                loGridList = new ObservableCollection<GSM05500DTO>(loReturn.Data);

            }
            catch (Exception ex)
            {
               loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetCurrencyId(string currencyCode)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM05500DTO() { CCURRENCY_CODE = currencyCode };
                var loResult = await _GSM05500Model.R_ServiceGetRecordAsync(loParam);

                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteCurrency(GSM05500DTO poProperty)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM05500DTO
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CCURRENCY_CODE = poProperty.CCURRENCY_CODE,
                    CCURRENCY_NAME = poProperty.CCURRENCY_NAME,
                    CUSER_ID = poProperty.CUSER_ID,
                   
                };
                await _GSM05500Model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSM05500DTO> SaveCurrency(GSM05500DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM05500DTO loResult = null;

            try
            {
                loResult = await _GSM05500Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

    }
}
