function likeSuccessHandler(data) {
    console.log(data);
    $('#post-' + data.postId).text(data.postLikes);
}

function commentLikeSuccessHandler(data) {
    console.log("was here!");
    console.log(data);
    $('#comment-' + data.commentId).text(data.commentLikes);
}
