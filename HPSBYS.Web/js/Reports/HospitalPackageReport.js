
var hospitalcode = document.mynamespace.hospitalcode;

$(document).ready(function () {
    loadNavigation('HospitalPackageReport', 'MtReports', 'pl', 'IPReports', 'Hospitals Report', '');
    $("#btnExcel1").hide();
    $("#btnExcel2").hide();
    $("#btnExcel3").hide();

    var d = new Date();
    var currentDate = d.getDate();
    var currMonth = d.getMonth();
    var currYear = d.getFullYear();
    var startDate = new Date(currYear, currMonth, 1);
    var endDate = new Date(currYear, currMonth, currentDate);
    $("#txtFromDate1").val(moment(startDate).format("DD-MMM-YY"));
    $("#txtToDate1").val(moment(endDate).format("DD-MMM-YY"));
    $("#txtFromDate2").val(moment(startDate).format("DD-MMM-YY"));
    $("#txtToDate2").val(moment(endDate).format("DD-MMM-YY"));
    $("#txtFromDate3").val(moment(startDate).format("DD-MMM-YY"));
    $("#txtToDate3").val(moment(endDate).format("DD-MMM-YY"));

    $(".datepicker").datepicker({
        numberOfMonths: 1,
        changeMonth: true,
        dateFormat: "dd-M-y",
        changeYear: true
    });

    getPackageBlockReport();
});

function getPackageBlockReport() {
    var param = {
        "Action": "BR", "HospitalCode": hospitalcode, "FromDate": $('#txtFromDate1').val(), "ToDate": $('#txtToDate1').val(),
        "IsPreAuth": $('#ddIsPreAuth1').val(), "DateType": $('#ddDateType1').val(), "Type": $('#ddBlockingMode1').val(), "AuthType": $('#ddAuthType1').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/HospitalPackageBlockReport",
        type: "POST",
        data: param,
        contentType: 'application/json',
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (response) {
            bindgrdPackageBlock(response);
        },
        error: function (error) {
            console.log(error.responseText);
        },
        complete: function () {
            $("#loader").hide();
        }
    });
}

function bindgrdPackageBlock(rData) {
    if (rData.length > 0) {
        $("#btnExcel1").show();
    }
    else {
        $("#btnExcel1").hide();
    }
    $("#grdPackageBlock").DataTable({
        dom: 'Blfrtip',
        buttons: [
            {
                extend: 'excel',
                messageTop: 'Period: ' + $("#txtFromDate1").val() + " To " + $("#txtToDate1").val(),
                title: 'HospitalPackageBlock-Report',
                exportOptions: { columns: 'th:not(.hiddenExport)' }
            },
        ],
        bLengthChange: true,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        bDestroy: true,
        bFilter: true,
        bSort: true,
        bPaginate: true,
        data: rData,
        columns: [
            { type: "text", title: "SL No", align: "center", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
            { data: "caseNo", type: "text", title: "Case No", align: "left" },
            { data: "urn", type: "text", title: "URN", align: "left" },
            {
                title: '<div class="prehed-bold"> Member Details </div><div class="prehed-small"> ID | Name | Is Pre-Auth </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    var memberDetails = '<b class="text-success">' + row.memberId + '</b>' + '<div class="pre-bold">' + row.memberName + '</div>'
                        + '<div class="badge badge-success badge-font mt-1">' + row.isPreAuth + '</div>';
                    return memberDetails;
                }
            },
            { data: "memberName", type: "text", title: "Member Name", visible: false },
            { data: "memberId", type: "text", title: "Member Id", visible: false },
            {
                title: '<div class="prehed-bold"> Package Details </div><div class="prehed-small"> Code | Name </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    return '<b class="text-success">' + row.procedureCode + '</b> <div class="pre-bold">' + row.packageName + '</div>';
                }
            },
            { data: "packageName", type: "text", title: "Package Name", visible: false },
            { data: "procedureCode", type: "text", title: "Package Code", visible: false },
            { data: "isPreAuth", type: "text", title: "Is PreAuth", visible: false },
            {
                title: '<div class="prehed-bold">Admission Date</div> <div class="prehed-small"> Block Date Time </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    return '<div><span class="pre-bold">' + row.admissionDate + '</span></div> <div><span class="pre-small">'
                        + row.blockDateTime.slice(0, 9) + '</span></div><div><span class="pre-small">'
                        + row.blockDateTime.slice(-11) + '</span></div>';
                }
            },
            { data: "admissionDate", type: "text", title: "Actual Admission Date", visible: false },
            { data: "blockDateTime", type: "text", title: "Block Date Time", visible: false },
            {
                title: '<div class="prehed-bold"> Verification Mode </div><div class="prehed-small"> Blocking Mode </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    return '<div class="badge badge-success badge-font">' + row.verificationMode + '</div><div class="pre-small">' + row.surgicalType + '</div>';
                }
            },
            { data: "surgicalType", type: "text", title: "Blocking Mode", visible: false },
            { data: "verificationMode", type: "text", title: "Verification Mode", visible: false }
        ]
    });
    $('.buttons-excel').hide(); // Hide default & download from custom button - btnExcel
    $('#btnExcel1').off().click(() => {
        $('#grdPackageBlock').DataTable().buttons(0, 0).trigger()
    });
}

function getPackageUnblockReport() {
    var param = {
        "Action": "UR", "HospitalCode": hospitalcode, "FromDate": $('#txtFromDate2').val(), "ToDate": $('#txtToDate2').val(),
        "IsPreAuth": $('#ddIsPreAuth2').val(), "DateType": $('#ddDateType2').val(), "AuthType": $('#ddAuthType2').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/HospitalPackageUnblockReport",
        type: "POST",
        data: param,
        contentType: 'application/json',
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (response) {
            bindgrdPackageUnblock(response);
        },
        error: function (error) {
            console.log(error.responseText);
        },
        complete: function () {
            $("#loader").hide();
        }
    });
}

function bindgrdPackageUnblock(rData) {
    console.log(rData);
    if (rData.length > 0) {
        $("#btnExcel2").show();
    }
    else {
        $("#btnExcel2").hide();
    }
    $("#grdPackageUnblock").DataTable({
        dom: 'Blfrtip',
        buttons: [
            {
                extend: 'excel',
                messageTop: 'Period: ' + $("#txtFromDate2").val() + " To " + $("#txtToDate2").val(),
                title: 'HospitalPackageUnBlock-Report',
                exportOptions: { columns: 'th:not(.hiddenExport)' }
            },
        ],
        bLengthChange: true,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        bDestroy: true,
        bFilter: true,
        bSort: true,
        bPaginate: true,
        data: rData,
        columns: [
            { type: "text", title: "SL No", align: "center", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
            { data: "caseNo", type: "text", title: "Case No", align: "left" },
            { data: "urn", type: "text", title: "URN", align: "left" },
            {
                title: '<div class="prehed-bold"> Member Details </div><div class="prehed-small"> ID | Name | Is Pre-Auth </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    var memberDetails = '<b class="text-success">' + row.memberId + '</b>' + '<div class="pre-bold">' + row.memberName + '</div>'
                        + '<div class="badge badge-success badge-font mt-1">' + row.isPreAuth + '</div>';
                    return memberDetails;
                }
            },
            { data: "memberName", type: "text", title: "Member Name", visible: false },
            { data: "memberId", type: "text", title: "Member Id", visible: false },
            {
                title: '<div class="prehed-bold"> Package Details </div><div class="prehed-small"> Code | Name </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    return '<b class="text-success">' + row.procedureCode + '</b> <div class="pre-bold">' + row.packageName + '</div>';
                }
            },
            { data: "packageName", type: "text", title: "Package Name", visible: false },
            { data: "procedureCode", type: "text", title: "Package Code", visible: false },
            { data: "isPreAuth", type: "text", title: "Is PreAuth", visible: false },
            {
                title: '<div class="prehed-bold"> Package Date </div> <div class="prehed-small" > Block | Unblock </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    return '<div><span class="pre-bold">' + row.blockDateTime.slice(0, 9) + '</span></div><div><span class="pre-small">'
                        + row.blockDateTime.slice(-11) + '</span></div> <div><span class="pre-bold">' + row.unblockDateTime.slice(0, 9)
                        + '</span></div><div><span class="pre-small">' + row.unblockDateTime.slice(-11) + '</span></div>';
                }
            },
            { data: "blockDateTime", type: "text", title: "Package Block Date Time", visible: false },
            { data: "unblockDateTime", type: "text", title: "Package Unblock Date Time", visible: false },
            {
                title: '<div class="prehed-bold"> Total Amount </div> <div class="prehed-small" > Package | Unblocked </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    return '<i class="fa fa-rupee"></i> ' + row.totalPackageCost + '</div> <div><i class="fa fa-rupee"></i> ' + row.unblockAmount + '</div >';
                }
            },
            { data: "totalPackageCost", type: "text", title: "Total Package Cost", align: "left", visible: false },
            { data: "unblockAmount", type: "text", title: "Unblocked Amount", align: "left", visible: false },
            {
                title: '<div class="prehed-bold"> Verification Mode </div><div class="prehed-small"> Invoice No </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    return '<div class="badge badge-success badge-font">' + row.verificationMode + '</div><div class="pre-small">'
                        + row.unblockingInvoiceNumber + '</div>';
                }
            },
            { data: "unblockingInvoiceNumber", type: "text", title: "Unblock Invoice No.", visible: false },
            { data: "verificationMode", type: "text", title: "Unblock Verification Mode", visible: false }
        ]
    });
    $('.buttons-excel').hide(); // Hide default & download from custom button - btnExcel
    $('#btnExcel2').off().click(() => {
        $('#grdPackageUnblock').DataTable().buttons(0, 0).trigger()
    });
}

function getPackageDischargeReport() {
    var param = {
        "Action": "DR", "HospitalCode": hospitalcode, "FromDate": $('#txtFromDate3').val(), "ToDate": $('#txtToDate3').val(),
        "IsPreAuth": $('#ddIsPreAuth3').val(), "DateType": $('#ddDateType3').val(), "AuthType": $('#ddAuthType3').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/HospitalPackageDischargeReport",
        type: "POST",
        data: param,
        contentType: 'application/json',
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (response) {
            bindgrdPackageDischarge(response);
        },
        error: function (error) {
            console.log(error.responseText);
        },
        complete: function () {
            $("#loader").hide();
        }
    });
}

function bindgrdPackageDischarge(rData) {
    if (rData.length > 0) {
        $("#btnExcel3").show();
    }
    else {
        $("#btnExcel3").hide();
    }
    $("#grdPackageDischarge").DataTable({
        dom: 'Blfrtip',
        buttons: [
            {
                extend: 'excel',
                messageTop: 'Period: ' + $("#txtFromDate3").val() + " To " + $("#txtToDate3").val(),
                title: 'HospitalPackageDischarge-Report',
                exportOptions: { columns: 'th:not(.hiddenExport)' }
            },
        ],
        bLengthChange: true,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        bDestroy: true,
        bFilter: true,
        bSort: true,
        bPaginate: true,
        data: rData,
        columns: [
            { type: "text", title: "SL No", align: "center", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
            { data: "caseNo", type: "text", title: "Case No", align: "left" },
            { data: "urn", type: "text", title: "URN", align: "left" },
            {
                title: '<div class="prehed-bold"> Member Details </div><div class="prehed-small"> ID | Name | Is Pre-Auth </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    var memberDetails = '<b class="text-success">' + row.memberId + '</b>' + '<div class="pre-bold">' + row.memberName + '</div>'
                        + '<div class="badge badge-success badge-font mt-1">' + row.isPreAuth + '</div>';
                    return memberDetails;
                }
            },
            { data: "memberName", type: "text", title: "Member Name", visible: false },
            { data: "memberId", type: "text", title: "Member Id", visible: false },
            {
                title: '<div class="prehed-bold"> Package Details </div><div class="prehed-small"> Code | Name </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    return '<b class="text-success">' + row.procedureCode + '</b> <div class="pre-bold">' + row.packageName + '</div>';
                }
            },
            { data: "packageName", type: "text", title: "Package Name", visible: false },
            { data: "procedureCode", type: "text", title: "Package Code", visible: false },
            { data: "isPreAuth", type: "text", title: "Is PreAuth", visible: false },
            {
                title: '<div class="prehed-bold">Date of Discharge</div> <div class="prehed-small"> Package Discharge </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    return '<div><span class="pre-bold">' + row.dischargeDate + '</span></div> <div><span class="pre-small">'
                        + row.packageDischargeDateTime.slice(0, 9) + '</span></div><div><span class="pre-small">'
                        + row.packageDischargeDateTime.slice(-11) + '</span></div >';
                }
            },
            { data: "dischargeDate", type: "text", title: "Actual Date of Discharge", visible: false },
            { data: "packageDischargeDateTime", type: "text", title: "Package Discharge Date Time", visible: false },
            {
                title: '<div class="prehed-bold"> Total Amount </div> <div class="prehed-small"> Blocked | Claimed | Insufficient </div>',
                align: "left",
                className: "hiddenExport",
                render: function (data, type, row, meta) {
                    return '<div> <i class="fa fa-rupee"></i> ' + row.amountBlocked + '</div> <div> <i class="fa fa-rupee"></i> ' + row.claimed_Amount + '</div>'
                        + '<div> <i class="fa fa-rupee"></i> ' + row.insufficientAmount + '</div>';
                }
            },
            { data: "amountBlocked", type: "text", title: "Blocked Amount", visible: false },
            { data: "claimed_Amount", type: "text", title: "Claimed Amount", visible: false },
            { data: "insufficientAmount", type: "text", title: "Insufficient Amount", visible: false },
            {
                title: '<div class="prehed-bold"> Verification Mode </div>',
                align: "left",
                render: function (data, type, row, meta) {
                    return '<div class="badge badge-danger badge-font">' + row.verificationMode + '</div>';
                }
            }
        ]
    });
    $('.buttons-excel').hide(); // Hide default & download from custom button - btnExcel
    $('#btnExcel3').off().click(() => {
        $('#grdPackageDischarge').DataTable().buttons(0, 0).trigger()
    });
}

function getDefaultReport(res) {
    switch (res) {
        case "tUnblock":
            if ($('#hdnTUnblock').val() == '0') {
                getPackageUnblockReport();
                $('#hdnTUnblock').val('1');
            }
            break;

        case "tDischarge":
            if ($('#hdnTDischarge').val() == '0') {
                getPackageDischargeReport();
                $('#hdnTDischarge').val('1');
            }
            break;
    }
}