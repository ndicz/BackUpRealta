using System.Collections.Generic;
using GSM04500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM04500Common
{
    public interface IGSM04510 : R_IServiceCRUDBase<GSM04510DTO>
    {
        IAsyncEnumerable<GSM04510DTO> GetJournalGroupGOAListStream();
    }
}