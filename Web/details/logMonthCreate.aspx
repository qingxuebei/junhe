<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logMonthCreate.aspx.cs" Inherits="Web.details.logMonthCreate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/logMonthCreate.js"></script>

</head>
<body>
    <form id="form1" class="easyui-form">
        <div id="div_id" style="display: none">
            <div>
                <table style="width: 80%; border: 0">
                    <tr>
                        <td>
                            <div id="tb_lzd" style="padding: 5px; height: auto">
                                <a href="#" class="easyui-linkbutton" id="lbtn_add" data-options="iconCls:'icon-add'">手动执行调整会员状态（根据上月订单）</a>
                                <span></span>
                            </div>
                            <div class="easyui-datagrid" id="dg"></div>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </form>
</body>
</html>
