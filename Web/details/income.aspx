<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="income.aspx.cs" Inherits="Web.details.income" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/income.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%; border: 0">
                <tr>
                    <td>
                        <div id="tb_lzd" style="padding: 5px; height: auto">
                            <select id="txt_Year" class="easyui-combobox" name="txt_Year2" style="width: 100px;">
                                <option value="2016">2016</option>
                                <option value="2017">2017</option>
                            </select>
                            <select id="txt_Month" class="easyui-combobox" name="txt_Month2" style="width: 100px;">
                                <option value="01">1</option>
                                <option value="02">2</option>
                                <option value="03">3</option>
                                <option value="04">4</option>
                                <option value="05">5</option>
                                <option value="06">6</option>
                                <option value="07">7</option>
                                <option value="08">8</option>
                                <option value="09">9</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                            </select>
                            <span></span>
                            <select id="txt_type" class="easyui-combobox" name="txt_Year2" style="width: 100px;">
                                <option value="">全部</option>
                                <option value="0">代理人</option>
                                <option value="1">代理商</option>
                                <option value="2">合伙人</option>
                            </select>
                            <span></span>
                            <a href="#" class="easyui-linkbutton" id="lbtn_get" data-options="iconCls:'icon-search'">搜索</a>
                            <a href="#" class="easyui-linkbutton" id="lbtn_export" data-options="iconCls:'icon-excel'">导出</a>
                        </div>
                        <div class="easyui-datagrid" id="dg"></div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
