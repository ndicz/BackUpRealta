using FastReport;
using System.Collections;

namespace GSDesign
{
    public partial class Form1 : Form
    {
        private Report loReport;
        public Form1()
        {
            InitializeComponent();
        }

        private void GSM00700_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(GSM00700Common.Model.GSM00700ModelDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loReport = new Report();
        }

        private void BaseHeader_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(BaseHeaderReportCommon.Model.GenerateDataModelHeader.DefaultData);
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void BaseHeaderLanscape_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(BaseHeaderReportCommon.Model.GenerateDataModelHeader.DefaultData);
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }
    }
}