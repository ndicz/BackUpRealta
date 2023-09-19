//using R_BusinessObjectFront;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using GSM00700Common;
//using GSM00700Common.DTO.Upload_DTO;
//using GSM00710Common.DTO.Upload_DTO_GSM00710;
//using R_APIClient;
//using R_BlazorFrontEnd.Exceptions;

//namespace GSM00700Model.Model
//{
//    public class GSM00710UploadModel : R_BusinessObjectServiceClientBase<GSM00710UploadCashFlowDTO>, IUPLOAD00710
//    {
//        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
//        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM00710Upload";
//        private const string DEFAULT_MODULE = "gs";

//        public GSM00710UploadModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
//            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
//            bool plSendWithContext = true,
//            bool plSendWithToken = true) :
//            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
//        {
//        }


//        //public IAsyncEnumerable<GSM00710UploadCashFlowDTO> GetUploadListGSM00710()
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //public IAsyncEnumerable<GSM00710UploadCashFlowErrorDTO> GetErrorListGSM00710()
//        //{
//        //    throw new NotImplementedException();
//        //}

//        public GSM00710UploadCashFlowCheckRessultDTO CheckUploadGSM00710()
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<GSM00710UploadCashFlowErrorResultDTO> GetErrorProcessListAsync()
//        {
//            var loEx = new R_Exception();
//            List<GSM00710UploadCashFlowErrorDTO> loResult = null;
//            GSM00710UploadCashFlowErrorResultDTO loRtn = new GSM00710UploadCashFlowErrorResultDTO();

//            try
//            {
//                R_HTTPClientWrapper.httpClientName = _HttpClientName;

//                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM00710UploadCashFlowErrorDTO>(
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
//        public async Task<GSM00710UploadCashFlowCheckRessultDTO> CheckResultUpload()
//        {
//            var loEx = new R_Exception();
//            GSM00710UploadCashFlowCheckRessultDTO loRtn = new GSM00710UploadCashFlowCheckRessultDTO();

//            try
//            {
//                //R_HTTPClientWrapper.httpClientName = _HttpClientName;

//                //loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM00710UploadCashFlowCheckRessultDTO>(
//                //    _RequestServiceEndPoint,
//                //    nameof(IUPLOAD00710.CheckUploadGSM00710),
//                //    DEFAULT_MODULE,
//                //    _SendWithContext,
//                //    _SendWithToken);
//            }
//            catch (Exception ex)
//            {
//                loEx.Add(ex);
//            }

//        EndBlock:
//            loEx.ThrowExceptionIfErrors();

//            return loRtn;
//        }

//        public async Task<GSM00710UploadCashFlowResultDTO> GetUploadList()
//        {
//            var loEx = new R_Exception();
//            List<GSM00710UploadCashFlowDTO> loResult = null;
//            GSM00710UploadCashFlowResultDTO loRtn = new GSM00710UploadCashFlowResultDTO();
//            //R_ContextHeader loContextHeader = new R_ContextHeader();

//            try
//            {
//                R_HTTPClientWrapper.httpClientName = _HttpClientName;

//                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM00710UploadCashFlowDTO>(
//                    _RequestServiceEndPoint,
//                    nameof(IUPLOAD00710.GetUploadListGSM00710),
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

//        public IAsyncEnumerable<GSM00710UploadDTO> GetUploadListGSM00710()
//        {
//            throw new NotImplementedException();
//        }

//        public IAsyncEnumerable<GSM00710UploadErrorValidateDTO> GetErrorListGSM00710()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
