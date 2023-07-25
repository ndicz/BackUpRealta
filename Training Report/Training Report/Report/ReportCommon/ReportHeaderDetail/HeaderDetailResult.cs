using ReportCommon.ProductObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon.ReportHeaderDetail
{
    public class HeaderDetailResult
    {
        public string Header { get; set; }
        public CategoryColumnDTO ColumnCategory { get; set; }
        public ProductColumnDTO ColumnProduct { get; set; }
        public string Footer { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }

    public static class GenerateDataModel
    {
        public static HeaderDetailResult DefaultData()
        {
            HeaderDetailResult loRtn = new HeaderDetailResult()
            {
                Header = "Header Detail Header",
                Footer = "Header Detail Footer",
                ColumnCategory = new CategoryColumnDTO(),
                ColumnProduct = new ProductColumnDTO()
            };
            List<CategoryDTO> loCategories = new List<CategoryDTO>();
            int count = 2;
            for (int i = 1; i <= count; i++)
            {
                loCategories.Add(new CategoryDTO()
                {
                    CategoryId = $"C_ID {i}",
                    CategoryName = $"Category {i}",
                    Products = GenerateProduct(i)
                }
               );
            }
            loRtn.Categories=loCategories;

            return loRtn;
        }

        private static List<ProductDTO> GenerateProduct(int pnInt)
        {
            List<ProductDTO> loProducts = new List<ProductDTO>();
            int count = 2 * pnInt;
            for (int i = 1; i <= count; i++)
            {
                loProducts.Add(new ProductDTO()
                {
                    ProductId = $"P_ID {pnInt}-{i}",
                    ProductName = $"Product {pnInt}-{i}"
                }
               );
            }
            return loProducts;
        }
    }
}
