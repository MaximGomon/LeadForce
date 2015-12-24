function LeadForceComment(handlerPath, fileUploadHandlerPath, contentId, commentType, isHtmlEditEnabled, htmlEditorId) {
    this.handlerPath = handlerPath;
    this.fileUploadHandlerPath = fileUploadHandlerPath;
    this.contentId = contentId;
    this.commentType = commentType;
    this.destinationUserId = '';
    this.replyToCommentId = '';    
    this.callBackFunction = '';
    this.isHtmlEditEnabled = isHtmlEditEnabled;
    this.htmlEditorId = htmlEditorId;
    this.HTMLEditor = '';
    this.LeaveCommentCalled = false;

    var commentObject = this;

    $.ajaxSetup({ type: "POST", contentType: "application/json; charset=utf-8", dataType: "json" });
    $(".ajax-loader").parent().ajaxStart(function () { $(this).show(); });
    $(".ajax-loader").parent().ajaxComplete(function (event, request, settings) {
        $(this).hide();        
        commentObject.InitCommentInputs();
    });

    this.SelectComments = function() {
        var data = Sys.Serialization.JavaScriptSerializer.serialize({ contentId: this.contentId, commentType: this.commentType });
        $.ajax({
            url: this.handlerPath + "/GetComments",
            data: data,
            success: function(msg) {
                var result = msg.d ? msg.d : msg;
                if (result && result.length > 0) {
                    for (var i = 0, len = result.length; i < len; i++) {
                        var dataItem = result[i];
                        if ($('#comment' + dataItem.ID).html() == null) {
                            $("#cr" + contentId).show().prepend("#comment-template", dataItem, { IsHtmlEnabled: commentObject.isHtmlEditEnabled });
                        }
                    }
                }
            }
        });
    };

    this.LeaveCommentSubmit = function (text, fileName) {
        var data = Sys.Serialization.JavaScriptSerializer.serialize({ contentId: this.contentId, commentText: text, destinationUserId: this.destinationUserId, replyToCommentId: this.replyToCommentId, fileName: fileName, commentType: this.commentType, isInternal: $('#chxIsInternal').is(':checked') });
        $.ajax({
            url: this.handlerPath + "/LeaveComment",
            data: data,
            success: function (msg) {
                if (msg.d) {
                    var result = msg.d ? msg.d : msg;
                    $("#cr" + contentId).show().append("#comment-template", result, { IsHtmlEnabled: commentObject.isHtmlEditEnabled });
                    $("#stats" + contentId + " .comments .count").html(parseInt($("#stats" + contentId + " .comments .count").html()) + 1);
                    $(".destinations").hide();
                    commentObject.destinationUserId = '';
                    commentObject.replyToCommentId = '';
                    commentObject.LeaveCommentCalled = false;
                    $('#chxIsInternal').removeAttr('checked');                    
                    $("textarea[class*=expand]").css('height', '20px');
                    if (commentObject.callBackFunction != '')
                        eval(commentObject.callBackFunction);
                }                
            }
        });
    };

    this.LikeComment = function(element, id) {
        var data = Sys.Serialization.JavaScriptSerializer.serialize({ contentId: this.contentId, commentId: id, commentType: this.commentType });
        $.ajax({
            url: this.handlerPath + "/LikeComment",
            data: data,
            success: function(msg) {
                var currentLikeCount = parseInt($("#comment" + id + " .like span.count").html());
                if (msg.d == "1") {
                    $(element).html("Больше не нравится");
                    currentLikeCount++;
                } else {
                    $(element).html("Мне нравится");
                    currentLikeCount--;
                }
                $("#comment" + id + " .like span.count").html(currentLikeCount);
            }
        });
    };

    this.CheckOfficialAnswer = function (element, id) {
        var data = Sys.Serialization.JavaScriptSerializer.serialize({ contentId: this.contentId, commentId: id, commentType: this.commentType });
        $.ajax({
            url: this.handlerPath + "/CheckOfficialAnswer",
            data: data,
            success: function (msg) {
                if (msg.d != "error") {
                    $("#" + contentId + " .off-comment").show();
                    $("#" + contentId + " .off-comment > p").html($("#comment" + id + " p").html());
                    $("#" + contentId + " .comment-item").each(function () {
                        if ($(this).find(".official-answer").html() == null) {
                            $(this).find(".comment-operations").append("<li class='official-answer'><a href='javascript:;'>Официальный ответ</a></li>");
                            var commentId = this.id.replace("comment", "");
                            $(this).find(".comment-operations .official-answer a").click(function () {
                                commentObject.CheckOfficialAnswer(this, commentId);
                            });
                        }
                    });
                    $(element).parent().remove();
                }
            }
        });
    };

    this.EditComment = function (commentId) {
        if ($("#comment" + commentId + " .tmp").length != 0)
            return;

        var data = Sys.Serialization.JavaScriptSerializer.serialize({ commentId: commentId, commentType: this.commentType });
        $.ajax({
            url: this.handlerPath + "/GetComment",
            data: data,
            success: function (msg) {
                var result = msg.d ? msg.d : msg;
                $("#comment" + commentId + " .comment-text").hide();
                $("<div class='tmp'><textarea class='focus-field expand'>" + result.Comment + "</textarea><br/><a href='javascript:;' class='update'>Обновить</a> <a href='javascript:;' class='cancel'>Отмена</a></div>").insertBefore($("#comment" + commentId + " .comment-text"));
                $(".tmp .update").click(function () { commentObject.UpdateComment(commentId); });
                $(".tmp .cancel").click(function () { commentObject.Cancel(commentId); });
            }
        });
    };

    this.EditHTMLComment = function (commentId) {
        if ($("#comment" + commentId + " .tmp").length != 0 || $("#htmlEditor").css('display') == 'block')
            return;

        var data = Sys.Serialization.JavaScriptSerializer.serialize({ commentId: commentId, commentType: this.commentType });
        $.ajax({
            url: this.handlerPath + "/GetComment",
            data: data,
            success: function (msg) {
                var result = msg.d ? msg.d : msg;
                $("#comment" + commentId + " .comment-text").hide();
                $("<div class='tmp'><div class='clear'></div><br/><a href='javascript:;' class='update'>Обновить</a> <a href='javascript:;' class='cancel'>Отмена</a><br/></div>").insertBefore($("#comment" + commentId + " .comment-text"));
                $("#comment" + commentId + " .tmp").prepend($("#htmlEditor"));
                HTMLEditor = eval(htmlEditorId + '_Editor()');                
                HTMLEditor.set_html(result.Comment);
                eval(htmlEditorId + "_setupEditor()");
                $("#htmlEditor").show();
                $("#comment" + commentId + " .update").click(function () { commentObject.UpdateComment(commentId); });
                $("#comment" + commentId + " .cancel").click(function () { commentObject.Cancel(commentId); });
            }
        });
    };

    this.Cancel = function (commentId) {
        $('#htmlEditorContainer').append($("#htmlEditor"));
        $("#htmlEditor").hide();
        $("#comment" + commentId + " .tmp").remove();
        $("#comment" + commentId + " .comment-text").show();        
    };

    this.UpdateComment = function (commentId) {
        var commentText = '';
        if ($("#comment" + commentId + " .tmp #htmlEditor").length > 0)
            commentText = HTMLEditor.get_html();
        else
            commentText = $("#comment" + commentId + " .tmp textarea").val();

        var data = Sys.Serialization.JavaScriptSerializer.serialize({ commentId: commentId, commentText: commentText, commentType: this.commentType });
        $.ajax({
            url: this.handlerPath + "/UpdateCommentText",
            data: data,
            success: function (msg) {
                var result = msg.d ? msg.d : msg;
                $("#comment" + commentId + " .comment-text").html(result);
                commentObject.Cancel(commentId);
            }
        });
    };
    this.DeleteComment = function (commentId) {
        if (!confirm("Вы действительно хотите удалить комментарий?"))
            return;
        var data = Sys.Serialization.JavaScriptSerializer.serialize({ commentId: commentId, commentType: this.commentType });        
        $.ajax({
            url: this.handlerPath + "/DeleteComment",
            data: data,
            success: function (msg) {
                var result = msg.d ? msg.d : msg;
                if (result == true) {
                    $("#cr" + contentId).html('');
                    commentObject.SelectComments(contentId);
                    $("#stats" + contentId + " .comments .count").html(parseInt($("#stats" + contentId + " .comments .count").html()) - 1);
                }
            }
        });
    };

    this.ShowHideAddEditor = function () {
        this.HTMLEditor = eval(htmlEditorId + '_Editor()');
        this.HTMLEditor.enableEditing(true);
        if ($("#htmlEditor").css('display') != 'block') {
            $('#editorContainer').prepend($("#htmlEditor"));
            eval(htmlEditorId + "_setupEditor()");
            $("#htmlEditor").show();
            if ($("#textEditor").val() != '' && $("#textEditor").val() != 'Оставить комментарий...')
                this.HTMLEditor.set_html($("#textEditor").val());
            else
                this.HTMLEditor.set_html('');

            $("#textEditor").hide();
            $("#editorContainer i").show();
        } else if (this.HTMLEditor.get_html() == '<br>' || this.HTMLEditor.get_html() == '') {
            $('#htmlEditorContainer').append($("#htmlEditor"));
            $("#htmlEditor").hide();
            $("#textEditor").show();
            $("#textEditor").val('').focus().blur();
            $("#editorContainer i").hide();
        }
    };

    this.Reply = function (commentId) {
        var data = Sys.Serialization.JavaScriptSerializer.serialize({ commentId: commentId, commentType: this.commentType });
        $.ajax({
            url: this.handlerPath + "/GetComment",
            data: data,
            success: function (msg) {
                var result = msg.d ? msg.d : msg;
                $("#" + contentId + " .leave-comment textarea").focus();
                $("#" + contentId + " .leave-comment textarea").val(result.Comment);
                commentObject.replyToCommentId = commentId;
                commentObject.destinationUserId = result.UserID;
                commentObject.ShowDestination(result.UserFullName);
            }
        });
    };
    
    this.ShowDestination = function(userFullName) {
        var destinations = "#" + this.contentId + " .destinations";
        $(destinations + " .items").html(userFullName);
        $(destinations).show();
    };

    this.ReplyToUser = function (element) {        
        commentObject.destinationUserId = $(element).attr('rel');        
        commentObject.ShowDestination($(element).html());
    };

    this.ClearReply = function () {
        var destinations = "#" + this.contentId + " .destinations";
        commentObject.destinationUserId = '';
        commentObject.replyToCommentId = '';        
        $(destinations + " .items").html('');
        $(destinations).hide();
    };    

    this.ajaxFileUpload = function(fileUploadId, commentText) {
        $.ajaxFileUpload({
            url: this.fileUploadHandlerPath,
            secureuri: false,
            fileElementId: fileUploadId,
            dataType: 'json',
            data: { commentType: this.commentType },
            success: function(data, status) {
                if (typeof(data.error) != 'undefined') {
                    if (data.error != '') {
                        alert(data.error);
                    } else {
                        $('#' + fileUploadId).attr({ value: '' });
                        commentObject.ShowHideUpload($('#' + fileUploadId).parent());
                        commentObject.LeaveCommentSubmit(commentText, data.msg);
                    }
                }
            },
            error: function(data, status, e) {
                alert(e);
            }
        });
        return false;
    };

    this.ShowHideUpload = function() {
        var upload = $('#editorContainer .upload-file');
        if (upload.css('display') == 'none') upload.show(); else upload.hide();
    };

    this.InitCommentInputs = function () {
        //$("textarea[class*=expand]").TextAreaExpander(20);
        $('textarea[class*=expand]').autogrow();
        $('.leave-comment textarea').focus(function () {
            $(this).removeClass("idle-field").addClass("focus-field");
            if (this.value == this.defaultValue) {
                this.value = '';
            }
            if (this.value != this.defaultValue) {
                this.select();
            }
            $(this).parent().find("i").css("display", "block");
            //$(this).parent().css("padding-bottom", "3px");
        });
        $('.leave-comment textarea').blur(function () {
            $(this).removeClass("focus-field").addClass("idle-field");
            if ($.trim(this.value) == '') {
                this.value = (this.defaultValue ? this.defaultValue : '');
            }
            $(this).parent().find("i").css("display", "none");
            //$(this).parent().css("padding-bottom", "9px");
        });
        $('.leave-comment textarea').keypress(function (event) {
            if ((event.ctrlKey) && ((event.keyCode == 0xA) || (event.keyCode == 0xD)) && !commentObject.LeaveCommentCalled) {
                commentObject.CallLeaveComment(this,'');
            }
        });
    };
    this.CallLeaveComment = function (element, callBackFunction) {
        if (($(element).val() != '' && $(element).val() != 'Оставить комментарий...') || ($(element).parent().find('.upload-file input').val() != '')) {
            commentObject.callBackFunction = callBackFunction;
            if ($(element).parent().find('.upload-file input').val() != '') {
                if ($(element).val() == 'Оставить комментарий...')
                    $(element).val('');
                commentObject.LeaveCommentCalled = true;
                commentObject.ajaxFileUpload($(element).parent().find('.upload-file input').attr('id'), $(element).val());
            } else {
                commentObject.LeaveCommentCalled = true;
                commentObject.LeaveCommentSubmit($(element).val(), '');
            }
            $(element).val('');
            $(element).blur();
            commentObject.InitCommentInputs();

            return false;
        }
        return true;
    };
    this.HTMLOnKeyUp = function (event, editor) {
        if ($('.leave-comment #htmlEditor').length > 0)
            $('#textEditor').val(editor.get_html().replace('<p><br></p><br>', '').replace('<p>&nbsp;</p>', ''));        
    };
    this.HTMLOnKeyPress = function (event, editor) {
        if ($('.leave-comment #htmlEditor').length > 0)
        if ((event.ctrlKey) && ((event.keyCode == 0xA) || (event.keyCode == 0xD))) {
            editor.enableEditing(false);
            $('#textEditor').val(editor.get_html().replace('<p><br></p><br>', '').replace('<p>&nbsp;</p>', ''));
            editor.set_html('');
            commentObject.CallLeaveComment($('#textEditor'), '');
            commentObject.ShowHideAddEditor();
        }
    };
}