using System;
using System.Collections.Generic;
using System.Linq;
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
using static GSM05500Common.DTO.GSM05510ListDTO;

namespace GSM05500Service
{   [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM05510Controller : ControllerBase, IGSM05510
    {

        private LogGSM05500Common _logger;

        public GSM05510Controller(ILogger<GSM05510Controller> logger)
        {
            LogGSM05500Common.R_InitializeLogger(logger);
            _logger = LogGSM05500Common.R_GetInstanceLogger();
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM05510DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05510DTO> poParameter)
        {
            _logger.LogInfo("Begin || GetRecordRateType(Controller)");
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM05510DTO>();

            try
            {
                _logger.LogInfo("Set Parameter || GetRecordRateType(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new GSM05510Cls();

                _logger.LogInfo("Run GetRecordRateTypeCls || GetRecordRateType(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordRateType(Controller)");
            return loRtn;

        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GSM05510DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05510DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceSaveRateType(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM05510DTO> loRtn = null;
            GSM05510Cls loCls;

            try
            {
                loCls = new GSM05510Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM05510DTO>();
                _logger.LogInfo("Set Parameter || ServiceSaveRateType(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                _logger.LogInfo("Run SaveRateTypeCls || ServiceSaveRateType(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceSaveRateType(Controller)");
            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05510DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceDeleteRateType(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM05510Cls loCls;

            try
            {
                _logger.LogInfo("Set Parameter || ServiceDeleteRateType(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                loCls = new GSM05510Cls();
                _logger.LogInfo("Run DeleteRateTypeCls || ServiceDeleteRateType(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceDeleteRateType(Controller)");
            return loRtn;
           
        }

        //[HttpPost]
        //public GSM05510ListDTO GetAllRateTypeList()
        //{
        //    R_Exception loEx = new R_Exception();
        //    GSM05510ListDTO loRtn = null;
        //    List<GSM05510DTO> loResult;
        //    GSM05500DBParameter loDbPar;
        //    GSM05510Cls loCls;

        //    try
        //    {
        //        //loDbPar = new GSM05500DBParameter();
        //        //loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        //loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        //loDbPar.CUSER_ID = "Admin";

        //        //loCls = new GSM05510Cls();
        //        //loResult = loCls.GetAllRateType(loDbPar);
        //        //loRtn = new GSM05510ListDTO { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();

        //    return loRtn;
        //}

        [HttpPost]
        public IAsyncEnumerable<GSM05510DTO> GetAllRateTypeStream()
        {
            _logger.LogInfo("Begin || GetAllRateTypeStream(Controller)");
            R_Exception loException = new R_Exception();
            GSM05500DBParameter loDbPar;
            List<GSM05510DTO> loRtnTmp;
            GSM05510Cls loCls;
            IAsyncEnumerable<GSM05510DTO> loRtn = null;

            try
            {

                loDbPar = new GSM05500DBParameter();
                _logger.LogInfo("Set Parameter || GetAllRateTypeStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM05510Cls();
                _logger.LogInfo("Run GetAllRateTypeListCls || GetAllRateTypeStream(Controller)");
                loRtnTmp = loCls.GetAllRateType(loDbPar);
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


        private async IAsyncEnumerable<GSM05510DTO> GetRateType(List<GSM05510DTO> poParameter)
        {
            foreach (GSM05510DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}   
