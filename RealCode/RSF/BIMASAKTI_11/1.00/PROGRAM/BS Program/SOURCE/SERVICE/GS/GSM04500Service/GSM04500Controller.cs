using GSM04500Common;
using GSM04500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM04500Service
{

[Route("api/[controller]/[action]")]
[ApiController]
    public class GSM04500Controller : ControllerBase, IGSM04500
    {
        public R_ServiceGetRecordResultDTO<GSM04500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        public R_ServiceSaveResultDTO<GSM04500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04500DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM04500DTO> GetJournalGroupListStream()
        {
            throw new NotImplementedException();
        }

        public GSM04500PropertyListDTO GetAllPropertyList()
        {
            throw new NotImplementedException();
        }

        public GSM04500JournalGroupTypeListDTO GetListJournalGroupType()
        {
            throw new NotImplementedException();
        }
    }
}