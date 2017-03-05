using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CargoVerify
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //设置服务器URL地址
            BLL.Common.ServiceURL = RWConfig.Instance.GetValue("webservice");
            BLL.Common.ADMINUID = RWConfig.Instance.GetValue("adminuid"); 
            BLL.Common.ADMINPWD = RWConfig.Instance.GetValue("adminpwd"); 
            BLL.Common.SCHEME = RWConfig.Instance.GetValue("scheme");
            txtUserID.Text = RWConfig.Instance.GetValue("login", "username");

            pBoxLogo.Image = CargoVerify.Properties.Resources.logo;

            //用户名全选
            txtUserID.SelectAll();
        }

        /// <summary>
        /// 输入用户名按下回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            string userId = txtUserID.Text.Trim();
            if (!string.IsNullOrEmpty(userId) && e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
                txtPassword.SelectAll();
            }
        }

        /// <summary>
        /// 输入了密码按下回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果按下了回车，密码不是必须输入所以没有验证
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //获取界面数据
            string userId = txtUserID.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("请输入用户名！");
                return;
            }
            string errMsg = string.Empty;
            bool flag = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                flag = BLL.Common.Instance.Login(userId, password, out errMsg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            if (flag)
            {
                //MessageBox.Show("登录成功！");
                //记录用户名
                RWConfig.Instance.SetValue("login", "username", userId);

                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
                MessageBox.Show(errMsg);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}