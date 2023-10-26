using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GSM02300Back;
using GSM02300Common;
using GSM02300Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM02300Service
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM02300Controller : ControllerBase, IGSM02300
    {

        private LogGSM02300Common _logger;

        public GSM02300Controller(ILogger<GSM02300Controller> logger)
        {
            LogGSM02300Common.R_InitializeLogger(logger);
            _logger = LogGSM02300Common.R_GetInstanceLogger();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM02300DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02300DTO> poParameter)
        {
            _logger.LogInfo("Begin || GetRecordPropertyType(Controller)");

            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM02300DTO>();
            GSM02300DBParaneter loDbPar = new GSM02300DBParaneter();

            try
            {
                var loCls = new GSM02300Cls();
                _logger.LogInfo("Set Parameter || GetRecordPropertyType(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetRecordPropertyTypeCls || GetRecordPropertyType(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordPropertyType(Controller)");
            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GSM02300DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02300DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceSavePropertyType(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02300DTO> loRtn = null;
            GSM02300Cls loCls;

            try
            {
                loCls = new GSM02300Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM02300DTO>();
                _logger.LogInfo("Set Parameter || ServiceSavePropertyType(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                _logger.LogInfo("Run ServiceSavePropertyTypeCls || ServiceSavePropertyType(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();   
            _logger.LogInfo("End || ServiceSavePropertyType(Controller)");
            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02300DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceDeletePropertyType(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02300Cls loCls;
            try
            {
                _logger.LogInfo("Set Parameter || ServiceDeletePropertyType(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                loCls = new GSM02300Cls();

                _logger.LogInfo("Run ServiceDeletePropertyTypeCls || ServiceDeletePropertyType(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceDeletePropertyType(Controller)");
            return loRtn;
        }
        //[HttpPost]
        //public GSM02300ListDTO GetAllProperty()
        //{
        //    R_Exception loException = new R_Exception();
        //    GSM02300ListDTO loRtn = null;
        //    List<GSM02300DTO> loResult;
        //    GSM02300DBParaneter loDbPar;
        //    GSM02300Cls loCls;

        //    try
        //    {
        //        loDbPar = new GSM02300DBParaneter();
        //        loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        //loDbPar.CUSER_ID = "Admin";

        //        loCls = new GSM02300Cls();
        //        loResult = loCls.GetAllProperty(loDbPar);
        //        loRtn = new GSM02300ListDTO() { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }

        //    loException.ThrowExceptionIfErrors();

        //    return loRtn;
        //}
        //[HttpPost]
        //public GSM02300ListPropertyTypeDTO GetPropertyType()
        //{
        //    R_Exception loException = new R_Exception();
        //    GSM02300ListPropertyTypeDTO loRtn = null;
        //    List<GSM02300PropertyTypeDTO> loResult;
        //    GSM02300DBParaneter loDbPar;
        //    GSM02300Cls loCls;

        //    try
        //    {
        //        loDbPar = new GSM02300DBParaneter();
        //        loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        //loDbPar.CUSER_ID = "Admin";

        //        loCls = new GSM02300Cls();
        //        loResult = loCls.GetPropertyType(loDbPar);
        //        loRtn = new GSM02300ListPropertyTypeDTO() { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }

        //    loException.ThrowExceptionIfErrors();

        //    return loRtn;
        //}
        [HttpPost]
        public IAsyncEnumerable<GSM02300DTO> GetAllPropertyStream()
        {
            _logger.LogInfo("Begin || GetAllPropertyStream(Controller)");
            R_Exception loException = new R_Exception();
            GSM02300DBParaneter loDbPar;
            GSM02300Cls loCls;
            List<GSM02300DTO> loRtnTmp;
            IAsyncEnumerable<GSM02300DTO> loRtn = null;
            try
            {

                loDbPar = new GSM02300DBParaneter();
                _logger.LogInfo("Set Parameter || GetAllPropertyStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls = new GSM02300Cls();
                _logger.LogInfo("Run GetAllPropertyListCls || GetAllPropertyStream(Controller)");
                loRtnTmp = loCls.GetAllProperty(loDbPar);
                _logger.LogInfo("Run GetAllPropertyStream || GetAllPropertyStream(Controller)");
                loRtn = GetProperty(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllPropertyStream(Controller)");

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM02300PropertyTypeDTO> GetPropertyTypeStream()
        {
            _logger.LogInfo("Begin || GetPropertyTypeStream(Controller)");
            R_Exception loException = new R_Exception();
            GSM02300DBParaneter loDbPar;
            List<GSM02300PropertyTypeDTO> loRtnTmp;
            GSM02300Cls loCls;
            IAsyncEnumerable<GSM02300PropertyTypeDTO> loRtn = null;
            try
            {

                loDbPar = new GSM02300DBParaneter();
                _logger.LogInfo("Set Parameter || GetPropertyTypeStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls = new GSM02300Cls();
                _logger.LogInfo("Run GetPropertyTypeListCls || GetPropertyTypeStream(Controller)");
                loRtnTmp = loCls.GetPropertyType(loDbPar);
                _logger.LogInfo("Run GetPropertyTypeStream || GetPropertyTypeStream(Controller)");
                loRtn = GetPropertyType(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPropertyTypeStream(Controller)");
            return loRtn;
        }



        private async IAsyncEnumerable<GSM02300DTO> GetProperty(List<GSM02300DTO> poParameter)
        {
            foreach (GSM02300DTO item in poParameter)
            {
                yield return item;
            }
        }
        private async IAsyncEnumerable<GSM02300PropertyTypeDTO> GetPropertyType(List<GSM02300PropertyTypeDTO> poParameter)
        {
            foreach (GSM02300PropertyTypeDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
