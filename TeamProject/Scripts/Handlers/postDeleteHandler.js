/*jQuery.postJSON = function (url, data, success, antiForgeryToken, dataType) {
      if (dataType === void 0) { dataType = "json"; }
      if (typeof (data) === "object") { data = JSON.stringify(data); }
      var ajax = {
          url: url,
          type: "POST",
          contentType: "application/json; charset=utf-8",
          dataType: dataType,
          data: data,
          success: success
      };
      if (antiForgeryToken) {
          ajax.headers = {
              "__RequestVerificationToken": antiForgeryToken
          };
      };

      return jQuery.ajax(ajax);
  };*/

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