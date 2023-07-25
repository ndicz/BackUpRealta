using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCommon.SalesProduct
{
    public class SalesProductRawDataResult
    {
        public string Header { get; set; }
        public string Footer { get; set; }
        public List<SalesProductRawDTO> Datas { get; set; }
    }
    public class ProductPerSalesmanResult
    {
        public string Header { get; set; }
        public string Footer { get; set; }
        public List<ProductBySalesmanDTO> Datas { get; set; }
    }

    public static class GenerateDataModel
    {
        public static ProductPerSalesmanResult DefaultProductPerSalesmanData()
        {
            ProductPerSalesmanResult loRtn = new ProductPerSalesmanResult()
            {
                Header = "Product per Salesman Header",
                Footer = "Product per Salesman Footer",
            };
            List<SalesProductRawDTO> loRawData = GenerateSalesProductRaw(20);

            loRtn.Datas=loRawData.GroupBy(g => new { SalesmanId = g.SalesmanId, SalesmanName = g.SalesmanName }, g => new ProductSalesDTO() { ProductId = g.ProductId, ProductName = g.ProductName, SalesValue = g.SalesValue })
            .Select(s => new ProductBySalesmanDTO() { SalesmanId = s.Key.SalesmanId, SalesmanName = s.Key.SalesmanName, SalesPerProduct = s.ToList() }).ToList();

            return loRtn;
        }
        public static SalesProductRawDataResult DefaultRawData()
        {
            SalesProductRawDataResult loRtn = new SalesProductRawDataResult()
            {
                Header = "Sales Product Raw Header",
                Footer = "Sales Product Raw Footer",
                Datas= GenerateSalesProductRaw(20)
            };

            return loRtn;
        }

        private static List<SalesProductRawDTO> GenerateSalesProductRaw(int pnProduct)
        {
            int lnSales;
            List<SalesProductRawDTO> loRawSalesProducts = new List<SalesProductRawDTO>();
            for (int p = 1; p <= pnProduct; p++)
            {
                lnSales = (p%3)+1;
                for (int s = 1; s <= lnSales; s++)
                    {
                    loRawSalesProducts.Add(new SalesProductRawDTO()
                    {
                        SalesmanId = $"S_ID-{s}",
                        SalesmanName = $"Salesman {s}",
                        ProductId = $"P_ID-{p}",
                        ProductName = $"Product {p}",
                        SalesValue = p * s
                    }
                   ); ;
                }
            }
            
            return loRawSalesProducts;
        }
    }
}
