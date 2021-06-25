var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.log(error.message)
    });

function sendMessageToHub(receiverUsername, message) {
    connection.invoke('sendMessage', receiverUsername, message)
}

//who sends the message
const senderUsername = SenderUsername;
//message text
let text = document.getElementById("messageText");
//who receives the message
let receiverUsername = document.getElementById("receiverUsername");
console.log("receiver out:" + receiverUsername.value);
console.log("senderUsername out" + senderUsername);
//get chat element
const chat = document.getElementById("chat");
//queue of messages
const messageQueue = [];


//send message event listenet
document.getElementById('submitButton').addEventListener('click', () => {

    if (text.value != "" && senderUsername != "" && receiverUsername.value != "") {
        //save message in db
        $.ajax({
            type: "GET",
            url: baseUrl + 'account/SaveMessage',
            data: { senderUsername: senderUsername, receiverUsername: receiverUsername.value, text: text.value },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });
    }

    function successFunc(data) {
        if (data.message === "ok") {
            //move element in top of the q
/*            var id = "#" + CSS.escape(receiverUsername.value);
            $(id).prependTo('.conversations_ctn');*/
            //clear the input field
            clearInputField();

            var friend = receiverUsername.value;
            console.log("aici 1");
            document.querySelector("[friend=" + CSS.escape(friend) + "]").style.background = 'white';
            document.querySelector("[friend=" + CSS.escape(friend) + "]").style.color = 'black';
            console.log("aici 2");
            sendMessage();
            //succes
        } else {
            document.getElementById("messageText").value = "A aparut o eroare";
            console.log(data.message);
        }
    }
    function errorFunc(data) {
        //show in message ctn there was a problem sending the message 
    }
});

function clearInputField() {
    console.log("pppppppppp");
    var text = document.getElementById("messageText");
    messageQueue.push(text.value);
    text.value = '';
}


function sendMessage() {
    let text = messageQueue.shift() || "";
    if (text.trim() === "") return;

    /*let date = new Date();*/
    sendMessageToHub(receiverUsername.value, text);
}


//add message to chat WEBSOCKET
function addMessageToChat(message, who) {

    var user = document.getElementById("receiverUsername").value;

    let isCurrentUserMessage = "";

    if (who === senderUsername) {
        isCurrentUserMessage = true;
        notifyConversation(senderUsername, message, isCurrentUserMessage);
    } else {
        isCurrentUserMessage = false;
        notifyConversation(who, message, isCurrentUserMessage);
/*        var id = "#" + CSS.escape(senderUsername);
        $(id).prependTo('.conversations_ctn');
        var ctn = document.getElementById("#conversations_ctn");
        var convo = document.getElementById(id);
        ctn.prepend(convo);*/
    }

    //receiving
    //who == who sends the message
/*    notifyConversation(who, message, isCurrentUserMessage);
    //sending
    notifyConversation(senderUsername, message, isCurrentUserMessage);*/

    if (isCurrentUserMessage == true || user == who) {
        let container = document.createElement('div');
        container.className = isCurrentUserMessage ? "dark_message_ctn" : "light_message_ctn";

        let sender = document.createElement('p');

        let text = document.createElement('p');
        text.innerHTML = message;

        let date = document.createElement('span');
        date.className = "msg_date";
        var currentDate = new Date();
        date.innerHTML = currentDate.getHours() + ":" + currentDate.getMinutes();

        container.appendChild(text);
        container.appendChild(date);
        if (chat.hasChildNodes) {
            chat.prepend(container);
        } else {
            chat.append(container);
        }
    }
}


//update last message in conversation button when a new message is received
function notifyConversation(who, message, isCurrent) {
    //if it is current user update the convo
    if (isCurrent == true) {
        var friend = chat.getAttribute("convo_name");
        var button = document.getElementById(friend);
        if (button != "undefined" && button != null) {
/*            document.querySelector("[friend=" + CSS.escape(friend) + "]").style.background = "yellow";
            document.querySelector("[friend=" + CSS.escape(who) + "]").style.color = "white";
            document.querySelector("[friend=" + CSS.escape(friend) + "] > div.message_txt > div.text_ctn > p").innerHTML = message;
            document.querySelector("[friend=" + CSS.escape(friend) + "] > div.message_txt > div.sender_ctn > p").innerHTML = "Tu:";
            var date = new Date();
            document.querySelector("[friend=" + CSS.escape(friend) + "] > div.message_time > p").innerHTML = date.getHours() + ":" + date.getMinutes();*/
        }
    } else {//if it not current user
        var button = document.getElementById(who);
        //if conversation container exists
        if (button != "undefined" && button != null) {
/*            document.querySelector("[friend=" + CSS.escape(friend) + "]").style.background = "blue";
            document.querySelector("[friend=" + CSS.escape(who) + "]").style.color = "red";
            document.querySelector("[friend=" + CSS.escape(who) + "] > div.message_txt > div.text_ctn > p").innerHTML = message;
            document.querySelector("[friend=" + CSS.escape(who) + "] > div.message_txt > div.sender_ctn > p").innerHTML = who;
            var date = new Date();
            document.querySelector("[friend=" + CSS.escape(who) + "] > div.message_time > p").innerHTML = date.getHours() + ":" + date.getMinutes();*/
        } else {
            //if conversation ctn does not exists create one :)))))
            createConversationCtn(who, message);
        }
    }

}



//create a new vonversation div
function createConversationCtn(who, message) {
    var conversations_ctn = document.getElementById("conversations_ctn");
    console.log(conversations_ctn);

    var convo_ctn = document.createElement('button');
    convo_ctn.setAttribute('type', 'submit');
    convo_ctn.setAttribute('id', who);
    convo_ctn.setAttribute('friend', who);
    convo_ctn.className = "convo_ctn message_not_seen";

    //create div for the conversation name
    var friend = document.createElement('div');
    friend.className = "friend";
    //text node for the convo name
    var text = document.createElement('p');
    text.innerHTML = who;
    friend.appendChild(text);

    //container for message sender & text
    var message_ctn = document.createElement('div');
    message_ctn.className = "message_txt";

    //who send the message
    var sender_ctn = document.createElement('div');
    sender_ctn.className = "sender_ctn";
    var sender_text = document.createElement('p');
    sender_text.innerHTML= who;
    sender_ctn.append(sender_text);

    //what is the message
    var text_ctn = document.createElement('div');
    text_ctn.className = "text_ctn";
    var message_text = document.createElement('p');
    message_text.innerHTML= message;
    text_ctn.append(message_text);

    //append sender and message to mesage text ctn
    message_ctn.append(sender_ctn);
    message_ctn.append(text_ctn);

    //time of the message
    var message_time = document.createElement('div');
    message_time.className = "message_time";
    var today = new Date();
    var time_text = document.createElement('p');
    time_text.innerHTML ="Azi: " + today.getHours() + ":" + today.getMinutes();
    message_time.append(time_text);

    //append child to convo button
    convo_ctn.append(friend);
    convo_ctn.append(message_ctn);
    convo_ctn.append(message_time);

    console.log(convo_ctn);
    //append button to conversations container
    conversations_ctn.prepend(convo_ctn);
}

//get the messages whith that friend when a new conversation is opened
/*$("button.convo_ctn").click(function () {
    //get the name email of the friend and update the de input field
*//*    var friend = $(this).attr('id');
    document.getElementById("receiverUsername").value = friend;
    chat.setAttribute("convo_name", receiverUsername.value);

    $.ajax({
        type: "GET",
        url: baseUrl + 'account/Chat',
        data: { friend: friend },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    })
    //if messages are received, fill the container with them 
    function successFunc(data) {
        chat.innerHTML = "";        
        var i;
        for (i = 0; i < data.length; i++){
            addMessage(data[i]);  
        }
    }
    function errorFunc(data) {
        console.log(data)
    }*//*
})
*/

//function to add message from action to chat
function addMessage(item) {
    
    let isCurrentUserMessage = "";
    let ok = 0;
    if (item.senderUsername === senderUsername) {
        isCurrentUserMessage = true;
    } else {
        isCurrentUserMessage = false;
    }

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "dark_message_ctn" : "light_message_ctn";

    /*let sender = document.createElement('p');*/

    let text = document.createElement('p');
    text.innerHTML = item.text;

    let date = document.createElement('span');
    date.className = "msg_date";
    var currentDate = new Date(item.date);
    date.innerHTML = currentDate.getHours() + ":" + currentDate.getMinutes();

    container.appendChild(text);
    container.appendChild(date);
    if (chat.hasChildNodes) {
        chat.append(container);
    } else {
        chat.prepend(container);
    } 
}
$(document).on('click', '.user_result', function () {
    var who = $(this).attr('id');
    var result_ctn = document.getElementById("result_ctn");
    result_ctn.innerHTML = "";
    createConversationCtn(who, "");
})



$(document).on('click', '.convo_ctn', function () {
    //get the name email of the friend and update the de input field
    var friend = $(this).attr('id');
    document.getElementById("receiverUsername").value = friend;
    chat.setAttribute("convo_name", receiverUsername.value);

    $.ajax({
        type: "GET",
        url: baseUrl + 'account/Chat',
        data: { friend: friend },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    })
    //if messages are received, fill the container with them 
    function successFunc(data) {
        chat.innerHTML = "";
        var i;
        for (i = 0; i < data.length; i++) {
            addMessage(data[i]);
        }
    }
    function errorFunc(data) {
        console.log(data)
    }
})






//find your friends
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
                fillResultCOnversationCtn(entry.userName, entry.lastName, entry.firstName);

/*                var result_ctn = document.getElementsByClassName("result_ctn")[0];

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

                result_ctn.appendChild(userResult);*/

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


//fills the container with user search results 
function fillResultCOnversationCtn(who, lastName, firstName) {
    var result_ctn = document.getElementById("result_ctn");

    //create button with class user result
    var userResult = document.createElement('button');
    userResult.className = "user_result";
    userResult.setAttribute('id', who);

    //create text node for user's name
    var userName = document.createElement('p');
    userName.className = "user_name";
    userName.innerHTML = lastName + " " + firstName;

    //create text node for user's email
    var userEmail = document.createElement('p');
    userEmail.className = "user_email";
    userEmail.innerHTML = who;

    userResult.appendChild(userName);
    userResult.appendChild(userEmail);

    result_ctn.appendChild(userResult);
}
