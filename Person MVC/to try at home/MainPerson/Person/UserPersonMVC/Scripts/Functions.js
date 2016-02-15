var increment = 1;
function addField() {
    $("#phoneNumber").append("<div class='form-group form-inline'> <div class='col-md-10'> Phone Number <input class='form-control' type='text' name=\"PhoneNumber" + (increment += 1) + "\" pattern='^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$'/></div><div class='col-md-10'>Phone Type <input class='form-control' type='tel' name=\"PhoneType" + (increment) + "\" /></div></div>")
    //$("#phoneNumber").append("<div class='col-md-10'> @Html.EditorFor(model => model.PhoneNumbers[" + increment + "].PhoneNumber, new { htmlAttributes = new { @class = 'form-control' } }) @Html.ValidationMessageFor(model => model.PhoneNumbers[" + increment + "].PhoneNumber, '', new { @class = 'text-danger' })</div> <div class='col-md-10'> @Html.EditorFor(model => model.PhoneNumbers[" + increment + "].PhoneNumberType, new { htmlAttributes = new { @class = 'form-control' } }) @Html.ValidationMessageFor(model => model.PhoneNumbers[" + increment + "].PhoneNumberType, '', new { @class = 'text-danger' })</div>)");
    increment++;
};

function AddPerson() {
    $("#modelDialog").modal();
    $.ajax({
        method: "GET",
        url: '/User/AddPerson',
        success: function (result) {
            $("#dialogContent").html(result);
        }
    })
};

function DeletePerson(id) {
    $("#modelDialog").modal();
    $.ajax({
        method: "GET",
        url: '/User/Delete',
        data: { "personID": id },
        success: function (result) {
            $("#dialogContent").html(result);
        }
    })
};

function EditPerson(id) {
    $("#modelDialog").modal();
    $.ajax({
        method: "GET",
        url: '/User/Edit',
        data: { "PersonID": id },
        success: function (result) {
            $("#dialogContent").html(result);
        }
    })
};

function AddAddress(id/*, firstName, lastname, age*/) {
    $("#modelDialog").modal();
    $.ajax({
        method: "GET",
        url: '/User/AddAddress',
        data: { "PersonID": id/*, "FirstName": firstName, "LastName": lastName, "Age": age */},
        success: function (result) {
            $("#dialogContent").html(result);
        }
    })
};

function AddPicture() {
    $("#modelDialog").modal();
    $.ajax({
        method: "GET",
        url: '/User/AddPicture',
        success: function (result) {
            $("#dialogContent").html(result);
        }
    })
};

function Calculate() {
    var res = 0;
    var operator = $("#operator").val();
    var number1 = $("#x").val();
    var number2 = $("#y").val();
    switch (operator) {
        case '+':
            res = +number1 + +number2;
            break;
        case '-':
            res = number1 - number2;
            break;
        case '*':
            res = number1 * number2;
            break;
        case '/':
            res = number1 / number2;
            break;
        default:
            throw new Error('This operator does not exist!');
            break;
    }
    $("#res").val(res);
};


$("#submitButton").click(function () {
    $('#modelDialog').modal('hide');
    //$("#modalWindow").hide();
});
//function CloseModalWindow() {
//    $('#modelDialog').modal('hide');
//    if (result.indexOf("table") > 0) {
//        $('body').removeClass('modal-open');
//        $('.modal-backdrop').remove();
//        $('#bodyModal').html("");
//        $("#tableContainer").html(result);
//    }

//    else {
//        $("#bodyModal").html(result);
//    }
//}