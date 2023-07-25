using ReportCommon.SalesProduct;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using R_Cache;
using System.Collections;
using R_BackEnd;

namespace ReportService
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalesProductController : ControllerBase, ISalesProduct
    {
        private R_ReportFastReportBackClass _ReportCls;
        private AllSalesProductParameterDTO _AllSalesProductParameter;
        string _lcType;

        #region instantiate
        public SalesProductController()
        {
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            switch (_lcType)
            {
                case "HD":
                    pcFileTemplate = "Reports\\SalesProductHeaderDetail.frx";
                    break;
                case "M":
                    pcFileTemplate = "Reports\\SalesProductMatrix.frx";
                    break;
                case "G":
                    pcFileTemplate = "Reports\\SalesProductGroup.frx";
                    break;
                default:
                    pcFileTemplate = "";
                    break;
            }

        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            if (_lcType == "HD")
            {
                poData.Add(GenerateHDData(_AllSalesProductParameter.GenerateCountSalesProduct));
            }
            else
            {
                poData.Add(GenerateRawData(_AllSalesProductParameter.GenerateCountSalesProduct));
            }

            pcDataSourceName = "ResponseDataModel";
        }


        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        }
        #endregion


        [HttpPost]
        public R_DownloadFileResultDTO AllSalesProductPost(AllSalesProductParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<AllSalesProductParameterDTO>(poParameter));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet]
        public FileStreamResult StreamSalesProductGroupGet(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            try
            {
                _lcType = "G";
                //Get Parameter
                _AllSalesProductParameter = R_NetCoreUtility.R_DeserializeObjectFromByte<AllSalesProductParameterDTO>(R_DistributedCache.Cache.Get(pcGuid));
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }


        [HttpGet]
        public FileStreamResult StreamSalesProductMatrixGet(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            try
            {
                _lcType = "M";
                //Get Parameter
                _AllSalesProductParameter = R_NetCoreUtility.R_DeserializeObjectFromByte<AllSalesProductParameterDTO>(R_DistributedCache.Cache.Get(pcGuid));
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpGet]
        public FileStreamResult StreamSalesProductHeaderDetailGet(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            try
            {
                _lcType = "HD";
                //Get Parameter
                _AllSalesProductParameter = R_NetCoreUtility.R_DeserializeObjectFromByte<AllSalesProductParameterDTO>(R_DistributedCache.Cache.Get(pcGuid));
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }


        #region Helper
        private SalesProductRawDataResult GenerateRawData(int pnCount)
        {
            SalesProductRawDataResult loRtn = new SalesProductRawDataResult()
            {
                Header = "Sales Product Raw Header",
                Footer = "Sales Product Raw Footer",
                Datas = GenerateSalesProductRaw(pnCount)
            };

            return loRtn;
        }

        private ProductPerSalesmanResult GenerateHDData(int pnCount)
        {
            ProductPerSalesmanResult loRtn = new ProductPerSalesmanResult()
            {
                Header = "Product per Salesman Header",
                Footer = "Product per Salesman Footer",
            };
            List<SalesProductRawDTO> loRawData = GenerateSalesProductRaw(pnCount);

            loRtn.Datas = loRawData.GroupBy(g => new { SalesmanId = g.SalesmanId, SalesmanName = g.SalesmanName }, g => new ProductSalesDTO() { ProductId = g.ProductId, ProductName = g.ProductName, SalesValue = g.SalesValue })
            .Select(s => new ProductBySalesmanDTO() { SalesmanId = s.Key.SalesmanId, SalesmanName = s.Key.SalesmanName, SalesPerProduct = s.ToList() }).ToList();

            return loRtn;
        }

        private List<SalesProductRawDTO> GenerateSalesProductRaw(int pnProduct)
        {
            int lnSales;
            List<SalesProductRawDTO> loRawSalesProducts = new List<SalesProductRawDTO>();
            for (int p = 1; p <= pnProduct; p++)
            {
                lnSales = (p % 3) + 1;
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

       

        #endregion

    }
}
