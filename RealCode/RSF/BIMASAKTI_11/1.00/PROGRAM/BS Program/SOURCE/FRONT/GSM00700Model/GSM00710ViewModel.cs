    using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common.DTO;
using GSM00700Model.Model;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using System.Transactions;
using GSM00700Common;
using System.Linq;

namespace GSM00700Model
{
    public class GSM00710ViewModel : R_ViewModel<GSM00710DTO>
    {
        private GSM00710Model _GSM00710Model = new GSM00710Model();

        public ObservableCollection<GSM00710DTO> loGridList = new ObservableCollection<GSM00710DTO>();
        public ObservableCollection<GSM00710CashFlowTypeDTO> loCashFlow = new ObservableCollection<GSM00710CashFlowTypeDTO>();
        public GSM00710DTO loEntity = new GSM00710DTO();
        public string CashFlowGroupCode = ""; // for filter
        public string CashFlowGroupName = ""; // for filter
        public string csquence = ""; // for squence
        public string CashFlowTypGrp = ""; // for filter
        public List<GSM00710CashFlowTypeDTO> loCashFlowType { get; set; } = new List<GSM00710CashFlowTypeDTO>();



        public async Task GetCashFlowTypeList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GSM00710Model.GetListCashFlowTypeAsync();
                loCashFlowType = loResult.Data.OrderByDescending(x => x.CCODE).ToList(); // Urutkan dan simpan ke dalam list
                loCashFlow = new ObservableCollection<GSM00710CashFlowTypeDTO>(loCashFlowType);
                CashFlowTypGrp = loCashFlowType[0].CCODE;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }



        public async Task GetCashFlowList()
        {
            var loEx = new R_Exception();

            try
            {
                
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE, CashFlowGroupCode);
                var loReturn = await _GSM00710Model.GetAllCashFlowStreamAsync();
                loGridList = new ObservableCollection<GSM00710DTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //public async Task GetCashFlowList()
        //{
        //    var loEx = new R_Exception();

        //    try
        //    {
        //        R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE, CashFlowGroupCode);
        //        var loReturn = await _GSM00710Model.GetAllCashFlowListAsync();
        //        var loData = loReturn.Data;


        //        loGridList = new ObservableCollection<GSM00710DTO>();

        //        int currentSequence = 1; // Nilai awal untuk CSQUENCE

        //        foreach (var dto in loData)
        //        {
        //            dto.CSEQUENCE = currentSequence.ToString(); // Setel nilai CSQUENCE
        //            loGridList.Add(dto);

        //            currentSequence++; // Peningkatan nilai CSQUENCE
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}


        public async Task GetCashFlowId(string cashFlowCode, string cashFlowGroupCode)
        {
            var loEx = new R_Exception();
            try
            {

                //R_FrontContext.R_SetStreamingContext(ContextConstantGSM00700.CCASH_FLOW_CODE, cashFlowCode);  

                var loParam = new GSM00710DTO() { CCASH_FLOW_CODE = cashFlowCode, CCASH_FLOW_GROUP_CODE = cashFlowGroupCode};
                var loResult = await _GSM00710Model.R_ServiceGetRecordAsync(loParam);
                loEntity = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //public async Task SaveCashFlow(GSM00710DTO poNewEntity, R_eConductorMode peConductorMode, string cashFlowGroupCode)
        //{
        //    var loEx = new R_Exception();
        //    GSM00710DTO loResult = null;
        //    try
        //    {
        //        poNewEntity.CCASH_FLOW_GROUP_CODE = CashFlowGroupCode;

        //        // Mendapatkan nilai paling terakhir dari loGridList
        //        var lastDto = loGridList.LastOrDefault();
        //        int currentSequence = 1;

        //        if (lastDto != null)
        //        {
        //            // Mengambil nilai CSEQUENCE dari dto terakhir dan menambahkan 1
        //            currentSequence = int.Parse(lastDto.CSEQUENCE) + 1;
        //        }

        //        poNewEntity.CSEQUENCE = currentSequence.ToString(); // Mengubah nilai CSEQUENCE menjadi string

        //        loResult = await _GSM00710Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
        //        loEntity = loResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //}

        public async Task SaveCashFlow(GSM00710DTO poNewEntity, R_eConductorMode peConductorMode, string cashFlowGroupCode)
        {
            var loEx = new R_Exception();
            GSM00710DTO loResult = null;
            try
            {
                poNewEntity.CCASH_FLOW_GROUP_CODE = CashFlowGroupCode;
                poNewEntity.CCASH_FLOW_TYPE = "I";

                // Mendapatkan nilai paling terbesar dari loGridList berdasarkan CSEQUENCE
                var maxDto = loGridList.OrderByDescending(dto => int.Parse(dto.CSEQUENCE)).FirstOrDefault();
                int currentSequence = 1;

                if (maxDto != null)
                {
                    // Mengambil nilai CSEQUENCE dari dto terbesar dan menambahkan 1
                    currentSequence = int.Parse(maxDto.CSEQUENCE) + 1;
                }

                poNewEntity.CSEQUENCE = currentSequence.ToString(); // Mengubah nilai CSEQUENCE menjadi string

                loResult = await _GSM00710Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        public async Task DeleteCashFlow(GSM00710DTO poNewEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new GSM00710DTO
                {
                    CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                    CCASH_FLOW_GROUP_CODE = poNewEntity.CCASH_FLOW_GROUP_CODE,
                    CCASH_FLOW_CODE = poNewEntity.CCASH_FLOW_CODE,
                    CCASH_FLOW_NAME = poNewEntity.CCASH_FLOW_NAME,
                    CCASH_FLOW_TYPE = poNewEntity.CCASH_FLOW_TYPE,
                    CUSER_ID = poNewEntity.CUSER_ID,
                    CSEQUENCE = poNewEntity.CSEQUENCE

                };
                await _GSM00710Model.R_ServiceDeleteAsync(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
