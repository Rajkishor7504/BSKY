var hospitalcode = document.mynamespace.hospitalcode;
var userImg = document.mynamespace.userImg;
$(document).ready(function () {


    $('#path').html('View Block Package List');
    $('#example2').DataTable({
        autoWidth: false,
    });

    var d = new Date();
    var currentDate = d.getDate();
    var currMonth = d.getMonth();
    var currYear = d.getFullYear();
    var startDate = new Date(currYear, currMonth, 1);
    var endDate = new Date(currYear, currMonth, currentDate);
    $("#fromdate").val(moment(startDate).format("DD-MMM-YY"));
    $("#todate").val(moment(endDate).format("DD-MMM-YY"));
    var frmdate = $("#fromdate").val();
    var to_date = $("#todate").val();
    getData(frmdate, to_date);
});
$(function () {
    $(".datepicker").datepicker({
        numberOfMonths: 1,
        changeMonth: true,
        dateFormat: "dd-M-y",
        changeYear: true

    });
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
        swal({
            text: "Please select From date",
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
            text: "Please select To date",
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
            text: "From date should not be greater than To date",
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
function getData(frmdate, to_date) {

    var param = {

        HospitalCode: hospitalcode,
        //HospitalCode: '@Session["HospitalCode"]',
        fromdate: frmdate,
        todate: to_date,
        AdmissionDateType: $('#ddAdmissionDateType').val()
    };
    $.ajax({
        url: ServiceURL + "/api/BlockPackage/GetViewBlockpackagedetailsList",
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

function drawTable(resultBal) {
    var table = $('#example2').DataTable();
    table.destroy();
    $('#example2').dataTable({
        responsive: true,
        autoWidth: false,
        width: "200",
        className: "text-center",

        // sDom: 'lrtip',//remove search textbox from datatable
        "aaData": resultBal,   //this is your JSON object, which is what is showing in your post above with console.log(data)
        "aoColumns": [
            {
                "render": function (resultBal, type, full, meta) {
                    return meta.row + 1;
                }
            },
            { "mDataProp": "urn" },
            { "mDataProp": "membername" },
            { "mDataProp": "memberid" },
            { "mDataProp": "uidnumber" },
            { "mDataProp": "caseno" },
            { "mDataProp": "admissiondate", "className": "dt-body-center" },
            { "mDataProp": "blockingDate", "className": "dt-body-center" },
            {
                "render": function (resultBal, type, row) {
                    return '<span style="float: right;"><button type="button" tooltip="Hospital Slip" onclick="Hospitalslip(this)" class="btn btn-outline-secondary focusedBtn btn-sm my-2 my-sm-0 mr-2"><i class="fa fa-files-o"></i></button><button type = "button" onclick = "Blockpackageslip(this)" tooltip = "Patient Slip" class="btn btn-outline-warning focusedBtn btn-sm my-2 my-sm-0 mr-2" ><i class="fa fa-file-text-o"></i></button></i></button><button type = "button" tooltip = "View Details" data - toggle="modal" onclick = "ShowBlockdetails(' + row.transactionid + ')"class="btn btn-outline-danger focusedBtn btn-sm my-2 mr-2 my-sm-0" > <i class="fa fa-eye"></i></button></span>'
                }
            },

        ]
    });
}
function downloadfile() {


    var hospitalcode = document.mynamespace.hospitalcode;
    var PREAUTHDOC = $('#preauthdocid').val();
    // var PATIENTPHOTO= $('#patientpicid').val();
    var URN = $('#urnid').val();
    var ADMISSIONDATE = $('#admissiondateid').val();

    window.location.href = "Downloaddocfile?hospitalcode=" + hospitalcode + "&filename=" + PREAUTHDOC + "&urn=" + URN + "&dateofadmision=" + ADMISSIONDATE; //"@Url.Action("Downloaddocfile", "BlockPackage")?hospitalcode=" + hospitalcode + "&filename=" + PREAUTHDOC + "&urn=" + URN + "&dateofadmision=" + ADMISSIONDATE;


}
function downloadphotofile() {
    var hospitalcode = document.mynamespace.hospitalcode;
    //var PREAUTHDOC=$('#preauthdocid').val();
    var PATIENTPHOTO = $('#patientpicid').val();
    var URN = $('#urnid').val();
    var ADMISSIONDATE = $('#admissiondateid').val();
    window.location.href = "downloadphotofile?hospitalcode=" + hospitalcode + "&filename=" + PATIENTPHOTO + "&urn=" + URN + "&dateofadmision=" + ADMISSIONDATE;


}
function ShowBlockdetails(transctionid) {

    var param = {
        hospitalcode: hospitalcode,
        TRANSACTIONID: transctionid
    };
    $.ajax({
        url: ServiceURL + "/api/BlockPackage/GetViewBlockpackagedetailsListByID",
        type: "POST",
        data: param,
        dataType: "json",
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (resultBal) {
            $('#membernameid').html(resultBal[0].membername);
            $('#memberid').html(resultBal[0].memberid);
            $('#genderid').html(resultBal[0].patientgender);
            $('#age').html(resultBal[0].age);
            $('#adharno').html(resultBal[0].uidnumber);
            $('#surgicaltype').html(resultBal[0].surgicaltype);
            $('#idremarks').html(resultBal[0].remarks);
            $('#tblverifieddetails').empty();
            for (var i = 0; i < resultBal.length; i++) {
                var htm = '';
                if ($('#tblverifieddetails tr').length == 0) {
                    htm += '<thead><tr> <th>Sl#</th><th>Member Id</th><th>Name</th><th>Gender</th><th>Age</th><th>Aadhar Card No</th><th>Ration Card No</th><th>Verified Through</th></tr></thead>'
                    htm += '<tr> <td> 1 </td><td>' + resultBal[i].verifiedmemberid + '</td>'
                    htm += '<td>' + resultBal[i].verifiedmembername + '</td>'
                    htm += '<td>' + resultBal[i].verifiedmembergender + '</td>'
                    htm += '<td>' + resultBal[i].verifiedmemberage + '</td>'
                    htm += '<td>' + resultBal[i].verifiedmemberaadhar + '</td>'
                    htm += '<td>' + resultBal[i].urn + '</td>'
                    htm += '<td>' + resultBal[i].verificationmode + '</td></tr>'
                }

                else {
                    htm += '<tr> <td> ' + $('#tblverifieddetails tr').length.toString() + '<td>' + resultBal[i].verifiedmemberid + '</td>'
                    htm += '<td>' + resultBal[i].verifiedmembername + '</td>'
                    htm += '<td>' + resultBal[i].verifiedmembergender + '</td>'
                    htm += '<td>' + resultBal[i].verifiedmemberage + '</td>'
                    htm += '<td>' + resultBal[i].verifiedmemberaadhar + '</td>'
                    htm += '<td>' + resultBal[i].urn + '</td>'
                    htm += '<td>' + resultBal[i].verificationmode + '</td></tr>'

                }
                $("#tblverifieddetails").append(htm);

            }
            //for package details
            $("#bodypackagedetails").empty();
            $.ajax({
                url: ServiceURL + "/api/BlockPackage/GetPackageDetailsListByID",
                type: "POST",
                data: param,
                dataType: "json",
                beforeSend: function () {
                    $("#loader").show();
                },
                success: function (response) {

                    for (var i = 0; i < response.packagedetails.length; i++) {
                        //     $('#HighendAmt').text(response.packagedetails[i].amountblocked);
                        //$('#TotalPackageCost').text(response.packagedetails[i].totalpackagecost);

                        var slno = i + 1;
                        console.log("tr", response.packagedetails[i]);
                        var AddParameter = "<tr class='parent'>" +
                            "<td>" + slno + "</td>" +
                            "<td>" + response.packagedetails[i].packageheadername + "</td>" +
                            "<td>" + response.packagedetails[i].packagesubcategoryname + "</td>" +
                            "<td>" + response.packagedetails[i].procedurecode + "</td>" +
                            "<td>" + response.packagedetails[i].treatmentcost + "</td>" +
                            "<td>" + response.packagedetails[i].days + "</td>" +
                            "<td>" + response.packagedetails[i].preauthstatus + "</td>" +
                            "<td>" + response.packagedetails[i].blockinguserdate + "</td>" +

                            "<td><button type='button' class='btn btn-outline-success focusedBtn btn-sm my-2 my-sm-0 detailsPanal' data-toggle='collapse' data-target='#pkgmodalcls_" + slno + "'> View Details <i class='fa fa-eye'></i></button></td></tr>";
                        AddParameter += " </tr>";

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
                        for (var j = 0; j < response.implantdetails.length; j++) {
                            if (response.implantdetails[j].txnpackagedetailid == response.packagedetails[i].txnpackagedetailid) {
                                if (j == 0) {
                                    implantTotalamount = response.implantdetails[0].totalamount
                                }
                                AddParameter += "<tr>";
                                AddParameter += "<td>" + response.implantdetails[j].implantname + "</td>";
                                AddParameter += "<td>" + response.implantdetails[j].unit + "</td>";
                                AddParameter += "<td>" + response.implantdetails[j].unitcycleprice + "</td>";
                                AddParameter += "<td>" + response.implantdetails[j].amount + "</td>";
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
                        for (var k = 0; k < response.highenddrugsdetails.length; k++) {
                            if (response.highenddrugsdetails[k].txnpackagedetailid == response.packagedetails[i].txnpackagedetailid) {
                                if (k == 0) {
                                    highAndDrugTotalamount = response.highenddrugsdetails[0].totalhedprice
                                }
                                AddParameter += "<tr>";
                                AddParameter += "<td>" + response.highenddrugsdetails[k].hedname + "</td>";
                                AddParameter += "<td>" + response.highenddrugsdetails[k].hedunit + "</td>";
                                AddParameter += "<td>" + response.highenddrugsdetails[k].hedpriceperunit + "</td>";
                                AddParameter += "<td>" + response.highenddrugsdetails[k].hedprice + "</td>";
                                AddParameter += "<td>" + response.highenddrugsdetails[k].preauth + "</td>";
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


                        AddParameter += "</tfoot>";

                        AddParameter += "</table>";
                        AddParameter += "<tr><td colspan='8' class='font-weight-5 clsImplTotalInner1'>" + "Amount Blocked" + "</td>";
                        AddParameter += "<td colspan='2' class='font-weight-5 clsImplTotalInner1'>" + response.packagedetails[i].amountblocked + "</td></tr>";
                        AddParameter += "<tr><td colspan='8' class='font-weight-5 clsImplTotalInner1'>" + "Total Cost (Total Package Cost +Implant Cost +Highend Drugs Cost)" + "</td>";
                        AddParameter += "<td colspan='2' class='font-weight-5 clsImplTotalInner1'>" + response.packagedetails[i].totalpackagecost + "</td></tr>";
                        AddParameter += "</div>";
                        AddParameter += "</div>";
                        AddParameter += "</div>";
                        AddParameter += "</div>";
                        AddParameter += "</div>";
                        AddParameter += "</td>";
                        AddParameter += "</tr>";
                        $("#bodypackagedetails").append(AddParameter);
                    }

                }
            });


            //for admission details
            $.ajax({
                url: ServiceURL + "/api/BlockPackage/GetAdmissionDetailsListByID",
                type: "POST",
                data: param,
                dataType: "json",
                beforeSend: function () {
                    $("#loader").show();
                },
                success: function (resultBal) {
                    $('#patientdiv').hide();
                    $('#preauthdiv').hide();
                    if (resultBal[0].preauthdoc != null) {
                        $('#preauthdiv').show();
                        $('#preauthdocid').val(resultBal[0].preauthdoc);
                    }
                    else {
                        $('#preauthdiv').hide();
                    }
                    if (resultBal[0].patientphoto != null) {
                        $('#patientdiv').show();
                        $('#patientpicid').val(resultBal[0].patientphoto);
                        $('#patientImg').attr('src', resultBal[0].patientphoto);
                    }
                    else {
                        $('#patientdiv').hide();
                        $('#patientImg').attr('src', userImg);
                    }
                    $('#urnid').val(resultBal[0].urn);
                    $('#admissiondateid').val(resultBal[0].admissiondate);
                    $("#tbladmissiondetails").empty();
                    for (var i = 0; i < resultBal.length; i++) {
                        var htm = '';
                        if ($('#tbladmissiondetails tr').length == 0) {
                            htm += '<thead><tr> <th>Sl#</th><th>Actual Date of Admission</th><th>Doctor Name</th><th>Doctor Phone No</th><th>Patient Phone No</th><th>Override Code</th><th>Referal Code</th><th>Description</th></tr></thead>'
                            htm += '<tr> <td> 1 </td><td>' + resultBal[i].admissiondate + '</td>'
                            htm += '<td>' + resultBal[i].doctorname + '</td>'
                            htm += '<td>' + resultBal[i].doctorphno + '</td>'

                            htm += '<td>' + resultBal[i].patientcontactnumber + '</td>'
                            htm += '<td>' + resultBal[i].overridecode + '</td>'
                            htm += '<td>' + resultBal[i].referalcode + '</td>'
                            htm += '<td>' + resultBal[i].description + '</td></tr>'
                        }

                        else {
                            htm += '<tr> <td> ' + $('#tbladmissiondetails tr').length.toString() + '<td>' + resultBal[i].admissiondate + '</td>'
                            htm += '<td>' + resultBal[i].doctorname + '</td>'
                            htm += '<td>' + resultBal[i].doctorphno + '</td>'
                            htm += '<td>' + resultBal[i].patientcontactnumber + '</td>'
                            htm += '<td>' + resultBal[i].overridecode + '</td>'
                            htm += '<td>' + resultBal[i].referalcode + '</td>'
                            htm += '<td>' + resultBal[i].description + '</td></tr>'

                        }
                        $("#tbladmissiondetails").append(htm);


                    }
                },
                complate: function () {
                    $("#loader").hide();
                },
            });
            //for vital parameter
            $.ajax({
                url: ServiceURL + "/api/BlockPackage/GetVitalparameterListByID",
                type: "POST",
                data: param,
                dataType: "json",
                beforeSend: function () {
                    $("#loader").show();
                },
                success: function (resultBal) {
                    $("#tblvitalparameter").empty();
                    if (resultBal.length == 0) {
                        $('#vitalparameterid').hide();
                    }
                    else {
                        for (var i = 0; i < resultBal.length; i++) {
                            var htm = '';
                            if ($('#tblvitalparameter tr').length == 0) {
                                htm += '<tr> <th>Sl#</th><th>Parameter</th><th>Value</th></tr>'
                                htm += '<tr> <td> 1 </td><td>' + resultBal[i].vitalsign + '</td>'
                                htm += '<td>' + resultBal[i].vitalvalue + '</td></tr>'
                            }

                            else {
                                htm += '<tr> <td> ' + $('#tblvitalparameter tr').length.toString() + '<td>' + resultBal[i].vitalsign + '</td>'
                                htm += '<td>' + resultBal[i].vitalvalue + '</td></tr>'

                            }

                            $("#tblvitalparameter").append(htm);


                        }
                        $('#vitalparameterid').show();
                    }
                },
                complete: function () {
                    $("#loader").hide();
                },
            });
            $("#ViewDetails").modal("show");
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
}

function Blockpackageslip(ele) {
    var BlockpackageSlip = [];
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();

    if (localStorage.getItem('InsertedData') != null) {
        localStorage.removeItem('InsertedData');
    }

    BlockpackageSlip.push(JSON.stringify({
        INVOICE: dataRow.invoice,
        URN: dataRow.urn
    }));
    localStorage.setItem('InsertedData', BlockpackageSlip);
    windowBlockpackageSlipPopUp();
}
function windowBlockpackageSlipPopUp() {
    var url = "ViewBlockingSlip";

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

function Hospitalslip(ele) {
    var BlockpackageSlip = [];
    var dataRow = $('#example2').DataTable().row($(ele).closest('tr')).data();

    if (localStorage.getItem('InsertedData') != null) {
        localStorage.removeItem('InsertedData');
    }

    BlockpackageSlip.push(JSON.stringify({
        INVOICE: dataRow.invoice,
        URN: dataRow.urn
    }));
    localStorage.setItem('InsertedData', BlockpackageSlip);
    windowHospitalSlipPopUp();
}
function windowHospitalSlipPopUp() {
    var url = "ViewHospitalSlip";
    //var url = '@Url.Action("ViewHospitalSlip", "BlockPackage")';
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

function popup1(url) {
    var width = 1400;
    var height = 700;
    var left = (screen.width - width) / 2;
    var top = (screen.height - height) / 2;
    //var left   = 0;
    // var top    = 0;
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

//Restrict special character input into fields
$('input').bind('input', function () {
    var c = this.selectionStart,
        r = /[^a-z0-9 .]/gi,
        v = $(this).val();
    if (r.test(v)) {
        $(this).val(v.replace(r, ''));
        c--;
    }
    this.setSelectionRange(c, c);
});

// breadcrumb active & menu active
$(document).ready(function () {
    loadNavigation('ViewBlockPackageDetails', 'Mlblockpackage', 'pl', 'Block Package', 'View BlockPackage Details', '');

});
