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

<<<<<<< HEAD
    $(function () {
        $('#back-btn').click(function () {
            window.history.back();
        });
    })
=======
    $("#fileInput").keyup(function () {
        console.log("I");
       // alert(this.files[0].size / 1024 / 1024);
    });

    function checkFile() {
        console.log($("#fileInput"));
    };


>>>>>>> Like-sort
});