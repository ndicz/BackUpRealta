using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GSM00700Common.DTO;
using GSM00700Common.DTO.Upload_DTO;
using GSM00700Common.DTO.Upload_DTO_GSM00720;
using GSM00700Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GSM00700Front
{
    public partial class GSM00720Upload : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }

        private GSM00720UploadViewModel _ViewModelUpload = new GSM00720UploadViewModel();
        private GSM00720ViewModel _GSM00720ViewModel = new GSM00720ViewModel();
        private R_Grid<GSM00720UploadCashFlowPlanDTO> _ConGriedRef;
        private R_Conductor R_conduct;
        private R_ConductorGrid R_ConductorGrid;
        private R_Grid<GSM00720DTO> _gridRef00720;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private bool IsUploadSuccesful = true;

        private bool IsFileExist = false;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            var param = (GSM00720UploadCashFlowPlanDTO)poParameter;
            try
            {
                _ViewModelUpload.CashFlowPlanGroupCode = param.CCASHFLOW_GROUP_CODE;
                _ViewModelUpload.CashFlowPlanGroupName = param.CCASHFLOW_GROUP_NAME;
                _ViewModelUpload.CashFlowPlanCode = param.CCASH_FLOW_CODE;
                _ViewModelUpload.CashFlowPlanName = param.CCASH_FLOW_NAME;
                _ViewModelUpload.CashFlowPlanYear = param.CCYEAR;
                _ViewModelUpload.SelectedCompanyId = loClientHelper.CompanyId;
                _ViewModelUpload.SelectedUserId = loClientHelper.UserId;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (GSM00720UploadCashFlowPlanDTO)eventArgs.Data;

            if (loData.LEXIST)
            {
                eventArgs.RowStyle = new R_GridRowRenderStyle
                {
                    FontColor = "red"
                };
            }
        }

        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<GSM00720UploadCashFlowPlanExcelDTO> loExcelList = new List<GSM00720UploadCashFlowPlanExcelDTO>();

                loExcelList = _ViewModelUpload.loUploadCashFlowPlanDisplayList.Select(x => new GSM00720UploadCashFlowPlanExcelDTO()
                {
                    PeriodNo = x.CPERIOD_NO,
                    LocalAmount = x.NLOCAL_AMOUNT,
                    BaseAmount = x.NBASE_AMOUNT,
                    Notes = x.CNOTES,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "CashFlowPlan";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error CashFlowPlan.xlsx";

                await JS.downloadFileFromStreamHandler(saveFileName, loByteFile);
                await this.Close(true, true);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        B:
            loEx.ThrowExceptionIfErrors();
        }
        public void ReadExcelFile()
        {
            var loEx = new R_Exception();
            List<GSM00720UploadCashFlowPlanExcelDTO> loExtract = new List<GSM00720UploadCashFlowPlanExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(_ViewModelUpload.fileByte, new string[] { "CashFlowPlan" });

                var loResult = R_FrontUtility.R_ConvertTo<GSM00720UploadCashFlowPlanExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<GSM00720UploadCashFlowPlanExcelDTO>(loResult);

                _ViewModelUpload.loUploadCashFlowPlanList = loExtract.Select(x => new GSM00720UploadCashFlowPlanDTO
                {
                    CPERIOD_NO = x.PeriodNo,
                    NLOCAL_AMOUNT = x.LocalAmount,
                    NBASE_AMOUNT = x.BaseAmount,
                    CCASHFLOW_GROUP_CODE = _ViewModelUpload.CashFlowPlanGroupCode,
                    CCASH_FLOW_CODE = _ViewModelUpload.CashFlowPlanCode,
                    CCYEAR = _ViewModelUpload.CashFlowPlanYear,
                    CNOTES = "",
                    LEXIST = false,
                }).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                _ViewModelUpload.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                _ViewModelUpload.fileByte = loMS.ToArray();

               ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", "File Type must Microsoft Excel .xlsx", R_eMessageBoxButtonType.OK);
                }
                if (eventArgs.File.Name.Length > 0)
                {
                    IsFileExist = true;
                }
                else
                {
                    IsFileExist = false;
                }
                await _ConGriedRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (_ViewModelUpload.IsErrorEmptyFile)
                {
                    await R_MessageBox.Show("", "File is Empty", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loEx.Add(ex);
                }
            }

            //loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _ViewModelUpload.AttachFile();
                await _ViewModelUpload.ValidateUpload();
                await _ViewModelUpload.GetUploadCashFlowPlanListStreamAsync();
                IsUploadSuccesful = !_ViewModelUpload.VisibleError;
                eventArgs.ListEntityResult = _ViewModelUpload.loUploadCashFlowPlanDisplayList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            //loEx.ThrowExceptionIfErrors();
        }

        private async Task OnProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                await R_ConductorGrid.R_SaveBatch();
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task OnCancel()
        {
            await this.Close(true, false);
        }
        private async Task R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            var loEx = new R_Exception();
            try
            {

                var loTemp = await R_MessageBox.Show("", "Are You sure want process these records? ", R_eMessageBoxButtonType.YesNo);

                if (loTemp == R_eMessageBoxResult.No)
                {
                    events.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _ViewModelUpload.loUploadCashFlowPlanList = (List<GSM00720UploadCashFlowPlanDTO>)eventArgs.Data;
                await _ViewModelUpload.SaveUploadCashFlowPlanAsync();



            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await this.Close(true, true);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
