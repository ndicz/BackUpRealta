using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon.SalesProduct
{
    public interface ISalesProduct
    {
        R_DownloadFileResultDTO AllSalesProductPost(AllSalesProductParameterDTO poParameter);
    }
}
