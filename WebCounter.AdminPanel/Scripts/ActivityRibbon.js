$.ajaxSetup({type: "POST",contentType: "application/json; charset=utf-8",dataType: "json"});
$(".ajax-loader").parent().ajaxStart(function () { $(this).show();});
$(".ajax-loader").parent().ajaxComplete(function (event, request, settings) {
    $(this).hide();
    $("textarea[class*=expand]").css('height', '20px');
});
function LikePublication(element, id) {
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
    var data = Sys.Serialization.JavaScriptSerializer.serialize({ publicationId: id, commentText: text, fileName: fileName }); 
    $.ajax({        
        url: handlerPath + "/LeaveComment",
        data: data,        
        success: function (msg) {
            var result = msg.d ? msg.d : msg;            
            $("#cr" + id).show();
            $("#cr" + id).append("#comment-template", result);
            $("#stats" + id + " .comments .count").html(parseInt($("#stats" + id + " .comments .count").html()) + 1);
            InitCommentInputs();
        }
    });
}
function EditComment(commentId) {
    var data = Sys.Serialization.JavaScriptSerializer.serialize({ commentId: commentId });
    $.ajax({        
        url: handlerPath + "/GetCommentText",
        data: data,        
        success: function (msg) {
            var result = msg.d ? msg.d : msg;            
            $("#comment" + commentId + " .comment-text").hide();
            $("<div class='tmp'><textarea class='focus-field expand'>" + result + "</textarea><br/><a href='javascript:;' onclick=\"UpdateComment('"+commentId+"')\">Обновить</a> <a href='javascript:;' onclick=\"Cancel('"+ commentId +"')\">Отмена</a></div>").insertBefore($("#comment" + commentId + " .comment-text"));
            //$("textarea[class*=expand]").TextAreaExpander(22);
	    $('textarea[class*=expand]').autogrow();
        }
    });
}
function Cancel(commentId) {
    $("#comment" + commentId + " .tmp").remove();
    $("#comment" + commentId + " .comment-text").show();
}
function UpdateComment(commentId) {
    var data = Sys.Serialization.JavaScriptSerializer.serialize({ commentId: commentId, commentText: $("#comment" + commentId + " .tmp textarea").val() });
    $.ajax({        
        url: handlerPath + "/UpdateCommentText",
        data: data,        
        success: function (msg) {
            var result = msg.d ? msg.d : msg;    
            $("#comment" + commentId + " .comment-text").html(result);
            Cancel(commentId);            
        }
    });
}
function DeleteComment(commentId, publicationId) {
    if (!confirm("Вы действительно хотите удалить комментарий?"))
        return;    
    var data = Sys.Serialization.JavaScriptSerializer.serialize({ commentId: commentId });    
    $.ajax({        
        url: handlerPath + "/DeleteComment",
        data: data, 
        success: function (msg) {
            var result = msg.d ? msg.d : msg;            
            if (result == true) {
                $("#cr" + publicationId).html('');
                SelectComments(publicationId);
                $("#stats" + publicationId + " .comments .count").html(parseInt($("#stats" + publicationId + " .comments .count").html()) - 1);
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
                        $("#cr" + id).show();
                        $("#cr" + id).prepend("#comment-template", dataItem);
                    }
                }                
            }
        }
    });
}
function CheckOfficialAnswer(element, id, publicationId) {
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
    $("textarea[class*=expand]").css('height','20px');
    //$("textarea[class*=expand]").TextAreaExpander(20);
    $('textarea[class*=expand]').autogrow();
    $('.leave-comment textarea').focus(function () {
        $(this).removeClass("idle-field").addClass("focus-field");
        if (this.value == this.defaultValue) { this.value = ''; }
        if (this.value != this.defaultValue) { this.select(); }
        $(this).parent().find("i").css("display", "block");
        $(this).parent().css("padding-bottom", "3px");
    });
    $('.leave-comment textarea').blur(function () {
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

                $(this).val('');
                $(this).blur();
                InitCommentInputs();
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
                    $("textarea[class*=expand]").css('height', '22px');
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