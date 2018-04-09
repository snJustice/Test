namespace 条烟纠错系统
{
    partial class SearchForm
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
            this.components = new System.ComponentModel.Container();
            this.btnSearch = new CCWin.SkinControl.SkinButton();
            this.skinGroupBox1 = new CCWin.SkinControl.SkinGroupBox();
            this.btnNextCustomer = new CCWin.SkinControl.SkinButton();
            this.btnLastCustomer = new CCWin.SkinControl.SkinButton();
            this.txtPickDate = new DevExpress.XtraEditors.TextEdit();
            this.txtCustomerID = new DevExpress.XtraEditors.TextEdit();
            this.txtPickPackage = new DevExpress.XtraEditors.TextEdit();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.dgCurrentPackageDetails = new System.Windows.Forms.DataGridView();
            this.dgCurrentPackage = new System.Windows.Forms.DataGridView();
            this.skinGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPickDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPickPackage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCurrentPackageDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCurrentPackage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSearch.DownBack = null;
            this.btnSearch.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(801, 37);
            this.btnSearch.MouseBack = null;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormlBack = null;
            this.btnSearch.Size = new System.Drawing.Size(73, 29);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // skinGroupBox1
            // 
            this.skinGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox1.BorderColor = System.Drawing.Color.Red;
            this.skinGroupBox1.Controls.Add(this.btnNextCustomer);
            this.skinGroupBox1.Controls.Add(this.btnLastCustomer);
            this.skinGroupBox1.Controls.Add(this.txtPickDate);
            this.skinGroupBox1.Controls.Add(this.txtCustomerID);
            this.skinGroupBox1.Controls.Add(this.txtPickPackage);
            this.skinGroupBox1.Controls.Add(this.skinLabel2);
            this.skinGroupBox1.Controls.Add(this.skinLabel1);
            this.skinGroupBox1.Controls.Add(this.skinLabel7);
            this.skinGroupBox1.Controls.Add(this.btnSearch);
            this.skinGroupBox1.ForeColor = System.Drawing.Color.Blue;
            this.skinGroupBox1.Location = new System.Drawing.Point(0, 3);
            this.skinGroupBox1.Name = "skinGroupBox1";
            this.skinGroupBox1.RectBackColor = System.Drawing.Color.White;
            this.skinGroupBox1.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinGroupBox1.Size = new System.Drawing.Size(943, 134);
            this.skinGroupBox1.TabIndex = 2;
            this.skinGroupBox1.TabStop = false;
            this.skinGroupBox1.Text = "操作";
            this.skinGroupBox1.TitleBorderColor = System.Drawing.Color.Red;
            this.skinGroupBox1.TitleRectBackColor = System.Drawing.Color.White;
            this.skinGroupBox1.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // btnNextCustomer
            // 
            this.btnNextCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextCustomer.BackColor = System.Drawing.Color.Transparent;
            this.btnNextCustomer.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnNextCustomer.DownBack = null;
            this.btnNextCustomer.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNextCustomer.Location = new System.Drawing.Point(459, 91);
            this.btnNextCustomer.MouseBack = null;
            this.btnNextCustomer.Name = "btnNextCustomer";
            this.btnNextCustomer.NormlBack = null;
            this.btnNextCustomer.Size = new System.Drawing.Size(112, 29);
            this.btnNextCustomer.TabIndex = 25;
            this.btnNextCustomer.Text = "下一客户";
            this.btnNextCustomer.UseVisualStyleBackColor = false;
            this.btnNextCustomer.Visible = false;
            this.btnNextCustomer.Click += new System.EventHandler(this.btnNextCustomer_Click);
            // 
            // btnLastCustomer
            // 
            this.btnLastCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLastCustomer.BackColor = System.Drawing.Color.Transparent;
            this.btnLastCustomer.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnLastCustomer.DownBack = null;
            this.btnLastCustomer.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLastCustomer.Location = new System.Drawing.Point(255, 91);
            this.btnLastCustomer.MouseBack = null;
            this.btnLastCustomer.Name = "btnLastCustomer";
            this.btnLastCustomer.NormlBack = null;
            this.btnLastCustomer.Size = new System.Drawing.Size(107, 29);
            this.btnLastCustomer.TabIndex = 24;
            this.btnLastCustomer.Text = "上一客户";
            this.btnLastCustomer.UseVisualStyleBackColor = false;
            this.btnLastCustomer.Visible = false;
            this.btnLastCustomer.Click += new System.EventHandler(this.btnLastCustomer_Click);
            // 
            // txtPickDate
            // 
            this.txtPickDate.Location = new System.Drawing.Point(98, 39);
            this.txtPickDate.Name = "txtPickDate";
            this.txtPickDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtPickDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPickDate.Properties.Appearance.Options.UseFont = true;
            this.txtPickDate.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
            this.txtPickDate.Properties.Mask.EditMask = "\\d\\d\\d\\d-\\d\\d-\\d\\d";
            this.txtPickDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.txtPickDate.Size = new System.Drawing.Size(120, 26);
            this.txtPickDate.TabIndex = 23;
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Location = new System.Drawing.Point(597, 38);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtCustomerID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerID.Properties.Appearance.Options.UseFont = true;
            this.txtCustomerID.Properties.Mask.EditMask = "d";
            this.txtCustomerID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCustomerID.Size = new System.Drawing.Size(119, 26);
            this.txtCustomerID.TabIndex = 22;
            // 
            // txtPickPackage
            // 
            this.txtPickPackage.Location = new System.Drawing.Point(340, 39);
            this.txtPickPackage.Name = "txtPickPackage";
            this.txtPickPackage.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtPickPackage.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPickPackage.Properties.Appearance.Options.UseFont = true;
            this.txtPickPackage.Properties.Mask.EditMask = "d";
            this.txtPickPackage.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPickPackage.Size = new System.Drawing.Size(120, 26);
            this.txtPickPackage.TabIndex = 21;
            // 
            // skinLabel2
            // 
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.ForeColor = System.Drawing.Color.SaddleBrown;
            this.skinLabel2.Location = new System.Drawing.Point(522, 42);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(79, 20);
            this.skinLabel2.TabIndex = 19;
            this.skinLabel2.Text = "客户编号：";
            // 
            // skinLabel1
            // 
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.skinLabel1.Location = new System.Drawing.Point(279, 42);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(65, 20);
            this.skinLabel1.TabIndex = 17;
            this.skinLabel1.Text = "订单号：";
            // 
            // skinLabel7
            // 
            this.skinLabel7.AutoSize = true;
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel7.ForeColor = System.Drawing.Color.SaddleBrown;
            this.skinLabel7.Location = new System.Drawing.Point(24, 42);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(79, 20);
            this.skinLabel7.TabIndex = 15;
            this.skinLabel7.Text = "分拣日期：";
            // 
            // dgCurrentPackageDetails
            // 
            this.dgCurrentPackageDetails.AllowUserToAddRows = false;
            this.dgCurrentPackageDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCurrentPackageDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCurrentPackageDetails.Location = new System.Drawing.Point(469, 193);
            this.dgCurrentPackageDetails.Name = "dgCurrentPackageDetails";
            this.dgCurrentPackageDetails.ReadOnly = true;
            this.dgCurrentPackageDetails.RowTemplate.Height = 23;
            this.dgCurrentPackageDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCurrentPackageDetails.Size = new System.Drawing.Size(474, 305);
            this.dgCurrentPackageDetails.TabIndex = 3;
            // 
            // dgCurrentPackage
            // 
            this.dgCurrentPackage.AllowUserToAddRows = false;
            this.dgCurrentPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCurrentPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCurrentPackage.Location = new System.Drawing.Point(12, 193);
            this.dgCurrentPackage.Name = "dgCurrentPackage";
            this.dgCurrentPackage.ReadOnly = true;
            this.dgCurrentPackage.RowTemplate.Height = 23;
            this.dgCurrentPackage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCurrentPackage.Size = new System.Drawing.Size(458, 305);
            this.dgCurrentPackage.TabIndex = 4;
            this.dgCurrentPackage.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCurrentPackage_RowEnter);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 510);
            this.Controls.Add(this.dgCurrentPackage);
            this.Controls.Add(this.dgCurrentPackageDetails);
            this.Controls.Add(this.skinGroupBox1);
            this.Name = "SearchForm";
            this.Text = "查询";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.skinGroupBox1.ResumeLayout(false);
            this.skinGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPickDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPickPackage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCurrentPackageDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCurrentPackage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinButton btnSearch;
        private CCWin.SkinControl.SkinGroupBox skinGroupBox1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private System.Windows.Forms.DataGridView dgCurrentPackageDetails;
        private System.Windows.Forms.DataGridView dgCurrentPackage;
        private DevExpress.XtraEditors.TextEdit txtPickPackage;
        private DevExpress.XtraEditors.TextEdit txtPickDate;
        private DevExpress.XtraEditors.TextEdit txtCustomerID;
        private CCWin.SkinControl.SkinButton btnNextCustomer;
        private CCWin.SkinControl.SkinButton btnLastCustomer;
    }
}