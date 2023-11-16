
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM02000Back;
using LMM02000Common;
using LMM02000Common.DTO;
using R_CommonFrontBackAPI;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace LMM02000Services
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMM02000Controller : ControllerBase, ILMM02000
    {
        private LogLMM02000Common _logger;

        public LMM02000Controller(ILogger<LMM02000Controller> logger)
        {
            LogLMM02000Common.R_InitializeLogger(logger);
            _logger = LogLMM02000Common.R_GetInstanceLogger();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM02000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM02000DTO> poParameter)
        {
            _logger.LogInfo("Begin || GetRecordSalesman(Controller)");
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM02000DTO>();
            LMM02000DBParameter loDbPar;
            try
            {
                _logger.LogInfo("Set Parameter || GetRecordSalesman(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                var loCls = new LMM02000Cls();
                _logger.LogInfo("Run GetRecordSalesmanCls || GetRecordSalesman(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordSalesman(Controller)");
            return loRtn;
        }
        
        [HttpPost]
        public R_ServiceSaveResultDTO<LMM02000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM02000DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceSaveSalesman(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM02000DTO> loRtn = null;
            LMM02000Cls loCls;
            var loDbPar = new LMM02000DBParameter();
            try
            {
                loCls = new LMM02000Cls();
                loRtn = new R_ServiceSaveResultDTO<LMM02000DTO>();
                _logger.LogInfo("Set Parameter || ServiceSaveSalesman(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                _logger.LogInfo("Run SaveSalesmanCls || ServiceSaveSalesman(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceSaveSalesman(Controller)");
            return loRtn;
        }
       
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02000DTO> poParameter)
        {
            _logger.LogInfo("Begin || ServiceDeleteSalesman(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            LMM02000Cls loCls;

            try
            {
                _logger.LogInfo("Set Parameter || ServiceDeleteSalesman(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                loCls = new LMM02000Cls();
                _logger.LogInfo("Run DeleteSalesmanCls || ServiceDeleteSalesman(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceDeleteSalesman(Controller)");
            return loRtn;
        }
        
        [HttpPost]
        public IAsyncEnumerable<LMM02000DTO> GetAllLMM02000Stream()
        {
            _logger.LogInfo("Begin || GetAllSalesmanStream(Controller)");
            R_Exception loException = new R_Exception();
            LMM02000DBParameter loDbPar;
            List<LMM02000DTO> loRtnTmp;
            LMM02000Cls loCls;
            IAsyncEnumerable<LMM02000DTO> loRtn = null;

            try
            {

                loDbPar = new LMM02000DBParameter();
                _logger.LogInfo("Set Parameter || GetAllSalesmanStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new LMM02000Cls();
                _logger.LogInfo("Run GetAllSalesmanListCls || GetAllSalesmanStream(Controller)");
                loRtnTmp = loCls.GetListSalesman(loDbPar);
                _logger.LogInfo("End Run GetAllSalesmanStream || GetAllSalesmanStream(Controller)");
                loRtn = GetSalesman(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllSalesmanStream(Controller)");
            return loRtn;
        }

        [HttpPost]
        public LMM02000ListDTO GetAllLMM02000List()
        {
            _logger.LogInfo("Begin || GetAllSalesmanList(Controller)");
            R_Exception loException = new R_Exception();
            LMM02000ListDTO loRtn = null;
            List<LMM02000DTO> loResult;
            LMM02000DBParameter loDbPar;
            LMM02000Cls loCls;

            try
            {
                loDbPar = new LMM02000DBParameter();
                _logger.LogInfo("Set Parameter || GetAllSalesmanList(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";
                //loDbPar.CPROPERTY_ID = "ASHMD";

                loCls = new LMM02000Cls();
                _logger.LogInfo("Run GetAllSalesmanListCls || GetAllSalesmanList(Controller)");
                loResult = loCls.GetListSalesman(loDbPar);
                loRtn = new LMM02000ListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllSalesmanList(Controller)");
            return loRtn;
        }

        [HttpPost]
        public LMM02010ListDTO GetAllLMM02010List()
        {
            _logger.LogInfo("Begin || GetAllSalesmanList(Controller)");
            R_Exception loException = new R_Exception();
            LMM02010ListDTO loRtn = null;
            List<LMM02010DTO> loResult;
            LMM02000DBParameter loDbPar;
            LMM02000Cls loCls;

            try
            {
                loDbPar = new LMM02000DBParameter();
                _logger.LogInfo("Set Parameter || GetAllSalesmanList(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);
                loDbPar.CSALESMAN_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CSALESMAN_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";
                //loDbPar.CPROPERTY_ID = "ASHMD";
                //loDbPar.CSALESMAN_ID = "S0001";

                loCls = new LMM02000Cls();
                _logger.LogInfo("Run GetAllSalesmanListCls || GetAllSalesmanList(Controller)");
                loResult = loCls.GetListSalesmenDetail(loDbPar);
                loRtn = new LMM02010ListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }


            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllSalesmanList(Controller)");
            return loRtn;
        }
       
        [HttpPost]
        public LMM02000ListPropertyDTO GetLMM02000Property()
        {
            _logger.LogInfo("Begin || GetProperty(Controller)");
            R_Exception loException = new R_Exception();
            LMM02000ListPropertyDTO loRtn = null;
            List<LMM02000PropertyDTO> loResult;
            LMM02000DBParameter loDbPar;
            LMM02000Cls loCls;

            try
            {
                loDbPar = new LMM02000DBParameter();
                _logger.LogInfo("Set Parameter || GetProperty(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new LMM02000Cls();
                _logger.LogInfo("Run GetPropertyCls || GetProperty(Controller)");
                loResult = loCls.GetAllPropertyList(loDbPar);
                loRtn = new LMM02000ListPropertyDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetProperty(Controller)");
            return loRtn;
        }
       
        [HttpPost]
        public IAsyncEnumerable<LMM02000PropertyDTO> GetAllLMM02000PropertyStream()
        {
            _logger.LogInfo("Begin || GetAllPropertyStream(Controller)");
            R_Exception loException = new R_Exception();
            LMM02000DBParameter loDbPar;
            List<LMM02000PropertyDTO> loRtnTmp;
            LMM02000Cls loCls;
            IAsyncEnumerable<LMM02000PropertyDTO> loRtn = null;

            try
            {

                loDbPar = new LMM02000DBParameter();
                _logger.LogInfo("Set Parameter || GetAllPropertyStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                //loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new LMM02000Cls();
                _logger.LogInfo("Run GetAllPropertyListCls || GetAllPropertyStream(Controller)");
                loRtnTmp = loCls.GetAllPropertyList(loDbPar);
                _logger.LogInfo("Run GetPropertyStream || GetAllPropertyStream(Controller)");
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
        public LMM02000ListGenderTypeDTO GetGender()
        {
            _logger.LogInfo("Begin || GetGender(Controller)");
            R_Exception loException = new R_Exception();
            LMM02000ListGenderTypeDTO loRtn = null;
            List<LMM02000GenderTypeDTO> loResult;
            LMM02000DBParameter loDbPar;
            LMM02000Cls loCls;

            try
            {
                loDbPar = new LMM02000DBParameter();
                _logger.LogInfo("Set Parameter || GetGender(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                loCls = new LMM02000Cls();
                _logger.LogInfo("Run GetGenderCls || GetGender(Controller)");
                loResult = loCls.GetGender(loDbPar);
                loRtn = new LMM02000ListGenderTypeDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetGender(Controller)");
            return loRtn;
        }
        
        [HttpPost]
        public LMM02000ListSalesmanTypeDTO GetSalesmanType()
        {
            _logger.LogInfo("Begin || GetSalesmanType(Controller)");
            R_Exception loException = new R_Exception();
            LMM02000ListSalesmanTypeDTO loRtn = null;
            List<LMM02000SalesmanTypeDTO> loResult;
            LMM02000DBParameter loDbPar;
            LMM02000Cls loCls;

            try
            {
                loDbPar = new LMM02000DBParameter();
                _logger.LogInfo("Set Parameter || GetSalesmanType(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                loCls = new LMM02000Cls();
                _logger.LogInfo("Run GetSalesmanTypeCls || GetSalesmanType(Controller)");
                loResult = loCls.GetSalesmanType(loDbPar);
                loRtn = new LMM02000ListSalesmanTypeDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSalesmanType(Controller)");
            return loRtn;
        }
       
        [HttpPost]
        public LMM02000ActiveInactiveDTO GetActiveInactive(LMM02000ActiveInactiveParam poParamDto)
        {
            _logger.LogInfo("Begin || GetActiveInactive(Controller)");
            R_Exception loException = new R_Exception();
            LMM02000DBParameter loParam = new LMM02000DBParameter();
            LMM02000ActiveInactiveDTO loRtn = new LMM02000ActiveInactiveDTO();
            LMM02000Cls loCls = new LMM02000Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetActiveInactive(Controller)");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = poParamDto.CPROPERTY_ID;
                loParam.CSALESMAN_ID = poParamDto.CSALESMAN_ID;
                loParam.LACTIVE = poParamDto.LACTIVE;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loParam.CCOMPANY_ID = "RCD";
                //loParam.CPROPERTY_ID = "ASHMD";
                //loParam.CSALESMAN_ID = "S0001";
                //loParam.LACTIVE = false;
                //loParam.CUSER_ID = "Admin";
                _logger.LogInfo("Run ActiveInactiveCls || GetActiveInactive(Controller)");
                loCls.RSP_GS_ACTIVE_INACTIVE_LMM02000(loParam);
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
       
        [HttpPost]
        public LMM02000Template GetTemplate()
        {
            var loException = new R_Exception();
            var loRtn = new LMM02000Template();

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_LM_API");
                var lcResourceFile = "BIMASAKTI_LM_API.Template.Salesman.xlsx";

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
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }


        private async IAsyncEnumerable<LMM02000DTO> GetSalesman(List<LMM02000DTO> poParameter)
        {
            foreach (LMM02000DTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<LMM02000PropertyDTO> GetProperty(List<LMM02000PropertyDTO> poParameter)
        {
            foreach (LMM02000PropertyDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}