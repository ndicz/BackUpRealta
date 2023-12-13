using GSM05500Common;
using GSM05500Common.DTO;
using GSM05500Model.Model;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM05500Model
{
    public class GSM05520ViewModel : R_ViewModel<GSM05520DTO>
    {

        private GSM05520Model _GSM05520Model = new GSM05520Model();

        public ObservableCollection<GSM05520DTO> loGridList = new ObservableCollection<GSM05520DTO>();
        public ObservableCollection<GSM05520DTOGetRateType>loGridListRate = new ObservableCollection<GSM05520DTOGetRateType>();
        public GSM05520DTOLocalBaseCurrency loGridListLc = new GSM05520DTOLocalBaseCurrency();

        public GSM05520DTO loEntity = new GSM05520DTO();
        public List<GSM05520DTO> loRateTypeList { get; set; } = new List<GSM05520DTO>();

        public List<GSM05520DTOGetRateType> loRateType { get; set; } = new List<GSM05520DTOGetRateType>();
        public string RateTypeCode = "";
        public string CrateDate = DateTime.Now.ToString("yyyyMMdd");
        public string CurrencyCode = "";
        public string CreateCode = "";

        public DateTime CrateTime = DateTime.Now;


    
        //grid
        public async Task GetRateList()
        {

            var loEx = new R_Exception();

            try
            {

                var loReturn = await _GSM05520Model.GetAllStreamingAsync(CreateCode, CrateDate);
                loEntity.CRATE_DATE = CrateDate;
                loGridList = new ObservableCollection<GSM05520DTO>(loReturn.Data);
                

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        //dropdown
        public async Task GetRateListP()
        {

            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05520Model.GetRateTypeStreamingAsync();
                loRateType = loReturn.Data;
                CreateCode = loRateType[0].CRATETYPE_CODE;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //public async Task GetRateListP()
        //{
        //    var loEx = new R_Exception();

        //    try
        //    {
        //        var loReturn = await _GSM05520Model.GetRateList();
        //        loGridListRate = new ObservableCollection<GSM05520DTOGetRateType>(loReturn.Data);

        //        // Mendapatkan nilai pertama dari loGridListRate
        //        var firstDefault = loGridListRate.FirstOrDefault();
        //        if (firstDefault != null)
        //        {
        //            // Gunakan nilai firstDefault di sini
        //            // ...
        //            // Contoh: Mendapatkan nilai crate_type dan crate_date
        //            string crateType = firstDefault.CRATE_TYPE_CODE;

        //            // ...
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //}


        public async Task GetLcCurrency()
         {

             var loEx = new R_Exception();

             try
             {

                 loGridListLc = await _GSM05520Model.GetLccCurrency();

            }
             catch (Exception ex)
             {
                 loEx.Add(ex);
             }
             loEx.ThrowExceptionIfErrors();
         }


        public async Task GetRateId(string currencyCode, string createCode, string crateDate)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM05520DTO() { CCURRENCY_CODE = currencyCode, CRATETYPE_CODE = createCode, CRATE_DATE = crateDate};
                var loResult = await _GSM05520Model.R_ServiceGetRecordAsync(loParam);

                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSM05520DTO> SaveRateType(GSM05520DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            var loEx = new R_Exception();
            GSM05520DTO loResult = null;

            try
            {
                RateTypeCode = poNewEntity.CRATETYPE_CODE;
                CurrencyCode = poNewEntity.CCURRENCY_CODE;
                loResult = await _GSM05520Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task DeleteRate(GSM05520DTO poProperty)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM05520DTO
                {
                    CCOMPANY_ID = poProperty.CCOMPANY_ID,
                    CCURRENCY_CODE = poProperty.CCURRENCY_CODE,
                    CRATETYPE_CODE = poProperty.CRATETYPE_CODE,
                    CRATE_DATE = poProperty.CRATE_DATE,
                    NLBASE_RATE_AMOUNT = poProperty.NLBASE_RATE_AMOUNT,
                    NLCURRENCY_RATE_AMOUNT = poProperty.NLCURRENCY_RATE_AMOUNT,
                    NBBASE_RATE_AMOUNT = poProperty.NBBASE_RATE_AMOUNT,
                    NBCURRENCY_RATE_AMOUNT = poProperty. NBCURRENCY_RATE_AMOUNT

                };
                await _GSM05520Model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }

  
}
