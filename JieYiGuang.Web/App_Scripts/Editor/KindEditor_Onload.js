var editor = null;

KindEditor.ready(function (K) {
    $.getScript('/App_Scripts/Editor/KindEditor/lang/zh_CN.js', function () {
        editor = K.editor({
            allowFileManager: true
        });
    });
});

//动态加载编辑器
function LoadKindEditor(textname) {
    if (editor == null) {
        editor = KindEditor.create('textarea[name="' + textname + '"]', {
            afterChange: function () {
                editor.sync();
                $("#word_count_" + textname).html("您当前输入了" + this.count() + "个字符，" + this.count('text') + "个文字");
            },
            allowFileManager: true,
            newlineTag: 'p'
        });
    }
}
//注销编辑器
function RemoveKindEditor(textname) {
    if (editor != null) {
        editor.remove();
        editor = null;
        $("#word_count_" + textname).html("");
    }
}

//Kind文件上传选择
function KindUploadManager(id, type) {
    editor.loadPlugin('dialogupload', function () {
        editor.plugin.dialogupload({
            fileUrl: $('#' + id).val(),
            fileType: type,
            clickFn: function (url, title) {
                $('#' + id).val(url);
                editor.hideDialog();
            }
        });
    });
}

function ColorPicker(btnId, objID) {
    var colorpickerPos = KindEditor('#' + btnId).pos();
    var colorpicker = KindEditor.colorpicker({
        x: colorpickerPos.x,
        y: colorpickerPos.y + KindEditor('#' + btnId).height(),
        z: 19811214,
        selectedColor: 'default',
        noColor: '无颜色',
        click: function (color) {
            KindEditor('#' + objID).val(color);
            colorpicker.remove();
            colorpicker = null;
        }
    });
}

//文件预览
function FilePreview(id) {
    if ($('#' + id).val() != "") {
        window.open($('#' + id).val(), "", "fullscreen=1")
    }
    else {
        alert("请先上传文件！");
    }
}

//检查是否存在远程图片
function CheckRemoteFile(TextareaName,IsLoadImg) {
    if (! +[1, ])
        var img = document.frames['ke-edit-iframe'].document.getElementsByTagName('img'); //是ie浏览器
    else
        var img = document.getElementById('ke-edit-iframe').contentDocument.getElementsByTagName('img');
    var piccount = 0; //符合远程图片规则
    var url = []//收集远程图片地址的数组
    if (img.length > 0) {

        var rx = new RegExp('^https?:\\/\\/' + location.host.replace(/\./g, '\\.'), 'i')//本站主域名的正则
        var rxRemote = /^https?:\/\// //远程图片正则
        for (var i = 0; i < img.length; i++) {
            src = img[i].src;
            if (!rx.test(src) && rxRemote.test(src)) {
                piccount = piccount + 1;
            }
        }
        //alert(uploadUrl);
        if (piccount > 0) {
            document.getElementById("PicConfirm_" + TextareaName).innerHTML = "有<span sytle='color:red'>" + piccount + "</span>图片可以传到服务器上";
            if (IsLoadImg == "checked") {

                return uploadpic(TextareaName);

            } else {

				return true;

            }
        }
        else {
            document.getElementById("PicConfirm_" + TextareaName).innerHTML = "";
        }
    }
    return true;
}
//获取图片的父对象是否为链接，是则返回以便同时更新链接url
function getParantA(obj) { while (obj = obj.parentNode) if (obj.tagName == 'A') return obj; return false; }

var piclist = "";
///Ajax+ASHX 下载图片到本地
function uploadpic(TextareaName) {
    var uploadUrl = "UploadFiles/image";
    if (! +[1, ])
        var img = document.frames['ke-edit-iframe'].document.getElementsByTagName('img'); //是ie浏览器
    else
        var img = document.getElementById('ke-edit-iframe').contentDocument.getElementsByTagName('img');
    var upimgs = []; //收集远程图片对象img的数组，方便更新src属性
    var upOk = 0, upfalse = 0;
    if (img.length > 0) {
        var url = []//收集远程图片地址的数组
        var rx = new RegExp('^https?:\\/\\/' + location.host.replace(/\./g, '\\.'), 'i')//本站主域名的正则
        var rxRemote = /^https?:\/\// //远程图片正则
        var xhr, src;
        for (var i = 0; i < img.length; i++) {
            src = img[i].src;
            if (!rx.test(src) && rxRemote.test(src)) { url[url.length] = src; upimgs[upimgs.length] = img[i]; } //收集远程图片地址和img对象
        }
        if (url.length > 0) {
            piclist = url.join('|');
            $.ajax({
                url: "/App_Scripts/Editor/KindEditor/net/uploadpic.ashx",
                data: { "pic": piclist, "uploadUrl": uploadUrl },
                type: "Post",
                async: false,
                //dataType: "json",
                beforeSend: function () {
                    $("#PicConfirm_" + TextareaName).text($("#PicConfirm_" + TextareaName).text() + ",正在上传中...");
                },
                success: function (responsevalue) {
                    //alert(responsevalue);
                    responsevalue = eval("(" + responsevalue + ")");
                    if (responsevalue.success) {
                        //ashx返回的items数组对象一定要和发送时的upimgs数组的长度一致
                        //alert(upimgs.length);
                        if (responsevalue.items.length == upimgs.length) {
                            var pNode;
                            for (var i = 0; i < responsevalue.items.length; i++) {
                                if (responsevalue.items[i] != "false") {
                                    //alert(responsevalue.items[i]);
                                    upimgs[i].src = responsevalue.items[i];
                                    //upimgs[i].data-ke-src = responsevalue.items[i];
                                    $(upimgs[i]).attr("data-ke-src", responsevalue.items[i])
                                    //	                        pNode = getParantA(upimgs[i]); //========看图片是否加了链接，加了同时更新链接的href
                                    //	                        if (pNode) pNode.href = responsevalue.items[i];
                                    upOk = upOk + 1;
                                }
                                else {
                                    upfalse = upfalse + 1;
                                }
                            }
                            if (upfalse > 0) {
                                $("#PicConfirm_" + TextareaName).html("<span class='red'>" + upOk + "</span>张远程图片抓取成功！;<span class='red'>" + upfalse + "</span>张远程图片抓取失败！");
                            }
                            else {
                                $("#PicConfirm_" + TextareaName).html("<span class='red'>" + upOk + "</span>张远程图片抓取成功！");
                            }
                        }
                    }
                    else {
                        $("#confirm").text(responsevalue.Msg);
                    }

                },
                error: function (responseValue) {
                    alert("远程图片自动上传失败！");
                }
            });
        }

    }
    if (upOk == upimgs.length)
        return true;
    else
        return false;
}

