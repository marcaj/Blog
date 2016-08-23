function getData(url, onSuccess) {
    $.ajax({
        url: url,
        method: 'get'
    }).success(onSuccess);
}

function getPost(i) {
    getData('http://localhost:49408/Post/GetPosts?offset=' + i, function (data) {
        displayPostFromJson(data[0]);
        postIndex++;
    });
}

function displayPostFromJson(data) {
    if (data) {
        var template = $('#postTemplate').clone().html(),
            post = template.replace(/_POST_Id_/g, data.Id)
                .replace(/_POST_Title_/g, data.Title)
                .replace(/_POST_PublishDateString_/g, data.PublishDateString)
                .replace(/_POST_Content_/g, data.Content);
    
        $('#postList').append($(post));
    }
}

function doOnScroll(className, callback) {
    var $ajaxAutoLoad = $('.' + className),
        $window = $(window);

    $window.scroll(
        function () {
            var offset = $ajaxAutoLoad.offset(),
                diff = 0;
            if (offset) {
                diff = $window.scrollTop() + $window.height() - offset.top;
            }
            if (diff > 0) {
                callback();
                $ajaxAutoLoad.remove();
                $(window).unbind('scroll');
                $('#postList').append($('<div class="ajax-auto-load"></div>'));
                doOnScroll('ajax-auto-load',
                    function () {
                        getPost(postIndex);
                    });
            }
            
        }
    );
}

var postIndex = 5;
jQuery(document).ready(function ($) {
    if ($('#postList').length > 0) {
        doOnScroll('ajax-auto-load',
            function() {
                getPost(postIndex);
            });
    }
});
