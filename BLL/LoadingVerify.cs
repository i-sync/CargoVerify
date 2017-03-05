using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BLL.Service;

namespace BLL
{
    public class LoadingVerify
    {
        /// <summary>
        /// 根据提货单号查询物资明细
        /// </summary>
        /// <param name="cNO1"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetOrderDetail(string cNO1,out string errMsg)
        {
            errMsg = string.Empty;
            queryOption queryOption = new queryOption { batchNo = 1, batchSize = 1000 };
            //第一步：根据编号查询主表ID
            //queryResult queryResult = Common.Instance.query("ERP_MM_Order0", null, string.Format("cNO1='{0}'", cNO1), queryOption); //string.Format("cNO1='{0}'", cNO1)
            queryResult queryResult = Soap.Instance.query("ERP_MM_Order0", null, string.Format("cNO1='{0}'", cNO1), queryOption, out errMsg);
            int ID = 0;
            if (queryResult.result > 0)
            {
                if (queryResult.records == null)
                {
                    errMsg = "没有查询到数据！";
                    return null;
                }
                //ID = Convert.ToInt32(queryResult.records[0].values[0]);
                //把返回的数据，放在表中，为了使用字段名称来获取数据
                DataTable dt = new DataTable();
                //遍历列名
                DataColumn dc;
                foreach (colInfo col in queryResult.metaData.colInfo)
                {
                    dc = new DataColumn(col.name);
                    //dc.Caption = col.label;
                    dt.Columns.Add(dc);
                }
                DataRow row = dt.NewRow();
                for (int i = 0; i < queryResult.records[0].size; i++)
                {
                    row[i] = queryResult.records[0].values[i];
                }
                dt.Rows.Add(row);
                ID = Convert.ToInt32(dt.Rows[0]["ID"]);
            }
            else
            {
                errMsg = "查询主表出错："+queryResult.message;
                return null;
            }
            //第二步：根据主表ID查询子表明细
            queryResult = Common.Instance.query("ERP_MM_Order0_tDetail", null, string.Format("fID={0}", ID), queryOption);
            //queryResult = Soap.Instance.query("ERP_MM_Order0_tDetail", null, string.Format("fID={0}", ID), queryOption,out errMsg);
            if (queryResult.result > 0)
            { 
                //第三步：封装数据结果
                DataTable dt = new DataTable();
                //遍历列名
                DataColumn dc;
                foreach (colInfo col in queryResult.metaData.colInfo)
                {
                    dc = new DataColumn(col.name);
                    dc.Caption = col.label;
                    dt.Columns.Add(dc);
                }
                //类型标识0：装车，1：出门
                dc = new DataColumn("type");
                dt.Columns.Add(dc);

                //判断是否有数据
                if (queryResult.records == null)
                {
                    errMsg = "子表没有数据!";
                    return null;
                }

                DataRow row;
                //遍历数据
                foreach (lbRecord lbR in queryResult.records)
                {
                    row = dt.NewRow();
                    for (int i = 0; i < lbR.size; i++)
                    {
                        row[i] = lbR.values[i];
                    }
                    row["ikey0"] = row["ikey0"].Equals("0") ? "已上车" : "未上车";
                    row["type"] = 0;
                    dt.Rows.Add(row);
                }

                //返回数据
                return dt;
            }
            errMsg = "查询子表出错：" + queryResult.message;
            return null;
        }

        /// <summary>
        /// 装车验证
        /// </summary>
        /// <param name="ID">明细ID</param>
        /// <param name="errMsg">错误人yth</param>
        /// <returns></returns>
        public bool Verify( string ID, out string errMsg)
        {
            errMsg = string.Empty;

            //传递参数：操作员及当前操作时间
            lbParameter[] parms =
            {
                new lbParameter(){name ="cUser1",value=BLL.Common.Instance.UserInfo.name},
                new lbParameter(){name="dDate1",value= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
            };
            bizProcessResult bpResult = Common.Instance.execBizProcess("ERP_MM_Order0_tDetail_M8", ID, parms, null);
            //bizProcessResult bpResult = Soap.Instance.execBizProcess("ERP_MM_Order0_tDetail_M8", ID, parms, null,out errMsg);
            if (bpResult.result <= 0)
            {
                errMsg = bpResult.message;
                return false;
            }
            return true;
        }
    }
}
