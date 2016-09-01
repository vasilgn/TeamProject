
function addComment(data) {

    event.preventDefault();
    
    var com = {
        commentId: data.model.CommentId,
        text: data.model.Text,
        postId: data.model.PostId,
        userName: data.model.UserName,
        fullName: data.model.FullName,
        commentDate: data.model.CommentDate,
        likes: 0
    }

    console.log(com);
    console.log('comment added!');
    var html = Mustache.to_html($("#comment-template").html(), com);

    $("#comment-section-" + com.postId).prepend(html);
    $('#commentText').val('').removeAttr('checked').removeAttr('selected');
    //notification();
}

