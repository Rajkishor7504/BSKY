$(document).ready(function () {

    $('#path').html('View Unblock Package List');
    loadNavigation('ViewUnblockPackageDetails', 'Mlunblockpackage', 'pl', 'Unblock Package', 'View Unblock Package Details', '');
    $(".datepicker").datepicker({
        numberOfMonths: 1,
        changeMonth: true,
        dateFormat: "dd-M-y",
        changeYear: true
    });
    $('#example2').DataTable({
        responsive: true
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

function getData(frmdate, to_date) {
    var groupid = document.mynamespace.groupid;
    var param = {
        statecode: $('#ddState').val(),
        districtcode: $('#ddDistrict').val(),
        HospitalCode: $('#ddHospital').val(),
        groupid: groupid,
        fromdate: frmdate,
        todate: to_date,
        searchtype: $("#ddlDateType").val()
    };
    $.ajax({
        url: ServiceURL + "/api/UnblockPackage/GetViewUnBlockpackagedetailsList",
        type: "POST",
        data: param,
        dataType: "json",
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (resultBal) {
            drawTable(resultBal);
        },
        complete: function () {
            $("#loader").hide();
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
};

function drawTable(data) {
    var table = $('#example2').DataTable();
    table.destroy();
    $('#example2').dataTable({
        "responsive": true,
        //sDom: 'lrtip',//remove search textbox from datatable
        "aaData": data,   //this is your JSON object, which is what is showing in your post above with console.log(data)
        "aoColumns": [
            {
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                }
            },
            { "mDataProp": "urn" },
            {
                mDataProp: null,
                render: function (data, type, row) {
                    return '<b class="text-success">' + row.memberid + '</b><div class="pre-bold" >' + row.membername + '</div>';
                }
            },
            //{ "mDataProp": "membername" },
            //{ "mDataProp": "memberid" },
            { "mDataProp": "uidnumber" },
            { "mDataProp": "caseno" },
            { "mDataProp": "admissiondate" },
            { "mDataProp": "unblockeddate" },
            {
                "render": function (data, type, row) {
                    var actionContent = "<span style='float:right;'>";
                    actionContent += "<button type = 'button' tooltip = 'Patient Slip' onclick='UnBlockpackageslip(this)' class='btn btn-outline-success focusedBtn btn-sm my-2 my-sm-0 mr-2' > <i class='fa fa-file-text-o'></i></button>" +
                        "<button type = 'button' tooltip = 'Hosiptal Slip' onclick='UnBlockHospitalSlip(this)' class='btn btn-outline-secondary focusedBtn btn-sm my-2 my-sm-0 mr-2' > <i class='fa fa-files-o'></i></button>"
                        + "<button type = 'button' tooltip = 'View Details' onclick='ShowUnBlockdetails(this)' class='btn btn-outline-danger focusedBtn btn-sm my-2 mr-2 my-sm-0' ><i class='fa fa-eye'></i></button></span>";
                    return actionContent;
                    return actionContent;
                }
            },
        ]
    });
}

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
        swal({
            text: "Please select From Date.",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#fromdate").focus();
        });
    }
    else if (from != "" && to === "") {
        swal({
            text: "Please select to Date.",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#todate").focus();
        });
    }
    else if (from > to) {
        swal({
            text: "From date should not be greater than To date.",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#fromdate").focus();
        });
    }
    else {
        getData(fromDt, toDt);
    }
});

function ShowUnBlockdetails(ele) {
    var hospitalcode = document.mynamespace.hospitalcode;
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();
    var param = {
        hospitalcode: hospitalcode,
        transactionid: dataRow.transactionid,
        txnpackagedetailid: dataRow.txnpackagedetailid
    };
    $.ajax({
        url: ServiceURL + "/api/UnblockPackage/GetUnBlockDetailsView",
        type: "POST",
        data: param,
        dataType: "json",
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (response) {
            $("#packageDetailsbody").empty();
            console.log("unblock viw", response);
            $('#pmembername').html(response.patientDetails.membername);
            $('#memberid').html(response.patientDetails.memberid);
            $('#patientgender').html(response.patientDetails.patientgender);
            $('#age').html(response.patientDetails.age);
            $('#uidnumber').html(response.patientDetails.uidnumber);
            $('#surgicaltype').html(response.patientDetails.surgicaltype);
            $('#idremarks').html(response.patientDetails.remarks);
            $('#admissiondate').html(response.patientDetails.admissiondate);
            $('#doctorname').html(response.patientDetails.doctorname);
            $('#doctorphno').html(response.patientDetails.doctorphno);
            $('#patientcontactnumber').html(response.patientDetails.patientcontactnumber);
            $('#overridecode').html(response.patientDetails.overridecode);
            $('#referalcode').html(response.patientDetails.referalcode);
            $('#description').html(response.patientDetails.description);
            $('#description').html(response.patientDetails.description);
            $('#verifiedmemberid').html(response.patientDetails.verifiedmemberid);
            $('#verifiedmembergender').html(response.patientDetails.verifiedmembergender);
            $('#verifiedmemberage').html(response.patientDetails.verifiedmemberage);
            $('#verifiedmemberaadhar').html(response.patientDetails.verifiedmemberaadhar);
            $('#urn').html(response.patientDetails.urn);
            $('#verificationmode').html(response.patientDetails.verificationmode);
            $('#verifiedmembername').html(response.patientDetails.verifiedmembername);
            var TotatCost = 0;
            for (var i = 0; i < response.packageDetails.length; i++) {
                if (i == 0) {
                    TotatCost = response.packageDetails[0].totalpackagecost;
                    $('#TotalPackageCost').html(TotatCost);
                }
                var slno = i + 1;
                var AddParameter = "<tr class='parent'>" +
                    "<td>" + slno + "</td>" +
                    "<td>" + response.packageDetails[i].packageheadername + "</td>" +
                    "<td>" + response.packageDetails[i].packagesubcategoryname + "</td>" +
                    "<td>" + response.packageDetails[i].procedurecode + "</td>" +
                    "<td>" + response.packageDetails[i].wardcost + "</td>" +
                    "<td>" + response.packageDetails[i].days + "</td>" +
                    "<td>" + response.packageDetails[i].preauthstatus + "</td>" +
                    "<td>" + response.packageDetails[i].blockinguserdate + "</td>" +
                    "<td><button type='button' class='btn btn-outline-success focusedBtn btn-sm my-2 my-sm-0 detailsPanal' data-toggle='collapse' data-target='#pkgmodalcls_" + slno + "'> View Details <i class='fa fa-eye'></i></button></td></tr>";

                AddParameter += "<tr class ='child'>";
                AddParameter += "<td colspan='12' class='m-0 p-0 border-0'>";
                AddParameter += "<div class='accordian-body collapse' id='pkgmodalcls_" + slno + "'>";
                AddParameter += "<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 mt-3'>";
                AddParameter += "<div class='headingpkg-bg'> Package Details </div>";
                AddParameter += "<div class='row d-flex align-items-center'>";
                AddParameter += "<div class='col-lg-6 col-md-12 col-sm-12 col-xs-12'>";
                AddParameter += "<div class='impl_head'> Implant Details </div>";
                AddParameter += "</div>";
                AddParameter += "<div class='col-lg-6 col-md-12 col-sm-12 col-xs-12'>";
                AddParameter += "<div class='row d-flex align-items-center'>";
                AddParameter += "<div class='col-lg-7 col-md-7 col-sm-12 col-xs-12'>";
                AddParameter += "<div class='impl_head'> HighEndDrugs Details </div>";
                AddParameter += "</div>";
                AddParameter += "<div class='col-lg-5 col-md-5 col-sm-12 col-xs-12'></div>";
                AddParameter += "</div>";
                AddParameter += "</div>";
                AddParameter += "</div>";
                AddParameter += "<div class='row'>";
                AddParameter += "<div class='col-lg-6' id='table-scroll'>";
                AddParameter += "<div class='col-lg-6 col-md-12 col-sm-12 col-xs-12 mt-3'></div>";
                AddParameter += "<table class='table table-striped tblImplView' id='tblImplantView1'>";
                AddParameter += "<thead>";
                AddParameter += "<tr>";
                AddParameter += "<th scope='bg-white text-align-left' style='background-color:#fff !important;'> Implant Name</th>";
                AddParameter += "<th scope='bg-white text-align-center' style='background-color:#fff !important;'> Unit </th>";
                AddParameter += "<th scope='bg-white text-align-right' style='background-color:#fff !important;'> Price </th>";
                AddParameter += "<th scope='bg-white text-align-right' style='background-color:#fff !important;'> Total Price </th>";
                AddParameter += "</tr>";
                AddParameter += "</thead>";
                AddParameter += "<tbody id=Implant_" + slno + ">";
                var implantTotalamount = 0;
                for (var j = 0; j < response.implantDetails.length; j++) {
                    if (response.implantDetails[j].txnpackagedetailid == response.packageDetails[i].txnpackagedetailid) {
                        if (j == 0) {
                            implantTotalamount = response.implantDetails[0].totalamount
                        }
                        AddParameter += "<tr>";
                        AddParameter += "<td>" + response.implantDetails[j].implantname + "</td>";
                        AddParameter += "<td>" + response.implantDetails[j].unit + "</td>";
                        AddParameter += "<td>" + response.implantDetails[j].unitcycleprice + "</td>";
                        AddParameter += "<td>" + response.implantDetails[j].amount + "</td>";
                        AddParameter += "</tr>";
                    }
                }
                AddParameter += "</tbody>";
                AddParameter += "<tfoot>";
                AddParameter += "<tr>";
                AddParameter += "<td class='font-weight-5'>Total </td>";
                AddParameter += "<td class='font-weight-5'></td>";
                AddParameter += "<td></td>";
                AddParameter += "<td colspan='2' class='font-weight-5 clsImplTotalInner1'>" + implantTotalamount + "</td>";
                AddParameter += "</tr>";
                AddParameter += "</tfoot>";
                AddParameter += "</table>";
                AddParameter += "</div>";
                AddParameter += "<div class='col-lg-6 col-md-12 col-sm-12 col-xs-12 mt-3'>";
                AddParameter += "<div id='table-scroll' class='table-scroll table-responsive mb-3'>";
                AddParameter += "<table id='tblHgDrugsInnerList1' class='main-table table table-striped'>";
                AddParameter += "<thead>";
                AddParameter += " <tr>";
                AddParameter += " <th scope='bg-white text-align-left' style='background-color:#fff !important;'> HighEndDrugs Name </th>";
                AddParameter += "<th scope='bg-white text-align-center' style='background-color:#fff !important;'> Unit </th>";
                AddParameter += "<th scope='bg-white text-align-right' style='background-color:#fff !important;'> Price </th>";
                AddParameter += "<th scope='bg-white text-align-right' style='background-color:#fff !important;'> Total Price </th>";
                AddParameter += "<th scope='bg-white text-align-right' style='background-color:#fff !important;'> Pre-Auth Required </th>";
                AddParameter += "</tr>";
                AddParameter += "</thead>";
                AddParameter += "<tbody id='HgDrugs_" + slno + "'>";
                var highAndDrugTotalamount = 0;
                for (var k = 0; k < response.highAndDrugDetails.length; k++) {
                    if (response.highAndDrugDetails[k].txnpackagedetailid == response.packageDetails[i].txnpackagedetailid) {
                        if (k == 0) {
                            implantTotalamount = response.highAndDrugDetails[0].totalhedprice
                        }
                        AddParameter += "<tr>";
                        AddParameter += "<td>" + response.highAndDrugDetails[k].hedname + "</td>";
                        AddParameter += "<td>" + response.highAndDrugDetails[k].hedunit + "</td>";
                        AddParameter += "<td>" + response.highAndDrugDetails[k].hedpriceperunit + "</td>";
                        AddParameter += "<td>" + response.highAndDrugDetails[k].preauth + "</td>";
                        AddParameter += "</tr>";
                    }
                }
                AddParameter += "</tbody>";
                AddParameter += "<tfoot>";
                AddParameter += "<tr>";
                AddParameter += "<td colspan='1'>Total </td>";
                AddParameter += "<td class='font-weight-5'>&nbsp;</td>";
                AddParameter += "<td>&nbsp;</td>";
                AddParameter += "<td class='font-weight-5' colspan='3'>" + highAndDrugTotalamount + "</td>";
                AddParameter += " </tr>";
                AddParameter += "</tfoot>";
                AddParameter += "</table>";
                AddParameter += "</div>";
                AddParameter += "</div>";
                AddParameter += "</div>";
                AddParameter += "</div>";
                AddParameter += "</div>";
                AddParameter += "</td>";
                AddParameter += "</tr>";
                $("#packageDetailsbody").append(AddParameter);
            }
            $("#ViewDetails").modal("show");
        },
        complete: function () {
            $("#loader").hide();
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
}

function UnBlockpackageslip(ele) {
    var DischargeSlip = [];
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();
    if (localStorage.getItem('InsertedUnbloackData') != null) {
        localStorage.removeItem('InsertedUnbloackData');
    }
    DischargeSlip.push(JSON.stringify({
        URN: dataRow.urn,
        MemberId: dataRow.memberID,
        HospitalCode: hospitalcode,
        Invoiceno: dataRow.unblockedinvoice
    }));
    localStorage.setItem('InsertedUnbloackData', DischargeSlip);
    windowUnblockPackageSlipPopUp();
}

function windowUnblockPackageSlipPopUp() {
    var url = "UnBlockSlip"; /*'@Url.Action("UnBlockSlip", "UnblockPackage")';*/
    var width = 1200;
    var height = 700;
    var left = (screen.width - width) / 2;
    var top = (screen.height - height) / 2;
    var params = 'width=' + width + ', height=' + height;
    params += ', top=' + top + ', left=' + left;
    params += ', directories=no';
    params += ', location=no';
    params += ', menubar=no';
    params += ', resizable=no';
    params += ', scrollbars=yes';
    params += ', status=no';
    params += ', toolbar=no';
    newwin = window.open(url, 'windowname5', params);
    if (window.focus) { newwin.focus() }
    return false;
}

function UnBlockHospitalSlip(ele) {
    var DischargeSlip = [];
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();
    if (localStorage.getItem('InsertedUnbloackData') != null) {
        localStorage.removeItem('InsertedUnbloackData');
    }
    DischargeSlip.push(JSON.stringify({
        URN: dataRow.urn,
        MemberId: dataRow.memberID,
        HospitalCode: hospitalcode,
        Invoiceno: dataRow.unblockedinvoice
    }));
    localStorage.setItem('InsertedUnbloackData', DischargeSlip);
    windowUnblockHospitalSlipPopUp();
}

function windowUnblockHospitalSlipPopUp() {
    var url = "UnBlockHospitalSlip";/*'@Url.Action("UnBlockHospitalSlip", "UnblockPackage")';*/
    var width = 1200;
    var height = 700;
    var left = (screen.width - width) / 2;
    var top = (screen.height - height) / 2;
    var params = 'width=' + width + ', height=' + height;
    params += ', top=' + top + ', left=' + left;
    params += ', directories=no';
    params += ', location=no';
    params += ', menubar=no';
    params += ', resizable=no';
    params += ', scrollbars=yes';
    params += ', status=no';
    params += ', toolbar=no';
    newwin = window.open(url, 'windowname5', params);
    if (window.focus) { newwin.focus() }
    return false;
}
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

function getStates() {
    var params = { Action: 'S' };
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
    var params = { Action: 'D', StateCode: stateCode };
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
    var params = { Action: 'H', StateCode: stateCode, DistrictCode: districtCode };
    $.ajax({
        url: ServiceURL + "/api/Common/GetHospitalByStateCodeAndDistrictCode",
        type: 'GET',
        data: params,
        contentType: 'application/json; charset=UTF-8',
        success: function (result) {
            // //debugger; console.log(result);
            $.each(result, function (i, data) {
                $("#ddHospital").append('<option value=' + data.hospitalCode + '>' + data.hospitalname + '</option>');
            });
        },
        error: function (jqXhr, textStatus, errorMessage) {
            alert(textStatus);
        }
    });
}

