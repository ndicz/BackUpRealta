using GSM05500Common.DTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using static GSM05500Common.DTO.GSM05500ListDTO;

namespace GSM05500Common
{
    public interface IGSM05500 : R_IServiceCRUDBase<GSM05500DTO>
    {
      IAsyncEnumerable<GSM05500DTO> GetAllCurrencyStream();

      GSM05500ListDTO GetAllCurrencyList();
    }
}
