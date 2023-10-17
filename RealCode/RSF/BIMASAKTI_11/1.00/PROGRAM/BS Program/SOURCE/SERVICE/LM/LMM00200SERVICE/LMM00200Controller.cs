using LMM00200Back;
using LMM00200Common;
using LMM00200Common.DTO_s;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM00200Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMM00200Controller : ControllerBase, ILMM00200
    {
        [HttpPost]
        public IAsyncEnumerable<LMM00200StreamDTO> GetUserParamList()
        {
            R_Exception loException = new R_Exception();
            List<LMM00200StreamDTO> loRtnTemp = null;
            LMM00200DBListParam loDbParam;
            LMM00200Cls loCls;
            try
            {
                loCls = new LMM00200Cls();
                loRtnTemp = loCls.GetUserParamList(new LMM00200DBListParam()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return LMM00200StreamListHelper(loRtnTemp);
        }

        private async IAsyncEnumerable<LMM00200StreamDTO> LMM00200StreamListHelper(List<LMM00200StreamDTO> loRtnTemp)
        {
            foreach (LMM00200StreamDTO loEntity in loRtnTemp)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM00200DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM00200DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM00200DTO> poParameter)
        {
            R_ServiceGetRecordResultDTO<LMM00200DTO> loRtn = null;
            R_Exception loException = new R_Exception();
            LMM00200Cls loCls;
            try
            {
                loCls = new LMM00200Cls(); //create cls class instance
                loRtn = new R_ServiceGetRecordResultDTO<LMM00200DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCODE);
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM00200DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM00200DTO> poParameter)
        {
            R_ServiceSaveResultDTO<LMM00200DTO> loRtn = null;
            R_Exception loException = new R_Exception();
            LMM00200Cls loCls;
            try
            {
                loCls = new LMM00200Cls();
                loRtn = new R_ServiceSaveResultDTO<LMM00200DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);//call clsMethod to save
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public LMM00200ActiveInactiveParamDTO GetActiveParam()
        {
            LMM00200ActiveInactiveParamDTO loRtn = null;
            R_Exception loException = new R_Exception();
            LMM00200Cls loCls;
            try
            {
                loCls = new LMM00200Cls();
                loRtn = new LMM00200ActiveInactiveParamDTO();
                var loParam = new LMM00200DTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CCODE = R_Utility.R_GetContext<string>(ContextConstant.CCODE),
                    LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.LACTIVE),
                    CACTION = R_Utility.R_GetContext<string>(ContextConstant.CACTION),
                    CDESCRIPTION = "",
                    CUSER_LEVEL_OPERATOR_SIGN = "",
                    CVALUE = "",
                    IUSER_LEVEL = 0
                };
                //call clsMethod to save
                loCls.ActiveInactiveUserParam(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}