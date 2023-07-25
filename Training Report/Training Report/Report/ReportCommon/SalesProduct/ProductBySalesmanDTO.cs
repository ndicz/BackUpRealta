using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon.SalesProduct
{
    public class ProductBySalesmanDTO
    {
        public string SalesmanId { get; set; }
        public string SalesmanName { get; set; }

        public List<ProductSalesDTO> SalesPerProduct { get; set; }
        
    }
}
