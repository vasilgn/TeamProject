function likeSuccessHandler(data) {
    console.log(data);
    $('#post-' + data.postId).text(data.postLikes);
}