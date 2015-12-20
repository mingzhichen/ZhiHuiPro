function ShowPre(o){
	var that= this;
	this.box = $("#"+o["box"]);
	this.btnP = $("#"+o.Pre);
	this.btnN= $("#"+o.Next);
	this.v = o.v||1;
	this.c = 0;
	var li_node = "li";
	this.loop = o.loop||false;
	
    //全屏居中设置
	var vwidth= screen.width;
	$(".indexBanner_num").css("width",vwidth);
	var preheight= $(".banner_index .btn, .banner_pro .bBtn").height();
	$(".banner_index .btn, .banner_pro .bBtn").css("margin-top",-preheight/2);	
	
	//循环生成dom
	if(this.loop){
		this.li =  this.box.find(li_node);
		this.box.append(this.li.eq(0).clone(true));
	};
	this.li = this.box.find(li_node);
	this.l = this.li.length;

	//滑动条件不成立
	if(this.l<=this.v){
		this.btnP.hide();
		this.btnN.hide();
	};
	this.deInit = true;
	this.w = this.li.outerWidth(true);
	this.box.width(this.w*this.l);
	this.maxL = this.l - this.v;

	
	//生成状态图标
	this.numIco = null;
	if(o.numIco){
		this.numIco  = $("#"+o.numIco);
		var numHtml = "";
		numL = this.loop?(this.l-1):this.l;
		for(var i = 0;i<numL;i++){
				numHtml+="<a href='javascript:void(0);'>"+i+"</a>";
		};
		this.numIco.html(numHtml);
		this.numIcoLi = this.numIco.find("a");
		this.numIcoLi.bind("click",function(){
			if(that.c==$(this).html())return false;
			that.c=$(this).html();
			that.move();
		});
	};

	//滑动点击操作(循环or不循环)
	if(this.loop){
		this.btnP.bind("click",function(){
			if(that.c<=0){
				that.c = that.l-1;
				that.box.css({left:-that.c*that.w});		
			};
			that.c --;
			that.move(1);
		});
		this.btnN.bind("click",function(){
			if(that.c>=(that.l-1)){
				that.box.css({left:0});		
				that.c = 0;
			};
			that.c++;
			that.move(1);
		});
	}else{
		this.btnP.bind("click",function(){
			that.c> 0? that.c--:that.c=that.maxL ;
			that.move(1);
		});
		this.btnN.bind("click",function(){
			that.c<that.maxL ?that.c++:that.c=0;
			that.move(1);
		});
	};
	that.timer = null;
	if(o.auto){
		that.box.bind("mouseover",function(){
			clearInterval(that.timer);
		});
		that.box.bind("mouseleave",function(){
			that.autoPlay();
		});
		that.autoPlay();
		
	};
	this.move();
}

ShowPre.prototype = {
	move:function(test){ //滑动方法
		var that = this;
		var pos = this.c*this.w;
		//document.title = (test&&that.timer);
		if(test&&that.timer){
			clearInterval(that.timer);
		};
		//当前序号图标
		if(that.numIco){ 
			that.numIcoLi.removeClass("on");
			var numC = that.c;
			if(that.loop&&(that.c==(this.l-1))){
				numC= 0;	
			};
			that.numIcoLi.eq(numC).addClass("on");
		};

		this.box.stop();
		this.box.animate({left:-pos},function(){
			if(test&&that.auto){
				that.autoPlay();
			};
			if(that.loop&&that.c==that.maxL){
				that.c = 0;
				that.box.css({left:0})
			};
		});
	},
	
	autoPlay:function(){ //自动播放方法
		var that =this;
		that.timer = setInterval(function(){
			that.c<that.maxL?that.c++:that.c=0;
			that.move();
		},3000);
	}
}

function setLang(theme, style) {
    document.cookie = "theme=" + theme;
    document.cookie = "style=" + style;
    window.location.href = window.location.href;
}
//设为首页
function SetHome(obj, url) {
    try {
        obj.style.behavior = 'url(#default#homepage)';
        obj.setHomePage(url);
    } catch (e) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            } catch (e) {
                alert("抱歉，此操作被浏览器拒绝！\n\n请在浏览器地址栏输入“about:config”并回车然后将[signed.applets.codebase_principal_support]设置为'true'");
            }
        } else {
            alert("抱歉，您所使用的浏览器无法完成此操作。\n\n您需要手动将【" + url + "】设置为首页。");
        }
    }
}
//收藏本站
function AddFavorite(title, url) {
    try {
        window.external.addFavorite(url, title);
    }
    catch (e) {
        try {
            window.sidebar.addPanel(title, url, "");
        }
        catch (e) {
            alert("抱歉，您所使用的浏览器无法完成此操作。\n\n加入收藏失败，请使用Ctrl+D进行添加");
        }
    }
}