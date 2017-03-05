namespace CargoVerify
{
    partial class OutVerifyForm
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
            this.dataGrid = new System.Windows.Forms.DataGrid();
            this.cbManually = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnManually = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.btnDepartment = new System.Windows.Forms.Button();
            this.btnSecurity = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblcUser = new System.Windows.Forms.Label();
            this.lblcUser2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbliUser2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbliUser3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbliUser4 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbliUser = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtBarCode = new CargoVerify.MyTextBox();
            this.txtCNO = new CargoVerify.MyTextBox();
            this.panelConfirm = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.txtConfirm = new CargoVerify.MyTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelConfirm.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid.Location = new System.Drawing.Point(4, 51);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(231, 128);
            this.dataGrid.TabIndex = 3;
            // 
            // cbManually
            // 
            this.cbManually.Enabled = false;
            this.cbManually.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cbManually.Location = new System.Drawing.Point(185, 28);
            this.cbManually.Name = "cbManually";
            this.cbManually.Size = new System.Drawing.Size(53, 20);
            this.cbManually.TabIndex = 2;
            this.cbManually.Text = "手动";
            this.cbManually.CheckStateChanged += new System.EventHandler(this.cbManually_CheckStateChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.Text = "条  码:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.Text = "出门证:";
            // 
            // btnManually
            // 
            this.btnManually.Enabled = false;
            this.btnManually.Location = new System.Drawing.Point(10, 268);
            this.btnManually.Name = "btnManually";
            this.btnManually.Size = new System.Drawing.Size(103, 27);
            this.btnManually.TabIndex = 4;
            this.btnManually.Text = "手动验证";
            this.btnManually.Click += new System.EventHandler(this.btnManually_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(125, 268);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(104, 27);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnDepartment
            // 
            this.btnDepartment.Enabled = false;
            this.btnDepartment.Location = new System.Drawing.Point(125, 241);
            this.btnDepartment.Name = "btnDepartment";
            this.btnDepartment.Size = new System.Drawing.Size(104, 27);
            this.btnDepartment.TabIndex = 6;
            this.btnDepartment.Text = "物资部确认";
            this.btnDepartment.Click += new System.EventHandler(this.btnDepartment_Click);
            // 
            // btnSecurity
            // 
            this.btnSecurity.Enabled = false;
            this.btnSecurity.Location = new System.Drawing.Point(10, 241);
            this.btnSecurity.Name = "btnSecurity";
            this.btnSecurity.Size = new System.Drawing.Size(103, 27);
            this.btnSecurity.TabIndex = 5;
            this.btnSecurity.Text = "保卫部确认";
            this.btnSecurity.Click += new System.EventHandler(this.btnSecurity_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(1, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.Text = "提货人:";
            // 
            // lblcUser
            // 
            this.lblcUser.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.lblcUser.Location = new System.Drawing.Point(57, 182);
            this.lblcUser.Name = "lblcUser";
            this.lblcUser.Size = new System.Drawing.Size(44, 20);
            // 
            // lblcUser2
            // 
            this.lblcUser2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.lblcUser2.Location = new System.Drawing.Point(170, 182);
            this.lblcUser2.Name = "lblcUser2";
            this.lblcUser2.Size = new System.Drawing.Size(46, 20);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(124, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 20);
            this.label7.Text = "经办人:";
            // 
            // lbliUser2
            // 
            this.lbliUser2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.lbliUser2.Location = new System.Drawing.Point(56, 202);
            this.lbliUser2.Name = "lbliUser2";
            this.lbliUser2.Size = new System.Drawing.Size(44, 20);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label9.Location = new System.Drawing.Point(2, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 20);
            this.label9.Text = "审核人:";
            // 
            // lbliUser3
            // 
            this.lbliUser3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.lbliUser3.Location = new System.Drawing.Point(170, 202);
            this.lbliUser3.Name = "lbliUser3";
            this.lbliUser3.Size = new System.Drawing.Size(46, 20);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label11.Location = new System.Drawing.Point(124, 202);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 20);
            this.label11.Text = "过磅人:";
            // 
            // lbliUser4
            // 
            this.lbliUser4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.lbliUser4.Location = new System.Drawing.Point(56, 222);
            this.lbliUser4.Name = "lbliUser4";
            this.lbliUser4.Size = new System.Drawing.Size(44, 20);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label13.Location = new System.Drawing.Point(2, 222);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 20);
            this.label13.Text = "监磅人:";
            // 
            // lbliUser
            // 
            this.lbliUser.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.lbliUser.Location = new System.Drawing.Point(170, 222);
            this.lbliUser.Name = "lbliUser";
            this.lbliUser.Size = new System.Drawing.Size(46, 20);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label15.Location = new System.Drawing.Point(124, 222);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 20);
            this.label15.Text = "开票人:";
            // 
            // txtBarCode
            // 
            this.txtBarCode.Enabled = false;
            this.txtBarCode.Location = new System.Drawing.Point(55, 26);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(130, 23);
            this.txtBarCode.TabIndex = 1;
            this.txtBarCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarCode_KeyPress);
            // 
            // txtCNO
            // 
            this.txtCNO.Location = new System.Drawing.Point(55, 2);
            this.txtCNO.Name = "txtCNO";
            this.txtCNO.Size = new System.Drawing.Size(180, 23);
            this.txtCNO.TabIndex = 0;
            this.txtCNO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCNO_KeyPress);
            // 
            // panelConfirm
            // 
            this.panelConfirm.Controls.Add(this.btnCancel);
            this.panelConfirm.Controls.Add(this.btnConfirm);
            this.panelConfirm.Controls.Add(this.txtConfirm);
            this.panelConfirm.Controls.Add(this.label3);
            this.panelConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConfirm.Location = new System.Drawing.Point(0, 0);
            this.panelConfirm.Name = "panelConfirm";
            this.panelConfirm.Size = new System.Drawing.Size(238, 298);
            this.panelConfirm.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(138, 155);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(65, 155);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(72, 32);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确认";
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(40, 108);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(171, 23);
            this.txtConfirm.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(40, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.Text = "确认人：";
            // 
            // OutVerifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 298);
            this.Controls.Add(this.panelConfirm);
            this.Controls.Add(this.lbliUser);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lbliUser4);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lbliUser3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbliUser2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblcUser2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblcUser);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.cbManually);
            this.Controls.Add(this.txtBarCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCNO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSecurity);
            this.Controls.Add(this.btnManually);
            this.Controls.Add(this.btnDepartment);
            this.Controls.Add(this.btnBack);
            this.Menu = this.mainMenu1;
            this.Name = "OutVerifyForm";
            this.Text = "出门验证";
            this.Load += new System.EventHandler(this.OutVerifyForm_Load);
            this.panelConfirm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid;
        private System.Windows.Forms.CheckBox cbManually;
        private MyTextBox txtBarCode;
        private System.Windows.Forms.Label label2;
        private MyTextBox txtCNO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnManually;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.Button btnDepartment;
        private System.Windows.Forms.Button btnSecurity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblcUser;
        private System.Windows.Forms.Label lblcUser2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbliUser2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbliUser3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbliUser4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbliUser;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panelConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private MyTextBox txtConfirm;
        private System.Windows.Forms.Label label3;
    }
}