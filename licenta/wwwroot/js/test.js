var baseUrl = "https://localhost:44368/";
var correctAnswers = 0;
var category = model.categoryId;
var questionsIds = [];
var wrongQuestions = [];
var index = -1;
var answers = document.getElementById("answers");

/*create a array to track questions*/
for (var i = 0; i < model.questions.length; i++) {
    questionsIds.push(model.questions[i].id);
}

/*when answered wrong show question explanation*/
$("#show_explanation").click(function () {
    $("#explanation").text(model.questions.find(element => element.id === questionsIds[index + 1]).explanation);
})

/*start tests or go to the next question*/
$("#btn_next_question").click(function () {
    $("#explanation").hide();
    $("#show_explanation").hide();
    $("#explanation").text("");
/*for the first question shown in test*/
    if (index < 0) {
    $(".wrong_correct_answ").show();
        $("#verify_answer").show();
        $("#btn_next_question").attr('value', 'Urmatoarea intrebare');
    }
/*to resume unanswered questions*/
    if (index + 1 >= questionsIds.length) {
        index = -1;
    }
 /*if the number of incorrectly answered questions is less than the maximum */
        if (wrongQuestions.length < model.numberOfWrongAnswer) {
/*if there are still questions go to the next one else the user passed the examn*/
            if (questionsIds.length > 0) {
                next_question();
            } else {
    /*ADDDD BUTTONS TO TAKE ANOTHER TEST OR TO GO TO THE TEST PAGE*/
                var data = take_data();
                $("#result").text("Ai trecut examinarea cu următoarele rezultate:");
                $("#result").css("border-bottom", "2px solid #184D68");
                save_test();
            /*btn_new_test();*/
                $("#btn_new_test").show();
                $("#question_container").text("");
    /*show to wrong answered questions*/
                if (wrongQuestions.length > 0) {
                    $("#anch_show").show();
                    $("#anch_show").attr("href", baseUrl+"questions/WrongAnswered?data=" + JSON.stringify(wrongQuestions));
                }
                $("#btn_next_question").hide();
                $("#verify_answer").hide();
            }
        } else { 
/*if the user had the maximum number of wrong questions*/
        $("#result").text("Ai picat examinarea cu următoarele rezultate:");
        $("#result").css("border-bottom", "2px solid #184D68");
        $("#question_container").text("");
        $("#anch_show").show();
        $("#anch_show").attr("href", baseUrl +"questions/WrongAnswered?data=" + JSON.stringify(wrongQuestions));
        $("#btn_next_question").hide();
        $("#verify_answer").hide();
            save_test();
        /*btn_new_test();*/
            $("#btn_new_test").show();

    }

});
/*to verify the answer*/
$("#verify_answer").click(function () {
    if (questionsIds.length > 0) {
/*take user's answer*/
        var data = take_data();
        if (data === "[]") {
            $("#error_message").text("Înainte de a verifica un răspuns, selectează unul.");
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
/*verify the number of wrong answered to show the proper message*/
        if (wrongQuestions.length > model.numberOfWrongAnswer) {
            $("#result").text("Ai picat examinarea cu următoarele rezultate:");
            $("#result").css("background-color", "2px solid #184D68");
            /*btn_new_test();*/
            $("#btn_new_test").show();
        } else {
            $("#result").text("Ai trecut examinarea cu următoarele rezultate:");
            $("#result").css("background-color", "2px solid #184D68");
        /*btn_new_test();*/
            $("#btn_new_test").show();
        }
        save_test();
    }
});
$("#btn_new_test").click(function () {
    location.reload();
})
/*update the interface for the next question*/
function next_question() {
    index++;
    $("#verify_answer").show();
    $("#explanation").text("");
    $("#error_message").text("");
    $("#question_text").text(model.questions.find(element => element.id === questionsIds[index]).text);
    $("#answers").empty();
/*add the answer*/
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
/*function  to colect the user's answer*/
function take_data() {
    var selected = [];
    $('#answers input:checked').each(function () {
        selected.push($(this).attr('value'));
    });
    var data = JSON.stringify(selected);
    return data;
}
function btn_new_test() {
    var btn_another_test = document.createElement("button");
    btn_another_test.setAttribute("id", "btn_test");
    btn_another_test.textContent ="Da un alt test";
    $("#test_buttons").append(btn_another_test);
}



/*call action to save the test when ready*/
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
        $("#error_message").text("error from save test");
    }
}
/*function to call the action to verify the user's answer*/
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
/*updates the interface with the call answer*/
    function successFunc(data) {
        if (data.id == 0) {
            if (index >= -1) {
                correctAnswers++;
                $("#correct_answers").text(correctAnswers);
            }
            $("#error_message").text("Ai răspuns corect");

        } else if (data.id == 3) {
            $("#error_message").text("A apărut o eroare de la noi, te rugăm încearcă să reiei testul.");

        } else if (data.id == 1) {
            $("#error_message").text("Înainte de a verifica un raspuns, selectează unul.");

        } else if (data.id == 2) {
/*wrong asnwer: save the question to show later*/
            if (index >= -1) {
                $("#wrong_answers").text(function (i, oldvalue) {
                    return parseInt(oldvalue, 10) + 1;
                });
            }
            wrongQuestions.push(questionsIds[index + 1]);
            $("#explanation").show();
            $("#show_explanation").show();
            $("#error_message").text("Ai răspuns gresit.");
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
        $("#error_message").text("erorare din verificarea răspunsului");
    }
}