var hospitalcode = document.mynamespace.hospitalcode;
    var vitaladddata = [];
$(document).ready(function () {
    var hospitalcode = document.mynamespace.hospitalcode;
        ////debugger;
        $('#path').html('Discharge Referal Form');
        $('#RemoveInvestigationDoc').hide();
        $('#RemoveReferalDoc').hide();
        const params = new Proxy(new URLSearchParams(window.location.search), {
            get: (searchParams, prop) => searchParams.get(prop),
        });
        if (params.URN != null) {
            let urn = params.URN; // URN=21111770150000133
            $("#txtPatientUrn").val(urn).attr("readonly", "true");
            $(".formDirect").remove();
            $('#txtHospitalReferTo').val(hospitalcode);
            getUrnData($("#txtPatientUrn").val(), "Family");
        }
        else {
            $(".formRedirect").remove();
        }

        $(".dateFormat").datepicker({
            dateFormat: 'dd-M-y',
            defaultDate: new Date(),
            maxDate: 0,
            onSelect: function (date, datepicker) {
                if (date != "") {
                    $(this).removeClass("border-danger")
                    $(this).prev("span").text("").hide();
                }
            }
        });
        
        getCurrentDate();
        getVitalsList();
        getStates();

        $(".inputRequired").focusout(function () {
            if ($.trim($(this).val()) != "") {
                $(this).removeClass("border-danger")
                $(this).prev("span").text("").hide();
            }
        });
        $(".dropdownRequired").change(function () {
            if ($(this).val() != "0") {
                $(this).removeClass("border-danger")
                $(this).prev("span").text("").hide();
            }
        });
    });

    $("#btnSearchUrn").click(function () {
        if ($("#txtPatientUrn").val() != "") {
            getUrnData($("#txtPatientUrn").val(), "Family");
        }
        else {
            swal({
                text: "Please enter NFSA/SFSS or BSKY card no.",
                confirmButtonColor: "#36A865",
                type: "error",
                confirmButtonText: "OK",
                closeOnConfirm: false
            }).then(function () {
                $("#txtPatientUrn").focus();
            });
        }
    });

    function getUrnData(data, mode) {
        var params = { URN: data };
        $.ajax({
            url: ServiceURL + "/api/URN/GetFamilyMemeberList",
            type: "GET",
            data: params,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loader").show();
            },
            error: function (error) {
                console.log(error.statusText);
            },
            complete: function () {
                $("#loader").hide();
            }
        }).done(function (result) {
            //console.log(result);
            if (result.length > 0) {
                if (mode == "Family") {
                    $("#ddPatientName").empty().append('<option value="0">-Select-</option>').removeClass("border-success border-danger");
                    $("#txtPatientAge").val('');
                    $("#ddPatientGender").val("0");
                    $.each(result, function (i, data) {
                        $("#ddPatientName").append('<option value=' + data.memberID + '>' + data.memberName + '</option>');
                        $("#ddPatientName").addClass("border-success");
                    });
                }
                else {
                    // Patient selected from dropdown
                    if (mode != "0") {
                        for (i = 0; i < result.length; i++) {
                            if (result[i].memberID == mode) {
                                $("#ddPatientName").removeClass("border-success border-danger");
                                $("#txtPatientAge").val(result[i].memberAge).removeClass("border-danger");
                                $("#txtPatientAge").prev("span").text("").hide();
                                $("#ddPatientGender option").filter(function () {
                                    return $(this).text() == result[i].memberGender;
                                }).prop('selected', true);
                                $("#ddPatientGender").removeClass("border-danger").prev("span").text("").hide();
                            }
                        }
                    }
                    else {
                        $("#txtPatientAge").val('');
                        $("#ddPatientGender").val("0");
                    }
                }
            }
            else {
                sweetAlert("Alert", "Family members details not found!");
                $("#ddPatientName").empty().append('<option value="0">-Select-</option>').removeClass("border-success border-danger");
                $("#txtPatientAge").val('');
                $("#ddPatientGender").val("0");
            }
        });
    }

    $('#ddPatientName').change(function () {
        getUrnData($("#txtPatientUrn").val(), $("#ddPatientName").val());
    });

    function ClearPage() {
        if ($('#txtPatientUrn').prop('readonly')) {
            $('input[type=text]:not("#txtHospitalReferTo"):not("#txtPatientUrn"), [type=file], textarea').val('');
        }
        else {
            $('input[type=text], [type=file], textarea').val('');
            $("#ddPatientName").empty().append('<option value="0">-Select-</option>').removeClass("border-success");
        }
        $("select").val("0");
        getCurrentDate();
        $("#tblVitals > tbody").html("");
        vitaladddata = [];
        $("html, body").animate({ scrollTop: "0" }, 500);
    }

    function ClosePage() {
        window.location.href = "/HPSBYS.Web/Dashboard/Index" /*'@Url.Action("Index", "Dashboard")'*/;
    }

    function getCurrentDate() {
        var d = new Date();
        $(".dateFormat").val(moment(new Date(d.getFullYear(), d.getMonth(), d.getDate())).format("DD-MMM-YY"));
    }

    function getVitalsList() {
        var params = { Action: 'A' };
        $.ajax({
            url: ServiceURL + "/api/Common/GetVitalParameter",
            type: "GET",
            data: params,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                //console.log(data);
                $('#ddVitals').empty().append('<option selected="selected" value="0">-Select-</option>');
                $.each(data, function () {
                    $('#ddVitals').append($("<option></option>").val(this['id']).text(this['vitalsign']));
                });
            },
            error: function (error) {
                console.log(error.statusText);
            }
        });
    }

    function validateVitals() {
        if ($("#ddVitals").val() == "0") {
            $("#errddVitals").text("Please select parameter!").show();
            $("#ddVitals").addClass("border-danger").focus();
            return false;
        }
        else {
            var flag = "0";
            $.each(vitaladddata, function (key, val) {
                if ($("#ddVitals").val() == val.vitalsignid) {
                    $("#errddVitals").text("Parameter already added!").show();
                    $("#ddVitals").addClass("border-danger").focus();
                    flag = "1";
                }
            });
            if (flag == "1") {
                return false;
            }
        }
        if ($.trim($("#txtVitalsValue").val()) == "") {
            $("#errVitalsValue").text("Value cannot be blank!").show();
            $("#txtVitalsValue").addClass("border-danger").focus();
            return false;
        }
        else {
            $("#ddVitals").removeClass("border-danger")
            $("#errddVitals").text("").hide();
            $("#txtVitalsValue").removeClass("border-danger")
            $("#errVitalsValue").text("").hide();
            return true;
        }
    }

    function validateForm() {
        var flag = "0";
        $(".inputRequired").each(function (i, obj) {
            if ($.trim($(this).val()) == "") {
                $(this).addClass("border-danger");
                $(this).prev("span").text("Field cannot be blank!").show();
                flag = "1";
                //console.log($(this));
            }
        });
        $(".dropdownRequired").each(function (i, obj) {
            if ($.trim($(this).val()) == "0") {
                $(this).addClass("border-danger");
                $(this).prev("span").text("Field must be selected!").show();
                flag = "1";
                //alert($(this)); console.log($(this));
            }
        });
        if (flag == "1") {
            $('html, body').animate({
                scrollTop: ($('.text-danger').first().offset().top)
            }, 500);
            return false;
        }
        return true;
    }

    //Add More Vital Parameter.
    $("#btnAddVitals").click(function () {
        if (validateVitals()) {
            ////debugger;
            /* alert($("#tblVitalParam > tbody > tr").length);*/
            var vitalsignid = parseInt($("#tblVitals > tbody > tr").length, 10) + 1;
            var vitalid = $('#ddVitals').find(":selected").val();
            var vitalname = $('#ddVitals').find(":selected").text();
            var vtlParamVal = $("#txtVitalsValue").val();
            var AddParamVal = "<tr><td>" + vitalsignid + "</td><td>" + vitalname + "</td><td>" + vtlParamVal + "</td><td><button type='button' id='btnremovevital' onclick='removeVitalsData(this)' class='btn btn-outline-danger focusedBtn btn-sm my-2 my-sm-0'><i class='fa fa-trash'></i></button></td></tr>";
            $("#tblVitals").append(AddParamVal);
            //var d = {"vitalsignid": vitalsignid, "vitalsign": vitalname, "vitalvalue": vtlParamVal };
    //vitaladddata.push(d);
    vitaladddata.push({"vitalsignid": vitalid, "vitalsign": vitalname, "vitalvalue": vtlParamVal });
    console.log("vitaladddata parameter add", vitaladddata);
    $("#ddVitals").val("0");
    $("#txtVitalsValue").val("");
        }
    });

    // Remove Vital Record
    function removeVitalsData(ele) {
        var currentRow = $(ele).closest("tr");
    var col1 = currentRow.find("td:eq(0)").text();
    var col2 = currentRow.find("td:eq(1)").text();
    var col3 = currentRow.find("td:eq(2)").text();
    var removedata = {"vitalsignid": col1, "vitalsign": col2, "vitalvalue": col3 };
    vitaladddata.splice((parseInt(removedata.vitalsignid) - 1), 1);
    $(ele).closest('tr').remove();

        $("#tblVitals > tbody > tr").each(function (i) {
        $(this).find("td:eq(0)").text(i + 1);
        });
    console.log(vitaladddata);
    }

    $('#ddState').change(function () {
        var stateCode = $(this).val();
    $('#ddDistrict').empty();
    $('#ddDistrict').html('<option value="0">-Select-</option>');
    $('#ddHospitalReferTo').empty();
    $('#ddHospitalReferTo').html('<option value="0">-Select-</option>');
    getDistricts(stateCode);
    });

    $('#ddDistrict').change(function () {
        var stateCode = $('#ddState').val();
    var districtCode = $(this).val();
    $('#ddHospitalReferTo').empty();
    $('#ddHospitalReferTo').html('<option value="0">-Select-</option>');
    getHospitals(stateCode, districtCode);
    });

    function getStates() {
        var params = {Action: 'S' };
    $.ajax({
        url: ServiceURL + "/api/Common/GetAllState",
    type: 'GET',
    data: params,
    contentType: 'application/json; charset=UTF-8',
    success: function (result) {
        // //debugger; console.log(result);
        $.each(result, function (i, data) {
            $("#ddState").append('<option value=' + data.stateCode + '>' + data.stateName + '</option>');
        });
            },
    error: function (jqXhr, textStatus, errorMessage) {
        alert(textStatus);
            }
        });
    }

    function getDistricts(stateCode) {
        var params = {Action: 'D', StateCode: stateCode };
    $.ajax({
        url: ServiceURL + "/api/Common/GetDistrictsByStateCode",
    type: 'GET',
    data: params,
    contentType: 'application/json; charset=UTF-8',
    success: function (result) {
        // //debugger; console.log(result);
        $.each(result, function (i, data) {
            $("#ddDistrict").append('<option value=' + data.districtCode + '>' + data.districtName + '</option>');
        });
            },
    error: function (jqXhr, textStatus, errorMessage) {
        alert(textStatus);
            }
        });
    }

    function getHospitals(stateCode, districtCode) {
        var params = {Action: 'H', StateCode: stateCode, DistrictCode: districtCode };
    $.ajax({
        url: ServiceURL + "/api/Common/GetHospitalByStateCodeAndDistrictCode",
    type: 'GET',
    data: params,
    contentType: 'application/json; charset=UTF-8',
    success: function (result) {
        // //debugger; console.log(result);
        $.each(result, function (i, data) {
            $("#ddHospitalReferTo").append('<option value=' + data.hospitalCode + '>' + data.hospitalname + '</option>');
        });
            },
    error: function (jqXhr, textStatus, errorMessage) {
        alert(textStatus);
            }
        });
    }

    function confirmPatientReferralForm() {
        if (validateForm()) {
        swal({
            title: "Are you sure?",
            text: "You wish to submit this form",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes"
        }).then(function (result) {
            if (result.value) {
                $("#btnSubmit").attr("disabled", true);
                submitPatientReferralForm();
            }
        });
        }
    }

function submitPatientReferralForm() {
    var hospitalcode = document.mynamespace.hospitalcode;
    var userID = document.mynamespace.UserId;
        console.log(vitaladddata);
        //debugger;
        //if (validateForm()) {
            var PatientReferralData = new FormData();
    var InvestigationReportfile = $("#fileInvestigationReport").get(0);
    var InvestigationReportDoc = InvestigationReportfile.files;
            if (InvestigationReportDoc.length > 0) {PatientReferralData.append("InvestigationDoc", InvestigationReportDoc[0]); }
    var ReferralDocumentfile = $("#referaluploaddocs").get(0);
    var ReferralDoc = ReferralDocumentfile.files;
            if (ReferralDoc.length > 0) {PatientReferralData.append("ReferralDoc", ReferralDoc[0]); }
    PatientReferralData.append("__RequestVerificationToken", gettoken());
    PatientReferralData.append("Action", "A");
    PatientReferralData.append("URN", $('#txtPatientUrn').val());
    PatientReferralData.append("MemberId", $('#ddPatientName').val());
    PatientReferralData.append("HospitalCode", hospitalcode);
    PatientReferralData.append("ReferralDate", $('#txtPatientFormDate').val());
    var referralCode = Math.floor(100000 + Math.random() * 900000);
    PatientReferralData.append("ReferralCode", referralCode);
    PatientReferralData.append("PatientName", $('#ddPatientName option:selected').text());
    PatientReferralData.append("Age", $('#txtPatientAge').val());
    PatientReferralData.append("Gender", $('#ddPatientGender').val());
    PatientReferralData.append("RegdNo", $('#txtRegdNo').val());
    PatientReferralData.append("FromHospitalName", $('#txtHospitalReferFrom').val());
    PatientReferralData.append("FromHospitalCode", "0");
    PatientReferralData.append("FromDrName", $('#txtDoctorName').val());
    PatientReferralData.append("FromDeptName", $('#ddDepartmentFrom').val());
    PatientReferralData.append("FromReferralDate", $('#txtReferralFromDate').val());
    if ($('#ddState').val() != null) {
        PatientReferralData.append("ToState", $('#ddState').val());
    PatientReferralData.append("ToDistrict", $('#ddDistrict').val());
    PatientReferralData.append("ToHospital", $('#ddHospitalReferTo option:selected').text());
    PatientReferralData.append("ToHospitalCode", $('#ddHospitalReferTo').val());
            }
    else {
        PatientReferralData.append("ToHospitalCode", hospitalcode);
    PatientReferralData.append("ToHospital", $('#txtHospitalReferTo').val());
            }
    PatientReferralData.append("ReasonForRefer", $('#txtReasonForRefer').val());
    PatientReferralData.append("ToReferralDate", $('#txtReferralToDate').val());
    PatientReferralData.append("Diagnosis", $('#txtDiagnosis').val());
    PatientReferralData.append("PatientBriefHistory", $('#txtPatientBriefHistory').val());
    PatientReferralData.append("TreatmentGiven", $('#txtTreatmentGiven').val());
    PatientReferralData.append("InvestigationRemark", 'NA');
    PatientReferralData.append("TreatmentAdvised", $('#txtTreatmentAdvised').val());
    //PatientReferralData.append("Document", 'Doc');
    PatientReferralData.append("UserId", userID);

    var vitaldataPushInModel = [];
    vitaldataPushInModel.push(vitaladddata);

    $.each(vitaldataPushInModel, function (index, jsonObject) {
        $.each(jsonObject, function (key, val) {
            PatientReferralData.append("VitalParameterList[" + key + "].VITALSIGNID", val.vitalsignid);
            //PatientReferralData.append("VitalParameterList[" + key + "].VITALSIGN", val.vitalsign);
            PatientReferralData.append("VitalParameterList[" + key + "].VITALVALUE", val.vitalvalue);
        });
            }); //VitalParameterList = model.VitalParameterList,
    //PatientReferralData.append("XmlVitalParameter", 'XML');
    //}

    $.ajax({
        url: "PatientReferralRequest"/*'@Url.Action("PatientReferralRequest", "Discharge")'*/,
    type: "POST",
    data: PatientReferralData,
    processData: false,
    contentType: false,
    beforeSend: function () {
        $("#loader").show();
                },
    success: function (data) {
        console.log(data);
                    if (data.length > 0) {
        sweetAlert("Alert", "Patient referral generated successfully!")
            .then(function () {
                window.location.href = "PatientReferralView"/*'@Url.Action("PatientReferralView", "Discharge")'*/;
            });
                    }
    else {
        sweetAlert("Alert", "Patient referral failed!");
    $("#btnSubmit").attr("disabled", false);
                    }
                },
    complete: function () {
        $("#loader").hide();
                },
    error: function (error) {
        console.log(error.statusText);
                }
            });
        //}
    }
    $(function () {
        $('#txtRegdNo').keydown(function (e) {
            if (e.shiftKey || e.ctrlKey || e.altKey) {
                e.preventDefault();
            } else {
                var key = e.keyCode;
                if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                    e.preventDefault();
                }
            }
        });
    });
    function inputLimiter(e, allow) {
        var AllowableCharacters = '';
    if (allow == 'NameCharactersymbol') {
        AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz./&';
        }
    if (allow == 'NameCharacters') {
        AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-.\'';
        }
        //if (allow == 'NameAndNumbers') {
        //    AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
        //}
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

    $("#fileInvestigationReport").on("change", function (e) {
        //debugger;
        var fileExtension = ['pdf', 'jpeg','jpg'];
    var filename = $('#fileInvestigationReport').val();
    var extension = filename.replace(/^.*\./, '');
    var fileSize = $("#fileInvestigationReport")[0].files[0].size;
    if ($.inArray(extension.toLowerCase(), fileExtension) == -1 && filename.length != 0) {
        swal({
            text: "Please upload a valid file of type pdf,jpeg,jpg!!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#fileInvestigationReport").focus();
            $("#fileInvestigationReport").val("");
        });
    return false;
        }
        if (fileSize > 5000141) {
        swal({
            text: "File size cannot be greater than 5MB !!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#fileInvestigationReport").focus();
            $("#fileInvestigationReport").val("");
        });
    return false;
        }
    else {
        $("#RemoveInvestigationDoc").show();
        }
    });

    $("#referaluploaddocs").on("change", function (e) {
        //debugger;
        var fileExtension = ['pdf', 'jpeg', 'jpg'];
    var filename = $('#referaluploaddocs').val();
    var extension = filename.replace(/^.*\./, '');
    var fileSize = $("#referaluploaddocs")[0].files[0].size;
    if ($.inArray(extension.toLowerCase(), fileExtension) == -1 && filename.length != 0) {
        swal({
            text: "Please upload a valid file of type pdf,jpeg,jpg!!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#referaluploaddocs").focus();
            $("#referaluploaddocs").val("");
        });
    return false;
        }
        if (fileSize > 5000141) {
        swal({
            text: "File size cannot be greater than 5MB !!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#referaluploaddocs").focus();
            $("#referaluploaddocs").val("");
        });
    return false;
        }
    else {
        $("#RemoveReferalDoc").show();
        }
    });

    function RemoveReferalDoc(ele) {
        $("#referaluploaddocs").val("");
    $('#RemoveReferalDoc').hide();
    }

    function RemoveInvestigationDoc(ele) {
        $("#fileInvestigationReport").val("");
    $('#RemoveInvestigationDoc').hide();
    }

    function inputLimiter(e, allow) {
        var AllowableCharacters = '';
    if (allow == 'NameCharactersymbolDoctor') {
        AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.';
        }
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

    // breadcrumb active & menu active
        $(document).ready(function () {
            loadNavigation('PatientReferalForm', 'MtReferal', 'pl', 'Discharge', 'Patient Referral Add', '');
    });
    