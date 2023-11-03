﻿using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02000ViewModel : R_ViewModel<GSL02000DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL02000DTO> GeographyGrid = new ObservableCollection<GSL02000DTO>();

        public async Task GetGeographyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02000GetGeographyListAsync();

                GeographyGrid = new ObservableCollection<GSL02000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
