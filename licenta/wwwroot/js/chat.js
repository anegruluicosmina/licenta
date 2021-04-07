var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
        .catch(error => {
            console.log(error.message)
        });

function sendMessageToHub(receiverUsername, message) {
    connection.invoke('sendMessage', receiverUsername, message)
}

class Message {
    constructor(senderUsername, receiverUsername, text, date) {
        this.senderUsername = senderUsername;
        this.receiverUsername = receiverUsername;
        this.text = text;
        this.date = date;
    }
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
        console.log(data);
        console.log("message sent successfully");
        //succes
    }
    function errorFunc(data) {
        console.log(data);
        console.log("message sent without success ");
        //show in message ctn there was a problem sending the message 
    }
});

function clearInputField() {
    var text = document.getElementById("messageText").value;
    messageQueue.push(text);
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
    console.log("start" + who);
    let isCurrentUserMessage = "";

    if (who === senderUsername) {
        isCurrentUserMessage = true;
    } else {
        isCurrentUserMessage = false;
    }

    let container = document.createElement('p');
    container.className = isCurrentUserMessage ? "container darker" : "container";

    let sender = document.createElement('p');
    sender.className = "sender";
    console.log("end" + who);
    sender.innerHTML = who;
    let text = document.createElement('p');
    text.innerHTML = message;

    let date = document.createElement('span');
    date.className = isCurrentUserMessage ? "time-left" : "time-right";
    var currentDate = new Date();
    date.innerHTML =
        + currentDate.getDate() + "/"
        + currentDate.getFullYear() + " "
        + currentDate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', });
    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(date);
    chat.appendChild(container);
}
function addMessageToChatFromFired(message, who) {

    let isCurrentUserMessage = "";

    if (who === senderUsername) {
        isCurrentUserMessage = true;
    } else {
        isCurrentUserMessage = false;
    }

    let container = document.createElement('p');
    container.className = isCurrentUserMessage ? "container darker" : "container";

    let sender = document.createElement('p');
    sender.className = "sender";
    sender.innerHTML = who;
    let text = document.createElement('p');
    text.innerHTML = message;

    let date = document.createElement('span');
    date.className = isCurrentUserMessage ? "time-left" : "time-right";
    var currentDate = new Date();
    date.innerHTML =
        + currentDate.getDate() + "/"
        + currentDate.getFullYear() + " "
        + currentDate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', });
    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(date);
    chat.appendChild(container);
}