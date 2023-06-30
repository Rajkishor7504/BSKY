$('input').on('keypress', function (e) {
    var blockSpecialRegex = /[~`*"|'!@#$%^&()_={}[\]:;,.<>+\/?-]/;
    var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (blockSpecialRegex.test(key)) {
        e.preventDefault();
        return false;
    }
});
$('.inputOnlyNumber').on('keypress', function (e) {
    var blockSpecialRegex = /[~`*"|'!@#$%^&()_={}[\]:;,.<>+\/?-A-Za-z]/;
    var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (blockSpecialRegex.test(key)) {
        e.preventDefault();
        return false;
    }
});