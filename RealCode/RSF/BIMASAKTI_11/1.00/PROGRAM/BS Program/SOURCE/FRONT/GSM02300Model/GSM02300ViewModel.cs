using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Text;
using GSM02300Common.DTO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;

namespace GSM02300Model
{
    public class GSM02300ViewModel : R_ViewModel<GSM02300DTO>
    {
        private Model.GSM02300Model _GSM02300Model = new Model.GSM02300Model();
        public ObservableCollection<GSM02300DTO> loGridList = new ObservableCollection<GSM02300DTO>();
        public ObservableCollection<GSM02300PropertyTypeDTO> loGridListPropertyType = new ObservableCollection<GSM02300PropertyTypeDTO>();
        public GSM02300DTO loEntity = new GSM02300DTO();


        public async Task GetAllProperty()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM02300Model.GetAllPropertyAsync();
                loGridList = new ObservableCollection<GSM02300DTO>(loReturn.Data);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyType()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM02300Model.GetAllPropertyTypeAsync();
                loGridListPropertyType = new ObservableCollection<GSM02300PropertyTypeDTO>(loReturn.Data);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
