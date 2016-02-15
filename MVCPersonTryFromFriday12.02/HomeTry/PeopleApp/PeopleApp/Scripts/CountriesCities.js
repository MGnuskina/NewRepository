$(function () {
    //function ChangeCities() {
    $("#countries").change(function () {
        var selectedCountry = $("#countries :selected").val();
        selectedCountry = selectedCountry == "" ? 0 : selectedCountry;
        if (selectedCountry != "") {
            $("#cities").empty();
        }

        $.ajax({
            method: "POST",
            url: "/People/GetCities",
            data: { "CountryID": selectedCountry },
            //contentType: "application/json; charset=utf-8",
            //dataType: "json",
            success: function (data) {
                if (data != null) {
                    $("#cities").empty();
                    $.each(data, function (index, data) {
                        $("#cities").append('<option value="' + data.Value + '">' + data.Text + '</option>');
                    });
                }
            }
        }).fail(function (responce) {
            if (responce.status != 0) {
                alert(responce.status + " " + responce.statusText);
            }
        })
    });
});