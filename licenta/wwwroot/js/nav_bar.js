function show_menu() {
    var menu = document.getElementById("menu_list");
    if (menu.style.display === "none" || menu.style.display === "") {
        menu.style.display = "block";
    } else {
        menu.style.display = "none";
        setTimeout(function () {
            menu.classList.remove('fade-in'); // Remove .fade-in class
        }, 5000); 
    }
}
