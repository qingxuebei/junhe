$(document).ready(function () {
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
        pageSize: 20,
        pageList: [20, 30, 40, 50],
        queryParams: {
            type: "get",
            wherestr: $("#txt_Id").val() + ","
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
                                return value == 0 ? "删除" : "正常";
                            }
                        }
                    },

                    {
                        field: 'operatorb', title: '编辑', align: 'center', width: 70,
                        formatter: function (value, row, index) {
                            return '<a href="javascript:void(0);" onclick="update(' + index + ')" style="text-decoration: none;color: #800080;">编辑</a>';
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
            wherestr: $("#txt_Id").val() + ","
        });
    });

    $("#lbtn_add").bind('click', function () {
        clear();
        $('#dlg').dialog('open').dialog('setTitle', '新增');
    });
});