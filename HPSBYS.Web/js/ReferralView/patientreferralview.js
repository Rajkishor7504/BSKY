var hospitalcode = document.mynamespace.hospitalcode;
var userID = document.mynamespace.UserId;
    $(function () {
        $('.secondary-menu li a').tooltip();
    });

    $(document).ready(function () {
        $('#path').html('Block Override View');
        loadNavigation('PatientReferralView', 'MtReferal', 'pl', 'Discharge', 'Patient Referral View', '');
        $(".dateFormat").datepicker({
            numberOfMonths: 1,
            changeMonth: true,
            dateFormat: "dd-M-y",
            changeYear: true
        });
        var d = new Date();
        var currentDate = d.getDate();
        var currMonth = d.getMonth();
        var currYear = d.getFullYear();
        var startDate = new Date(currYear, currMonth, 1);
        var endDate = new Date(currYear, currMonth, currentDate);
        $("#txtFromDate").val(moment(startDate).format("DD-MMM-YY"));
        $("#txtToDate").val(moment(endDate).format("DD-MMM-YY"));
        var from_date = $("#txtFromDate").val();
        var to_date = $("#txtToDate").val();
        getSearchData(from_date, to_date);

        $('#DvPatientReferralView').show();
        //bindgrdPatientReferralView();
    });

    $("#btnSearch").click(function () {
        var fromDt = $("#txtFromDate").val();
        var toDt = $("#txtToDate").val();
        var from = "";
        var to = "";
        if (fromDt != "")
            from = new Date($("#txtFromDate").val());
        if (toDt != "")
            to = new Date($("#txtToDate").val());
        if (to != "" && from === "") {
            swal({
                text: "Please select From Date",
                confirmButtonColor: "#36A865",
                type: "error",
                confirmButtonText: "OK",
                closeOnConfirm: false
            }).then(function () {
                $("#txtFromDate").focus();
            });
        }
        else if (from != "" && to === "") {
            swal({
                text: "Please select To Date",
                confirmButtonColor: "#36A865",
                type: "error",
                confirmButtonText: "OK",
                closeOnConfirm: false
            }).then(function () {
                $("#txtToDate").focus();
            });
        }
        else if (from > to) {
            swal({
                text: "From date should not be greater than To date",
                confirmButtonColor: "#36A865",
                type: "error",
                confirmButtonText: "OK",
                closeOnConfirm: false
            }).then(function () {
                $("#txtFromDate").focus();
            });
        }
        else {
            getSearchData(fromDt, toDt);
        }
    });

    function getSearchData(fromdate, todate) {
        var param = {
            Action: 'A',
            UserId: userID,
            FromReferralDate: fromdate,
            ToReferralDate: todate,
            HospitalCode: hospitalcode
        };
        $.ajax({
            url: ServiceURL + "/api/Discharge/GetPatientReferralList",
            type: "POST",
            data: param,
            dataType: "json",
            beforeSend: function () {
                $("#loader").show();
            },
            success: function (response) {
                bindgrdPatientReferralView(response);
            },
            complete: function () {
                $("#loader").hide();
            },
            error: function (error) {
                console.log(error.responseText);
            }
        });
    };

    function bindgrdPatientReferralView(responseData) {
        //debugger;
        $("#grdPatientReferralView").DataTable({
            bLengthChange: true,
            lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
            bFilter: true,
            bSort: false,
            paging: true,
            bDestroy: true,
            data: responseData,
            columns: [
                { data: 'id', type: "text", title: '<span style="width:5% !important;">Sl No</span>', align: "center", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
                { data: "urn", type: "text", title: '<span style="width:15% !important;">URN</span>', align: "center" },
                //{ data: "patientName", type: "text", title: "Member Name", align: "center" },
                //{ data: "memberId", type: "text", title: "Member Id", align: "center" },
                {
                    type: "text", title: '<span style="width:20% !important;"><div class="prehed-bold"> Member Details</div><div class="prehed-small"> ID | Name </div></span>', align: "left", className: "hiddenExport", render: function (data, type, row, meta) {
                        return ('<b class="text-success">' + row.memberId + '</b><div class="pre-bold">' + row.patientName + '</div>');
                    }
                },
                { data: "fromHospitalName", type: "text", title: '<span style="width:10% !important;"> From Hospital </span>', align: "center" },
                { data: "toHospital", type: "text", title: '<span style="width:10% !important;"> To Hospital </span>', align: "center" },
                { data: "referralDate", type: "text", title: '<span style="width:10% !important;"> Referral date </span>', align: "center" },
                {
                    type: "text", title: '<div class="prehed-bold"> Referal Code </div><div class="prehed-small"> Status </div>', align: "left", className: "hiddenExport", render: function (data, type, row, meta) {
                        if (row.referralStatus == "Approved") {
                            return ('<div class="mb-1">' + row.referralCode +'</div><div class="badge badge-success badge-font mb-1"> '+row.referralStatus +' </div>');
                        }
                        if (row.referralStatus == "Pending") {
                            return ('<div class="badge badge-warning badge-font mb-1"> ' + row.referralStatus + ' </div>');
                        }
                    }
                },
                //{
                //    type: "text", title: "Referral Code", align: "center",
                //    render: function (data, type, row, meta) {
                //        if (row.referralStatus == "Approved") {
                //            return row.referralCode;
                //        }
                //        return '';
                //    }
                //},
                //{ data: "referralStatus", type: "text", title: "Status", align: "center" },
                {
                    data: null,
                    type: "text",
                    width: "200",
                    className: "text-center",
                    title: "Action",
                    render: function (data, type, row, meta) {
                        var actionContent = "<span style='float:center;'><button type = 'button' tooltip='View Details' onclick='ReferralViewDetails(this);' class='btn btn-outline-danger focusedBtn btn-sm my-2 mr-2 my-sm-0' ><i class='fa fa-eye'></i></button></span>";
                        return actionContent;
                    }
                }
            ]
        });
    }

function OldbindgrdPatientReferralView() {
    var userID = document.mynamespace.UserId;
        var params = { Action: 'A', UserId: userID };
        params = JSON.stringify(params);
        return $.ajax({
            url: ServiceURL + "/api/Discharge/GetPatientReferralList",
            type: "POST",
            data: params,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loader").show();
            },
            success: function (response) {
                $("#grdPatientReferralView").DataTable({
                    bLengthChange: true,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                    bFilter: false,
                    bSort: true,
                    paging: true,
                    data: response,
                    columns: [
                        { data: 'id', type: "text", title: "Sl#", align: "center", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
                        { data: "memberId", type: "text", title: "Id", align: "center" },
                        { data: "patientName", type: "text", title: "Name", align: "center" },
                        { data: "urn", type: "text", title: "URN", align: "center" },
                        {
                            type: "text", title: "Referral Code", align: "center",
                            render: function (data, type, row, meta) {
                                if (row.fromHospitalName == "abc4") {
                                    return row.referralCode;
                                }
                                return 'REFC' + meta.row + meta.settings._iDisplayStart + 1;
                            }
                        },
                        { data: "fromHospitalName", type: "text", title: "From Hospital", align: "center" },
                        { data: "toHospital", type: "text", title: "To Hospital", align: "center" },
                        {
                            type: "text", title: "Status", align: "center",
                            render: function (data, type, row, meta) {
                                var rowNumber = parseInt(meta.row + meta.settings._iDisplayStart + 1);
                                if (rowNumber % 2 == 0) {
                                    return 'Yes';
                                }
                                else {
                                    return 'No';
                                }
                            }
                        }
                    ]
                });
            },
            complete: function () {
                $("#loader").hide();
            },
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
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

    function ReferralViewDetails(ele) {
        var dataRow = $('#grdPatientReferralView').DataTable().row($(ele).closest('tr')).data();
        $.ajax({
            url: ServiceURL + "/api/Discharge/GetPatientReferralViewDetails?referralid=" + dataRow.referalid,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loader").show();
            },
            success: function (response) {
                console.log("ReferralViewDetails", response);
                $("#briefhistory").text(response.briefhistory);
                $("#diagnosis").text(response.diagnosis);
                $("#fromdeptname").text(response.fromdeptname);
                $("#fromdrname").text(response.fromdrname);
                $("#fromhospitalname").text(response.fromhospitalname);
                $("#fromreferraldate").text(response.fromreferraldate);
                $("#hospitalcode").text(response.hospitalcode);
                $("#reasonforrefer").text(response.reasonforrefer)
                $("#todistrict").text(response.todistrict);
                $("#tohospital").text(response.tohospital);
                $("#toreferraldate").text(response.toreferraldate);
                $("#toreferraldate").text(response.toreferraldate);
                $("#tostate").text(response.tostate);
                $("#treatmentadvised").text(response.treatmentadvised);
                $("#treatmentgiven").text(response.treatmentgiven);
                if (response.investigationdoc != null) {
                    console.log("investigationdocDatapath", response.investigationdoc);
                    $('#investigationdoc').prop("disabled", false);
                    $('#investigationdoc').addClass("btn btn-outline-success btn-sm");
                    $('#investigationdoc').removeClass("btn btn-outline-warning btn-sm user-select-none");
                    $('#investigationdoc').data('pathName', response.investigationdoc);
                    $('#investigationdoc').data('filename', response.investigationdocname);
                }
                else {
                    console.log("investigationdocDatapathelse", response.investigationdoc);
                    $('#investigationdoc').prop("disabled", true);
                    $('#investigationdoc').removeClass("btn btn-outline-success btn-sm");
                    $('#investigationdoc').addClass("btn btn-outline-warning btn-sm user-select-none");
                }
                if (response.referraldoc != null) {
                    console.log("referraldocDatapath", response.referraldoc);
                    $('#referraldoc').prop("disabled", false);
                    $('#referraldoc').addClass("btn btn-outline-success btn-sm");
                    $('#referraldoc').removeClass("btn btn-outline-warning btn-sm user-select-none");
                    $('#referraldoc').data('pathName', response.referraldoc);
                    $('#referraldoc').data('filename', response.referraldocname);
                }
                else {
                    console.log("referraldocDatapathelse", response.referraldoc);
                    $('#referraldoc').prop("disabled", true);
                    $('#referraldoc').removeClass("btn btn-outline-success btn-sm");
                    $('#referraldoc').addClass("btn btn-outline-warning btn-sm user-select-none");
                }
                $('#viewref').modal('show');
            },
            complete: function () {
                $("#loader").hide();
            },
            error: function (error) {
                console.log(error.responseText);
            }
        });
    }

    function DownloadDoc(ele) {
        var referraldoc = $(ele).data('pathName');
        var filename = $(ele).data('filename');
        window.location.href = "Downloadfile?pathName=" + referraldoc +"&filename=" + filename;
    }

