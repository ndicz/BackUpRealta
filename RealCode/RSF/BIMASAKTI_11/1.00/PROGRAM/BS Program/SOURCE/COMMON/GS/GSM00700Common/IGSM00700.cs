using System;
using System.Collections.Generic;
using System.Text;
using GSM00700Common.DTO;
using R_CommonFrontBackAPI;

namespace GSM00700Common
{
    public interface IGSM00700 : R_IServiceCRUDBase<GSM00700DTO>
    {
        IAsyncEnumerable<GSM00700DTO> GetAllCashFlowGroupStream();

        GSM00700ListDTO GetAllCashFlowGroupList();
        GSM00700CashFlowGroupTypeListDTO GetListCashFlowGroupType();
    }
}
