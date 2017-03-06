$(document).ready(function () {
    document.getElementById("div_id").style.display = "";

    $("#txt_Year").combobox("setValue", getCurrentYear());
    $("#txt_Month").combobox("setValue", getCurrentMonth());

    $('#txt_AgentId').combobox({
        url: '../ashx/Base.ashx?type=getAgents',
        valueField: 'Id',
        textField: 'Name'
    });
    $('#txt_ProductsId').combobox({
        url: '../ashx/Base.ashx?type=getProducts',
        valueField: 'Id',
        textField: 'ProductName'
    });

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
            wherestr: $("#txt_SearchAgentsId").val() + "," + $("#txt_Year").combobox('getValue') + $("#txt_Month").combobox('getValue')
        },
        columns: [[
                    { field: 'Id', title: '编号', width: 100, align: 'left' },
                    { field: 'AgentId', title: '代理人编码', width: 100, align: 'left' },
                    { field: 'AgentName', title: '代理人姓名', width: 100, align: 'left' },
                    { field: 'YearMonth', title: '年月', width: 100, align: 'left' },
                    { field: 'Price', title: '价格', width: 100, align: 'left' },
                    {
                        field: 'CreateTime', title: '创建时间', width: 100, align: 'left',
                        formatter: function (value, row, index) { if (value) { return ShortDatetime(value) } }
                    },
                    { field: 'CreatePerson', title: '创建人', width: 100, align: 'left' },
                    {
                        field: 'State', title: '状态', width: 100, align: 'left',
                        formatter: function (value, row, index) {
                            if (value == 0) {
                                return "已作废";
                            } else if (value == 1) {
                                return "正常";
                            }
                        }
                    },

                    {
                        field: 'operatorb', title: '编辑', align: 'center', width: 70,
                        formatter: function (value, row, index) {
                            if (IsThisYearMonth(row.YearMonth) && row.State != 0) {
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
            wherestr: $("#txt_SearchAgentsId").val() + "," + $("#txt_Year").combobox('getValue') + $("#txt_Month").combobox('getValue')
        });
    });



    $("#btn_add").linkbutton({
        plain: true,
        text: "添加产品",
        iconCls: "icon-add",
        onClick: function () {
            if ($('#txt_ProductsId').combogrid("getValue") == "") {
                $.messager.alert("提醒", "请选择产品！", "info");
                return;
            }
            if ($('#txt_Num').numberbox("getValue") == "0") {
                $.messager.alert("提醒", "请填写数量！", "info");
                return;
            }
            if ($("#txt_Ids").val().indexOf($('#txt_ProductsId').combogrid("getValue") + ",") >= 0) {
                $.messager.alert("提醒", "该产品已添加，如需修改请删除后重新添加！", "info");
                return;
            }
            $.ajax({
                datatype: "text",
                url: "../ashx/ordersdetail.ashx?i=" + Math.random(),
                data: { type: "add", ProductId: $('#txt_ProductsId').combogrid("getValue"), Num: $('#txt_Num').numberbox("getValue"), OrdersId: $("#txt_Guid").val() },
                success: function (mess) {
                    if (mess.indexOf("$") >= 0) {
                        var va = mess.split('$');
                        $("#txt_Ids").val(va[0]);
                        $("#div_nums").val(va[1]);
                        $('#txt_AllPrice').numberbox('setValue', va[2]);
                    }
                    $('#data_xx').datagrid('load', {
                        type: "getDetailByOrdersId",
                    });
                }
            });

        }
    });

    $('#lbtn_add').bind('click', function () {
        clear();
        $("#txt_Guid").val(newGuid());
        $('#dlg_add').dialog('open').dialog('setTitle', '新增');

        $('#data_xx').datagrid({
            url: "../ashx/Base.ashx?orderId=" + $("#txt_Guid").val() + "&i=cx" + Math.random(),
            title: '产品信息',
            iconCls: 'icon-ok',
            width: 450,
            height: 160,
            striped: true,
            collapsible: true,
            singleSelect: true,
            rownumbers: true,
            queryParams: {
                type: "getDetailByOrdersId"
            },
            columns: [[
                { field: "Id", checkbox: false, hidden: true },
                              { field: 'ProductName', title: '产品名称', width: 180, align: 'center' },
                              { field: 'Num', title: '数量', width: 50, align: 'center' },
                              { field: 'UnitPrice', title: '单价', width: 50, align: 'center' },
                              { field: 'Price', title: '总金额', width: 50, align: 'center' },
                {
                    field: 'operator', title: '操作', align: 'center', width: 50,
                    formatter: function (value, row, index) {
                        return "<a href='javascript:void(0);' onclick='deleteDetail(" + index + ")' style='text-decoration: none;color: #800080;'>删除</a>";
                    }
                }
            ]]
        });
    });

    $("#lbtn_bd").linkbutton({
        plain: true,
        iconCls: "icon-add",
        onClick: function () {
            if ($('#txt_AgentId').combogrid("getValue") == "") {
                $.messager.alert("提醒", "请选择代理人！", "info");
                return;
            }
            if ($("#txt_Ids").val() == "") {
                $.messager.alert("提醒", "请至少选择一件产品！", "info");
                return;
            }
            $('#lbtn_bd').attr('disabled', "true");
            $.ajax({
                datatype: "text",
                url: "../ashx/orders.ashx?i=" + Math.random(),
                data: { type: "add", AgentId: $('#txt_AgentId').combogrid("getValue"), Price: $('#txt_AllPrice').numberbox("getValue"), OrdersId: $("#txt_Guid").val() },
                success: function (mess) {
                    if (mess == "0") {
                        $.messager.alert("提醒", "保存成功！", "info");
                        $('#dlg_add').dialog('close');
                        $("#lbtn_get").click();
                    } else {
                        alert(mess);
                    }
                }
            });
            $('#lbtn_bd').removeAttr("disabled");

        }
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
    if (confirm("确定要作废？")) {
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
}
function deleteDetail(index) {
    $("#data_xx").datagrid("selectRow", index);
    var row = $("#data_xx").datagrid("getSelected");
    if (row) {
        $.messager.confirm("确认对话框", "是否删除数据？", function (r) {
            if (r) {
                $.ajax({
                    datatype: "text",
                    url: "../ashx/ordersdetail.ashx?i=" + Math.random(),
                    data: { type: "delete", id: row.Id, OrdersId: $("#txt_Guid").val() },
                    success: function (mess) {
                        if (mess.indexOf("$") >= 0) {
                            var va = mess.split('$');
                            $("#txt_Ids").val(va[0]);
                            $("#div_nums").val(va[1]);
                            $('#txt_AllPrice').numberbox('setValue', va[2]);
                        }
                        $('#data_xx').datagrid('load', {
                            type: "getDetailByOrdersId",
                        });
                    }
                });
            }
        });
    }
}
function clear() {
    $("#txt_Guid").val("");
    $("#txt_Ids").val("");
    $('#txt_AllPrice').numberbox('setValue', 0);
    $('#txt_AgentId').combobox('setValue', '');
    $('#txt_ProductsId').combobox('setValue', '');
}
function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid;
}
