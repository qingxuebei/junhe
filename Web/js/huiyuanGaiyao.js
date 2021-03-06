﻿$(document).ready(function () {
    document.getElementById("div_id").style.display = "";

    $("#txt_Year").combobox("setValue", getCurrentYear());
    $("#txt_Month").combobox("setValue", getCurrentMonth());

    $("#dg").datagrid({
        url: "../ashx/income.ashx?i=cx" + Math.random(),
        toolbar: "#tb_lzd",
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
            type: "get",
            yearMonth: $("#txt_Year").combobox('getValue') + $("#txt_Month").combobox('getValue'),
            u_type: $("#txt_type").combobox('getValue'),
            wherestr: $("#txt_SearchId").val() + "," + $("#txt_SearchName").val()
        },
        columns: [[
                    { field: "Id", checkbox: false, hidden: true },
                    { field: 'YearMonth', title: '月份', width: 100, align: 'left' },
                    { field: 'AgentId', title: '会员编号', width: 100, align: 'left' },
                    { field: 'AgentName', title: '会员姓名', width: 100, align: 'left' },
                    { field: 'CareerStatus', title: '事业状态', width: 100, align: 'left' },
                    { field: 'Rank', title: '职级', width: 100, align: 'left' },

                    { field: 'RefereeId', title: '推荐人编号', width: 100, align: 'left' },
                    { field: 'RefereeName', title: '推荐人姓名', width: 100, align: 'left' },
                    { field: 'AgencyId', title: '代理商编号', width: 100, align: 'left' },
                    { field: 'AgencyName', title: '代理商姓名', width: 100, align: 'left' }
        ]],
        loadFilter: function (data) {
            if (data)
                return data;
        }
    });

    $('#lbtn_get').bind('click', function () {
        $('#dg').datagrid('load', {
            type: "get",
            yearMonth: $("#txt_Year").combobox('getValue') + $("#txt_Month").combobox('getValue'),
            u_type: $("#txt_type").combobox('getValue'),
            wherestr: $("#txt_SearchId").val() + "," + $("#txt_SearchName").val()
        });
    });
    $('#lbtn_export').bind('click', function () {
        $.ajax({
            datatype: "text",
            url: "../ashx/income.ashx?i=" + Math.random(),
            data: {
                type: "export",
                style: "gy",
                yearMonth: $("#txt_Year").combobox('getValue') + $("#txt_Month").combobox('getValue'),
                u_type: $("#txt_type").combobox('getValue'),
                wherestr: $("#txt_SearchId").val() + "," + $("#txt_SearchName").val()
            },
            success: function (mess) {
                window.open(mess);
            }
        });
    });
})