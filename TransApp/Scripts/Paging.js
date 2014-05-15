$(document).ready(function () {
    console.log("Java")

    $("#SearchID").keyup(function () {
        console.log("KEYUP")
        var empty = false;
        $("#SearchID").each(function () {
            console.log("in each function");
            if ($(this).val().length == 0) {
                console.log("is empty?");
                empty = true;
            }
        });
        console.log(empty + " empty?");
        if (empty) {
            $("#SearchBtnID").attr('disabled', 'disabled');
        } else {
            $("#SearchBtnID").removeAttr('disabled');
        }
    });

    console.log("Java2")
    $("#CommentID").keyup(function () {
        console.log("KEYUP2")
        var empty = false;
        $("#CommentID").each(function () {
            console.log("in each function");
            if ($(this).val().length == 0) {
                console.log("is empty?");
                empty = true;
            }
        });
        console.log(empty + " empty?");
        if (empty) {
            $("#CommhBtnID").attr('disabled', 'disabled');
        } else {
            $("#CommhBtnID").removeAttr('disabled');
        }
    });

    $(function () {
        $('#back-btn').click(function () {
            window.history.back();
        });
    })

    function checkFile() {
        console.log($("#fileInput"));
    };
    var placeholder = 'This is a line \nthis should be a new line';
    $('textarea').attr('value', placeholder);

    $('textarea').focus(function () {
        if ($(this).val() === placeholder) {
            $(this).attr('value', '');
        }
    });

    $('textarea').blur(function () {
        if ($(this).val() === '') {
            $(this).attr('value', placeholder);
        }
    });
});