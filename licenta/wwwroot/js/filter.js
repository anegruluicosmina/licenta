$("#filters_menu_btn").click(function () {
    var menu = document.getElementById("filter_form");
    if (menu.style.display === "none" || menu.style.display === "") {
        menu.style.display = "flex";
    } else {
        menu.style.display = "none";
    }
})