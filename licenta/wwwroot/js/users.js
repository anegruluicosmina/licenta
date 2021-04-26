$('button.delete_btn').click(function () {
    var userName = $(this).attr('name');
    var userId = $(this).attr('userId');
    var response = confirm("Ești sigur că vrei să ștergi utilizatorul " + userName + "?");
    if (response == true) {
        $.ajax({
            type: "GET",
            url: baseUrl + 'Administration/DeleteUser',
            data: { userId: userId },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunction,
            error: errorFunction
            });
    }
    function successFunction(data) {
        if (data != 'Succes') {
            alert(data.errors);
        } else {
            location.reload();
        }
    }
    function errorFunction(data) {
        console.log(data);
    }
})