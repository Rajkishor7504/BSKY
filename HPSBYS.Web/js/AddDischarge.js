function validateDischarge() {
    if ($('#dischargeDate').val() == "") {
        alert('Please Enter Discharge Date!!!');
        $('#dischargeDate').focus();
        return false;
    }
    else if ($("#TreComCertificate").val() == "") {
        alert('Please Upload Treatment Completion Certificate !!!');
        $('#TreComCertificate').focus();
        return false;
    }
    else if ($('#description').val() == "") {
        alert('Please Enter Description!!!');
        $('#description').focus();
        return false;
    }
    else if ($('#mortality').val() == 0) {
        alert('Please Select Mortality!!!');
        $('#mortality').focus();
        return false;
    }
    else if ($('#mSummery').val() == "") {
        alert('Please Enter Mortality Summery!!!');
        $('#mSummery').focus();
        return false;
    }
    else {
        return true;
    }
}
function validateDownward() {
    if ($('#refReqYes').prop("checked")) {
        if ($('#ddlDistrict').val() == 0) {
            alert('Please Select District!!!');
            $('#ddlDistrict').focus();
            return false;
        }
        else if ($('#ddlHlthBlk').val() == 0) {
            alert('Please Select Health Block!!!');
            $('#ddlHlthBlk').focus();
            return false;
        }
        else if ($('#ddlPhc').val() == 0) {
            alert('Please Select Name Of PHC!!!');
            $('#ddlPhc').focus();
            return false;
        }
        //else if ($('#ddlSubCntr').val() == 0) {
        //    alert('Please Select Name Of SubCenter!!!');
        //    $('#ddlSubCntr').focus();
        //    return false;
        //}
        else {
            return true;
        }
    }
    else {
        return true;
    }
    if ($('#dischargeDate').val() == "") {
        alert('Please Enter Discharge Date!!!');
        $('#dischargeDate').focus();
        return false;
    }
    else if ($("#TreComCertificate").val() == "") {
        alert('Please Upload Treatment Completion Certificate !!!');
        $('#TreComCertificate').focus();
        return false;
    }
    else if ($('#description').val() == "") {
        alert('Please Enter Description!!!');
        $('#description').focus();
        return false;
    }
    else if ($('#mortality').val() == 0) {
        alert('Please Select Mortality!!!');
        $('#mortality').focus();
        return false;
    }
    else if ($('#mSummery').val() == "") {
        alert('Please Enter Mortality Summery!!!');
        $('#mSummery').focus();
        return false;
    }
    else {
        return true;
    }
}
function checkconfirm() {
    if (window.confirm("Are you sure to Discharge?")) {
        return true;
    }
    else {
        return false;
    }
}
function cinformDischargedate(NextReqDate) {
    if (window.confirm("The discharge date will be treated as: " + NextReqDate.toDateString() + " ,Are you sure to Continue?")) {
        return true;
    }
    else {
        return false;
    }
}
function BalCheckCinform() {
    if (window.confirm("Your Actual Amount is More than Card Balance ,Are you sure to Continue?")) {
        return true;
    }
    else {
        return false;
    }
}
function ValidateFile(cntr, strText) {

    var strValue = $('#' + cntr).get(0).files.length;
    if (strValue == "0") {
        alert("Please upload " + strText);
        return false;
    }
    else
        return true;
}
function CheckFileType(cntr, ftype) {

    // Get the file upload control file extension
    var extn = $('#' + cntr).val().split('.').pop().toLowerCase();
    if (extn != '') {

        // Create array with the files extensions to upload
        var fileListToUpload;
        if (parseInt(ftype) == 1)
            fileListToUpload = new Array('pdf', 'jpg', 'jpeg');
        else if (parseInt(ftype) == 2)
            fileListToUpload = new Array('gif', 'jpg', 'jpeg');
        else
            fileListToUpload = new Array('pdf');

        //Check the file extension is in the array.
        var isValidFile = $.inArray(extn, fileListToUpload);

        // isValidFile gets the value -1 if the file extension is not in the list.
        if (isValidFile == -1) {
            if (parseInt(ftype) == 1) {
                alert('Please upload a valid document of type pdf/jpg/jpeg.!!!');
                $('#' + cntr).replaceWith($('#' + cntr).val('').clone(true));
            }
            else if (parseInt(ftype) == 2) {
                alert('Please upload a valid document of type gif/jpg/jpeg.!!!');
                $('#' + cntr).replaceWith($('#' + cntr).val('').clone(true));
            }
            else {
                alert('Please Upload a valid document !!!');
                $('#' + cntr).replaceWith($('#' + cntr).val('').clone(true));
            }
        }
        else {
            // Restrict the file size to 2MB(1024 * 2048;)
            if ($('#' + cntr).get(0).files[0].size > (200000)) {
                alert('Document size should not exceed 200KB.!!!');
                $('#' + cntr).replaceWith($('#' + cntr).val('').clone(true));
            }
            if ($('#' + cntr).get(0).files[0].name.length > 100) {
                alert('Document Name should be maximum 100 Characters !!!');
                $('#' + cntr).replaceWith($('#' + cntr).val('').clone(true));
            }
            else
                return true;
        }
    }
    else
        return true;
}
function inputLimiter(e, allow) {
    var AllowableCharacters = '';
    if (allow == 'NameCharactersymbol') {
        AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz./&';
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