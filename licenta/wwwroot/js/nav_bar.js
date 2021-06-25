var host = window.location.hostname;
var port = window.location.port;
var baseUrl = "https://" + host + ":" + port + "/";
function show_menu() {
    var menu = document.getElementById("navigation_ctn");
    if (menu.style.display === "none" || menu.style.display === "") {
        menu.style.display = "block";
        var menu = document.getElementById("profile_menu_nv").style.display = "none";
    } else {
        menu.style.display = "none";
    }
}
function show_profile_menu() {
    var menu = document.getElementById("profile_menu_nv");
    if (menu.style.display === "none" || menu.style.display === "") {
        menu.style.display = "block";
        /*if admin is on mobile device do not show and menu list*/
        if ($(window).width() < 750) {
            document.getElementById("navigation_ctn").style.display = "none";
        }
    } else {
        menu.style.display = "none";
    }
}
$("#send_contact_form").click(function () {
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    var emailAddress = $("#form_email").val();
    var messageSend = $("#form_message").val();
    if (!$.trim(emailAddress)) {
        $("#error_message").text("Nu ati introdus o valorea pentru adresa de email.");
    }else if (!testEmail.test(emailAddress)) {
        $("#error_message").text("Adresa de email nu este una valida");
    } else if (!$.trim(messageSend)) {
        $("#error_message").text("Nu ati completat campul corespunzator mesajului");
    } else {
            $.ajax({
                type: "POST",
                url: baseUrl + 'account/SendMessage',
                data: { email: emailAddress, message: messageSend },
                success: successFunc,
                error: errorFunc
            });
            function successFunc(data) {
                $("#form_email").val("");
                $("#form_message").val("");
                $("#error_message").text(data.error_message);
            }
            function errorFunc(data) {
            $("#error_message").text("A aparut o eroare. Mai inceasrca o data!");
            }
    }
})