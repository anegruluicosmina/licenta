var baseUrl = "https://localhost:44368/";
var correctAnswers = 0;
var category = model.categoryId;
var questionsIds = [];
var wrongQuestions = [];
var index = -1;
var answers = document.getElementById("answers");

for (var i = 0; i < model.questions.length; i++) {
    questionsIds.push(model.questions[i].id);
}

$("#show_explanation").click(function () {
    $("#explanation").text(model.questions.find(element => element.id === questionsIds[index + 1]).explanation);
})

$("#btn_next_question").click(function () {
    $("#explanation").hide();
    $("#show_explanation").hide();
    $("#explanation").text("");
    if (index < 0) {
    $(".wrong_correct_answ").show();
        $("#verify_answer").show();
        $("#btn_next_question").attr('value', 'Urmatoarea intrebare');
    }

    if (index + 1 >= questionsIds.length) {
        index = -1;
    }
    if (wrongQuestions.length < model.numberOfWrongAnswer) {
        if (questionsIds.length > 0) {
            next_question();
        } else {
            var data = take_data();
            $("#result").text("Ai trecut examinarea cu urmatoarele rezultate:");
            $("#result").css("border-bottom", "2px solid #184D68");
            save_test();
            $("#question_container").text("");
            if (wrongQuestions.length > 0) {
                $("#anch_show").show();
                $("#anch_show").attr("href", baseUrl+"questions/WrongAnswered?data=" + JSON.stringify(wrongQuestions));
            }
            $("#btn_next_question").hide();
            $("#verify_answer").hide();
        }
    } else {
        $("#result").text("Ai picat examinarea cu urmatoarele rezultate:");
        $("#result").css("border-bottom", "2px solid #184D68");
        $("#question_container").text("");
        $("#anch_show").show();
        $("#anch_show").attr("href", baseUrl +"questions/WrongAnswered?data=" + JSON.stringify(wrongQuestions));
        $("#btn_next_question").hide();
        $("#verify_answer").hide();
        save_test();
    }

});

$("#verify_answer").click(function () {
    if (questionsIds.length > 0) {
        var data = take_data();
        if (data === "[]") {
            $("#error_message").text("Inainte de a verifica un raspuns, selecteaza unul.");
        } else {
            verify_answers(data);
            $("#verify_answer").hide();
            index--;
        }
    } else {
        if (wrongQuestions.length > 0) {
            $("#question_container").hide();
            $("#anch_show").show();
        }
        if (wrongQuestions.length > model.numberOfWrongAnswer) {
            $("#result").text("Ai picat examinarea cu urmatoarele rezultate:");
            $("#result").css("background-color", "2px solid #184D68");
        } else {
            $("#result").text("Ai trecut examinarea cu urmatoarele rezultate:");
            $("#result").css("background-color", "2px solid #184D68");
        }
        save_test();
    }
});

function next_question() {
    index++;
    $("#verify_answer").show();
    $("#explanation").text("");
    $("#error_message").text("");
    $("#question_text").text(model.questions.find(element => element.id === questionsIds[index]).text);
    $("#answers").empty();
    for (var i = 0; i < model.questions[index].answers.length; i++) {
        var answer = document.createElement("div");
        answer.setAttribute("class", "answer");
        var check = document.createElement("INPUT");
        check.setAttribute("type", "checkbox");
        check.setAttribute("value", model.questions.find(element => element.id === questionsIds[index]).answers[i].id);
        var nod = document.createTextNode(model.questions.find(element => element.id === questionsIds[index]).answers[i].text);
        answer.append(check, nod);
        $("#answers").append(answer);

    }
}

function take_data() {
    var selected = [];
    $('#answers input:checked').each(function () {
        selected.push($(this).attr('value'));
    });
    var data = JSON.stringify(selected);
    return data;
}

function save_test() {
    $.ajax({
        type: "GET",
        url: baseUrl + 'questions/SaveTest',
        data: { correctAnswers: correctAnswers, categoryId: model.categoryId },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: errorFunc
    });
    function errorFunc() {
        $("#error_message").text("error from save_test");
    }
}

function verify_answers(data) {
    $.ajax({
        type: "GET",
        url: baseUrl+'questions/verifyAnswer',
        data: { data: data, questionid: questionsIds[index] },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
    function successFunc(data) {
        if (data.id == 0) {
            if (index >= -1) {
                correctAnswers++;
                $("#correct_answers").text(correctAnswers);
            }
            $("#error_message").text("Ai raspuns corect");

        } else if (data.id == 3) {
            $("#error_message").text("A aparut o eroare de la noi, te rugam incearca sa reiei testul");

        } else if (data.id == 1) {
            $("#error_message").text("Inainte de a verifica un raspuns, selecteaza unul.");

        } else if (data.id == 2) {
            if (index >= -1) {
                $("#wrong_answers").text(function (i, oldvalue) {
                    return parseInt(oldvalue, 10) + 1;
                });
            }
            wrongQuestions.push(questionsIds[index + 1]);
            $("#explanation").show();
            $("#show_explanation").show();
            $("#error_message").text("Ai raspuns gresit");
        }

        $("#verify_answer").hide();
        if (index >= -1) {
            $("#nr_questions").text(function (i, oldvalue) {
                return parseInt(oldvalue, 10) - 1;
            });
            questionsIds.splice(index + 1, 1);
        }
    }

    function errorFunc(data) {
        $("#error_message").text("error from verify answer");
    }
}