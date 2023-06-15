using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GSM05500Back;
using GSM05500Common;
using GSM05500Common.DTO;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using static GSM05500Common.DTO.GSM05500ListDTO;

namespace GSM05500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM05500Controller : ControllerBase, IGSM05500
    {
        [HttpPost]

        public R_ServiceGetRecordResultDTO<GSM05500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05500DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM05500DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new GSM05500Cls();

                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;




        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GSM05500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05500DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM05500DTO> loRtn = null;
            GSM05500Cls loCls;

            try
            {
                loCls = new GSM05500Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM05500DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05500DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM05500Cls loCls;

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;


                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                loCls = new GSM05500Cls();
                loCls.R_Delete(poParameter.Entity);
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
        public GSM05500ListDTO GetAllCurrencyList()
        {
            R_Exception loEx = new R_Exception();
            GSM05500ListDTO loRtn = null;
            List<GSM05500DTO> loResult;
            GSM05500DBParameter loDbPar;
            GSM05500Cls loCls;

            try
            {
                loDbPar = new GSM05500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";


                loCls = new GSM05500Cls();
                loResult = loCls.GetAllCurrency(loDbPar);
                loRtn = new GSM05500ListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }



        [HttpPost]
        public IAsyncEnumerable<GSM05500DTO> GetAllCurrencyStream()
        {
            R_Exception loException = new R_Exception();
            GSM05500DBParameter loDbPar;
            List<GSM05500DTO> loRtnTmp;
            GSM05500Cls loCls;
            IAsyncEnumerable<GSM05500DTO> loRtn = null;

            try
            {

                loDbPar = new GSM05500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM05500Cls();
                loRtnTmp = loCls.GetAllCurrency(loDbPar);

                loRtn = GetCurrencyStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<GSM05500DTO> GetCurrencyStream(List<GSM05500DTO> poParameter)
        {
            foreach (GSM05500DTO item in poParameter)
            {
                yield return item;
            }
        }

    }
}
    