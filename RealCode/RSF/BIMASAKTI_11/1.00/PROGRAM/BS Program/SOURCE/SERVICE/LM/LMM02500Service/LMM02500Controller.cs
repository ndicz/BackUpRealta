using LMM02500Back;
using LMM02500Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMM02500Controller : ControllerBase, ILMM02500
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM02500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM02500DTO> poParameter)
        {
            
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM02500DTO>();
            LMM02500DBParameter loDbPar;
            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                var loCls = new LMM02500Cls();
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM02500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM02500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM02500InitialProcessDTO> GetInitialProcessStream()
        {
            
            R_Exception loException = new R_Exception();
            LMM02500DBParameter loDbPar;
            List<LMM02500InitialProcessDTO> loRtnTmp;
            LMM02500Cls loCls;
            IAsyncEnumerable<LMM02500InitialProcessDTO> loRtn = null;

            try
            {

                loDbPar = new LMM02500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                //loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new LMM02500Cls();
                loRtnTmp = loCls.GetInitialProcess(loDbPar);
                loRtn = GetInitialHelper(loRtnTmp);
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
        public IAsyncEnumerable<LMM02500DTO> GetTenantGroupListStream()
        {

            R_Exception loException = new R_Exception();
            LMM02500DBParameter loDbPar;
            List<LMM02500DTO> loRtnTmp;
            LMM02500Cls loCls;
            IAsyncEnumerable<LMM02500DTO> loRtn = null;

            try
            {

                loDbPar = new LMM02500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02500.CPROPERTY_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new LMM02500Cls();
   
                loRtnTmp = loCls.GetTenantGroupList(loDbPar);
      
                loRtn = GetTenantHelper(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
        
        private async IAsyncEnumerable<LMM02500DTO> GetTenantHelper(List<LMM02500DTO> poParameter)
        {
            foreach (LMM02500DTO item in poParameter)
            {
                yield return item;  
            }
        }
        private async IAsyncEnumerable<LMM02500InitialProcessDTO> GetInitialHelper(List<LMM02500InitialProcessDTO> poParameter)
        {
            foreach (LMM02500InitialProcessDTO item in poParameter)
            {
                yield return item;  
            }
        }
    }
}