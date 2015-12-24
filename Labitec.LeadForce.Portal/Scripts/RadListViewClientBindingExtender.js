/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Web.UI.Common.Core.js" assembly="Telerik.Web.UI"/>

(function () {
    Type.registerNamespace("Telerik.Web.UI");
    var $T = Telerik.Web.UI;

    $T.RadListViewClientBindingExtender = function (element) {
        $T.RadListViewClientBindingExtender.initializeBase(this, [element]);

        this._clientItemTemplate = "";
        this._itemContainerId = "";
        this._locationUrl = "";
        this._methodName = "";
        this._callBackFunction = "";
        this._itemContainer = null;
        this._currentPageIndex = 1;
        this._noMoreData = false;
        this._enableAutoLoad = false;
    }

    $T.RadListViewClientBindingExtender.prototype = {
        initialize: function () {
            $T.RadListViewClientBindingExtender.callBaseMethod(this, 'initialize');

            //get the itemContainer and hook to scroll event
            $(window).scroll(Function.createDelegate(this, this._handleScroll));

            // you may call loadData in case you want to populate the listView purly client-side 
            // Note: in such case make sure ListView is bound to at least one dummy item on the server
            if (this._enableAutoLoad)
                this._loadData();
        },

        dispose: function () {
            $T.RadListViewClientBindingExtender.callBaseMethod(this, 'dispose');
        },

        _handleScroll: function () {
            // if scroller is dragged to the bottom we request for more data
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                this._loadData();
            }
        },

        get_itemContainer: function () {
            if (this._itemContainer)
                return this._itemContainer;

            if (!this._itemContainerId || this._itemContainerId == "")
                throw new Error("There is no itemContainerId specified");
            return this._itemContainer = $("#" + this._itemContainerId);
        },

        _loadData: function () {
            if (this._isNullOrEmpty(this._locationUrl) || this._isNullOrEmpty(this._methodName) || this._noMoreData)
                return;
            $(".ajax-loader").parent().show();
            var url = String.format("{0}/{1}", this._locationUrl, this._methodName);
            var argumentsObject = { siteId: siteId, portalSettingsId: portalSettingsId, startIndex: this._currentPageIndex++, publicationType: $('#hfPublicationType').val(), publicationCategory: $('#hfPublicationCategory').val(), filter: $('#hfPublicationFilter').val() };
            try {
                $telerik.$.ajax({
                    type: "POST",
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: Sys.Serialization.JavaScriptSerializer.serialize(argumentsObject),
                    success: Function.createDelegate(this, this._bindContainer),
                    error: function (error) { alert(error); }
                });
            }
            catch (e) {
                throw new Error(e);
            }
        },

        _bindContainer: function (result) {
            $(".ajax-loader").parent().hide();
            var data = result.d ? result.d : result;
            if (data && data.length > 0) {
                var container = this.get_itemContainer();
                // container.html("");                    
                for (var i = 0, len = data.length; i < len; i++) {
                    var dataItem = data[i];
                    if ($('#' + dataItem.ID).html() == null) {
                        var itemHtml = this._constructTemplate(dataItem);
                        jQuery(container).append("clientItemTemplate", dataItem);
                    }

                }
                if (!this._isNullOrEmpty(this._callBackFunction)) {
                    eval(this._callBackFunction);
                }
            } else {
                this._noMoreData = true;
            }

        },

        _constructTemplate: function (dataItem) {
            if (this._isNullOrEmpty(this._clientItemTemplate))
                return "";

            if (!jQuery.templates.clientItemTemplate) {
                jQuery.templates.clientItemTemplate = jQuery.tmpl($telerik.$("<div>").html(this._clientItemTemplate).text());
            }
        },

        _isNullOrEmpty: function (value) {
            return !value || value === "";
        }
    }

    $T.RadListViewClientBindingExtender.registerClass('Telerik.Web.UI.RadListViewClientBindingExtender', Sys.UI.Behavior);
})();
