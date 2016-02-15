var increment = 1;
function addField() {
    $("#phoneNumber").append("<div class='form-group form-inline'> <div class='col-md-10'> Phone Number <input class='form-control' type='text' name=\"PhoneNumber" + (increment += 1) + "\" pattern='^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$'/></div><div class='col-md-10'>Phone Type <input class='form-control' type='tel' name=\"PhoneType" + (increment) + "\" /></div></div>")
};

function AddPerson() {
    $("#modelDialog").modal();
    $.ajax({
        method: "GET",
        url: '/Persons/AddPerson',
        success: function (result) {
            $("#dialogContent").html(result);
        }
    })
};

function DeletePerson(id) {
    $("#modelDialog").modal();
    $.ajax({
        method: "GET",
        url: '/Persons/Delete',
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
        url: '/Persons/Edit',
        data: { "PersonID": id },
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