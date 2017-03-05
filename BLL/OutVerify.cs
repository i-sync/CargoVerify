using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BLL.Service;
using System.Data;

namespace BLL
{
    public class OutVerify
    {
        /// <summary>
        /// 根据出门证查询物资明细
        /// </summary>
        /// <param name="cNO1"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetOrderDetail(string cNO, out Dictionary<string,object> dic, out string errMsg)
        {
            errMsg = string.Empty;
            int mainID, iState = 0;//单据状态（1|提货单;2|出门证;5|已确认;3|已出厂;4|已返厂）
            dic = new Dictionary<string, object>();

            //修改，添加    提货人：cUser/经办人：cUser2/审核领导：iUser2/开票人：iUser/过磅人：iUser3/监磅人：iUser4  
            string cUser, cUser2, iUser, iUser2, iUser3, iUser4;
            queryOption queryOption = new queryOption { batchNo = 1, batchSize = 1000 };
            //第一步：根据编号查询主表ID
            queryResult queryResult = Common.Instance.query("ERP_MM_Order0", null, string.Format("cNO='{0}'", cNO), queryOption);
            //queryResult queryResult = Soap.Instance.query("ERP_MM_Order0", null, string.Format("cNO='{0}'", cNO), queryOption, out errMsg);

            if (queryResult.result > 0)
            {
                if (queryResult.records == null)
                {
                    errMsg = "没有查询到数据！";
                    return null;
                }
                //把返回的数据，放在表中，为了使用字段名称来获取数据(如何按字段位置获取，如果字段位置改变会出现问题)
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
                mainID = Convert.ToInt32(dt.Rows[0]["ID"]);
                iState = Convert.ToInt32(dt.Rows[0]["iState"]);
                //提货人/经办人/审核领导....
                cUser = dt.Rows[0]["cUser"].ToString();
                cUser2 = dt.Rows[0]["cUser2"].ToString();
                iUser = dt.Rows[0]["iUser"].ToString();
                iUser2 = dt.Rows[0]["iUser2"].ToString();
                iUser3 = dt.Rows[0]["iUser3"].ToString();
                iUser4 = dt.Rows[0]["iUser4"].ToString();

                dic.Add("mainID", mainID);
                dic.Add("iState", iState);
                dic.Add("cUser", cUser);
                dic.Add("cUser2", cUser2);
                dic.Add("iUser", iUser);
                dic.Add("iUser2", iUser2);
                dic.Add("iUser3", iUser3);
                dic.Add("iUser4", iUser4);

                if (iState == 3)//已出厂
                {
                    errMsg = "单据已出厂！";
                    return null;
                }
            }
            else
            {
                errMsg = "查询主表出错：" + queryResult.message;
                return null;
            }
            //第二步：根据主表ID查询子表明细
            queryResult = Common.Instance.query("ERP_MM_Order0_tDetail", null, string.Format("fID={0}", mainID), queryOption);
            //queryResult = Soap.Instance.query("ERP_MM_Order0_tDetail", null, string.Format("fID={0}", mainID), queryOption, out errMsg);
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
                    row["ikey1"] = row["ikey1"].Equals("0") ? "已出门" : "未出门";
                    row["type"] = 1;
                   
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
        public bool Verify(string ID, out string errMsg)
        {
            errMsg = string.Empty;
            //传递参数：操作员及当前操作时间
            lbParameter[] parms =
            {
                new lbParameter(){name ="cUser2",value=BLL.Common.Instance.UserInfo.name},
                new lbParameter(){name="dDate2",value= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
            };
            bizProcessResult bpResult = Common.Instance.execBizProcess("ERP_MM_Order0_tDetail_M9", ID, parms, null);
            //bizProcessResult bpResult = Soap.Instance.execBizProcess("ERP_MM_Order0_tDetail_M9", ID, parms, null, out errMsg);
            if (bpResult.result <= 0)
            {
                errMsg = bpResult.message;
                return false; ;
            }
            return true;
        }

        /// <summary>
        /// 保卫部出门确认
        /// </summary>
        /// <param name="ID">主ID</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public bool SecurityVerify(string ID, string Name, out string errMsg)
        {
            errMsg = string.Empty;
            //传递参数：操作员及当前操作时间
            lbParameter[] parms =
            {
                new lbParameter(){name ="iUser6",value=Name },
                new lbParameter(){name="dDate6",value= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
            };
            bizProcessResult bpResult = Common.Instance.execBizProcess("ERP_MM_Order0_M7", ID, parms, null);
            //bizProcessResult bpResult = Soap.Instance.execBizProcess("ERP_MM_Order0_M7", ID, parms, null, out errMsg);
            if (bpResult.result <= 0)
            {
                errMsg = bpResult.message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 物资部出门确认
        /// </summary>
        /// <param name="ID">主ID</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public bool DepartmentVerify(string ID, string Name, out string errMsg)
        {
            errMsg = string.Empty;
            //传递参数：操作员及当前操作时间
            lbParameter[] parms =
            {
                new lbParameter(){name ="iUser7",value=Name },
                new lbParameter(){name="dDate7",value= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
            };
            bizProcessResult bpResult = Common.Instance.execBizProcess("ERP_MM_Order0_M8", ID, parms, null);
            //bizProcessResult bpResult = Soap.Instance.execBizProcess("ERP_MM_Order0_M8", ID, parms, null, out errMsg);
            if (bpResult.result <= 0)
            {
                errMsg = bpResult.message;
                return false;
            }
            return true;
        }
    }
}
