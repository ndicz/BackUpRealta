﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Back;
using GSM00700Common;
using GSM00700Common.DTO;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM00700Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM00700Controller : ControllerBase, IGSM00700
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM00700DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM00700DTO> poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM00700DTO>();
            try
            {
                var loCls = new GSM00700Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";

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
        public R_ServiceSaveResultDTO<GSM00700DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM00700DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM00700DTO> loRtn = null;
            GSM00700Cls loCls;


            try
            {
                loCls = new GSM00700Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM00700DTO>();
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM00700DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM00700Cls loCls;
            try
            {

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "Admin";

                loCls = new GSM00700Cls();
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
        public IAsyncEnumerable<GSM00700DTO> GetAllCashFlowGroupStream()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public GSM00700ListDTO GetAllCashFlowGroupList()
        {
            R_Exception loEx = new R_Exception();
            GSM00700ListDTO loRtn = null;
            List<GSM00700DTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00700Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                //loDbPar.CUSER_ID = "Admin";

                loCls = new GSM00700Cls();
                loResult = loCls.GetCashFlowGroupList(loDbPar);
                loRtn = new GSM00700ListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public GSM00700CashFlowGroupTypeListDTO GetListCashFlowGroupType()
        {
            R_Exception loEx = new R_Exception();
            GSM00700CashFlowGroupTypeListDTO loRtn = null;
            List<GSM00700CashFlowGroupTypeDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00700Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loDbPar.CCOMPANY_ID = "RCD";

                loCls = new GSM00700Cls();
                loResult = loCls.CashFlowGroupType(loDbPar);
                loRtn = new GSM00700CashFlowGroupTypeListDTO() { Data = loResult };

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GSM00700DTO> GetAllCashFlowStream(List<GSM00700DTO> poParameter)
        {
            foreach (GSM00700DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
