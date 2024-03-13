using System.Diagnostics;
using GSM10500Back;
using GSM10500Back.Activity;
using GSM10500Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM10500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM10500Controller : ControllerBase, IGSM10500
    {
        private readonly ActivitySource _activitySource;
        private LogGSM10500Common _logger;

        public GSM10500Controller(ILogger<GSM10500Controller> logger)
        {
            LogGSM10500Common.R_InitializeLogger(logger);
            _logger = LogGSM10500Common.R_GetInstanceLogger();
            _activitySource = GSM10500Activity.R_InitializeAndGetActivitySource(nameof(GSM10500Controller));
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM10500DTO> R_ServiceGetRecord(
            R_ServiceGetRecordParameterDTO<GSM10500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
            _logger.LogInfo("Begin || GetRecordAgeingHD(Controller)");
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM10500DTO>();

            try
            {
                _logger.LogInfo("Set Parameter || GetRecordAgeingHD(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new GSM10500Cls();

                _logger.LogInfo("Run GetRecordAgeingHDCls || GetRecordAgeingHD(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordAgeingHD(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM10500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM10500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceSave));
            _logger.LogInfo("Begin || ServiceSavedAgeingHD(Controller)");
            R_Exception loExceptionception = new R_Exception();
            R_ServiceSaveResultDTO<GSM10500DTO> loRtn = null;
            GSM10500Cls loCls;

            try
            {
                loCls = new GSM10500Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM10500DTO>();
                _logger.LogInfo("Set Parameter || ServiceSavedAgeingHD(Controller)");

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";   
                //poParameter.Entity.CUSER_ID = "Admin";
                _logger.LogInfo("Run SavedAgeingHDCls || ServiceSavedAgeingHD(Controller)");
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
            _logger.LogInfo("End || ServiceSavedAgeingHD(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM10500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceDelete));
            _logger.LogInfo("Begin || ServiceDeletedAgeingHD(Controller)");
            R_Exception loExceptionception = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM10500Cls loCls;

            try
            {
                _logger.LogInfo("Set Parameter || ServiceDeletedAgeingHD(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;


                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                loCls = new GSM10500Cls();
                _logger.LogInfo("Run DeletedAgeingHDCls || ServiceDeletedAgeingHD(Controller)");
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
            _logger.LogInfo("End || ServiceDeletedAgeingHD(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM10500DTO> GetAllAgeingHDStream()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetAllAgeingHDStream));
            _logger.LogInfo("Begin || GetAlldAgeingHDStream(Controller)");
            R_Exception loExceptionception = new R_Exception();
            GSM10500DBParameter loDbPar;
            List<GSM10500DTO> loRtnTmp;
            GSM10500Cls loCls;
            IAsyncEnumerable<GSM10500DTO> loRtn = null;

            try
            {
                loDbPar = new GSM10500DBParameter();
                _logger.LogInfo("Set Parameter || GetAlldAgeingHDStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM10500Cls();
                _logger.LogInfo("Run GetAlldAgeingHDListCls || GetAlldAgeingHDStream(Controller)");
                loRtnTmp = loCls.GetAllAgeingHD(loDbPar);
                _logger.LogInfo("Run GetdAgeingHDStream || GetAlldAgeingHDStream(Controller)");
                loRtn = GetAgeingHDStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loExceptionception.Add(ex);
                _logger.LogError(loExceptionception);
            }

            EndBlock:
            loExceptionception.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAlldAgeingHDStream(Controller)");
            return loRtn;
        }
[HttpPost]
        public IAsyncEnumerable<GSM10500RoundingModeDTO> GetRoundingMode()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetAllAgeingHDStream));
            _logger.LogInfo("Begin || GetAlldAgeingHDStream(Controller)");
            R_Exception loExceptionception = new R_Exception();
            GSM10500DBParameter loDbPar;
            List<GSM10500RoundingModeDTO> loRtnTmp;
            GSM10500Cls loCls;
            IAsyncEnumerable<GSM10500RoundingModeDTO> loRtn = null;

            try
            {
                loDbPar = new GSM10500DBParameter();
                _logger.LogInfo("Set Parameter || GetAlldAgeingHDStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CULTURE = R_BackGlobalVar.CULTURE;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM10500Cls();
                _logger.LogInfo("Run GetAlldAgeingHDListCls || GetAlldAgeingHDStream(Controller)");
                loRtnTmp = loCls.GetAllRoundingMode(loDbPar);
                _logger.LogInfo("Run GetdAgeingHDStream || GetAlldAgeingHDStream(Controller)");
                loRtn = GetRoundingMode(loRtnTmp);
            }
            catch (Exception ex)
            {
                loExceptionception.Add(ex);
                _logger.LogError(loExceptionception);
            }

            EndBlock:
            loExceptionception.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAlldAgeingHDStream(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<GSM10500DTO> GetAgeingHDStream(List<GSM10500DTO> poParameter)
        {
            foreach (GSM10500DTO item in poParameter)
            {
                yield return item;
            }
        }
        
        private async IAsyncEnumerable<GSM10500RoundingModeDTO> GetRoundingMode(List<GSM10500RoundingModeDTO> poParameter)
        {
            foreach (GSM10500RoundingModeDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}