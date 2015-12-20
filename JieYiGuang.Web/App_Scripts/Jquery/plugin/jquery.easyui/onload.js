//初始化左侧
var menudata = {
    "menus": [
        {
            "menuid": "0c84924c-95fa-4339-bc8d-619d049c6097",
            "icon": "icon-add",
            "menuname": "新闻管理",
            "menus": [
                {
                    "menuid": "d07509ee-14fd-433e-9063-e58511a3051b",
                    "menuname": "新闻类别管理",
                    "icon": "icon-add",
                    "url": "/admin/Module/ClassNews/view/list-ALL-0.aspx#"
                },
                {
                    "menuid": "6af826cf-4f1c-4b8e-888c-ca8cfee65e89",
                    "menuname": "新闻信息管理",
                    "icon": "icon-add",
                    "url": "/admin/Module/News/view/list-ALL-0.aspx#"
                }
            ]
        },
        {
            "menuid": "0c84924c-95fa-4339-bc8d-619d049c6096",
            "icon": "icon-add",
            "menuname": "管理员管理",
            "menus": [
                {
                    "menuid": "6af826cf-4f1c-4b8e-888c-ca8cfee65e88",
                    "menuname": "管理员管理",
                    "icon": "icon-add",
                    "url": "/admin/Module/Manage/view/list-ALL-0.aspx#"
                }
            ]
        }
    ]
}
function InitLeftMenu(id) {
//    ClearAccordion("#tabs");
    $("#tabs").accordion({ animate: true, border: false, fit: true });
    //展开侧面
    $('#windows').layout('expand', 'west');
    $('#tabs').html("<div class=\"panel-loading\">加载中...</div>");
//    $.ajax({
//        url: url,
//        type: "POST",
//        cache: false,
//        dataType: "json",
//        data: { ParentID: id },
//        success: function (data) {
//            $('#tabs').html("");
//            menudata = data;
//            AddAccordion("#tabs");
//        },
//        error: function () {
//            alert("载入菜单发生错误！");
//        }
    //    });
    $('#tabs').html("");
    AddAccordion("#tabs");
    
    $('body').layout();
}

//清空 Accordion - 手风琴 折叠菜单
function ClearAccordion(object) {
    if (menudata) {
        $.each(menudata.menus, function (i, n) {
            $(object).accordion('remove', n.menuname);
        });
    }
}

//增加 Accordion - 手风琴 折叠菜单
function AddAccordion(object) {
    //绑定菜单
    $.each(menudata.menus, function (i, n) {
        var menulist = '';
        menulist += '<ul>';
        $.each(n.menus, function (j, o) {
            menulist += '<li><div><a ref="' + o.menuid + '" href="javascript:void(0);" rel="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span><span class="nav">' + o.menuname + '</span></a></div></li> ';
        });
        menulist += '</ul>';
        $(object).accordion('add', {
            title: n.menuname,
            content: menulist,
            iconCls: 'icon ' + n.icon
        });
    });

    $(object).accordion('add', { title: 'demo' });
    $(object).accordion('remove', 'demo');

    //初始化按钮
    $('.easyui-accordion ul li a').click(function () {
        var tabTitle = $(this).find('.nav').text();
        var url = $(this).attr("rel");
        var menuid = $(this).attr("ref");
        var icon = getIcon(menudata, menuid, icon);
        addTab(tabTitle, url, icon);
        $('.easyui-accordion li div').removeClass("selected");
        $(this).parent().addClass("selected");
    }).hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });

    //初始化第一个
    if (menudata.menus.length > 1) {
        $(object).accordion('select', menudata.menus[0].menuname);
    }



}

//获取左侧导航的图标
function getIcon(data, menuid) {
    var icon = 'icon ';
    $.each(data.menus, function (i, n) {
        $.each(n.menus, function (j, o) {
            if (o.menuid == menuid) {
                icon += o.icon;
            }
        })
    })
    return icon;
}

function addTab(subtitle, url, icon) {
    //隐藏侧面
    $('#windows').layout('collapse', 'west');
    //创建内容页
    if (!$("#mainTab").tabs('exists', subtitle)) {
        $("#mainTab").tabs('add', {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            icon: icon,
            width:"98%",
            height:"98%"
        });
    } else {
        $("#mainTab").tabs('select', subtitle);
        $("#tab_menu-tabrefresh").trigger("click");
    }

    tabClose();
}

function createFrame(url) {
    return '<iframe scrolling="no" frameborder="0" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
}

$(function () {
    $(".ui-skin-nav .li-skinitem span").click(function () {
        var theme = $(this).attr("rel");
        $.messager.confirm('提示', '切换皮肤将重新加载系统！', function (r) {
            if (r) {
                $.post("../../Home/SetThemes", { value: theme }, function (data) { window.location.reload(); }, "json");
            }
        });
    });
});

$(function () {
    /*为选项卡绑定右键*/
    $(".tabs li").bind('contextmenu', function (e) {
        /*选中当前触发事件的选项卡 */
        var subtitle = $(this).text();
        $('#mainTab').tabs('select', subtitle);
        //显示快捷菜单
        $('#tab_menu').menu('show', {
            left: e.pageX,
            top: e.pageY
        });
        return false;
    });
});


function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    })
    /*为选项卡绑定右键*/
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#tab_menu').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children(".tabs-closable").text();

        $('#tab_menu').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
}


$(function () {
    $('#tab_menu-tabrefresh').click(function () {
        /*重新设置该标签 */
        var url = $(".tabs-panels .panel").eq($('.tabs-selected').index()).find("iframe").attr("src");
        $(".tabs-panels .panel").eq($('.tabs-selected').index()).find("iframe").attr("src", url);
    });
    //在新窗口打开该标签
    $('#tab_menu-openFrame').click(function () {
        var url = $(".tabs-panels .panel").eq($('.tabs-selected').index()).find("iframe").attr("src");
        window.open(url);
    });
    //关闭当前
    $('#tab_menu-tabclose').click(function () {
        var currtab_title = $('.tabs-selected .tabs-inner span').text();
        $('#mainTab').tabs('close', currtab_title);
        if ($(".tabs li").length == 0) {
            //open menu
            $(".layout-button-right").trigger("click");
        }
    });
    //全部关闭
    $('#tab_menu-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            if ($(this).parent().next().is('.tabs-close')) {
                var t = $(n).text();
                $('#mainTab').tabs('close', t);
            }
        });
        //open menu
        $(".layout-button-right").trigger("click");
    });
    //关闭除当前之外的TAB
    $('#tab_menu-tabcloseother').click(function () {
        var currtab_title = $('.tabs-selected .tabs-inner span').text();
        $('.tabs-inner span').each(function (i, n) {
            if ($(this).parent().next().is('.tabs-close')) {
                var t = $(n).text();
                if (t != currtab_title)
                    $('#mainTab').tabs('close', t);
            }
        });
    });
    //关闭当前右侧的TAB
    $('#tab_menu-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            $.messager.alert('提示', '前面没有了!', 'warning');
            return false;
        }
        nextall.each(function (i, n) {
            if ($('a.tabs-close', $(n)).length > 0) {
                var t = $('a:eq(0) span', $(n)).text();
                $('#mainTab').tabs('close', t);
            }
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#tab_menu-tabcloseleft').click(function () {

        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            $.messager.alert('提示', '后面没有了!', 'warning');
            return false;
        }
        prevall.each(function (i, n) {
            if ($('a.tabs-close', $(n)).length > 0) {
                var t = $('a:eq(0) span', $(n)).text();
                $('#mainTab').tabs('close', t);
            }
        });
        return false;
    });

});

//读取动态时间的变化
function ReadDateTimeShow() {
    var year = new Date().getFullYear();
    var Month = new Date().getMonth() + 1;
    var Day = new Date().getDate();
    var Time = new Date().toLocaleTimeString();
    var AddDate = year + "-" + Month + "-" + Day + " " + Time;
    $("#date").text(AddDate);
}

//改变皮肤
function onChangeTheme(theme) {
    var $easyuiTheme = $('#easyuiTheme');
    var url = $easyuiTheme.attr('href');
    var href = '/App_Scripts/Jquery/plugin/jquery.easyui/themes/' + theme + '/easyui.css';
    $easyuiTheme.attr('href', href);

    var $iframe = $('iframe');
    if ($iframe.length > 0) {
        for (var i = 0; i < $iframe.length; i++) {
            var ifr = $iframe[i];
            $(ifr).contents().find('#easyuiTheme').attr('href', href);
        }
    }
}
//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
    $.messager.alert(title, msgString, msgType);
}

function Msgshow(msg) {
    $.messager.show({
        title: '提示',
        msg: msg,
        showType: 'show'
    });
}
function Msgslide(msg) {
    $.messager.show({
        title: '提示',
        msg: msg,
        timeout: 3000,
        showType: 'slide'
    });
}
function Msgfade(msg) {
    $.messager.show({
        title: '提示',
        msg: msg,
        timeout: 3000,
        showType: 'fade'
    });
}

//页面载入窗口
function Loading() {
    $.messager.progress({
        title: '请等待',
        msg: '页面加载中...'
    });
}
function LoadingClose() {
    $.messager.progress('close');
}
//初始化编辑窗口
function openWindow(id, subtitle, url, icon) {
    return $(id).window({
        closed: true,
        iconCls: icon,            //图标class
        collapsible: true,        //折叠
        minimizable: false,       //最小化
        maximizable: true,        //最大化
        resizable: true,          //改变窗口大小
        title: subtitle,          //窗口标题
        shadow: true,             //显示阴影  
        modal: true,              //模态
        content: createFrame(url)
    });
}
function openDialog(id, subtitle, url, icon) {
    return $(id).dialog({
        closed: true,
        iconCls: icon,            //图标class
        collapsible: true,        //折叠
        minimizable: false,       //最小化
        maximizable: true,        //最大化
        resizable: true,          //改变窗口大小
        title: subtitle,          //窗口标题
        draggable: true,         //窗口拖拽
        modal: true,              //模态
        inline: false,            //定义如何放置窗口， true 就放在它的父容器里， false 就浮在所有元素的顶部。
        content: createFrame(url)
    });
}

function ajaxAction(url, type, timeout) {
    $.messager.progress();	//显示进度条
    $.ajax({
        url: url,
        type: type,
        cache: false,
        dataType: "json",
        timeout: timeout,
        error: function () {
            $.messager.alert('错误', '操作失败,提交超时!', 'error');
            // 如果表单是无效的则隐藏进度条
            $.messager.progress('close');
        },
        success: function (data) {
            if (data.success) {
                // 如果提交成功则隐藏进度条
                $.messager.progress('close');
                $.messager.alert('操作成功', '操作成功<span class="red">' + data.message + "</span>条记录！", 'info');
                refreshGrid();
            } else {
                // 如果表单是无效的则隐藏进度条
                $.messager.progress('close');
                $.messager.alert('操作错误', data.message, 'error');
            }
        }
    });
}

//验证扩展
$.extend($.fn.validatebox.defaults.rules, {
    CHS: {
        validator: function (value, param) {
            return /^[\u0391-\uFFE5]+$/.test(value);
        },
        message: '请输入汉字'
    },
    ZIP: {
        validator: function (value, param) {
            return /^[1-9]\d{5}$/.test(value);
        },
        message: '邮政编码不存在'
    },
    QQ: {
        validator: function (value, param) {
            return /^[1-9]\d{4,10}$/.test(value);
        },
        message: 'QQ号码不正确'
    },
    mobile: {
        validator: function (value, param) {
            return /^((\(\d{2,3}\))|(\d{3}\-))?13\d{9}$/.test(value);
        },
        message: '手机号码不正确'
    },
    loginName: {
        validator: function (value, param) {
            return /^[\u0391-\uFFE5\w]+$/.test(value);
        },
        message: '登录名称只允许汉字、英文字母、数字及下划线。'
    },
    safepass: {
        validator: function (value, param) {
            return safePassword(value);
        },
        message: '密码由字母和数字组成，至少6位'
    },
    equalTo: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: '两次输入的字符不一至'
    },
    noroot: {
        validator: function (value, param) {
            return /^[1-9]\d{0,10}$/.test(value);
        },
        message: '请选择栏目'
    },
    number: {
        validator: function (value, param) {
            return /^\d+$/.test(value);
        },
        message: '请输入数字'
    },
    idcard: {
        validator: function (value, param) {
            return idCard(value);
        },
        message: '请输入正确的身份证号码'
    }
});
/* 密码由字母和数字组成，至少6位 */
var safePassword = function (value) {
    return !(/^(([A-Z]*|[a-z]*|\d*|[-_\~!@#\$%\^&\*\.\(\)\[\]\{\}<>\?\\\/\'\"]*)|.{0,5})$|\s/.test(value));
}

var idCard = function (value) {
    if (value.length == 18 && 18 != value.length) return false;
    var number = value.toLowerCase();
    var d, sum = 0, v = '10x98765432', w = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2], a = '11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,50,51,52,53,54,61,62,63,64,65,71,81,82,91';
    var re = number.match(/^(\d{2})\d{4}(((\d{2})(\d{2})(\d{2})(\d{3}))|((\d{4})(\d{2})(\d{2})(\d{3}[x\d])))$/);
    if (re == null || a.indexOf(re[1]) < 0) return false;
    if (re[2].length == 9) {
        number = number.substr(0, 6) + '19' + number.substr(6);
        d = ['19' + re[4], re[5], re[6]].join('-');
    } else d = [re[9], re[10], re[11]].join('-');
    if (!isDateTime.call(d, 'yyyy-MM-dd')) return false;
    for (var i = 0; i < 17; i++) sum += number.charAt(i) * w[i];
    return (re[2].length == 9 || number.charAt(17) == v.charAt(sum % 11));
}

var isDateTime = function (format, reObj) {
    format = format || 'yyyy-MM-dd';
    var input = this, o = {}, d = new Date();
    var f1 = format.split(/[^a-z]+/gi), f2 = input.split(/\D+/g), f3 = format.split(/[a-z]+/gi), f4 = input.split(/\d+/g);
    var len = f1.length, len1 = f3.length;
    if (len != f2.length || len1 != f4.length) return false;
    for (var i = 0; i < len1; i++) if (f3[i] != f4[i]) return false;
    for (var i = 0; i < len; i++) o[f1[i]] = f2[i];
    o.yyyy = s(o.yyyy, o.yy, d.getFullYear(), 9999, 4);
    o.MM = s(o.MM, o.M, d.getMonth() + 1, 12);
    o.dd = s(o.dd, o.d, d.getDate(), 31);
    o.hh = s(o.hh, o.h, d.getHours(), 24);
    o.mm = s(o.mm, o.m, d.getMinutes());
    o.ss = s(o.ss, o.s, d.getSeconds());
    o.ms = s(o.ms, o.ms, d.getMilliseconds(), 999, 3);
    if (o.yyyy + o.MM + o.dd + o.hh + o.mm + o.ss + o.ms < 0) return false;
    if (o.yyyy < 100) o.yyyy += (o.yyyy > 30 ? 1900 : 2000);
    d = new Date(o.yyyy, o.MM - 1, o.dd, o.hh, o.mm, o.ss, o.ms);
    var reVal = d.getFullYear() == o.yyyy && d.getMonth() + 1 == o.MM && d.getDate() == o.dd && d.getHours() == o.hh && d.getMinutes() == o.mm && d.getSeconds() == o.ss && d.getMilliseconds() == o.ms;
    return reVal && reObj ? d : reVal;
    function s(s1, s2, s3, s4, s5) {
        s4 = s4 || 60, s5 = s5 || 2;
        var reVal = s3;
        if (s1 != undefined && s1 != '' || !isNaN(s1)) reVal = s1 * 1;
        if (s2 != undefined && s2 != '' && !isNaN(s2)) reVal = s2 * 1;
        return (reVal == s1 && s1.length != s5 || reVal > s4) ? -10000 : reVal;
    }
};

//全部替换
String.prototype.replaceAll = stringReplaceAll;
function stringReplaceAll(AFindText, ARepText) {
    var raRegExp = new RegExp(AFindText.replace(/([\(\)\[\]\{\}\^\$\+\-\*\?\.\"\'\|\/\\])/g, "\\$1"), "ig");
    return this.replace(raRegExp, ARepText);
}


//获得Fck编辑器内容
function GetFckContent(objId) {
    var fckEditor = FCKeditorAPI.GetInstance(objId);
    return fckEditor.GetXHTML(true);
}

//获得Fck编辑器内容的字数
function GetFckLength(objId) {
    var oEditor = FCKeditorAPI.GetInstance(objId);
    var oDOM = oEditor.EditorDocument;
    var iLength;
    if (document.all) {
        iLength = oDOM.body.innerText.length;
    } else {
        var r = oDOM.createRange();
        r.selectNodeContents(oDOM.body);
        iLength = r.toString().length;
    }
    return iLength;
}

//自动排版
function formatContent(objId) {
    var content = "";
    if (eval("editor_" + objId + " != null")) { eval("content = editor_" + objId + ".html()") }
    else { content = $("#" + objId).val(); }
    content = content.replace(/(<(?!(\/?(img|a)\b))[^>]*>)/gi, "");;
    content = content.replace(/^[ |　]+/gm, "");
    content = content.replace(/(^[\r\n]+)|([\r\n]+$)/g, "");
    content = content.replaceAll("　", "");
    content = content.replaceAll("&nbsp;", "");
    content = content.replace(/([\r\n])+/gm, "</p>$1<p style=\"text-indent:2em;\">");
    content = "<p style=\"text-indent:2em;\">" + content + "</p>";
    eval("editor_" + objId + ".html(content)")
}

//自动提取概述
function formatDescriptions(objId, toObjId, length) {
    var content = "";
    if (eval("editor_" + objId + " != null")) { eval("content = editor_" + objId + ".html()") }
    else { content = $("#" + objId).val(); }
    content = content.replace(/<[^>].*?>/g, "");
    content = content.replace(/^[ |　]+/gm, "");
    content = content.replace(/(^[\r\n]+)|([\r\n]+$)/g, "");
    content = content.replace(/([\r\n])+/gm, "");
    content = content.replaceAll(" ", "");
    content = content.replaceAll("	", "");
    content = content.replaceAll("&nbsp;", "");
    $("#" + toObjId).val(content.substring(0, length));
}

//FCK自动提取图片集
function formatFckImages(objId) {
    var fckEditor = FCKeditorAPI.GetInstance(objId);
    var content = fckEditor.GetXHTML(true);
    alert("2222");
    var imgSrcs = content.find('img');
    alert(imgSrcs.length);
}

function formatIsStatus(title) {
    switch (title) {
        case "全部": return 99; break;
        case "正常": return 0; break;
        case "锁定": return 1; break;
        case "审核": return 2; break;
        case "过期": return 3; break;
        case "停用": return 4; break;
        case "删除": return -1; break;
    }
}

function CheckAll(name, checked) {
    var codes = document.getElementsByName(name);
    var len = codes.length;
    if (len > 0) {
        var i = 0;
        for (i = 0; i < len; i++) {
            codes[i].checked = checked;
        }
    }
}

function InitAjaxUpload() {
    //上传组件
    $(".AjaxUploadButton").JSAjaxFileUploader({
        uploadUrl: '/Upload/UploadFile.html?Path=' + $(".AjaxUploadButton").attr("path"),
        inputText: '选择上传文件',
        //fileName: 'photo',
        maxFileSize: 2 * 1024 * 1024,    //Max 500 KB file 1kb=1024字节
        allowExt: 'gif|jpg|jpeg|png',
        zoomPreview: false,
        zoomWidth: 360,
        zoomHeight: 360,
        success: function (data) {
            $("[name='" + $(".AjaxUploadButton").attr("tarVal") + "']").val(data.srcfilepath);
            var show = $(".AjaxUploadButton").attr("show");
            if (show != undefined && show != '') {
                $("img[name='" + show + "'|id='" + show + "']").attr("src", data.smallpath);
            }
        },
        error: function (data) {
            alert(data.msg);
        }
    });
}

function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid + "";
}


$(function () {
    if($(".AjaxUploadButton").length > 0){
        try{
            InitAjaxUpload();
        } catch (e) {
            $.getScript("/App_Scripts/Jquery/plugin/AjaxFileUploader/JQuery.JSAjaxFileUploaderSingle.js", function () {
                InitAjaxUpload();
            });
        }
    }
});

