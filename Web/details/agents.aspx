<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agents.aspx.cs" Inherits="Web.details.agents" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/agents.js"></script>
</head>
<body>
   <form id="form1" class="easyui-form">
        <div>
            <table style="width: 100%; border: 0">
                <tr>
                    <td>
                        <div id="tb_lzd" style="padding: 5px; height: auto">
                            <input class="easyui-textbox" id="txt_Id" data-options="prompt: '代理人编号'" style="width: 200px; height: 25px" />
                            <span></span>

                            <a href="#" class="easyui-linkbutton" id="lbtn_get" data-options="iconCls:'icon-search'">搜索</a>
                            <span></span>
                            <a href="#" class="easyui-linkbutton" id="lbtn_add" data-options="iconCls:'icon-add'">新增</a>
                            <span></span>
                            <%--<a href="#" class="easyui-linkbutton" id="lbtn_del" data-options="iconCls:'icon-remove'">删除</a>--%>
                        </div>
                        <div class="easyui-datagrid" id="dg"></div>
                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
