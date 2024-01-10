using LMM03700Back;
using LMM03700Common;
using LMM03700Common.DTO;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM03700Controller;

[Route("api/[controller]/[action]")]
[ApiController]
public class LMM03700Controller : ControllerBase, ILMM03700
{
    [HttpPost]
    public R_ServiceGetRecordResultDTO<LMM03700DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM03700DTO> poParameter)
    {
        var loException = new R_Exception();
        var loReturn = new R_ServiceGetRecordResultDTO<LMM03700DTO>();
        var loCls = new LMM03700Cls();

        try
        {
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParameter.Entity.CPROPERTY_ID= R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CPROPERTY_ID);
            loReturn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            
        }
        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;

    }

    [HttpPost]
    public R_ServiceSaveResultDTO<LMM03700DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM03700DTO> poParameter)
    {
        var loException = new R_Exception();
        var loReturn = new R_ServiceSaveResultDTO<LMM03700DTO>();
        var loCls = new LMM03700Cls();

        try
        {
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CPROPERTY_ID= R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CPROPERTY_ID);
            loReturn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            
        }
        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;

    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM03700DTO> poParameter)
    {
        var loException = new R_Exception();
        var loReturn = new R_ServiceDeleteResultDTO();
        var loCls = new LMM03700Cls();

        try
        {
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            
        }
        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<LMM03700DTO> GetTenantClasificationGroupStream()
    {
     
        R_Exception loException = new R_Exception();
        LMM03700DBPamater loDbPar;
        List<LMM03700DTO> loRtnTmp;
        LMM03700Cls loCls;
        IAsyncEnumerable<LMM03700DTO> loRtn = null;
        try
        {
       
            loDbPar = new LMM03700DBPamater();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CPROPERTY_ID);
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loCls = new LMM03700Cls();

         
            loRtnTmp = loCls.GetTenanClasificationList(loDbPar);

        
            loRtn = TenantHelperStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
 
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<LMM03700InitialProcessDTO> GetInitialProcessStream()
    {
        R_Exception loException = new R_Exception();
        LMM03700DBPamater loDbPar;
        List<LMM03700InitialProcessDTO> loRtnTmp;
        LMM03700Cls loCls;
        IAsyncEnumerable<LMM03700InitialProcessDTO> loRtn = null;
        try
        {
       
            loDbPar = new LMM03700DBPamater();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loCls = new LMM03700Cls();

         
            loRtnTmp = loCls.GetInitialPropertyList(loDbPar);

        
            loRtn = InitialHelperStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
 
        return loRtn;
    }
    
    
    
    private async IAsyncEnumerable<LMM03700DTO> TenantHelperStream(List<LMM03700DTO> poParameter)
    {
        foreach (LMM03700DTO item in poParameter)
        {
            yield return item;
        }
    }
    
    private async IAsyncEnumerable<LMM03700InitialProcessDTO> InitialHelperStream(List<LMM03700InitialProcessDTO> poParameter)
    {
        foreach (LMM03700InitialProcessDTO item in poParameter)
        {
            yield return item;
        }
    }
}