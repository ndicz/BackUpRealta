using GSM05500Common.DTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM05500Common
{
    public interface IGSM05520 : R_IServiceCRUDBase<GSM05520DTO>
    {
        IAsyncEnumerable<GSM05520DTO> GetAllRateStream();

        GSM05520ListDTO GetAllRateList();


        GSM05520ListDTOGetRateType GetListRateType();

        GSM05520DTOLocalBaseCurrency GetLcCurrency();
    }
}
