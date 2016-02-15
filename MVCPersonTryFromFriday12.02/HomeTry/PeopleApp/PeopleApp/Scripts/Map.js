function showMap(lat, lng) {
    var myLatlng = new google.maps.LatLng(lat, lng);
    var myOptions = {
        zoom: 8,
        center: myLatlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    var map = new google.maps.Map(document.getElementById("googleMap"), myOptions);
    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
        title: "Hello World!"
    });
    showAddress(myLatlng);

    $("#cities").change(function () {
        var selectedCountry = $("#countries :selected").val();
        var selectedCity = $("#cities :selected").val();
        if ($("#street").val() != "") {
            FindLatLng();
        }
        else {
            FindLatLngByCityCountry();
        }
    });
}

function FindLatLngByCityCountry() {
    var address = $("#cities :selected").text() + "," + $("#countries :selected").text();
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            $("#lat").val(results[0].geometry.location.lat());
            $("#lng").val(results[0].geometry.location.lng());
            showMap(results[0].geometry.location.lat(), results[0].geometry.location.lng());
        }
    });
}

function FindLatLng() {
    var address = $("#street").val() + "," + $("#buildingNumber").val() + "," + $("#cities :selected").text() + "," + $("#countries :selected").text();
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            $("#lat").val(results[0].geometry.location.lat());
            $("#lng").val(results[0].geometry.location.lng());
            showMap(results[0].geometry.location.lat(), results[0].geometry.location.lng());
        }
    });
}

function showAddress(myLatlng) {
    var geocoder = geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'latLng': myLatlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[1]) {
                alert("Location: " + results[1].formatted_address);
            }
        }
    });
}


