using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common.DTO.Upload_DTO;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace GSM00700Model
{
    public class GSM00710UploadViewModel :  R_ViewModel<GSM00710UploadCashFlowDTO>, R_IProcessProgressStatus
    {
        public Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            throw new NotImplementedException();
        }

        public Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            throw new NotImplementedException();
        }

        public Task ReportProgress(int pnProgress, string pcStatus)
        {
            throw new NotImplementedException();
        }
    }
}
