using R_CommonFrontBackAPI;
using ReportCommon.ProductObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon.ReportHeaderDetail
{
    public interface IReportHeaderDetail
    {
        R_DownloadFileResultDTO AllCategoryPost(AllCategoryParameterDTO poParameter);
    }
}
