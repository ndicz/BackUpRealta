using LMI00100Back;
using LMI00100Common;
using LMI00100Common.DTO;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMI00100Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMI00100Controller : ControllerBase, ILMI00100
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMI00100DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMI00100DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<LMI00100DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMI00100DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMI00100DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public LMI00100ListDTO GetAllLMI00100List()
        {
            R_Exception loEx = new R_Exception();
            LMI00100ListDTO loRtn = null;
            List<LMI00100DTO> loResult;
            LMI00100DBParameter loDbPar;
            LMI00100Cls loCls;

            try
            {
                loDbPar = new LMI00100DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMI00100.CPROPERTY_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";
                //loDbPar.CPROPERTY_ID = "ASHMD";

                loCls = new LMI00100Cls();
                loResult = loCls.GetAllBankChannelList(loDbPar);
                loRtn = new LMI00100ListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public LMI00100ListPropertyDTO GetLMI00100Property()
        {
            R_Exception loEx = new R_Exception();
            LMI00100ListPropertyDTO loRtn = null;
            List<LMI00100PropertyDTO> loResult;
            LMI00100DBParameter loDbPar;
            LMI00100Cls loCls;

            try
            {
                loDbPar = new LMI00100DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new LMI00100Cls();
                loResult = loCls.GetAllPropertyList(loDbPar);
                loRtn = new LMI00100ListPropertyDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<LMI00100DTO> GetAllLMI00100Stream()
        {
            R_Exception loException = new R_Exception();
            LMI00100DBParameter loDbPar;
            List<LMI00100DTO> loRtnTmp;
            LMI00100Cls loCls;
            IAsyncEnumerable<LMI00100DTO> loRtn = null;
            try
            {

                loDbPar = new LMI00100DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMI00100.CPROPERTY_ID);

                loCls = new LMI00100Cls();
                loRtnTmp = loCls.GetAllBankChannelList(loDbPar);

                loRtn = GetBankChannel(loRtnTmp);
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
        public IAsyncEnumerable<LMI00100PropertyDTO> GetLMI00100PropertyStream()
        {
            R_Exception loException = new R_Exception();
            LMI00100DBParameter loDbPar;
            List<LMI00100PropertyDTO> loRtnTmp;
            LMI00100Cls loCls;
            IAsyncEnumerable<LMI00100PropertyDTO> loRtn = null;
            try
            {

                loDbPar = new LMI00100DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls = new LMI00100Cls();
                loRtnTmp = loCls.GetAllPropertyList(loDbPar);

                loRtn = GetProperty(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<LMI00100DTO> GetBankChannel(List<LMI00100DTO> poParameter)
        {
            foreach (LMI00100DTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<LMI00100PropertyDTO> GetProperty(List<LMI00100PropertyDTO> poParameter)
        {
            foreach (LMI00100PropertyDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}