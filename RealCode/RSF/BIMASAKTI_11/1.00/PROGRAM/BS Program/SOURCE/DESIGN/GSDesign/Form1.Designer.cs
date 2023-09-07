namespace GSDesign
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GSM00700 = new Button();
            BaseHeader = new Button();
            BaseHeaderLanscape = new Button();
            SuspendLayout();
            // 
            // GSM00700
            // 
            GSM00700.Location = new Point(1, 11);
            GSM00700.Name = "GSM00700";
            GSM00700.Size = new Size(145, 23);
            GSM00700.TabIndex = 0;
            GSM00700.Text = "GSM00700";
            GSM00700.UseVisualStyleBackColor = true;
            GSM00700.Click += GSM00700_Click;
            // 
            // BaseHeader
            // 
            BaseHeader.Location = new Point(1, 69);
            BaseHeader.Name = "BaseHeader";
            BaseHeader.Size = new Size(145, 23);
            BaseHeader.TabIndex = 1;
            BaseHeader.Text = "BaseHeader";
            BaseHeader.UseVisualStyleBackColor = true;
            BaseHeader.Click += BaseHeader_Click;
            // 
            // BaseHeaderLanscape
            // 
            BaseHeaderLanscape.Location = new Point(1, 40);
            BaseHeaderLanscape.Name = "BaseHeaderLanscape";
            BaseHeaderLanscape.Size = new Size(145, 23);
            BaseHeaderLanscape.TabIndex = 2;
            BaseHeaderLanscape.Text = "BaseHeaderLanscape";
            BaseHeaderLanscape.UseVisualStyleBackColor = true;
            BaseHeaderLanscape.Click += BaseHeaderLanscape_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BaseHeaderLanscape);
            Controls.Add(BaseHeader);
            Controls.Add(GSM00700);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button GSM00700;
        private Button BaseHeader;
        private Button BaseHeaderLanscape;
    }
}