$(document).ready(function () {

    let submit = $("#submitBtn");
    

    let isEmailTrue = function isEmail(email) {
        return /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/i.test(email);
    }

    $("#submitBtn").click(function (e) {
        e.preventDefault();

        let eml = $("#email21").val().toString();

        let email = $("#mc4wp-form-1 input[name='email']");
        //let success = $("#mc4wp-form-1 .alert-success");
        let warning = $("#mc4wp-form-1 .alert-warning");
        //success.css("display", "none");
        warning.css("display", "none");

        if (isEmailTrue(eml)) {

            $.ajax({
                url: "Home/Subscribe",
               
                type: "get",
                dataType: "json",
                data: {
                    eml: eml
                },
                success: function (response) {
                    if (response.status == true) {
                        console.log("success")
                        swal("Good job!", "Your message has been delivered to the other party", "success")

                    }
                    else {
                        warning.css("display", "block");
                        warning.text(response.message);
                    }
                },
                //success: function (response) {
                //    if (response.status == true) {
                //        success.css("display", "block");
                //        success.text(response.message);
                //    } else {
                //        warning.css("display", "block");
                //        warning.text(response.message);
                //    }
                //},
                error: function (error) {
                    console.log(error);
                },
                complete: function () {
                    email.val("");
                }
            });
        }
        else {
            swal("Error", "Please fill in the required information", "error")
            console.log("datalari doldur")
        }
    });

});