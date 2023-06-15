using GSM05500Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM05500Common.DTO;
using R_CommonFrontBackAPI;
using GSM05500Back;
using R_BackEnd;
using R_Common;

namespace GSM05500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM05520Controller : ControllerBase, IGSM05520
    {

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM05520DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05520DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM05520DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new GSM05520Cls();

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
        public R_ServiceSaveResultDTO<GSM05520DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05520DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM05520DTO> loRtn = null;
            GSM05520Cls loCls;
            var loDbPar = new GSM05500DBParameter();

            try
            {
                loCls = new GSM05520Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM05520DTO>();
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05520DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM05520Cls loCls;

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls = new GSM05520Cls();
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
        public IAsyncEnumerable<GSM05520DTO> GetAllRateStream()
        {
            R_Exception loException = new R_Exception();
            GSM05500DBParameter loDbPar;
            List<GSM05520DTO> loRtnTmp;
            GSM05520Cls loCls;
            IAsyncEnumerable<GSM05520DTO> loRtn = null;

            try
            {

                loDbPar = new GSM05500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM05520Cls();
                loRtnTmp = loCls.GetAllCurrencyRate(loDbPar);

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

        [HttpPost]
        public GSM05520ListDTO GetAllRateList()
        {
            R_Exception loEx = new R_Exception();
            GSM05520ListDTO loRtn = null;
            List<GSM05520DTO> loResult;
            GSM05500DBParameter loDbPar;
            GSM05520Cls loCls;

            try
            {
                loDbPar = new GSM05500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CRATETYPE_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM05500.CRATETYPE_CODE);
                loDbPar.CRATE_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM05500.CRATE_DATE);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";


                loCls = new GSM05520Cls();
                loResult = loCls.GetAllCurrencyRate(loDbPar);
                loRtn = new GSM05520ListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }


        [HttpPost]

        public GSM05520DTOLocalBaseCurrency GetLcCurrency()
        {
            R_Exception loEx = new R_Exception();
            GSM05520DTOLocalBaseCurrency loRtn = null;
            GSM05520DTOLocalBaseCurrency loResult;
            GSM05500DBParameter loDbPar;
            GSM05520Cls loCls;

            try
            {
                loDbPar = new GSM05500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";


                loCls = new GSM05520Cls();
                loResult = loCls.GetLocalCurrency(loDbPar);
                loRtn = new GSM05520DTOLocalBaseCurrency();
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM05520ListDTOGetRateType GetListRateType()
        {
            R_Exception loEx = new R_Exception();
            GSM05520ListDTOGetRateType loRtn = null;
            List<GSM05520DTOGetRateType> loResult;
            GSM05500DBParameter loDbPar;
            GSM05520Cls loCls;

            try
            {
                loDbPar = new GSM05500DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";


                loCls = new GSM05520Cls();
                loResult = loCls.GetRateType(loDbPar);
                loRtn = new GSM05520ListDTOGetRateType() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GSM05520DTO> GetCurrencyStream(List<GSM05520DTO> poParameter)
        {
            foreach (GSM05520DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
