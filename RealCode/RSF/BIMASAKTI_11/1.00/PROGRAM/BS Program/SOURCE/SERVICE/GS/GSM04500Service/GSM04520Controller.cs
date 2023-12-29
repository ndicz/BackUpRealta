using GSM04500Back;
using GSM04500Common;
using GSM04500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM04500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM04520Controller : ControllerBase, IGSM04520
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM04520DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04520DTO> poParameter)
        {
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM04520DTO>();
            GSM04500DBParameter loDbPar = new GSM04500DBParameter();

            try
            {
                var loCls = new GSM04520Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

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
        public R_ServiceSaveResultDTO<GSM04520DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04520DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM04520DTO> loRtn = null;
            GSM04520Cls loCls;

            try
            {
                loCls = new GSM04520Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM04520DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04520DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM04520Cls loCls;

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                loCls = new GSM04520Cls();


                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            ;
            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM04520DTO> GetJournalGroupGOADeptListStream()
        {
            R_Exception loException = new R_Exception();
            GSM04500DBParameter loDbPar;
            List<GSM04520DTO> loRtnTmp;
            GSM04520Cls loCls;
            IAsyncEnumerable<GSM04520DTO> loRtn = null;

            try
            {
                loDbPar = new GSM04500DBParameter();
                
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstanGSM04500.CPROPERTY_ID);
                loDbPar.CJOURNAL_GROUP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstanGSM04500.CJOURNAL_GROUP_TYPE);
                loDbPar.CJOURNAL_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstanGSM04500.CJOURNAL_GROUP_CODE);
                loDbPar.CGOA_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstanGSM04500.CGOA_CODE);


                loCls = new GSM04520Cls();
                loRtnTmp = loCls.GetJournalGroupGOADeptList(loDbPar);
                loRtn = GetJournalGroupGOADeptStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();


            return loRtn;   
        }
        
        private async IAsyncEnumerable<GSM04520DTO> GetJournalGroupGOADeptStream(List<GSM04520DTO> poParameter)
        {
            foreach (GSM04520DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}