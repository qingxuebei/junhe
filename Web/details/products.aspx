<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="Web.details.products" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/products.js"></script>
    
</head>
<body>
    <form id="form1" class="easyui-form">
        <div>
            <table style="width: 100%; border: 0">
                <tr>
                    <td>
                        <div id="tb_lzd" style="padding: 5px; height: auto">
                            <input class="easyui-textbox" id="txt_productsName" data-options="prompt: '产品名称'" style="width: 200px; height: 25px" />
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
        <div id="dlg_products" class="easyui-dialog" data-options="closed:true,modal:true" style="width: 500px; height: 200px;">
            <table>
                <tr>
                    <td style="height: 3px"></td>
                </tr>
                <tr>
                    <td style="width: 10px"></td>
                    <td style="text-align: right">产品名称：</td>
                    <td colspan="8">
                        <input type="text" class="easyui-validatebox" id="txt_ProductName" style="width: 300px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 10px"></td>
                    <td style="height: 3px">
                        <input type="hidden" id="txt_Id" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 10px"></td>
                    <td style="text-align: right">单价：</td>
                    <td colspan="4">
                        <input class="easyui-numberbox" id="txt_UnitPrice" style="width: 150px" data-options="min:0,precision:2,prefix:'￥'" />
                    </td>
                    <td style="text-align: right">单位：</td>
                    <td colspan="4">
                        <input class="easyui-validatebox" id="txt_Unit" style="width: 150px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 10px"></td>
                    <td style="text-align: right">状态：</td>
                    <td colspan="4">
                        <select class="easyui-combobox" id="txt_State" data-options="width:150,panelHeight:60">
                            <option value="1">启用</option>
                            <option value="0">禁用</option>
                        </select>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
