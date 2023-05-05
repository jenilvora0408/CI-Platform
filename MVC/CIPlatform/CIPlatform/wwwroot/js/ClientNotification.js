"use strict";

//establishing connection with Notification Hub
var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

//appending the message sent to list of Notifications
connection.on("ReceiveMsg", function (message) {
    var li = document.createElement("li");
    document.getElementById("msgList").appendChild(li);
    li.textContent = `${message}`;
});

connection.start().then(function () {
   /* document.getElementById("sendButton").disabled = false;*/
}).catch(function (err) {
    return console.error(err.toString());
});

$("#sendButton").on("click", function (event) {
    //var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});