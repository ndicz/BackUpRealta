using System;
using System.Collections.Generic;
using System.Text;
using GSM00700Common.DTO.Upload_DTO;

namespace GSM00700Common
{
    public interface IUPLOAD00710
    {
        IAsyncEnumerable<GSM00710UploadCashFlowDTO> GetUploadListGSM00710();
        GSM00710UploadCashFlowCheckRessultDTO CheckUploadGSM00710();
    }
}
