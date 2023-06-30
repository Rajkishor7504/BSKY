
$(document).ready(function () {
    $('#path').html('Report');
    $('#example2').DataTable({
        autoWidth: false,
    });
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

    getStates();
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
        HospitalCode: $('#ddHospital').val(),
        statecode: $('#ddState').val(),
        districtcode: $('#ddDistrict').val(),
        groupid: groupid,
        Action: "S",
        fromdate: frmdate,
        todate: to_date
    };
    $.ajax({
        url: ServiceURL + "/api/Report/GetNewOverRideDetails",
        type: "POST",
        data: param,
        dataType: "json",
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (resultBal) {
            console.log("GetNewOverRideDetails", resultBal);
            drawTable(resultBal);
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
            { "mDataProp": "purpose" },
            { "mDataProp": "otp" },
            { "mDataProp": "iris" },
            { "mDataProp": "pos" },
            { "mDataProp": "override" },
            {
                "mDataProp": "view", "render": function (resultBal, type, row) {
                    return '<span><button type = "button" tooltip = "View Details" data-toggle="Modal" onclick = "getnewoverrideview(this)" class="btn btn-outline-success focusedBtn btn-sm my-2 mr-2 my-sm-0"><i class="fa fa-eye"></i></button></span>'
                }
            }
        ]
    });
}

function getnewoverrideview(ele) {
    var PurposeId = "";
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();
    if (dataRow.purpose == "BLOCKING") {
        PurposeId = "0301";
        $('#popModalTitle').html('Blocking');
    }
    if (dataRow.purpose == "UNBLOCKING") {
        PurposeId = "0302";
        $('#popModalTitle').html('Unblocking');
    }
    if (dataRow.purpose == "DISCHARGE") {
        PurposeId = "0303";
        $('#popModalTitle').html('Discharge');
    }
    $('#grdReportDetails').DataTable().clear().destroy();
    $('#grdReportDetails').empty();
    var param = {
        "Action": "A", "HospitalCode": $("#ddHospital").val(),
        "stateCode": $("#ddState").val(),
        "districtCode": $("#ddDistrict").val(),
        "VARIFIEDTHROUGH": PurposeId, "from_date": $("#fromdate").val(), "to_date": $("#todate").val()
    };
    /* param = JSON.stringify(param);*/
    $.ajax({
        url: ServiceURL + "/api/Report/GetAdminAuthView",
        type: "GET",
        data: param,
        contentType: 'application/json',
        success: function (response) {
            //console.log("GetOverRideView", response);
            $("#grdReportDetails").DataTable({
                dom: 'Blfrtip',
                buttons: [
                    {
                        extend: 'excel',
                        messageTop: 'Period: ' + $("#fromdate").val() + " To " + $("#todate").val(),
                        title: 'Authentication' + $('#popModalTitle').html() + '-Report',
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
                    { type: "text", title: '<span style="width:5% !important;">Sl NO</span>', align: "center", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
                    { data: "urn", type: "text", title: '<span style="width:5% !important;">URN</span>', align: "left" },
                    {
                        data: null, type: "text", title: '<span style="width:15% !important;"><div class="prehed-bold">Member Details</div><div class="prehed-small"> ID | Name </div></span>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            return '<span><b class="text-success">' + row.memberid + '</b><div class="pre-bold">' + row.fullnameenglish + '</div></span>'
                            //console.log('data', data);
                        }
                    },
                    { data: "fullnameenglish", type: "text", title: "MEMBER NAME", visible: false },
                    { data: "memberid", type: "text", title: "MEMBER ID", visible: false },
                    { data: "createdon", type: "text", title: "DATE & TIME", align: "left" },
                    { data: "purpose", type: "text", title: "PURPOSE", align: "left" },
                    { data: "verificationMode", type: "text", title: "VERIFICATION TYPE", align: "left" },
                    {
                        data: null, type: "text", title: '<span style="width:20% !important;">Hospital Details</span>',
                        className: "hiddenExport",
                        align: "left",
                        render: function (data, type, row, meta) {
                            return '<span><b class="text-success">' + row.hospitalname + '</b><div class="pre-bold">' + row.districtname + '</div><div>'
                                + row.statename + '</div></span>'
                            //console.log('data', data);
                        }
                    },
                    { data: "hospitalname", type: "text", title: "Hospital Name", visible: false },
                    { data: "districtname", type: "text", title: "District Name", visible: false },
                    { data: "statename", type: "text", title: "State Name", visible: false },
                    { data: "approvestatus", type: "text", title: "STATUS", align: "left" }
                ]
            });
            $('.buttons-excel').hide(); // Hide default & download from custom button - btnExcel
            //$('#popModal').modal('show');
            $('#btnExcel').off().click(() => {
                $('#grdReportDetails').DataTable().buttons(0, 0).trigger()
            });
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
    $('#popModal').modal('show')
}

$(document).ready(function () {
    loadNavigation('NewOverRideDetails', 'MtReports', 'pl', 'IPReports', 'Authentication Details', '');
});