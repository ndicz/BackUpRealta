using System;
using System.Collections.Generic;
using System.Text;
using GSM05500Common.DTO;
using R_CommonFrontBackAPI;
using static GSM05500Common.DTO.GSM05500ListDTO;
using static GSM05500Common.DTO.GSM05510ListDTO;

namespace GSM05500Common
{
    public interface IGSM05510 : R_IServiceCRUDBase<GSM05510DTO>
    {
        IAsyncEnumerable<GSM05510DTO> GetAllRateTypeStream();

    }
}
