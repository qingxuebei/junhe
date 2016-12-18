<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="Web.details.orders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/orders.js"></script>
</head>
<body>
    <form id="form1" class="easyui-form">
        <div>
            <table style="width: 100%; border: 0">
                <tr>
                    <td>
                        <div id="tb_lzd" style="padding: 5px; height: auto">
                            <input class="easyui-textbox" id="txt_SearchAgentsId" data-options="prompt: '代理人编号'" style="width: 200px; height: 25px" />
                            <span></span>

                            <a href="#" class="easyui-linkbutton" id="lbtn_get" data-options="iconCls:'icon-search'">搜索</a>
                            <span></span>
                            <a href="#" class="easyui-linkbutton" id="lbtn_add" data-options="iconCls:'icon-add'">新增</a>
                            <span></span>
                        </div>
                        <div class="easyui-datagrid" id="dg"></div>
                    </td>
                </tr>

            </table>
        </div>
        <div id="dlg_detail" class="easyui-dialog" data-options="closed:true,modal:true" style="width: 600px; height: 300px;">
            <div class="easyui-datagrid" id="dg_detail"></div>
        </div>

        <div id="dlg_add" class="easyui-dialog" data-options="closed:true,modal:true" style="width: 600px; height: 500px;">

            <table>
                <tr>
                    <td style="height: 3px"></td>
                    <td style="height: 3px"></td>
                    <td style="height: 3px"></td>
                    <td style="height: 3px"></td>
                    <td style="height: 3px"></td>
                    <td style="height: 3px"></td>
                    <td style="height: 3px"></td>
                    <td style="height: 3px"></td>
                </tr>
                <tr>
                    <td style="width: 10px"></td>
                    <td style="text-align: right">代理人：</td>
                    <td>
                        <input id="txt_AgentId" name="txt_AgentId" value="" data-options="width:150,panelHeight:160" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 10px"></td>
                    <td><input type="hidden" id="txt_Ids" />
                        <input type="hidden" id="txt_Counts" />
                        <input type="hidden" id="txt_Guid" />
                    </td>
                </tr>
                <tr>

                    <td style="width: 10px"></td>
                    <td></td>
                    <td colspan="6">产品名称：<input id="txt_ProductsId" style="width: 240px;" />
                        数量：<input type="text" id="txt_Num" class="easyui-numberbox" data-options="width:30,min:1" value="1" />
                    </td>
                    <td><a id="btn_add" class="easyui-linkbutton"></a></td>
                </tr>

                <tr>

                    <td style="width: 10px"></td>
                    <td style="text-align: right"></td>
                    <td colspan="7">
                        <div class="easyui-DataGrid" id="data_xx"></div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 3px"></td>
                </tr>
                <tr>

                    <td style="width: 10px"></td>
                    <td style="text-align: right">总金额：</td>
                    <td colspan="7">
                        <input type="text" id="txt_AllPrice" class="easyui-numberbox" data-options="width:80,min:0,precision:2" value="0" readonly="true"/>元&nbsp;&nbsp;
                   <input class="easyui-validatebox" id="div_nums" style="width: 120px" readonly="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="height: 3px"></td>
                </tr>
                <tr>
                    <td style="width: 10px"></td>
                    <td colspan="8">
                        <a href="#" class="easyui-linkbutton" id="lbtn_bd">添加</a>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
