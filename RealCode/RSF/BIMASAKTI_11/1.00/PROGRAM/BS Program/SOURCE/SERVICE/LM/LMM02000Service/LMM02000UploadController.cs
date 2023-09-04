using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM02000Back;
using LMM02000Common;
using LMM02000Common.DTO.UPLOAD_DTO_LMM02000;

namespace LMM02000Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMM02000UploadController : ControllerBase, IUPLOADLMM02000
    {
        [HttpPost]
        public IAsyncEnumerable<LMM02000UploadSalesmanDTO> GetUploadListLMM02000()
        {
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<LMM02000UploadSalesmanDTO> loRtn = null;
            List<LMM02000UploadSalesmanDTO> loParam = new List<LMM02000UploadSalesmanDTO>();
            LMM02000UploadSalesmanCls loCls = new LMM02000UploadSalesmanCls();
            List<LMM02000UploadSalesmanDTO> loTempRtn = null;

            try
            {
                //loParam = R_Utility.R_GetStreamingContext<List<LMM02000UploadSalesmanDTO>>(ContextConstantGSM00700.UPLOAD_Salesman_STREAMING_CONTEXT);

                foreach (var iten in loParam)
                {
                    iten.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }
                { }
                loTempRtn = loCls.GetLMM02000UploadSalesmanList(loParam);


                loRtn = GetUploadFloorStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<LMM02000UploadSalesmanErrorDTO> GetErrorListLMM02000()
        {
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<LMM02000UploadSalesmanErrorDTO> loRtn = null;
            LMM02000UploadSalesmanValidateCls loCls = new LMM02000UploadSalesmanValidateCls();
            List<LMM02000UploadSalesmanErrorDTO> loTempRtn = null;

            try
            {
                string lcKeyGuid = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM02000.UPLOAD_CENTER_ERROR_GUID_STREAMING_CONTEXT);

                loTempRtn = loCls.GetErrorProcess(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);

                //loRtn = GetErrorProcessStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM02000UploadSalesmanCheckResultDTO CheckUploadLMM02000()
        {
            R_Exception loException = new R_Exception();
            LMM02000UploadSalesmanCheckResultDTO loRtn = new LMM02000UploadSalesmanCheckResultDTO();
            LMM02000UploadSalesmanCheckUsedParameterDTO loParam = new LMM02000UploadSalesmanCheckUsedParameterDTO();
            LMM02000UploadSalesmanCls loCls = new LMM02000UploadSalesmanCls();

            try
            {
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam = R_Utility.R_GetContext<LMM02000UploadSalesmanCheckUsedParameterDTO>(ContextConstantGSM00700.UPLOAD_Salesman_CHECK_IS_Salesman_USED_CONTEXT);

                loRtn.Data = loCls.CheckIsSalesmanUsed(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }


        private async IAsyncEnumerable<LMM02000UploadSalesmanErrorDTO> GetUploadFloorStream(List<LMM02000UploadSalesmanErrorDTO> poParameter)
        {
            foreach (LMM02000UploadSalesmanErrorDTO item in poParameter)
            {
                yield return item;
            }
        }
        private async IAsyncEnumerable<LMM02000UploadSalesmanDTO> GetUploadFloorStream(List<LMM02000UploadSalesmanDTO> poParameter)
        {
            foreach (LMM02000UploadSalesmanDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
