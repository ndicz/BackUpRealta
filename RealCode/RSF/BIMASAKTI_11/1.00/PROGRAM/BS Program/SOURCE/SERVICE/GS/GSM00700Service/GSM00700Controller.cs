using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Back;
using GSM00700Common;
using GSM00700Common.DTO;
using GSM00700Common.DTO.Report_DTO_GSM00700;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM00700Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM00700Controller : ControllerBase, IGSM00700
    {

        private LogGSM00700Common _logger;

        public GSM00700Controller(ILogger<GSM00700Controller> logger)
        {
            LogGSM00700Common.R_InitializeLogger(logger);
            _logger = LogGSM00700Common.R_GetInstanceLogger();
        }


        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM00700DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM00700DTO> poParameter)
        {
            _logger.LogInfo("Begin || GetRecordCashFlowGroup(Controller)");

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM00700DTO>();
            GSM00700DBParameter loDbPar = new GSM00700DBParameter();

            try
            {

                var loCls = new GSM00700Cls();

                _logger.LogInfo("Set Parameter || GetRecordCashFlowGroup(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetRecordCashFlowGroupCls || GetRecordCashFlowGroup(Controller)");

                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(ex);

            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordCashFlowGroup(Controller)");
            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GSM00700DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM00700DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceSaveCashFlowGroup(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM00700DTO> loRtn = null;
            GSM00700Cls loCls;


            try
            {
                loCls = new GSM00700Cls();
                _logger.LogInfo("Set Parameter || ServiceSaveCashFlowGroup(Controller)");
                loRtn = new R_ServiceSaveResultDTO<GSM00700DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run ServiceSaveCls || ServiceSaveCashFlowGroup(Controller)");

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceSaveCashFlowGroup(Controller)");
            return loRtn;

        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM00700DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceDeleteCashFlowGroup(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM00700Cls loCls;
            try
            {
                _logger.LogInfo("Set Parameter || ServiceDeleteCashFlowGroup(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run ServiceDeleteCls || ServiceDeleteCashFlowGroup(Controller)");

                loCls = new GSM00700Cls();
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceDeleteCashFlowGroup(Controller)");
            return loRtn;
        }
        //streaming
        [HttpPost]
        public IAsyncEnumerable<GSM00700DTO> GetAllCashFlowGroupStream()
        {
            _logger.LogInfo("Begin || GetAllCashFlowGroupStream(Controller)");
            R_Exception loException = new R_Exception();
            GSM00700DBParameter loDbPar;
            List<GSM00700DTO> loRtnTmp;
            GSM00700Cls loCls;
            IAsyncEnumerable<GSM00700DTO> loRtn = null;
            try
            {
                _logger.LogInfo("Set Parameter || GetAllCashFlowGroupStream(Controller)");
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls = new GSM00700Cls();

                _logger.LogInfo("Run GetAllCashFlowGr oupStreamCls || GetAllCashFlowGroupStream(Controller)");
                loRtnTmp = loCls.GetCashFlowGroupList(loDbPar);
                loRtn = GetAllCashFlowGroupStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllCashFlowGroupStream(Controller)");
            return loRtn;
        }
        //list
        //[HttpPost]
        //public GSM00700ListDTO GetAllCashFlowGroupList()
        //{
        //    R_Exception loEx = new R_Exception();
        //    GSM00700ListDTO loRtn = null;
        //    List<GSM00700DTO> loResult;
        //    GSM00700DBParameter loDbPar;
        //    GSM00700Cls loCls;

        //    try
        //    {
        //        loDbPar = new GSM00700DBParameter();
        //        loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        //loDbPar.CUSER_ID = "Admin";

        //        loCls = new GSM00700Cls();
        //        loResult = loCls.GetCashFlowGroupList(loDbPar);
        //        loRtn = new GSM00700ListDTO() { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();

        //    return loRtn;
        //}
        [HttpPost]
        public GSM00700CashFlowGroupTypeListDTO GetListCashFlowGroupType()
        {
            _logger.LogInfo("Begin || GetCashFlowGroupType(Controller)");
            R_Exception loEx = new R_Exception();
            GSM00700CashFlowGroupTypeListDTO loRtn = null;
            List<GSM00700CashFlowGroupTypeDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00700Cls loCls;

            try
            {
                _logger.LogInfo("Set Parameter || GetAllCashFlowGroupStream(Controller)");
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loDbPar.CCOMPANY_ID = "RCD";

                loCls = new GSM00700Cls();
                _logger.LogInfo("Run GetCashFlowGroupTypeCls || GetAllCashFlowGroupStream(Controller)");
                loResult = loCls.CashFlowGroupType(loDbPar);
                loRtn = new GSM00700CashFlowGroupTypeListDTO() { Data = loResult };

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllCashFlowGroupStream(Controller)");
            return loRtn;
        }
        [HttpPost]  
        public IAsyncEnumerable<GSM00700DTO> GetPrintCashFlow(GSM00700PrintCashFlowParameterDTo poParameter)
        {
            _logger.LogInfo("Begin || GetPrintCashFlowGroup(Controller)");
            R_Exception loEx = new R_Exception();
            GSM00700PrintCashFlowParameterDTo loDbPar;
            List<GSM00700DTO> loRtnTmp;
            GSM00700Cls loCls;
            IAsyncEnumerable<GSM00700DTO> loRtn = null;
            try
            {
                _logger.LogInfo("Set Parameter || GetPrintCashFlowGroup(Controller)");
                loDbPar = poParameter;

                loCls = new GSM00700Cls();
                _logger.LogInfo("Run GetPrintCashFlowGroup || GetPrintCashFlowGroup(Controller)");

                loRtnTmp = loCls.GetPrintParam(loDbPar);
                loRtn = GetCashFlowPrint(loRtnTmp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPrintCashFlowGroup(Controller)");

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM00700DTO> GetYearFromPrint()
        {
            _logger.LogInfo("Begin || GetYearFromPrintCashFlowGroup(Controller)");

            R_Exception loException = new R_Exception();
            GSM00700DBParameter loDbPar;
            List<GSM00700DTO> loRtnTmp;
            GSM00700Cls loCls;
            IAsyncEnumerable<GSM00700DTO> loRtn = null;
            try
            {
                loDbPar = new GSM00700DBParameter();
                _logger.LogInfo("Set Parameter || GetYearFromPrintCashFlowGroup(Controller)");

                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loCls = new GSM00700Cls();
                _logger.LogInfo("Run GetYearFromPrintCls || GetYearFromPrintCashFlowGroup(Controller)");

                loRtnTmp = loCls.YearFromComboBoxPrint(loDbPar);
                loRtn = GetYearFromPrintStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetYearFromPrintCashFlowGroup(Controller)");

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM00700DTO> GetYearToPrint()
        {
            _logger.LogInfo("Begin || GetYearToPrintCashFlowGroup(Controller)");

            R_Exception loException = new R_Exception();
            GSM00700DBParameter loDbPar;
            List<GSM00700DTO> loRtnTmp;
            GSM00700Cls loCls;
            IAsyncEnumerable<GSM00700DTO> loRtn = null;
            try
            {
                loDbPar = new GSM00700DBParameter();
                _logger.LogInfo("Set Parameter || GetYearToPrintCashFlowGroup(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loCls = new GSM00700Cls();
                _logger.LogInfo("Run GetYearFromPrintCls || GetYearToPrintCashFlowGroup(Controller)");

                loRtnTmp = loCls.YearToComboBoxPrint(loDbPar);
                loRtn = GetYearToPrintStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetYearToPrintCashFlowGroup(Controller)");

            return loRtn;
        }



        private async IAsyncEnumerable<GSM00700DTO> GetYearFromPrintStream(List<GSM00700DTO> poParameter)
        {
            foreach (GSM00700DTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<GSM00700DTO> GetYearToPrintStream(List<GSM00700DTO> poParameter)
        {
            foreach (GSM00700DTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<GSM00700DTO> GetAllCashFlowGroupStream(List<GSM00700DTO> poParameter)
        {
            foreach (GSM00700DTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<GSM00700DTO> GetCashFlowPrint(List<GSM00700DTO> poParameter)
        {
            foreach (GSM00700DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
