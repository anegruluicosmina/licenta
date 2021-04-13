var baseUrl = "https://localhost:44368/";
$("#find_people").click(function () {
    document.getElementsByClassName("result_ctn")[0].innerHTML = "";
    var searchString = document.getElementById("search_string").value;
    if (searchString == "") {
        var result_ctn = document.getElementsByClassName("result_ctn")[0];
        var message = document.createElement('p');
        message.className = "error_message_txt";
        message.innerHTML = "Nu ati introdus nicio valoare";
        result_ctn.appendChild(message);
    } else {
        $.ajax({
            type: "GET",
            url: baseUrl + "administration/findUser",
            data: { searchString: searchString },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successfunction,
        });
    }
    function successfunction(data) {
        if (data.length > 0) {
            data.forEach(function (entry) {
                var result_ctn = document.getElementsByClassName("result_ctn")[0];

                var userResult = document.createElement('button');
                userResult.name = "friend";
                userResult.value = entry.userName;

                var userName = document.createElement('p');
                userName.className = "user_name";
                userName.innerHTML = entry.lastName + " " + entry.firstName;

                var userEmail = document.createElement('p');
                userEmail.className = "user_email";
                userEmail.innerHTML = entry.userName;

                userResult.appendChild(userName);
                userResult.appendChild(userEmail);

                result_ctn.appendChild(userResult);

            });
        } else {
            var result_ctn = document.getElementsByClassName("result_ctn")[0];
            var message = document.createElement('p');
            message.className = "error_message_txt";
            message.innerHTML = "Niciun rezultat";
            result_ctn.appendChild(message);
        }
    }
})