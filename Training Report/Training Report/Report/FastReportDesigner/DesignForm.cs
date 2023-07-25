using FastReport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastReportDesigner
{
    public partial class DesignForm : Form
    {
        private Report loReport;
        public DesignForm()
        {
            InitializeComponent();
        }
        private void DesignForm_Load(object sender, EventArgs e)
        {
            loReport = new Report();
        }


        private void btnProduct_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(ReportCommon.ProductObject.GenerateDataModel.DefaultData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void btnGlobalization_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(ReportCommon.ProductObject.GenerateDataModel.DefaultData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void btnReportBaseHeader_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(ReportCommon.BaseHeader.GenerateDataModel.DefaultData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void btnReportInherit_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(ReportCommon.ProductObject.GenerateDataModel.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void btnHeaderDetail_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(ReportCommon.ReportHeaderDetail.GenerateDataModel.DefaultData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void btnGroupSalesmanProduct_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(ReportCommon.SalesProduct.GenerateDataModel.DefaultRawData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void btnMatrixSalesmanProduct_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(ReportCommon.SalesProduct.GenerateDataModel.DefaultRawData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void btnParentChildSalesmanProduct_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(ReportCommon.SalesProduct.GenerateDataModel.DefaultProductPerSalesmanData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
