using System.Diagnostics;
using GSM10500Back;
using GSM10500Back.Activity;
using GSM10500Common.DTO;
using GSM10510Back;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM10500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM10510Controller : ControllerBase, IGSM10510
    {
        private readonly ActivitySource _activitySource;
        private LogGSM10500Common _logger;

        public GSM10510Controller(ILogger<GSM10510Controller> logger)
        {
            LogGSM10500Common.R_InitializeLogger(logger);
            _logger = LogGSM10500Common.R_GetInstanceLogger();
            _activitySource = GSM10510Activity.R_InitializeAndGetActivitySource(nameof(GSM10510Controller));
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM10510DTO> R_ServiceGetRecord(
            R_ServiceGetRecordParameterDTO<GSM10510DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
            _logger.LogInfo("Begin || GetRecordAgeingHD(Controller)");
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM10510DTO>();

            try
            {
                _logger.LogInfo("Set Parameter || GetRecordAgeingHD(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                // poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new GSM10510Cls();

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
        public R_ServiceSaveResultDTO<GSM10510DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM10510DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceSave));
            _logger.LogInfo("Begin || ServiceSavedAgeingHD(Controller)");
            R_Exception loExceptionception = new R_Exception();
            R_ServiceSaveResultDTO<GSM10510DTO> loRtn = null;
            GSM10510Cls loCls;

            try
            {
                loCls = new GSM10510Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM10510DTO>();
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM10510DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceDelete));
            _logger.LogInfo("Begin || ServiceDeletedAgeingHD(Controller)");
            R_Exception loExceptionception = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM10510Cls loCls;

            try
            {
                _logger.LogInfo("Set Parameter || ServiceDeletedAgeingHD(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;


                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                loCls = new GSM10510Cls();
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
        public IAsyncEnumerable<GSM10510DTO> GetAllAgeingDTStream()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetAllAgeingDTStream));
            _logger.LogInfo("Begin || GetAlldAgeingHDStream(Controller)");
            R_Exception loExceptionception = new R_Exception();
            GSM10500DBParameter loDbPar;
            List<GSM10510DTO> loRtnTmp;
            GSM10510Cls loCls;
            IAsyncEnumerable<GSM10510DTO> loRtn = null;

            try
            {
                loDbPar = new GSM10500DBParameter();
                _logger.LogInfo("Set Parameter || GetAlldAgeingHDStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CAGEING_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstanGSM10500.CAGEING_CODE);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM10510Cls();
                _logger.LogInfo("Run GetAlldAgeingHDListCls || GetAlldAgeingHDStream(Controller)");
                loRtnTmp = loCls.GetAllAgeingDT(loDbPar);
                _logger.LogInfo("Run GetdAgeingHDStream || GetAlldAgeingHDStream(Controller)");
                loRtn = GetAgeingDTStream(loRtnTmp);
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

        private async IAsyncEnumerable<GSM10510DTO> GetAgeingDTStream(List<GSM10510DTO> poParameter)
        {
            foreach (GSM10510DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}