$(document).ready(function () {
    $('#lbtn_get').bind('click', function () {

        if ($("#txt_SearchAgentsId").val() == "") {
            alert('请填写代理人编号！');
            return;
        }

        $("#dg").datagrid({
            url: "../ashx/agentsSelect.ashx?i=cx" + Math.random(),
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
                agentId: $("#txt_SearchAgentsId").val(),
                style: "getList"
            },
            columns: [[
                        { field: 'Id', title: '编号', width: 100, align: 'left' },
                        { field: 'AgentsName', title: '姓名', width: 100, align: 'left' },
                        {
                            field: 'Birthday', title: '生日', width: 100, align: 'left',
                            formatter: function (value, row, index) { if (value) { return ShortDatetime(value) } }
                        },
                        { field: 'CareerStatus', title: '事业状态', width: 100, align: 'left' },
                        {
                            field: 'JoinDate', title: '加入日期', width: 100, align: 'left',
                            formatter: function (value, row, index) { if (value) { return ShortDatetime(value) } }
                        },
                        { field: 'Rank', title: '职级', width: 100, align: 'left' },
                        { field: 'RefereeId', title: '推荐人编号', width: 100, align: 'left' },
                        { field: 'RefereeName', title: '推荐人', width: 100, align: 'left' },
                        { field: 'Phone', title: '手机', width: 100, align: 'left' },
                        {
                            field: 'State', title: '状态', width: 100, align: 'left',
                            formatter: function (value, row, index) {
                                if (value == -1) {
                                    return "新添加";
                                } else if (value == 0) {
                                    return "已过期";
                                } else if (value == 1) {
                                    return "正常";
                                }
                            }
                        }
            ]],
            loadFilter: function (data) {
                if (data)
                    return data;
            }
        });


        $.ajax({
            datatype: "text",
            url: "../ashx/agentsSelect.ashx?i=" + Math.random(),
            data: {
                type: "get",
                agentId: $("#txt_SearchAgentsId").val(),
                style: "getSelf"
            },
            success: function (mess) {
                if (mess) {
                    var data = eval('(' + mess + ')');
                    var row = data.rows[0];
                    $("#lbl_Id").text(row.Id);
                    $("#lbl_Name").text(row.AgentsName);
                    $("#lbl_CareerStatus").text(row.CareerStatus);
                    $("#lbl_Rank").text(row.Rank);
                    $("#lbl_JoinDate").text(ShortDatetime(row.JoinDate));
                    $("#lbl_RefereeId").text(row.RefereeId);
                    $("#lbl_RefereeName").text(row.RefereeName);
                    $("#lbl_State").text(row.State == -1 ? "新添加" : (row.State == 0 ? "已过期" : "正常"));
                }
            }
        });
    });
});
