using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LMI00100Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMI00100Model
{
    public class LMI00100ViewModel  : R_ViewModel<LMI00100DTO>
    {
        private Model.LMI00100Model _LMI00100Model = new Model.LMI00100Model();
        public ObservableCollection<LMI00100DTO> loGridList = new ObservableCollection<LMI00100DTO>();
        public List<LMI00100PropertyDTO>PropertyList { get; set; } = new List<LMI00100PropertyDTO>();

        public string PropertyValue = "";


        public async Task GetProperty()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMI00100Model.GetAllPropertyAsync();

                PropertyList = loResult.Data;
                PropertyValue = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMI00100Model.GetAllBankAccountAsync(PropertyValue);

                loGridList = new ObservableCollection<LMI00100DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
