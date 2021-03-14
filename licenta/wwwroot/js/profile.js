var baseUrl = '@Url.Action("ChangePassword", "Account")';
var urlEditInfo = '@Url.Action("EditUser", "Account")';
var baseUrl = "https://localhost:44368/";
$('#reset_pwd_btn').click(function () {
    $('#profile_ctn').load(baseUrl + "Account/ChangePassword");
})
$("#edit_info_btn").click(function () {
    $('#profile_ctn').load(baseUrl + "Account/EditUser");
})
$("#edit_email_btn").click(function () {
    $('#profile_ctn').load(baseUrl + "Account/ChangeEmail");
})