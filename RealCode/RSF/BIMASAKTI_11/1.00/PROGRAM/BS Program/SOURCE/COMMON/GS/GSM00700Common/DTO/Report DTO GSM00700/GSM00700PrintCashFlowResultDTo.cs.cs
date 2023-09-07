using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO.Report_DTO_GSM00700
{
    public class GSM00700PrintCashFlowResultDTo
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public bool PrintDept { get; set; }
        public GSM00700PrintCashFlowColoumnDTO Column { get; set; }
        public List<GSM00700Data> Data { get; set; }
    }

    public class GSM00700PrintCashFlowResultWithBaseHeaderPrintDTO : BaseHeaderReportCommon.BaseHeader.BaseHeaderResult
    {
        public GSM00700PrintCashFlowResultDTo CenterData { get; set; }
    }
}
