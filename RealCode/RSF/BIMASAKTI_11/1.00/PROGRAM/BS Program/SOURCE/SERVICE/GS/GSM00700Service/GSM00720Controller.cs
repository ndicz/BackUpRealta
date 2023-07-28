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
using System.Reflection;

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

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";
                //loDbPar.CCASH_FLOW_GROUP_CODE = "CF0012";
                //loDbPar.CCASH_FLOW_CODE = "F001";
                //loDbPar.CCYEAR = "2023";
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

                //loDbPar.CCOMPANY_ID = "RCD";
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

        [HttpPost]
        public GSM00720CopyFromYearListDTO GetCopyFromYearList()
        {
            R_Exception loEx = new R_Exception();
            GSM00720CopyFromYearListDTO loRtn = null;
            List<GSM00720CopyFromYearDTO> loResult;
            GSM00700DBParameter loDbPar;

            GSM00720Cls loCls;
            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CFROM_CASH_FOW_FLAG = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CFROM_CASH_FOW_FLAG);
                loDbPar.CFROM_CASH_FLOW_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CFROM_CASH_FLOW_CODE);
                loDbPar.CFROM_YEAR = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CFROM_YEAR);
                loDbPar.CTO_CASH_FLOW_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CTO_CASH_FLOW_CODE);
                loDbPar.CTO_YEAR = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CTO_YEAR);

                loCls = new GSM00720Cls();
                loResult = loCls.CopyFromYear(loDbPar);
                loRtn = new GSM00720CopyFromYearListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public GSM00720CopyBaseAmountListDTO GetCopyBaseAmountList()
        {
            R_Exception loEx = new R_Exception();
            GSM00720CopyBaseAmountListDTO loRtn = null;
            List<GSM00720CopyBaseLocalAmountDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00720Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                loDbPar.CCASH_FLOW_GROUP = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_GROUP);
                loDbPar.CCASH_FLOW_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_CODE);
                loDbPar.CYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CYEAR);
                loDbPar.CCURRENCY_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCURRENCY_CODE);
                loDbPar.CCURRENCY_RATE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCURRENCY_RATE);
                loDbPar.INO_PERIOD_FROM = R_Utility.R_GetStreamingContext<Int32>(ContextConstantGSM00700.INO_PERIOD_FROM);
                loDbPar.INO_PERIOD_TO = R_Utility.R_GetStreamingContext<Int32>(ContextConstantGSM00700.INO_PERIOD_TO);

                loCls = new GSM00720Cls();
                loResult = loCls.UpdateBaseAmount(loDbPar);
                loRtn = new GSM00720CopyBaseAmountListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public GSM00720CopyLocalAmountListDTO GetCopyLocalAmountList()
        {
            R_Exception loEx = new R_Exception();
            GSM00720CopyLocalAmountListDTO loRtn = null;
            List<GSM00720CopyBaseLocalAmountDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00720Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                loDbPar.CCASH_FLOW_GROUP = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_GROUP);
                loDbPar.CCASH_FLOW_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_CODE);
                loDbPar.CYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CYEAR);
                loDbPar.CCURRENCY_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCURRENCY_CODE);
                loDbPar.CCURRENCY_RATE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCURRENCY_RATE);
                loDbPar.INO_PERIOD_FROM = R_Utility.R_GetStreamingContext<Int32>(ContextConstantGSM00700.INO_PERIOD_FROM);
                loDbPar.INO_PERIOD_TO = R_Utility.R_GetStreamingContext<Int32>(ContextConstantGSM00700.INO_PERIOD_TO);

                loCls = new GSM00720Cls();
                loResult = loCls.UpdateLocalAmount(loDbPar);
                loRtn = new GSM00720CopyLocalAmountListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public GSM00720CurrencyDTO GetCurrencyList()
        {

            R_Exception loEx = new R_Exception();
            GSM00720CurrencyDTO loRtn = null;
            GSM00720CurrencyDTO loResult;
            GSM00700DBParameter loDbPar;
            GSM00720Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                loCls = new GSM00720Cls();
                loResult = loCls.GetCurrency(loDbPar);
                loRtn = new GSM00720CurrencyDTO();
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
        public GSM00720InitialProsesListDTO GetInitialProses()
        {
            R_Exception loEx = new R_Exception();
            GSM00720InitialProsesListDTO loRtn = null;
            List<GSM00720InitialProsesDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00720Cls loCls;
            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                loCls = new GSM00720Cls();
                loResult = loCls.InitialProses(loDbPar);
                loRtn = new GSM00720InitialProsesListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]

            public GSM00710TemplateCashFlowUserInterface GetTemplate()
        {
            var loEx = new R_Exception();
            var loRtn = new GSM00710TemplateCashFlowUserInterface();

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Cash Flow Parameter.xlsx";

                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;

        }

            [HttpPost]

            public GSM00720TemplateCashFlowPlan GetTemplate720()
            {
                var loEx = new R_Exception();
                var loRtn = new GSM00720TemplateCashFlowPlan();

                try
                {
                    Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");
                    var lcResourceFile = "BIMASAKTI_GS_API.Template.Cash Flow Parameter.xlsx";

                    using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                    {
                        var ms = new MemoryStream();
                        resFilestream.CopyTo(ms);
                        var bytes = ms.ToArray();

                        loRtn.FileBytes = bytes;
                    }
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
