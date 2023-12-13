using System.Collections.Generic;
using GSM04500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM04500Common
{
    public interface IGSM04500 : R_IServiceCRUDBase<GSM04500DTO>    
    {
        IAsyncEnumerable<GSM04500DTO> GetJournalGroupListStream();
        GSM04500PropertyListDTO GetAllPropertyList();
        GSM04500JournalGroupTypeListDTO GetListJournalGroupType();
    }
}