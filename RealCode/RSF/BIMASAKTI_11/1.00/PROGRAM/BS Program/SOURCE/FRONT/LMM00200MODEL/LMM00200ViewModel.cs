using LMM00200Common;
using LMM00200Common.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LMM00200Model
{
    public class LMM00200ViewModel : R_ViewModel<LMM00200DTO>
    {
        private LMM00200Model _model = new LMM00200Model();

        public ObservableCollection<LMM00200StreamDTO> UserParamList { get; set; } = new ObservableCollection<LMM00200StreamDTO>();
        public LMM00200DTO loUserParam { get; set; } = new LMM00200DTO();

        public string CUSER_LEVEL_OPERATOR_SIGN { get; set; } ="";
        public List<RadioModel> Options { get; set; } = new List<RadioModel>
        {
            new RadioModel { Value = "=", Text = "(=)" },
            new RadioModel { Value= ">=", Text = "(>=)" },
        };
        public string liUserParamCode{ get; set; }
        public bool ActiveDept { get; set; }

        public string _action { get; set; }


        public async Task GetUserParamList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetUserParamListAsync();
                UserParamList = new ObservableCollection<LMM00200StreamDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUserParamRecord(LMM00200DTO poDept)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCODE, liUserParamCode);
                LMM00200DTO loParam = new LMM00200DTO();
                loParam = poDept;
                var loResult = await _model.R_ServiceGetRecordAsync(loParam);
                loUserParam = R_FrontUtility.ConvertObjectToObject<LMM00200DTO>(loResult);
                CUSER_LEVEL_OPERATOR_SIGN = loUserParam.CUSER_LEVEL_OPERATOR_SIGN;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUserParam(LMM00200DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                loUserParam = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ActiveInactiveProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CCODE, liUserParamCode);
                R_FrontContext.R_SetContext(ContextConstant.LACTIVE, ActiveDept);
                R_FrontContext.R_SetContext(ContextConstant.CACTION, _action);
                await _model.GetActiveParamAsync();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }

    public class RadioModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
