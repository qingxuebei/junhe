$(document).ready(function () {

    $('#txt_AccountBank').combobox({
        url: '../ashx/Base.ashx?type=getBank',
        valueField: 'Id',
        textField: 'Name'
    });
    $('#txt_RefereeId').combobox({
        url: '../ashx/Base.ashx?type=getAgents',
        valueField: 'Id',
        textField: 'Name'
    });
    $('#txt_Province').combobox({
        url: '../ashx/Base.ashx?type=getRegion&st=p',
        valueField: 'RegionCode',
        textField: 'RegionName',
        onSelect: function (rec) {
            var url = '../ashx/Base.ashx?type=getRegion&st=c&id=' + rec.RegionCode;
            $('#txt_City').combobox('setValue', "");
            $('#txt_City').combobox('reload', url);
        }
    });
    $('#txt_City').combobox({
        url: '../ashx/Base.ashx?type=getRegion&st=reload',
        valueField: 'RegionCode',
        textField: 'RegionName',
    });

    $("#dg").datagrid({
        url: "../ashx/agents.ashx?i=cx" + Math.random(),
        toolbar: "#tb_lzd",
        striped: true,
        rownumbers: true,
        resizable: true,
        remoteSort: false,
        pagination: true,
        fitColumns: true,
        showFooter: true,
        singleSelect: true,
        pageSize: 20,
        pageList: [20, 30, 40, 50],
        queryParams: {
            type: "get",
            wherestr: $("#txt_SearchId").val() + ","
        },
        columns: [[
                    { field: 'Id', title: '编号', width: 100, align: 'left' },
                    { field: 'Name', title: '姓名', width: 100, align: 'left' },
                    {
                        field: 'Birthday', title: '生日', width: 100, align: 'left',
                        formatter: function (value, row, index) {
                            if (value) {
                                return value.toString().substring(0, 10);
                            }
                        }
                    },
                    { field: 'CareerStatus', title: '事业状态', width: 100, align: 'left' },
                    {
                        field: 'JoinDate', title: '加入日期', width: 100, align: 'left',
                        formatter: function (value, row, index) {
                            if (value) {
                                return value.toString().substring(0, 10);
                            }
                        }
                    },
                    { field: 'Rank', title: '职级', width: 100, align: 'left' },
                    { field: 'RefereeId', title: '推荐人编号', width: 100, align: 'left' },
                    { field: 'RefereeName', title: '推荐人', width: 100, align: 'left' },
                    { field: 'AgencyId', title: '代理商编号', width: 100, align: 'left' },
                    { field: 'AgencyName', title: '代理商', width: 100, align: 'left' },
                    { field: 'Province', title: '省', hidden: true },
                    { field: 'City', title: '市', hidden: true },
                    { field: 'Village', title: '区', hidden: true },
                    { field: 'AccountBank', title: '个人账户开户行', hidden: true },
                    { field: 'AccountBankBranch', title: '个人账户支行', hidden: true },
                    { field: 'Account', title: '个人账户', hidden: true },
                    { field: 'Address', title: '联系地址', hidden: true },
                    { field: 'ZipCode', title: '邮编', hidden: true },
                    { field: 'Phone', title: '手机', width: 100, align: 'left' },
                    {
                        field: 'State', title: '状态', width: 100, align: 'left',
                        formatter: function (value, row, index) {
                            if (value) {
                                return value == 0 ? "已删除" : "正常";
                            }
                        }
                    },

                    {
                        field: 'operatorb', title: '编辑', align: 'center', width: 70,
                        formatter: function (value, row, index) {
                            return '<a href="javascript:void(0);" onclick="update(' + index + ')" style="text-decoration: none;color: #800080;">编辑</a>';
                        }
                    },
                    {
                        field: 'operatorb1', title: '详情', align: 'center', width: 70,
                        formatter: function (value, row, index) {
                            return '<a href="javascript:void(0);" onclick="detail(' + index + ')" style="text-decoration: none;color: #800080;">详情</a>';
                        }
                    }
        ]],
        loadFilter: function (data) {
            if (data)
                return data;
        }
    });

    $('#lbtn_get').bind('click', function () {
        $('#dg').datagrid('load', {
            type: "get",
            wherestr: $("#txt_SearchId").val() + ","
        });
    });

    $("#lbtn_add").bind('click', function () {
        clear();
        $('#dlg').dialog('open').dialog('setTitle', '新增');

    });
    $('#dlg').dialog({
        buttons: [{
            id: "btn_mbtj",
            text: '保存',
            iconCls: "icon-ok",//{ mbmc: $("#txt_mb").val()}
            handler: function () {

                if ($("#txt_Name").val() == "") {
                    alert('请填写姓名！');
                    return;
                }
                if ($('#txt_Birthday').datebox('getValue') == "") {
                    alert("请填写生日！")
                    return;
                }
                if ($('#txt_Province').combobox('getValue') == "" || $('#txt_City').combobox('getValue') == "") {
                    alert("请选择省市！")
                    return;
                }
                if ($('#txt_JoinDate').datebox('getValue') == "") {
                    alert("请填写加入日期！")
                    return;
                }
                if ($('#txt_RefereeId').combobox('getValue') == "") {
                    alert('请选择推荐人！');
                    return;
                }
                loadMbData();
                $.ajax({
                    datatype: "text",
                    url: "../ashx/agents.ashx?i=" + Math.random(),
                    data: csdata,
                    success: function (mess) {
                        if (mess == "0") {
                            $.messager.alert("提醒", "保存成功！", "info");
                            $('#dlg').dialog('close');
                            $("#lbtn_get").click();
                        } else {
                            alert(mess);
                        }
                    }
                });

            }
        }, {
            text: '关闭',
            iconCls: "icon-cancel",
            handler: function () {
                $('#dlg').dialog('close');
            }
        }]
    });
});
function loadMbData() {
    var type = "add";
    if ($("#txt_Id").val() != "") {
        type = "update";
    }
    csdata = {
        type: type,
        Id: $("#txt_Id").val(),
        Name: $("#txt_Name").val(),
        Birthday: $('#txt_Birthday').datebox('getValue'),
        Province: $('#txt_Province').combobox('getValue'),
        City: $('#txt_City').combobox('getValue'),
        JoinDate: $('#txt_JoinDate').datebox('getValue'),
        RefereeId: $('#txt_RefereeId').combobox('getValue'),
        AccountBank: $('#txt_AccountBank').combobox('getValue'),
        AccountBankBranch: $("#txt_AccountBankBranch").val(),
        Account: $("#txt_Account").val(),
        Phone: $("#txt_Phone").val(),
        Address: $("#txt_Address").val(),
        ZipCode: $("#txt_ZipCode").val(),
        //State: $('#txt_State').combobox('getValue')
    }
}

function clear() {
    $("#txt_Id").val("");
    $("#txt_Name").val("");
    $("#txt_Birthday").val("");
    $('#txt_Province').combobox('setValue', "");
    $('#txt_City').combobox('setValue', "");

    $("#txt_JoinDate").val("");
    $("#txt_RefereeId").val("");
    $("#txt_AccountBank").val("");
    $('#txt_AccountBankBranch').val("");
    $('#txt_Account').val("");

    $("#txt_Phone").val("");
    $("#txt_Address").val("");
    $("#txt_ZipCode").val("");

}
function update(index) {
    $('#dlg').dialog('open').dialog('setTitle', '信息查看');
    $("#dg").datagrid("selectRow", index);
    var row = $("#dg").datagrid("getSelected");
    if (row) {
        $("#txt_Id").val(row.Id);

        $("#txt_Name").val(row.Name);
        $("#txt_Birthday").datebox('setValue', row.Birthday);
        $('#txt_Province').combobox('setValue', row.Province);
        $('#txt_City').combobox('setValue', row.City);

        $("#txt_JoinDate").datebox('setValue', row.JoinDate);
        $("#txt_RefereeId").combobox('setValue', row.RefereeId);
        $("#txt_AccountBank").combobox('setValue', row.AccountBank);
        $('#txt_AccountBankBranch').val(row.AccountBankBranch);
        $('#txt_Account').val(row.Account);

        $("#txt_Phone").val(row.Phone);
        $("#txt_Address").val(row.Address);
        $("#txt_ZipCode").val(row.ZipCode);
    }
}
function detail(index) {
    $()
}