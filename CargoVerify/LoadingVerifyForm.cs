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
    public partial class LoadingVerifyForm : Form
    {
        DataTable dtSource;
        public LoadingVerifyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadingVerifyForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 扫描提货单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCNO1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string cNO1 = txtCNO1.Text.Trim();
            if (!string.IsNullOrEmpty(cNO1) && e.KeyChar == (char)Keys.Enter)
            {
                string errMsg;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    dtSource = new BLL.LoadingVerify().GetOrderDetail(cNO1, out errMsg);
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
                    txtCNO1.Focus();
                    txtCNO1.SelectAll();
                    return;
                }

                //如果查询成功
                txtCNO1.Enabled = false;
                txtBarCode.Enabled = cbManually.Enabled = true;

                //listView.Columns.Add(new ColumnHeader());
                DataGridTableStyle dts = new DataGridTableStyle();
                DataGridCustomColumnBase dtbc;
                foreach (DataColumn dc in dtSource.Columns)
                {
                    //这几个字段不显示
                    if (dc.ColumnName.Equals("fID") || dc.ColumnName.Equals("sID") || dc.ColumnName.Equals("ikey1") || dc.ColumnName.Equals("type") || dc.ColumnName.Equals("cUser2") || dc.ColumnName.Equals("dDate2")) continue;
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

                //foreach (DataColumn dc in dt.Columns)
                //{
                //    //这两个字段不显示
                //    if (dc.ColumnName.Equals("ID")||dc.ColumnName.Equals("fID")||dc.ColumnName.Equals("sID")) continue;
                //    listView.Columns.Add(dc.Caption, dc.MaxLength, HorizontalAlignment.Center);
                //}
                //listView.Columns.Add("确认", 100, HorizontalAlignment.Center);

                //ListViewItem lvi;
                //foreach (DataRow row in dt.Rows)
                //{
                //    lvi = new ListViewItem();
                //    lvi.Tag = row["ID"];
                //    for (int j = 1; j < row.ItemArray.Length; j++)
                //    {
                //        lvi.SubItems.Add(row[j].ToString());
                //    }
                //    listView.Items.Add(lvi);
                //}
            }
        }

        /// <summary>
        /// 扫描条码验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            string barCode = txtBarCode.Text.Trim();
            if (!string.IsNullOrEmpty(barCode) && e.KeyChar == (char)Keys.Enter)
            {
                string errMsg;
                //第一步：判断该条码是否在该单据中
                bool flag = false;
                DataRow temprow = null;
                foreach (DataRow row in dtSource.Rows)
                {
                    if (row["cNO"].Equals(barCode))
                    {
                        flag = true;
                        temprow = row;
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

                if (temprow["ikey0"].Equals("已上车"))
                {
                    MessageBox.Show("该物资已经上车！");
                    txtBarCode.Focus();
                    txtBarCode.SelectAll();
                    return;
                }

                //第三步：验证该条码
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    flag = new BLL.LoadingVerify().Verify(temprow["ID"].ToString(), out errMsg);
                    if (!flag)
                        throw new Exception(errMsg);
                    //修改当前行的状态
                    temprow["ikey0"] = "已上车";
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
        /// 手动验证
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
                    MessageBox.Show("请选择要验证的行！");
                    return;
                }
                DataRow row = dtSource.Rows[index];
                //判断当前行是否已经上车
                bool flag = row["ikey0"].Equals("已上车");
                string ID = row["ID"].ToString();
                if (flag)
                {
                    MessageBox.Show("该物资已经上车！");
                    return;
                }

                string errMsg;
                //手动验证
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    flag = new BLL.LoadingVerify().Verify(ID, out errMsg);
                    if (!flag)
                        throw new Exception(errMsg);

                    row["ikey0"] = "已上车";
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 点击返回事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            //判断数据源中是否有数据：若没有直接退出，若有则判断是否所有都已经上车
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                bool flag = false;
                foreach (DataRow row in dtSource.Rows)
                {
                    //还有未上车的物资
                    if (!row["ikey0"].Equals("已上车"))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    DialogResult dr = MessageBox.Show("还有未上车的物资，你确定要退出吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                        return;
                }
            }
            this.Close();
        }
    }
}