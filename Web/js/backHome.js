
$(document).ready(function () {

    $.ajax({
        datatype: "text",
        url: "../ashx/Base.ashx?i=" + Math.random(),
        data: {
            type: "sumPrice"
        },
        success: function (datastr) {
            console.log(datastr);
            var data = eval(datastr);
            if (data.length > 0) {
                $("#dt_dayPrice").html(StringIsNull(data[0], "0.00"));
                $("#dt_monthPrice").html(StringIsNull(data[1], "0.00"));
            }
        }
    });

    $.ajax({
        datatype: "text",
        url: "../ashx/Base.ashx?i=" + Math.random(),
        data: {
            type: "sumPerson"
        },
        success: function (datastr) {
            console.log(datastr);
            var data = eval(datastr);
            if (data.length > 0) {
                $("#dt_dayPerson").html(data[0]);
                $("#dt_monthPerson").html(data[1]);
            }
        }
    });
});