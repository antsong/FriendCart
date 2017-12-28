$(function () {
    //全选组件
    $("#selectAll").click(function () {
        var value = $(this).prop("checked");
        $(".select").each(function (index, s) {
            $(s).prop({ checked: value });
        });
    });
});

$(window).scroll(function () {
    if ($(window).scrollTop() > $(window).height()) {//滚动条滚动，返回顶部按钮显示、隐藏
        $(".zh-backtotop").fadeIn(500);
    } else {
        $(".zh-backtotop").fadeOut(500);
    }
});

$(function () {
    //返回顶部
    $(".btn-backtotop").click(function () {
        $('body,html').animate({ scrollTop: 0 }, 500);
    });

    $("#content").focus(function () {
        $(this).animate({ width: '360px' }, 500); // 是 cc 的宽度在300毫秒的时间变化
    }).blur(function () { $(this).animate({ width: '153px' }, 500); });
});

window.alert = function (str) {
    var shield = document.createElement_x("DIV");
    shield.id = "shield";
    shield.style.position = "absolute";
    shield.style.left = "0px";
    shield.style.top = "0px";
    shield.style.width = "100%";
    shield.style.height = document.body.scrollHeight + "px";
    //弹出对话框时的背景颜色
    shield.style.background = "#fff";
    shield.style.textAlign = "center";
    shield.style.zIndex = "25";
    //背景透明 IE有效
    //shield.style.filter = "alpha(opacity=0)";
    var alertFram = document.createElement_x("DIV");
    alertFram.id = "alertFram";
    alertFram.style.position = "absolute";
    alertFram.style.left = "50%";
    alertFram.style.top = "50%";
    alertFram.style.marginLeft = "-225px";
    alertFram.style.marginTop = "-75px";
    alertFram.style.width = "450px";
    alertFram.style.height = "150px";
    alertFram.style.background = "#ff0000";
    alertFram.style.textAlign = "center";
    alertFram.style.lineHeight = "150px";
    alertFram.style.zIndex = "300";
    strHtml = "<ul style=\"list-style:none;margin:0px;padding:0px;width:100%\">\n";
    strHtml += " <li style=\"background:#DD828D;text-align:left;padding-left:20px;font-size:14px;font-weight:bold;height:25px;line-height:25px;border:1px solid #F9CADE;\">[自定义提示]</li>\n";
    strHtml += " <li style=\"background:#fff;text-align:center;font-size:12px;height:120px;line-height:120px;border-left:1px solid #F9CADE;border-right:1px solid #F9CADE;\">" + str + "</li>\n";
    strHtml += " <li style=\"background:#FDEEF4;text-align:center;font-weight:bold;height:25px;line-height:25px; border:1px solid #F9CADE;\"><input type=\"button\" value=\"确 定\" onclick=\"doOk()\" /></li>\n";
    strHtml += "</ul>\n";
    alertFram.innerHTML = strHtml;
    document.body.appendChild(alertFram);
    document.body.appendChild(shield);
    var ad = setInterval("doAlpha()", 5);
    this.doOk = function () {
        alertFram.style.display = "none";
        shield.style.display = "none";
    }
    alertFram.focus();
    document.body.onselectstart = function () { return false; };
}