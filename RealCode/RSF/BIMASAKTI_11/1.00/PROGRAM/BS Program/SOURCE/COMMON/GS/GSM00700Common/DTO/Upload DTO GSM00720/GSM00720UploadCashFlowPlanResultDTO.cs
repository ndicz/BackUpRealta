using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO.Upload_DTO_GSM00720
{
    public class GSM00720UploadCashFlowPlanResultDTO : R_APIResultBaseDTO
    {
        public List<GSM00720UploadCashFlowPlanDTO> Data { get; set; }
    }
}
