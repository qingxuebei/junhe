<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hzorders.aspx.cs" Inherits="Web.details.hzorders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/hzorders.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div_id" style="display: none;height: auto; width: 100%;">
            <div class="easyui-tabs" id="tab_dg" style="height: auto; width: 99%;">
                <div title="产品销售汇总" style="height: auto; width: auto;" data-options="iconCls:'icon-standard-application-home'">
                    <div id="tb_ddjl" style="padding: 5px; height: auto">
                        <select id="txt_Year1" class="easyui-combobox" name="txt_Year1" style="width: 100px;">
                            <option value="2016">2016</option>
                            <option value="2017">2017</option>
                        </select>
                        <select id="txt_Month1" class="easyui-combobox" name="txt_Month1" style="width: 100px;">
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
                        <a href="#" class="easyui-linkbutton" id="lbtn_cpxs" data-options="iconCls:'icon-search'">汇总</a>
                    </div>

                    <div class="easyui-datagrid" id="dg_cpxs"></div>
                </div>
                <div title="月度订单汇总" data-options="iconCls:'icon-standard-application-home'">
                    <div id="tb_tcjl" style="padding: 5px; height: auto">
                        <select id="txt_Year2" class="easyui-combobox" name="txt_Year2" style="width: 100px;">
                            <option value="2016">2016</option>
                            <option value="2017">2017</option>
                        </select>
                        <select id="txt_Month2" class="easyui-combobox" name="txt_Month2" style="width: 100px;">
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
                        <a href="#" class="easyui-linkbutton" id="lbtn_ydjd" data-options="iconCls:'icon-search'">汇总</a>
                    </div>
                    <div class="easyui-datagrid" id="dg_ydjd"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
