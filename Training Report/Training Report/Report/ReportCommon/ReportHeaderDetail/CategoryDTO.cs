using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon.ReportHeaderDetail
{
    public class CategoryDTO
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<ProductDTO> Products { get; set; }
       
    }
}
