using ReportCommon.ProductObject;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using R_Cache;
using System.Collections;
using ReportCommon;
using R_BackEnd;
using ReportCommon.BaseHeader;

namespace ReportService
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductInheritController
    {
        private R_ReportFastReportBackClass _ReportCls;
        private AllProductParameterDTO _AllProductParameter;

        #region instantiate
        public ProductInheritController()
        {
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            //_ReportCls.R_SetMainParameter += _ReportCls_R_SetMainParameter;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = "Reports\\ProductObjectInherit.frx";
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateData(_AllProductParameter.GenerateCountProduct));
            pcDataSourceName = "ResponseDataModel";
        }

        //private void _ReportCls_R_SetMainParameter(ref Dictionary<string, object> poParameters)
        //{
        //    poParameters.Add("Parameter1", "Ini Parameter1");
        //    poParameters.Add("Parameter2", "Ini Parameter2");
        //}

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
        public R_DownloadFileResultDTO AllProductPost(AllProductParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<AllProductParameterDTO>(poParameter));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet]
        public FileStreamResult AllStreamProductGet(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                _AllProductParameter = R_NetCoreUtility.R_DeserializeObjectFromByte<AllProductParameterDTO>(R_DistributedCache.Cache.Get(pcGuid));
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
        private ProductWithHeaderResult GenerateData(int pnTotalRow)
        {
            ProductWithHeaderResult loRtn = new ProductWithHeaderResult();
            ProductResult loProductData = new ProductResult()
            {
                Header = "Product Header",
                Footer = "Product Footer",
                ColumnProduct = new ProductColumnDTO()
            };
            List<ProductDTO> loCollection = new List<ProductDTO>();
            for (int i = 1; i <= pnTotalRow; i++)
            {
                loCollection.Add(new ProductDTO()
                {
                    Id = $"ID {i}",
                    Quantity = i + 1,
                    Price = 2.23m + i * 1.7m
                }
               );
            }
            loProductData.Products = loCollection;

            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CompanyId = "C-01",
                CompanyName = "Company 01",
                UserId = "U-01",
                UserName = "User 01"
            };
            loRtn.ProductObjectData = loProductData;

            return loRtn;
        }

        #endregion


    }
}
