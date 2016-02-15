//$(function () {
function ChangeCities() {
    //$("#person_Address_0__Street_City_Country").change(function () {
    var selectedCountry = $("#person_Address_0__Street_City_Country :selected").val();
    selectedCountry = selectedCountry == "" ? 0 : selectedCountry;
    if (selectedCountry != "") {
        $("#person_Address_0__Street_City").empty();
    }

    $.ajax({
        method: "POST",
        url: "/User/GetCities",
        data: { "CountryID": selectedCountry },
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (data) {
            if (data != null) {
                $("#person_Address_0__Street_City").empty();
                $.each(data, function (index, data) {
                    $("#person_Address_0__Street_City").append('<option value="' + data.Value + '">' + data.Text + '</option>');
                });
            }
        }
        }).fail(function (responce) {
            if (responce.status != 0) {
                alert(responce.status + " " + responce.statusText);
            }
    })
}
//});