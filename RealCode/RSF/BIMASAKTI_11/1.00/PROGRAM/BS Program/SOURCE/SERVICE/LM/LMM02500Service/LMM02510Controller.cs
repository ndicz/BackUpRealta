using LMM02500Back;
using LMM02500Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02500Service{

public class LMM02510Controller : ControllerBase, ILMM02510
{[HttpPost]
    public R_ServiceGetRecordResultDTO<LMM02510DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM02510DTO> poParameter)
    {
        var loException = new R_Exception();
        var loRtn = new R_ServiceGetRecordResultDTO<LMM02510DTO>();
        LMM02500DBParameter loDbPar;
        try
        {
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            //poParameter.Entity.CCOMPANY_ID = "RCD";
            //poParameter.Entity.CUSER_ID = "Admin";
            var loCls = new LMM02510Cls();
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
        return loRtn;
    }
[HttpPost]
    public R_ServiceSaveResultDTO<LMM02510DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM02510DTO> poParameter)
    {
        throw new NotImplementedException();
    }
[HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02510DTO> poParameter)
    {
        R_Exception loException = new R_Exception();
        R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
        LMM02510Cls loCls;

        try
        {
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            //poParameter.Entity.CCOMPANY_ID = "RCD";
            //poParameter.Entity.CUSER_ID = "Admin";
            loCls = new LMM02510Cls();
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
          
        };
        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loRtn;
    }
}}