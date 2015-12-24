var Dialogs = new Array();
ObjDialog = function () {
    this.Html = null;
    this.Mode = null;
    this.ActivityCode = null;
    this.Parameter = '';
    this.PopupDelayAppear = null;
    this.PopupEffectAppear = null;
    this.PopupAlign = null;
    this.CallOnClosing = null;
    this.DelayAppearOnClosing = null;
    this.SizeFieldOnClosing = null;
    this.FloatingButtonName = null;
    this.FloatingButtonIcon = null;
    this.FloatingButtonBackground = null;
    this.FloatingButtonPosition = null;
    this.FloatingButtonMargin = null;
    this.Shown = false;
    this.ContainerId = false;
};

if (!WebCounter) {
    window.onmousemove = handleMouseMove;

    var WebCounter = {
        LG_Counter: function () {
            this.assignContactID();
            this.SiteID = arguments[0];

            if (!this.CounterCalled) {
                var ajaxCounter = new WebCounter.Ajax.Counter();
                ajaxCounter.jsonData = '{"SiteID":"' + this.SiteID + '","ContactID":"' + this.ContactID + '","URL":"' + this.URL + '","RefferURL":"' + this.RefferURL + '","Resolution":"' + screen.width + 'x' + screen.height + '"}';
                ajaxCounter.send(WebCounter.Util.getAssetHost() + "/CounterService.svc/LG_CounterService");
                this.CounterCalled = true;
            }
        },

        LG_Link: function () {
            var ActivityCode = arguments[0];
            this.assignContactID();
            options["imagesPath"] = WebCounter.Util.getAssetHost() + '/images';
            WebCounter.Overlay.init();
            WebCounter.Dialog.init();
            var ajaxLink = new WebCounter.Ajax.Link();
            ajaxLink.jsonData = '{"SiteID":"' + this.SiteID + '","ContactID":"' + this.ContactID + '","ActivityCode":"' + ActivityCode + '","register":"true"}';
            ajaxLink.send(WebCounter.Util.getAssetHost() + "/CounterService.svc/LG_LinkService");
        },

        LG_Form: function () {
            var ActivityCode = arguments[0];
            var Mode = arguments[1];
            var FromVisit = arguments[2] == undefined ? "" : arguments[2];
            var Through = arguments[3] == undefined ? "" : arguments[3];
            var Period = arguments[4] == undefined ? "" : arguments[4];
            var Parameter = '';
            var ContactCategory = '0';

            var jsonPopupAppear = '';
            var PopupDelayAppear = 0;
            var PopupEffectAppear = 0;
            var PopupAlign = 4;
            var DelayAppearOnClosing = 10;
            var SizeFieldOnClosing = 50;

            var FloatingButtonName = '';
            var FloatingButtonIcon = '';
            var FloatingButtonBackground = 'transparent';
            var FloatingButtonPosition = '0';
            var FloatingButtonMargin = '0';

            var register = true;

            switch (Mode) {
                case "0":
                    if (arguments.length == 7) {
                        Parameter = arguments[5] == undefined ? "" : arguments[5];
                        ContactCategory = arguments[6] == undefined ? "" : arguments[6];
                    }
                    if (arguments.length == 6)
                        Parameter = arguments[5] == undefined ? "" : arguments[5];

                    jsonPopupAppear = '[]';
                    break;
                case "1":
                    if (arguments.length == 5) {
                        Parameter = arguments[2] == undefined ? "" : arguments[2];
                        PopupEffectAppear = arguments[3] == undefined ? "" : arguments[3];
                        PopupAlign = arguments[4] == undefined ? "" : arguments[4];
                    }
                    if (arguments.length == 3)
                        Parameter = arguments[2] == undefined ? "" : arguments[2];

                    jsonPopupAppear += '[{"N":"PopupEffectAppear","V":"' + PopupEffectAppear + '"},{"N":"PopupAlign","V":"' + PopupAlign + '"}]';
                    break;
                case "2":
                    if (arguments.length == 10) {
                        Parameter = arguments[5] == undefined ? "" : arguments[5];
                        ContactCategory = arguments[6] == undefined ? "" : arguments[6];
                        PopupDelayAppear = arguments[7] == undefined ? "" : arguments[7];
                        PopupEffectAppear = arguments[8] == undefined ? "" : arguments[8];
                        PopupAlign = arguments[9] == undefined ? "" : arguments[9];
                    }
                    if (arguments.length == 6) {
                        Parameter = arguments[5] == undefined ? "" : arguments[5];
                    }

                    jsonPopupAppear += '[{"N":"PopupDelayAppear","V":"' + PopupDelayAppear + '"},{"N":"PopupEffectAppear","V":"' + PopupEffectAppear + '"},{"N":"PopupAlign","V":"' + PopupAlign + '"}]';

                    register = false;
                    break;
                case "3":
                    Parameter = arguments[5] == undefined ? "" : arguments[5];
                    ContactCategory = arguments[6] == undefined ? "" : arguments[6];
                    PopupEffectAppear = arguments[7] == undefined ? "" : arguments[7];
                    PopupAlign = arguments[8] == undefined ? "" : arguments[8];
                    FloatingButtonName = arguments[9] == undefined ? "" : arguments[9];
                    FloatingButtonIcon = arguments[10] == undefined ? "" : arguments[10];
                    if (FloatingButtonIcon != "")
                        //FloatingButtonIcon = "http://localhost:52960/files/" + this.SiteID + "/FloatingButtonIcons/" + FloatingButtonIcon;
                    FloatingButtonIcon = "http://admin-demo.leadforce.ru/files/" + this.SiteID + "/FloatingButtonIcons/" + FloatingButtonIcon;
                    FloatingButtonBackground = arguments[11] == undefined ? "" : arguments[11];
                    FloatingButtonPosition = arguments[12] == undefined ? "" : arguments[12];
                    FloatingButtonMargin = arguments[13] == undefined ? "" : arguments[13];

                    jsonPopupAppear += '[{"N":"PopupDelayAppear","V":"' + PopupDelayAppear + '"},{"N":"PopupEffectAppear","V":"' + PopupEffectAppear + '"},{"N":"PopupAlign","V":"' + PopupAlign + '"},{"N":"FloatingButtonName","V":"' + FloatingButtonName + '"},{"N":"FloatingButtonIcon","V":"' + FloatingButtonIcon + '"},{"N":"FloatingButtonBackground","V":"' + FloatingButtonBackground + '"},{"N":"FloatingButtonPosition","V":"' + FloatingButtonPosition + '"},{"N":"FloatingButtonMargin","V":"' + FloatingButtonMargin + '"}]';

                    register = false;
                    break;
                case "4":
                    Parameter = arguments[5] == undefined ? "" : arguments[5];
                    ContactCategory = arguments[6] == undefined ? "" : arguments[6];
                    PopupEffectAppear = arguments[7] == undefined ? "" : arguments[7];
                    PopupAlign = arguments[8] == undefined ? "" : arguments[8];
                    DelayAppearOnClosing = arguments[9] == undefined ? "" : arguments[9];
                    SizeFieldOnClosing = arguments[10] == undefined ? "" : arguments[10];

                    jsonPopupAppear += '[{"N":"PopupEffectAppear","V":"' + PopupEffectAppear + '"},{"N":"PopupAlign","V":"' + PopupAlign + '"},{"N":"DelayAppearOnClosing","V":"' + DelayAppearOnClosing + '"},{"N":"SizeFieldOnClosing","V":"' + SizeFieldOnClosing + '"}]';

                    register = false;
                    break;
            }

            this.assignContactID();
            if (!WebCounter.Form.isInited) {
                options["imagesPath"] = WebCounter.Util.getAssetHost() + '/images';
                WebCounter.Form.init();
                WebCounter.Form.isInited = true;
            }

            options["imagesPath"] = WebCounter.Util.getAssetHost() + '/images';
            WebCounter.Overlay.init();
            WebCounter.Dialog.ActivityCode = ActivityCode;
            WebCounter.Dialog.Parameter = Parameter;
            WebCounter.Dialog.PopupEffectAppear = PopupEffectAppear;
            WebCounter.Dialog.PopupAlign = PopupAlign;
            WebCounter.Dialog.init();

            var ajaxForm = new WebCounter.Ajax.Form();
            ajaxForm.id = "wcform-" + WebCounter.Util.guid();
            ajaxForm.jsonData = '{"SiteID":"' + this.SiteID + '","ContactID":"' + this.ContactID + '","ActivityCode":"' + ActivityCode + '","Mode":"' + Mode + '","FromVisit":"' + FromVisit + '","Through":"' + Through + '","Period":"' + Period + '","Parameter":"' + Parameter + '","ContactCategory":"' + ContactCategory + '","PopupAppear":' + jsonPopupAppear + ',"register":"' + register + '"}';
            ajaxForm.send(WebCounter.Util.getAssetHost() + "/CounterService.svc/LG_FormService");
            if (Mode == 0) {
                ActivityCode = ActivityCode.replace(/\[/g, '\\[').replace(/\]/g, '\\]');
                var regex = new RegExp("_lfq.push(.*)([\"|']WebCounter.LG_Form[\"|'])(.*)([\"|']" + ActivityCode + "[\"|'])", "gi");
                var scripts = document.getElementsByTagName('script');
                for (var i = 0; i < scripts.length; i++) {
                    if (regex.test(scripts[i].innerHTML)) {
                        var div = document.createElement('div');
                        div.id = ajaxForm.id;
                        div.className = "wcform";
                        scripts[i].parentNode.insertBefore(div, scripts[i]);
                    }
                }
            }
            
            if (Mode != 0) {
                var div = document.createElement('div');
                div.id = ajaxForm.id;
                div.className = "wcdialog";
                if (Mode == 1)
                    document.getElementById('WebCounter-dialog-container').appendChild(div);
                else
                    document.getElementById('WebCounter-dialog-container').parentNode.insertBefore(div, document.getElementById('WebCounter-dialog-container'));
            }
        },

        LG_FormFloatingButton: function () {
            var ActivityCode = arguments[0];
            var Parameter = arguments[1] == undefined ? "" : arguments[1];

            for (var i = 0; i < Dialogs.length; i++) {
                if (Dialogs[i].Mode == 3 && Dialogs[i].ActivityCode == ActivityCode && !WebCounter.Dialog.IsActive) {
                    //document.getElementById("WebCounter-dialog-container").innerHTML = Dialogs[i].Html;
                    document.getElementById("WebCounter-dialog-close").setAttribute('onclick', 'WebCounter.LG_FormServiceCancel(\'' + ActivityCode + '\', \'' + Parameter + '\'); WebCounter.Dialog.hide()');
                    WebCounter.Dialog.PopupEffectAppear = Dialogs[i].PopupEffectAppear;
                    WebCounter.Dialog.PopupAlign = Dialogs[i].PopupAlign;
                    //WebCounter.Dialog.show(Dialogs[i].ContainerId);
                    WebCounter.Dialog.preshow(Dialogs[i].ContainerId);
                    WebCounter.LG_FormServiceOpen(Dialogs[i].ActivityCode, Parameter);
                }
            }
        },

        LG_FormServiceResult: function () {
            var ActivityCode = arguments[0];
            var Parameter = arguments[1] == undefined ? "" : arguments[1];
            var ajaxFormServiceResult = new WebCounter.Ajax.FormServiceResult();
            var form = document.getElementById("CounterServiceForm-" + ActivityCode);
            var errorCount = 0;
            var inputs = form.getElementsByTagName("input");
            var i;
            for (i = 0; i < inputs.length; i++) {
                if (inputs[i].value == "" && inputs[i].getAttribute("rel") == "required") {
                    inputs[i].parentNode.style.backgroundColor = "#ffebe8";
                    errorCount++;
                } else inputs[i].parentNode.style.backgroundColor = "#fff";
            }

            var textareas = form.getElementsByTagName("textarea");
            for (i = 0; i < textareas.length; i++) {
                if (textareas[i].value == "" && textareas[i].getAttribute("rel") == "required") {
                    textareas[i].parentNode.style.backgroundColor = "#ffebe8";
                    errorCount++;
                } else textareas[i].parentNode.style.backgroundColor = "#fff";
            }

            var selects = form.getElementsByTagName("select");
            for (i = 0; i < selects.length; i++) {
                if (selects[i].options[selects[i].selectedIndex].className == "" && selects[i].options[selects[i].selectedIndex].getAttribute("rel") == "required") {
                    selects[i].parentNode.style.backgroundColor = "#ffebe8";
                    errorCount++;
                } else selects[i].parentNode.style.backgroundColor = "#fff";
            }


            if (errorCount > 0) {
                if (document.getElementById("LFErrorMessage")) {
                    if (document.getElementById("LFErrorMessageTextBlock")) {
                        document.getElementById("LFErrorMessageTextBlock").innerHTML = document.getElementById("LFErrorMessage").innerHTML;
                        document.getElementById("LFErrorMessageTextBlock").style.display = 'block';
                    } else {
                        alert(document.getElementById("LFErrorMessage").innerHTML);
                    }
                }
                return;
            } else if (document.getElementById("LFErrorMessageTextBlock")) {
                document.getElementById("LFErrorMessageTextBlock").style.display = 'none';
            }


            var json = '{';
            json = json + '"SiteID":"' + this.SiteID + '","ContactID":"' + this.ContactID + '","ActivityCode":"' + ActivityCode + '","Parameter":"' + Parameter + '","register":"true"';
            json = json + ',';
            if (form.getElementsByTagName("input").length > 0 || form.getElementsByTagName("select").length > 0) {
                json = json + '"FormData":[';
                for (i = 0; i < form.getElementsByTagName("input").length; i++) {
                    json = json + '{ "N":"' + form.getElementsByTagName("input")[i].className + '", "V":"' + form.getElementsByTagName("input")[i].value + '" },';
                }
                for (i = 0; i < form.getElementsByTagName("select").length; i++) {
                    //if (form.getElementsByTagName("select")[i].selectedIndex != 0)
                    if (form.getElementsByTagName("select")[i].options[form.getElementsByTagName("select")[i].selectedIndex].value != '')
                        json = json + '{ "N":"' + form.getElementsByTagName("select")[i].options[form.getElementsByTagName("select")[i].selectedIndex].className + '", "V":"' + form.getElementsByTagName("select")[i].options[form.getElementsByTagName("select")[i].selectedIndex].value + '" },';
                }
                for (i = 0; i < form.getElementsByTagName("textarea").length; i++) {
                    json = json + '{ "N":"' + form.getElementsByTagName("textarea")[i].className + '", "V":"' + form.getElementsByTagName("textarea")[i].value.replace(/\n/g, "<br />") + '" },';
                }
                json = json.substring(0, json.length - 1);
                json = json + ']';
            }
            json = json + '}';
            ajaxFormServiceResult.jsonData = json;
            ajaxFormServiceResult.send(WebCounter.Util.getAssetHost() + "/CounterService.svc/LG_FormServiceResult");
        },

        LG_FormServiceCancel: function () {
            if (document.getElementById("WebCounter-SuccessMessage") == undefined) {
                var ActivityCode = arguments[0];
                var Parameter = arguments[1] == undefined ? "" : arguments[1];
                var ajaxFormServiceCancel = new WebCounter.Ajax.FormServiceCancel();
                ajaxFormServiceCancel.jsonData = '{"SiteID":"' + this.SiteID + '","ContactID":"' + this.ContactID + '","ActivityCode":"' + ActivityCode + '","Parameter":"' + Parameter + '"}';
                ajaxFormServiceCancel.send(WebCounter.Util.getAssetHost() + "/CounterService.svc/LG_FormServiceCancel");   
            }
        },

        LG_FormServiceOpen: function () {
            var ActivityCode = arguments[0];
            var Parameter = arguments[1] == undefined ? "" : arguments[1];
            var ajaxFormServiceOpen = new WebCounter.Ajax.FormServiceOpen();
            ajaxFormServiceOpen.jsonData = '{"SiteID":"' + this.SiteID + '","ContactID":"' + this.ContactID + '","ActivityCode":"' + ActivityCode + '","Parameter":"' + Parameter + '"}';
            ajaxFormServiceOpen.send(WebCounter.Util.getAssetHost() + "/CounterService.svc/LG_FormServiceOpen");
        },

        URL: document.URL,

        RefferURL: document.referrer,

        SiteID: null,

        ContactID: null,

        assignContactID: function () {
            this.ContactID = WebCounter.Cookie.getCookie("WebCounterUserID");
            if (this.ContactID == null) {
                this.ContactID = WebCounter.Util.guid();
                WebCounter.Cookie.setCookie("WebCounterUserID", this.ContactID, "365", "/");
            }

            if (window.location.hash.toString().indexOf("#lg:") === 0) {
                var actionLinkId = window.location.hash.toString().substring(4);

                this.CounterCalled = true;
                var ajaxLinkProcessing = new WebCounter.Ajax.LinkProcessing();
                //ajaxLinkProcessing.jsonData = '{"SiteID":"' + hash[0] + '","ContactID":"' + this.ContactID + '","RefferID":"' + hash[1] + '","ActionLinkID":"' + hash[2] + '","Resolution":"' + screen.width + 'x' + screen.height + '"}';
                ajaxLinkProcessing.jsonData = '{"ContactID":"' + this.ContactID + '","ActionLinkID":"' + actionLinkId + '","Resolution":"' + screen.width + 'x' + screen.height + '"}';
                ajaxLinkProcessing.send(WebCounter.Util.getAssetHost() + "/CounterService.svc/LG_LinkProcessing");
            }
        },

        CounterCalled: false,

        Timer: 0,

        TimerFunc: function () {
            if (Dialogs.length > 0) {
                for (var i = 0; i < Dialogs.length; i++) {
                    if (Dialogs[i].Mode == 2 && Dialogs[i].PopupDelayAppear <= WebCounter.Timer && !Dialogs[i].Shown && !WebCounter.Dialog.IsActive) {
                        //document.getElementById("WebCounter-dialog-container").innerHTML = Dialogs[i].Html;
                        document.getElementById("WebCounter-dialog-close").setAttribute('onclick', 'WebCounter.LG_FormServiceCancel(\'' + Dialogs[i].ActivityCode + '\', \'' + Dialogs[i].Parameter + '\'); WebCounter.Dialog.hide()');
                        WebCounter.Dialog.PopupEffectAppear = Dialogs[i].PopupEffectAppear;
                        WebCounter.Dialog.PopupAlign = Dialogs[i].PopupAlign;
                        //WebCounter.Dialog.show(Dialogs[i].ContainerId);
                        WebCounter.Dialog.preshow(Dialogs[i].ContainerId);
                        Dialogs[i].Shown = true;
                        WebCounter.LG_FormServiceOpen(Dialogs[i].ActivityCode, Dialogs[i].Parameter);
                    }
                }
            }
            WebCounter.Timer++;
        }
    };

    var options = {
        overlayColor: '#000', // HEX color
        overlayOpacity: 0.7, // digit from 0 to 1
        overlayClose: false, // true or false
        zIndex: 1000, // digit more 1
        dialogZIndex: 1008,
        dialogCloseZIndex: 1009,
        overlayZIndex: 1007,
        imagesPath: null
    };

    setInterval(WebCounter.TimerFunc, 1000);
}

function handleMouseMove(e) {
    var posx = 0;
    var posy = 0;
    if (!e) var e = window.event;
    if (e.pageX || e.pageY) {
        posx = e.pageX;
        posy = e.pageY;
    }
    else if (e.clientX || e.clientY) {
        posx = e.clientX + document.body.scrollLeft
			+ document.documentElement.scrollLeft;
        posy = e.clientY + document.body.scrollTop
			+ document.documentElement.scrollTop;
    }

    if (Dialogs.length > 0) {
        for (var i = 0; i < Dialogs.length; i++) {
            if (Dialogs[i].Mode == 4 && Dialogs[i].CallOnClosing == 'true' && Dialogs[i].DelayAppearOnClosing <= WebCounter.Timer && Dialogs[i].SizeFieldOnClosing >= posy && !Dialogs[i].Shown && !WebCounter.Dialog.IsActive) {
                //document.getElementById("WebCounter-dialog-container").innerHTML = Dialogs[i].Html;
                document.getElementById("WebCounter-dialog-close").setAttribute('onclick', 'WebCounter.LG_FormServiceCancel(\'' + Dialogs[i].ActivityCode + '\', \'' + Dialogs[i].Parameter + '\'); WebCounter.Dialog.hide()');
                WebCounter.Dialog.PopupEffectAppear = Dialogs[i].PopupEffectAppear;
                WebCounter.Dialog.PopupAlign = Dialogs[i].PopupAlign;
                //WebCounter.Dialog.show(Dialogs[i].ContainerId);
                WebCounter.Dialog.preshow(Dialogs[i].ContainerId);
                Dialogs[i].Shown = true;
                WebCounter.LG_FormServiceOpen(Dialogs[i].ActivityCode, Dialogs[i].Parameter);
                break;
            }
        }
    }
}

WebCounter.Overlay = {
    id: 'WebCounter-overlay',

    init: function() {
        if (document.getElementById(this.id) == null) {
            var overlay = document.createElement('div');
            overlay.setAttribute('id', this.id);
            if (options['overlayClose'] == true)
                overlay.setAttribute('onclick', 'WebCounter.Dialog.hide()');
            document.body.insertBefore(overlay, document.body.firstChild);
            WebCounter.Util.includeCss(WebCounter.Util.render(this.css_template, options));
        }
    },

    show: function() {
        document.getElementById(this.id).style.display = 'block';
    },

    hide: function() {
        document.getElementById(this.id).style.display = 'none';
    },

    css_template: "\
		#WebCounter-overlay { background-color: #{overlayColor}; position: fixed; top: 0; left: 0; filter: alpha(opacity='#{alphaOverlayOpacity}'); opacity: #{overlayOpacity}; z-index: #{overlayZIndex}; width: 100%; height: 100%; display: none; }\
	"
};


WebCounter.Form = {
    isInited: false,

    init: function() {
        WebCounter.Util.includeCss(WebCounter.Util.render(this.css_template, options));
    },

    css_template: "\
.wcform iframe { height:1px; width: 100%; overflow: hidden; }\
.wcfloatingbutton { display: block; position: fixed; font-family: Tahoma, Geneva, sans-serif; font-size: 14px; line-height: 16px; z-index: 99; padding: 5px 5px 9px 5px; text-decoration: none; } \
.wcfloatingbutton img { margin-right: 5px; vertical-align: bottom; border: 0; }\
.wcfloatingbutton-left { -o-transform: rotate(270deg); -khtml-transform: rotate(270deg); -webkit-transform: rotate(270deg); -moz-transform: rotate(270deg); -ms-transform: rotate(270deg); }\
.wcfloatingbutton-right { -o-transform: rotate(90deg); -khtml-transform: rotate(90deg); -webkit-transform: rotate(90deg); -moz-transform: rotate(90deg); -ms-transform: rotate(90deg); }\
#WebCounter-SuccessMessage { padding-top: 18px!important; text-align:center; }\
	",

    css_template_old: "\
        .wcform { font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 16px; position: relative; padding-bottom: 38px; }\
        .wcform * { margin: 0; padding: 0; outline: none; }\
        .wcform .field-wrapper { text-align: center; width: 100%; display: inline-block; background-color: #fff; border: 1px solid #999; margin-bottom: 5px; border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px; }\
        .wcform label { width: 100%; display: inline-block; color: #666; vertical-align: top; padding: 6px 0 3px 0; }\
        .wcform input { width: 100%; height: 15px; background-color: transparent; border: 0; font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 15px; margin: 5px; }\
        .wcform textarea { width: 100%; height: 43px; background-color: transparent; border: 0; font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 14px; margin: 5px; resize: none; }\
        .wcform select { width: 100%; height: 27px; background-color: transparent; border:0; font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 27px; padding: 5px; border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px; }\
.wcform .hor-left { width: 25%; display: table-cell; vertical-align: top; } .wcform .hor-left label { width: 49%; float: left; } .wcform .hor-left input { width: 97%; float: left; } .wcform .hor-left select { width: 100%; float: left; } .wcform .hor-left textarea { width: 97%; float: left; } \
.wcform .hor-top { width: 25%; display: table-cell; padding: 2px; vertical-align: top; } .wcform .hor-top label { width: 99%; float: left; } .wcform .hor-top input { width: 97%; float: left; } .wcform .hor-top select { width: 100%; float: left; } .wcform .hor-top textarea { width: 97%; float: left; } \
.wcform .ver-left { width: 100%; display: table; margin-bottom: 5px; } .wcform .ver-left label { width: 50%; display: table-cell; } .wcform .ver-left input { width: 98%; } .wcform .ver-left select { width: 100%; } .wcform .ver-left textarea { width: 98%; } \
.wcform .ver-top { width: 100%; } .wcform .ver-top label { width: 99%; } .wcform .ver-top input { width: 99%; } .wcform .ver-top select { width: 100%; } .wcform .ver-top textarea { width: 99%; } \
.wcform .hor-none { width: 25%; display: table-cell; padding: 2px; vertical-align: top; } .wcform .hor-none input { width: 99%; float: left; } .wcform .hor-none select { width: 100%; float: left; } .wcform .hor-none textarea { width: 99%; float: left; } \
.wcform .ver-none { width: 100%; } .wcform .ver-none input { width: 99%; } .wcform .ver-none select { width: 100%; } .wcform .ver-none textarea { width: 99%; } \
.wcform .ver-left .field-wrapper { width: 50%; display: table-cell; } \
.wcform .hor-left .field-wrapper { width: 50%; } \
        .wcform a { display: inline-block; font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 14px; color: #333; font-weight: bold; margin: 5px 0; }\
        .wcform .btn { height: 27px; display: inline-block; font-size: 13px; font-weight: normal; line-height: 27px; text-decoration: none!important; text-align: center; color: #fff; background-color: #64c400; -webkit-border-radius: 16px; -moz-border-radius: 16px; border-radius: 16px; }\
        .wcform .btn em { width: 13px; height: 27px; display: inline-block; float: left; background: url(#{imagesPath}/sprite.png) 0 -97px no-repeat; }\
        .wcform .btn span { height: 27px; display: inline-block; float: left; background: url(#{imagesPath}/sprite.png) right -70px no-repeat; padding: 0 26px 0 12px; }\
        .wcform a:hover.btn em { background-position: 0 -151px; }\
        .wcform a:hover.btn span { background-position: right -124px; }\
        .wcform a:active.btn em { background-position: 0 -205px; }\
        .wcform a:active.btn span { background-position: right -178px; }\
        .wcform .hide { display: none!important; }\
.wcform .clear { clear: both; height: 0; line-height: 0; font-size: 0 }\
.wcform .clearfix { zoom: 1; }\
.wcform .clearfix:after { content: ' '; clear: both; display: block; width: 0; height: 0; overflow: hidden; font-size: 0; }\
.wcfloatingbutton { display: block; position: fixed; font-family: Tahoma, Geneva, sans-serif; font-size: 14px; line-height: 16px; z-index: 99; padding: 5px 5px 9px 5px; text-decoration: none; } \
.wcfloatingbutton img { margin-right: 5px; vertical-align: bottom; border: 0; }\
.wcfloatingbutton-left { -o-transform: rotate(270deg); -khtml-transform: rotate(270deg); -webkit-transform: rotate(270deg); -moz-transform: rotate(270deg); -ms-transform: rotate(270deg); }\
.wcfloatingbutton-right { -o-transform: rotate(90deg); -khtml-transform: rotate(90deg); -webkit-transform: rotate(90deg); -moz-transform: rotate(90deg); -ms-transform: rotate(90deg); }\
	",

    toggleForm: function(className) {
        var div = document.getElementsByClassName(className)[0];
        if (div.className.indexOf('hide') == -1)
            div.className = div.className + ' hide';
        else
            div.className = div.className.replace(' hide', '');
    }
};

function slide(effect, needMargin, currentMargin, delay, step) {
    var intNeedMargin = parseInt(needMargin.replace("px", ""));
    var intCurrentMargin = parseInt(currentMargin.replace("px", ""));
    var margin;

    switch (effect) {
        case 'slideUp':
            margin = parseInt((document.getElementById("WebCounter-dialog").style.marginTop).replace("px", ""));
            if (-(-intCurrentMargin - step) < intNeedMargin) {
                document.getElementById("WebCounter-dialog").style.marginTop = (margin + step) + 'px';
                currentMargin = document.getElementById("WebCounter-dialog").style.marginTop;
                setTimeout("slide('" + effect + "', '" + needMargin + "', '" + currentMargin + "', " + delay + ", " + step + ")", delay);
            } else {
                document.getElementById("WebCounter-dialog").style.marginTop = needMargin;
            }
            break;
        case 'slideRight':
            margin = parseInt((document.getElementById("WebCounter-dialog").style.marginLeft).replace("px", ""));
            if (-(-intCurrentMargin + step) > intNeedMargin) {
                document.getElementById("WebCounter-dialog").style.marginLeft = (margin - step) + 'px';
                currentMargin = document.getElementById("WebCounter-dialog").style.marginLeft;
                setTimeout("slide('" + effect + "', '" + needMargin + "', '" + currentMargin + "', " + delay + ", " + step + ")", delay);
            } else {
                document.getElementById("WebCounter-dialog").style.marginLeft = needMargin;
            }
            break;
        case 'slideDown':
            margin = parseInt((document.getElementById("WebCounter-dialog").style.marginTop).replace("px", ""));
            if (-(-intCurrentMargin + step) > intNeedMargin) {
                document.getElementById("WebCounter-dialog").style.marginTop = (margin - step) + 'px';
                currentMargin = document.getElementById("WebCounter-dialog").style.marginTop;
                setTimeout("slide('" + effect + "', '" + needMargin + "', '" + currentMargin + "', " + delay + ", " + step + ")", delay);
            } else {
                document.getElementById("WebCounter-dialog").style.marginTop = needMargin;
            }
            break;
        case 'slideLeft':
            margin = parseInt((document.getElementById("WebCounter-dialog").style.marginLeft).replace("px", ""));
            if (-(-intCurrentMargin - step) < intNeedMargin) {
                document.getElementById("WebCounter-dialog").style.marginLeft = (margin + step) + 'px';
                currentMargin = document.getElementById("WebCounter-dialog").style.marginLeft;
                setTimeout("slide('" + effect + "', '" + needMargin + "', '" + currentMargin + "', " + delay + ", " + step + ")", delay);
            } else {
                document.getElementById("WebCounter-dialog").style.marginLeft = needMargin;
            }
            break;
    }
}

function zoom(needWidth, needHeight, currentWidth, currentHeight, stepWidth, stepHeight) {
    if (needWidth > (currentWidth + stepWidth)) {
        currentWidth = currentWidth + stepWidth;
        document.getElementById("WebCounter-dialog").style.width = currentWidth + 'px';
    }
    else {
        currentWidth = needWidth;
        document.getElementById("WebCounter-dialog").style.width = needWidth + 'px';
    }

    if (needHeight > (currentHeight + stepHeight)) {
        currentHeight = currentHeight + stepHeight;
        document.getElementById("WebCounter-dialog").style.height = currentHeight + 'px';
    }
    else {
        currentHeight = needHeight;
        document.getElementById("WebCounter-dialog").style.height = needHeight + 'px';
    }

    if (currentWidth != needWidth || currentHeight != needHeight)
        setTimeout("zoom(" + needWidth + ", " + needHeight + ", " + currentWidth + ", " + currentHeight + ", " + stepWidth + ", " + stepHeight + ")", 10);
    else
        document.getElementById("WebCounter-dialog").style.overflow = "visible";
}

WebCounter.Dialog = {
    id: 'WebCounter-dialog',

    IsActive: false,

    ActivityCode: null,

    Parameter: null,

    PopupEffectAppear: 0,

    PopupAlign: 4,

    ContainerId: null,

    init: function () {
        if (document.getElementById(this.id) == null) {
            var html = '<a href="javascript:;" id="WebCounter-dialog-close" onclick="WebCounter.LG_FormServiceCancel(\'' + this.ActivityCode + '\', \'' + this.Parameter + '\'); WebCounter.Dialog.hide()"><span>close</span></a>'
                + '<div id="WebCounter-dialog-loader">Подождите...</div><div id="WebCounter-dialog-container" style="clear:both;"></div>';
            var dialog = document.createElement('div');
            dialog.setAttribute('id', this.id);
            dialog.innerHTML = html;
            document.body.insertBefore(dialog, document.body.firstChild);

            WebCounter.Util.includeCss(WebCounter.Util.render(this.css_template, options));
        } else
            document.getElementById('WebCounter-dialog-close').setAttribute('onclick', 'WebCounter.LG_FormServiceCancel(\'' + this.ActivityCode + '\', \'' + this.Parameter + '\'); WebCounter.Dialog.hide()');
    },

    repaint: function (iframeContainerId) {
        var formWidth = document.getElementById(iframeContainerId).getElementsByTagName("iframe")[0].offsetWidth;
        var formHeight = document.getElementById(iframeContainerId).getElementsByTagName("iframe")[0].offsetHeight;

        document.getElementById(this.id).style.width = formWidth + 'px';
        if (formHeight == 10)
            document.getElementById(this.id).style.height = '50px';
        else
            document.getElementById(this.id).style.height = formHeight + 'px';

        switch (this.PopupAlign) {
            case "1":
                document.getElementById("WebCounter-dialog").style.left = '50%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth / 2) + 'px';
                break;
            case "2":
                document.getElementById("WebCounter-dialog").style.left = '100%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth + 31) + 'px';
                break;
            case "3":
                document.getElementById("WebCounter-dialog").style.top = '50%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight / 2) + 'px';
                break;
            case "4":
                document.getElementById("WebCounter-dialog").style.top = '50%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight / 2) + 'px';
                document.getElementById("WebCounter-dialog").style.left = '50%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth / 2) + 'px';
                break;
            case "5":
                document.getElementById("WebCounter-dialog").style.top = '50%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight / 2) + 'px';
                document.getElementById("WebCounter-dialog").style.left = '100%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth + 31) + 'px';
                break;
            case "6":
                document.getElementById("WebCounter-dialog").style.top = '100%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight + 21) + 'px';
                break;
            case "7":
                document.getElementById("WebCounter-dialog").style.top = '100%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight + 21) + 'px';
                document.getElementById("WebCounter-dialog").style.left = '50%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth / 2) + 'px';
                break;
            case "8":
                document.getElementById("WebCounter-dialog").style.top = '100%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight + 21) + 'px';
                document.getElementById("WebCounter-dialog").style.left = '100%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth + 31) + 'px';
                break;
        }
    },

    preshow: function (iframeContainerId) {
        this.ContainerId = iframeContainerId;

        WebCounter.Overlay.show();

        document.getElementById('WebCounter-dialog-loader').style.display = 'block';
        document.getElementById(this.id).style.display = 'block';
        document.getElementById(this.id).style.visibility = 'hidden';

        var divs = document.getElementById(this.id).getElementsByTagName('div');
        for (var i = 0; i < divs.length; i++) {
            if (divs[i].className == 'wcdialog')
                divs[i].style.display = 'none';
        }

        var formWidth;
        var formHeight;

        if (this.ContainerId == null) {
            formWidth = 596;
            formHeight = 50;
            document.getElementById("WebCounter-dialog").style.width = formWidth + 'px';
            document.getElementById("WebCounter-dialog").style.height = formHeight + 'px';
        }
        else {
            document.getElementById(iframeContainerId).style.display = 'block';

            //document.getElementById(this.id).style.visibility = 'hidden';

            formWidth = document.getElementById(iframeContainerId).getElementsByTagName("iframe")[0].offsetWidth;
            formHeight = document.getElementById(iframeContainerId).getElementsByTagName("iframe")[0].offsetHeight;

            if (formWidth == 0)
                document.getElementById("WebCounter-dialog").style.width = '596px';
            else
                document.getElementById("WebCounter-dialog").style.width = formWidth + 'px';

            if (formHeight == 0 || formHeight == 1)
                document.getElementById("WebCounter-dialog").style.height = '50px';
            else
                document.getElementById("WebCounter-dialog").style.height = formHeight + 'px';
        }

        document.getElementById("WebCounter-dialog").style.marginLeft = '0';
        document.getElementById("WebCounter-dialog").style.marginTop = '11px';
        document.getElementById("WebCounter-dialog").style.left = '0';
        document.getElementById("WebCounter-dialog").style.top = '0';

        this.IsActive = true;
    },

    show: function (iframeContainerId) {
        //document.getElementById(this.id).style.visibility = 'visible';
        /*if (this.IsActive)
        return;*/

        this.ContainerId = iframeContainerId;

        WebCounter.Overlay.show();

        document.getElementById('WebCounter-dialog-loader').style.display = 'block';
        document.getElementById(this.id).style.display = 'block';

        var divs = document.getElementById(this.id).getElementsByTagName('div');
        for (var i = 0; i < divs.length; i++) {
            if (divs[i].className == 'wcdialog')
                divs[i].style.display = 'none';
        }

        var formWidth;
        var formHeight;

        if (this.ContainerId == null) {
            formWidth = 596;
            formHeight = 50;
            document.getElementById("WebCounter-dialog").style.width = formWidth + 'px';
            document.getElementById("WebCounter-dialog").style.height = formHeight + 'px';
        }
        else {
            document.getElementById(iframeContainerId).style.display = 'block';

            //document.getElementById(this.id).style.visibility = 'hidden';

            formWidth = document.getElementById(iframeContainerId).getElementsByTagName("iframe")[0].offsetWidth;
            formHeight = document.getElementById(iframeContainerId).getElementsByTagName("iframe")[0].offsetHeight;

            if (formWidth == 0)
                document.getElementById("WebCounter-dialog").style.width = '596px';
            else
                document.getElementById("WebCounter-dialog").style.width = formWidth + 'px';

            if (formHeight == 0 || formHeight == 1)
                document.getElementById("WebCounter-dialog").style.height = '50px';
            else
                document.getElementById("WebCounter-dialog").style.height = formHeight + 'px';
        }

        document.getElementById("WebCounter-dialog").style.marginLeft = '0';
        document.getElementById("WebCounter-dialog").style.marginTop = '11px';
        document.getElementById("WebCounter-dialog").style.left = '0';
        document.getElementById("WebCounter-dialog").style.top = '0';

        switch (this.PopupAlign.toString()) {
            case "1":
                document.getElementById("WebCounter-dialog").style.left = '50%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth / 2) + 'px';
                break;
            case "2":
                document.getElementById("WebCounter-dialog").style.left = '100%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth + 31) + 'px';
                break;
            case "3":
                document.getElementById("WebCounter-dialog").style.top = '50%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight / 2) + 'px';
                break;
            case "4":
                
                document.getElementById("WebCounter-dialog").style.top = '50%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight / 2) + 'px';
                document.getElementById("WebCounter-dialog").style.left = '50%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth / 2) + 'px';
                break;
            case "5":
                document.getElementById("WebCounter-dialog").style.top = '50%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight / 2) + 'px';
                document.getElementById("WebCounter-dialog").style.left = '100%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth + 31) + 'px';
                break;
            case "6":
                document.getElementById("WebCounter-dialog").style.top = '100%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight + 21) + 'px';
                break;
            case "7":
                document.getElementById("WebCounter-dialog").style.top = '100%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight + 21) + 'px';
                document.getElementById("WebCounter-dialog").style.left = '50%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth / 2) + 'px';
                break;
            case "8":
                document.getElementById("WebCounter-dialog").style.top = '100%';
                document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight + 21) + 'px';
                document.getElementById("WebCounter-dialog").style.left = '100%';
                document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth + 31) + 'px';
                break;
        }

        if (this.ContainerId == null)
            return;

        document.getElementById(this.id).style.visibility = 'visible';
        if (!this.IsActive || true) {
            var needMargin;
            var currentMargin;
            var delay = 10;
            var step = 20;
            switch (this.PopupEffectAppear) {
                case '1':
                    needMargin = document.getElementById("WebCounter-dialog").style.marginTop;
                    currentMargin = document.getElementById("WebCounter-dialog").offsetHeight + document.getElementById("WebCounter-dialog").offsetTop;
                    if (this.PopupAlign == 6 || this.PopupAlign == 7 || this.PopupAlign == 8) {
                        currentMargin += document.getElementById("WebCounter-dialog").offsetHeight;
                        step = 40;
                    }
                    if (this.PopupAlign == 3 || this.PopupAlign == 4 || this.PopupAlign == 5) {
                        currentMargin += (document.getElementById("WebCounter-dialog").offsetHeight) / 2;
                        step = 30;
                    }
                    currentMargin = "-" + currentMargin + "px";
                    document.getElementById("WebCounter-dialog").style.marginTop = currentMargin;
                    setTimeout("slide('slideUp', '" + needMargin + "', '" + currentMargin + "', " + delay + ", " + step + ")", 10);
                    break;
                case '2':
                    needMargin = document.getElementById("WebCounter-dialog").style.marginLeft;
                    currentMargin = document.body.offsetWidth - document.getElementById("WebCounter-dialog").offsetLeft;
                    if (this.PopupAlign == 1 || this.PopupAlign == 4 || this.PopupAlign == 7) {
                        currentMargin += (document.getElementById("WebCounter-dialog").offsetWidth) / 2;
                        step = 30;
                    }
                    if (this.PopupAlign == 0 || this.PopupAlign == 3 || this.PopupAlign == 6) {
                        currentMargin += document.getElementById("WebCounter-dialog").offsetWidth;
                        step = 40;
                    }
                    if (this.PopupAlign == 2 || this.PopupAlign == 5 || this.PopupAlign == 8) {
                        currentMargin -= document.getElementById("WebCounter-dialog").offsetWidth;
                    }
                    currentMargin = currentMargin + "px";
                    document.getElementById("WebCounter-dialog").style.marginLeft = currentMargin;
                    setTimeout("slide('slideRight', '" + needMargin + "', '" + currentMargin + "', " + delay + ", " + step + ")", 10);
                    break;
                case '3':
                    needMargin = document.getElementById("WebCounter-dialog").style.marginTop;
                    currentMargin = document.body.offsetHeight - document.getElementById("WebCounter-dialog").offsetTop;
                    if (this.PopupAlign == 0 || this.PopupAlign == 1 || this.PopupAlign == 2) {
                        currentMargin += document.getElementById("WebCounter-dialog").offsetHeight;
                        step = 40;
                    }
                    if (this.PopupAlign == 3 || this.PopupAlign == 4 || this.PopupAlign == 5) {
                        currentMargin += (document.getElementById("WebCounter-dialog").offsetHeight) / 2;
                        step = 30;
                    }
                    currentMargin = currentMargin + "px";
                    document.getElementById("WebCounter-dialog").style.marginTop = currentMargin;
                    setTimeout("slide('slideDown', '" + needMargin + "', '" + currentMargin + "', " + delay + ", " + step + ")", 10);
                    break;
                case '4':
                    needMargin = document.getElementById("WebCounter-dialog").style.marginLeft;
                    currentMargin = document.getElementById("WebCounter-dialog").offsetWidth + document.getElementById("WebCounter-dialog").offsetLeft;
                    if (this.PopupAlign == 1 || this.PopupAlign == 4 || this.PopupAlign == 7) {
                        currentMargin += (document.getElementById("WebCounter-dialog").offsetWidth) / 2;
                        step = 30;
                    }
                    if (this.PopupAlign == 2 || this.PopupAlign == 5 || this.PopupAlign == 8) {
                        currentMargin += document.getElementById("WebCounter-dialog").offsetWidth;
                        step = 40;
                    }
                    currentMargin = "-" + currentMargin + "px";
                    document.getElementById("WebCounter-dialog").style.marginLeft = currentMargin;
                    setTimeout("slide('slideLeft', '" + needMargin + "', '" + currentMargin + "', " + delay + ", " + step + ")", 10);
                    break;
                case '5':
                    var width = parseInt((document.getElementById("WebCounter-dialog").style.width).replace("px", ""));
                    var height = parseInt((document.getElementById("WebCounter-dialog").style.height).replace("px", ""));
                    var stepWidth = Math.ceil(width / 20);
                    var stepHeight = Math.ceil(height / 20);
                    document.getElementById("WebCounter-dialog").style.width = 0;
                    document.getElementById("WebCounter-dialog").style.height = 0;
                    document.getElementById("WebCounter-dialog").style.overflow = "hidden";
                    setTimeout("zoom(" + width + ", " + height + ", 150, 50, " + stepWidth + ", " + stepHeight + ")", 10);
                    break;
            }
        }

        this.IsActive = true;
    },

    hide: function () {
        document.getElementById(this.id).style.display = 'none';
        WebCounter.Overlay.hide();
        document.getElementById('WebCounter-dialog').style.width = '596px';
        document.getElementById('WebCounter-dialog-container').innerHTML = '';
        this.ContainerId = null;
        this.IsActive = false;
    },

    css_template: "\
        #WebCounter-dialog * { margin: 0; padding: 0; position: relative; outline: none; }\
        #WebCounter-dialog { width: 596px; display: none; background-color: #fff; font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 16px; top: 50%; left: 50%; position: fixed; z-index: #{dialogZIndex}; margin-top: -200px; margin-left: -298px; padding: 10px; border-radius: 10px; -moz-border-radius: 10px; -webkit-border-radius: 10px; }\
        #WebCounter-dialog-close { width: 28px; height: 28px; background: url(#{imagesPath}/sprite.png) 0 -14px no-repeat; position: absolute; top: 0; right: 0; z-index: #{dialogCloseZIndex}; margin: -10px -10px 0 0!important; text-decoration: none; }\
        a:hover#WebCounter-dialog-close { background: url(#{imagesPath}/sprite.png) 0 -42px no-repeat; }\
        #WebCounter-dialog-close span { display: none; }\
        #WebCounter-dialog-top { width: 596px; height: 7px; background: url(#{imagesPath}/sprite.png) no-repeat; font-size: 0; line-height: 0; }\
        #WebCounter-dialog-bottom { width: 596px; height: 7px; background: url(#{imagesPath}/sprite.png) 0 -7px no-repeat; font-size: 0; line-height: 0; }\
        #WebCounter-dialog .field-wrapper { text-align: center; width: 100%; display: inline-block; background-color: #fff; border: 1px solid #999; margin-bottom: 5px; border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px; }\
        #WebCounter-dialog label { width: 100%; display: inline-block; color: #666; vertical-align: top; padding: 6px 0 3px 0; }\
        #WebCounter-dialog input { width: 100%; height: 15px; background-color: transparent; border: 0; font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 15px; margin: 5px; }\
        #WebCounter-dialog textarea { width: 100%; height: 43px; background-color: transparent; border: 0; font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 14px; margin: 5px; resize: none; }\
        #WebCounter-dialog select { width: 100%; height: 27px; background-color: transparent; border:0; font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 27px; padding: 5px; border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px; }\
#WebCounter-dialog .hor-left { width: 25%; display: table-cell; vertical-align: top; } #WebCounter-dialog .hor-left label { width: 49%; float: left; } #WebCounter-dialog .hor-left input { width: 97%; float: left; } #WebCounter-dialog .hor-left select { width: 100%; float: left; } #WebCounter-dialog .hor-left textarea { width: 97%; float: left; } \
#WebCounter-dialog .hor-top { width: 25%; display: table-cell; padding: 2px; vertical-align: top; } #WebCounter-dialog .hor-top label { width: 99%; float: left; } #WebCounter-dialog .hor-top input { width: 97%; float: left; } #WebCounter-dialog .hor-top select { width: 100%; float: left; } #WebCounter-dialog .hor-top textarea { width: 97%; float: left; } \
#WebCounter-dialog .ver-left { width: 100%; display: table; margin-bottom: 5px; } #WebCounter-dialog .ver-left label { width: 50%; display: table-cell; } #WebCounter-dialog .ver-left input { width: 98%; } #WebCounter-dialog .ver-left select { width: 100%; } #WebCounter-dialog .ver-left textarea { width: 98%; } \
#WebCounter-dialog .ver-top { width: 100%; } #WebCounter-dialog .ver-top label { width: 99%; } #WebCounter-dialog .ver-top input { width: 99%; } #WebCounter-dialog .ver-top select { width: 100%; } #WebCounter-dialog .ver-top textarea { width: 99%; } \
#WebCounter-dialog .hor-none { width: 25%; display: table-cell; padding: 2px; vertical-align: top; } #WebCounter-dialog .hor-none input { width: 99%; float: left; } #WebCounter-dialog .hor-none select { width: 100%; float: left; } #WebCounter-dialog .hor-none textarea { width: 99%; float: left; } \
#WebCounter-dialog .ver-none { width: 100%; } #WebCounter-dialog .ver-none input { width: 99%; } #WebCounter-dialog .ver-none select { width: 100%; } #WebCounter-dialog .ver-none textarea { width: 99%; } \
#WebCounter-dialog .ver-left .field-wrapper { width: 50%; display: table-cell; } \
#WebCounter-dialog .hor-left .field-wrapper { width: 50%; } \
#WebCounter-dialog a { display: inline-block; font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 14px; color: #333; font-weight: bold; margin: 5px 0; }\
        #WebCounter-dialog a { display: inline-block; font-family: Tahoma, Geneva, sans-serif; font-size: 12px; line-height: 14px; color: #333; font-weight: bold; margin: 5px 0; }\
        #WebCounter-dialog .btn { height: 27px; display: inline-block; font-size: 13px; font-weight: normal; line-height: 27px; text-decoration: none!important; text-align: center; color: #fff; background-color: #64c400; -webkit-border-radius: 16px; -moz-border-radius: 16px; border-radius: 16px; }\
        #WebCounter-dialog .btn em { width: 13px; height: 27px; display: inline-block; float: left; background: url(#{imagesPath}/sprite.png) 0 -97px no-repeat; }\
        #WebCounter-dialog .btn span { height: 27px; display: inline-block; float: left; background: url(#{imagesPath}/sprite.png) right -70px no-repeat; padding: 0 26px 0 12px; }\
        #WebCounter-dialog a:hover.btn em { background-position: 0 -151px; }\
        #WebCounter-dialog a:hover.btn span { background-position: right -124px; }\
        #WebCounter-dialog a:active.btn em { background-position: 0 -205px; }\
        #WebCounter-dialog a:active.btn span { background-position: right -178px; }\
        #WebCounter-dialog .hide { display: none; }\
#WebCounter-dialog .clear { clear: both; height: 0; line-height: 0; font-size: 0 }\
#WebCounter-dialog .clearfix { zoom: 1; }\
#WebCounter-dialog .clearfix:after { content: ' '; clear: both; display: block; width: 0; height: 0; overflow: hidden; font-size: 0; }\
#WebCounter-dialog-loader { top: 27px; width: 200px; position: absolute; left: 50%; margin-left: -100px; text-align: center; }\
#WebCounter-dialog iframe { height:1px; width: 100%; overflow: hidden; }\
	"
};


WebCounter.Util = {
    sslAssetHost: "https://svc-demo.leadforce.ru",
    //sslAssetHost: "https://localhost:52965",

    assetHost: "http://svc-demo.leadforce.ru",
    //assetHost: "http://localhost:52965",

    getAssetHost: function () {
        return ("https:" == document.location.protocol) ? this.sslAssetHost : this.assetHost;
    },

    render: function (template, params) {
        return template.replace( /\#{([^{}]*)}/g , function(a, b) {
            var r = params[b];
            return typeof r === 'string' || typeof r === 'number' ? r : a;
        });
    },

    includeCss: function (css) {
        var styleElement = document.createElement('style');
        styleElement.setAttribute('type', 'text/css');
        styleElement.setAttribute('media', 'screen');
        if (styleElement.styleSheet) {
            styleElement.styleSheet.cssText = css;
        } else {
            styleElement.appendChild(document.createTextNode(css));
        }
        document.getElementsByTagName('head')[0].appendChild(styleElement);
    },

    sendJSONP: function (url, callback) {
        var oElem = null;
        //if (!url || !callback) return;
        if (!url) return;

        if (callback != null)
            url += '&jscallback=' + callback;

        if (oElem) {
            oElem.parentNode.removeChild(oElem);
        }
        oElem = document.createElement('script');
        oElem.setAttribute('type', 'text/javascript');
        document.getElementsByTagName('head')[0].appendChild(oElem);
        oElem.setAttribute('src', url);
    },

    S4: function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    },

    guid: function () {
        return (this.S4() + this.S4() + "-" + this.S4() + "-" + this.S4() + "-" + this.S4() + "-" + this.S4() + this.S4() + this.S4());
    }
};

WebCounter.Callback = {
    Counter: function () {
        while (funcqueue.length > 0) {
            (funcqueue.shift())();
        }

        var forms = document.getElementsByTagName("form");
        for (var i = 0; i < forms.length; i++) {
            if (forms[0].action != null && forms[0].action.indexOf("externalFormHandler.ashx") != -1)
                forms[0].action = forms[0].action + "&WebCounterUserID=" + WebCounter.ContactID;
        }
    },

    Form: function (result) {
        if (result != null) {
            var jsonPopupAppear = LFJSON.parse(result.PopupAppear);

            if (result.Mode == 0)
                eval(result.Value);
            //document.getElementById(result.ContainerID).innerHTML = result.Value.replace(/\[BR\]/g, "\n");

            if (result.Mode == 1) {
                //document.getElementById("WebCounter-dialog-container").innerHTML = result.Value.replace(/\[BR\]/g, "\n");
                eval(result.Value);
                WebCounter.Dialog.PopupEffectAppear = jsonPopupAppear.PopupEffectAppear;
                WebCounter.Dialog.PopupAlign = jsonPopupAppear.PopupAlign;
                //WebCounter.Dialog.show(null);
                //WebCounter.Dialog.preshow(null);
            }


            if (result.Mode == 2) {
                var objDialog = new ObjDialog();
                //objDialog.Html = result.Value.replace(/\[BR\]/g, "\n");
                objDialog.Mode = result.Mode;
                objDialog.ActivityCode = result.ActivityCode;
                objDialog.Parameter = result.Parameter;
                objDialog.PopupDelayAppear = jsonPopupAppear.PopupDelayAppear;
                objDialog.PopupEffectAppear = jsonPopupAppear.PopupEffectAppear;
                objDialog.PopupAlign = jsonPopupAppear.PopupAlign;
                objDialog.ContainerId = result.ContainerID;
                Dialogs.push(objDialog);

                eval(result.Value);
            }
            if (result.Mode == 3) {
                var objDialog = new ObjDialog();
                //objDialog.Html = result.Value.replace(/\[BR\]/g, "\n");
                objDialog.Mode = result.Mode;
                objDialog.ActivityCode = result.ActivityCode;
                objDialog.Parameter = result.Parameter;
                objDialog.PopupDelayAppear = jsonPopupAppear.PopupDelayAppear;
                objDialog.PopupEffectAppear = jsonPopupAppear.PopupEffectAppear;
                objDialog.PopupAlign = jsonPopupAppear.PopupAlign;

                var floatingButton = document.createElement('a');
                if (jsonPopupAppear.FloatingButtonIcon != '') {
                    var floatingButtonImg = document.createElement('img');
                    floatingButtonImg.setAttribute('src', jsonPopupAppear.FloatingButtonIcon);
                    floatingButtonImg.setAttribute('alt', jsonPopupAppear.FloatingButtonName);
                    floatingButton.appendChild(floatingButtonImg);
                    var floatingButtonSpan = document.createElement('span');
                    floatingButtonSpan.innerHTML = jsonPopupAppear.FloatingButtonName;
                    floatingButton.appendChild(floatingButtonSpan);
                }
                else
                    floatingButton.innerHTML = jsonPopupAppear.FloatingButtonName;
                floatingButton.setAttribute("href", "javascript:WebCounter.LG_FormFloatingButton('" + result.ActivityCode + "', '" + result.Parameter + "')");

                document.body.appendChild(floatingButton);
                floatingButton.style.position = "fixed";
                if (jsonPopupAppear.FloatingButtonBackground != null)
                    floatingButton.style.backgroundColor = "#" + jsonPopupAppear.FloatingButtonBackground;

                switch (jsonPopupAppear.FloatingButtonPosition) {
                    case "0":
                        floatingButton.className = "wcfloatingbutton wcfloatingbutton-right";
                        floatingButton.style.right = "-" + ((floatingButton.offsetWidth / 2) - (floatingButton.offsetHeight / 2)) + 'px';
                        if (jsonPopupAppear.FloatingButtonMargin != '')
                            floatingButton.style.top = jsonPopupAppear.FloatingButtonMargin + 'px';
                        else {
                            floatingButton.style.top = '50%';
                            floatingButton.style.marginTop = "-" + (floatingButton.offsetHeight / 2) + 'px';
                        }
                        break;
                    case "1":
                        floatingButton.className = "wcfloatingbutton wcfloatingbutton-left";
                        floatingButton.style.left = "-" + ((floatingButton.offsetWidth / 2) - (floatingButton.offsetHeight / 2)) + 'px';
                        if (jsonPopupAppear.FloatingButtonMargin != '')
                            floatingButton.style.top = jsonPopupAppear.FloatingButtonMargin + 'px';
                        else {
                            floatingButton.style.top = '50%';
                            floatingButton.style.marginTop = "-" + (floatingButton.offsetHeight / 2) + 'px';
                        }
                        break;
                    case "2":
                        floatingButton.className = "wcfloatingbutton";
                        floatingButton.style.top = 0;
                        if (jsonPopupAppear.FloatingButtonMargin != '')
                            floatingButton.style.left = jsonPopupAppear.FloatingButtonMargin + 'px';
                        else {
                            floatingButton.style.left = '50%';
                            floatingButton.style.marginLeft = "-" + (floatingButton.offsetWidth / 2) + 'px';
                        }
                        break;
                    case "3":
                        floatingButton.className = "wcfloatingbutton";
                        floatingButton.style.bottom = 0;
                        if (jsonPopupAppear.FloatingButtonMargin != '')
                            floatingButton.style.left = jsonPopupAppear.FloatingButtonMargin + 'px';
                        else {
                            floatingButton.style.left = '50%';
                            floatingButton.style.marginLeft = "-" + (floatingButton.offsetWidth / 2) + 'px';
                        }
                        break;
                }

                objDialog.ContainerId = result.ContainerID;
                Dialogs.push(objDialog);

                eval(result.Value);
            }

            if (result.Mode == 4) {
                var objDialog = new ObjDialog();
                //objDialog.Html = result.Value.replace(/\[BR\]/g, "\n");
                objDialog.Mode = result.Mode;
                objDialog.ActivityCode = result.ActivityCode;
                objDialog.Parameter = result.Parameter;
                objDialog.PopupEffectAppear = jsonPopupAppear.PopupEffectAppear;
                objDialog.PopupAlign = jsonPopupAppear.PopupAlign;
                objDialog.CallOnClosing = "true";
                objDialog.DelayAppearOnClosing = jsonPopupAppear.DelayAppearOnClosing;
                objDialog.SizeFieldOnClosing = jsonPopupAppear.SizeFieldOnClosing;
                objDialog.ContainerId = result.ContainerID;
                Dialogs.push(objDialog);

                eval(result.Value);
            }
        }
    },

    Link: function (result) {
        if (result.RuleType == "Link" || result.RuleType == "File")
            window.location = result.Value;

        if (result.RuleType == "Form") {
            document.getElementById("WebCounter-dialog-container").innerHTML = result.Value.replace(/\[BR\]/g, "\n");
            WebCounter.Dialog.show();
        }
    },

    FormServiceResult: function (result) {
        window.location = result.Value;
    },

    LinkProcessingResult: function (result) {
        window.location = result.Value;
    }
};

fsr = function(result) {
    window.location = result.Value;
};

WebCounter.Ajax = {
    Counter: function() {
        this.jsonData = "";

        this.send = function(url) {
            url = url + '?' + LFJSON.toQuerystring(this.jsonData);
            WebCounter.Util.sendJSONP(url, "WebCounter.Callback.Counter");
        };
    },

    Link: function() {
        this.jsonData = "";

        this.send = function(url) {
            url = url + '?' + LFJSON.toQuerystring(this.jsonData);
            WebCounter.Util.sendJSONP(url, "WebCounter.Callback.Link");
        };
    },

    Form: function() {
        this.id = "";

        this.jsonData = "";

        this.send = function(url) {
            url = url + '?' + LFJSON.toQuerystring(this.jsonData) + '&ContainerID=' + this.id;
            WebCounter.Util.sendJSONP(url, "WebCounter.Callback.Form");
        };
    },

    FormServiceResult: function() {
        this.jsonData = "";

        this.send = function(url) {
            url = url + '?' + LFJSON.toQuerystring(this.jsonData);
            //WebCounter.Util.sendJSONP(url, "WebCounter.Callback.FormServiceResult");
            WebCounter.Util.sendJSONP(url, "fsr");
        };
    },

    FormServiceCancel: function() {
        this.jsonData = "";

        this.send = function(url) {
            url = url + '?' + LFJSON.toQuerystring(this.jsonData);
            WebCounter.Util.sendJSONP(url, null);
        };
    },

    FormServiceOpen: function () {
        this.jsonData = "";

        this.send = function (url) {
            url = url + '?' + LFJSON.toQuerystring(this.jsonData);
            WebCounter.Util.sendJSONP(url, null);
        };
    },

    LinkProcessing: function() {
        this.jsonData = "";

        this.send = function(url) {
            url = url + '?' + LFJSON.toQuerystring(this.jsonData);
            WebCounter.Util.sendJSONP(url, "WebCounter.Callback.LinkProcessingResult");
        };
    }
};


WebCounter.Cookie = {
    setCookie: function(name, value, expires, path, domain, secure) {
        if (expires) {
            var date = new Date();
            date.setTime(date.getTime() + (expires * 24 * 60 * 60 * 1000));
        }
        document.cookie = name + "=" + escape(value) +
            ((expires) ? "; expires=" + date.toGMTString() : "") +
                ((path) ? "; path=" + path : "") +
                    ((domain) ? "; domain=" + domain : "") +
                        ((secure) ? "; secure" : "");
    },

    getCookie: function(name) {
        var cookie = " " + document.cookie;
        var search = " " + name + "=";
        var setStr = null;
        var offset = 0;
        var end = 0;
        if (cookie.length > 0) {
            offset = cookie.indexOf(search);
            if (offset != -1) {
                offset += search.length;
                end = cookie.indexOf(";", offset);
                if (end == -1) {
                    end = cookie.length;
                }
                setStr = unescape(cookie.substring(offset, end));
            }
        }
        return (setStr);
    }
};

LFJSON = {
    parse: function (str) {
        if (str != null)
            str = str.replace(/\\u000d\\u000a/g, "<br />");
        if (str === "") str = '""';
        eval("var p=" + str + ";");
        return p;
    },

    toQuerystring: function (str) {
        var data = this.parse(str);
        var params = new Array();
        var formData = '';
        for (var key in data) {
            var val = data[key];
            formData = '';
            switch (key) {
            case 'FormData':
                for (var i = 0; i < data[key].length; i++) {
                    var keyFormData = data[key][i];
                    if (i > 0) formData += ",";
                    formData += '{"N":"' + keyFormData.N + '","V":"' + keyFormData.V + '"}';
                }
                params.push('FormData=[' + encodeURIComponent(formData) + ']');
                break;
            case 'PopupAppear':
                for (var i = 0; i < data[key].length; i++) {
                    var keyFormData = data[key][i];
                    if (i > 0) formData += ",";
                    formData += '"' + keyFormData.N + '":"' + keyFormData.V + '"';
                }
                params.push('PopupAppear={' + formData + '}');
                break;
            default:
                params.push(key + '=' + encodeURIComponent(val));
                break;
            }
        }
        return params.join("&");
    }
};

if (document.getElementsByClassName == undefined) {
    document.getElementsByClassName = function (cl) {
        var retnode = [];
        var myclass = new RegExp('\\b' + cl + '\\b');
        var elem = this.getElementsByTagName('*');
        for (var i = 0; i < elem.length; i++) {
            var classes = elem[i].className;
            if (myclass.test(classes)) {
                retnode.push(elem[i]);
            }
        }
        return retnode;
    }
}

var funcqueue = [];

var wrapFunction = function(fn, context, params) {
    return function() {
        fn.apply(context, params);
    };
};

var lfCalled = false;
LeadForce = function(params) {
    if (lfCalled) return;
    lfCalled = true;

    var newParams;
    for (var i = 0; i < params.length; i++) {
        newParams = new Array();
        if (params[i][0].toLowerCase() == 'webcounter.lg_counter') {
            for (var j = 1; j < params[i].length; j++)
                newParams.push('"' + params[i][j] + '"');
            eval(params[i][0] + '(' + newParams.join(',') + ');');
        } else {
            for (j = 1; j < params[i].length; j++)
                newParams.push(params[i][j]);
            funcqueue.push(wrapFunction(eval(params[i][0]), WebCounter, newParams));
        }
    }
};

if (document.documentElement.innerHTML.toLowerCase().indexOf("</body>") != -1) {
    LeadForce(_lfq);
}

var readyCalled = false;
function ready() {
    if (readyCalled) return;
    readyCalled = true;
    LeadForce(_lfq);
}

if (document.addEventListener) {
    document.addEventListener("DOMContentLoaded", function() { ready(); }, false);
} else if (document.attachEvent) {
    if (document.documentElement.doScroll && window == window.top) {
        function tryScroll() {
            if (readyCalled) return;
            //if (!document.body) return;
            try {
                document.documentElement.doScroll("left");
                ready();
            } catch(e) { setTimeout(tryScroll, 1); }
        }
        tryScroll();
    }

    document.attachEvent("onreadystatechange", function() {
        if (document.readyState === "complete") { ready(); }
    });
}

if (window.addEventListener)
    window.addEventListener('load', ready, false);
else if (window.attachEvent)
    window.attachEvent('onload', ready);

(function (N, d, p, K, k, H) { var b = this; var n = Math.floor(Math.random() * 10000); var q = Function.prototype; var Q = /^((http.?:)\/\/([^:\/\s]+)(:\d+)*)/; var R = /[\-\w]+\/\.\.\//; var F = /([^:])\/\//g; var I = ""; var o = {}; var M = N.easyXDM; var U = "easyXDM_"; var E; var y = false; var i; var h; function C(X, Z) { var Y = typeof X[Z]; return Y == "function" || (!!(Y == "object" && X[Z])) || Y == "unknown" } function u(X, Y) { return !!(typeof (X[Y]) == "object" && X[Y]) } function r(X) { return Object.prototype.toString.call(X) === "[object Array]" } function c() { try { var X = new ActiveXObject("ShockwaveFlash.ShockwaveFlash"); i = Array.prototype.slice.call(X.GetVariable("$version").match(/(\d+),(\d+),(\d+),(\d+)/), 1); h = parseInt(i[0], 10) > 9 && parseInt(i[1], 10) > 0; X = null; return true } catch (Y) { return false } } var v, x; if (C(N, "addEventListener")) { v = function (Z, X, Y) { Z.addEventListener(X, Y, false) }; x = function (Z, X, Y) { Z.removeEventListener(X, Y, false) } } else { if (C(N, "attachEvent")) { v = function (X, Z, Y) { X.attachEvent("on" + Z, Y) }; x = function (X, Z, Y) { X.detachEvent("on" + Z, Y) } } else { throw new Error("Browser not supported") } } var W = false, J = [], L; if ("readyState" in d) { L = d.readyState; W = L == "complete" || (~navigator.userAgent.indexOf("AppleWebKit/") && (L == "loaded" || L == "interactive")) } else { W = !!d.body } function s() { if (W) { return } W = true; for (var X = 0; X < J.length; X++) { J[X]() } J.length = 0 } if (!W) { if (C(N, "addEventListener")) { v(d, "DOMContentLoaded", s) } else { v(d, "readystatechange", function () { if (d.readyState == "complete") { s() } }); if (d.documentElement.doScroll && N === top) { var g = function () { if (W) { return } try { d.documentElement.doScroll("left") } catch (X) { K(g, 1); return } s() }; g() } } v(N, "load", s) } function G(Y, X) { if (W) { Y.call(X); return } J.push(function () { Y.call(X) }) } function m() { var Z = parent; if (I !== "") { for (var X = 0, Y = I.split("."); X < Y.length; X++) { Z = Z[Y[X]] } } return Z.easyXDM } function e(X) { N.easyXDM = M; I = X; if (I) { U = "easyXDM_" + I.replace(".", "_") + "_" } return o } function z(X) { return X.match(Q)[3] } function f(X) { return X.match(Q)[4] || "" } function j(Z) { var X = Z.toLowerCase().match(Q); var aa = X[2], ab = X[3], Y = X[4] || ""; if ((aa == "http:" && Y == ":80") || (aa == "https:" && Y == ":443")) { Y = "" } return aa + "//" + ab + Y } function B(X) { X = X.replace(F, "$1/"); if (!X.match(/^(http||https):\/\//)) { var Y = (X.substring(0, 1) === "/") ? "" : p.pathname; if (Y.substring(Y.length - 1) !== "/") { Y = Y.substring(0, Y.lastIndexOf("/") + 1) } X = p.protocol + "//" + p.host + Y + X } while (R.test(X)) { X = X.replace(R, "") } return X } function P(X, aa) { var ac = "", Z = X.indexOf("#"); if (Z !== -1) { ac = X.substring(Z); X = X.substring(0, Z) } var ab = []; for (var Y in aa) { if (aa.hasOwnProperty(Y)) { ab.push(Y + "=" + H(aa[Y])) } } return X + (y ? "#" : (X.indexOf("?") == -1 ? "?" : "&")) + ab.join("&") + ac } var S = (function (X) { X = X.substring(1).split("&"); var Z = {}, aa, Y = X.length; while (Y--) { aa = X[Y].split("="); Z[aa[0]] = k(aa[1]) } return Z } (/xdm_e=/.test(p.search) ? p.search : p.hash)); function t(X) { return typeof X === "undefined" } var O = function () { var Y = {}; var Z = { a: [1, 2, 3] }, X = '{"a":[1,2,3]}'; if (typeof JSON != "undefined" && typeof JSON.stringify === "function" && JSON.stringify(Z).replace((/\s/g), "") === X) { return JSON } if (Object.toJSON) { if (Object.toJSON(Z).replace((/\s/g), "") === X) { Y.stringify = Object.toJSON } } if (typeof String.prototype.evalJSON === "function") { Z = X.evalJSON(); if (Z.a && Z.a.length === 3 && Z.a[2] === 3) { Y.parse = function (aa) { return aa.evalJSON() } } } if (Y.stringify && Y.parse) { O = function () { return Y }; return Y } return null }; function T(X, Y, Z) { var ab; for (var aa in Y) { if (Y.hasOwnProperty(aa)) { if (aa in X) { ab = Y[aa]; if (typeof ab === "object") { T(X[aa], ab, Z) } else { if (!Z) { X[aa] = Y[aa] } } } else { X[aa] = Y[aa] } } } return X } function a() { var Y = d.body.appendChild(d.createElement("form")), X = Y.appendChild(d.createElement("input")); X.name = U + "TEST" + n; E = X !== Y.elements[X.name]; d.body.removeChild(Y) } function A(X) { if (t(E)) { a() } var Z; if (E) { Z = d.createElement('<iframe name="' + X.props.name + '"/>') } else { Z = d.createElement("IFRAME"); Z.name = X.props.name } Z.id = Z.name = X.props.name; delete X.props.name; if (X.onLoad) { v(Z, "load", X.onLoad) } if (typeof X.container == "string") { X.container = d.getElementById(X.container) } if (!X.container) { T(Z.style, { position: "absolute", top: "-2000px" }); X.container = d.body } var Y = X.props.src; delete X.props.src; T(Z, X.props); Z.border = Z.frameBorder = 0; Z.allowTransparency = true; X.container.appendChild(Z); Z.src = Y; X.props.src = Y; return Z } function V(aa, Z) { if (typeof aa == "string") { aa = [aa] } var Y, X = aa.length; while (X--) { Y = aa[X]; Y = new RegExp(Y.substr(0, 1) == "^" ? Y : ("^" + Y.replace(/(\*)/g, ".$1").replace(/\?/g, ".") + "$")); if (Y.test(Z)) { return true } } return false } function l(Z) { var ae = Z.protocol, Y; Z.isHost = Z.isHost || t(S.xdm_p); y = Z.hash || false; if (!Z.props) { Z.props = {} } if (!Z.isHost) { Z.channel = S.xdm_c; Z.secret = S.xdm_s; Z.remote = S.xdm_e; ae = S.xdm_p; if (Z.acl && !V(Z.acl, Z.remote)) { throw new Error("Access denied for " + Z.remote) } } else { Z.remote = B(Z.remote); Z.channel = Z.channel || "default" + n++; Z.secret = Math.random().toString(16).substring(2); if (t(ae)) { if (j(p.href) == j(Z.remote)) { ae = "4" } else { if (C(N, "postMessage") || C(d, "postMessage")) { ae = "1" } else { if (Z.swf && C(N, "ActiveXObject") && c()) { ae = "6" } else { if (navigator.product === "Gecko" && "frameElement" in N && navigator.userAgent.indexOf("WebKit") == -1) { ae = "5" } else { if (Z.remoteHelper) { Z.remoteHelper = B(Z.remoteHelper); ae = "2" } else { ae = "0" } } } } } } } Z.protocol = ae; switch (ae) { case "0": T(Z, { interval: 100, delay: 2000, useResize: true, useParent: false, usePolling: false }, true); if (Z.isHost) { if (!Z.local) { var ac = p.protocol + "//" + p.host, X = d.body.getElementsByTagName("img"), ad; var aa = X.length; while (aa--) { ad = X[aa]; if (ad.src.substring(0, ac.length) === ac) { Z.local = ad.src; break } } if (!Z.local) { Z.local = N } } var ab = { xdm_c: Z.channel, xdm_p: 0 }; if (Z.local === N) { Z.usePolling = true; Z.useParent = true; Z.local = p.protocol + "//" + p.host + p.pathname + p.search; ab.xdm_e = Z.local; ab.xdm_pa = 1 } else { ab.xdm_e = B(Z.local) } if (Z.container) { Z.useResize = false; ab.xdm_po = 1 } Z.remote = P(Z.remote, ab) } else { T(Z, { channel: S.xdm_c, remote: S.xdm_e, useParent: !t(S.xdm_pa), usePolling: !t(S.xdm_po), useResize: Z.useParent ? false : Z.useResize }) } Y = [new o.stack.HashTransport(Z), new o.stack.ReliableBehavior({}), new o.stack.QueueBehavior({ encode: true, maxLength: 4000 - Z.remote.length }), new o.stack.VerifyBehavior({ initiate: Z.isHost })]; break; case "1": Y = [new o.stack.PostMessageTransport(Z)]; break; case "2": Y = [new o.stack.NameTransport(Z), new o.stack.QueueBehavior(), new o.stack.VerifyBehavior({ initiate: Z.isHost })]; break; case "3": Y = [new o.stack.NixTransport(Z)]; break; case "4": Y = [new o.stack.SameOriginTransport(Z)]; break; case "5": Y = [new o.stack.FrameElementTransport(Z)]; break; case "6": if (!i) { c() } Y = [new o.stack.FlashTransport(Z)]; break } Y.push(new o.stack.QueueBehavior({ lazy: Z.lazy, remove: true })); return Y } function D(aa) { var ab, Z = { incoming: function (ad, ac) { this.up.incoming(ad, ac) }, outgoing: function (ac, ad) { this.down.outgoing(ac, ad) }, callback: function (ac) { this.up.callback(ac) }, init: function () { this.down.init() }, destroy: function () { this.down.destroy() } }; for (var Y = 0, X = aa.length; Y < X; Y++) { ab = aa[Y]; T(ab, Z, true); if (Y !== 0) { ab.down = aa[Y - 1] } if (Y !== X - 1) { ab.up = aa[Y + 1] } } return ab } function w(X) { X.up.down = X.down; X.down.up = X.up; X.up = X.down = null } T(o, { version: "2.4.15.118", query: S, stack: {}, apply: T, getJSONObject: O, whenReady: G, noConflict: e }); o.DomHelper = { on: v, un: x, requiresJSON: function (X) { if (!u(N, "JSON")) { d.write('<script type="text/javascript" src="' + X + '"><\/script>') } } }; (function () { var X = {}; o.Fn = { set: function (Y, Z) { X[Y] = Z }, get: function (Z, Y) { var aa = X[Z]; if (Y) { delete X[Z] } return aa } } } ()); o.Socket = function (Y) { var X = D(l(Y).concat([{ incoming: function (ab, aa) { Y.onMessage(ab, aa) }, callback: function (aa) { if (Y.onReady) { Y.onReady(aa) } } }])), Z = j(Y.remote); this.origin = j(Y.remote); this.destroy = function () { X.destroy() }; this.postMessage = function (aa) { X.outgoing(aa, Z) }; X.init() }; o.Rpc = function (Z, Y) { if (Y.local) { for (var ab in Y.local) { if (Y.local.hasOwnProperty(ab)) { var aa = Y.local[ab]; if (typeof aa === "function") { Y.local[ab] = { method: aa} } } } } var X = D(l(Z).concat([new o.stack.RpcBehavior(this, Y), { callback: function (ac) { if (Z.onReady) { Z.onReady(ac) } } }])); this.origin = j(Z.remote); this.destroy = function () { X.destroy() }; X.init() }; o.stack.SameOriginTransport = function (Y) { var Z, ab, aa, X; return (Z = { outgoing: function (ad, ae, ac) { aa(ad); if (ac) { ac() } }, destroy: function () { if (ab) { ab.parentNode.removeChild(ab); ab = null } }, onDOMReady: function () { X = j(Y.remote); if (Y.isHost) { T(Y.props, { src: P(Y.remote, { xdm_e: p.protocol + "//" + p.host + p.pathname, xdm_c: Y.channel, xdm_p: 4 }), name: U + Y.channel + "_provider" }); ab = A(Y); o.Fn.set(Y.channel, function (ac) { aa = ac; K(function () { Z.up.callback(true) }, 0); return function (ad) { Z.up.incoming(ad, X) } }) } else { aa = m().Fn.get(Y.channel, true)(function (ac) { Z.up.incoming(ac, X) }); K(function () { Z.up.callback(true) }, 0) } }, init: function () { G(Z.onDOMReady, Z) } }) }; o.stack.FlashTransport = function (aa) { var ac, X, ab, ad, Y, ae; function af(ah, ag) { K(function () { ac.up.incoming(ah, ad) }, 0) } function Z(ah) { var ag = aa.swf + "?host=" + aa.isHost; var aj = "easyXDM_swf_" + Math.floor(Math.random() * 10000); o.Fn.set("flash_loaded" + ah.replace(/[\-.]/g, "_"), function () { o.stack.FlashTransport[ah].swf = Y = ae.firstChild; var ak = o.stack.FlashTransport[ah].queue; for (var al = 0; al < ak.length; al++) { ak[al]() } ak.length = 0 }); if (aa.swfContainer) { ae = (typeof aa.swfContainer == "string") ? d.getElementById(aa.swfContainer) : aa.swfContainer } else { ae = d.createElement("div"); T(ae.style, h && aa.swfNoThrottle ? { height: "20px", width: "20px", position: "fixed", right: 0, top: 0} : { height: "1px", width: "1px", position: "absolute", overflow: "hidden", right: 0, top: 0 }); d.body.appendChild(ae) } var ai = "callback=flash_loaded" + ah.replace(/[\-.]/g, "_") + "&proto=" + b.location.protocol + "&domain=" + z(b.location.href) + "&port=" + f(b.location.href) + "&ns=" + I; ae.innerHTML = "<object height='20' width='20' type='application/x-shockwave-flash' id='" + aj + "' data='" + ag + "'><param name='allowScriptAccess' value='always'></param><param name='wmode' value='transparent'><param name='movie' value='" + ag + "'></param><param name='flashvars' value='" + ai + "'></param><embed type='application/x-shockwave-flash' FlashVars='" + ai + "' allowScriptAccess='always' wmode='transparent' src='" + ag + "' height='1' width='1'></embed></object>" } return (ac = { outgoing: function (ah, ai, ag) { Y.postMessage(aa.channel, ah.toString()); if (ag) { ag() } }, destroy: function () { try { Y.destroyChannel(aa.channel) } catch (ag) { } Y = null; if (X) { X.parentNode.removeChild(X); X = null } }, onDOMReady: function () { ad = aa.remote; o.Fn.set("flash_" + aa.channel + "_init", function () { K(function () { ac.up.callback(true) }) }); o.Fn.set("flash_" + aa.channel + "_onMessage", af); aa.swf = B(aa.swf); var ah = z(aa.swf); var ag = function () { o.stack.FlashTransport[ah].init = true; Y = o.stack.FlashTransport[ah].swf; Y.createChannel(aa.channel, aa.secret, j(aa.remote), aa.isHost); if (aa.isHost) { if (h && aa.swfNoThrottle) { T(aa.props, { position: "fixed", right: 0, top: 0, height: "20px", width: "20px" }) } T(aa.props, { src: P(aa.remote, { xdm_e: j(p.href), xdm_c: aa.channel, xdm_p: 6, xdm_s: aa.secret }), name: U + aa.channel + "_provider" }); X = A(aa) } }; if (o.stack.FlashTransport[ah] && o.stack.FlashTransport[ah].init) { ag() } else { if (!o.stack.FlashTransport[ah]) { o.stack.FlashTransport[ah] = { queue: [ag] }; Z(ah) } else { o.stack.FlashTransport[ah].queue.push(ag) } } }, init: function () { G(ac.onDOMReady, ac) } }) }; o.stack.PostMessageTransport = function (aa) { var ac, ad, Y, Z; function X(ae) { if (ae.origin) { return j(ae.origin) } if (ae.uri) { return j(ae.uri) } if (ae.domain) { return p.protocol + "//" + ae.domain } throw "Unable to retrieve the origin of the event" } function ab(af) { var ae = X(af); if (ae == Z && af.data.substring(0, aa.channel.length + 1) == aa.channel + " ") { ac.up.incoming(af.data.substring(aa.channel.length + 1), ae) } } return (ac = { outgoing: function (af, ag, ae) { Y.postMessage(aa.channel + " " + af, ag || Z); if (ae) { ae() } }, destroy: function () { x(N, "message", ab); if (ad) { Y = null; ad.parentNode.removeChild(ad); ad = null } }, onDOMReady: function () { Z = j(aa.remote); if (aa.isHost) { var ae = function (af) { if (af.data == aa.channel + "-ready") { Y = ("postMessage" in ad.contentWindow) ? ad.contentWindow : ad.contentWindow.document; x(N, "message", ae); v(N, "message", ab); K(function () { ac.up.callback(true) }, 0) } }; v(N, "message", ae); T(aa.props, { src: P(aa.remote, { xdm_e: j(p.href), xdm_c: aa.channel, xdm_p: 1 }), name: U + aa.channel + "_provider" }); ad = A(aa) } else { v(N, "message", ab); Y = ("postMessage" in N.parent) ? N.parent : N.parent.document; Y.postMessage(aa.channel + "-ready", Z); K(function () { ac.up.callback(true) }, 0) } }, init: function () { G(ac.onDOMReady, ac) } }) }; o.stack.FrameElementTransport = function (Y) { var Z, ab, aa, X; return (Z = { outgoing: function (ad, ae, ac) { aa.call(this, ad); if (ac) { ac() } }, destroy: function () { if (ab) { ab.parentNode.removeChild(ab); ab = null } }, onDOMReady: function () { X = j(Y.remote); if (Y.isHost) { T(Y.props, { src: P(Y.remote, { xdm_e: j(p.href), xdm_c: Y.channel, xdm_p: 5 }), name: U + Y.channel + "_provider" }); ab = A(Y); ab.fn = function (ac) { delete ab.fn; aa = ac; K(function () { Z.up.callback(true) }, 0); return function (ad) { Z.up.incoming(ad, X) } } } else { if (d.referrer && j(d.referrer) != S.xdm_e) { N.top.location = S.xdm_e } aa = N.frameElement.fn(function (ac) { Z.up.incoming(ac, X) }); Z.up.callback(true) } }, init: function () { G(Z.onDOMReady, Z) } }) }; o.stack.NameTransport = function (ab) { var ac; var ae, ai, aa, ag, ah, Y, X; function af(al) { var ak = ab.remoteHelper + (ae ? "#_3" : "#_2") + ab.channel; ai.contentWindow.sendMessage(al, ak) } function ad() { if (ae) { if (++ag === 2 || !ae) { ac.up.callback(true) } } else { af("ready"); ac.up.callback(true) } } function aj(ak) { ac.up.incoming(ak, Y) } function Z() { if (ah) { K(function () { ah(true) }, 0) } } return (ac = { outgoing: function (al, am, ak) { ah = ak; af(al) }, destroy: function () { ai.parentNode.removeChild(ai); ai = null; if (ae) { aa.parentNode.removeChild(aa); aa = null } }, onDOMReady: function () { ae = ab.isHost; ag = 0; Y = j(ab.remote); ab.local = B(ab.local); if (ae) { o.Fn.set(ab.channel, function (al) { if (ae && al === "ready") { o.Fn.set(ab.channel, aj); ad() } }); X = P(ab.remote, { xdm_e: ab.local, xdm_c: ab.channel, xdm_p: 2 }); T(ab.props, { src: X + "#" + ab.channel, name: U + ab.channel + "_provider" }); aa = A(ab) } else { ab.remoteHelper = ab.remote; o.Fn.set(ab.channel, aj) } ai = A({ props: { src: ab.local + "#_4" + ab.channel }, onLoad: function ak() { var al = ai || this; x(al, "load", ak); o.Fn.set(ab.channel + "_load", Z); (function am() { if (typeof al.contentWindow.sendMessage == "function") { ad() } else { K(am, 50) } } ()) } }) }, init: function () { G(ac.onDOMReady, ac) } }) }; o.stack.HashTransport = function (Z) { var ac; var ah = this, af, aa, X, ad, am, ab, al; var ag, Y; function ak(ao) { if (!al) { return } var an = Z.remote + "#" + (am++) + "_" + ao; ((af || !ag) ? al.contentWindow : al).location = an } function ae(an) { ad = an; ac.up.incoming(ad.substring(ad.indexOf("_") + 1), Y) } function aj() { if (!ab) { return } var an = ab.location.href, ap = "", ao = an.indexOf("#"); if (ao != -1) { ap = an.substring(ao) } if (ap && ap != ad) { ae(ap) } } function ai() { aa = setInterval(aj, X) } return (ac = { outgoing: function (an, ao) { ak(an) }, destroy: function () { N.clearInterval(aa); if (af || !ag) { al.parentNode.removeChild(al) } al = null }, onDOMReady: function () { af = Z.isHost; X = Z.interval; ad = "#" + Z.channel; am = 0; ag = Z.useParent; Y = j(Z.remote); if (af) { Z.props = { src: Z.remote, name: U + Z.channel + "_provider" }; if (ag) { Z.onLoad = function () { ab = N; ai(); ac.up.callback(true) } } else { var ap = 0, an = Z.delay / 50; (function ao() { if (++ap > an) { throw new Error("Unable to reference listenerwindow") } try { ab = al.contentWindow.frames[U + Z.channel + "_consumer"] } catch (aq) { } if (ab) { ai(); ac.up.callback(true) } else { K(ao, 50) } } ()) } al = A(Z) } else { ab = N; ai(); if (ag) { al = parent; ac.up.callback(true) } else { T(Z, { props: { src: Z.remote + "#" + Z.channel + new Date(), name: U + Z.channel + "_consumer" }, onLoad: function () { ac.up.callback(true) } }); al = A(Z) } } }, init: function () { G(ac.onDOMReady, ac) } }) }; o.stack.ReliableBehavior = function (Y) { var aa, ac; var ab = 0, X = 0, Z = ""; return (aa = { incoming: function (af, ad) { var ae = af.indexOf("_"), ag = af.substring(0, ae).split(","); af = af.substring(ae + 1); if (ag[0] == ab) { Z = ""; if (ac) { ac(true) } } if (af.length > 0) { aa.down.outgoing(ag[1] + "," + ab + "_" + Z, ad); if (X != ag[1]) { X = ag[1]; aa.up.incoming(af, ad) } } }, outgoing: function (af, ad, ae) { Z = af; ac = ae; aa.down.outgoing(X + "," + (++ab) + "_" + af, ad) } }) }; o.stack.QueueBehavior = function (Z) { var ac, ad = [], ag = true, aa = "", af, X = 0, Y = false, ab = false; function ae() { if (Z.remove && ad.length === 0) { w(ac); return } if (ag || ad.length === 0 || af) { return } ag = true; var ah = ad.shift(); ac.down.outgoing(ah.data, ah.origin, function (ai) { ag = false; if (ah.callback) { K(function () { ah.callback(ai) }, 0) } ae() }) } return (ac = { init: function () { if (t(Z)) { Z = {} } if (Z.maxLength) { X = Z.maxLength; ab = true } if (Z.lazy) { Y = true } else { ac.down.init() } }, callback: function (ai) { ag = false; var ah = ac.up; ae(); ah.callback(ai) }, incoming: function (ak, ai) { if (ab) { var aj = ak.indexOf("_"), ah = parseInt(ak.substring(0, aj), 10); aa += ak.substring(aj + 1); if (ah === 0) { if (Z.encode) { aa = k(aa) } ac.up.incoming(aa, ai); aa = "" } } else { ac.up.incoming(ak, ai) } }, outgoing: function (al, ai, ak) { if (Z.encode) { al = H(al) } var ah = [], aj; if (ab) { while (al.length !== 0) { aj = al.substring(0, X); al = al.substring(aj.length); ah.push(aj) } while ((aj = ah.shift())) { ad.push({ data: ah.length + "_" + aj, origin: ai, callback: ah.length === 0 ? ak : null }) } } else { ad.push({ data: al, origin: ai, callback: ak }) } if (Y) { ac.down.init() } else { ae() } }, destroy: function () { af = true; ac.down.destroy() } }) }; o.stack.VerifyBehavior = function (ab) { var ac, aa, Y, Z = false; function X() { aa = Math.random().toString(16).substring(2); ac.down.outgoing(aa) } return (ac = { incoming: function (af, ad) { var ae = af.indexOf("_"); if (ae === -1) { if (af === aa) { ac.up.callback(true) } else { if (!Y) { Y = af; if (!ab.initiate) { X() } ac.down.outgoing(af) } } } else { if (af.substring(0, ae) === Y) { ac.up.incoming(af.substring(ae + 1), ad) } } }, outgoing: function (af, ad, ae) { ac.down.outgoing(aa + "_" + af, ad, ae) }, callback: function (ad) { if (ab.initiate) { X() } } }) }; o.stack.RpcBehavior = function (ad, Y) { var aa, af = Y.serializer || O(); var ae = 0, ac = {}; function X(ag) { ag.jsonrpc = "2.0"; aa.down.outgoing(af.stringify(ag)) } function ab(ag, ai) { var ah = Array.prototype.slice; return function () { var aj = arguments.length, al, ak = { method: ai }; if (aj > 0 && typeof arguments[aj - 1] === "function") { if (aj > 1 && typeof arguments[aj - 2] === "function") { al = { success: arguments[aj - 2], error: arguments[aj - 1] }; ak.params = ah.call(arguments, 0, aj - 2) } else { al = { success: arguments[aj - 1] }; ak.params = ah.call(arguments, 0, aj - 1) } ac["" + (++ae)] = al; ak.id = ae } else { ak.params = ah.call(arguments, 0) } if (ag.namedParams && ak.params.length === 1) { ak.params = ak.params[0] } X(ak) } } function Z(an, am, ai, al) { if (!ai) { if (am) { X({ id: am, error: { code: -32601, message: "Procedure not found."} }) } return } var ak, ah; if (am) { ak = function (ao) { ak = q; X({ id: am, result: ao }) }; ah = function (ao, ap) { ah = q; var aq = { id: am, error: { code: -32099, message: ao} }; if (ap) { aq.error.data = ap } X(aq) } } else { ak = ah = q } if (!r(al)) { al = [al] } try { var ag = ai.method.apply(ai.scope, al.concat([ak, ah])); if (!t(ag)) { ak(ag) } } catch (aj) { ah(aj.message) } } return (aa = { incoming: function (ah, ag) { var ai = af.parse(ah); if (ai.method) { if (Y.handle) { Y.handle(ai, X) } else { Z(ai.method, ai.id, Y.local[ai.method], ai.params) } } else { var aj = ac[ai.id]; if (ai.error) { if (aj.error) { aj.error(ai.error) } } else { if (aj.success) { aj.success(ai.result) } } delete ac[ai.id] } }, init: function () { if (Y.remote) { for (var ag in Y.remote) { if (Y.remote.hasOwnProperty(ag)) { ad[ag] = ab(Y.remote[ag], ag) } } } aa.down.init() }, destroy: function () { for (var ag in Y.remote) { if (Y.remote.hasOwnProperty(ag) && ad.hasOwnProperty(ag)) { delete ad[ag] } } aa.down.destroy() } }) }; b.easyXDM = o })(window, document, location, window.setTimeout, decodeURIComponent, encodeURIComponent);

function SocketSetup(remoteUrl, containerId, mode) {
    new easyXDM.Socket(
        {
            remote: remoteUrl,
            container: document.getElementById(containerId),
            swf: WebCounter.Util.getAssetHost() + '/SystemForms/Shared/Scripts/easyxdm.swf',
            onMessage: function(message, origin) {
                var params = message.toString().split('&&');
                var pairs = new Array();
                for (var i = 0; i < params.length; i++) {
                    var p = params[i].toString().split('@@');
                    pairs[p[0]] = p[1];
                }
                if (pairs['yandexgoals'] !== undefined) {
                    eval(pairs['yandexgoals']);
                }
                if (pairs['url'] !== undefined) {
                    if (pairs['url'] == 'reload')
                        window.location.href = window.location.href;
                    else
                        window.location.href = pairs['url'];
                }
                if (pairs['successmessage'] !== undefined) {
                    document.getElementById("WebCounter-dialog-container").innerHTML = '<div id="WebCounter-SuccessMessage">' + pairs['successmessage'] + '</div>';
                    WebCounter.Dialog.show(null);
                    document.getElementById('WebCounter-dialog-loader').style.display = 'none';
                }

                if (pairs['height'] != this.container.getElementsByTagName('iframe')[0].style.height.replace('px', '')
                    || (pairs['width'] !== undefined && pairs['width'] != this.container.getElementsByTagName('iframe')[0].style.width.replace('px', ''))) {

                    this.container.getElementsByTagName('iframe')[0].style.height = pairs['height'] + 'px';
                    
                    if (pairs['width'] !== undefined)
                        this.container.getElementsByTagName('iframe')[0].style.width = pairs['width'] + 'px';

                    document.getElementById('WebCounter-dialog').style.backgroundColor = pairs['backgroundcolor'];

                    if (WebCounter.Dialog.IsActive && mode != 0 && WebCounter.Dialog.ContainerId == containerId) {
                        WebCounter.Dialog.show(containerId);
                        //WebCounter.Dialog.repaint(containerId);
                    }
                    
                    if (mode == 1 && pairs['height'].toString() == "0") {
                        WebCounter.Dialog.preshow(containerId);
                    }

                    if (mode == 1 && pairs['height'].toString() != "0") {
                        //WebCounter.Dialog.preshow(containerId);
                        WebCounter.Dialog.show(containerId);
                        //WebCounter.Dialog.repaint(containerId);
                    }
                    
                    document.getElementById('WebCounter-dialog-loader').style.display = 'none';
                }
            }
        });
}