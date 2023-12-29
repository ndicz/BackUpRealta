using GSM04500Back;
using GSM04500Common;
using GSM04500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM04500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
  
    public class GSM04500Controller : ControllerBase, IGSM04500
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM04500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04500DTO> poParameter)
        {
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM04500DTO>();
            GSM04500DBParameter loDbPar = new GSM04500DBParameter();

            try
            {
                var loCls = new GSM04500Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

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
        public R_ServiceSaveResultDTO<GSM04500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04500DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM04500DTO> loRtn = null;
            GSM04500Cls loCls;

            try
            {
                loCls = new GSM04500Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM04500DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04500DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM04500Cls loCls;

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls = new GSM04500Cls();


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
        public IAsyncEnumerable<GSM04500DTO> GetJournalGroupListStream()
        {
            R_Exception loException = new R_Exception();
            GSM04500DBParameter loDbPar;
            List<GSM04500DTO> loRtnTmp;
            GSM04500Cls loCls;
            IAsyncEnumerable<GSM04500DTO> loRtn = null;

            try
            {
                loDbPar = new GSM04500DBParameter();
                
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstanGSM04500.CPROPERTY_ID);
                loDbPar.CJOURNAL_GROUP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstanGSM04500.CJOURNAL_GROUP_TYPE);


                loCls = new GSM04500Cls();
                loRtnTmp = loCls.GetJournalGroupList(loDbPar);
                loRtn = GetJournalGroupStream(loRtnTmp);
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
        public IAsyncEnumerable<GSM04500PropertyDTO> GetAllPropertyListStream()
        {
            R_Exception loException = new R_Exception();
            GSM04500DBParameter loDbPar;
            List<GSM04500PropertyDTO> loRtnTmp;
            GSM04500Cls loCls;
            IAsyncEnumerable<GSM04500PropertyDTO> loRtn = null;
            try
            {
                loDbPar = new GSM04500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM04500Cls();

                loCls = new GSM04500Cls();
                loRtnTmp = loCls.GetInitProperty(loDbPar);
                loRtn = GetPropertyStream(loRtnTmp);
            }

           catch (Exception ex)
            {
                loException.Add(ex);

            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM04500JournalGroupTypeDTO> GetListJournalGroupTypeStream()
        {
            R_Exception loException = new R_Exception();
            GSM04500DBParameter loDbPar;
            List<GSM04500JournalGroupTypeDTO> loRtnTmp;
            GSM04500Cls loCls;
            IAsyncEnumerable<GSM04500JournalGroupTypeDTO> loRtn = null;
            try
            {
                loDbPar = new GSM04500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM04500Cls();

                loCls = new GSM04500Cls();
                loRtnTmp = loCls.GetJournalGroupTypeList(loDbPar);
                loRtn = GetJournalTypeGroupStream(loRtnTmp);
            }

            catch (Exception ex)
            {
                loException.Add(ex);

            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        private async IAsyncEnumerable<GSM04500DTO> GetJournalGroupStream(List<GSM04500DTO> poParameter)
        {
            foreach (GSM04500DTO item in poParameter)
            {
                yield return item;
            }
        }
        
        private async IAsyncEnumerable<GSM04500PropertyDTO> GetPropertyStream(List<GSM04500PropertyDTO> poParameter)
        {
            foreach (GSM04500PropertyDTO item in poParameter)
            {
                yield return item;
            }
        }
        
        private async IAsyncEnumerable<GSM04500JournalGroupTypeDTO> GetJournalTypeGroupStream(List<GSM04500JournalGroupTypeDTO> poParameter)
        {
            foreach (GSM04500JournalGroupTypeDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}