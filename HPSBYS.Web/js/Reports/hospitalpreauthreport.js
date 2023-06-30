var hospitalcode = document.mynamespace.hospitalcode;
$(document).ready(function () {
    $("#btnExcel").hide();
    var d = new Date();
    var currentDate = d.getDate();
    var currMonth = d.getMonth();
    var currYear = d.getFullYear();
    var startDate = new Date(currYear, currMonth, 1);
    var endDate = new Date(currYear, currMonth, currentDate);
    $("#txtFromDate").val(moment(startDate).format("DD-MMM-YY"));
    $("#txtToDate").val(moment(endDate).format("DD-MMM-YY"));
    getPreAuthReport();
});

$(function () {
    $(".datepicker").datepicker({
        numberOfMonths: 1,
        changeMonth: true,
        dateFormat: "dd-M-y",
        changeYear: true
    });
});

function getPreAuthReport() {
    var param = {
        "Action": "A", "HospitalCode": hospitalcode, "FromDate": $('#txtFromDate').val(), "ToDate": $('#txtToDate').val(),
        "Urn": $('#txtUrnNo').val(), "Gender": $('#ddPatientGender').val(), "Status": $('#ddPreAuthStatus').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/HospitalPreAuthReport",
        type: "POST",
        data: param,
        contentType: 'application/json',
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (response) {
            if (response.length > 0) {
                $("#btnExcel").show();
            }
            else {
                $("#btnExcel").hide();
            }
            $("#grdPreAuthReportDetails").DataTable({
                dom: 'Blfrtip',
                buttons: [
                    {
                        extend: 'excel',
                        messageTop: 'Period: ' + $("#txtFromDate").val() + " To " + $("#txtToDate").val(),
                        title: 'HospitalPreAuth-Report',
                        exportOptions: { columns: 'th:not(.hiddenExport)' }
                    },
                ],
                bLengthChange: true,
                lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                bDestroy: true,
                bFilter: true,
                bSort: true,
                bPaginate: true,
                data: response,
                columns: [
                    { type: "text", title: "SL No", align: "center", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
                    {
                        data: "urn", type: "text", title: "URN", align: "left", render: function (data, type, row, meta) {
                            return ('<div class="pre-bold">' + row.urn + '</div>');
                        }
                    },
                    {
                        type: "text", title: "Member Details <div class='prehed-small'> Contact | Gender </div> ", align: "left", className: "hiddenExport", render: function (data, type, row, meta) {
                            return ('<b class="text-success">' + row.memberId + '</b> <div class="pre-bold">' + row.memberName + '</div> <div class="prehed-small">'
                                + row.patientContactNumber + ' | ' + row.gender + '</div>');
                        }
                    },
                    { data: "memberName", type: "text", title: "Member Name", visible: false },
                    { data: "memberId", type: "text", title: "Member Id", visible: false },
                    { data: "gender", type: "text", title: "Gender", visible: false },
                    { data: "patientContactNumber", type: "text", title: "Contact No.", visible: false },

                    { data: "package", type: "text", title: "Package Name <div class='prehed-small'>Package Code </div>", align: "left" },
                    {
                        type: "text", title: "PreAuth Amount <div class='prehed-small'> Date & Time </div>", align: "left", className: "hiddenExport", render: function (data, type, row, meta) {
                            return ('<div class="pre-bold">  &#8377; ' + row.requestAmount + '</div> <div class="prehed-small">' + row.appliedDate + '</div>');
                        }
                    },
                    { data: "requestAmount", type: "text", title: "PreAuth Req Amt", visible: false },
                    { data: "appliedDate", type: "text", title: "Pre Auth Applied Date & Time", visible: false },

                    {
                        type: "text", title: "SNA Approved Amount  <div class='prehed-small'> Date & Time </div>", align: "left", className: "hiddenExport", render: function (data, type, row, meta) {
                            return ('<div class="pre-bold">  &#8377; ' + row.approvedAmount + '</div> <div class="prehed-small">' + row.approvedDate + '</div>');
                        }
                    },
                    { data: "approvedDate", type: "text", title: "SNA Approved/Rejected Date & Time", visible: false },
                    { data: "approvedAmount", type: "text", title: "Approved Amount", visible: false },

                    { data: "actionTakenBy", type: "text", title: "Action Taken", align: "left" },
                    {
                        type: "text", title: "Status", align: "left", render: function (data, type, row, meta) {
                            switch (row.status) {
                                case "BLOCKED (SNA APPROVED)":
                                case "BLOCKED (AUTO-APPROVED)":
                                case "BLOCKED (APPROVED)":
                                case "SNA APPROVED":
                                case "AUTO-APPROVED":
                                    return '<span class="badge badge-success badge-font">' + row.status + '</span></div>';

                                case "UNBLOCKED (SNA APPROVED)":
                                    return '<span class="badge badge-success badge-font" style="background:#18b894; color: #fff;">' + row.status + '</span></div>';
                                case "UNBLOCKED (AUTO-APPROVED)":
                                    return '<span class="badge badge-success badge-font"  style="background:#1393d6; color: #fff;">' + row.status + '</span></div>';
                                case "UNBLOCKED (APPROVED)":
                                    return '<span class="badge badge-success badge-font" >' + row.status + '</span></div>';
                                case "PENDING (FRESH)": 
                                    return '<span class="badge badge-warning badge-font" style="color: #4e4e4e; background - color: #bcedff;">' + row.status + '</span></div>';
                                case "PENDING (QUERY)":
                                    return '<span class="badge badge-warning badge-font">' + row.status + '</span></div>';

                                case "PENDING (QUERY-COMPLIED)":
                                case "HOSPITAL CANCELLED":
                                    return '<span class="badge badge-danger badge-font" style="background:#ff6c3e; color: #fff;">' + row.status + '</span></div>';

                                case "SNA REJECTED":
                                case "EXPIRED":
                                case "AUTO-REJECTED":
                                    return '<span class="badge badge-danger badge-font">' + row.status + '</span></div>';
                            }
                        }
                    },
                    { data: "statusDate", type: "text", title: "Action Date & Time", visible: false }
                ]
            });
            $('.buttons-excel').hide(); // Hide default & download from custom button - btnExcel
            $('#btnExcel').off().click(() => {
                $('#grdPreAuthReportDetails').DataTable().buttons(0, 0).trigger()
            });
        },
        error: function (error) {
            console.log(error.responseText);
        },
        complete: function () {
            $("#loader").hide();
        }
    });
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

// breadcrumb active & menu active
$(document).ready(function () {
    loadNavigation('HospitalPreAuthReport', 'MtReports', 'pl', 'IPReports', 'PreAuth Report', '');
});