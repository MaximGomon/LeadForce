if (!WebCounter) {
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

            if (Mode == 1)
                Parameter = arguments[2] == undefined ? "" : arguments[2];
            else
                Parameter = arguments[5] == undefined ? "" : arguments[5];


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
            WebCounter.Dialog.init();

            var ajaxForm = new WebCounter.Ajax.Form();
            ajaxForm.id = "wcform-" + WebCounter.Util.guid();
            ajaxForm.jsonData = '{"SiteID":"' + this.SiteID + '","ContactID":"' + this.ContactID + '","ActivityCode":"' + ActivityCode + '","Mode":"' + Mode + '","FromVisit":"' + FromVisit + '","Through":"' + Through + '","Period":"' + Period + '","Parameter":"' + Parameter + '","register":"true"}';
            ajaxForm.send(WebCounter.Util.getAssetHost() + "/CounterService.svc/LG_FormService");
            if (Mode == 0) {
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
                }
                else inputs[i].parentNode.style.backgroundColor = "#fff";
            }

            var textareas = form.getElementsByTagName("textarea");
            for (i = 0; i < textareas.length; i++) {
                if (textareas[i].value == "" && textareas[i].getAttribute("rel") == "required") {
                    textareas[i].parentNode.style.backgroundColor = "#ffebe8";
                    errorCount++;
                }
                else textareas[i].parentNode.style.backgroundColor = "#fff";
            }

            var selects = form.getElementsByTagName("select");
            for (i = 0; i < selects.length; i++) {
                if (selects[i].options[selects[i].selectedIndex].className == "" && selects[i].options[selects[i].selectedIndex].getAttribute("rel") == "required") {
                    selects[i].parentNode.style.backgroundColor = "#ffebe8";
                    errorCount++;
                }
                else selects[i].parentNode.style.backgroundColor = "#fff";
            }

            if (errorCount > 0) return;

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
            var ActivityCode = arguments[0];
            var Parameter = arguments[1] == undefined ? "" : arguments[1];
            var ajaxFormServiceCancel = new WebCounter.Ajax.FormServiceCancel();
            ajaxFormServiceCancel.jsonData = '{"SiteID":"' + this.SiteID + '","ContactID":"' + this.ContactID + '","ActivityCode":"' + ActivityCode + '","Parameter":"' + Parameter + '"}';
            ajaxFormServiceCancel.send(WebCounter.Util.getAssetHost() + "/CounterService.svc/LG_FormServiceCancel");
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

        CounterCalled: false
    }

    var options = {
        overlayColor: '#000', // HEX color
        overlayOpacity: 0.7, // digit from 0 to 1
        overlayClose: false, // true or false
        zIndex: 1000, // digit more 1
        dialogZIndex: 1008,
        dialogCloseZIndex: 1009,
        overlayZIndex: 1007,
        imagesPath: null
    }
}



WebCounter.Overlay = {
    id: 'WebCounter-overlay',

    init: function () {
        if (document.getElementById(this.id) == null) {
            var overlay = document.createElement('div');
            overlay.setAttribute('id', this.id);
            if (options['overlayClose'] == true)
                overlay.setAttribute('onclick', 'WebCounter.Dialog.hide()');
            document.body.insertBefore(overlay, document.body.firstChild);
            WebCounter.Util.includeCss(WebCounter.Util.render(this.css_template, options));
        }
    },

    show: function () {
        document.getElementById(this.id).style.display = 'block';
    },

    hide: function () {
        document.getElementById(this.id).style.display = 'none';
    },

    css_template: "\
		#WebCounter-overlay { background-color: #{overlayColor}; position: fixed; top: 0; left: 0; filter: alpha(opacity='#{alphaOverlayOpacity}'); opacity: #{overlayOpacity}; z-index: #{overlayZIndex}; width: 100%; height: 100%; display: none; }\
	"
}



WebCounter.Form = {
    isInited: false,

    init: function () {
        var IE6 = false/*@cc_on || @_jscript_version < 5.7@*/;
        if (!IE6)
            options["backgroundGradient"] = 'background: url(' + WebCounter.Util.getAssetHost() + '/images' + '/sprite.png) 0 -70px no-repeat;';
        
        WebCounter.Util.includeCss(WebCounter.Util.render(this.css_template, options));
    },

    css_template: "\
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
        .wcform .btn { width: 120px; height: 27px; display: inline-block; font-size: 13px; font-weight: normal; line-height: 27px; text-decoration: none!important; text-align: center; color: #fff; background-color: #64c400; -webkit-border-radius: 16px; -moz-border-radius: 16px; border-radius: 16px; }\
        .wcform .btn span { width: 120px; height: 27px; display: inline-block; #{backgroundGradient} }\
        .wcform a:hover.btn span { background-position: 0 -97px; } \
        .wcform a:active.btn span { background-position: 0 -124px; } \
        .wcform .hide { display: none!important; }\
.wcform .clear { clear: both; height: 0; line-height: 0; font-size: 0 }\
.wcform .clearfix { zoom: 1; }\
.wcform .clearfix:after { content: ' '; clear: both; display: block; width: 0; height: 0; overflow: hidden; font-size: 0; }\
	",

    toggleForm: function (className) {
        var div = document.getElementsByClassName(className)[0];
        if (div.className.indexOf('hide') == -1)
            div.className = div.className + ' hide';
        else
            div.className = div.className.replace(' hide', '');
    }
}



WebCounter.Dialog = {
    id: 'WebCounter-dialog',

    ActivityCode: null,

    Parameter: null,

    init: function () {
        if (document.getElementById(this.id) == null) {
            var html = '<a href="javascript:;" id="WebCounter-dialog-close" onclick="WebCounter.LG_FormServiceCancel(\'' + this.ActivityCode + '\', \'' + this.Parameter + '\'); WebCounter.Dialog.hide()"><span>close</span></a>'
					+ '<div id="WebCounter-dialog-container" style="clear:both;"></div>';
            var dialog = document.createElement('div');
            dialog.setAttribute('id', this.id);
            dialog.innerHTML = html;
            document.body.insertBefore(dialog, document.body.firstChild);

            var IE6 = false/*@cc_on || @_jscript_version < 5.7@*/;
            if (!IE6)
                options["backgroundGradient"] = 'background: url(' + WebCounter.Util.getAssetHost() + '/images' + '/sprite.png) 0 -70px no-repeat;';
            WebCounter.Util.includeCss(WebCounter.Util.render(this.css_template, options));
        }
        else
            document.getElementById('WebCounter-dialog-close').setAttribute('onclick', 'WebCounter.LG_FormServiceCancel(\'' + this.ActivityCode + '\', \'' + this.Parameter + '\'); WebCounter.Dialog.hide()');
    },

    show: function () {
        WebCounter.Overlay.show();
        document.getElementById(this.id).style.display = 'block';
        var formWidth = document.getElementById(this.id).getElementsByTagName("form")[0].offsetWidth;
        var formHeight = document.getElementById(this.id).getElementsByTagName("form")[0].offsetHeight;
        document.getElementById("WebCounter-dialog").style.width = formWidth + 'px';
        document.getElementById("WebCounter-dialog").style.marginLeft = '-' + (formWidth / 2) + 'px';
        document.getElementById("WebCounter-dialog").style.height = formHeight + 'px';
        document.getElementById("WebCounter-dialog").style.marginTop = '-' + (formHeight / 2) + 'px';
    },

    hide: function () {
        document.getElementById(this.id).style.display = 'none';
        WebCounter.Overlay.hide();
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
        #WebCounter-dialog .btn { width: 120px; height: 27px; display: inline-block; font-size: 13px; font-weight: normal; line-height: 27px; text-decoration: none!important; text-align: center; color: #fff; background-color: #64c400; -webkit-border-radius: 16px; -moz-border-radius: 16px; border-radius: 16px; }\
        #WebCounter-dialog .btn span { width: 120px; height: 27px; display: inline-block; #{backgroundGradient} }\
        #WebCounter-dialog a:hover.btn span { background-position: 0 -97px; } \
        #WebCounter-dialog a:active.btn span { background-position: 0 -124px; } \
        #WebCounter-dialog .hide { display: none; }\
#WebCounter-dialog .clear { clear: both; height: 0; line-height: 0; font-size: 0 }\
#WebCounter-dialog .clearfix { zoom: 1; }\
#WebCounter-dialog .clearfix:after { content: ' '; clear: both; display: block; width: 0; height: 0; overflow: hidden; font-size: 0; }\
	"
}


WebCounter.Util = {
    sslAssetHost: "https://svc-demo.leadforce.ru",

    assetHost: "http://svc-demo.leadforce.ru",

    getAssetHost: function () {
        return ("https:" == document.location.protocol) ? this.sslAssetHost : this.assetHost;
    },

    render: function (template, params) {
        return template.replace(/\#{([^{}]*)}/g, function (a, b) {
            var r = params[b];
            return typeof r === 'string' || typeof r === 'number' ? r : a;
        })
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
}

WebCounter.Callback = {
    Counter: function () {
        while (funcqueue.length > 0) {
            (funcqueue.shift())();
        }
    },

    Form: function (result) {
        if (result != null) {
            if (result.Mode == 0)
                document.getElementById(result.ContainerID).innerHTML = result.Value.replace(/\[BR\]/g, "\n");
            if (result.Mode == 1 || result.Mode == 2) {
                document.getElementById("WebCounter-dialog-container").innerHTML = result.Value.replace(/\[BR\]/g, "\n");
                WebCounter.Dialog.show();
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
}

fsr = function (result) {
    window.location = result.Value;
}

WebCounter.Ajax = {
    Counter: function () {
        this.jsonData = "";

        this.send = function (url) {
            url = url + '?' + JSON.toQuerystring(this.jsonData);
            WebCounter.Util.sendJSONP(url, "WebCounter.Callback.Counter");
        }
    },

    Link: function () {
        this.jsonData = "";

        this.send = function (url) {
            url = url + '?' + JSON.toQuerystring(this.jsonData);
            WebCounter.Util.sendJSONP(url, "WebCounter.Callback.Link");
        }
    },

    Form: function () {
        this.id = "";

        this.jsonData = "";

        this.send = function (url) {
            url = url + '?' + JSON.toQuerystring(this.jsonData) + '&ContainerID=' + this.id;
            WebCounter.Util.sendJSONP(url, "WebCounter.Callback.Form");
        }
    },

    FormServiceResult: function () {
        this.jsonData = "";

        this.send = function (url) {
            url = url + '?' + JSON.toQuerystring(this.jsonData);
            //WebCounter.Util.sendJSONP(url, "WebCounter.Callback.FormServiceResult");
            WebCounter.Util.sendJSONP(url, "fsr");
        }
    },

    FormServiceCancel: function () {
        this.jsonData = "";

        this.send = function (url) {
            url = url + '?' + JSON.toQuerystring(this.jsonData);
            WebCounter.Util.sendJSONP(url, null);
        }
    },

    LinkProcessing: function () {
        this.jsonData = "";

        this.send = function (url) {
            url = url + '?' + JSON.toQuerystring(this.jsonData);
            WebCounter.Util.sendJSONP(url, "WebCounter.Callback.LinkProcessingResult");
        }
    }
}



WebCounter.Cookie = {
    setCookie: function (name, value, expires, path, domain, secure) {
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


    getCookie: function (name) {
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

}

JSON = {
    parse: function (str) {
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
            if (key == 'FormData') {
                for (var i = 0; i < data[key].length; i++) {
                    var keyFormData = data[key][i];
                    if (i > 0) formData += ",";
                    formData += '{"N":"' + keyFormData.N + '","V":"' + keyFormData.V + '"}';
                }
                /*for (var keyFormData in data[key]) {
                    if (keyFormData > 0) formData += ",";
                    formData += '{"N":"' + data[key][keyFormData].N + '","V":"' + data[key][keyFormData].V + '"}';
                }*/
                params.push('FormData=[' + formData + ']');
            }
            else {
                params.push(key + '=' + encodeURIComponent(val));
            }
        }
        return params.join("&");
    }
}

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

var wrapFunction = function (fn, context, params) {
    return function () {
        fn.apply(context, params);
    };
}

var lfCalled = false;
LeadForce = function (params) {
    if (lfCalled) return;
    lfCalled = true;
    
    var newParams;
    for (var i = 0; i < params.length; i++) {
        newParams = new Array();
        if (params[i][0].toLowerCase() == 'webcounter.lg_counter') {
            for (var j = 1; j < params[i].length; j++)
                newParams.push('"' + params[i][j] + '"');
            eval(params[i][0] + '(' + newParams.join(',') + ');');
        }
        else {
            for (j = 1; j < params[i].length; j++)
                newParams.push(params[i][j]);
            funcqueue.push(wrapFunction(eval(params[i][0]), WebCounter, newParams));
        }
    }
}

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