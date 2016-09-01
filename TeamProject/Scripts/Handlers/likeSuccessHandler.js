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
    //notification();
}
function notification() {
    /*var form = $(this);
    event.preventDefault();
    $.getJSON(form.attr('action'),
        form.serialize(),
        function(data) {
            var html = Mustache.to_html($('#notification-template').html(), { notifications: data });
            $('#notifications').append(html);
        });*/
    var url = "/Home/GetNotifications";
    $.get(url, function (response) {
        console.log(response.alerts[0]);
        console.log(response.size);
        for (var i = 0; i < response.size; i++) {
       
            var a = {
                alertStyle: response.alerts[i].AlertStyle,
                dismissable: response.alerts[i].Dismissable,
                message: response.alerts[i].Message
            }
            

            console.log(a);
            var html = Mustache.to_html($('#notification-template').html(), a);
            $('#notification').append(html);

        }
        /*var html = Mustache.to_html($('#notification-template').html(), { response: data });*/
    });
    console.log('here');

}