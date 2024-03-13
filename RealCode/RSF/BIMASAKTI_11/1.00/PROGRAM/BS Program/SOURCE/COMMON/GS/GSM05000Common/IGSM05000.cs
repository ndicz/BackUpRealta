using System.Collections.Generic;
using GSM05000Common.DTO;
using R_CommonFrontBackAPI;

namespace GSM05000Common
{
    public interface IGSM05000 : R_IServiceCRUDBase<GSM05000GridDTO>
    {
        IAsyncEnumerable<GSM05000GridDTO> GetTransactionlistStream();
        GSM05000LimiterListDTO GetLimiter();
    }
}   