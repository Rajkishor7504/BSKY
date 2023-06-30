
var hospitalcode = document.mynamespace.hospitalcode;

$(document).ready(function () {
    $("#btnExcel").hide();
    var d = new Date();
    var currentDate = d.getDate();
    var currMonth = d.getMonth();
    var currYear = d.getFullYear();
    var startDate = new Date(currYear, currMonth, currentDate);
    var endDate = new Date(currYear, currMonth, currentDate);
    $("#txtFromDate").val(moment(startDate).format("DD-MMM-YY"));
    $("#txtToDate").val(moment(endDate).format("DD-MMM-YY"));

    getStates();
    getReferralReport();
});

function getReferralReport() {
    var groupid = document.mynamespace.groupid;
    var param = {
        "Action": "A", "Type": $("input[type='radio'][name='optRefer']:checked").val(),
        "HospitalCode": $('#ddHospital').val(),
        "statecode": $('#ddState').val(),
        "districtcode": $('#ddDistrict').val(),
        "groupid": groupid,
        "FromDate": $('#txtFromDate').val(), "ToDate": $('#txtToDate').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/HospitalReferralReport",
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
            $("#grdReferralReportDetails").DataTable({
                dom: 'Blfrtip',
                buttons: [
                    {
                        extend: 'excel',
                        messageTop: 'Period: ' + $("#txtFromDate").val() + " To " + $("#txtToDate").val(),
                        title: 'Referral-Report',
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
                    { data: "memberName", type: "text", title: "Member Name", visible: false },
                    { data: "memberId", type: "text", title: "Member Id", visible: false },
                    { data: "fromHospitalName", type: "text", title: "From Hospital", align: "left" },
                    { data: "toHospitalName", type: "text", title: "To Hospital", align: "left" },
                    {
                        data: "referralDate", type: "text", title: "Referral Date", align: "left", "render": function (result, type, full, meta) {
                            return result.split(' ')[0];
                        }
                    },
                    { data: "referralCode", type: "text", title: "Referral Code", align: "left" },
                    {
                        type: "text", title: "Status", align: "left", render: function (data, type, row, meta) {
                            switch (row.status) {
                                case "Used":
                                    return '<span class="badge badge-success badge-font">' + row.status + '</span>';
                                case "Not Used":
                                    return '<span class="badge badge-warning badge-font">' + row.status + '</span>';
                                case "Auto Invalid":
                                    return '<span class="badge badge-danger badge-font">' + row.status + '</span>';
                            }
                        },
                    }
                ]
            });
            $('.buttons-excel').hide(); // Hide default & download from custom button - btnExcel
            $('#btnExcel').off().click(() => {
                $('#grdReferralReportDetails').DataTable().buttons(0, 0).trigger()
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

// breadcrumb active & menu active
$(document).ready(function () {
    loadNavigation('HospitalReferralReport', 'MtReports', 'pl', 'IPReports', 'Referral Report', '');
});
