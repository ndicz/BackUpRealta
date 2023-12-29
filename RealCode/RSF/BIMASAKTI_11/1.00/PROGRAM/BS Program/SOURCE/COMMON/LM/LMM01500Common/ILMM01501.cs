using System.Collections.Generic;
using LMM01500Common.DTOs;
using R_CommonFrontBackAPI;

namespace LMM01500Common
{
    public interface ILMM01501 : R_IServiceCRUDBase<LMM01500TemplateBankAccountDTO>
    {
        IAsyncEnumerable<LMM01500TemplateBankAccountDTO> GetInvoiceGroupDeptList();
        LMM01500TemplateBankAccountListDTO GetInvoiceGroup();
        //LMM01500TemplateBankAccountListDTO GetInvoiceGroupDeptDetail();
    }
}