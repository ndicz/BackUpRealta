using System;
using System.Threading.Tasks;
using LMM02500Common;
using LMM02500Model.Model;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMM02500Model
{
    public class LMM02510ViewModel : R_ViewModel<LMM02510DTO>
    {
        private LMM02510Model _LMM02000Model = new LMM02510Model();
        public LMM02500ViewModel _LMM02500ViewModel = new LMM02500ViewModel();
        public LMM02510DTO loEntity = new LMM02510DTO();
        public LMM02500DTO loEntityLMM02500 = new LMM02500DTO();

        public async Task Init(object poParam)
        {
            loEntityLMM02500 = (LMM02500DTO)poParam;
        }

        public async Task GetEntity(LMM02510DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var poParam = new LMM02500ParameterDTO();
                poParam.CPROPERTY_ID = loEntityLMM02500.CPROPERTY_ID;
                poParam.CTENANT_GROUP_ID = loEntityLMM02500.CTENANT_GROUP_ID;
                poParam.CTENANT_GROUP_NAME = loEntityLMM02500.CTENANT_GROUP_NAME;
                
                loEntity = await _LMM02000Model.R_ServiceGetRecordAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);   
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Delete(LMM02510DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                await _LMM02000Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}