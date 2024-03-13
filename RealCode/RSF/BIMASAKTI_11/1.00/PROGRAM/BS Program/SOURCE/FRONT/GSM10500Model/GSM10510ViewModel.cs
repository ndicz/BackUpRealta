using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GSM10500Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM10500Model
{
    public class GSM10510ViewModel : R_ViewModel<GSM10510DTO>
    {
        private Model.GSM10510Model _GSM10510Model = new Model.GSM10510Model();
        public ObservableCollection<GSM10510DTO> loGridList { get; set; } = new ObservableCollection<GSM10510DTO>();
        public GSM10510DTO loEntity = new GSM10510DTO();
        public string ageingCode = ""; // for filter


        public async Task GetAgeingDTList()
        {
            R_FrontContext.R_SetStreamingContext(ContextConstanGSM10500.CAGEING_CODE, ageingCode);
            var loReturn = await _GSM10510Model.GetAllAgeingDTStreamAsync();
            loGridList = new ObservableCollection<GSM10510DTO>(loReturn.Data);
        }

        public async Task GetAgeingDTID(string ageingCode, int coloumnNo)
        {
            var loParam = new GSM10510DTO() { CAGEING_CODE = ageingCode, ICOLUMN_NO = coloumnNo };
            var loResult = await _GSM10510Model.R_ServiceGetRecordAsync(loParam);
            loEntity = loResult;
        }

        public async Task DeleteAgeingDT(GSM10510DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                await _GSM10510Model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveAgeingDT(GSM10510DTO poNewEntity, R_eConductorMode peConductorMode)
        {
            // var loEx = new R_Exception();
            // GSM10510DTO loResult = null;
            // try
            // {
            //     poNewEntity.CAGEING_CODE = ageingCode;
            //     // Mendapatkan nilai paling terbesar dari loGridList berdasarkan CSEQUENCE
            //     var maxDto = loGridList.OrderByDescending(dto => dto.ICOLUMN_NO).FirstOrDefault();
            //     int currentSequence = 1;
            //
            //     if (maxDto != null)
            //     {
            //         // Mengambil nilai CSEQUENCE dari dto terbesar dan menambahkan 1
            //         currentSequence = maxDto.ICOLUMN_NO + 1;
            //     }
            //
            //     poNewEntity.ICOLUMN_NO = currentSequence; // Mengubah nilai CSEQUENCE menjadi string
            //     loResult = await _GSM10510Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
            //     loEntity = loResult;
            //     }
            //     catch (Exception ex)
            //     {
            //         loEx.Add(ex);
            //     }
            //
            //     loEx.ThrowExceptionIfErrors();
            // }
            var loEx = new R_Exception();
            GSM10510DTO loResult = null;
            try
            {
                if (peConductorMode == R_eConductorMode.Add)
                {
                    var maxDto = loGridList.OrderByDescending(dto => dto.ICOLUMN_NO).FirstOrDefault();
                    int currentSequence = 1;

                    if (maxDto != null)
                    {
                        currentSequence = maxDto.ICOLUMN_NO + 1;
                    }

                    poNewEntity.ICOLUMN_NO = currentSequence;
                }

                loResult = await _GSM10510Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peConductorMode);
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}