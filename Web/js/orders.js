$(document).ready(function () {

    $("#dg").datagrid({
        url: "../ashx/orders.ashx?i=cx" + Math.random(),
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
            wherestr: $("#txt_SearchAgentsId").val() + ","
        },
        columns: [[
                    { field: 'Id', title: '编号', width: 100, align: 'left' },
                    { field: 'AgentId', title: '代理人编码', width: 100, align: 'left' },
                    { field: 'AgentName', title: '代理人姓名', width: 100, align: 'left' },
                    { field: 'YearMonth', title: '年月', width: 100, align: 'left' },
                    { field: 'Price', title: '价格', width: 100, align: 'left' },
                    { field: 'CreateTime', title: '创建时间', width: 100, align: 'left' },
                    { field: 'CreatePerson', title: '创建人', width: 100, align: 'left' },
                    {
                        field: 'State', title: '状态', width: 100, align: 'left',
                        formatter: function (value, row, index) {
                            if (value) {
                                return value == 0 ? "已作废" : "正常";
                            }
                        }
                    },

                    {
                        field: 'operatorb', title: '编辑', align: 'center', width: 70,
                        formatter: function (value, row, index) {
                            //if (IsThisYearMonth(row.YearMonth) && row.State != 0) {
                            if (row.State != 0) {
                                return '<a href="javascript:void(0);" onclick="update(' + index + ')" style="text-decoration: none;color: #800080;">作废</a>';
                            } else return "";
                        }
                    },
                    {
                        field: 'operatorb1', title: '产品详情', align: 'center', width: 70,
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
            wherestr: $("#txt_SearchAgentsId").val() + ","
        });
    });
});
function detail(index) {
    $('#dlg_detail').dialog('open').dialog('setTitle', '信息查看');
    $("#dg").datagrid("selectRow", index);
    var row = $("#dg").datagrid("getSelected");
    if (row) {
        $("#dg_detail").datagrid({
            url: "../ashx/Base.ashx?orderId=" + row.Id + "&i=cx" + Math.random(),
            //toolbar: "#tb_lzd",
            striped: true,
            rownumbers: true,
            resizable: true,
            remoteSort: false,
            pagination: false,
            fitColumns: true,
            showFooter: true,
            singleSelect: true,
            //pageSize: 20,
            //pageList: [20, 30, 40, 50],
            queryParams: {
                type: "getDetailByOrdersId"
            },
            columns: [[
                        { field: "Id", checkbox: false, hidden: true },
                        { field: 'ProductName', title: '产品名称', width: 200, align: 'left' },
                        { field: 'UnitPrice', title: '单价', width: 100, align: 'left' },
                        { field: 'Num', title: '数量', width: 100, align: 'left' },
                        { field: 'Price', title: '合计', width: 100, align: 'left' },

            ]],
            loadFilter: function (data) {
                if (data)
                    return data;
            }
        });
    }
}
function update(index) {
    $("#dg").datagrid("selectRow", index);
    var row = $("#dg").datagrid("getSelected");
    if (row) {
        $.ajax({
            datatype: "text",
            url: "../ashx/orders.ashx?type=update&id=" + row.Id + "&agentsId=" + row.AgentId + "&i=cx" + Math.random(),
            success: function (mess) {
                if (mess == "0") {
                    $.messager.alert("提醒", "保存成功！", "info");
                    $("#lbtn_get").click();
                } else {
                    alert(mess);
                }
            }
        });
    }
}

