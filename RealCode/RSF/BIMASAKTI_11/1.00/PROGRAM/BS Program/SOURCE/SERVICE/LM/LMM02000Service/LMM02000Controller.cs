
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

namespace LMM02000Services
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMM02000Controller : ControllerBase, ILMM02000
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM02000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM02000DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM02000DTO>();
            LMM02000DBParameter loDbPar;
            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                var loCls = new LMM02000Cls();

                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<LMM02000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM02000DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM02000DTO> loRtn = null;
            LMM02000Cls loCls;
            var loDbPar = new LMM02000DBParameter();
            try
            {
                loCls = new LMM02000Cls();
                loRtn = new R_ServiceSaveResultDTO<LMM02000DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02000DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            LMM02000Cls loCls;

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";
                loCls = new LMM02000Cls();
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<LMM02000DTO> GetAllLMM02000Stream()
        {
            R_Exception loException = new R_Exception();
            LMM02000DBParameter loDbPar;
            List<LMM02000DTO> loRtnTmp;
            LMM02000Cls loCls;
            IAsyncEnumerable<LMM02000DTO> loRtn = null;

            try
            {

                loDbPar = new LMM02000DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new LMM02000Cls();
                loRtnTmp = loCls.GetListSalesman(loDbPar);

                loRtn = GetSalesman(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public LMM02000ListDTO GetAllLMM02000List()
        {
            R_Exception loEx = new R_Exception();
            LMM02000ListDTO loRtn = null;
            List<LMM02000DTO> loResult;
            LMM02000DBParameter loDbPar;
            LMM02000Cls loCls;

            try
            {
                loDbPar = new LMM02000DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";
                //loDbPar.CPROPERTY_ID = "ASHMD";

                loCls = new LMM02000Cls();
                loResult = loCls.GetListSalesman(loDbPar);
                loRtn = new LMM02000ListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM02010ListDTO GetAllLMM02010List()
        {
            R_Exception loEx = new R_Exception();
            LMM02010ListDTO loRtn = null;
            List<LMM02010DTO> loResult;
            LMM02000DBParameter loDbPar;
            LMM02000Cls loCls;

            try
            {
                loDbPar = new LMM02000DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);
                loDbPar.CSALESMAN_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CSALESMAN_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";
                //loDbPar.CPROPERTY_ID = "ASHMD";
                //loDbPar.CSALESMAN_ID = "S0001";

                loCls = new LMM02000Cls();
                loResult = loCls.GetListSalesmenDetail(loDbPar);
                loRtn = new LMM02010ListDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public LMM02000ListPropertyDTO GetLMM02000Property()
        {
            R_Exception loEx = new R_Exception();
            LMM02000ListPropertyDTO loRtn = null;
            List<LMM02000PropertyDTO> loResult;
            LMM02000DBParameter loDbPar;
            LMM02000Cls loCls;

            try
            {
                loDbPar = new LMM02000DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new LMM02000Cls();
                loResult = loCls.GetAllPropertyList(loDbPar);
                loRtn = new LMM02000ListPropertyDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<LMM02000PropertyDTO> GetAllLMM02000PropertyStream()
        {
            R_Exception loException = new R_Exception();
            LMM02000DBParameter loDbPar;
            List<LMM02000PropertyDTO> loRtnTmp;
            LMM02000Cls loCls;
            IAsyncEnumerable<LMM02000PropertyDTO> loRtn = null;

            try
            {

                loDbPar = new LMM02000DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.CPROPERTY_ID);
                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new LMM02000Cls();
                loRtnTmp = loCls.GetAllPropertyList(loDbPar);

                loRtn = GetProperty(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM02000ListGenderTypeDTO GetGender()
        {
            R_Exception loEx = new R_Exception();
            LMM02000ListGenderTypeDTO loRtn = null;
            List<LMM02000GenderTypeDTO> loResult;
            LMM02000DBParameter loDbPar;
            LMM02000Cls loCls;

            try
            {
                loDbPar = new LMM02000DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                loCls = new LMM02000Cls();
                loResult = loCls.GetGender(loDbPar);
                loRtn = new LMM02000ListGenderTypeDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public LMM02000ListSalesmanTypeDTO GetSalesmanType()
        {
            R_Exception loEx = new R_Exception();
            LMM02000ListSalesmanTypeDTO loRtn = null;
            List<LMM02000SalesmanTypeDTO> loResult;
            LMM02000DBParameter loDbPar;
            LMM02000Cls loCls;

            try
            {
                loDbPar = new LMM02000DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                loCls = new LMM02000Cls();
                loResult = loCls.GetSalesmanType(loDbPar);
                loRtn = new LMM02000ListSalesmanTypeDTO { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public LMM02000ActiveInactiveDTO GetActiveInactive()
        {
            R_Exception loException = new R_Exception();
            LMM02000DBParameter loParam = new LMM02000DBParameter();
            LMM02000ActiveInactiveDTO loRtn = new LMM02000ActiveInactiveDTO();
            LMM02000Cls loCls = new LMM02000Cls();

            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstantLMM02000.CPROPERTY_ID);
                loParam.CSALESMAN_ID = R_Utility.R_GetContext<string>(ContextConstantLMM02000.CSALESMAN_ID);
                loParam.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstantLMM02000.LACTIVE);
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loParam.CCOMPANY_ID = "RCD";
                //loParam.CPROPERTY_ID = "ASHMD";
                //loParam.CSALESMAN_ID = "S0001";
                //loParam.LACTIVE = false;
                //loParam.CUSER_ID = "Admin";

                loCls.RSP_GS_ACTIVE_INACTIVE_LMM02000(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;

        }
        [HttpPost]
        public LMM02000Template GetTemplate()
        {
            var loEx = new R_Exception();
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
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

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