function likeSuccessHandler(data) {
    console.log(data);
    $('#post-' + data.postId).text(data.postLikes);
    $('#post-dislikes-' + data.postId).text(data.postDislikeCount);
    $('#post-likes-' + data.postId).text(data.postLikeCount);

}

function commentLikeSuccessHandler(data) {
    console.log("was here!");
    console.log(data);
    $('#comment-' + data.commentId).text(data.commentLikes);
    $('#comment-dislikes-' + data.commentId).text(data.commentDislikeCount);
    $('#comment-likes-' + data.commentId).text(data.commentLikeCount);
}

function addCommentSuccessHandler(data) {
    console.log(data.model);
    
    /*console.log(obj);
    console.log(data.data['commentId']);

    $('#comment-section-' + data.postId)
        .appendTo('<div>')
        .addClass("post-footer-comment-wrapper")
        .appendTo('<div>')
        .addClass("comment-" + data.data)
        .appendTo('<div>')
        .addClass("media-left");*/
}
