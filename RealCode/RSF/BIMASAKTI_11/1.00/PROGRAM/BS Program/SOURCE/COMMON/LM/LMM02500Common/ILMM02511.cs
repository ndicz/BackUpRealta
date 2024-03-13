using R_CommonFrontBackAPI;

namespace LMM02500Common
{
    public interface ILMM02511 : R_IServiceCRUDBase<LMM02511DTO>
    {
        LMM02511ListDTO GetTaxCode();
        LMM02511ListDTO GetIDType();
        LMM02511ListDTO GetTaxType();
        
    }
}