
var hospitalcode = document.mynamespace.hospitalcode;

$(document).ready(function () {
    $('#path').html('Report');
    $('#example2').DataTable({
        autoWidth: false,
    });
    getStates();
    var d = new Date();
    var currentDate = d.getDate();
    var currMonth = d.getMonth();
    var currYear = d.getFullYear();
    var startDate = new Date(currYear, currMonth, currentDate);
    var endDate = new Date(currYear, currMonth, currentDate);
    $("#fromdate").val(moment(startDate).format("DD-MMM-YY"));
    $("#todate").val(moment(endDate).format("DD-MMM-YY"));
    var frmdate = $("#fromdate").val();
    var to_date = $("#todate").val();
    getData(frmdate, to_date);
});

$("#btnSearch").click(function () {
    var fromDt = $("#fromdate").val();
    var toDt = $("#todate").val();
    var from = "";
    var to = "";
    if (fromDt != "")
        from = new Date($("#fromdate").val());
    if (toDt != "")
        to = new Date($("#todate").val());
    if (to != "" && from === "") {
        sweetAlert('Alert', 'Please Select From Date').then(() => {
            $('#fromdate').focus();
        });
    }
    else if (from != "" && to === "") {
        sweetAlert('Alert', 'Please Select To Date').then(() => {
            $('#todate').focus();
        });
    }
    else if (from > to) {
        sweetAlert('Alert', 'From date should not be greater than To date')
            .then(() => {
                $('#fromdate').focus();
            });
    }
    else {
        getData(fromDt, toDt);
    }
});

function getData(frmdate, to_date) {
    var groupid = document.mynamespace.groupid;
    var param = {
        statecode: $('#ddState').val(),
        districtcode: $('#ddDistrict').val(),
        HospitalCode: $('#ddHospital').val(),
        groupid: groupid,
        fromdate: frmdate,
        todate: to_date,
        Gender: $('#ddPatientGender').val(),
        AdmissionDateType: $('#ddAdmissionDateType').val()
    };
    $.ajax({
        url: ServiceURL + "/api/BlockPackage/GetHospitalDetailsReport",
        type: "POST",
        data: param,
        dataType: "json",
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (resultBal) {
            drawTable(resultBal)
        },
        error: function (error) {
            console.log(error.responseText);
        },
        complete: function () {
            $("#loader").hide();
        }
    });
};

function drawTable(resultBal) {
    var table = $('#example2').DataTable();
    table.destroy();
    $('#example2').dataTable({
        autoWidth: true,
        resposnive: true,
        // sDom: 'lrtip',//remove search textbox from datatable
        "aaData": resultBal,   //this is your JSON object, which is what is showing in your post above with console.log(data)
        "aoColumns": [
            {
                "render": function (resultBal, type, full, meta) {
                    return meta.row + 1;
                }
            },
            { "mDataProp": "hospitalname" },
            { "mDataProp": "hospitalcode" },
            {
                "mDataProp": "admission", "render": function (resultBal, type, full, meta) {
                    if (resultBal == "0") {
                        return "<button type = 'button' class='btn btn-outline-success focusedBtn btn-sm my-2 my-sm-0 mr-2'>" + resultBal + "</button>";
                    }
                    return "<button type = 'button' onclick='getAdmissionReport(this)' class='btn btn-outline-success focusedBtn btn-sm my-2 my-sm-0 mr-2' tooltip='View Admission Details'  data-toggle='modal' data-target='#popModal'>" + resultBal + "</button>";
                }
            },
            {
                "mDataProp": "block", "render": function (resultBal, type, full, meta) {
                    if (resultBal == "0") {
                        return "<button type = 'button' class='btn btn-outline-primary focusedBtn btn-sm my-2 my-sm-0 mr-2'>" + resultBal + "</button>";
                    }
                    return "<button type = 'button' onclick='getBlockingReport(this)' class='btn btn-outline-primary focusedBtn btn-sm my-2 my-sm-0 mr-2' tooltip='View Blocking Details'  data-toggle='modal' data-target='#popModal'>" + resultBal + "</button>";
                }
            },
            {
                "mDataProp": "unblock", "render": function (resultBal, type, full, meta) {
                    if (resultBal == "0") {
                        return "<button type = 'button' class='btn btn-outline-secondary focusedBtn btn-sm my-2 my-sm-0 mr-2'>" + resultBal + "</button>";
                    }
                    return "<button type = 'button' onclick='getUnblockingReport(this)' class='btn btn-outline-secondary focusedBtn btn-sm my-2 my-sm-0 mr-2' tooltip='View Unblocking Details'  data-toggle='modal' data-target='#popModal'>" + resultBal + "</button>";
                }
            },
            {
                "mDataProp": "discharge", "render": function (resultBal, type, full, meta) {
                    if (resultBal == "0") {
                        return "<button type = 'button' class='btn btn-outline-info btn-sm my-2 my-sm-0 mr-2'>" + resultBal + "</button>";
                    }
                    return "<button type = 'button' onclick='getDischargeReport(this)' class='btn btn-outline-info focusedBtn btn-sm my-2 my-sm-0 mr-2' tooltip='View Discharge Details'  data-toggle='modal' data-target='#popModal'>" + resultBal + "</button>";
                }
            },
            {
                "mDataProp": "pedingpreauth", "render": function (resultBal, type, full, meta) {
                    if (resultBal == "0") {
                        return "<button type = 'button' class='btn btn-outline-warning focusedBtn btn-sm my-2 my-sm-0 mr-2'>" + resultBal + "</button>";
                    }
                    return "<button type = 'button' onclick='getPreAuthReport(this)' class='btn btn-outline-warning focusedBtn btn-sm my-2 my-sm-0 mr-2' tooltip='View PreAuth Details'  data-toggle='modal' data-target='#popModal'>" + resultBal + "</button>";
                }
            },
        ]
    });
}

function getAdmissionReport(ele) {
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();
    var modalTitle = 'Admission Report <small><b class="text-success">(' + dataRow.hospitalname + ' | ' + dataRow.districtName + ' | ' + dataRow.stateName + ')</b></small>';
    $('#popModalTitle').html(modalTitle);
    $('#grdReportDetails').DataTable().clear().destroy();
    $('#grdReportDetails').empty();
    var param = {
        "Action": "A", "HospitalCode": dataRow.hospitalcode, Gender: $('#ddPatientGender').val(),
        "FromDate": $('#fromdate').val(), "ToDate": $('#todate').val(), AdmissionDateType: $('#ddAdmissionDateType').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/AdmissionReport",
        type: "POST",
        data: param,
        contentType: 'application/json',
        success: function (response) {
            $("#grdReportDetails").DataTable({
                dom: 'Blfrtip',
                buttons: [
                    {
                        extend: 'excel',
                        messageTop: dataRow.hospitalname + ' | ' + dataRow.districtName + ' | ' + dataRow.stateName + ' | Period: ' + $("#fromdate").val() + " To " + $("#todate").val(),
                        title: 'Admission-Report',
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
                    { data: "urn", type: "text", title: "URN", align: "left" },
                    {
                        data: null,
                        title: '<div class="prehed-bold"> Member Details </div><div class="prehed- small"> ID | Name </div>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var memberColumn = '<b class="text-success">' + row.memberId + '</b>';
                            memberColumn += '<div class="pre-bold">' + row.memberName + '</div>';
                            return memberColumn;
                        }
                    },
                    { data: "memberName", type: "text", title: "Member Name", align: "left", visible: false },
                    { data: "memberId", type: "text", title: "Member Id", align: "left", visible: false },
                    { data: "gender", type: "text", title: "Gender", align: "left" },
                    { data: "admissionDate", type: "text", title: "Admission Date", align: "left" },
                    { data: "caseNo", type: "text", title: "Case No.", align: "left" }
                ]
            });
            $('.buttons-excel').hide(); // Hide default & download from custom button - btnExcel
            $('#btnExcel').off().click(() => {
                $('#grdReportDetails').DataTable().buttons(0, 0).trigger()
            });
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
}

function getBlockingReport(ele) {
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();
    var modalTitle = 'Blocking Report <small><b class="text-success">(' + dataRow.hospitalname + ' | ' + dataRow.districtName + ' | ' + dataRow.stateName + ')</b></small>';
    $('#popModalTitle').html(modalTitle);
    $('#grdReportDetails').DataTable().clear().destroy();
    $('#grdReportDetails').empty();
    var param = {
        "Action": "B", "HospitalCode": dataRow.hospitalcode, Gender: $('#ddPatientGender').val(),
        "FromDate": $('#fromdate').val(), "ToDate": $('#todate').val(), AdmissionDateType: $('#ddAdmissionDateType').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/BlockedReport",
        type: "POST",
        data: param,
        contentType: 'application/json',
        success: function (response) {
            console.log('res', response);
            $("#grdReportDetails").DataTable({
                dom: 'Blfrtip',
                buttons: [
                    {
                        extend: 'excel',
                        messageTop: dataRow.hospitalname + ' | ' + dataRow.districtName + ' | ' + dataRow.stateName + ' | Period: ' + $("#fromdate").val() + " To " + $("#todate").val(),
                        title: 'Blocking-Report',
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
                    { data: "urn", type: "text", title: "URN", align: "left" },
                    {
                        data: null,
                        title: '<div class="prehed-bold"> Member Details </div><div class="prehed- small"> Name | Gender </div>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var memberColumn = '<b class="text-success">' + row.memberId + '</b>';
                            memberColumn += '<div class="pre-bold">' + row.memberName + '</div><div class="prehed-small">' + row.gender + '</div>'
                            return memberColumn;
                        }
                    },
                    {
                        data: null,
                        title: '<div class="prehed-bold"> Package Details </div><div class="prehed- small"> Name | Code </div>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var memberColumn = ' <div class="pre-bold">' + row.packageHeaderName + '</div>';
                            memberColumn += '<div>' + row.procedureCode + '</div>'
                            return memberColumn;
                        }
                    },
                    {
                        data: "memberName", type: "text", title: "Member Name", align: "left",
                        render: function (data, type, row, meta) {
                            return data;
                        }
                    },
                    { data: "memberName", type: "text", title: "Member Name", align: "left", visible: false },
                    { data: "memberId", type: "text", title: "Member Id", align: "left", visible: false  },
                    { data: "gender", type: "text", title: "Gender", align: "left", visible: false  },
                    { data: "packageHeaderName", type: "text", title: "Package Header Name", align: "left", visible: false  },
                    { data: "procedureCode", type: "text", title: "Package Code", align: "left", visible: false  },
                    { data: "blockingUserDate", type: "text", title: "Blocking Date", align: "left" },
                    { data: "admissiondate", type: "text", title: "Admission Date", align: "left" },
                    { data: "caseno", type: "text", title: "Case No", align: "left" },
                    {
                        data: "amountblocked", type: "text", title: "AMOUNT", align: "right", className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var amountClass = row.amountblocked;
                            return '<div class="pre-bold"> &#8377; ' + row.amountblocked + '</div>';
                        }
                    },
                    { data: "amountblocked", type: "text", title: "Amount", align: "left", visible: false },
                    {
                        data: "preauthstatus", type: "text", title: "Pre-Auth Status", align: "left", render: function (data, type, row, meta) {
                            if (row.preauthstatus == "YES") {
                                var html = '<div class="badge badge-success mb - 3 p - 2">' + row.preauthstatus + '</div>'
                            }
                            if (row.preauthstatus == "NO") {
                                var html = '<div class="badge badge-danger mb - 3 p - 2">' + row.preauthstatus + '</div>'
                            }
                            return html;
                        }
                    }
                ]
            });
            $('.buttons-excel').hide();
            $('#btnExcel').off().click(() => {
                $('#grdReportDetails').DataTable().buttons(0, 0).trigger()
            });
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
}

function getDischargeReport(ele) {
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();
    var modalTitle = 'Discharge Report <small><b class="text-success">(' + dataRow.hospitalname + ' | ' + dataRow.districtName + ' | ' + dataRow.stateName + ')</b></small>';
    $('#popModalTitle').html(modalTitle);
    $('#grdReportDetails').DataTable().clear().destroy();
    $('#grdReportDetails').empty();
    var param = {
        "Action": "C", "HospitalCode": dataRow.hospitalcode, Gender: $('#ddPatientGender').val(),
        "FromDate": $('#fromdate').val(), "ToDate": $('#todate').val(), AdmissionDateType: $('#ddAdmissionDateType').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/DischargeReport",
        type: "POST",
        data: param,
        contentType: 'application/json',
        success: function (response) {

            $("#grdReportDetails").DataTable({
                dom: 'Blfrtip',
                buttons: [
                    {
                        extend: 'excel',
                        messageTop: dataRow.hospitalname + ' | ' + dataRow.districtName + ' | ' + dataRow.stateName + ' | Period: ' + $("#fromdate").val() + " To " + $("#todate").val(),
                        title: 'Discharge-Report',
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
                    { data: "urn", type: "text", title: "URN", align: "left" },
                    {
                        data: null,
                        title: '<div class="prehed-bold"> Member Details </div><div class="prehed- small"> Name | Gender </div>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var memberColumn = '<b class="text-success">' + row.memberId + '</b>';
                            memberColumn += '<div class="pre-bold">' + row.memberName + '</div><div class="prehed-small">' + row.gender + '</div>'
                            return memberColumn;
                        }
                    },
                    { data: "memberName", type: "text", title: "Member Name", visible: false },
                    { data: "memberId", type: "text", title: "Member Id", visible: false },
                    { data: "gender", type: "text", title: "Gender", visible: false },
                    { data: "dateOfDischarge", type: "text", title: "Discharge Date", align: "left" },
                    { data: "admissiondate", type: "text", title: "Admission Date", align: "left" },
                    { data: "caseNo", type: "text", title: "Case No.", align: "left" }

                ]
            });
            $('.buttons-excel').hide();
            $('#btnExcel').off().click(() => {
                $('#grdReportDetails').DataTable().buttons(0, 0).trigger()
            });
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
}

function getPreAuthReport(ele) {
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();
    var modalTitle = 'Pre-Auth Pending Report <small><b class="text-success">(' + dataRow.hospitalname + ' | ' + dataRow.districtName + ' | ' + dataRow.stateName + ')</b></small>';
    $('#popModalTitle').html(modalTitle);
    $('#grdReportDetails').DataTable().clear().destroy();
    $('#grdReportDetails').empty();
    var param = {
        "Action": "D", "HospitalCode": dataRow.hospitalcode, Gender: $('#ddPatientGender').val(),
        "FromDate": $('#fromdate').val(), "ToDate": $('#todate').val(), AdmissionDateType: $('#ddAdmissionDateType').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/PreAuthReport",
        type: "POST",
        data: param,
        contentType: 'application/json',
        success: function (response) {
            console.log('res', response);
            $("#grdReportDetails").DataTable({
                dom: 'Blfrtip',
                buttons: [
                    {
                        extend: 'excel',
                        messageTop: dataRow.hospitalname + ' | ' + dataRow.districtName + ' | ' + dataRow.stateName + ' | Period: ' + $("#fromdate").val() + " To " + $("#todate").val(),
                        title: 'PreAuth-Report',
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
                    { data: "urn", type: "text", title: "URN", align: "left" },
                    {
                        data: null,
                        title: '<div class="prehed-bold"> Member Details </div><div class="prehed- small"> Name | Gender </div>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var memberColumn = '<b class="text-success">' + row.memberId + '</b>';
                            memberColumn += '<div class="pre-bold">' + row.memberName + '</div><div class="prehed-small">' + row.gender + '</div>'
                            return memberColumn;
                        }
                    },
                    {
                        data: null,
                        title: '<div class="prehed-bold"> Package Details </div><div class="prehed- small"> Name | Code </div>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var memberColumn = ' <div class="pre-bold">' + row.packageHeaderName + '</div>';
                            memberColumn += '<div>' + row.procedureCode + '</div>'
                            return memberColumn;
                        }
                    },
                    { data: "memberName", type: "text", title: "Member Name", visible: false },
                    { data: "memberId", type: "text", title: "Member Id", visible: false },
                    { data: "gender", type: "text", title: "Gender", visible: false },
                    { data: "packageHeaderName", type: "text", title: "Package Header Name", visible: false },
                    { data: "procedureCode", type: "text", title: "Package Code", visible: false },
                    { data: "blockingUserDate", type: "text", title: "Request Date", align: "left" },
                    { data: "admissiondate", type: "text", title: "Admission Date", align: "left" },
                    { data: "caseno", type: "text", title: "Case No", align: "left" },
                    {
                        data: "amountblocked", type: "text", title: "AMOUNT", align: "right", className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var amountClass = row.amountblocked;
                            return '<div class="pre-bold"> &#8377; ' + row.amountblocked + '</div>';
                        }
                    },
                    { data: "amountblocked", type: "text", title: "Amount", visible: false }

                ]
            });
            $('.buttons-excel').hide();
            $('#btnExcel').off().click(() => {
                $('#grdReportDetails').DataTable().buttons(0, 0).trigger()
            });
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
}

function getUnblockingReport(ele) {
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();
    var modalTitle = 'Unblocking Report <small><b class="text-success">(' + dataRow.hospitalname + ' | ' + dataRow.districtName + ' | ' + dataRow.stateName + ')</b></small>';
    $('#popModalTitle').html(modalTitle);
    $('#grdReportDetails').DataTable().clear().destroy();
    $('#grdReportDetails').empty();
    var param = {
        "Action": "E", "HospitalCode": dataRow.hospitalcode, Gender: $('#ddPatientGender').val(),
        "FromDate": $('#fromdate').val(), "ToDate": $('#todate').val(), AdmissionDateType: $('#ddAdmissionDateType').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/UnblockedReport",
        type: "POST",
        data: param,
        contentType: 'application/json',
        success: function (response) {
            console.log('res', response);
            $("#grdReportDetails").DataTable({
                dom: 'Blfrtip',
                buttons: [
                    {
                        extend: 'excel',
                        messageTop: dataRow.hospitalname + ' | ' + dataRow.districtName + ' | ' + dataRow.stateName + ' | Period: ' + $("#fromdate").val() + " To " + $("#todate").val(),
                        title: 'Unblocking-Report',
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
                    { data: "urn", type: "text", title: "URN", align: "left" },
                    {
                        data: null,
                        title: '<div class="prehed-bold"> Member Details </div><div class="prehed- small"> Name | Gender </div>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var memberColumn = '<b class="text-success">' + row.memberId + '</b>';
                            memberColumn += '<div class="pre-bold">' + row.memberName + '</div><div class="prehed-small">' + row.gender + '</div>'
                            return memberColumn;
                        }
                    },
                    {
                        data: null,
                        title: '<div class="prehed-bold"> Package Details </div><div class="prehed- small"> Name | Code </div>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var memberColumn = ' <div class="pre-bold">' + row.packageHeaderName + '</div>';
                            memberColumn += '<div>' + row.procedureCode + '</div>'
                            return memberColumn;
                        }
                    },
                    { data: "memberName", type: "text", title: "Member Name", visible: false },
                    { data: "memberId", type: "text", title: "Member Id", visible: false },
                    { data: "gender", type: "text", title: "Gender", visible: false },
                    { data: "packageHeaderName", type: "text", title: "Package Header Name", visible: false },
                    { data: "procedureCode", type: "text", title: "Package Code", visible: false },
                    { data: "blockingUserDate", type: "text", title: "Unblocking Date", align: "left" },
                    { data: "admissiondate", type: "text", title: "Admission Date", align: "left" },
                    { data: "caseno", type: "text", title: "Case No", align: "left" },
                    {
                        data: "amountblocked", type: "text", title: "AMOUNT", align: "right", className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            var amountClass = row.amountblocked;
                            return '<div class="pre-bold"> &#8377; ' + row.amountblocked + '</div>';
                        }
                    },
                    { data: "amountblocked", type: "text", title: "Amount", visible: false },
                    {
                        data: "preauthstatus", type: "text", title: "Pre-Auth Status", align: "left", render: function (data, type, row, meta) {
                            if (row.preauthstatus == "YES") {
                                var html = '<div class="badge badge-success mb - 3 p - 2">' + row.preauthstatus + '</div>'
                            }
                            if (row.preauthstatus == "NO") {
                                var html = '<div class="badge badge-danger mb - 3 p -2">' + row.preauthstatus + '</div>'
                            }
                            return html;
                        }
                    }
                ]
            });
            $('.buttons-excel').hide();
            $('#btnExcel').off().click(() => {
                $('#grdReportDetails').DataTable().buttons(0, 0).trigger()
            })
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
}

// breadcrumb active & menu active
$(document).ready(function () {
    loadNavigation('HospitalDetailsReport', 'MtReports', 'pl', 'IPReports', 'Hospitals Report', '');
});
