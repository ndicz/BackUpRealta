using GSM05500Common.DTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM05500Common
{
    public interface IGSM05520 : R_IServiceCRUDBase<GSM05520DTO>
    {

        //GSM05520ListDTO GetAllRateList();
        //GSM05520ListDTOGetRateType GetListRateType();
        GSM05520DTOLocalBaseCurrency GetLcCurrency();

        IAsyncEnumerable<GSM05520DTO> GetAllRateStream();
        IAsyncEnumerable<GSM05520DTOGetRateType> GetAllRateTypeStream();
        //IAsyncEnumerable<GSM05520DTOLocalBaseCurrency> GwtAllLcCurrencyStream();
    }
}
