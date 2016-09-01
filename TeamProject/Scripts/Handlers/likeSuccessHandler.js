function likeSuccessHandler(data) {
    console.log(data);
    console.log("post like dislike successfully!");
    $('#post-' + data.postId).text(data.postLikes);
    $('#post-dislikes-' + data.postId).text(data.postDislikeCount);
    $('#post-likes-' + data.postId).text(data.postLikeCount);

}

function commentLikeSuccessHandler(data) {
    console.log("comment like dislike successfully!");
    console.log(data);
    $('#comment-' + data.commentId).text(data.commentLikes);
    $('#comment-dislikes-' + data.commentId).text(data.commentDislikeCount);
    $('#comment-likes-' + data.commentId).text(data.commentLikeCount);
    notification();
}
function notification() {
   /* $.ajax({
        url: '..Views/Shared/_Alert',
        contenType: 'application/html; chaset=utf-8',
        type: 'GET',
        dataType: 'html'
    })*/
    $(document)
        .ready(function() {
            console.log('change occuered');
          
                console.log($(window));
                console.log('empty');
        });
}