namespace 条烟纠错系统
{
    partial class ExceptionForm
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
            this.skinGroupBox1 = new CCWin.SkinControl.SkinGroupBox();
            this.btnClearAll = new CCWin.SkinControl.SkinButton();
            this.txtPickDate = new DevExpress.XtraEditors.TextEdit();
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.btnSearch = new CCWin.SkinControl.SkinButton();
            this.dgExceptionTable = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pickDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.custmoerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exceptionDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skinGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPickDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgExceptionTable)).BeginInit();
            this.SuspendLayout();
            // 
            // skinGroupBox1
            // 
            this.skinGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox1.BorderColor = System.Drawing.Color.Red;
            this.skinGroupBox1.Controls.Add(this.btnClearAll);
            this.skinGroupBox1.Controls.Add(this.txtPickDate);
            this.skinGroupBox1.Controls.Add(this.skinLabel7);
            this.skinGroupBox1.Controls.Add(this.btnSearch);
            this.skinGroupBox1.ForeColor = System.Drawing.Color.Blue;
            this.skinGroupBox1.Location = new System.Drawing.Point(0, 2);
            this.skinGroupBox1.Name = "skinGroupBox1";
            this.skinGroupBox1.RectBackColor = System.Drawing.Color.White;
            this.skinGroupBox1.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinGroupBox1.Size = new System.Drawing.Size(1115, 108);
            this.skinGroupBox1.TabIndex = 3;
            this.skinGroupBox1.TabStop = false;
            this.skinGroupBox1.Text = "异常查询";
            this.skinGroupBox1.TitleBorderColor = System.Drawing.Color.Red;
            this.skinGroupBox1.TitleRectBackColor = System.Drawing.Color.White;
            this.skinGroupBox1.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // btnClearAll
            // 
            this.btnClearAll.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAll.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnClearAll.DownBack = null;
            this.btnClearAll.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearAll.Location = new System.Drawing.Point(37, 73);
            this.btnClearAll.MouseBack = null;
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.NormlBack = null;
            this.btnClearAll.Size = new System.Drawing.Size(113, 29);
            this.btnClearAll.TabIndex = 24;
            this.btnClearAll.Text = "清除所有";
            this.btnClearAll.UseVisualStyleBackColor = false;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // txtPickDate
            // 
            this.txtPickDate.Location = new System.Drawing.Point(258, 39);
            this.txtPickDate.Name = "txtPickDate";
            this.txtPickDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtPickDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPickDate.Properties.Appearance.Options.UseFont = true;
            this.txtPickDate.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
            this.txtPickDate.Properties.Mask.EditMask = "\\d\\d\\d\\d-\\d\\d-\\d\\d";
            this.txtPickDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.txtPickDate.Size = new System.Drawing.Size(200, 26);
            this.txtPickDate.TabIndex = 23;
            // 
            // skinLabel7
            // 
            this.skinLabel7.AutoSize = true;
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel7.ForeColor = System.Drawing.Color.SaddleBrown;
            this.skinLabel7.Location = new System.Drawing.Point(184, 42);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(79, 20);
            this.skinLabel7.TabIndex = 15;
            this.skinLabel7.Text = "分拣日期：";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSearch.DownBack = null;
            this.btnSearch.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(973, 33);
            this.btnSearch.MouseBack = null;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormlBack = null;
            this.btnSearch.Size = new System.Drawing.Size(73, 29);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgExceptionTable
            // 
            this.dgExceptionTable.AllowUserToAddRows = false;
            this.dgExceptionTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgExceptionTable.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgExceptionTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExceptionTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.pickDate,
            this.lineID,
            this.custmoerID,
            this.customerName,
            this.packID,
            this.productID,
            this.exceptionDetail});
            this.dgExceptionTable.Location = new System.Drawing.Point(7, 121);
            this.dgExceptionTable.Name = "dgExceptionTable";
            this.dgExceptionTable.ReadOnly = true;
            this.dgExceptionTable.RowTemplate.Height = 23;
            this.dgExceptionTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgExceptionTable.Size = new System.Drawing.Size(1108, 371);
            this.dgExceptionTable.TabIndex = 4;
            // 
            // Time
            // 
            this.Time.Frozen = true;
            this.Time.HeaderText = "发生时间";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Visible = false;
            // 
            // pickDate
            // 
            this.pickDate.Frozen = true;
            this.pickDate.HeaderText = "日期";
            this.pickDate.Name = "pickDate";
            this.pickDate.ReadOnly = true;
            this.pickDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pickDate.Visible = false;
            // 
            // lineID
            // 
            this.lineID.Frozen = true;
            this.lineID.HeaderText = "线路名称";
            this.lineID.Name = "lineID";
            this.lineID.ReadOnly = true;
            this.lineID.Visible = false;
            // 
            // custmoerID
            // 
            this.custmoerID.Frozen = true;
            this.custmoerID.HeaderText = "客户编码";
            this.custmoerID.Name = "custmoerID";
            this.custmoerID.ReadOnly = true;
            this.custmoerID.Visible = false;
            // 
            // customerName
            // 
            this.customerName.Frozen = true;
            this.customerName.HeaderText = "客户名称";
            this.customerName.Name = "customerName";
            this.customerName.ReadOnly = true;
            this.customerName.Visible = false;
            // 
            // packID
            // 
            this.packID.Frozen = true;
            this.packID.HeaderText = "包号";
            this.packID.Name = "packID";
            this.packID.ReadOnly = true;
            this.packID.Visible = false;
            // 
            // productID
            // 
            this.productID.Frozen = true;
            this.productID.HeaderText = "商品名称";
            this.productID.Name = "productID";
            this.productID.ReadOnly = true;
            this.productID.Visible = false;
            // 
            // exceptionDetail
            // 
            this.exceptionDetail.Frozen = true;
            this.exceptionDetail.HeaderText = "异常原因";
            this.exceptionDetail.Name = "exceptionDetail";
            this.exceptionDetail.ReadOnly = true;
            this.exceptionDetail.Visible = false;
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 458);
            this.Controls.Add(this.dgExceptionTable);
            this.Controls.Add(this.skinGroupBox1);
            this.Name = "ExceptionForm";
            this.Text = "异常查询";
            this.skinGroupBox1.ResumeLayout(false);
            this.skinGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPickDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgExceptionTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinGroupBox skinGroupBox1;
        private DevExpress.XtraEditors.TextEdit txtPickDate;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private CCWin.SkinControl.SkinButton btnSearch;
        private System.Windows.Forms.DataGridView dgExceptionTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn pickDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineID;
        private System.Windows.Forms.DataGridViewTextBoxColumn custmoerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn packID;
        private System.Windows.Forms.DataGridViewTextBoxColumn productID;
        private System.Windows.Forms.DataGridViewTextBoxColumn exceptionDetail;
        private CCWin.SkinControl.SkinButton btnClearAll;
    }
}