using System;
using System.Collections.Generic;

namespace ReportCommon.ProductObject
{
    public class ProductResult
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public ProductColumnDTO ColumnProduct { get; set; }
        public string Footer { get; set; }
        public List<ProductDTO> Products { get; set; }
    }

    public static class GenerateDataModel
    {
        public static ProductResult DefaultData()
        {
            ProductResult loData = new ProductResult()
            {
                Title = "ProductTitle",
                Header = "Product Header",
                Footer = "Product Footer",
                ColumnProduct = new ProductColumnDTO()
            };
            List<ProductDTO> loCollection = new List<ProductDTO>();
            for (int i = 0; i < 5; i++)
            {
                loCollection.Add(new ProductDTO()
                {
                    Id = $"ID {i}",
                    Quantity = i + 1,
                    Price = 2.23m + i * 1.7m
                }
               );
            }
            loData.Products = loCollection;

            return loData;
        }

        public static ProductWithHeaderResult DefaultDataWithHeader()
        {
            ProductWithHeaderResult loRtn = new ProductWithHeaderResult();
            loRtn.BaseHeaderData = BaseHeader.GenerateDataModel.DefaultData().BaseHeaderData;
            loRtn.ProductObjectData = GenerateDataModel.DefaultData();

            return loRtn;
        }

    }

}
