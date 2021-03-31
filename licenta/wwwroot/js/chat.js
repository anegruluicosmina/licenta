var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on('receiveMessage', addMessageToChat);
connection.start()
    .catch(error => {
        console.log(error.message)
    });

function sendMessageToHub(message) {
    connection.invoke('sendMessage', message)
}
class Message {
    constructor(username, text, date) {
        this.username = username;
        this.text = text;
        this.date = date;
    }
}

const username = userName;
const textInput = document.getElementById("messageText");
const chat = document.getElementById("chat");
const messageQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
    var currentDate = new Date();
    date.innerHtTML = (currentDate.getMonth() + 1) + "/"
        + currentDate.getDate() + "/"
        + currentDate.getFullYear() + " "
        + currentDate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', });
});

function clearInputField(){
    messageQueue.push(textInput.value);
    textInput.value = '';
}
function sendMessage() {

    let text = messageQueue.shift() || "";
    if (text.trim() === "") return;

    let date = new Date();
    let message = new Message(username, text);
    sendMessageToHub(message);
}
function addMessageToChat(message) {
    let isCurrentUserMessage = message.username === username;

    let container = document.createElement('p');
    container.className = isCurrentUserMessage ? "container darker" : "container";

    let sender = document.createElement('p');
    sender.className = "sender";
    sender.innerHTML = message.userName;
    let text = document.createElement('p');
    text.innerHTML = message.text;

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

/*"use strict";

import { error } from "jquery";


//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});*/