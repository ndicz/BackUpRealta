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
using Microsoft.Extensions.Logging;

namespace GSM05500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM05520Controller : ControllerBase, IGSM05520
    {

        private LogGSM05500Common _logger;

        public GSM05520Controller(ILogger<GSM05520Controller> logger)
        {
            LogGSM05500Common.R_InitializeLogger(logger);
            _logger = LogGSM05500Common.R_GetInstanceLogger();
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM05520DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05520DTO> poParameter)
        {
            _logger.LogInfo("Begin || GetRecordCurrencyRate(Controller)");
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM05520DTO>();

            try
            {
                _logger.LogInfo("Set Parameter || GetRecordCurrencyRate(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new GSM05520Cls();
                _logger.LogInfo("Run GetRecordCurrencyRateCls || GetRecordCurrencyRate(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordCurrencyRate(Controller)");
            return loRtn;

        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM05520DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05520DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceSaveCurrencyRate(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM05520DTO> loRtn = null;
            GSM05520Cls loCls;
            var loDbPar = new GSM05500DBParameter();

            try
            {
                loCls = new GSM05520Cls();
                _logger.LogInfo("Set Parameter || ServiceSaveCurrencyRate(Controller)");
                loRtn = new R_ServiceSaveResultDTO<GSM05520DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
               
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                _logger.LogInfo("Run SaveCurrencyRateCls || ServiceSaveCurrencyRate(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceSaveCurrencyRate(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05520DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceDeleteCurrencyRate(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM05520Cls loCls;

            try
            {
                _logger.LogInfo("Set Parameter || ServiceDeleteCurrencyRate(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls = new GSM05520Cls();
                _logger.LogInfo("Run DeleteCurrencyRateCls || ServiceDeleteCurrencyRate(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceDeleteCurrencyRate(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM05520DTO> GetAllRateStream()
        {
            _logger.LogInfo("Begin || GetAllRateStream(Controller)");
            R_Exception loException = new R_Exception();
            GSM05500DBParameter loDbPar;
            List<GSM05520DTO> loRtnTmp;
            GSM05520Cls loCls;
            IAsyncEnumerable<GSM05520DTO> loRtn = null;

            try
            {

                loDbPar = new GSM05500DBParameter();
                _logger.LogInfo("Set Parameter || GetAllRateStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CRATETYPE_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM05500.CRATETYPE_CODE);
                loDbPar.CRATE_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM05500.CRATE_DATE);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM05520Cls();
                _logger.LogInfo("Run GetAllCurrencyRateCls || GetAllRateStream(Controller)");
                loRtnTmp = loCls.GetAllCurrencyRate(loDbPar);
                _logger.LogInfo("Run GetCurrencyStream || GetAllRateStream(Controller)");
                loRtn = GetCurrencyStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllRateStream(Controller)");
            return loRtn;
        }

        //[HttpPost]
        //public GSM05520ListDTO GetAllRateList()
        //{
        //    R_Exception loEx = new R_Exception();
        //    GSM05520ListDTO loRtn = null;
        //    List<GSM05520DTO> loResult;
        //    GSM05500DBParameter loDbPar;
        //    GSM05520Cls loCls;

        //    try
        //    {
        //        loDbPar = new GSM05500DBParameter();
        //        loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
        //        loDbPar.CRATETYPE_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM05500.CRATETYPE_CODE);
        //        loDbPar.CRATE_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM05500.CRATE_DATE);
        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        //loDbPar.CUSER_ID = "Admin";


        //        loCls = new GSM05520Cls();
        //        loResult = loCls.GetAllCurrencyRate(loDbPar);
        //        loRtn = new GSM05520ListDTO() { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();

        //    return loRtn;
        //}


        [HttpPost]

        public GSM05520DTOLocalBaseCurrency GetLcCurrency()
        {
            _logger.LogInfo("Begin || GetLocalCurrencyRate(Controller)");
            R_Exception loException = new R_Exception();
            GSM05520DTOLocalBaseCurrency loRtn = null;
            GSM05520DTOLocalBaseCurrency loResult;
            GSM05500DBParameter loDbPar;
            GSM05520Cls loCls;

            try
            {
                loDbPar = new GSM05500DBParameter();
                _logger.LogInfo("Set Parameter || GetLocalCurrencyRate(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";


                loCls = new GSM05520Cls();
                _logger.LogInfo("Run GetLocalCurrencyCls || GetLocalCurrencyRate(Controller)");
                loResult = loCls.GetLocalCurrency(loDbPar);
                loRtn = new GSM05520DTOLocalBaseCurrency();
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetLocalCurrencyRate(Controller)");
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM05520DTOGetRateType> GetAllRateTypeStream()
        {
            _logger.LogInfo("Begin || GetAllRateTypeStream(Controller)");
            R_Exception loException = new R_Exception();
            GSM05500DBParameter loDbPar;
            List<GSM05520DTOGetRateType> loRtnTmp;
            GSM05520Cls loCls;
            IAsyncEnumerable<GSM05520DTOGetRateType> loRtn = null;
            try
            {
                loDbPar = new GSM05500DBParameter();
                _logger.LogInfo("Set Parameter || GetAllRateTypeStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
        
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM05520Cls();
                _logger.LogInfo("Run GetRateTypeCls || GetAllRateTypeStream(Controller)");
                loRtnTmp = loCls.GetRateType(loDbPar);
                _logger.LogInfo("Run GetRateTypeStream || GetAllRateTypeStream(Controller)");
                loRtn = GetRateType(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllRateTypeStream(Controller)");
            return loRtn;
        }
        //public IAsyncEnumerable<GSM05520DTOLocalBaseCurrency> GwtAllLcCurrencyStream()
        //{
        //    R_Exception loException = new R_Exception();
        //    GSM05500DBParameter loDbPar;
        //    List<GSM05520DTOLocalBaseCurrency> loRtnTmp;
        //    GSM05520Cls loCls;
        //    IAsyncEnumerable<GSM05520DTOLocalBaseCurrency> loRtn = null;
        //    try
        //    {
        //        loDbPar = new GSM05500DBParameter();
        //        loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        //loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        //loDbPar.CUSER_ID = "Admin";

        //        loCls = new GSM05520Cls();
        //        loRtnTmp = loCls.GetLocalCurrency(loDbPar);
        //        loRtn = GetLocalCurrency(loRtnTmp);
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }

        //    EndBlock:
        //    loException.ThrowExceptionIfErrors();

        //    return loRtn;
        //}
        //[HttpPost]
        //public GSM05520ListDTOGetRateType GetListRateType()
        //{
        //    R_Exception loEx = new R_Exception();
        //    GSM05520ListDTOGetRateType loRtn = null;
        //    List<GSM05520DTOGetRateType> loResult;
        //    GSM05500DBParameter loDbPar;
        //    GSM05520Cls loCls;

        //    try
        //    {
        //        loDbPar = new GSM05500DBParameter();
        //        loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        //loDbPar.CUSER_ID = "Admin";


        //        loCls = new GSM05520Cls();
        //        loResult = loCls.GetRateType(loDbPar);
        //        loRtn = new GSM05520ListDTOGetRateType() { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();

        //    return loRtn;
        //}

        private async IAsyncEnumerable<GSM05520DTO> GetCurrencyStream(List<GSM05520DTO> poParameter)
        {
            foreach (GSM05520DTO item in poParameter)
            {
                yield return item;
            }
        }
        private async IAsyncEnumerable<GSM05520DTOGetRateType> GetRateType(List<GSM05520DTOGetRateType> poParameter)
        {
            foreach (GSM05520DTOGetRateType item in poParameter)
            {
                yield return item;
            }
        }
        private async IAsyncEnumerable<GSM05520DTOLocalBaseCurrency> GetLocalCurrency(List<GSM05520DTOLocalBaseCurrency> poParameter)
        {
            foreach (GSM05520DTOLocalBaseCurrency item in poParameter)
            {
                yield return item;
            }
        }
    }
}
