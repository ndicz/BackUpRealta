namespace FastReportDesigner
{
    partial class DesignForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnProduct = new Button();
            btnReportInherit = new Button();
            btnReportBaseHeader = new Button();
            btnGlobalization = new Button();
            btnHeaderDetail = new Button();
            btnParentChildSalesmanProduct = new Button();
            btnGroupSalesmanProduct = new Button();
            btnMatrixSalesmanProduct = new Button();
            errorProvider1 = new ErrorProvider(components);
            errorProvider2 = new ErrorProvider(components);
            errorProvider3 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider3).BeginInit();
            SuspendLayout();
            // 
            // btnProduct
            // 
            btnProduct.Location = new Point(3, 2);
            btnProduct.Name = "btnProduct";
            btnProduct.Size = new Size(177, 23);
            btnProduct.TabIndex = 3;
            btnProduct.Text = "Product Object";
            btnProduct.UseVisualStyleBackColor = true;
            btnProduct.Click += btnProduct_Click;
            // 
            // btnReportInherit
            // 
            btnReportInherit.Location = new Point(3, 89);
            btnReportInherit.Name = "btnReportInherit";
            btnReportInherit.Size = new Size(177, 23);
            btnReportInherit.TabIndex = 5;
            btnReportInherit.Text = "Report Inherit";
            btnReportInherit.UseVisualStyleBackColor = true;
            btnReportInherit.Click += btnReportInherit_Click;
            // 
            // btnReportBaseHeader
            // 
            btnReportBaseHeader.Location = new Point(3, 60);
            btnReportBaseHeader.Name = "btnReportBaseHeader";
            btnReportBaseHeader.Size = new Size(177, 23);
            btnReportBaseHeader.TabIndex = 10;
            btnReportBaseHeader.Text = "Report Base Header";
            btnReportBaseHeader.UseVisualStyleBackColor = true;
            btnReportBaseHeader.Click += btnReportBaseHeader_Click;
            // 
            // btnGlobalization
            // 
            btnGlobalization.Location = new Point(3, 31);
            btnGlobalization.Name = "btnGlobalization";
            btnGlobalization.Size = new Size(177, 23);
            btnGlobalization.TabIndex = 11;
            btnGlobalization.Text = "Globalization";
            btnGlobalization.UseVisualStyleBackColor = true;
            btnGlobalization.Click += btnGlobalization_Click;
            // 
            // btnHeaderDetail
            // 
            btnHeaderDetail.Location = new Point(200, 2);
            btnHeaderDetail.Name = "btnHeaderDetail";
            btnHeaderDetail.Size = new Size(152, 23);
            btnHeaderDetail.TabIndex = 12;
            btnHeaderDetail.Text = "Report Header Detail";
            btnHeaderDetail.UseVisualStyleBackColor = true;
            btnHeaderDetail.Click += btnHeaderDetail_Click;
            // 
            // btnParentChildSalesmanProduct
            // 
            btnParentChildSalesmanProduct.Location = new Point(387, 60);
            btnParentChildSalesmanProduct.Name = "btnParentChildSalesmanProduct";
            btnParentChildSalesmanProduct.Size = new Size(200, 23);
            btnParentChildSalesmanProduct.TabIndex = 15;
            btnParentChildSalesmanProduct.Text = "Parent Child Salesman Product";
            btnParentChildSalesmanProduct.UseVisualStyleBackColor = true;
            btnParentChildSalesmanProduct.Click += btnParentChildSalesmanProduct_Click;
            // 
            // btnGroupSalesmanProduct
            // 
            btnGroupSalesmanProduct.Location = new Point(387, 2);
            btnGroupSalesmanProduct.Name = "btnGroupSalesmanProduct";
            btnGroupSalesmanProduct.Size = new Size(200, 23);
            btnGroupSalesmanProduct.TabIndex = 14;
            btnGroupSalesmanProduct.Text = "Group Salesman Product";
            btnGroupSalesmanProduct.UseVisualStyleBackColor = true;
            btnGroupSalesmanProduct.Click += btnGroupSalesmanProduct_Click;
            // 
            // btnMatrixSalesmanProduct
            // 
            btnMatrixSalesmanProduct.Location = new Point(387, 31);
            btnMatrixSalesmanProduct.Name = "btnMatrixSalesmanProduct";
            btnMatrixSalesmanProduct.Size = new Size(200, 23);
            btnMatrixSalesmanProduct.TabIndex = 13;
            btnMatrixSalesmanProduct.Text = "Matrix Salesman Product";
            btnMatrixSalesmanProduct.UseVisualStyleBackColor = true;
            btnMatrixSalesmanProduct.Click += btnMatrixSalesmanProduct_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            errorProvider3.ContainerControl = this;
            // 
            // DesignForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnParentChildSalesmanProduct);
            Controls.Add(btnGroupSalesmanProduct);
            Controls.Add(btnMatrixSalesmanProduct);
            Controls.Add(btnHeaderDetail);
            Controls.Add(btnGlobalization);
            Controls.Add(btnReportBaseHeader);
            Controls.Add(btnReportInherit);
            Controls.Add(btnProduct);
            Name = "DesignForm";
            Text = "DesignForm";
            Load += DesignForm_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider2).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnProduct;
        private Button btnReportInherit;
        private Button btnReportBaseHeader;
        private Button btnGlobalization;
        private Button btnHeaderDetail;
        private Button btnParentChildSalesmanProduct;
        private Button btnGroupSalesmanProduct;
        private Button btnMatrixSalesmanProduct;
        private ErrorProvider errorProvider1;
        private ErrorProvider errorProvider2;
        private ErrorProvider errorProvider3;
    }
}