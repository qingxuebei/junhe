<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agents.aspx.cs" Inherits="Web.details.agents" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/agents.js"></script>
</head>
<body>
    <form id="form1" class="easyui-form">
        <div id="div_id" style="display: none">
            <div>
                <table style="width: 100%; border: 0">
                    <tr>
                        <td>
                            <div id="tb_lzd" style="padding: 5px; height: auto">
                                <input class="easyui-textbox" id="txt_SearchId" data-options="prompt: '会员编号'" style="width: 100px; height: 25px" />
                                <span></span>
                                <input class="easyui-textbox" id="txt_SearchName" data-options="prompt: '姓名'" style="width: 100px; height: 25px" />
                                <span></span>
                                <select id="txt_type" class="easyui-combobox" name="txt_type" style="width: 100px;">
                                    <option value="">选择会员类别</option>
                                    <option value="0">代理人</option>
                                    <option value="1">代理商</option>
                                    <option value="2">合伙人</option>
                                </select>
                                <span></span>
                                <input class="easyui-textbox" id="txt_SearchPhone" data-options="prompt: '手机'" style="width: 100px; height: 25px" />
                                <span></span>
                                <select id="txt_State" class="easyui-combobox" name="txt_type" style="width: 100px;">
                                    <option value="">选择会员状态</option>
                                    <option value="-1">新添加</option>
                                    <option value="0">已过期</option>
                                    <option value="1">正常</option>
                                </select>
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

            <div id="dlg" class="easyui-dialog" data-options="closed:true,modal:true" style="width: 600px; height: 300px;">
                <table>
                    <tr>
                        <td style="height: 3px"></td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="height: 3px">
                            <input type="hidden" id="txt_Id" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">姓名：</td>
                        <td colspan="4">
                            <input type="text" class="easyui-validatebox" id="txt_Name" style="width: 150px" />
                        </td>
                        <td style="text-align: right">生日：</td>
                        <td colspan="4">
                            <input type="text" class="easyui-datebox" id="txt_Birthday" style="width: 150px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">省市：</td>
                        <td colspan="7">
                            <input id="txt_Province" value="" class="easyui-combobox" data-options="width:150,panelHeight:160" />
                            <input id="txt_City" class="easyui-combobox" value="" data-options="valueField:'RegionCode',textField:'RegionName',width:150,panelHeight:160" /></td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">加入日期：</td>
                        <td colspan="4">
                            <input class="easyui-datebox" id="txt_JoinDate" style="width: 150px" />
                        </td>
                        <td style="text-align: right">推荐代理人：</td>
                        <td colspan="4">
                            <input id="txt_RefereeId" name="txt_RefereeId" value="" data-options="width:150,panelHeight:160" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">个人账户开户行：</td>
                        <td colspan="4">
                            <input id="txt_AccountBank" name="txt_AccountBank" value="" data-options="width:150,panelHeight:160" />
                        </td>
                        <td style="text-align: right">个人账户支行：</td>
                        <td colspan="4">
                            <input class="easyui-validatebox" id="txt_AccountBankBranch" data-options="width:150" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">个人账号：</td>
                        <td colspan="4">
                            <input class="easyui-validatebox" id="txt_Account" data-options="width:150" />
                        </td>
                        <td style="text-align: right">手机：</td>
                        <td colspan="4">
                            <input type="text" class="easyui-validatebox" id="txt_Phone" style="width: 150px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">联系地址：</td>
                        <td colspan="7">
                            <input type="text" class="easyui-validatebox" id="txt_Address" style="width: 400px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">邮编：</td>
                        <td colspan="4">
                            <input type="text" class="easyui-validatebox" id="txt_ZipCode" style="width: 150px" />
                        </td>
                        <%-- <td style="text-align: right">状态：</td>
                    <td colspan="4">
                        <select class="easyui-combobox" id="txt_State" data-options="width:150,panelHeight:60">
                            <option value="1">启用</option>
                            <option value="0">禁用</option>
                        </select>
                    </td>--%>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
