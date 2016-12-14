document.onkeydown = function (event) {
    var e = event || window.event || arguments.callee.caller.arguments[0];
    var d = e.srcElement || e.target;
    if (e && e.keyCode == 8) {
        var dss = d.tagName.toUpperCase();
        return d.tagName.toUpperCase() == 'INPUT' || d.tagName.toUpperCase() == 'TEXTAREA' ? true : false
    }
}

$.postJSON = function (url, data, success, error) {
    return $.ajax({
        'type': 'POST',
        'url': url,
        'contentType': 'application/json',
        'data': JSON.stringify(data),
        'dataType': 'json',
        'success': success,
        'error': function (data) {
            if (error) {
                error(data.responseJSON)
            }
            else {
                $.messager.alert('提醒', data.responseJSON.Info.Message, 'info');
                //alert(data.responseJSON.Info.Message);
            }
        }
    });
};

//获取数据库时间（年月日）
function ShortDatetime(time) {
    if (time == "" || time == "null" || time == null)
        return "";
    else if (time.length < 10)
        return "";
    else if (time.substr(0, 10) == "1900-01-01")
        return "";
    else
        return time.substr(0, 10);
}
function dateConvert(value) {
    var reg = new RegExp('/', 'g');
    var d = eval('new ' + value.replace(reg, ''));
    return new Date(d).format('yyyy-MM-dd')
}
//判断数据是否为空
function GetIsNull(Paramet) {
    if (Paramet == null || Paramet == "null" || Paramet == "") {
        return "";
    } else {
        return Paramet;
    }

}
//判断数字是否为空
function GetIsNum(Paramet) {
    if (Paramet == null || Paramet == "null" || Paramet == "") {
        return 0;
    } else {
        return Paramet;
    }

}


function GetTimeNum() {
    return (new Date()).valueOf();
}

function numIsNull(num) {
    if (num == null || num == "") {
        return 0;
    } else
        return num;
}

function IsThisYearMonth(yearMonth) {
    if (yearMonth == null || yearMonth == "") {
        return false;
    }
    var date = new Date;
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    month = (month < 10 ? "0" + month : month);
    var mydate = (year.toString() + month.toString());
    if (mydate == yearMonth) { return true; }
    return false;
}

