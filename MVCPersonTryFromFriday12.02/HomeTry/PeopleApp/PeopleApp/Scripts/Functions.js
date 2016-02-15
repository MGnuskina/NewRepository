var increment = 1;
function addField() {
    $("#phoneNumber").append("<div class='form-group form-inline'> <div class='col-md-10'> Phone Number <input class='form-control' type='text' name=\"PhoneNumber" + (increment += 1) + "\" pattern='^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$'/></div><div class='col-md-10'>Phone Type <input class='form-control' type='tel' name=\"PhoneType" + (increment) + "\" /></div></div>")
    increment++;
};

//function AddPerson() {
//    $("#modelDialog").modal();
//    $.ajax({
//        method: "GET",
//        url: '/People/AddPerson',
//        success: function (result) {
//            $("#dialogContent").html(result);
//        }
//    })
//};

function DeletePerson(id) {
    $("#modelDialog").modal();
    $.ajax({
        method: "GET",
        url: '/People/Delete',
        data: { "personID": id },
        success: function (result) {
            $("#dialogContent").html(result);
        }
    })
};

//function EditPerson(id) {
//    $("#modelDialog").modal();
//    $.ajax({
//        method: "GET",
//        url: '/People/Edit',
//        data: { "PersonID": id },
//        success: function (result) {
//            $("#dialogContent").html(result);
//        }
//    })
//};

//function AddAddress(id) {
//    $("#modelDialog").modal();
//    $.ajax({
//        method: "GET",
//        url: '/People/AddAddress',
//        data: { "PersonID": id },
//        success: function (result) {
//            $("#dialogContent").html(result);
//        }
//    })
//};

function AddPhoneNumber(id) {
    $("#modelDialog").modal();
    $.ajax({
        method: "GET",
        url: '/People/AddPN',
        data: { "PersonID": id },
        success: function (result) {
            $("#dialogContent").html(result);
        }
    })
};

function AddPicture() {
    $("#modelDialogUser").modal();
    $.ajax({
        method: "GET",
        url: '/User/AddPicture',
        success: function (result) {
            $("#dialogContent").html(result);
        }
    })
};

$("#submitButton").click(function () {
    $('#modelDialog').modal('hide');
});