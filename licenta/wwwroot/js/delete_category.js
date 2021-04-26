$("button.delete_category_btn").click(function () {
    var categoryId = $(this).attr('categoryId');
    var result = confirm("Sigur vrei să ștergi categoria " + categoryName + "?");
    if (result == true) {
        $.ajax({
            type: "GET",
            url: baseUrl + "questions/DeleteCategory",
            data: { id: categoryId },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunction,
            error: errorFunction
        })
    }

    function successFunction(data) {
        if (data.type == 'Error') {
            alert(data.message);
        } else {
            window.location.replace(baseUrl + "questions/Categories/1");
        }
    }

    function errorFunction(data) {
        console.log(data);
    }
})