$(".ajax-loader").parent().ajaxStart(function () { $(this).show(); });
$(".ajax-loader").parent().ajaxComplete(function (event, request, settings) {$(this).hide();});
$(document).ready(function () {
    var searchPageIndex = 1;
    leadForceSearch = {
        text: $('#searchText').val(),
        handlerPath: '',
        search: function () {
            searchPageIndex = 1;
            this.handle("search");
        },
        toComment: function (url) {
            url = ' ' + url + ' ';
            var selector = '';
            if ($('#txtComment').size() > 0) selector = '#txtComment';
            else if ($('.tmp textarea').size() > 0) selector = '.tmp textarea';
            else if ($('.leave-comment textarea').size() > 0) {
                selector = '.leave-comment textarea';
                if (Comments.HTMLEditor != '')
                    Comments.HTMLEditor.set_html(Comments.HTMLEditor.get_html() + url);
            }
            $(selector).focus().val($(selector).val() + url);
            CloseChooseAnswerRadWindow();
        },
        input: function (text) {
            //            if (text == '') {
            //                $("#search-result-container").hide();
            //                $("#search-result").html('');
            //                return;
            //            }
            //if (this.text == text) return; else 
            this.text = text;
            this.search();
        },
        more: function () {
            searchPageIndex++;
            this.handle("more");
        },
        getPublication: function (element, publicationId) {
            var data = Sys.Serialization.JavaScriptSerializer.serialize({ siteId: $('#hfSiteId').val(), publicationId: publicationId });
            $.ajax({
                url: this.handlerPath + "/GetPublication",
                data: data,
                type: "POST", contentType: "application/json; charset=utf-8", dataType: "json",
                error: function () { $(".ajax-loader").parent().hide(); },
                success: function (msg) {
                    var result = msg.d ? msg.d : msg;
                    var publication = result;
                    $('#publication-text h3').html($(element).html());
                    $('#publication-text p').html(publication);
                    $('#steps').hide();
                    $('#publication-text').show();
                }
            });
        },
        handle: function (type) {
            var data = Sys.Serialization.JavaScriptSerializer.serialize({ siteId: $('#hfSiteId').val(), text: this.text, startIndex: searchPageIndex, publicationType: $('#hfPublicationType').val() });
            $.ajax({
                url: this.handlerPath + "/SearchPublicationForFeedback",
                data: data,
                type: "POST", contentType: "application/json; charset=utf-8", dataType: "json",
                error: function () { $(".ajax-loader").parent().hide(); },
                success: function (msg) {
                    var result = msg.d ? msg.d : msg;
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
                    else {
                        $("#search-result-container").show();
                        $("#search-result").html('<br/><p>Публикаций не найдено</p>');
                    }

                    var itemsCount = $("#search-result .search-item").length;

                    if (itemsCount < result.TotalCount) {
                        $("#more-result").css('visibility','visible');
                        $("#more-result a span").html(result.TotalCount - itemsCount);
                    }
                    else {
                        $("#more-result").css('visibility', 'hidden');
                        $("#more-result a span").html(0);
                    }
                }
            });
        }
    };
});