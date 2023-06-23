using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common;
using GSM00700Common.DTO;
using R_CommonFrontBackAPI;
using GSM00700Back;
using R_BackEnd;
using R_Common;

namespace GSM00700Service
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM00720Controller : ControllerBase, IGSM00720
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM00720DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM00720DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GSM00720DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM00720DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM00720DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public GSM00720ListDTO GetAllCashFlowPlan()
        {
            R_Exception loEx = new R_Exception();   
            GSM00720ListDTO loRtn = null;
            List<GSM00720DTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00720Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CCASH_FLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE);
                loDbPar.CCASH_FLOW_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_CODE);
                loDbPar.CCYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CYEAR);

                loDbPar.CCOMPANY_ID = "RCD";
                loDbPar.CUSER_ID = "Admin";
                loDbPar.CCASH_FLOW_GROUP_CODE = "CF0012";
                loDbPar.CCASH_FLOW_CODE = "F001";
                loDbPar.CCYEAR = "2023";
                loCls = new GSM00720Cls();
                loResult = loCls.GetCashFlowPlan(loDbPar);
                loRtn = new GSM00720ListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public GSM00720YearListDTO GetYearList()
        {
            R_Exception loEx = new R_Exception();
            GSM00720YearListDTO loRtn = null;
            List<GSM00720YearDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00720Cls loCls;
            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                loDbPar.CCOMPANY_ID = "RCD";
                loCls = new GSM00720Cls();
                loResult = loCls.GetYearList(loDbPar);
                loRtn = new GSM00720YearListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
