function showMap(lat,lng) {
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

    google.maps.event.addListener(map, 'click', function (e) {
        var latlng = new google.maps.LatLng(e.latLng.lat(), e.latLng.lng());
        var geocoder = geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[1]) {
                    SetLngLat(e.latLng.lng(), e.latLng.lat());
                    alert("Location: " + results[1].formatted_address + "\r\nLatitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                }
            }
        });
    });
}

google.maps.event.addListener(marker, 'click', function () {
    infowindow.open(map, marker);
});

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

function SetLngLat(lng, lat) {
    //$("")
}

