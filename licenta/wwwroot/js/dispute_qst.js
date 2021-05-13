$("#attention_btn").click(function () {
    var questionsId = $(this).attr("questionid");
    $.ajax({
        type: "GET",
        url: baseUrl + "questions/WrongQuestion",
        data: { questionId: questionsId },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
    });
    function successFunc(data) {
        alert("Observația ta a fost salvată. Mulțumim!");
    }
})