    var currentHours;
    var currentMinutes;
    var currentSeconds;
    var timeOfDay;

    function updateHours() {
        var currentTime = new Date();
        if (currentHours != currentTime.getHours()) {
            currentHours = currentTime.getHours();
            if (currentHours < 10) {
                $("#clockHours").text("0" + currentHours);
            }
            else {
                $("#clockHours").text(currentHours);
            }
        }
    };

    function updateMinutes() {
        var currentTime = new Date();
        if (currentMinutes != currentTime.getMinutes()) {
            currentMinutes = currentTime.getMinutes();
            if (currentMinutes < 10) {
                $("#clockMinutes").text("0" + currentMinutes);
            }
            else {
                $("#clockMinutes").text(currentMinutes);
            }
        }
    };

    function updateSeconds() {
        var currentTime = new Date();
        currentSeconds = currentTime.getSeconds();
        if (currentSeconds < 10) {
            $("#clockSeconds").text("0" + currentSeconds);
        }
        else {
            $("#clockSeconds").text(currentSeconds);
        }
        //$("#clockSeconds").effect("fade", {},1,1);
    };