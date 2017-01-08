$(document).ready(function () {
    document.getElementById("div_id").style.display = "";

    $("#dg").datagrid({
        url: "../ashx/logMonthCreate.ashx?i=cx" + Math.random(),
        toolbar: "#tb_lzd",
        striped: true,
        rownumbers: true,
        resizable: true,
        remoteSort: false,
        pagination: false,
        fitColumns: true,
        showFooter: true,
        singleSelect: true,
        pageSize: 20,
        pageList: [20, 30, 40, 50],
        queryParams: {
            type: "get"
        },
        columns: [[
                    { field: "Id", checkbox: false, hidden: true },
                    { field: 'YearMonth', title: '年月', width: 100, align: 'left' },
                    {
                        field: 'CreateTime', title: '创建时间', width: 100, align: 'left',
                        formatter: function (value, row, index) { if (value) { return ShortDatetime(value) } }
                    },
                    {
                        field: 'State', title: '状态', width: 100, align: 'left',
                        formatter: function (value, row, index) {
                            if (value == 0) {
                                return "执行中";
                            } else if (value == 1) { return "执行完毕"; }
                        }
                    }
        ]],
        loadFilter: function (data) {
            if (data)
                return data;
        }
    });

    $("#lbtn_add").bind('click', function () {
        if (confirm("确定要手动执行调整会员状态？")) {
            if (confirm("此操作不可恢复！你确定？")) {
                $.ajax({
                    datatype: "text",
                    url: "../ashx/logMonthCreate.ashx?i=" + Math.random(),
                    data: { type: "add" },
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
    });

});