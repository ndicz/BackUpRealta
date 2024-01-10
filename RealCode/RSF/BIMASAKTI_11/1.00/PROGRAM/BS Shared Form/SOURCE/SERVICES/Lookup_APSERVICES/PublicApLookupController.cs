using System.Runtime.InteropServices;
using Lookup_APCOMMON;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00400;
using Lookup_APBACK;
using Lookup_APCOMMON.DTOs;
using Lookup_APCOMMON.DTOs.APL00500;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using Lookup_APCOMMON.Loggers;

namespace Lookup_APSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicApLookupController : ControllerBase, IPublicAPLookup
    {
        private LoggerAPPublicLookup _loggerAp;

        public PublicApLookupController(ILogger<LoggerAPPublicLookup> logger)
        {
            //Initial and Get Logger
            LoggerAPPublicLookup.R_InitializeLogger(logger);
            _loggerAp = LoggerAPPublicLookup.R_GetInstanceLogger();
        }


        [HttpPost]
        public IAsyncEnumerable<APL00100DTO> APL00100SupplierLookUp()
        {
            var loException = new R_Exception();
            IAsyncEnumerable<APL00100DTO> loRtn = null;
            _loggerAp.LogInfo("Start APL00100SupplierLookUp");

            try
            {
                var loCls = new PublicAPLookUpCls();
                var poParam = new APL00100ParameterDTO();
                
                _loggerAp.LogInfo("Set Param APL00100SupplierLookUp");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParam.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSEARCH_TEXT);

                _loggerAp.LogInfo("Call Back Method GetSupplierLookup");
                var loResult = loCls.SupplierLookup(poParam);
                
                _loggerAp.LogInfo("Call Stream Method Data APL00100SupplierLookUp");
                loRtn = GetStream<APL00100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00100SupplierLookUp");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APL00110DTO> APL00110SupplierInfoLookUp()
        {
            var loException = new R_Exception();
            IAsyncEnumerable<APL00110DTO> loRtn = null;
            _loggerAp.LogInfo("Start APL00110SupplierInfoLookUp");

            try
            {
                var loCls = new PublicAPLookUpCls();
                var poParam = new APL00110ParameterDTO();

                _loggerAp.LogInfo("Set Param APL00110SupplierInfoLookUp");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParam.CSUPPLIER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSUPPLIER_ID);

                _loggerAp.LogInfo("Call Back Method GetSupplierInfoLookup");
                var loResult = loCls.SupplierInfoLookup(poParam);

                _loggerAp.LogInfo("Call Stream Method Data APL00110SupplierInfoLookUp");
                loRtn = GetStream<APL00110DTO>(loResult);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00110SupplierInfoLookUp");
            return loRtn;

        }
        [HttpPost]
        public IAsyncEnumerable<APL00200DTO> APL00200ExpenditureLookUp()
        {
            var loException = new R_Exception();
            IAsyncEnumerable<APL00200DTO> loRtn = null;
            _loggerAp.LogInfo("Start APL00200ExpenditureLookUp");

            try
            {
                var loCls = new PublicAPLookUpCls();
                var poParam = new APL00200ParameterDTO();

                _loggerAp.LogInfo("Set Param APL00200ExpenditureLookUp");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParam.CTAX_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAX_DATE);
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParam.CTAXABLE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAXABLE_TYPE);
                poParam.CACTIVE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CACTIVE_TYPE);
                poParam.CCATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCATEGORY_ID);

                _loggerAp.LogInfo("Call Back Method GetExpenditureLookup");
                var loResult = loCls.ExpenditureLookup(poParam);

                _loggerAp.LogInfo("Call Stream Method Data APL00200ExpenditureLookUp");
                loRtn = GetStream<APL00200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00200ExpenditureLookUp");
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<APL00300DTO> APL00300ProductLookUp()
        {
            var loException = new R_Exception();
            IAsyncEnumerable<APL00300DTO> loRtn = null;
            _loggerAp.LogInfo("Start APL00300ProductLookUp");
            try
            {
                var loCls = new PublicAPLookUpCls();
                var poParam = new APL00300ParameterDTO();

                _loggerAp.LogInfo("Set Param APL00300ProductLookUp");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParam.CTAX_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAX_DATE);
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParam.CTAXABLE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTAXABLE_TYPE);
                poParam.CACTIVE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CACTIVE_TYPE);
                poParam.CCATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCATEGORY_ID);

                _loggerAp.LogInfo("Call Back Method GetProductLookup");
                var loResult = loCls.ProductLookup(poParam);

                _loggerAp.LogInfo("Call Stream Method Data APL00300ProductLookUp");
                loRtn = GetStream<APL00300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00300ProductLookUp");
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<APL00400DTO> APL00400ProductAllocationLookUp()
        {
            var loException = new R_Exception();
            IAsyncEnumerable<APL00400DTO> loRtn = null;
            _loggerAp.LogInfo("Start APL00400ProductAllocationLookUp");
            try
            {
                var loCls = new PublicAPLookUpCls();
                var poParam = new APL00400ParameterDTO();

                _loggerAp.LogInfo("Set Param APL00400ProductAllocationLookUp");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParam.CACTIVE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CACTIVE_TYPE);

                _loggerAp.LogInfo("Call Back Method GetProductAllocationLookup");
                var loResult = loCls.ProductAllocationLookup(poParam);

                _loggerAp.LogInfo("Call Stream Method Data APL00400ProductAllocationLookUp");
                loRtn = GetStream<APL00400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00400ProductAllocationLookUp");
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<APL00500DTO> APL00500TransactionLookup()
        {
            var loException = new R_Exception();
            IAsyncEnumerable<APL00500DTO> loRtn = null;
            _loggerAp.LogInfo("Start APL00400ProductAllocationLookUp");
            try
            {
                var loCls = new PublicAPLookUpCls();
                var poParam = new APL00500ParameterDTO();

                _loggerAp.LogInfo("Set Param APL00400ProductAllocationLookUp");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParam.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CTRANS_CODE);
                poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CDEPT_CODE);
                poParam.CSUPPLIER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CSUPPLIER_ID);
                poParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPERIOD);
                poParam.LHAS_REMAINING = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LHAS_REMAINING); 
                poParam.LNO_REMAINING = R_Utility.R_GetStreamingContext<bool>(ContextConstantPublicLookup.LNO_REMAINING);
                

                _loggerAp.LogInfo("Call Back Method GetProductAllocationLookup");
                var loResult = loCls.TransactionLookup(poParam);

                _loggerAp.LogInfo("Call Stream Method Data APL00400ProductAllocationLookUp");
                loRtn = GetStream<APL00500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00400ProductAllocationLookUp");
            return loRtn;
        }
        [HttpPost]
        public APL00500PeriodDTO APLInitiateTransactionLookup()
        {
            var loException = new R_Exception(); 
            APL00500PeriodDTO loRtn = null;
            _loggerAp.LogInfo("Start APL00400ProductAllocationLookUp");
            try
            {
                var loCls = new PublicAPLookUpCls();
                var poParam = new APL00500ParameterDTO();

                _loggerAp.LogInfo("Set Param APL00400ProductAllocationLookUp");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                

                _loggerAp.LogInfo("Call Back Method GetProductAllocationLookup");
                loRtn = loCls.InitialProcessApl00500(poParam);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00400ProductAllocationLookUp");
            return loRtn;
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }
    }
}