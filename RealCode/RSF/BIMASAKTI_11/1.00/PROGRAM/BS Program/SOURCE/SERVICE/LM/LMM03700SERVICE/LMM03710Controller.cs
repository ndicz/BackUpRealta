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

public class LMM03710Controller : ControllerBase, ILMM03710
{
    [HttpPost]
    public R_ServiceGetRecordResultDTO<LMM03710DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM03710DTO> poParameter)
    {
        var loException = new R_Exception();
        var loReturn = new R_ServiceGetRecordResultDTO<LMM03710DTO>();
        var loCls = new LMM03710Cls();

        try
        {
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
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
    public R_ServiceSaveResultDTO<LMM03710DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM03710DTO> poParameter)
    {
        var loException = new R_Exception();
        var loReturn = new R_ServiceSaveResultDTO<LMM03710DTO>();
        var loCls = new LMM03710Cls();

        try
        {
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CPROPERTY_ID);
            poParameter.Entity.CTENANT_CLASSIFICATION_GROUP_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CTENANT_CLASSIFICATION_GROUP_ID);
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
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM03710DTO> poParameter)
    {
        var loException = new R_Exception();
        var loReturn = new R_ServiceDeleteResultDTO();
        var loCls = new LMM03710Cls();

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
    public IAsyncEnumerable<LMM03710DTO> GetTenanClasificationGroupListStream()
    {
        R_Exception loException = new R_Exception();
        LMM03700DBPamater loDbPar;
        List<LMM03710DTO> loRtnTmp;
        LMM03710Cls loCls;
        IAsyncEnumerable<LMM03710DTO> loRtn = null;
        try
        {
       
            loDbPar = new LMM03700DBPamater();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CPROPERTY_ID);
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loCls = new LMM03710Cls();

         
            loRtnTmp = loCls.GetTenanClasificationGroupList(loDbPar);

        
            loRtn = StreamHelper(loRtnTmp);
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
    public IAsyncEnumerable<LMM03710DTO> GetTenanClasificationListStream()
    {
        R_Exception loException = new R_Exception();
        LMM03700DBPamater loDbPar;
        List<LMM03710DTO> loRtnTmp;
        LMM03710Cls loCls;
        IAsyncEnumerable<LMM03710DTO> loRtn = null;
        try
        {
       
            loDbPar = new LMM03700DBPamater();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CPROPERTY_ID);
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CTENANT_CLASSIFICATION_GROUP_ID);
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loCls = new LMM03710Cls();

         
            loRtnTmp = loCls.GetTenanClasificationList(loDbPar);

        
            loRtn = StreamHelper(loRtnTmp);
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
    public IAsyncEnumerable<LMM03710DTO> GetTenantListStream()
    {
        R_Exception loException = new R_Exception();
        LMM03700DBPamater loDbPar;
        List<LMM03710DTO> loRtnTmp;
        LMM03710Cls loCls;
        IAsyncEnumerable<LMM03710DTO> loRtn = null;
        try
        {
       
            loDbPar = new LMM03700DBPamater();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CPROPERTY_ID);
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CTENANT_CLASSIFICATION_ID);
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loCls = new LMM03710Cls();

         
            loRtnTmp = loCls.GetTenantList(loDbPar);

        
            loRtn = StreamHelper(loRtnTmp);
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
    public IAsyncEnumerable<LMM03710DTO> AssignAvailableTenantListStream()
    {
        R_Exception loException = new R_Exception();
        LMM03700DBPamater loDbPar;
        List<LMM03710DTO> loRtnTmp;
        LMM03710Cls loCls;
        IAsyncEnumerable<LMM03710DTO> loRtn = null;
        try
        {
       
            loDbPar = new LMM03700DBPamater();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CTENANT_CLASSIFICATION_GROUP_ID);
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CTENANT_CLASSIFICATION_ID);
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CPROPERTY_ID);
            
            loCls = new LMM03710Cls();

         
            loRtnTmp = loCls.AssignAvailableTenantList(loDbPar);

        
            loRtn = StreamHelper(loRtnTmp);
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
    public IAsyncEnumerable<LMM03710DTO> AssignSelectedTenantListStream()
    {
        R_Exception loException = new R_Exception();
        LMM03700DBPamater loDbPar;
        List<LMM03710DTO> loRtnTmp;
        LMM03710Cls loCls;
        IAsyncEnumerable<LMM03710DTO> loRtn = null;
        try
        {
       
            loDbPar = new LMM03700DBPamater();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CTENANT_CLASSIFICATION_ID);
            loCls = new LMM03710Cls();

         
            loRtnTmp = loCls.AssignSelectedTenantList(loDbPar);

        
            loRtn = StreamHelper(loRtnTmp);
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
    public IAsyncEnumerable<LMM03710DTO> MoveTenantDropownStream()
    {
        R_Exception loException = new R_Exception();
        LMM03700DBPamater loDbPar;
        List<LMM03710DTO> loRtnTmp;
        LMM03710Cls loCls;
        IAsyncEnumerable<LMM03710DTO> loRtn = null;
        try
        {
       
            loDbPar = new LMM03700DBPamater();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CPROPERTY_ID);
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CTENANT_CLASSIFICATION_GROUP_ID);
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CTENANT_CLASSIFICATION_ID);
            loCls = new LMM03710Cls();

         
            loRtnTmp = loCls.MoveTenantDropown(loDbPar);

        
            loRtn = StreamHelper(loRtnTmp);
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
    public IAsyncEnumerable<LMM03710DTO> MoveGetTenantListStream()
    {
        R_Exception loException = new R_Exception();
        LMM03700DBPamater loDbPar;
        List<LMM03710DTO> loRtnTmp;
        LMM03710Cls loCls;
        IAsyncEnumerable<LMM03710DTO> loRtn = null;
        try
        {
       
            loDbPar = new LMM03700DBPamater();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CPROPERTY_ID);
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantLMM03700.CTENANT_CLASSIFICATION_ID);
            loCls = new LMM03710Cls();

         
            loRtnTmp = loCls.MoveGetTenantList(loDbPar);

        
            loRtn = StreamHelper(loRtnTmp);
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
    public LMM03700Dump AssignTenant(LMM03700ParamDTO poParam)
    {
        R_Exception loException = new R_Exception();
        LMM03710Cls loCls;
        try
        {
            loCls = new LMM03710Cls();
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
            loCls.AssignTenantProecess(poParam);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        EndBlock:
        loException.ThrowExceptionIfErrors();
        return new();
    }

    [HttpPost]
    public LMM03700Dump MoveTenant(LMM03700ParamDTO poParam)
    {
        R_Exception loException = new R_Exception();
        LMM03710Cls loCls;
        try
        {
            loCls = new LMM03710Cls();
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
            //loCls.MoveTenantUsingRDT(poParam);
            loCls.MoveTenantProecess(poParam);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        EndBlock:
        loException.ThrowExceptionIfErrors();
        return new();
    }
    
    private async IAsyncEnumerable<T> StreamHelper<T>(List<T> entityList)
    {
        foreach (T entity in entityList)
        {
            yield return entity;
        }
    }
}