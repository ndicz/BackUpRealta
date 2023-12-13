using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM02000Common;
using LMM02000Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMM02000Model
{
    public class LMM02000ViewModel : R_ViewModel<LMM02000DTO>
    {
        private Model.LMM02000Model _LMM02000Model = new Model.LMM02000Model();
        public ObservableCollection<LMM02000DTO> loGridList = new ObservableCollection<LMM02000DTO>();
        public ObservableCollection<LMM02000PropertyDTO> loGridListProperty = new ObservableCollection<LMM02000PropertyDTO>();
        //public ObservableCollection<LMM02000ListGenderSalesmanTypeDTO> logridListGenderSalesmanType = new ObservableCollection<LMM02000ListGenderSalesmanTypeDTO>();
        public LMM02000DTO loEntity = new LMM02000DTO();
        public List<LMM02000PropertyDTO> PropertyList { get; set; } = new List<LMM02000PropertyDTO>();

        public List<LMM02000GenderTypeDTO> GenderList { get; set; } = new List<LMM02000GenderTypeDTO>();
        //{
        //    new LMM02000GenderSalesmanTypeDTO() {CCODE = "F", CDESCRIPTION = "Female"},
        //    new LMM02000GenderSalesmanTypeDTO() {CCODE = "M", CDESCRIPTION = "Male"}
        //};

        public List<LMM02000SalesmanTypeDTO> SalesmanTypeList { get; set; } = new List<LMM02000SalesmanTypeDTO>();
        //{
        //    new LMM02000GenderSalesmanTypeDTO() {CCODE = "I", CDESCRIPTION = "Internal"},
        //    new LMM02000GenderSalesmanTypeDTO() {CCODE = "E", CDESCRIPTION = "Eksternal"}
        //};
        public string propertyCode = "";
        public string propertyValue = "";
        public string genderTypeValue = "";
        public bool salesmanTypeCode = false;
        public bool SelectedActiveInactiveLACTIVE;



        public async Task ActiveInactiveProcessAsync()
        {
            R_Exception loException = new R_Exception();

            try
            {
                var poParam = new LMM02000ActiveInactiveParam();
                poParam.CSALESMAN_ID = loEntity.CSALESMAN_ID;
                poParam.LACTIVE = !loEntity.LACTIVE;
                poParam.CPROPERTY_ID = loEntity.CPROPERTY_ID;

                var loreturn = await _LMM02000Model.GetActiveInactiveS(poParam);

                //R_FrontContext.R_SetContext(ContextConstantLMM02000.CPROPERTY_ID, loEntity.CPROPERTY_ID);

                //R_FrontContext.R_SetContext(ContextConstantLMM02000.CSALESMAN_ID, loEntity.CSALESMAN_ID);

                //R_FrontContext.R_SetContext(ContextConstantLMM02000.LACTIVE, !loEntity.LACTIVE);



            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }





        public async Task GetGenderList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02000Model.GetGenderAll();

                foreach (var VARIABLE in loResult.Data)
                {
                    VARIABLE.CCODE = VARIABLE.CCODE.Trim();
                }
                GenderList = loResult.Data.OrderByDescending(x => x.CCODE).ToList(); // Urutkan dan simpan ke dalam list



            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetSalesmanType()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02000Model.GetSalesmanTypeAll();
                SalesmanTypeList = loResult.Data.OrderByDescending(x => x.CCODE).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetProperty()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02000Model.GetPropertyStreamAsync();   
                PropertyList = loResult.Data;
                propertyValue = PropertyList[0].CPROPERTY_ID;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetGridList()
        {
            var loEx = new R_Exception();
            try
            {

                var loReturn = await _LMM02000Model.GetSalesmanStreamAsync(propertyValue);
                loGridList = new ObservableCollection<LMM02000DTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(LMM02000DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                loEntity = await _LMM02000Model.R_ServiceGetRecordAsync(poEntity);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        public async Task SaveEntity(LMM02000DTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                poEntity.CPROPERTY_ID = propertyValue;
                poEntity.CEXT_COMPANY_NAME = "";
                loEntity = await _LMM02000Model.R_ServiceSaveAsync(poEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task Delete(LMM02000DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                poEntity.CPROPERTY_ID = propertyValue;
                await _LMM02000Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);

            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<LMM02000Template> DownloadTemplate()
        {
            var loEx = new R_Exception();
            LMM02000Template loResult = null;

            try
            {
                loResult = await _LMM02000Model.GetTemplateAsync();
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
