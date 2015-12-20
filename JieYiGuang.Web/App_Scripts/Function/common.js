//替换字符串中所有符合的字符内容
String.prototype.replaceAll = function (reallyDo, replaceWith, ignoreCase) {
    if (!RegExp.prototype.isPrototypeOf(reallyDo)) {
        return this.replace(new RegExp(reallyDo, (ignoreCase ? "gi" : "g")), replaceWith);
    } else {
        return this.replace(reallyDo, replaceWith);
    }
}
//只能输入数字 onkeypress="IsNum()"
function IsNum() {
    var keyCode = event.keyCode;
    if ((keyCode >= 48 && keyCode <= 57)) {
        event.returnValue = true;
    } else {
        event.returnValue = false;
    }
}

//关键字加亮
function SearchKey(value,key) {
    return value.replace(key, "<span style=\"color:#f00\">" + key+"</span>")
}

function AutoResizeImage(maxWidth, maxHeight, objImg) {
    var img = new Image();
    img.src = objImg.src;
    var hRatio;
    var wRatio;
    var Ratio = 1;
    var w = img.width;
    var h = img.height;
    wRatio = maxWidth / w;
    hRatio = maxHeight / h;
    if (maxWidth == 0 && maxHeight == 0) {
        Ratio = 1;
    } else if (maxWidth == 0) {//
        if (hRatio < 1) Ratio = hRatio;
    } else if (maxHeight == 0) {
        if (wRatio < 1) Ratio = wRatio;
    } else if (wRatio < 1 || hRatio < 1) {
        Ratio = (wRatio <= hRatio ? wRatio : hRatio);
    }
    if (Ratio < 1) {
        w = w * Ratio;
        h = h * Ratio;
    }
    objImg.height = h;
    objImg.width = w;
}

//隔行变色
function TableBG(TableID) {
    //隔行变色 奇偶变色，添加样式
    $("#" + TableID + " tr:even").addClass("trStyle1");
    $("#" + TableID + " tr").hover(
        function () {
            $(this).addClass("hover");
        },
        function () {
            $(this).removeClass("hover");
        }
    );
}
//触发当前选中行Input
function TableInput(TableID) {
    //点击选中当前行
    $("#" + TableID + " tr").click(

        function () {
            if ($(this).find("input:checkbox[name='ID']").length > 0) {
                if (!$(this).find("input:checkbox[name='ID']").attr("checked")) {
                    $(this).addClass("marked");
                    $(this).find("input:checkbox[name='ID']").attr("checked", "true");
                } else {
                    $(this).removeClass("marked");
                    $(this).find("input:checkbox[name='ID']").removeAttr("checked");
                }
            }
        }
    );
    $("input:checkbox[name='ID']").click(function () {
        $(this).parents("tr").trigger("click");
    })
}

// 全选操作
function MarkAll(obj) {
    if (obj.checked) {

        if (MarkAllRows('Table')) return false;
    }
    else {

        if (unMarkAllRows('Table')) return false;
    }
}

// 全选操作
function MarkAllRows(TableID) {
    //全选
    $("#" + TableID).find("input:checkbox[name='ALL']").attr("checked", "true");
    $("#" + TableID).find("input:checkbox[name='ID']").attr("checked", "true"); //全选
    $("#" + TableID).find("input:checkbox[name='ID']").parents("tr").addClass("marked");
    return true;
}

function unMarkAllRows(TableID) {
    $("#" + TableID).find("input:checkbox[name='ALL']").removeAttr("checked"); //全选
    $("#" + TableID).find("input:checkbox[name='ID']").removeAttr("checked"); //全选
    $("#" + TableID).find("input:checkbox[name='ID']").parents("tr").removeClass("marked");
    return true;
}

//选择操作模式
function GetType(FormName) {
    SelectNum = 0
    var DOAction = $("#DO").val();
    var SearchClassID = $("#SearchClassID").val();
    var SelectNum = $("#" + FormName).find("input:checkbox[name='ID'][checked='true']").length;
    if (SelectNum == 0) { $("#DO").val(""); alert("请选择操作对象！"); return false; }
    if (DOAction == "MoreTo" && SearchClassID == "") { $("#DO").val(""); alert("请选择移动到对象！"); return false; }
    if (DOAction == "CopyTo" && SearchClassID == "") { $("#DO").val(""); alert("请选择复制到对象！"); return false; }
    top.window.DivDialog.Operating();
    $("#" + FormName).submit();
    $("#DO").val("");
}



function SelColors(FiledName, PicName, Path) {
    if (typeof (Path) == "undefined" || Path == "") { Path = "../../"; }
    var retval;
    retval = window.showModalDialog(Path + "Script/Inc/SelectColor/Select_color.htm", "SelColor", "dialogWidth:290px;dialogHeight:250px;dialogLeft:372px;dialogTop:210px;directories:no;localtion:no;menubar:no;status:no;toolbar:no;scrollbars:yes;Resizeable:no;help:no");
    if (retval != null) {
        document.getElementById(FiledName).value = retval;
        if (typeof (PicName) != "undefined" && PicName != "") {
            document.getElementById(PicName).style.backgroundColor = retval;
        }
    }
}

//设置tags内容	
function SelectTag(TagsValueId, tag) {
    tags = document.getElementById(TagsValueId);
    if (!tags)
        tags = document.getElementById(TagsValueId);
    if (tags.value.indexOf(tag) < 0) {
        if ((tag.indexOf(" ") > 0) || (tag.indexOf("|") > 0) || (tag.indexOf("　") > 0) || (tag.indexOf("| ") > 0)) {
            tags.value += " \"" + tag + "\"";
        }
        else {
            if (tags.value == "")
            { tags.value = tag; }
            else
            { tags.value += "|" + tag + "|"; }
        }
    }
}

//显示隐藏DIV内容	
function DisplayDiv(TagsValueId) {
    TagsDiv = document.getElementById(TagsValueId);
    if (TagsDiv.style.display == "none")
    { TagsDiv.style.display = ""; }
    else
    { TagsDiv.style.display = "none"; }
}

//打开上传模态对话框
function UploadDialog(TextName, UploadType, UploadFolder, IsSmall, Width, Height) {
    $.dialog.data('UploadFileName', TextName); //上传输入框名称
    if ($("#" + TextName).val()) {
        $.dialog.data('UploadFileValue', $("#" + TextName).val()); //上传输入框值
    } else {
        $.dialog.data('UploadFileValue', " "); //上传输入框值
    }
    $.dialog.open('/Upload/UploadFiles.aspx?Upload_UploadType=' + UploadType + '&Upload_UploadFolder=' + UploadFolder + '&Upload_IsSmall=' + IsSmall + '&Upload_Width=' + Width + '&Upload_Height=' + Height, { title: '文件上传：', width: 460, height: 340 });
}



function RemoveHtml(str) {
    str = str.replace(/&<BR>/g, " ");
    str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
    str.value = str.replace(/[ | ]*\n/g, '\n'); //去除行尾空白
    str = str.replace(/\n[\s| | ]*\r/g, '\n'); //去除多余空行
    str = str.replace(/&lt;/g, "<");
    str = str.replace(/&gt;/g, ">");
    str = str.replace(/&nbsp;/g, " ");
    return str;
}

function GetDescribe(str, str2) {
    $("#" + str2).value = RemoveHtml($("#" + str).value).substr(0, 130);
}

//json鏃堕棿鎴宠浆鎹㈡棩鏈�
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month 
        "d+": this.getDate(), //day 
        "h+": this.getHours(), //hour 
        "m+": this.getMinutes(), //minute 
        "s+": this.getSeconds(), //second 
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
        "S": this.getMilliseconds() //millisecond 
    };
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};
//"yyyy-MM-dd hh:mm:ss"
function FormatTime(jsonDate, format) {
    try {
        var date = eval(jsonDate.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
        return date.format(format);
    } catch (e) {
        return jsonDate;
    }
}


/*图片接触预览*/
$(function () {
    /* CONFIG */

    xOffset = 10;
    yOffset = 30;

    /* END CONFIG */
    $(".screenshot").hover(function (e) {
        $("body").append("<p id='screenshot' style='position: absolute;'><img src='" + $(this).attr("src") + "'/></p>");
        $("#screenshot")
			.css("top", (e.pageY - xOffset) + "px")
			.css("left", (e.pageX + yOffset) + "px")
            .css("z-index", "99999999")
			.fadeIn("fast");
    },
	function () {
	    this.title = this.t;
	    $("#screenshot").remove();
	});
    $(".screenshot").mousemove(function (e) {
        $("#screenshot")
			.css("top", (e.pageY - xOffset) + "px")
			.css("left", (e.pageX + yOffset) + "px");
    });
});


//img等比例缩略图
function AutoSize(ImgD, MaxWidth, MaxHeight) {
    var image = new Image();
    image.src = ImgD.src;
    if (image.width > 0 && image.height > 0) {
        flag = true;
        if (image.width / image.height >= MaxWidth / MaxHeight) {
            if (image.width > MaxWidth) {
                ImgD.width = MaxWidth;
                ImgD.height = (image.height * MaxWidth) / image.width;
            }
            else {
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
            //ImgD.alt="原始尺寸:宽" + image.width+",高"+image.height;
        }
        else {
            if (image.height > MaxHeight) {
                ImgD.height = MaxHeight;
                ImgD.width = (image.width * MaxHeight) / image.height;
            }
            else {
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
            //ImgD.alt="原始尺寸:宽" + image.width+",高"+image.height;
        }
    }
}