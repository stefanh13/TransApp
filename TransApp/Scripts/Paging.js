$(document).ready(function () {

    // If there is no text in the searchbox then the user can not search.
    $("#SearchID").keyup(function () {
        var empty = false;
        $("#SearchID").each(function () {
            if ($(this).val().length == 0) {
                empty = true;
            }
        });
        // The search button is diabled by default.
        if (empty) {
            $("#SearchBtnID").attr('disabled', 'disabled');
        } else {
            $("#SearchBtnID").removeAttr('disabled');
        }
    });

    // If there is no text in the commentbox then the user can not comment.
    $("#CommentID").keyup(function () {
        var empty = false;
        $("#CommentID").each(function () {
            if ($(this).val().length == 0) {
                empty = true;
            }
        });
        // The comment button is diabled by default.
        if (empty) {
            $("#CommhBtnID").attr('disabled', 'disabled');
        } else {
            $("#CommhBtnID").removeAttr('disabled');
        }
    });

    // Button that takes the user to the previous page.
    $(function () {
        $('#back-btn').click(function () {
            window.history.back();
        });
    })
});