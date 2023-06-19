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
            //R_Exception loEx = new R_Exception();
            //GSM00710ListDTO loRtn = null;
            //List<GSM00710DTO> loResult;
            //GSM00700DBParameter loDbPar;
            //GSM00710Cls loCls;
            throw new NotImplementedException();
        }
        [HttpPost]
        public GSM00720YearListDTO GetYearList()
        {
            throw new NotImplementedException();
        }
    }
}
