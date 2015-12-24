function LeadForceComment(handlerPath, fileUploadHandlerPath, contentId, commentType) {
    this.handlerPath = handlerPath;
    this.fileUploadHandlerPath = fileUploadHandlerPath;
    this.contentId = contentId;
    this.commentType = commentType;
    this.destinationUserId = '';
    this.replyToCommentId = '';
    this.callBackFunction = '';
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
        var data = Sys.Serialization.JavaScriptSerializer.serialize({ contentId: this.contentId, commentText: text, destinationUserId: this.destinationUserId, replyToCommentId: this.replyToCommentId, fileName: fileName, commentType: this.commentType });
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
                commentObject.ShowDestinations(result.UserID);
            }
        });
    };

    this.ShowDestinations = function (selectId) {
        var data = Sys.Serialization.JavaScriptSerializer.serialize({ contentId: this.contentId, commentType: this.commentType });
        var destinations = "#" + this.contentId + " .destinations";        
        if ($(destinations).css('display') == 'block' && (selectId === undefined || selectId == '')) {
            $(destinations + " .items").html('');
            $(destinations).hide();
            return;
        }
        $.ajax({
            url: this.handlerPath + "/GetContentCommentAuthors", data: data,
            success: function (msg) {
                var result = msg.d ? msg.d : msg;
                var element = $("#" + commentObject.contentId + " .show-destination");
                $(".destinations .items").html('');
                $(".destinations").hide();
                if (result && result.length > 0) {
                    for (var i = 0, len = result.length; i < len; i++) {
                        var dataItem = result[i];
                        if ($('#' + dataItem.UserID).html() == null) {
                            $(destinations).show();
                            $(destinations + " .items").append("#destination-template", dataItem);
                        }
                    }
                    $(destinations + " a").click(function () {
                        var isAlreadyChecked = false;
                        if ($(this).css('font-weight') == '700') {
                            isAlreadyChecked = true;
                            commentObject.destinationUserId = '';
                        }
                        $(destinations + " a").css('font-weight', 'normal');
                        if (!isAlreadyChecked) {
                            $(this).css('font-weight', '700');
                            commentObject.destinationUserId = $(this).attr('id');
                        }
                    });

                    if (selectId != '') {                        
                        $(destinations + ' #' + selectId).click();
                    }
                }
            }
        });
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

        $("textarea[class*=expand]").TextAreaExpander(20);
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
}