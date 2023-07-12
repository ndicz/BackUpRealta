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
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM02300Service
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM02300Controller : ControllerBase, IGSM02300
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM02300DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02300DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM02300DTO>();
            GSM02300DBParaneter loDbPar = new GSM02300DBParaneter();

            try
            {
                var loCls = new GSM02300Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
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
        public R_ServiceSaveResultDTO<GSM02300DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02300DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02300DTO> loRtn = null;
            GSM02300Cls loCls;

            try
            {
                loCls = new GSM02300Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM02300DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02300DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02300Cls loCls;
            try
            {

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";

                loCls = new GSM02300Cls();
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
        public GSM02300ListDTO GetAllProperty()
        {
            R_Exception loEx = new R_Exception();
            GSM02300ListDTO loRtn = null;
            List<GSM02300DTO> loResult;
            GSM02300DBParaneter loDbPar;
            GSM02300Cls loCls;

            try
            {
                loDbPar = new GSM02300DBParaneter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM02300Cls();
                loResult = loCls.GetAllProperty(loDbPar);
                loRtn = new GSM02300ListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public GSM02300ListPropertyTypeDTO GetPropertyType()
        {
            R_Exception loEx = new R_Exception();
            GSM02300ListPropertyTypeDTO loRtn = null;
            List<GSM02300PropertyTypeDTO> loResult;
            GSM02300DBParaneter loDbPar;
            GSM02300Cls loCls;

            try
            {
                loDbPar = new GSM02300DBParaneter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM02300Cls();
                loResult = loCls.GetPropertyType(loDbPar);
                loRtn = new GSM02300ListPropertyTypeDTO() { Data = loResult };
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
