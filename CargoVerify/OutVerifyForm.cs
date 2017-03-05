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
    public partial class OutVerifyForm : Form
    {
        DataTable dtSource;
        private int mainID;//主表ID
        private int iState = 0;//1|提货单;2|出门证;5|已确认;3|已出厂;4|已返厂
        private Dictionary<string, object> dic;
        public OutVerifyForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutVerifyForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 扫描出门证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            string cNO = txtCNO.Text.Trim();
            if (!string.IsNullOrEmpty(cNO) && e.KeyChar == (char)Keys.Enter)
            {
                string errMsg;                
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    dtSource = new BLL.OutVerify().GetOrderDetail(cNO,out dic , out errMsg);
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

                if (dtSource == null)
                {
                    MessageBox.Show(errMsg);
                    txtCNO.Focus();
                    txtCNO.SelectAll();
                    return;
                }

                mainID = Convert.ToInt32(dic["mainID"]);
                iState = Convert.ToInt32(dic["iState"]);
                lblcUser.Text = dic["cUser"].ToString();
                lblcUser2.Text = dic["cUser2"].ToString();
                lbliUser2.Text = dic["iUser2"].ToString();
                lbliUser3.Text = dic["iUser3"].ToString();
                lbliUser4.Text = dic["iUser4"].ToString();
                lbliUser.Text = dic["iUser"].ToString();

                //如果查询成功
                txtCNO.Enabled = false;
                txtBarCode.Enabled = cbManually.Enabled = true;

                //listView.Columns.Add(new ColumnHeader());
                DataGridTableStyle dts = new DataGridTableStyle();
                DataGridCustomColumnBase dtbc;
                foreach (DataColumn dc in dtSource.Columns)
                {
                    //这几个字段不显示
                    if (dc.ColumnName.Equals("fID") || dc.ColumnName.Equals("sID") || dc.ColumnName.Equals("ikey0") || dc.ColumnName.Equals("type") || dc.ColumnName.Equals("cUser1")||  dc.ColumnName.Equals("dDate1")) continue;
                    dtbc = new DataGridCustomColumnBase();
                    dtbc.HeaderText = dc.Caption;
                    dtbc.MappingName = dc.ColumnName;
                    dtbc.Owner = dataGrid;
                    //dtbc.Width = dc.MaxLength;
                    dts.GridColumnStyles.Add(dtbc);
                }

                dataGrid.TableStyles.Add(dts);

                dataGrid.DataSource = dtSource;

                //条码获取焦点
                txtBarCode.Focus();

                //判断是否已经扫描完成
                JudgeScanComplete();
            }
        }

        /// <summary>
        /// 扫描物资条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            string barCode = txtBarCode.Text.Trim();
            if (!string.IsNullOrEmpty(barCode) && e.KeyChar == (char)Keys.Enter)
            {
                string errMsg;
                DataRow temprow = null;//行对象
                //第一步：判断该条码是否在该单据中
                bool flag = false;
                foreach (DataRow row in dtSource.Rows)
                {
                    if (row["cNO"].Equals(barCode))
                    {
                        temprow = row;
                        flag = true;
                        break;
                    }
                }
                //如果不存在
                if (!flag)
                {
                    MessageBox.Show("对不起，此物资不在该单据中！");
                    txtBarCode.Focus();
                    txtBarCode.SelectAll();
                    return;
                }

                //第二步：判断该条码是否已经上车                
                if (temprow["ikey1"].Equals("已出门"))
                {
                    MessageBox.Show("该物资已出门！");
                    txtBarCode.Focus();
                    txtBarCode.SelectAll();
                    return;
                }

                //第三步：验证该条码
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    flag = new BLL.OutVerify().Verify(temprow["ID"].ToString(), out errMsg);
                    if (!flag)
                        throw new Exception(errMsg);
                    //修改当前行的状态
                    temprow["ikey1"] = "已出门";
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

                dataGrid.DataSource = dtSource;

                JudgeScanComplete();
                //成功后
                txtBarCode.Focus();
                txtBarCode.SelectAll();
            }
        }

        /// <summary>
        /// 验证方式改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbManually_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbManually.Checked)
            {
                txtBarCode.Enabled = false;
                btnManually.Enabled = true;
            }
            else
            {
                txtBarCode.Enabled = true;
                btnManually.Enabled = false;
                txtBarCode.Focus();
            }
        }

        /// <summary>
        /// 手动验证物资
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManually_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前选中行
                if (dtSource == null || dtSource.Rows.Count == 0)
                    return;
                int index = dataGrid.CurrentRowIndex;
                if (index < 0)
                {
                    MessageBox.Show("请选择要验证的物资！");
                    return;
                }
                DataRow row = dtSource.Rows[index];
                //判断当前行是否已经上车
                bool flag = row["ikey1"].Equals("已出门");
                string ID = row["ID"].ToString();
                if (flag)
                {
                    MessageBox.Show("该物资已出门！");
                    return;
                }

                string errMsg;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    flag = new BLL.OutVerify().Verify(ID, out errMsg);
                    if (!flag)
                        throw new Exception(errMsg);
                    //手动验证
                    dtSource.Rows[index]["ikey1"] = "已出门";
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
               
                dataGrid.DataSource = dtSource;

                JudgeScanComplete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 判断是否验证完成
        /// </summary>
        private void JudgeScanComplete()
        {
            bool flag = true;
            foreach (DataRow row in dtSource.Rows)
            {
                if (row["ikey1"].Equals("未出门"))
                {
                    flag = false;
                    break;
                }
            }

            if (flag)//如果验证完成
            {
                switch (iState)//判断状态
                {
                    case 2:
                        btnSecurity.Enabled = true;//
                        break;
                    case 5:
                        btnDepartment.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// 保卫部确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSecurity_Click(object sender, EventArgs e)
        {
            panelConfirm.Visible = true;//显示Panel
            panelConfirm.Tag = "Security";
            txtConfirm.Focus();
        }

        /// <summary>
        /// 物资部确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDepartment_Click(object sender, EventArgs e)
        {
            panelConfirm.Visible = true;//显示Panel
            panelConfirm.Tag = "Department";
            txtConfirm.Focus();
        }

        /// <summary>
        /// 点击返回按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            //判断数据源中是否有数据：若没有直接退出，若有则判断是否所有都已出门
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                bool flag = false;
                foreach (DataRow row in dtSource.Rows)
                {
                    //还有未出门的物资
                    if (!row["ikey1"].Equals("已出门"))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    DialogResult dr = MessageBox.Show("还有未出门的物资，你确定要退出吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                        return;
                }
            }
            this.Close();  
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //判断类型
            if (panelConfirm.Tag == null)
                return;
            //判断确认人是否为空
            string confirmName = txtConfirm.Text.Trim();
            if(string.IsNullOrEmpty(confirmName))
            {
                MessageBox.Show("请输入确认人！");
                txtConfirm.Focus();
                return;
            }

            bool flag = false;
            string errMsg;
            //判断是保卫部出门 还是物资部出门
            if(panelConfirm.Tag.Equals("Security"))
            {
                flag = new BLL.OutVerify().SecurityVerify(mainID.ToString(), confirmName, out errMsg);
                if (flag)
                {
                    btnSecurity.Enabled = false;
                    btnDepartment.Enabled = true;
                    btnCancel_Click(null, null);//隐藏panel
                }
                else
                {
                    MessageBox.Show("保卫部出门确认失败！"+errMsg );
                }
            }
            else if (panelConfirm.Tag.Equals("Department"))
            {
                flag = new BLL.OutVerify().DepartmentVerify(mainID.ToString(), confirmName, out errMsg);
                if (flag)
                {
                    MessageBox.Show("出门验证完成！");
                    btnDepartment.Enabled = false;
                    btnCancel_Click(null, null);//隐藏panel
                }
                else
                {
                    MessageBox.Show("物资部出门确认失败！" + errMsg );
                }
            }
        }

        /// <summary>
        /// 隐藏panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelConfirm.Visible = false;
            panelConfirm.Tag = null;
            txtConfirm.Text = string.Empty;
        }
    }
}