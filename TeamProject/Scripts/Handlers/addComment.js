
function addComment(data) {
    console.log(data.model);
    var com = {
        commentId: data.model.CommentId,
        likes: data.model.CommentLikeCounter,
        postId: data.model.PostId,
        userId: data.model.UserId,
        commentDate: Date(data.model.CommentDate)

    }

    var html = Mustache.to_html(template, com);
    $("#comment-section-" + com.postID).append(html);

}
function addComment1(data) {

    event.preventDefault();
    var com = {
        commentId: data.model.CommentId,
        likes: data.model.CommentLikeCounter,
        postId: data.model.PostId,
        userId: data.model.UserId,
        commentDate: Date(data.model.CommentDate)
    }

    $(document)
        .ready(function () {
            $.getJSON(
                JSON.stringify(com),
                {},
                function (productsData, textStatus, jqXHr) {
                    $.get('comment-template.cshtml',
                        function (template, textStatus, jqXhr) {
                            var productHtml = Mustache.render(template, productsData);
                            $('#comment-section-' + com.postId).append(productHtml);
                        });
                });
        });

}

