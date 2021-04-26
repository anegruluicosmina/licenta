$("button.delete_btn").click(function () {
    var quesitonId = $(this).attr('questionId');
    var result = confirm("Sigur vrei să ștregi această întrebare?");
    if (result == true) {
        $.ajax({
            type: "GET",
            url: baseUrl + "Questions/DeleteQuestion",
            data: { id: quesitonId },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: succesFunction,
            error: errorFunctions
        })
    }
    function succesFunction(data) {
        if (data.type == "Error") {
            alert(data.message);
        } else {
            location.reload();
        }
    }

    function errorFunctions(data) {
        console.log(data);
    }
})