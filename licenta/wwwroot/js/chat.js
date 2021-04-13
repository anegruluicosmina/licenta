var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
        .catch(error => {
            console.log(error.message)
        });

function sendMessageToHub(receiverUsername, message) {
    connection.invoke('sendMessage', receiverUsername, message)
}


const senderUsername = SenderUsername;
let text = document.getElementById("messageText");
let receiverUsername = document.getElementById("receiverUsername");

const chat = document.getElementById("chat");
const messageQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
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
    function successFunc(data) {
        //clear the input field
        clearInputField();
        //
        sendMessage();
        //succes
    }
    function errorFunc(data) {
        //show in message ctn there was a problem sending the message 
    }
});

function clearInputField() {
    var text = document.getElementById("messageText");
    messageQueue.push(text.value);
    text.value = '';
}
function sendMessage() {

    let text = messageQueue.shift() || "";
    if (text.trim() === "") return;

    let date = new Date();
    sendMessageToHub(receiverUsername.value, text);
}
//add message to chat
function addMessageToChat(message, who) {
    let isCurrentUserMessage = "";

    if (who === senderUsername) {
        isCurrentUserMessage = true;
    } else {
        isCurrentUserMessage = false;
    }

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
    chat.prepend(container);
}
