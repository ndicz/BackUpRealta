using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO.Upload_DTO_GSM00720
{
    public class GSM00720UploadCashFlowPlanExcelDTO
    {
        public string PeriodNo { get; set; }
        public decimal LocalAmount { get; set; }
        public decimal BaseAmount { get; set; }
        public string Notes { get; set; }
    }
}
