using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CargoVerify
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //获取用户信息
            lblUserName.Text = BLL.Common.Instance.UserInfo.name;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 装车验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadingVerify_Click(object sender, EventArgs e)
        {
            LoadingVerifyForm form = new LoadingVerifyForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 出门验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutVerify_Click(object sender, EventArgs e)
        {
            OutVerifyForm form = new OutVerifyForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 返回登录窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            //注销
            string errMsg;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                bool flag = BLL.Common.Instance.Logout(out errMsg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default ;
            }
            Application.Exit();
        }
    }
}