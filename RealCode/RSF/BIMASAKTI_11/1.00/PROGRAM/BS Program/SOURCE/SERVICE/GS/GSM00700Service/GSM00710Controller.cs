using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common;
using GSM00700Common.DTO;
using R_CommonFrontBackAPI;
using GSM00700Back;
using R_BackEnd;
using R_Common;

namespace GSM00700Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM00710Controller : ControllerBase, IGSM00710
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM00710DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM00710DTO> poParameter)
        {
            var loEx = new R_Exception();
            GSM00700DBParameter loDbPar = new GSM00700DBParameter();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM00710DTO>();
            try
            {
                var loCls = new GSM00710Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //loDbPar.CCASH_FLOW_CODE = poParameter.Entity.CCASH_FLOW_CODE;

                //loDbPar.CCASH_FLOW_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_CODE);
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "HPC";


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
        public R_ServiceSaveResultDTO<GSM00710DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM00710DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM00710DTO> loRtn = null;
            GSM00710Cls loCls;


            try
            {
                loCls = new GSM00710Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM00710DTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "HPC";

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM00710DTO> poParameter)
        {
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM00710Cls loCls;
            try
            {

                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "HPC";

                loCls = new GSM00710Cls();
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
        //[HttpPost]
        //public GSM00710ListDTO GetAllCashFlowList()
        //{
        //    R_Exception loEx = new R_Exception();
        //    GSM00710ListDTO loRtn = null;
        //    List<GSM00710DTO> loResult;
        //    GSM00700DBParameter loDbPar;
        //    GSM00710Cls loCls;

        //    try
        //    {
        //        loDbPar = new GSM00700DBParameter();
        //        loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
        //        loDbPar.CCASH_FLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE);

        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        //loDbPar.CUSER_ID = "HPC";
        //        //loDbPar.CCASH_FLOW_GROUP_CODE = "CF001";

        //        loCls = new GSM00710Cls();
        //        loResult = loCls.GetCashFlowList(loDbPar);
        //        loRtn = new GSM00710ListDTO() { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();

        //    return loRtn;
        //}
        [HttpPost]
        public GSM00710CashFlowTypeListDTO GetListCashFlowType()
        {
            R_Exception loEx = new R_Exception();
            GSM00710CashFlowTypeListDTO loRtn = null;
            List<GSM00710CashFlowTypeDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00710Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loDbPar.CCOMPANY_ID = "RCD";  

                loCls = new GSM00710Cls();
                loResult = loCls.CashFlowType(loDbPar);
                loRtn = new GSM00710CashFlowTypeListDTO() { Data = loResult };

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM00710DTO> GetAllCashFlowStream()
        {
            R_Exception loException = new R_Exception();
            GSM00700DBParameter loDbPar;
            List<GSM00710DTO> loRtnTmp;
            GSM00710Cls loCls;
            IAsyncEnumerable<GSM00710DTO> loRtn = null;
            try
            {
                loDbPar = new GSM00700DBParameter();
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CCASH_FLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE);

                loCls = new GSM00710Cls();
                loRtnTmp = loCls.GetCashFlowList(loDbPar);
                loRtn = GetAllCashFlowStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<GSM00710DTO> GetAllCashFlowStream(List<GSM00710DTO> poParameter)
        {
            foreach (GSM00710DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
