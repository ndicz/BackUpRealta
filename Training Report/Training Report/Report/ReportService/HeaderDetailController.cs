using ReportCommon.ReportHeaderDetail;
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
    public class HeaderDetailController : ControllerBase,IReportHeaderDetail
    {
        private R_ReportFastReportBackClass _ReportCls;
        private AllCategoryParameterDTO _AllCategoryParameter;

        #region instantiate
        public HeaderDetailController()
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
            pcFileTemplate = "Reports\\HeaderDetail.frx";
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateData(_AllCategoryParameter.GenerateCountCategory));
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
        public R_DownloadFileResultDTO AllCategoryPost(AllCategoryParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<AllCategoryParameterDTO>(poParameter));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet]
        public FileStreamResult AllStreamCategoryGet(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                _AllCategoryParameter = R_NetCoreUtility.R_DeserializeObjectFromByte<AllCategoryParameterDTO>(R_DistributedCache.Cache.Get(pcGuid));
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
        private HeaderDetailResult GenerateData(int pnCount)
        {
            HeaderDetailResult loRtn = new HeaderDetailResult()
            {
                Header = "Category Header",
                Footer = "Category Footer",
                ColumnCategory = new CategoryColumnDTO(),
                ColumnProduct = new ProductColumnDTO()
            };
            List<CategoryDTO> loCategories = new List<CategoryDTO>();
            for (int i = 1; i <= pnCount; i++)
            {
                loCategories.Add(new CategoryDTO()
                {
                    CategoryId = $"C_ID {i}",
                    CategoryName = $"Category {i}",
                    Products = GenerateProduct(i)
                }
               );
            }
            loRtn.Categories = loCategories;
            return loRtn;
        }

        private List<ProductDTO> GenerateProduct(int pnInt)
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

       

        #endregion


    }
}
