
function postDeleteHandler(postId) {
    event.preventDefault();
    var deleteConfirmed = confirm("Are you sure?");
    if (deleteConfirmed) {
        var requestData = {
            id: $.trim(postId)
        }
        $.ajax({
            url: '/Post/DeleteComfirmed/' + postId,
            type: 'POST',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            error: function (xhr) {
                console.log('Error: ' + xhr.statusText);
            },
            success: function (result) {
                $('#full-post-' + postId).empty();
                $('#full-post-' + postId).hide();
                console.log(result);
                notification();

            },
            async: true,
            processData: false
        });
    }
}