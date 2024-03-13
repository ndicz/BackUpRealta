using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace GSM10500Common.DTO
{
    public interface IGSM10500 : R_IServiceCRUDBase<GSM10500DTO>
    {
        IAsyncEnumerable<GSM10500DTO> GetAllAgeingHDStream();
        IAsyncEnumerable<GSM10500RoundingModeDTO> GetRoundingMode();
    }
}