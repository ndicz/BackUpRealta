using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace GSM10500Common.DTO
{
    public interface IGSM10510 : R_IServiceCRUDBase<GSM10510DTO>
    {
        IAsyncEnumerable<GSM10510DTO> GetAllAgeingDTStream();
    }
}