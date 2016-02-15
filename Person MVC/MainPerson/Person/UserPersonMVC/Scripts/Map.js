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

    google.maps.event.addListener(map, 'click', function (e) {
        var latlng = new google.maps.LatLng(e.latLng.lat(), e.latLng.lng());
        var geocoder = geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[1]) {
                    SetLngLat(e.latLng.lng(), e.latLng.lat());
                    for (ac = 0; ac < results[1].address_components.length; ac++) {
                        if (results[1].address_components[ac].types[0] == "street_number") { $("#buildingNumber").val(results[1].address_components[ac].long_name); }
                        if (results[1].address_components[ac].types[0] == "route") { $("#street").val(results[1].address_components[ac].short_name); }
                        if (results[1].address_components[ac].types[0] == "locality") { $("#city").val(results[1].address_components[ac].long_name); }
                        if (results[1].address_components[ac].types[0] == "country") { $("#country").val(results[1].address_components[ac].long_name); }
                    }
                    $("#lat").val(e.latLng.lat());
                    $("#lng").val(e.latLng.lng());
                }
            }
        });

        google.maps.event.addListener(marker, 'click', function () {
            infowindow.open(map, marker);
        });
    });
}


function FindLatLng() {
        var address = $("#street").val() + "," + $("#buildingNumber").val() + "," + $("#city").val() + "," + $("#country").val();
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


