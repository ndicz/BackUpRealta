using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace LMM02000Common.DTO.UPLOAD_DTO_LMM02000
{
    public class LMM02000UploadSalesmanResultDTO : R_APIResultBaseDTO
    {
        public List<LMM02000UploadSalesmanDTO> Data { get; set; }
    }
}
