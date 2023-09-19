//using GSM00700Common.DTO.Upload_DTO;
//using GSM00700Common;
//using R_BusinessObjectFront;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using GSM00700Common.DTO.Upload_DTO_GSM00720;
//using R_APIClient;
//using R_BlazorFrontEnd.Exceptions;
//using System.Threading.Tasks;

//namespace GSM00700Model.Model
//{
//    public class GSM00720UploadModel : R_BusinessObjectServiceClientBase<GSM00720UploadCashFlowPlanDTO>, IUPLOAD00720
//    {
//        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
//        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM00720Upload";
//        private const string DEFAULT_MODULE = "gs";

//        public GSM00720UploadModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
//            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
//            bool plSendWithContext = true,
//            bool plSendWithToken = true) :
//            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
//        {
//        }


//        public IAsyncEnumerable<GSM00720UploadCashFlowPlanDTO> GetUploadListGSM00720()
//        {
//            throw new NotImplementedException();
//        }

//        public IAsyncEnumerable<GSM00720UploadCashFlowPlanErrorDTO> GetErrorListGSM00720()
//        {
//            throw new NotImplementedException();
//        }

//        public GSM00720UploadCashFlowPlanCheckResultDTO CheckUploadGSM00720()
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<GSM00720UploadCashFlowPlanErrorResultDTO> GetErrorProcessListAsync()
//        {
//            var loEx = new R_Exception();
//            List<GSM00720UploadCashFlowPlanErrorDTO> loResult = null;
//            GSM00720UploadCashFlowPlanErrorResultDTO loRtn = new GSM00720UploadCashFlowPlanErrorResultDTO();

//            try
//            {
//                R_HTTPClientWrapper.httpClientName = _HttpClientName;

//                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM00720UploadCashFlowPlanErrorDTO>(
//                    _RequestServiceEndPoint,
//                    nameof(IUPLOAD00710.GetErrorListGSM00710),
//                    DEFAULT_MODULE,
//                    _SendWithContext,
//                    _SendWithToken);

//                loRtn.Data = loResult;
//            }
//            catch (Exception ex)
//            {
//                loEx.Add(ex);
//            }

//            EndBlock:
//            loEx.ThrowExceptionIfErrors();

//            return loRtn;
//        }

//        public async Task<GSM00720UploadCashFlowPlanCheckResultDTO> CheckResultUpload()
//        {
//            var loEx = new R_Exception();
//            GSM00720UploadCashFlowPlanCheckResultDTO loRtn = new GSM00720UploadCashFlowPlanCheckResultDTO();

//            try
//            {
//                R_HTTPClientWrapper.httpClientName = _HttpClientName;

//                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM00720UploadCashFlowPlanCheckResultDTO>(
//                    _RequestServiceEndPoint,
//                    nameof(IUPLOAD00720.CheckUploadGSM00720),
//                    DEFAULT_MODULE,
//                    _SendWithContext,
//                    _SendWithToken);
//            }
//            catch (Exception ex)
//            {
//                loEx.Add(ex);
//            }

//        EndBlock:
//            loEx.ThrowExceptionIfErrors();

//            return loRtn;
//        }

//        public async Task<GSM00720UploadCashFlowPlanResultDTO> GetUploadList()
//        {
//            var loEx = new R_Exception();
//            List<GSM00720UploadCashFlowPlanDTO> loResult = null;
//            GSM00720UploadCashFlowPlanResultDTO loRtn = new GSM00720UploadCashFlowPlanResultDTO();
//            //R_ContextHeader loContextHeader = new R_ContextHeader();

//            try
//            {
//                R_HTTPClientWrapper.httpClientName = _HttpClientName;

//                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM00720UploadCashFlowPlanDTO>(
//                    _RequestServiceEndPoint,
//                    nameof(IUPLOAD00720.GetUploadListGSM00720),
//                    DEFAULT_MODULE,
//                    _SendWithContext,
//                    _SendWithToken);

//                loRtn.Data = loResult;
//            }
//            catch (Exception ex)
//            {
//                loEx.Add(ex);
//            }

//        EndBlock:
//            loEx.ThrowExceptionIfErrors();

//            return loRtn;
//        }

//    }
//}
