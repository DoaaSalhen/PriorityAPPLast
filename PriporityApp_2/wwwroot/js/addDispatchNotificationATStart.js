"use strict";

    function addNotificationToMenu(notificationCount, message, id)
    {
        var notificationCount = parseInt(document.getElementById("notificationbg").textContent) + notificationCount;
        document.getElementById("notificationbg").textContent = notificationCount;
        //document.getElementById("notificationbg2").value = notificationCount;
        var heading = document.getElementById("notificationbg");
        heading.textContent = notificationCount;
        var heading2 = document.getElementById("notificationbg2");
        heading2.textContent = notificationCount;
        document.getElementById("notificationId").value = id;
        var ul = document.getElementById("notificaionMenu");
        var li = document.createElement("li");
        li.setAttribute("class", "notification-item");
        li.textContent = message;
        ul.append(li);
        var hr = document.createElement("hr");
        hr.setAttribute("class", "dropdown-divider");
        ul.append(hr);



    }
