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
using Microsoft.Extensions.Logging;
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
        private LogGSM05500Common _logger;

        public GSM05500Controller(ILogger<GSM05500Controller> logger)
        {
            LogGSM05500Common.R_InitializeLogger(logger);
            _logger = LogGSM05500Common.R_GetInstanceLogger();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM05500DTO> R_ServiceGetRecord(
            R_ServiceGetRecordParameterDTO<GSM05500DTO> poParameter)
        {
            _logger.LogInfo("Begin || GetRecordCurrency(Controller)");
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM05500DTO>();

            try
            {
                _logger.LogInfo("Set Parameter || GetRecordCurrency(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new GSM05500Cls();

                _logger.LogInfo("Run GetRecordCurrencyCls || GetRecordCurrency(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordCurrency(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM05500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05500DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceSaveCurrency(Controller)");
            R_Exception loExceptionception = new R_Exception();
            R_ServiceSaveResultDTO<GSM05500DTO> loRtn = null;
            GSM05500Cls loCls;

            try
            {
                loCls = new GSM05500Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM05500DTO>();
                _logger.LogInfo("Set Parameter || ServiceSaveCurrency(Controller)");

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                _logger.LogInfo("Run SaveCurrencyCls || ServiceSaveCurrency(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loExceptionception.Add(ex);
                _logger.LogError(loExceptionception);
            }

            ;
            EndBlock:
            loExceptionception.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceSaveCurrency(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05500DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceDeleteCurrency(Controller)");
            R_Exception loExceptionception = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM05500Cls loCls;

            try
            {
                _logger.LogInfo("Set Parameter || ServiceDeleteCurrency(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;


                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                loCls = new GSM05500Cls();
                _logger.LogInfo("Run DeleteCurrencyCls || ServiceDeleteCurrency(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loExceptionception.Add(ex);
                _logger.LogError(loExceptionception);
            }

            ;
            EndBlock:
            loExceptionception.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceDeleteCurrency(Controller)");
            return loRtn;
        }

        //[HttpPost]
        //public GSM05500ListDTO GetAllCurrencyList()
        //{
        //    R_Exception loException = new R_Exception();
        //    GSM05500ListDTO loRtn = null;
        //    List<GSM05500DTO> loResult;
        //    GSM05500DBParameter loDbPar;
        //    GSM05500Cls loCls;

        //    try
        //    {
        //        //loDbPar = new GSM05500DBParameter();
        //        //loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        //loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
        //        ////loDbPar.CCOMPANY_ID = "RCD";
        //        ////loDbPar.CUSER_ID = "Admin";


        //        //loCls = new GSM05500Cls();
        //        //loResult = loCls.GetAllCurrency(loDbPar);
        //        //loRtn = new GSM05500ListDTO() { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }

        //    loException.ThrowExceptionIfErrors();

        //    return loRtn;
        //}


        [HttpPost]
        public IAsyncEnumerable<GSM05500DTO> GetAllCurrencyStream()
        {
            _logger.LogInfo("Begin || GetAllCurrencyStream(Controller)");
            R_Exception loExceptionception = new R_Exception();
            GSM05500DBParameter loDbPar;
            List<GSM05500DTO> loRtnTmp;
            GSM05500Cls loCls;
            IAsyncEnumerable<GSM05500DTO> loRtn = null;

            try
            {
                loDbPar = new GSM05500DBParameter();
                _logger.LogInfo("Set Parameter || GetAllCurrencyStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM05500Cls();
                _logger.LogInfo("Run GetAllCurrencyListCls || GetAllCurrencyStream(Controller)");
                loRtnTmp = loCls.GetAllCurrency(loDbPar);
                _logger.LogInfo("Run GetCurrencyStream || GetAllCurrencyStream(Controller)");
                loRtn = GetCurrencyStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loExceptionception.Add(ex);
                _logger.LogError(loExceptionception);
            }

            EndBlock:
            loExceptionception.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllCurrencyStream(Controller)");
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