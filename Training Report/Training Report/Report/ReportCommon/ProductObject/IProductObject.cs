using R_CommonFrontBackAPI;

namespace ReportCommon.ProductObject
{
    public interface IProductObject
    {
        R_DownloadFileResultDTO AllProductPost(AllProductParameterDTO poParameter);
    }
}
