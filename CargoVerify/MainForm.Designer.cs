namespace CargoVerify
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnLoadingVerify = new System.Windows.Forms.Button();
            this.btnOutVerify = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(24, 198);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(186, 52);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.Text = "用户:";
            // 
            // lblUserName
            // 
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.Red;
            this.lblUserName.Location = new System.Drawing.Point(56, 4);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(66, 20);
            this.lblUserName.Text = "label1";
            // 
            // btnLoadingVerify
            // 
            this.btnLoadingVerify.Location = new System.Drawing.Point(24, 51);
            this.btnLoadingVerify.Name = "btnLoadingVerify";
            this.btnLoadingVerify.Size = new System.Drawing.Size(186, 55);
            this.btnLoadingVerify.TabIndex = 1;
            this.btnLoadingVerify.Text = "装车验证";
            this.btnLoadingVerify.Click += new System.EventHandler(this.btnLoadingVerify_Click);
            // 
            // btnOutVerify
            // 
            this.btnOutVerify.Location = new System.Drawing.Point(24, 125);
            this.btnOutVerify.Name = "btnOutVerify";
            this.btnOutVerify.Size = new System.Drawing.Size(186, 52);
            this.btnOutVerify.TabIndex = 2;
            this.btnOutVerify.Text = "出门验证";
            this.btnOutVerify.Click += new System.EventHandler(this.btnOutVerify_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOutVerify);
            this.Controls.Add(this.btnLoadingVerify);
            this.Controls.Add(this.btnBack);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "主窗体";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnLoadingVerify;
        private System.Windows.Forms.Button btnOutVerify;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}

