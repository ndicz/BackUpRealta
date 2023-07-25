using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon.SalesProduct
{
    public class SalesProductRawDTO
    {
        public string SalesmanId { get; set; }
        public string SalesmanName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int SalesValue { get; set; }
    }
}
