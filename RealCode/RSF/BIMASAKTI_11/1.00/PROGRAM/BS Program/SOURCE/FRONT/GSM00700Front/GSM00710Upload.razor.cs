using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GSM00700Common.DTO;
using GSM00700Common.DTO.Upload_DTO;
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
    public partial class GSM00710Upload : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }

        private GSM00710UploadViewModel _ViewModelUpload = new GSM00710UploadViewModel();
        private GSM00710ViewModel _GSM00710ViewModel = new GSM00710ViewModel();
        private R_Grid<GSM00710UploadCashFlowDTO> _ConGriedRef;
        private R_Conductor R_conduct;
        private R_ConductorGrid R_ConductorGrid;
        private R_Grid<GSM00710DTO> _gridRef00710;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private bool IsUploadSuccesful = true;

        private bool IsFileExist = false;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            var param = (GSM00710UploadCashFlowDTO)poParameter;
            try
            {
                _ViewModelUpload.CashFlowGroupCode = param.CCASHFLOW_GROUP_CODE;
                _ViewModelUpload.CashFlowGroupName = param.CCASHFLOW_GROUP_NAME;
                
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
            var loData = (GSM00710UploadCashFlowDTO)eventArgs.Data;

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
                List<GSM00710UploadCashFlowExcelDTO> loExcelList = new List<GSM00710UploadCashFlowExcelDTO>();

                loExcelList = _ViewModelUpload.loUploadCashFlowDisplayList.Select(x => new GSM00710UploadCashFlowExcelDTO()
                {
                    DisplaySeq = x.CSEQ,
                    CashFlowCode = x.CCASHFLOW_CODE,
                    CashFlowName = x.CCASH_FLOW_NAME,
                    CashFlowType = x.CCASHFLOW_TYPE,
                    Notes = x.NOTES_,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "CashFlow";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error CashFlow.xlsx";

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

        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                _ViewModelUpload.fileByte = loMS.ToArray();

                _ViewModelUpload.ReadExcelFile();

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

                await _ViewModelUpload.GetUploadCashFlowListStreamAsync();
                IsUploadSuccesful = !_ViewModelUpload.VisibleError;
                eventArgs.ListEntityResult = _ViewModelUpload.loUploadCashFlowDisplayList;
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
                _ViewModelUpload.loUploadCashFlowList = (List<GSM00710UploadCashFlowDTO>)eventArgs.Data;
                await _ViewModelUpload.SaveUploadCashFlowAsync();

               

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
