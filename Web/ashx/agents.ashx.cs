using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// agents 的摘要说明
    /// </summary>
    public class agents : Base
    {
        public override String get(HttpContext context)
        {
            int pageRows, page; String order = "";
            pageRows = 20;
            page = 1;
            BLL.AgentsBLL agentsBLL = new BLL.AgentsBLL();
            String strWhere = " 1=1";
            string[] st = context.Request.Params["wherestr"].ToString().Split(',');
            if (!String.IsNullOrWhiteSpace(st[0]))
            {
                strWhere += " and Id='" + st[0] + "'";
            }
            if (null != context.Request["rows"])
            {
                pageRows = int.Parse(context.Request["rows"].ToString().Trim());
            }
            if (null != context.Request["page"])
            {
                page = int.Parse(context.Request["page"].ToString().Trim());
            }
            if (null != context.Request["sort"])
            {
                order = context.Request["sort"].ToString().Trim();
            }

            //调用分页的GetList方法  
            DataTable dt = agentsBLL.GetListByPage(strWhere.ToString(), order, (page - 1) * pageRows + 1, page * pageRows);
            int count = agentsBLL.GetRecordCount(strWhere.ToString());//获取条数  
            return MyData.Utils.EasyuiDataGridJson(dt, count);
        }
        public override String add(HttpContext context)
        {
            try
            {
                Model.Agents agents = new Model.Agents();
                agents.Id = MyData.Utils.getAgentsId();
                agents.Account = context.Request["Account"].ToString().Trim();
                agents.AccountBank = context.Request["AccountBank"].ToString().Trim();
                agents.AccountBankBranch = context.Request["AccountBankBranch"].ToString().Trim();
                agents.Address = context.Request["Address"].ToString().Trim();
                agents.Birthday = Convert.ToDateTime(context.Request["Birthday"].ToString().Trim());
                agents.Province = context.Request["Province"].ToString().Trim();
                agents.City = context.Request["City"].ToString().Trim();
                agents.Village = "";//此字段暂未使用
                agents.Name = context.Request["Name"].ToString().Trim();
                agents.Phone = context.Request["Phone"].ToString().Trim();
                agents.RefereeId = context.Request["RefereeId"].ToString().Trim();
                agents.State = Convert.ToInt32(MyData.AgentsState.新添加);
                agents.ZipCode = context.Request["ZipCode"].ToString().Trim();
                agents.JoinDate = Convert.ToDateTime(context.Request["JoinDate"].ToString().Trim());

                agents.CreatePerson = userName;
                agents.CreateTime = DateTime.Now;
                agents.UpdatePerson = userName;
                agents.UpdateTime = DateTime.Now;

                agents.CareerStatus = "A";//新用户进来事业状态为A
                agents.Rank = "S1";//新用户进来职级都为S1
                agents.AgentsStatus = Convert.ToInt32(MyData.AgentsStatus.代理人);//0代表代理人

                if (new BLL.AgentsBLL().Insert(agents))
                {
                    return "0";
                }
                else { return "添加失败！"; }
            }
            catch (Exception ex)
            {
                
            }
            return "添加失败！";
        }
        public override String update(HttpContext context)
        {
            try
            {
                Model.Agents agents = new Model.Agents();
                agents.Id = context.Request["Id"].ToString().Trim();
                agents.Account = context.Request["Account"].ToString().Trim();
                agents.AccountBank = context.Request["AccountBank"].ToString().Trim();
                agents.AccountBankBranch = context.Request["AccountBankBranch"].ToString().Trim();
                agents.Address = context.Request["Address"].ToString().Trim();
                agents.Birthday = Convert.ToDateTime(context.Request["Birthday"].ToString().Trim());
                agents.Province = context.Request["Province"].ToString().Trim();
                agents.City = context.Request["City"].ToString().Trim();
                agents.Name = context.Request["Name"].ToString().Trim();
                agents.Phone = context.Request["Phone"].ToString().Trim();
                agents.RefereeId = context.Request["RefereeId"].ToString().Trim();
                //agents.State = Convert.ToInt32(context.Request["State"].ToString().Trim());
                agents.ZipCode = context.Request["ZipCode"].ToString().Trim();
                agents.JoinDate = Convert.ToDateTime(context.Request["JoinDate"].ToString().Trim());

                agents.UpdatePerson = userName;
                agents.UpdateTime = DateTime.Now;

                if (new BLL.AgentsBLL().Update(agents))
                {
                    return "0";
                }
                else { return "修改失败！"; }
            }
            catch (Exception ex)
            {

            }
            return "修改失败！";
        }
        public override String del(HttpContext context)
        {
            return null;
        }
    }
}