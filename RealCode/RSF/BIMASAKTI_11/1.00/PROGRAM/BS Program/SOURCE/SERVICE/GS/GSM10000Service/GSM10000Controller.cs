using System.Diagnostics;
using GSM10000Back;
using GSM10000Back.Activity;
using GSM10000Common;
using GSM10000Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_OpenTelemetry;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM10000Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM10000Controller : ControllerBase, IGSM10000
    {
        private readonly ActivitySource _activitySource;
        private LogGSM10000Common _logger;

        public GSM10000Controller(ILogger<GSM10000Controller> logger)
        {
            LogGSM10000Common.R_InitializeLogger(logger);
            _logger = LogGSM10000Common.R_GetInstanceLogger();
            _activitySource = GSM10000Activity.R_InitializeAndGetActivitySource(nameof(GSM10000Controller));
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM10000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM10000DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
            _logger.LogInfo("Begin || GetRecordHoliday(Controller)");
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM10000DTO>();

            try
            {
                _logger.LogInfo("Set Parameter || GetRecordHoliday(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new GSM10000Cls();

                _logger.LogInfo("Run GetRecordHolidayCls || GetRecordHoliday(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordHoliday(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM10000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM10000DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceSave));
            _logger.LogInfo("Begin || ServiceSavedHoliday(Controller)");
            R_Exception loExceptionception = new R_Exception();
            R_ServiceSaveResultDTO<GSM10000DTO> loRtn = null;
            GSM10000Cls loCls;

            try
            {
                loCls = new GSM10000Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM10000DTO>();
                _logger.LogInfo("Set Parameter || ServiceSavedHoliday(Controller)");

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                _logger.LogInfo("Run SavedHolidayCls || ServiceSavedHoliday(Controller)");
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
            _logger.LogInfo("End || ServiceSavedHoliday(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM10000DTO> poParameter)
        {
            throw new NotImplementedException();
            // using Activity activity = _activitySource.StartActivity(nameof(R_ServiceDelete));
            // _logger.LogInfo("Begin || ServiceDeletedHoliday(Controller)");
            // R_Exception loExceptionception = new R_Exception();
            // R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            // GSM10000Cls loCls;
            //
            // try
            // {
            //     _logger.LogInfo("Set Parameter || ServiceDeletedHoliday(Controller)");
            //     poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            //     poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            //
            //
            //     //poParameter.Entity.CCOMPANY_ID = "RCD";
            //     //poParameter.Entity.CUSER_ID = "Admin";
            //     loCls = new GSM10000Cls();
            //     _logger.LogInfo("Run DeletedHolidayCls || ServiceDeletedHoliday(Controller)");
            //     loCls.R_Delete(poParameter.Entity);
            // }
            // catch (Exception ex)
            // {
            //     loExceptionception.Add(ex);
            //     _logger.LogError(loExceptionception);
            // }
            //
            // ;
            // EndBlock:
            // loExceptionception.ThrowExceptionIfErrors();
            // _logger.LogInfo("End || ServiceDeletedHoliday(Controller)");
            // return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM10000DTO> GetAllHolidayStream()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetAllHolidayStream));
            _logger.LogInfo("Begin || GetAlldHolidayStream(Controller)");
            R_Exception loExceptionception = new R_Exception();
            GSM10000DBParameter loDbPar;
            List<GSM10000DTO> loRtnTmp;
            GSM10000Cls loCls;
            IAsyncEnumerable<GSM10000DTO> loRtn = null;

            try
            {
                loDbPar = new GSM10000DBParameter();
                _logger.LogInfo("Set Parameter || GetAlldHolidayStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM10000Cls();
                _logger.LogInfo("Run GetAlldHolidayListCls || GetAlldHolidayStream(Controller)");
                loRtnTmp = loCls.GetAllHoliday(loDbPar);
                _logger.LogInfo("Run GetdHolidayStream || GetAlldHolidayStream(Controller)");
                loRtn = GetHolidayStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loExceptionception.Add(ex);
                _logger.LogError(loExceptionception);
            }

            EndBlock:
            loExceptionception.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAlldHolidayStream(Controller)");
            return loRtn;
        }

        [HttpPost]
        public GSM10000ActiveInactiveDTO GetActiveInactive(LMM02000ActiveInactiveParam poParamDto)
        {
            using Activity activity = _activitySource.StartActivity("GetActiveInactive");
            _logger.LogInfo("Begin || GetActiveInactive(Controller)");
            R_Exception loException = new R_Exception();
            GSM10000DBParameter loParam = new GSM10000DBParameter();
            GSM10000ActiveInactiveDTO loRtn = new GSM10000ActiveInactiveDTO();
            GSM10000Cls loCls = new GSM10000Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetActiveInactive(Controller)");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CHOLIDAY_DATE = poParamDto.CHOLIDAY_DATE;
                loParam.LACTIVE = poParamDto.LACTIVE;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loParam.CCOMPANY_ID = "RCD";
                //loParam.CPROPERTY_ID = "ASHMD";
                //loParam.CSALESMAN_ID = "S0001";
                //loParam.LACTIVE = false;
                //loParam.CUSER_ID = "Admin";
                _logger.LogInfo("Run ActiveInactiveCls || GetActiveInactive(Controller)");
                loCls.ActiveInactive(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetActiveInactive(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<GSM10000DTO> GetHolidayStream(List<GSM10000DTO> poParameter)
        {
            foreach (GSM10000DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}