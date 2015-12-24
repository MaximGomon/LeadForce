function UpdateFormCode() {
    var formCode = '';

    $("#floatingbutton").hide();
    $("#autocall").hide();
    $("#popupform").hide();
    $("#closecall").hide();
    $("#rowShowDelayPopup").hide();
    $("#clear1").hide();
    $("#clear2").hide();    

    switch ($("#rblFormMode input:checked").val()) {
        case "0":
            $("#autocall").show();
            formCode = integratedFormTemplate.replace("$Mode", $("#rblFormMode input:checked").val())
                                             .replace("$Parameter", $("#txtParameter").val())
                                             .replace("$FromVisit", $("#txtShowFromVisit").val())
                                             .replace("$Through", $("#txtThrough").val())
                                             .replace("$Period", $("#rblPeriod input:checked").val() ? $("#rblPeriod input:checked").val() : "")
                                             .replace("$ContactCategory", $("#rblContactCategory input:checked").val() ? $("#rblContactCategory input:checked").val() : "");
            break;
        case "1":
            $("#popupform").show();
            //$("#closecall").show();
            formCode = popupFormTemplate.replace("$Parameter", $("#txtParameter").val())
                                        .replace("$PopupEffectAppear", $("#ddlPopupEffectAppear").val())
                                        .replace("$PopupAlign", $("#rblPopupAlign input:checked").val() ? $("#rblPopupAlign input:checked").val() : "");
            break;
        case "2":
            $("#autocall").show();
            $("#popupform").show();
            $("#rowShowDelayPopup").show();
            formCode = autocallFormTemplate.replace("$Mode", $("#rblFormMode input:checked").val())
                                           .replace("$Parameter", $("#txtParameter").val())
                                           .replace("$FromVisit", $("#txtShowFromVisit").val())
                                           .replace("$Through", $("#txtThrough").val())
                                           .replace("$Period", $("#rblPeriod input:checked").val() ? $("#rblPeriod input:checked").val() : "")
                                           .replace("$ContactCategory", $("#rblContactCategory input:checked").val() ? $("#rblContactCategory input:checked").val() : "")
                                           .replace("$PopupDelayAppear", $("#txtPopupDelayAppear").val())
                                           .replace("$PopupEffectAppear", $("#ddlPopupEffectAppear").val())
                                           .replace("$PopupAlign", $("#rblPopupAlign input:checked").val() ? $("#rblPopupAlign input:checked").val() : "");
            break;
        case "3":
            $("#floatingbutton").show();
            $("#autocall").show();
            $("#popupform").show();
            $("#clear1").show();

            var bg = $find("rcpFloatingButtonBackground").get_selectedColor();
            if (bg != null)
                bg = bg.replace("#", "");

            formCode = floatingButtonFormTemplate.replace("$Mode", $("#rblFormMode input:checked").val())
                                           .replace("$Parameter", $("#txtParameter").val())
                                           .replace("$FromVisit", $("#txtShowFromVisit").val())
                                           .replace("$Through", $("#txtThrough").val())
                                           .replace("$Period", $("#rblPeriod input:checked").val() ? $("#rblPeriod input:checked").val() : "")
                                           .replace("$ContactCategory", $("#rblContactCategory input:checked").val() ? $("#rblContactCategory input:checked").val() : "")
                                           .replace("$PopupEffectAppear", $("#ddlPopupEffectAppear").val())
                                           .replace("$PopupAlign", $("#rblPopupAlign input:checked").val() ? $("#rblPopupAlign input:checked").val() : "")
                                           .replace("$FloatingButtonName", $("#txtFloatingButtonName").val())
                                           .replace("$FloatingButtonIcon", $("#aSelectIcon").attr("rel"))
                                           .replace("$FloatingButtonBackground", bg)
                                           .replace("$FloatingButtonPosition", $("#ddlFloatingButtonPosition").val())
                                           .replace("$FloatingButtonMargin", $("#txtFloatingButtonMargin").val());
            break;
        case "4":
            $("#autocall").show();
            $("#popupform").show();
            $("#closecall").show();
            $("#clear2").show();
            formCode = callOnClosingFormTemplate.replace("$Mode", $("#rblFormMode input:checked").val())
                                                .replace("$Parameter", $("#txtParameter").val())
                                                .replace("$FromVisit", $("#txtShowFromVisit").val())
                                                .replace("$Through", $("#txtThrough").val())
                                                .replace("$Period", $("#rblPeriod input:checked").val() ? $("#rblPeriod input:checked").val() : "")
                                                .replace("$ContactCategory", $("#rblContactCategory input:checked").val() ? $("#rblContactCategory input:checked").val() : "")
                                                .replace("$PopupEffectAppear", $("#ddlPopupEffectAppear").val())
                                                .replace("$PopupAlign", $("#rblPopupAlign input:checked").val() ? $("#rblPopupAlign input:checked").val() : "")
                                                .replace("$DelayAppearOnClosing", $("#txtDelayAppearOnClosing").val())
                                                .replace("$SizeFieldOnClosing", $("#txtSizeFieldOnClosing").val());
            break;        
    }
    $("#formCode pre code").html(formCode);
    $('#formCode pre code').each(function (i, e) { hljs.highlightBlock(e); });
}
function getPublicationTypes() {
    return  $("#chxPublicationType input:checked").map(function () { return this.value; }).get().join(",");
}

function Decode(encoded) {
    return encoded.replace(/&amp;/g, '&').replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&quot;/g, '"').replace(/&#39;/g, "'");
}