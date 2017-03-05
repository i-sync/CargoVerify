namespace CargoVerify
{
    partial class LoadingVerifyForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBack = new System.Windows.Forms.Button();
            this.txtBarCode = new MyTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCNO1 = new MyTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbManually = new System.Windows.Forms.CheckBox();
            this.dataGrid = new System.Windows.Forms.DataGrid();
            this.btnManually = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(152, 268);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(72, 24);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtBarCode
            // 
            this.txtBarCode.Enabled = false;
            this.txtBarCode.Location = new System.Drawing.Point(55, 28);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(130, 21);
            this.txtBarCode.TabIndex = 1;
            this.txtBarCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarCode_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.Text = "条  码:";
            // 
            // txtCNO1
            // 
            this.txtCNO1.Location = new System.Drawing.Point(55, 2);
            this.txtCNO1.Name = "txtCNO1";
            this.txtCNO1.Size = new System.Drawing.Size(180, 21);
            this.txtCNO1.TabIndex = 0;
            this.txtCNO1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCNO1_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.Text = "提货单:";
            // 
            // cbManually
            // 
            this.cbManually.Enabled = false;
            this.cbManually.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cbManually.Location = new System.Drawing.Point(185, 30);
            this.cbManually.Name = "cbManually";
            this.cbManually.Size = new System.Drawing.Size(53, 20);
            this.cbManually.TabIndex = 2;
            this.cbManually.Text = "手动";
            this.cbManually.CheckStateChanged += new System.EventHandler(this.cbManually_CheckStateChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid.Location = new System.Drawing.Point(4, 55);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(231, 207);
            this.dataGrid.TabIndex = 3;
            // 
            // btnManually
            // 
            this.btnManually.Enabled = false;
            this.btnManually.Location = new System.Drawing.Point(17, 268);
            this.btnManually.Name = "btnManually";
            this.btnManually.Size = new System.Drawing.Size(72, 24);
            this.btnManually.TabIndex = 4;
            this.btnManually.Text = "验证";
            this.btnManually.Click += new System.EventHandler(this.btnManually_Click);
            // 
            // LoadingVerifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.cbManually);
            this.Controls.Add(this.txtBarCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCNO1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnManually);
            this.Controls.Add(this.btnBack);
            this.Menu = this.mainMenu1;
            this.Name = "LoadingVerifyForm";
            this.Text = "装车验证";
            this.Load += new System.EventHandler(this.LoadingVerifyForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private MyTextBox txtBarCode;
        private System.Windows.Forms.Label label2;
        private MyTextBox txtCNO1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbManually;
        private System.Windows.Forms.DataGrid dataGrid;
        private System.Windows.Forms.Button btnManually;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}