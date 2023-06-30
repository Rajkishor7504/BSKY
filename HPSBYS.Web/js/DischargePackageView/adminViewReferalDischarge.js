
function popup(url) {
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

$(document).ready(function () {
    $('#path').html('View Discharged List');
    var d = new Date();
    var currentDate = d.getDate();
    var currMonth = d.getMonth();
    var currYear = d.getFullYear();
    var startDate = new Date(currYear, currMonth, currentDate);
    var endDate = new Date(currYear, currMonth, currentDate);
    $("#fromdate").val(moment(startDate).format("DD-MMM-YY"));
    $("#todate").val(moment(endDate).format("DD-MMM-YY"));
    $(".datepicker").datepicker({
        numberOfMonths: 1,
        changeMonth: true,
        dateFormat: "dd-M-y",
        changeYear: true

    });


    getStates();
    var frmdate = $("#fromdate").val();
    var to_date = $("#todate").val();
    DischargeRecord(frmdate, to_date);
});

function DischargeRecord(fromDt, toDt) {
    var hospitalcode = document.mynamespace.hospitalcode;
    var groupid = document.mynamespace.groupid;
    var data = JSON.stringify({
        Action: 'A',
        URN: "",
        MemberId: "",
        statecode: $('#ddState').val(),
        districtcode: $('#ddDistrict').val(),
        groupid: groupid,
        HospitalCode: $('#ddHospital').val(),
        Invoiceno: "",
        Formdate: fromDt,
        Todate: toDt,
        SearchBy: $("#ddlSearchByFilter option:selected").val()
    });
    $.ajax({
        url: ServiceURL + "/api/Discharge/GetAllDischarge",
        type: "POST",
        data: data,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (response) {
            $("#newData1").DataTable({
                bLengthChange: true,
                lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                bFilter: true,
                bSort: true,
                responsive: true,
                autoWidth: false,
                bPaginate: true,
                data: response,
                bDestroy: true,
                columns: [
                    { data: 'id', type: "text", title: "Sl#", align: "center", render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
                    { data: "urn", type: "text", title: "URN", align: "center" },
                    {
                        data: null,
                        align: "left",
                        render: function (data, type, row) {
                            var packageColumn = ' <b class="text-success">' + row.memberID + '</b>';
                            packageColumn += '<div class="pre-bold">' + row.memberName + '</div>';
                            return packageColumn;
                        }
                    },
                    //{ data: "memberName", type: "text", title: "Member Name", align: "center" },
                    //{ data: "memberID", type: "text", title: "Member Id", align: "center" },
                    { data: "caseno", type: "text", title: "Case No", align: "center" },
                    { data: "sysdischargedate", type: "text", title: "Discharge Date", align: "center" },
                    { data: "dateofDischarge", type: "text", title: "Actual Discharge Date", align: "center" },
                    {
                        data: "status", type: "text", title: "Status", align: "center", render: function (data, type, row, meta) {
                            if (row.status == "Fully Discharged") {
                                var html = '<div class="badge badge-success mb - 3 p - 2">' + row.status + '</div>'
                            }
                            if (row.status == "Partially Discharged") {
                                var html = '<div class="badge badge-danger mb - 3 p - 2">' + row.status + '</div>'
                            }
                            return html;
                        }
                    },
                ],
                columnDefs: [
                    {
                        targets: 7,
                        data: null,
                        width: "200",
                        className: "text-center",
                        title: "Action",
                        render: function (data, type, row, meta) {
                            var actionContent = "<span style='float:right;'>";
                            if (row.referralStatus == "Yes") {
                                actionContent += "<button type='button' tooltip='Referral Slip' onclick='RefaralSlip(this)' class='btn btn-outline-info focusedBtn btn-sm my-2 my-sm-0 mr-2'><i class='fa fa-file'></i></button>";
                            }
                            actionContent += "<button type='button' tooltip='Hospital Slip' onclick='btnHospitalSlip(this)' class='btn btn-outline-success focusedBtn btn-sm my-2 my-sm-0 mr-2'><i class='fa fa-files-o'></i></button>"
                                + "<button type = 'button' tooltip = 'Patient Slip' onclick='btnPatientSlip(this)' class='btn btn-outline-secondary focusedBtn btn-sm my-2 my-sm-0 mr-2' > <i class='fa fa-file-text-o'></i></button>"
                                + "<button type = 'button' tooltip = 'View Details' onclick='bindDischargeView(this)' class='btn btn-outline-danger focusedBtn btn-sm my-2 mr-2 my-sm-0' ><i class='fa fa-eye'></i></button></span>";
                            return actionContent;
                        }
                    }],
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
/*$("#btnSearch").click(function ()*/
function search() {

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
            text: "Please select To Date",
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
        DischargeRecord(fromDt, toDt);
    }
};

function RefaralSlip(ele) {
    var hospitalcode = document.mynamespace.hospitalcode;
    var dataRow = $('#newData1').DataTable().row($(ele).closest('tr')).data();
    var modeldata = JSON.stringify({
        Action: 'B',
        URN: dataRow.urn,
        MemberId: dataRow.memberID,
        HospitalCode: hospitalcode,
        Invoiceno: dataRow.invoiceNo,
    });
    $.ajax({
        url: ServiceURL + "/api/Discharge/GetDischargeReferalSlip",
        type: "POST",
        data: modeldata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (response) {
            console.log("Discharslip", response);
            if (response.length > 0) {
                localStorage.setItem('ReferralSlipData', JSON.stringify(response));
            }
            windowPopUp();
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

function btnHospitalSlip(ele) {
    var DischargeSlip = [];
    var dataRow = $('#newData1').DataTable().row($(ele).closest('tr')).data();
    if (localStorage.getItem('InsertedData') != null) {
        localStorage.removeItem('InsertedData');
    }
    DischargeSlip.push(JSON.stringify({
        URN: dataRow.urn,
        MemberId: dataRow.memberID,
        HospitalCode: document.mynamespace.hospitalcode,
        Invoiceno: dataRow.invoiceNo
    }));
    localStorage.setItem('InsertedData', DischargeSlip);
    windowDischargeSlipPopUp();
}

function bindDischargeView(ele) {
    var groupid = document.mynamespace.groupid;
    var hospitalcode = document.mynamespace.hospitalcode;
    var dataRow = $('#newData1').DataTable().row($(ele).closest('tr')).data();
    console.log(dataRow);
    var param = JSON.stringify({
        Action: 'A',
        groupid: groupid,
        Invoiceno: dataRow.invoiceNo,
    });
    $.ajax({
        url: ServiceURL + "/api/Discharge/GetDischargeDetails",
        type: "POST",
        data: param,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (response) {
            if (response.patientDetails.referralflag == "YES") {
                $("#referralStatus").show();
                if (response.refaralDetails.length > 0) {
                    $('#fromhospitalname').text(response.refaralDetails[0].fromhospitalname);
                    $('#fromdrname').text(response.refaralDetails[0].fromdrname);
                    $('#fromdeptname').text(response.refaralDetails[0].fromdeptname);
                    $('#fromreferraldate').text(response.refaralDetails[0].fromreferraldate);
                    $('#tohospital').text(response.refaralDetails[0].tohospital);
                    $('#reasonforrefer').text(response.refaralDetails[0].reasonforrefer);
                    $('#referralcode').text(response.refaralDetails[0].referralcode);
                }
            }
            else {
                $("#referralStatus").hide();
            }
            $("#packageDetailsbody").empty();
            $("#HgDrugs").empty();
            $("#tblverifieddetails").empty();
            $("#tbldVitaletails").empty();
            console.log("DischargeView", response);
            $('#PatientName').text(response.patientDetails.membername);
            $('#memberid').text(response.patientDetails.memberid);
            $('#gender').text(response.patientDetails.patientgender);
            $('#age').text(response.patientDetails.age);
            $('#adharno').text(response.patientDetails.uidnumber);
            $('#surgicaltype').text(response.patientDetails.treatmenttype);
            $('#admissiondate').text(response.admissionDetails.admissiondate);
            $('#doctorname').text(response.admissionDetails.doctorname);
            $('#doctorphno').text(response.admissionDetails.doctorphno);
            $('#patientcontactnumber').text(response.admissionDetails.patientcontactnumber);
            $('#overridecode').text(response.admissionDetails.overridecode);
            $('#referalcode').text(response.admissionDetails.referalcode);
            $('#description').text(response.admissionDetails.description);
            $('#TotalPackageCost').text(toIndianCurrency(parseFloat(response.patientDetails.totalpackagecost)));
           
            for (var i = 0; i < response.packageDetails.length; i++) {
                var slno = i + 1;
                console.log("tr", response.packageDetails[i]);
                var AddParameter = "<tr class='parent'>" +
                    "<td>" + slno + "</td>" +
                    "<td>" + response.packageDetails[i].packageheadername + "</td>" +
                    "<td>" + response.packageDetails[i].packagesubcategoryname + "</td>" +
                    "<td>" + response.packageDetails[i].procedurecode + "</td>" +
                    "<td>" + toIndianCurrency(parseFloat(response.packageDetails[i].totalpackagecost)) + "</td>" +
                    "<td>" + response.packageDetails[i].noofdays + "</td>" +
                    "<td>" + response.packageDetails[i].preauthstatus + "</td>" +
                    "<td>" + toIndianCurrency(parseFloat(response.packageDetails[i].amountblocked)) + "</td>" +
                    "<td><button type='button' class='btn btn-outline-success focusedBtn btn-sm my-2 my-sm-0 detailsPanal' data-toggle='collapse' data-target='#pkgmodalcls_" + slno + "'> View Details <i class='fa fa-eye'></i></button></td></tr>";

                AddParameter += "<tr class ='child'>";
                AddParameter += "<td colspan='12' class='m-0 p-0 border-0'>";
                AddParameter += "<div class='accordian-body collapse' id='pkgmodalcls_" + slno + "'>";
                AddParameter += "<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 mt-3'>";
                AddParameter += "<div class='border'>";
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
                        AddParameter += "<td>" + toIndianCurrency(parseFloat(response.implantDetails[j].unitcycleprice)) + "</td>";
                        AddParameter += "<td>" + toIndianCurrency(parseFloat(response.implantDetails[j].amount)) + "</td>";
                        AddParameter += "</tr>";
                    }
                }
                AddParameter += "</tbody>";
                AddParameter += "<tfoot>";
                AddParameter += "<tr>";
                AddParameter += "<td class='font-weight-5'>Total </td>";
                AddParameter += "<td class='font-weight-5'></td>";
                AddParameter += "<td></td>";
                AddParameter += "<td colspan='2' class='font-weight-5 clsImplTotalInner1'>" + toIndianCurrency(parseFloat(implantTotalamount)) + "</td>";
                AddParameter += "</tr>";
                AddParameter += "</tfoot>";
                AddParameter += "</table>";
                AddParameter += "</div>";
                AddParameter += "<div class='col-lg-6 col-md-12 col-sm-12 col-xs-12 mt-3'>";
                AddParameter += "<div id='table-scroll' class='table-scroll table-responsive mb-3'>";
                AddParameter += "<table id='tblHgDrugsInnerList1' class='main-table table table-striped'>";
                AddParameter += "<thead>";
                AddParameter += " <tr>";
                AddParameter += " <th style='background-color:#fff !important;'> HighEndDrugs Name </th>";
                AddParameter += "<th  style='background-color:#fff !important;'> Unit </th>";
                AddParameter += "<th  style='background-color:#fff !important;'> Price </th>";
                AddParameter += "<th  style='background-color:#fff !important;'> Total Price </th>";
                AddParameter += "<th  style='background-color:#fff !important;'> Pre-Auth Required </th>";
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
                        AddParameter += "<td>" + toIndianCurrency(parseFloat(response.highAndDrugDetails[k].hedpriceperunit)) + "</td>";
                        AddParameter += "<td>" + response.highAndDrugDetails[k].preauth + "</td>";
                        AddParameter += "</tr>";
                    }
                }
                AddParameter += "</tbody>";
                AddParameter += "<tfoot>";
                AddParameter += "<tr>";
                AddParameter += "<td colspan='1'>Total</td>";
                AddParameter += "<td class='font-weight-5'>&nbsp;</td>";
                AddParameter += "<td>&nbsp;</td>";
                AddParameter += "<td class='font-weight-5' colspan='3'>" + toIndianCurrency(parseFloat(highAndDrugTotalamount)) + "</td>";
                AddParameter += " </tr>";
                AddParameter += "</tfoot>";
                AddParameter += "</table>";
                AddParameter += "</div>";
                AddParameter += "</div>";
                AddParameter += "</div>";
                AddParameter += "</div>";


                AddParameter += "<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 mt-3 p-0'>";
                AddParameter += "<div class='border'>";
                AddParameter += "<div class='headingpkg-bg m-0'> Verified  Details </div>";

                AddParameter += "<table class='table table-striped'>";
                AddParameter += "<thead>";
                AddParameter += "<tr>";
                /* AddParameter += "<th> Sl No </th>";*/
                AddParameter += "<th> Member ID </th>";
                AddParameter += "<th> Name </th>";
                AddParameter += "<th> Ration Card No </th>";
                AddParameter += "<th> Verified Through </th>";
                AddParameter += " </tr>";
                AddParameter += "</thead>";
                AddParameter += "<tbody id='verifiedDetail_" + slno + "'>";
                for (var s = 0; s < response.verifiedDetails.length; s++) {
                    if (response.verifiedDetails[s].txnpackagedetailid == response.packageDetails[i].txnpackagedetailid) {
                        AddParameter += "<tr>";
                        /*AddParameter += "<td>" + response.verifiedDetails[s].txnpackagedetailid + " </td>";*/
                        AddParameter += "<td>" + response.verifiedDetails[s].disverifiedmemberid + "</td>";
                        AddParameter += "<td> " + response.verifiedDetails[s].disverifiedmembername + " </td>";
                        AddParameter += "<td> " + response.verifiedDetails[s].urn + " </td>";
                        AddParameter += "<td> " + response.verifiedDetails[s].verificationmode + " </td>";
                        AddParameter += "</tr>";
                    }
                }
                AddParameter += "</tbody>";
                AddParameter += "</table>";
                AddParameter += "</div>";
                AddParameter += "</div>";


                AddParameter += "<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 mt-3 mb-3 p-0'>";
                AddParameter += "<div class='border'>";
                AddParameter += "<div class='headingpkg-bg m-0'> Discharge Documents </div>";

                AddParameter += "<table id='tbldocuments' class='table table-striped'>";
                AddParameter += "<thead>";
                AddParameter += "<tr>";
                AddParameter += "<th> Intra Surgery </th>";
                AddParameter += "<th> Post Surgery </th>";
                AddParameter += "<th> Pre Surgery </th>";
                AddParameter += "<th> Specimen Removal </th>";
                AddParameter += "<th> Discharge Document </th>";
                AddParameter += " </tr>";
                AddParameter += "</thead>";

                AddParameter += "<tbody>";
                for (var x = 0; x < response.dischargeDocDetails.length; x++) {
                    if (response.dischargeDocDetails[x].txnpackagedetailid == response.packageDetails[i].txnpackagedetailid) {
                        AddParameter += "<tr>";
                        if (response.dischargeDocDetails[x].intrasurgery != null) {
                            AddParameter += "<td>" +
                                "<div class='pkghead-txt'>" +
                                "<button data-pathname='" + response.dischargeDocDetails[x].intrasurgery + "' data-filename='" + response.dischargeDocDetails[x].intrasurgeryname + "' onclick='DownloadDoc(this);' class='btn btn-outline-success btn-sm docDownloadClass'  target='blank'  title='Download'> <i class='fa fa-image'></i> Download </button>" +
                                "</div>" +
                                "</td>";
                        }
                        else {
                            AddParameter += "<td>" +
                                "<div class='pkghead-txt'>" +
                                "<button id='intrasurgery' class='btn btn-outline-warning btn-sm user-select-none' disabled  target='blank'  title='Download'> <i class='fa fa-image'></i> Download </button>" +
                                "</div>" +
                                "</td>";
                        }

                        if (response.dischargeDocDetails[x].postsurgery != null) {
                            AddParameter += "<td>" +
                                "<div class='pkghead-txt'>" +
                                "<button data-pathname='" + response.dischargeDocDetails[x].postsurgery + "' data-filename='" + response.dischargeDocDetails[x].postsurgeryname + "' onclick='DownloadDoc(this);' class='btn btn-outline-success btn-sm docDownloadClass'  target='blank'  title='Download'> <i class='fa fa-image'></i> Download </button>" +
                                "</div>" +
                                "</td>";
                        }
                        else {
                            AddParameter += "<td>" +
                                "<div class='pkghead-txt'>" +
                                "<button id='intrasurgery' class='btn btn-outline-warning btn-sm user-select-none' disabled  target='blank'  title='Download'> <i class='fa fa-image'></i> Download </button>" +
                                "</div>" +
                                "</td>";
                        }
                        if (response.dischargeDocDetails[x].presurgery != null) {
                            AddParameter += "<td>" +
                                "<div class='pkghead-txt'>" +
                                "<button data-pathname='" + response.dischargeDocDetails[x].presurgery + "' data-filename='" + response.dischargeDocDetails[x].presurgeryname + "' onclick='DownloadDoc(this);' class='btn btn-outline-success btn-sm docDownloadClass'  target='blank'  title='Download'> <i class='fa fa-image'></i> Download </button>" +
                                "</div>" +
                                "</td>";
                        }
                        else {
                            AddParameter += "<td>" +
                                "<div class='pkghead-txt'>" +
                                "<button id='intrasurgery' class='btn btn-outline-warning btn-sm user-select-none' disabled  target='blank'  title='Download'> <i class='fa fa-image'></i> Download </button>" +
                                "</div>" +
                                "</td>";
                        }
                        if (response.dischargeDocDetails[x].specimenremoval != null) {
                            AddParameter += "<td>" +
                                "<div class='pkghead-txt'>" +
                                "<button data-pathname='" + response.dischargeDocDetails[x].specimenremoval + "' data-filename='" + response.dischargeDocDetails[x].specimenremovalname + "' onclick='DownloadDoc(this);' class='btn btn-outline-success btn-sm docDownloadClass'  target='blank'  title='Download'> <i class='fa fa-image'></i> Download </button>" +
                                "</div>" +
                                "</td>";
                        }
                        else {
                            AddParameter += "<td>" +
                                "<div class='pkghead-txt'>" +
                                "<button id='intrasurgery' class='btn btn-outline-warning btn-sm user-select-none' disabled  target='blank'  title='Download'> <i class='fa fa-image'></i> Download </button>" +
                                "</div>" +
                                "</td>";
                        }
                        if (response.dischargeDocDetails[x].dischargedoc != null) {
                            AddParameter += "<td>" +
                                "<div class='pkghead-txt'>" +
                                "<button data-pathname='" + response.dischargeDocDetails[x].dischargedoc + "' data-filename='" + response.dischargeDocDetails[x].dischargedocname + "' onclick='DownloadDoc(this);' class='btn btn-outline-success btn-sm' target='blank' title='Download'> <i class='fa fa-image'></i> Download </button>" +
                                "</div>" +
                                "</td>";
                        }
                        else {
                            AddParameter += "<td>" +
                                "<div class='pkghead-txt'>" +
                                "<button id='intrasurgery' class='btn btn-outline-warning btn-sm user-select-none' disabled  target='blank'  title='Download'> <i class='fa fa-image'></i> Download </button>" +
                                "</div>" +
                                "</td>";
                        }
                    }
                }
                AddParameter += "</tbody>";
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
            //for (var s = 0; s < response.verifiedDetails.length; s++) {
            //    var slno1 = s + 1;
            //    var AddverifyDetails = "<tr>" +
            //        "<td>" + slno1 + "</td>" +
            //        "<td>" + response.verifiedDetails[s].disverifiedmemberid + "</td>" +
            //        "<td>" + response.verifiedDetails[s].disverifiedmembername + "</td>" +
            //        "<td>" + response.verifiedDetails[s].urn + "</td>" +
            //        "<td>" + response.verifiedDetails[s].verificationmode + "</td>" +
            //        "</tr>";
            //    $("#tblverifieddetails").append(AddverifyDetails);
            //}

            for (var t = 0; t < response.vitalDetails.length; t++) {
                var slno2 = t + 1;
                var AddVitaldetails = "<tr>" +
                    "<td>" + slno2 + "</td>" +
                    "<td>" + response.vitalDetails[t].vitalsign + "</td>" +
                    "<td>" + response.vitalDetails[t].vitalvalue + "</td>" +
                    "</tr>";
                $("#tbldVitaletails").append(AddVitaldetails);
            }
            $('#DischargeView').modal('show');
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

function windowPopUp() {
    var url = "ViewReferalSlip";//'@Url.Action("ViewReferalSlip", "Discharge")';
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

function windowDischargeSlipPopUp() {
    var url = "ViewDischargeSlip";//'@Url.Action("ViewDischargeSlip", "Discharge")';
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

function btnPatientSlip(ele) {

    var DischargeSlip = [];
    var dataRow = $('#newData1').DataTable().row($(ele).closest('tr')).data();
    if (localStorage.getItem('InsertedData') != null) {
        localStorage.removeItem('InsertedData');
    }
    DischargeSlip.push(JSON.stringify({
        URN: dataRow.urn,
        MemberId: dataRow.memberID,
        HospitalCode: document.mynamespace.hospitalcode,
        Invoiceno: dataRow.invoiceNo
    }));
    localStorage.setItem('InsertedData', DischargeSlip);
    windowPatientSlipPopUp();
}

function windowPatientSlipPopUp() {
    var url = "ViewPatientSlip";//'@Url.Action("ViewPatientSlip", "Discharge")';
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

function DownloadDoc(ele) {
    var referraldoc = $(ele).data("pathname");
    var filename = $(ele).data('filename');
    window.location.href = "Downloadfile?pathName=" + referraldoc + "&filename=" + filename;
}

const toIndianCurrency = (num) => {
    const curr = num.toLocaleString('en-IN', {
        style: 'currency',
        currency: 'INR'
    });
    return curr;
};


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

//breadcrumb active & menu active
$(document).ready(function () {
    loadNavigation('ViewReferalDischarge', 'MtReports', 'pl', 'Discharge', 'View Referral Discharge', '');
});

