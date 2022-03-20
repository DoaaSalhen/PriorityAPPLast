/// <reference path="profile-demo.js" />
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/PriorityTool/NotificationHub").build();
connection.on("SubmitNotification", (Message, count, id) => {
    var notificationCount = parseInt(document.getElementById("notificationbg").textContent) + count;
    document.getElementById("notificationbg").value = notificationCount;
    //document.getElementById("notificationbg2").value = notificationCount;
    var heading = document.getElementById("notificationbg");
    heading.textContent = notificationCount;
    var heading2 = document.getElementById("notificationbg2");
    heading2.textContent = notificationCount;
    var ul = document.getElementById("notificaionMenu");
    var li = document.createElement("li");
    li.setAttribute("class", "notification-item");
    li.textContent = Message;
    ul.append(li);
    var hr = document.createElement("hr");
    hr.setAttribute("class", "dropdown-divider");
    ul.append(hr);
    document.getElementById("notificationId").value = id;




    //var container = document.createElement("li");
    //var span = document.createElement("span");
    //span.textContent = Message;
    //container.append(span);
    //divNotfy.append(container);

    //var container = document.createElement("div");
    //var inputId = document.createElement("input");
    //inputId.setAttribute("type", "hidden");
    //inputId.setAttribute("value", id);
    //inputId.setAttribute("name", "notificationId");
    //inputId.setAttribute("id", "notificationId");


    //////var submitbtn = document.createElement("input");
    //////submitbtn.setAttribute("type", "submit");
    //////submitbtn.setAttribute("value", "seen");
    //////submitbtn.setAttribute("class", "seenBtn");
    //////form.setAttribute("asp-controller", "CSDeliveryOrder");
    //////form.setAttribute("asp-action", "SetSeenNotification");
    //////form.setAttribute("method", "post");

    //////form.prepend(submitbtn);
    //form.prepend(inputId);

    //container.appendChild(inputId);

    //var span = document.createElement("span");
    //span.textContent = Message;
    ///*a.href = "#";*/
    //span.setAttribute("class", "dropdown-item");
    //container.prepend(span);
    //divNotfy.prepend(container);
    //var div = document.createElement("div");
    //div.appendChild(heading);
    //div.appendChild(p);

    //document.getElementById("articleList").appendChild(div);
});
connection.start().catch(function (err) {
    //return console.error(err.toString());
});