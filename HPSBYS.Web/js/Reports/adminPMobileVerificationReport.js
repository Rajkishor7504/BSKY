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
    getPMobileVerificationReport();
});

function getPMobileVerificationReport() {
    var param = {
        "Action": "A", "FromDate": $('#txtFromDate').val(), "ToDate": $('#txtToDate').val(),
        "StateCode": $('#ddState').val(), "DistrictCode": $('#ddDistrict').val(), "HospitalCode": $('#ddHospital').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/PatientMobileVerificationReport",
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
            $("#grdPMobileVerificationReport").DataTable({
                dom: 'Blfrtip',
                buttons: [
                    {
                        extend: 'excel',
                        messageTop: 'Period: ' + $("#txtFromDate").val() + " To " + $("#txtToDate").val(),
                        title: 'PatientMobileVerification-Report',
                        exportOptions: { columns: ':not(.hiddenExport)' }
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
                        title: '<div class="prehed-bold"> Member Details </div><div class="prehed-small"> ID | Name </div>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            return '<b class="text-success">' + row.memberId + '</b><div class="pre-bold">' + row.memberName + '</div>';
                        }
                    },
                    { data: "memberName", type: "text", title: "Member Name", visible: false },
                    { data: "memberId", type: "text", title: "Member Id", visible: false },
                    { data: "patientContactNo", type: "text", title: "Patient Phone No.", align: "left" },
                    { data: "admissionDate", type: "text", title: "Admission Date", align: "left" },
                    { data: "treatmentType", type: "text", title: "Treatment Type", align: "left" },
                    {
                        title: '<div class="prehed-bold"> Hospital Details </div><div class="prehed-small"> Name | District | State </div>',
                        align: "left",
                        className: "hiddenExport",
                        render: function (data, type, row, meta) {
                            return '<span><b class="text-success">' + row.hospitalName + '</b><div class="pre-bold">' + row.districtName + '</div><div class="pre-small">'
                                + row.stateName + '</div></span>';
                        }
                    },
                    { data: "hospitalName", type: "text", title: "Hospital Name", visible: false },
                    { data: "districtName", type: "text", title: "District", visible: false },
                    { data: "stateName", type: "text", title: "State", visible: false },
                    { data: "status", type: "text", title: "Status", align: "left" }
                ]
            });
            $('.buttons-excel').hide(); // Hide default & download from custom button - btnExcel
            $('#btnExcel').off().click(() => {
                $('#grdPMobileVerificationReport').DataTable().buttons(0, 0).trigger()
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
/*
$('#ddState').change(function () {
    var stateCode = $(this).val();
    $('#ddDistrict').empty();
    $('#ddDistrict').html('<option value="0">-Select-</option>');
    $('#ddHospital').empty();
    $('#ddHospital').html('<option value="0">-Select-</option>');
    getDistricts(stateCode);
});

$('#ddDistrict').change(function () {
    var stateCode = $('#ddState').val();
    var districtCode = $(this).val();
    $('#ddHospital').empty();
    $('#ddHospital').html('<option value="0">-Select-</option>');
    getHospitals(stateCode, districtCode);
});
*/
// breadcrumb active & menu active
$(document).ready(function () {
    loadNavigation('PatientMobileVerificationReport', 'MtReports', 'pl', 'IPReports', 'Mobile Verification Report', '');
});
