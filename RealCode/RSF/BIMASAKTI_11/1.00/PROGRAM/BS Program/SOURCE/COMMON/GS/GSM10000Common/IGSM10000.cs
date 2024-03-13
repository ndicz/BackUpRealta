using System.Collections.Generic;
using GSM10000Common.DTO;
using R_CommonFrontBackAPI;

namespace GSM10000Common
{
    public interface IGSM10000 : R_IServiceCRUDBase<GSM10000DTO>
    {
        IAsyncEnumerable<GSM10000DTO> GetAllHolidayStream();
        GSM10000ActiveInactiveDTO GetActiveInactive(LMM02000ActiveInactiveParam poParamDto);
    }
}