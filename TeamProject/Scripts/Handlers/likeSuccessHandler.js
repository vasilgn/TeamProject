function likeSuccessHandler(data) {
    console.log(data);
    console.log("post like dislike successfully!");
    $('#post-' + data.postId).text(data.postLikes);
    $('#post-dislikes-' + data.postId).text(data.postDislikeCount);
    $('#post-likes-' + data.postId).text(data.postLikeCount);
    notification();
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
    
    var url = "/Home/GetNotifications";
    
    $.get(url, function (response) {
        console.log(response.alerts[0]);
        for (var i = 0; i < response.size; i++) {
            var a = {
                alertStyle: response.alerts[i].AlertStyle,
                message: response.alerts[i].Message,
                dismissable: response.alerts[i].Dismissable
            }
            if (response.alerts[i].Dismissable) {
                a.dismissableClass = "alert-dismissable";
            } else {
                a.dismissableClass = null;
            }
            
            var html = Mustache.to_html($('#notification-template').html(), a);
            $('#notifications').append(html);
        }
    });

}