$(document).ready(function () {
    document.getElementById("div_id").style.display = "";

    $("#txt_Year1").combobox("setValue", getCurrentYear());
    $("#txt_Month1").combobox("setValue", getCurrentMonth());
    $("#txt_Year2").combobox("setValue", getCurrentYear());
    $("#txt_Month2").combobox("setValue", getCurrentMonth());

    $('#lbtn_cpxs').bind('click', function () {
        $("#dg_cpxs").datagrid({
            url: "../ashx/Base.ashx?i=cx" + Math.random(),
            striped: true,
            rownumbers: true,
            resizable: true,
            remoteSort: false,
            pagination: false,
            fitColumns: true,
            showFooter: true,
            singleSelect: true,
            queryParams: {
                type: "HuiZongOrdersDetail",
                yearMonth: $("#txt_Year1").combobox('getValue') + $("#txt_Month1").combobox('getValue')
            },
            columns: [[
                        { field: 'ProductName', title: '产品名称', width: 200, align: 'left' },
                        { field: 'UnitPrice', title: '单价', width: 100, align: 'left' },
                        { field: 'Num', title: '数量', width: 100, align: 'left' },
                        { field: 'Price', title: '总金额', width: 100, align: 'left' }
            ]],
            loadFilter: function (data) {
                if (data)
                    return data;
            }
        });
    });

    $('#lbtn_ydjd').bind('click', function () {
        $("#dg_ydjd").datagrid({
            url: "../ashx/Base.ashx?i=cx" + Math.random(),
            striped: true,
            rownumbers: true,
            resizable: true,
            remoteSort: false,
            pagination: false,
            fitColumns: true,
            showFooter: true,
            singleSelect: true,
            queryParams: {
                type: "HuiZongOrders",
                yearMonth: $("#txt_Year2").combobox('getValue') + $("#txt_Month2").combobox('getValue')
            },
            columns: [[
                        { field: 'AgentId', title: '代理人编码', width: 100, align: 'left' },
                        { field: 'AgentName', title: '代理人姓名', width: 100, align: 'left' },
                        { field: 'CareerStatus', title: '事业状态', width: 100, align: 'left' },
                        { field: 'Rank', title: '职级', width: 100, align: 'left' },
                        {
                            field: 'State', title: '状态', width: 100, align: 'left',
                            formatter: function (value, row, index) {
                                if (value == 0) {
                                    return "已删除";
                                } else if (value == 1) { return "正常"; } else if (value == -1) { return "新添加"; }
                            }
                        },
                        {
                            field: 'AgentsStatus', title: '代理人状态', width: 100, align: 'left',
                            formatter: function (value, row, index) {
                                if (value == 0) {
                                    return "代理人";
                                } else if (value == 1) { return "代理商"; } else if (value == 2) { return "合伙人"; }
                            }
                        },
                        { field: 'CountOrders', title: '订单数量', width: 100, align: 'left' },
                        { field: 'Price', title: '订单金额', width: 100, align: 'left' }
            ]],
            loadFilter: function (data) {
                if (data)
                    return data;
            }
        });
    });
})