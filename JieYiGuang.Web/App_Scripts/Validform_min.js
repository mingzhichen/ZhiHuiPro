/*
    通用表单验证方法
    Validform version 5.1
	By sean during April 7, 2010 - July 31, 2012
	For more information, please visit http://validform.rjboy.cn
	Validform is available under the terms of the MIT license.
*/

(function (d, f, b) { var g = null, j = null, i = true; var e = { tit: "提示信息", w: "请输入正确信息！", r: "通过信息验证！", c: "正在检测信息…", s: "请填入信息！", v: "所填信息没有经过验证，请稍后…", p: "正在提交数据…", err: "出错了！请检查提交地址或返回数据格式是否正确！", abort: "Ajax操作被取消！" }; d.Tipmsg = e; var a = function (l, n, k) { var n = d.extend({}, a.defaults, n); n.datatype && d.extend(a.util.dataType, n.datatype); var m = this; m.tipmsg = {}; m.settings = n; m.forms = l; m.objects = []; if (k === true) { return false } l.each(function (p) { var o = d(this); this.status = "normal"; o.data("tipmsg", m.tipmsg); o.find("[datatype]").live("blur", function () { var q = arguments[1]; var t = a.util.getValue.call(o, d(this)); if (d(this).data("dataIgnore") === "dataIgnore" || n.dragonfly && !d(this).data("cked") && a.util.isEmpty.call(d(this), t)) { return false } var r = true, u; g = u = d(this); r = a.util.regcheck.call(o, d(this).attr("datatype"), t, d(this)); if (!r.passed) { a.util.abort.call(u[0]); u.addClass("Validform_error"); a.util.showmsg.call(o, r.info, n.tiptype, { obj: d(this), type: r.type, sweep: n.tipSweep }, "hide") } else { if (d(this).attr("ajaxurl")) { var s = d(this); if (s[0].valid === "posting") { return false } s[0].valid = "posting"; a.util.showmsg.call(o, m.tipmsg.c || e.c, n.tiptype, { obj: s, type: 1, sweep: n.tipSweep }, "hide"); a.util.abort.call(u[0]); u[0].ajax = d.ajax({ type: "POST", cache: false, url: s.attr("ajaxurl"), data: "param=" + t + "&name=" + d(this).attr("name"), dataType: "text", success: function (v) { if (d.trim(v) == "y") { s[0].valid = "true"; a.util.showmsg.call(o, m.tipmsg.r || e.r, n.tiptype, { obj: s, type: 2, sweep: n.tipSweep }, "hide"); u.removeClass("Validform_error"); g = null; if (q === "postform") { o.trigger("submit") } } else { s[0].valid = v; u.addClass("Validform_error"); a.util.showmsg.call(o, v, n.tiptype, { obj: s, type: 3, sweep: n.tipSweep }) } u[0].ajax = null }, error: function () { s[0].valid = m.tipmsg.err || e.err; u.addClass("Validform_error"); u[0].ajax = null; a.util.showmsg.call(o, m.tipmsg.err || e.err, n.tiptype, { obj: s, type: 3, sweep: n.tipSweep }) } }) } else { a.util.showmsg.call(o, r.info, n.tiptype, { obj: d(this), type: r.type, sweep: n.tipSweep }, "hide"); u.removeClass("Validform_error"); g = null } } }); a.util.hasDefaultText.call(o); a.util.recheckEnhance.call(o); if (n.usePlugin) { a.util.usePlugin.call(o, n.usePlugin, n.tiptype, n.tipSweep, p) } o.find(":checkbox[datatype],:radio[datatype]").each(function () { var r = d(this); var q = r.attr("name"); o.find("[name='" + q + "']").filter(":checkbox,:radio").bind("click", function () { setTimeout(function () { r.trigger("blur") }, 0) }) }); n.btnSubmit && o.find(n.btnSubmit).bind("click", function () { var q = a.util.submitForm.call(o, n); q === b && (q = true); if (q === true) { o[0].submit() } }); o.submit(function () { var q = a.util.submitForm.call(o, n); q === b && (q = true); return q }); o.find("[type='reset']").add(o.find(n.btnReset)).bind("click", function () { a.util.resetForm.call(o) }) }); if (n.tiptype == 1 || (n.tiptype == 2 && n.ajaxPost)) { c() } }; a.defaults = { tiptype: 1, tipSweep: false, showAllError: false, postonce: false, ajaxPost: false }; a.util = { dataType: { match: /^(.+?)(\d+)-(\d+)$/, "*": /[\w\W]+/, "*6-16": /^[\w\W]{6,16}$/, n: /^\d+$/, "n6-16": /^\d{6,16}$/, s: /^[\u4E00-\u9FA5\uf900-\ufa2d\w\.\s]+$/, "s6-18": /^[\u4E00-\u9FA5\uf900-\ufa2d\w\.\s]{6,18}$/, p: /^[0-9]{6}$/, m: /^13[0-9]{9}$|15[0-9]{9}$|18[0-9]{9}$/, e: /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/, url: /^(\w+:\/\/)?\w+(\.\w+)+.*$/ }, toString: Object.prototype.toString, getValue: function (m) { var l, k = this; if (m.is(":radio")) { l = k.find(":radio[name='" + m.attr("name") + "']:checked").val(); l = l === b ? "" : l } else { if (m.is(":checkbox")) { l = k.find(":checkbox[name='" + m.attr("name") + "']:checked").val(); l = l === b ? "" : l } else { l = m.val() } } return d.trim(l) }, isEmpty: function (k) { return k === "" || k === d.trim(this.attr("tip")) }, recheckEnhance: function () { var k = this; k.find("input[recheck]").each(function () { var m = d(this); var l = k.find("input[name='" + d(this).attr("recheck") + "']"); l.bind("keyup", function () { if (l.val() == m.val() && l.val() != "") { if (l.attr("tip")) { if (l.attr("tip") == l.val()) { return false } } m.trigger("blur") } }).bind("blur", function () { if (l.val() != m.val() && m.val() != "") { if (m.attr("tip")) { if (m.attr("tip") == m.val()) { return false } } m.trigger("blur") } }) }) }, hasDefaultText: function () { this.find("[tip]").each(function () { var l = d(this).attr("tip"); var k = d(this).attr("altercss"); d(this).focus(function () { if (d(this).val() == l) { d(this).val(""); if (k) { d(this).removeClass(k) } } }).blur(function () { if (d.trim(d(this).val()) === "") { d(this).val(l); if (k) { d(this).addClass(k) } } }) }) }, usePlugin: function (s, l, q, o) { var v = this; if (s.swfupload) { var p = v.find("input[plugin='swfupload']").val(""), k = { custom_settings: { form: v, showmsg: function (w, n) { a.util.showmsg.call(v, w, l, { obj: p, type: n, sweep: q }) } } }; k = d.extend(true, {}, s.swfupload, k); if (typeof (swfuploadhandler) != "undefined") { swfuploadhandler.init(k, o) } } if (s.datepicker) { if (s.datepicker.format) { Date.format = s.datepicker.format; delete s.datepicker.format } if (s.datepicker.firstDayOfWeek) { Date.firstDayOfWeek = s.datepicker.firstDayOfWeek; delete s.datepicker.firstDayOfWeek } var r = v.find("input[plugin='datepicker']"); s.datepicker.callback && r.bind("dateSelected", function () { var n = new Date(d.event._dpCache[this._dpId].getSelected()[0]).asString(Date.format); s.datepicker.callback(n, this) }); r.datePicker(s.datepicker) } if (s.passwordstrength) { s.passwordstrength.showmsg = function (w, x, n) { a.util.showmsg.call(v, x, l, { obj: w, type: n, sweep: q }, "hide") }; v.find("input[plugin*='passwordStrength']").passwordStrength(s.passwordstrength) } if (s.jqtransform) { var m = function (n) { var w = d(".jqTransformSelectWrapper ul:visible"); w.each(function () { var x = d(this).parents(".jqTransformSelectWrapper:first").find("select").get(0); if (!(n && x.oLabel && x.oLabel.get(0) == n.get(0))) { d(this).hide() } }) }; var t = function (n) { if (d(n.target).parents(".jqTransformSelectWrapper").length === 0) { m(d(n.target)) } }; var u = function () { d(document).mousedown(t) }; if (s.jqtransform.selector) { v.find(s.jqtransform.selector).filter('input:submit, input:reset, input[type="button"]').jqTransInputButton(); v.find(s.jqtransform.selector).filter("input:text, input:password").jqTransInputText(); v.find(s.jqtransform.selector).filter("input:checkbox").jqTransCheckBox(); v.find(s.jqtransform.selector).filter("input:radio").jqTransRadio(); v.find(s.jqtransform.selector).filter("textarea").jqTransTextarea(); if (v.find(s.jqtransform.selector).filter("select").length > 0) { v.find(s.jqtransform.selector).filter("select").jqTransSelect(); u() } } else { v.jqTransform() } v.find(".jqTransformSelectWrapper").find("li a").click(function () { v.find("select").trigger("blur") }) } }, regcheck: function (q, v, p) { var w = this, m = null, o = false, u = 3; if (p.attr("ignore") === "ignore" && a.util.isEmpty.call(p, v)) { if (p.data("cked")) { m = "" } return { passed: true, type: 4, info: m} } p.data("cked", "cked"); if (d.trim(p.attr("tip")) && v === d.trim(p.attr("tip"))) { return { passed: false, type: 3, info: p.attr("nullmsg") || w.data("tipmsg").s || e.s} } if (a.util.toString.call(a.util.dataType[q]) == "[object Function]") { o = a.util.dataType[q](v, p, w, a.util.dataType); if (o === true) { u = 2; m = w.data("tipmsg").r || e.r; if (p.attr("recheck")) { var l = w.find("input[name='" + p.attr("recheck") + "']:first"); if (v != l.val()) { o = false; u = 3; m = p.attr("errormsg") || w.data("tipmsg").w || e.w } } } else { m = o || p.attr("errormsg") || w.data("tipmsg").w || e.w; o = false; if (v === "") { return { passed: false, type: 3, info: p.attr("nullmsg") || w.data("tipmsg").s || e.s} } } return { passed: o, type: u, info: m} } if (!(q in a.util.dataType)) { var s = q.match(a.util.dataType.match), x; if (!s) { return false } for (var k in a.util.dataType) { x = k.match(a.util.dataType.match); if (!x) { continue } if (s[1] === x[1]) { var t = a.util.dataType[k].toString(), n = t.match(/\/[mgi]*/g)[1].replace("/", ""), r = new RegExp("\\{" + x[2] + "," + x[3] + "\\}", "g"); t = t.replace(/\/[mgi]*/g, "/").replace(r, "{" + s[2] + "," + s[3] + "}").replace(/^\//, "").replace(/\/$/, ""); a.util.dataType[q] = new RegExp(t, n); break } } } if (a.util.toString.call(a.util.dataType[q]) == "[object RegExp]") { o = a.util.dataType[q].test(v); if (o) { u = 2; m = w.data("tipmsg").r || e.r; if (p.attr("recheck")) { var l = w.find("input[name='" + p.attr("recheck") + "']:first"); if (v != l.val()) { o = false; u = 3; m = p.attr("errormsg") || w.data("tipmsg").w || e.w } } } else { m = p.attr("errormsg") || w.data("tipmsg").w || e.w; if (v === "") { return { passed: false, type: 3, info: p.attr("nullmsg") || w.data("tipmsg").s || e.s} } } return { passed: o, type: u, info: m} } return { passed: false, type: 3, info: w.data("tipmsg").w || e.w} }, showmsg: function (n, l, m, k) { if (n === null) { return false } d.extend(m, { curform: this }); if (typeof l == "function") { if (!(m.sweep && k == "hide")) { l(n, m, a.util.cssctl) } return false } if (l == 1 || k == "alwaysshow") { j.find(".Validform_info").html(n) } if (l == 1 && k != "hide" || k == "alwaysshow") { i = false; j.find(".iframe").css("height", j.outerHeight()); j.show(); h(j, 100) } if (l == 2 && m.obj) { m.obj.parent().next().find(".Validform_checktip").html(n); a.util.cssctl(m.obj.parent().next().find(".Validform_checktip"), m.type) } }, cssctl: function (l, k) { switch (k) { case 1: l.removeClass("Validform_right Validform_wrong").addClass("Validform_checktip Validform_loading"); break; case 2: l.removeClass("Validform_wrong Validform_loading").addClass("Validform_checktip Validform_right"); break; case 4: l.removeClass("Validform_right Validform_wrong Validform_loading").addClass("Validform_checktip"); break; default: l.removeClass("Validform_right Validform_loading").addClass("Validform_checktip Validform_wrong") } }, submitForm: function (m, k, o, q) { var s = this; if (s[0].status === "posting") { return false } if (m.postonce && s[0].status === "posted") { return false } var q = q === true ? false : true; var r = m.beforeCheck && m.beforeCheck(s); if (r === false) { return false } var p = true, l; s.find("[datatype]").each(function () { if (k) { return false } if (m.ignoreHidden && d(this).is(":hidden") || d(this).data("dataIgnore") === "dataIgnore") { return true } var u = a.util.getValue.call(s, d(this)), v; g = v = d(this); l = a.util.regcheck.call(s, d(this).attr("datatype"), u, d(this)); if (!l.passed) { v.addClass("Validform_error"); a.util.showmsg.call(s, l.info, m.tiptype, { obj: d(this), type: l.type, sweep: m.tipSweep }); if (!m.showAllError) { v.focus(); p = false; return false } p && (p = false); return true } if (d(this).attr("ajaxurl")) { if (this.valid !== "true") { var t = d(this); v.addClass("Validform_error"); a.util.showmsg.call(s, s.data("tipmsg").v || e.v, m.tiptype, { obj: t, type: 3, sweep: m.tipSweep }); if (!i || m.tiptype != 1) { setTimeout(function () { t.trigger("blur", ["postform"]) }, 1500) } if (!m.showAllError) { p = false; return false } p && (p = false); return true } } a.util.showmsg.call(s, l.info, m.tiptype, { obj: d(this), type: l.type, sweep: m.tipSweep }, "hide"); v.removeClass("Validform_error"); g = null }); if (m.showAllError) { s.find(".Validform_error:first").focus() } if (p) { var n = m.beforeSubmit && m.beforeSubmit(s); if (n === false) { return false } s[0].status = "posting"; if (m.ajaxPost || o === "ajaxPost") { a.util.showmsg.call(s, s.data("tipmsg").p || e.p, m.tiptype, { obj: s, type: 1, sweep: m.tipSweep }, "alwaysshow"); s[0].ajax = d.ajax({ type: "POST", dataType: "json", async: q, url: s.attr("action"), data: s.serializeArray(), success: function (t) { if (t.status === "y") { a.util.showmsg.call(s, t.info, m.tiptype, { obj: s, type: 2, sweep: m.tipSweep }, "alwaysshow") } else { s[0].posting = false; a.util.showmsg.call(s, t.info, m.tiptype, { obj: s, type: 3, sweep: m.tipSweep }, "alwaysshow") } m.callback && m.callback(t); s[0].status = "posted"; s[0].ajax = null }, error: function (t) { var u = t.statusText === "abort" ? s.data("tipmsg").abort || e.abort : s.data("tipmsg").err || e.err; s[0].posting = false; a.util.showmsg.call(s, u, m.tiptype, { obj: s, type: 3, sweep: m.tipSweep }, "alwaysshow"); s[0].status = "normal"; s[0].ajax = null } }) } else { if (!m.postonce) { s[0].status = "normal" } return m.callback && m.callback(s) } } return false }, resetForm: function () { var k = this; k.each(function () { this.reset(); this.status = "normal" }); k.find(".Validform_right").text(""); k.find(".passwordStrength").children().removeClass("bgStrength"); k.find(".Validform_checktip").removeClass("Validform_wrong Validform_right Validform_loading"); k.find(".Validform_error").removeClass("Validform_error"); k.find("[datatype]").removeData("cked").removeData("dataIgnore"); k.eq(0).find("input:first").focus() }, abort: function () { if (this.ajax) { this.ajax.abort() } } }; d.Datatype = a.util.dataType; a.prototype = { dataType: a.util.dataType, eq: function (l) { var k = this; if (l >= k.forms.length) { return null } if (!(l in k.objects)) { k.objects[l] = new a(d(k.forms[l]).get(), k.settings, true) } return k.objects[l] }, resetStatus: function () { var k = this; d(k.forms).each(function () { this.status = "normal" }); return this }, setStatus: function (k) { var l = this; d(l.forms).each(function () { this.status = k || "posting" }) }, getStatus: function () { var l = this; var k = d(l.forms)[0].status; return k }, ignore: function (k) { var l = this; d(l.forms).find(k).each(function () { d(this).data("dataIgnore", "dataIgnore").removeClass("Validform_error") }) }, unignore: function (k) { var l = this; d(l.forms).find(k).each(function () { d(this).removeData("dataIgnore") }) }, addRule: function (n) { var m = this; var n = n || []; for (var l in n) { var p = d(m.forms).find(n[l].ele); for (var k in n[l]) { k !== "ele" && p.attr(k, n[l][k]) } } }, ajaxPost: function (k, l) { var m = this; if (m.settings.tiptype == 1 || m.settings.tiptype == 2) { c() } a.util.submitForm.call(d(m.forms[0]), m.settings, k, "ajaxPost", l) }, submitForm: function (k) { var m = this; var l = a.util.submitForm.call(d(m.forms[0]), m.settings, k); l === b && (l = true); if (l === true) { m.forms[0].submit() } }, resetForm: function () { var k = this; a.util.resetForm.call(d(k.forms)) }, abort: function () { var k = this; d(k.forms).each(function () { a.util.abort.call(this) }) } }; d.fn.Validform = function (k) { return new a(this, k) }; function h(n, m) { var l = (d(window).width() - n.outerWidth()) / 2, k = (d(window).height() - n.outerHeight()) / 2, k = (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + (k > 0 ? k : 0); n.css({ left: l }).animate({ top: k }, { duration: m, queue: false }) } function c() { if (d("#Validform_msg").length !== 0) { return false } j = d('<div id="Validform_msg"><div class="Validform_title">' + e.tit + '<a class="Validform_close" href="javascript:void(0);">&chi;</a></div><div class="Validform_info"></div><div class="iframe"><iframe frameborder="0" scrolling="no" height="100%" width="100%"></iframe></div></div>').appendTo("body"); j.find("a.Validform_close").click(function () { j.hide(); i = true; if (g) { g.focus().addClass("Validform_error") } return false }).focus(function () { this.blur() }); d(window).bind("scroll resize", function () { !i && h(j, 400) }) } d.Showmsg = function (k) { c(); a.util.showmsg.call(f, k, 1, {}) }; d.Hidemsg = function () { j.hide(); i = true } })(jQuery, window);
//传入自定义datatype类型【方式一】;
$.extend($.Datatype, {
    "mobile": /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/,  //手机验证
    "tel": /^\d{3,4}-?\d{7,9}$/,                        //电话验证
    "verifycode": /^\d{6}$/,                        //6位验证码
    "u5-16": /^[a-zA-Z][a-zA-Z0-9_]{4,16}$/,            //由字母开头,5-16(字母、数字、下划线)
    "p5-16": /^[a-zA-Z0-9_]{5,16}$/,                    //5-16个字母、数字和下划线组成的字符
    "z2-4": /^[\u4E00-\u9FA5\uf900-\ufa2d]{2,4}$/

});
