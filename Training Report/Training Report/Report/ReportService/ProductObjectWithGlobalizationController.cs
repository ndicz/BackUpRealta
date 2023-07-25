using ReportCommon.ProductObject;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using R_Cache;
using System.Collections;
using ReportCommon;
using R_BackEnd;

namespace ReportService
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductObjectWithGlobalizationController
    {
        private R_ReportFastReportBackClass _ReportCls;
        private AllProductWithGlobalizationParameterDTO _AllProductWithGlobalizationParameter;

        #region instantiate
        public ProductObjectWithGlobalizationController()
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
            pcFileTemplate = "Reports\\ProductObject.frx";
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateData(_AllProductWithGlobalizationParameter));
            pcDataSourceName = "ResponseDataModel";
        }

        //private void _ReportCls_R_SetMainParameter(ref Dictionary<string, object> poParameters)
        //{
        //    poParameters.Add("Parameter1", "Ini Parameter1");
        //    poParameters.Add("Parameter2", "Ini Parameter2");
        //}

        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            poReportFormat.DecimalSeparator = _AllProductWithGlobalizationParameter.ReportDecimalSeparator;
            if (poReportFormat.DecimalSeparator == ".")
            {
                poReportFormat.GroupSeparator = ",";
            }
            else
            {
                poReportFormat.GroupSeparator = ".";
            }
            poReportFormat.DecimalPlaces = _AllProductWithGlobalizationParameter.ReportDecimalPlaces;
            poReportFormat.ShortDate = _AllProductWithGlobalizationParameter.ReportShortDate;
            poReportFormat.ShortTime = _AllProductWithGlobalizationParameter.ReportShortTime;
        }
        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO AllProductPost(AllProductWithGlobalizationParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            try
            {

                //{
                //                    "GenerateCountProduct": 20,
                //  "ReportCulture": "id",
                //  "ReportDecimalSeparator": ",",
                //  "ReportDecimalPlaces": 3,
                //  "ReportShortDate": "dd-MM-yyyy",
                //  "ReportShortTime": "HH:mm:ss"
                //}

                loRtn = new R_DownloadFileResultDTO();
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<AllProductWithGlobalizationParameterDTO>(poParameter));
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
                _AllProductWithGlobalizationParameter = R_NetCoreUtility.R_DeserializeObjectFromByte<AllProductWithGlobalizationParameterDTO>(R_DistributedCache.Cache.Get(pcGuid));
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
        private ProductResult GenerateData(AllProductWithGlobalizationParameterDTO poParameter)
        {
            System.Globalization.CultureInfo loCultureInfo = new System.Globalization.CultureInfo(poParameter.ReportCulture);
            ProductResult loRtn = new ProductResult()
            {
                Title =  "Product Title",
                Header = "Product Header",
                Footer = "Product Footer",
                ColumnProduct = new ProductColumnDTO()
                {
                    ColId = R_Utility.R_GetMessage(typeof(FastReportBackResources.Resources_Dummy_Class), "ProductId", loCultureInfo),
                    ColPrice = R_Utility.R_GetMessage(typeof(FastReportBackResources.Resources_Dummy_Class), "Price", loCultureInfo),
                    ColQuantity = R_Utility.R_GetMessage(typeof(FastReportBackResources.Resources_Dummy_Class), "Quantity", loCultureInfo)

                }
            };
            List<ProductDTO> loCollection = new List<ProductDTO>();
            for (int i = 1; i <= poParameter.GenerateCountProduct; i++)
            {
                loCollection.Add(new ProductDTO()
                {
                    Id = $"ID {i}",
                    Quantity = i + 1,
                    Price = 2.23m + i * 1.7m
                }
               );
            }
            loRtn.Products = loCollection;

            return loRtn;
        }

        #endregion



    }
}
