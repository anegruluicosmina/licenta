$('button.delete_btn').click(function () {
    var id = $(this).attr('id');
    var response = confirm("Esti sigur ca vrei sa stergi aceasta inregistrare?");
    if (response == true) {
        $.ajax({
            type: "GET",
            url: baseUrl + 'notes/DeleteNote',
            data: { id: id },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunction,
            error: errorFunction
        });
    }
    function successFunction() {
        location.reload();
    }
    function errorFunction(data) {
        console.log(data);
    }
})