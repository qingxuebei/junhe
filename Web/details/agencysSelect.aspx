<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agencysSelect.aspx.cs" Inherits="Web.details.agencysSelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/agencysSelect.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tab_dg" style="height: auto; width: 100%;">
            <div id="tb_ddjl" style="padding: 5px; height: auto">
                <input class="easyui-textbox" id="txt_SearchAgentsId" data-options="prompt: '代理商编号'" style="width: 200px; height: 25px" />
                <span></span>
                <a href="#" class="easyui-linkbutton" id="lbtn_get" data-options="iconCls:'icon-search'">搜索</a>
            </div>
            <div id="cc" class="easyui-layout" style="width: 100%; height: 500px;">
                <div data-options="region:'east',title:'代理商详情',split:true" style="width: 20%;">
                    <table>
                        <tr>
                            <td>代理人编号:</td>
                            <td>
                                <label id="lbl_Id" />
                            </td>
                        </tr>
                        <tr>
                            <td>代理人姓名:</td>
                            <td>
                                <label id="lbl_Name" />
                            </td>
                        </tr>
                        <tr>
                            <td>事业状态:</td>
                            <td>
                                <label id="lbl_CareerStatus" />
                            </td>
                        </tr>
                        <tr>
                            <td>职级:</td>
                            <td>
                                <label id="lbl_Rank" />
                            </td>
                        </tr>
                        <tr>
                            <td>加入日期:</td>
                            <td>
                                <label id="lbl_JoinDate" />
                            </td>
                        </tr>
                        <tr>
                            <td>推荐人编号:</td>
                            <td>
                                <label id="lbl_RefereeId" />
                            </td>
                        </tr>
                        <tr>
                            <td>推荐人:</td>
                            <td>
                                <label id="lbl_RefereeName" />
                            </td>
                        </tr>
                        <tr>
                            <td>状态:</td>
                            <td>
                                <label id="lbl_State" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div data-options="region:'center',title:'市场概要'" style="padding: 5px; background: #eee; width: 80%">
                    <div class="easyui-datagrid" id="dg"></div>
                </div>
            </div>

        </div>
        <div id="dlg_order" class="easyui-dialog" data-options="closed:true,modal:true" style="width: 600px; height: 400px;">
            <div class="easyui-datagrid" id="dg_order"></div>
        </div>
    </form>
</body>
</html>
