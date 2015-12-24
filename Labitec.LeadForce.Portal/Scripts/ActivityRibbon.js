$.ajaxSetup({ type: "POST", contentType: "application/json; charset=utf-8", dataType: "json" });
$(".ajax-loader").parent().ajaxStart(function () {$(this).show();});
$(".ajax-loader").parent().ajaxComplete(function (event, request, settings) {
    $(this).hide();
    $("textarea[class*=expand]").css('height', '22px');
});
function LikePublication(element, id) {    
    if (needLogin == true) { NeedLogin(); return; }
    var data = Sys.Serialization.JavaScriptSerializer.serialize({ publicationId: id });
    $.ajax({
        url: handlerPath + "/LikePublication",
        data: data,
        success: function (msg) {
            var likeCount = parseInt($("#" + id + " .stats .like span.count").html());
            if (msg.d == "like") {
                $(element).html("Больше не нравится");
                likeCount++;
            } else {
                $(element).html("Мне нравится");
                likeCount--;
            }
            $("#" + id + " .stats .like span.count").html(likeCount);
            $("#" + id + " .stats .like span.users").html(GetLikeUsersWithCase(likeCount));
        }
    });
}
function LikeComment(element, id, publicationId) {
    if (needLogin == true) { NeedLogin(); return; }
    var data = Sys.Serialization.JavaScriptSerializer.serialize({ commentId: id, publicationId: publicationId });
    $.ajax({
        url: handlerPath + "/LikeComment",
        data: data,
        success: function (msg) {
            var likeCount = parseInt($("#comment" + id + " .like span.count").html());
            if (msg.d == "like") {
                $(element).html("Больше не нравится");
                likeCount++;
            } else {
                $(element).html("Мне нравится");
                likeCount--;
            }
            $("#comment" + id + " .like span.count").html(likeCount);
        }
    });
}
function LeaveCommentSubmit(id, text, fileName) {    
    $("#lc" + id + " .warning-container").remove();
    var data = Sys.Serialization.JavaScriptSerializer.serialize({ publicationId: id, commentText: text, fileName: fileName });    
    $.ajax({ 
        url: handlerPath + "/LeaveComment",
        data: data,        
        success: function (msg) {            
            if (msg.d) {
                var result = msg.d ? msg.d : msg;                
                if (result.ErrorMessage == null || result.ErrorMessage == "") {
                    $("#cr" + id).show();
                    $("#cr" + id).append("#comment-template", result);
                    $("#stats" + id + " .comments .count").html(parseInt($("#stats" + id + " .comments .count").html()) + 1);
                    InitCommentInputs();
                }
                else if (result.ErrorMessage == "auth") {
                    NeedLogin();
                } else if (result.ErrorMessage == "access") {
                    $("#lc" + id).prepend("<div class=\"warning-container\"><div class=\"warning\">У вас недостаточно прав для комментирования записи.</div><br/></div>");
                    setTimeout(function () { $("#lc" + id + " .warning-container").fadeOut("slow"); }, 6000);
                }
            }
        }
    });
}
function SelectComments(id) {    
    var data = Sys.Serialization.JavaScriptSerializer.serialize({ publicationId: id });    
    $.ajax({
        url: handlerPath + "/GetComments",
        data: data,        
        success: function (msg) {
            var result = msg.d ? msg.d : msg;            
            if (result && result.length > 0) {
                for (var i = 0, len = result.length; i < len; i++) {
                    var dataItem = result[i];                    
                    if ($('#comment' + dataItem.ID).html() == null) {
                        $("#cr" + id).show().prepend("#comment-template", dataItem);
                    }
                }                
            }
        }
    });
}
function CheckOfficialAnswer(element, id, publicationId) {
    if (needLogin == true) { NeedLogin(); return; }

    var data = Sys.Serialization.JavaScriptSerializer.serialize({ commentId: id, publicationId: publicationId });
    $.ajax({
        url: handlerPath + "/CheckOfficialAnswer",
        data: data,        
        success: function (msg) {
            if (msg.d != "error") {                
                $("#" + publicationId + " .off-comment").show();
                $("#" + publicationId + " .off-comment > p").html($("#comment" + id + " p").html());
                $("#" + publicationId + " .comment-item").each(function() {
                    if ($(this).find(".official-answer").html() == null)
                        $(this).find(".comment-operations").append("<li class='official-answer'><a href='javascript:;' onclick='CheckOfficialAnswer(this, \"" + this.id.replace("comment", "") + "\", \"" + publicationId + "\")'>Официальный ответ</a></li>");
                });
                $(element).parent().remove();
            }
        }
    });
}
function LeaveComment(id) {
    if (needLogin == true) { NeedLogin(); return; }    
    $("#" + id + " .leave-comment input").focus();
}
function GetLikeUsersWithCase(likeCount) {    
    var likeCase = "пользователям";
    var lastChar = likeCount.toString().charAt(likeCount.toString().Length - 1);
    if (likeCount.toString().Length >= 2 && likeCount.toString().charAt(likeCount.toString().length - 2) == '1') { }
    else if (lastChar == '1') likeCase = "пользователю";
    else if (lastChar == '2' || lastChar == '3' || lastChar == '4') likeCase = "пользователям";

    return likeCase;
}
function InitCommentInputs() {
    $("textarea[class*=expand]").css('height', '22px');
    $("textarea[class*=expand]").TextAreaExpander(22);
    $('.leave-comment textarea').focus(function () {
        $(this).removeClass("idle-field").addClass("focus-field");
        if (this.value == this.defaultValue) { this.value = ''; }
        if (this.value != this.defaultValue) { this.select(); }
        $(this).parent().find("i").css("display", "block");
        $(this).parent().css("padding-bottom", "3px");
    });
    $('.leave-comment textarea').blur(function() {
        $(this).removeClass("focus-field").addClass("idle-field");
        if ($.trim(this.value) == '') { this.value = (this.defaultValue ? this.defaultValue : ''); }
        $(this).parent().find("i").css("display", "none");
        $(this).parent().css("padding-bottom", "9px");
    });
    $('.leave-comment textarea').keypress(function (event) {
        if ((event.ctrlKey) && ((event.keyCode == 0xA) || (event.keyCode == 0xD))) {
            if ($(this).val() != '' && $(this).val() != 'Оставить комментарий...') {
                if ($(this).parent().find('.upload-file input').val() != '')
                    ajaxFileUpload($(this).parent().find('.upload-file input').attr('id'), $(this).parent().find(".hf-publicationid").val(), $(this).val());
                else
                    LeaveCommentSubmit($(this).parent().find(".hf-publicationid").val(), $(this).val(), '');                
                if (needLogin == false) {
                    $(this).val('');
                    $(this).blur();
                }
            }
        }
    });
}

function ajaxFileUpload(fileUploadId, publicationId, commentText) {
    $.ajaxFileUpload({
        url: fileUploadHandlerPath,
        secureuri: false,
        fileElementId: fileUploadId,
        dataType: 'json',
        success: function (data, status) {
            if (typeof (data.error) != 'undefined') {
                if (data.error != '') {
                    alert(data.error);
                } else {
                    $('#' + fileUploadId).attr({ value: '' }); ;
                    ShowHideUpload($('#' + fileUploadId).parent());
                    LeaveCommentSubmit(publicationId, commentText, data.msg);
                    InitCommentInputs();
                }
            }
        },
        error: function (data, status, e) {
            alert(e);
        }
    });
    return false;
}

function ShowHideUpload(element) {
    var upload = $(element).parent().find('.upload-file');
    if (upload.css('display') == 'none') upload.show(); else upload.hide();
}

$(document).ready(function () {
    var searchPageIndex = 1;
    leadForceSearch = {
        text: $('#searchText').val(),
        search: function () {
            searchPageIndex = 1;
            this.handle("search");            
        },
        input: function (text) {
            if (text == '') {
                $("#search-result-container").hide();
                $("#search-result").html('');
                return;
            }
            if (this.text == text) return; else this.text = text;

            this.search();
        },
        more: function () {
            searchPageIndex++;
            this.handle("more");
        },
        handle: function (type) {
            var data = Sys.Serialization.JavaScriptSerializer.serialize({ siteId: siteId, portalSettingsId: portalSettingsId, text: this.text, startIndex: searchPageIndex });
            $(".ajax-loader").parent().show();
            $.ajax({                
                type: "POST", contentType: "application/json; charset=utf-8", dataType: "json",
                url: handlerPath + "/SearchPublication",
                data: data,                
                error: function () { $(".ajax-loader").parent().hide(); },
                success: function (msg) {
                    var result = msg.d ? msg.d : msg;
                    $(".ajax-loader").parent().hide();
                    var publications = result.Publications;
                    if (type != "more")
                        $("#search-result").html('');
                    if (publications && publications.length > 0) {
                        $("#search-result-container").show();
                        for (var i = 0, len = publications.length; i < len; i++) {
                            var dataItem = publications[i];
                            $("#search-result").append("#search-item-template", dataItem);
                        }
                    }
                    else
                        $("#search-result-container").hide();

                    var itemsCount = $("#search-result .search-item").length;

                    if (itemsCount < result.TotalCount) {
                        $("#more-result").show();
                        $("#more-result a span").html(result.TotalCount - itemsCount);
                    }
                    else {
                        $("#more-result").hide();
                        $("#more-result a span").html(0);
                    }
                }
            });
        }
    };
});