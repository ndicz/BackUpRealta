using LMM02500Back;
using LMM02500Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02500Service
{
    public class LMM02511Controller : ControllerBase, ILMM02511
    {[HttpPost]
        public R_ServiceGetRecordResultDTO<LMM02511DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM02511DTO> poParameter)
        {
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM02511DTO>();
            LMM02500DBParameter loDbPar;
            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                var loCls = new LMM02511Cls();
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
        public R_ServiceSaveResultDTO<LMM02511DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM02511DTO> poParameter)
        {
         
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM02511DTO> loRtn = null;
            LMM02511Cls loCls;
            var loDbPar = new LMM02500DBParameter();
            try
            {
                loCls = new LMM02511Cls();
                loRtn = new R_ServiceSaveResultDTO<LMM02511DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02500.CPROPERTY_ID);

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            };
            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
[HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02511DTO> poParameter)
        {
            throw new NotImplementedException();
        }
[HttpPost]
        public LMM02511ListDTO GetTaxCode()
        {
            R_Exception loException = new R_Exception();
            LMM02511ListDTO loRtn = null;
            List<LMM02511DTO> loResult;
            LMM02500DBParameter loDbPar;
            LMM02511Cls loCls;

            try
            {
                loDbPar = new LMM02500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                loCls = new LMM02511Cls();
                loResult = loCls.GetTaxCode(loDbPar);
                loRtn = new LMM02511ListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
[HttpPost]
        public LMM02511ListDTO GetIDType()
        {
            R_Exception loException = new R_Exception();
            LMM02511ListDTO loRtn = null;
            List<LMM02511DTO> loResult;
            LMM02500DBParameter loDbPar;
            LMM02511Cls loCls;

            try
            {
                loDbPar = new LMM02500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                loCls = new LMM02511Cls();
                loResult = loCls.GetIDType(loDbPar);
                loRtn = new LMM02511ListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
[HttpPost]
        public LMM02511ListDTO GetTaxType()
        {
            R_Exception loException = new R_Exception();
            LMM02511ListDTO loRtn = null;
            List<LMM02511DTO> loResult;
            LMM02500DBParameter loDbPar;
            LMM02511Cls loCls;

            try
            {
                loDbPar = new LMM02500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                loCls = new LMM02511Cls();
                loResult = loCls.GetTaxType(loDbPar);
                loRtn = new LMM02511ListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}