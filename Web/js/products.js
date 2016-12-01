$(document).ready(function () {
    $("#dg").datagrid({
        url: "../ashx/products.ashx?i=cx" + Math.random(),
        toolbar: "#tb_lzd",
        striped: true,
        rownumbers: true,
        resizable: true,
        remoteSort: false,
        pagination: false,
        fitColumns: true,
        showFooter: true,
        //pageSize: 20,
        //pageList: [20, 30, 40, 50],
        queryParams: {
            type: "get",
            wherestr: $("#txt_productsName").val() + ","
        },
        columns: [[
                    { field: "Id", checkbox: false, hidden: true },
                    { field: 'ProductName', title: '产品名称', width: 200, align: 'left' },
                    { field: 'UnitPrice', title: '单价', width: 100, align: 'left' },
                    { field: 'Unit', title: '单位', width: 100, align: 'left' },
                    { field: 'CreatePerson', title: '创建人', width: 100, align: 'left' },
                    {
                        field: 'CreateTime', title: '创建时间', width: 100, align: 'left',
                        formatter: function (value, row, index) {
                            if (value) {
                                return value.toString().substring(0, 10);
                            }
                        }
                    },
                    {
                        field: 'State', title: '状态', width: 100, align: 'left',
                        formatter: function (value, row, index) {
                            if (value) {
                                return value = 1 ? "启用" : "禁用";
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
            wherestr: $("#txt_productsName").val() + ","
        });
    });

    $("#lbtn_add").bind('click', function () {
        clear();
        $('#dlg_products').dialog('open').dialog('setTitle', '新增');
    });

    $('#dlg_products').dialog({
        buttons: [{
            id: "btn_mbtj",
            text: '保存',
            iconCls: "icon-ok",//{ mbmc: $("#txt_mb").val()}
            handler: function () {

                if ($("#txt_ProductName").val() == "") {
                    alert('请填写产品名称！');
                    return;
                } if ($("#txt_Unit").val() == "") {
                    alert('请填写单位！');
                    return;
                }
                loadMbData();
                $.ajax({
                    datatype: "text",
                    url: "../ashx/products.ashx?i=" + Math.random(),
                    data: csdata,
                    success: function (mess) {
                        if (mess == "0") {
                            $.messager.alert("提醒", "保存成功！", "info");
                            $('#dlg_products').dialog('close');
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
                $('#dlg_products').dialog('close');
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
        ProductName: $("#txt_ProductName").val(),
        Unit: $("#txt_Unit").val(),
        UnitPrice: $('#txt_UnitPrice').numberbox('getValue'),
        State: $('#txt_State').combobox('getValue')
    }
}

function clear() {
    $("#txt_Id").val("");
    $("#txt_ProductName").val("");
    $("#txt_Unit").val("");
    $('#txt_UnitPrice').numberbox('setValue', 0);
}

function update(index) {
    $('#dlg_products').dialog('open').dialog('setTitle', '信息查看');
    $("#dg").datagrid("selectRow", index);
    var row = $("#dg").datagrid("getSelected");
    if (row) {
        $("#txt_Id").val(row.Id);
        $("#txt_ProductName").val(row.ProductName);
        $("#txt_Unit").val(row.Unit);
        $('#txt_UnitPrice').numberbox('setValue', row.UnitPrice);
        $("#txt_State").select(row.State);
    }
}