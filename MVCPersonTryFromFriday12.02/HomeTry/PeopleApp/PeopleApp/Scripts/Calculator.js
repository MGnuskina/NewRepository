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