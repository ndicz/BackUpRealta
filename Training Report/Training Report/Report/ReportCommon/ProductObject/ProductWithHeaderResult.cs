using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon.ProductObject
{
    public class ProductWithHeaderResult : BaseHeader.BaseHeaderResult
    {
        public ProductResult ProductObjectData { get; set; }
    }
}
