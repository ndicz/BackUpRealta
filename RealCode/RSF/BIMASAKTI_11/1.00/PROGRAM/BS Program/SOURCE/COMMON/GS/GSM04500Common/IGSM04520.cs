using System.Collections.Generic;
using GSM04500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM04500Common
{
    public interface IGSM04520 : R_IServiceCRUDBase<GSM04520DTO>
    {
        IAsyncEnumerable<GSM04520DTO> GetJournalGroupGOADeptListStream();
    }
}