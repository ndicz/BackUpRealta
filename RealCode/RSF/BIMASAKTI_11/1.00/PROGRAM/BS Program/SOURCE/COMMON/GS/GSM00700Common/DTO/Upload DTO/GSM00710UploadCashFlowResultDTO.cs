using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO.Upload_DTO
{
    public class GSM00710UploadCashFlowResultDTO : R_APIResultBaseDTO
    {
        public List<GSM00710UploadCashFlowDTO> Data { get; set; }
    }
}
