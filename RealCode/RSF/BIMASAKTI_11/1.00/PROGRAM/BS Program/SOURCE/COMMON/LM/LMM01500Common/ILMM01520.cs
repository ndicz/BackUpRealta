using System.Collections.Generic;
using LMM01500Common.DTOs;
using R_CommonFrontBackAPI;

namespace LMM01500Common
{
    public interface ILMM01520 : R_IServiceCRUDBase<LMM01520ChargeDTO>
    {
        LMM01520ChargeListDTO GetInvoiceGroupCharge();
        IAsyncEnumerable<LMM01520ChargeListDTO> GetInvoiceGroupChargeDetail();
    }
}