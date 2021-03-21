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
            document.getElementById("menu_list").style.display = "none";
        }
    } else {
        menu.style.display = "none";
    }
}
