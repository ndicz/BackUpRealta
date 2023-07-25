using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon.ProductObject
{
    public class AllProductWithGlobalizationParameterDTO
    {
        public int GenerateCountProduct { get; set; }
        public string ReportCulture { get; set; }
        public string ReportDecimalSeparator { get; set; }
        public int ReportDecimalPlaces { get; set; }
        public string ReportShortDate { get; set; }
        public string ReportShortTime { get; set; }
    }
}
