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
    });

    $(function () {
        $(".datepicker").datepicker({
            numberOfMonths: 1,
            changeMonth: true,
            dateFormat: "dd-M-y",
            changeYear: true
        });
    });

    function getMortalityReport() {
        var param = {
            "Action": "A", "HospitalCode": hospitalcode, "FromDate": $('#txtFromDate').val(), "ToDate": $('#txtToDate').val()
        };
        param = JSON.stringify(param);
        $.ajax({
            url: ServiceURL + "/api/Report/HospitalMortalityReport",
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
                $("#grdMortalityReportDetails").DataTable({
                    dom: 'Blfrtip',
                    buttons: [
                        {
                            extend: 'excel',
                            messageTop: 'Period: ' + $("#txtFromDate").val() + " To " + $("#txtToDate").val(),
                            /*exportOptions: {columns:'th:not(.hiddenExport)'},*/
                            title: 'HospitalMortality-Report',
                            exportOptions: {
                                columns: ':not(:last-child):not(.hiddenExport)'
                                }
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
                            type: "text",
                            title: '<span style="width:20% !important;"><div class="prehed-bold"> Member Details</div> <div class="prehed-small"> Name | Gender </div></span>', align: "left",
                            className: "hiddenExport",
                            render: function (data, type, row, meta) {
                                return '<span><b class="text-success">' + row.memberId + '</b><div class="pre-bold">' + row.memberName + '</div><div class="pre-small">' + row.patientGender + ' </div></span>';
                                console.log('data', data);
                            }
                        },
                        { data: "memberName", type: "text", title: "Member Name", visible: false },
                        { data: "memberId", type: "text", title: "Member ID", visible: false },
                        { data: "patientGender", type: "text", title: "Member Gender", visible: false },
                        { data: "aadharNumber", type: "text", title: "Aadhar No.", align: "left" },
                        { data: "patientContactNumber", type: "text", title: "Contact No.", align: "left" },
                        { data: "admissionDate", type: "text", title: "Admission Date", align: "left" },
                        { data: "dateOfDischarge", type: "text", title: "Mortality Date", align: "left" },
                        {
                            data: "transactionId", type: "text", title: "Action", align: "left", "render": function (result, type, full, meta) {
                                return "<button type='button' onclick='getMortalityDetails(" + result + ");' class='btn btn-outline-success focusedBtn btn-sm my-2 my-sm-0' tooltip='View Details'  data-toggle='modal' data-target='#popModal'><i class='fa fa-eye'></i></button>";
                            }
                        }
                    ]
                });
                $('.buttons-excel').hide(); // Hide default & download from custom button - btnExcel
                $('#btnExcel').off().click(() => {
                    $('#grdMortalityReportDetails').DataTable().buttons(0, 0).trigger()
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

function getMortalityDetails(mData) {
    var hospitalcode = document.mynamespace.hospitalcode;
        var param = {
            "Action": "B", "HospitalCode": hospitalcode, "TransactionId": mData
        };
        param = JSON.stringify(param);
        $.ajax({
            url: ServiceURL + "/api/Report/HospitalMortalityReport",
            type: "POST",
            data: param,
            contentType: 'application/json',
            beforeSend: function () {
                $("#loader").show();
            },
            success: function (response) {
                //console.log(response[0]);
                if (response[0].packageDetails.length > 0) {
                    var htmlString = "";
                    $("#divMortalityPackage").show();
                    $('#tblMortalityPackage > tbody').empty();
                    for (var i = 0; i < response[0].packageDetails.length; i++) {
                        var statusClass = response[0].packageDetails[i].preAuthStatus === 'YES' ? "<div class='badge badge-success badge-font mb-1'>" : "<div class='badge badge-danger badge-font mb-1'>";
                        var statusText = response[0].packageDetails[i].preAuthStatus === 'YES' ? 'YES' : 'NO';
                        htmlString += "<tr><td>" + (i + 1) + "</td><td>" + response[0].packageDetails[i].packageHeaderName + "</td><td><div class='text-success'>" +
                            response[0].packageDetails[i].procedureCode + "</div><div class='pre-bold'>" + response[0].packageDetails[i].procedureName + "</div></td><td>" + response[0].packageDetails[i].blockDate + "</td><td>" + response[0].packageDetails[i].days + "</td><td>" +
                            response[0].packageDetails[i].amountBlocked + "</td><td>" + response[0].packageDetails[i].totalPackageCost + "</td><td>" + statusClass + " " + statusText + "</td>";
                    }
                    $('#tblMortalityPackage').append(htmlString);
                }
                else {
                    $("#divMortalityPackage").hide();
                }

                if (response[0].implantDetails.length > 0) {
                    var htmlString = "";
                    $("#divMortalityImplant").show();
                    $('#tblMortalityImplant > tbody').empty();
                    for (var i = 0; i < response[0].implantDetails.length; i++) {
                        htmlString += "<tr><td>" + (i + 1) + "</td><td>" + response[0].implantDetails[i].implantname +
                            "</td><td>" + response[0].implantDetails[i].unit + "</td><td>" + response[0].implantDetails[i].unitcycleprice +
                            "</td><td>" + response[0].implantDetails[i].amount + "</td><td>" + response[0].implantDetails[i].totalamount + "</td></tr>";
                    }
                    $('#tblMortalityImplant').append(htmlString);
                }
                else {
                    $("#divMortalityImplant").hide();
                }

                if (response[0].hedDetails.length > 0) {
                    var htmlString = "";
                    $("#divMortalityHed").show();
                    $('#tblMortalityHed > tbody').empty();
                    for (var i = 0; i < response[0].hedDetails.length; i++) {
                        htmlString += "<tr><td>" + (i + 1) + "</td><td>" + response[0].hedDetails[i].hedname +
                            "</td><td>" + response[0].hedDetails[i].hedunit + "</td><td>" + response[0].hedDetails[i].hedpriceperunit +
                            "</td><td>" + response[0].hedDetails[i].hedprice + "</td><td>" + response[0].hedDetails[i].preauth + "</td><td>" +
                            response[0].hedDetails[i].totalhedprice + "</td></tr>";
                    }
                    $('#tblMortalityHed').append(htmlString);
                }
                else {
                    $("#divMortalityHed").hide();
                }
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
        loadNavigation('HospitalMortalityReport', 'MtReports', 'pl', 'IPReports', 'Mortality Report', '');
    });
