
/* INDEX
 * FN#01 :- Date selector binding.
 * FN#02 :- Input limiter validation.
 * FN#03 :- State/District/Hospital dropdown binding.
 */

// ------------------------------------------------------------------------- //

/* FN#01 :- Date selector binding.
 *
 * Step 1: To bind date selector - Add class -> 'datepicker' to the Date input element.
 */

$(function () {
    $(".datepicker").datepicker({
        numberOfMonths: 1,
        changeMonth: true,
        dateFormat: "dd-M-y",
        changeYear: true
    });
});

/* FN#01 :- THE END */

// ------------------------------------------------------------------------- //

/* FN#02 :- Input Limiter binding.
 *
 * Step 1: To limit specific keypress - Add -> onkeypress="return inputLimiter(event,'Numbers')" to the input element.
 */

function inputLimiter(e, allow) {
    var AllowableCharacters = '';
    if (allow == 'NameCharactersymbol') {
        AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz./&';
    }
    if (allow == 'NameCharactersymbolDoctor') {
        AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.';
    }
    if (allow == 'NameCharacters') {
        AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-.\'';
    }
    if (allow == 'NameCharactersAndNumbers') {
        AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-,/.\'';
    }
    if (allow == 'Description') {
        AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_-,./\&(){}[]#$:';
    }
    if (allow == 'Numbers') {
        AllowableCharacters = '1234567890';
    }
    if (allow == 'Decimal') {
        AllowableCharacters = '1234567890.';
    }
    var k;
    k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
    if (k != 13 && k != 8 && k != 0) {
        if ((e.ctrlKey == false) && (e.altKey == false)) {
            return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }
}

/* FN#02 :- THE END */

// ------------------------------------------------------------------------- //

/* FN#03 :- State/District/Hospital dropdown binding.
 * 
 * Step 1: Call -> getStates(); inside -> $(document).ready(function () { }.
 * Step 2: To bind list of States - Add class -> 'bindStates' to the StateDropdown element.
 * Step 3: To bind list of Districts -  Add -> onchange="bindDistricts(this, DistrictDropdownId, HospitalDropdownId);" to the State dropdown element.
 * Step 4: To bind list of Hospitals -  Add -> onchange="bindHospitals(StateDropdownId, this, HospitalDropdownId);" to the District dropdown element.
 */

function getStates() {
    var params = { Action: 'S' };
    $.ajax({
        url: ServiceURL + "/api/Common/GetAllState",
        type: 'GET',
        data: params,
        contentType: 'application/json; charset=UTF-8',
        success: function (result) {
            //debugger; console.log(result);
            $.each(result, function (i, data) {
                $(".bindStates").append('<option value=' + data.stateCode + '>' + data.stateName + '</option>');
            });
        },
        error: function (jqXhr, textStatus, errorMessage) {
            alert(textStatus);
        }
    });
}

function getDistricts(stateCode, element) {
    var params = { Action: 'D', StateCode: stateCode };
    $.ajax({
        url: ServiceURL + "/api/Common/GetDistrictsByStateCode",
        type: 'GET',
        data: params,
        contentType: 'application/json; charset=UTF-8',
        success: function (result) {
            //debugger; console.log(result);
            $.each(result, function (i, data) {
                $(element).append('<option value=' + data.districtCode + '>' + data.districtName + '</option>');
            });
        },
        error: function (jqXhr, textStatus, errorMessage) {
            alert(textStatus);
        }
    });
}

function getHospitals(stateCode, districtCode, element) {
    var params = { Action: 'H', StateCode: stateCode, DistrictCode: districtCode };
    $.ajax({
        url: ServiceURL + "/api/Common/GetHospitalByStateCodeAndDistrictCode",
        type: 'GET',
        data: params,
        contentType: 'application/json; charset=UTF-8',
        success: function (result) {
            //debugger; console.log(result);
            $.each(result, function (i, data) {
                $(element).append('<option value=' + data.hospitalCode + '>' + data.hospitalname + '</option>');
            });
        },
        error: function (jqXhr, textStatus, errorMessage) {
            alert(textStatus);
        }
    });
}

function bindDistricts(mState, mDistrict, mHospital) {
    var stateCode = $(mState).val();
    $(mDistrict).empty();
    $(mDistrict).html('<option value="0">-Select-</option>');
    $(mHospital).empty();
    $(mHospital).html('<option value="0">-Select-</option>');
    getDistricts(stateCode, mDistrict);
}

function bindHospitals(nState, nDistrict, nHospital) {
    var stateCode = $(nState).val();
    var districtCode = $(nDistrict).val();
    $(nHospital).empty();
    $(nHospital).html('<option value="0">-Select-</option>');
    getHospitals(stateCode, districtCode, nHospital);
}

/* FN#03 :- THE END */

// ------------------------------------------------------------------------- //