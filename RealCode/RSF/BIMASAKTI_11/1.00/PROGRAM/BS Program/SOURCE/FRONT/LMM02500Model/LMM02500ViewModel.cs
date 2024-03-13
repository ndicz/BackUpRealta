using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LMM02500Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMM02500Model
{
    public class LMM02500ViewModel : R_ViewModel<LMM02500DTO>
    {
        private Model.LMM02500Model _model = new Model.LMM02500Model();
        public ObservableCollection<LMM02500DTO> loGridList = new ObservableCollection<LMM02500DTO>();
        public ObservableCollection<LMM02500InitialProcessDTO> loGridListProperty = new ObservableCollection<LMM02500InitialProcessDTO>();
        
        public LMM02500DTO loEntity = new LMM02500DTO();
        public LMM02500InitialProcessListDTO loInitialProcessEntity = new LMM02500InitialProcessListDTO();  
        
        public List<LMM02500InitialProcessDTO> InitialPropertyList { get; set; } = new List<LMM02500InitialProcessDTO>();

        public string propertyValue = "";
        public bool _comboBoxEnabled = true;

        public async Task GetInitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetInitialProcessStreamASync();   
                InitialPropertyList = loResult.Data;
                propertyValue = InitialPropertyList[0].CPROPERTY_ID;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
            
        public async Task TenantGroupList(string PropertyCode)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantLMM02500.CPROPERTY_ID, PropertyCode);
                var loReturn = await _model.GetTenantGroupListStreamAsync();
                loGridList = new ObservableCollection<LMM02500DTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
     
        
    }
}