using LMM03700Common;
using LMM03700Common.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03700Model
{
    public class LMM03700ViewModel : R_ViewModel<TenantClassificationGroupDTO>
    {
        private LMM03700Model _model = new LMM03700Model();
        public ObservableCollection<TenantClassificationGroupDTO> TenantClassificationGroupList { get; set; } = new ObservableCollection<TenantClassificationGroupDTO>();
        public TenantClassificationGroupDTO TenantClassificationGroup { get; set; } = new TenantClassificationGroupDTO();
        public List<PropertyDTO> PropertyList { get; set; } = new List<PropertyDTO>();
        public PropertyDTO Propertiy { get; set; } = new PropertyDTO();
        public string _propertyId { get; set; } = "";

        public async Task GetTenantClassGroupList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(LMM03700ContextConstant.CPROPERTY_ID, _propertyId);
                var loResult = await _model.GetTenantClassRecord();
                TenantClassificationGroupList = new ObservableCollection<TenantClassificationGroupDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetTenantClassGroupRecord(TenantClassificationGroupDTO loParam)
        {
            loParam.CPROPERTY_ID= _propertyId;
            var loResult = await _model.R_ServiceGetRecordAsync(loParam);
            TenantClassificationGroup = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(loResult);
        }
        public async Task SaveTenantClassGroup(TenantClassificationGroupDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                poNewEntity.CPROPERTY_ID = _propertyId;
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                TenantClassificationGroup = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteTenantClassGroup(TenantClassificationGroupDTO loParam)
        {
            var loEx = new R_Exception();

            try
            {
                loParam.CPROPERTY_ID = _propertyId;
                await _model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetPropertyListAsync();
                PropertyList = new List<PropertyDTO>(loResult);
                _propertyId = PropertyList.FirstOrDefault().CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
