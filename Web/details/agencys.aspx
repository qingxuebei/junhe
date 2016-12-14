<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agencys.aspx.cs" Inherits="Web.details.agencys" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../scripts/alljs.js"></script>
    <script src="../js/agencys.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" class="easyui-form">
        <div>
            <table style="width: 100%; border: 0">
                <tr>
                    <td>
                        <div id="tb_lzd" style="padding: 5px; height: auto">
                            <input class="easyui-textbox" id="txt_SearchId" data-options="prompt: '代理商编号'" style="width: 200px; height: 25px" />
                            <span></span>

                            <a href="#" class="easyui-linkbutton" id="lbtn_get" data-options="iconCls:'icon-search'">搜索</a>
                            <span></span>
                        </div>
                        <div class="easyui-datagrid" id="dg"></div>
                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
