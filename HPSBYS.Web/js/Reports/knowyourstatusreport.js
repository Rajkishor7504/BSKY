var hospitalcode = document.mynamespace.hospitalcode;

$(document).ready(function () {
    $("#lblddPatientName").hide();
    $("#divddPatientName").hide();
    $("#tableid").hide();
})

$("#btnSearchUrn").click(function () {
    if ($("#txtUrnNo").val() != "") {
        getUrnData($("#txtUrnNo").val());
    }
    else {
        swal({
            text: "Please enter NFSA/SFSS or BSKY card no.",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#txtUrnNo").focus();
        });
    }
});

function getUrnData(data) {
    var params = { URN: data };
    $.ajax({
        url: ServiceURL + "/api/URN/GetFamilyMemeberList",
        type: "GET",
        data: params,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loader").show();
        },
        error: function (error) {
            console.log(error.statusText);
        },
        complete: function () {
            $("#loader").hide();
        }
    }).done(function (result) {
        //console.log(result);
        if (result.length > 0) {
            $("#ddPatientName").empty().append('<option value="0">-Select-</option>').removeClass("border-success border-danger");
            $.each(result, function (i, data) {
                $("#ddPatientName").append('<option value=' + data.memberID + '>' + data.memberName + '</option>');
                $("#ddPatientName").addClass("border-success");
            });
            $("#lblddPatientName").show();
            $("#divddPatientName").show();

        }
        else {
            sweetAlert("Alert", "Family members details not found!");
            $("#ddPatientName").empty().append('<option value="0">-Select-</option>').removeClass("border-success border-danger");
            $("#lblddPatientName").hide();
            $("#divddPatientName").hide();
            $("#tableid").hide();
            $("#example2").empty();
        }
    });
}

$("#ddPatientName").change(function () {
    var data = $("#ddPatientName").val();
    var param = { memberid: data, URN: $("#txtUrnNo").val() };
    $.ajax({
        url: ServiceURL + "/api/Report/GetKnowYourStatusReport",
        type: "POST",
        data: param,
        dataType: "json",
        //beforeSend: function () {
        //    $("#loader").show();
        //},
        success: function (resultBal) {
            console.log('knowyourstatus', resultBal);
            $("#example2").empty();
            if (resultBal.length > 0) {


                var AddParameter = "<tr class='parent'>" +
                    "<td>" + 1 + "</td>" +
                    "<td>" + resultBal[0].urn + "</td>" +
                    "<td><div class='text-success'>" + resultBal[0].memberid + "</div><div class='pre-bold'>" + resultBal[0].membername + "</div></td>" +
                    "<td>" + resultBal[0].patientgender + "</td>" +
                    "<td>" + resultBal[0].age + "</td>" +
                    "<td>" + resultBal[0].uidnumber + "</td>" +
                    "<td>" + resultBal[0].patientcontactnumber + "</td>" +
                    /* "<td>" + resultBal[i].admissionstatus + "</td>" +*/

                    "<td><button type='button' class='btn btn-outline-success focusedBtn btn-sm my-2 my-sm-0 detailsPanal show' data-toggle='collapse' data-target='#pkgmodalcls_" + slno + "'><i class='fa fa-eye'></i></button></td></tr>";
                AddParameter += " </tr>";

                AddParameter += "<tr class='child'>";
                AddParameter += "<td colspan='12' class='m-0 p-0 border-0'>";
                AddParameter += "<div class='accordian-body collapse show' id='pkgmodalcls_" + slno + "'>";
                AddParameter += "<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 mt-3'>";
                AddParameter += "<div class='headingpkg-bg align-items-center m-0'> Package Details </div>";

                AddParameter += "<div class='col-lg-12 p-0' id='table-scroll'>";
                AddParameter += "<table class='table table-striped tblImplView' id='tblImplantView1'>";
                AddParameter += "<thead>";
                AddParameter += "<tr>";
                AddParameter += "<th scope='bg-white text-align-center' style='background-color:#fcffa1 !important;'> Sl No</th>";
                AddParameter += "<th scope='bg-white text-align-center' style='background-color:#fcffa1 !important;;'> Hospital Name</th>";
                AddParameter += "<th scope='bg-white text-align-center' style='background-color:#fcffa1 !important;;'>Date of Admission</th>";
                AddParameter += "<th scope='bg-white text-align-center' style='background-color:#fcffa1 !important;;'>Package Details </th>";
                AddParameter += "<th scope='bg-white text-align-center' style='background-color:#fcffa1 !important;;'>Package Code</th>";
                AddParameter += "<th scope='bg-white text-align-center' style='background-color:#fcffa1 !important;;'>Admission Status</th>";
                AddParameter += "</tr>";
                AddParameter += "</thead>";
                AddParameter += "<tbody id=Implant_" + slno + ">";

                for (var i = 0; i < resultBal.length; i++) {
                    var slno = i + 1;
                    AddParameter += "<tr>";
                    AddParameter += "<td>" + slno + "</td>";
                    AddParameter += "<td>" + resultBal[i].hospitalname + "</td>";
                    AddParameter += "<td>" + resultBal[i].admissiondate + "</td>";
                    AddParameter += "<td>" + resultBal[i].packagename + "</td>";
                    AddParameter += "<td>" + resultBal[i].procedurecode + "</td>";
                    AddParameter += "<td>" + resultBal[i].admissionstatus + "</td>";
                    AddParameter += "</tr>";
                }
                AddParameter += "</tbody>";

                AddParameter += "</table>";
                AddParameter += "</div>";

                $("#example2").append(AddParameter);
                $("#tableid").show();
                // }

                //$("#example2").append(htm);
            }
            else {
                sweetAlert("Not Admitted");
                $("#example2").empty();
                $("#tableid").hide();


            }
        }


        // }
    });
});

function Showdetails(memberid) {
    console.log(memberid);
}
function inputLimiter(e, allow) {
    var AllowableCharacters = '';
    if (allow == 'Numbers') {
        AllowableCharacters = '1234567890';
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
    loadNavigation('KnowYourStatus', 'MtReports', 'pl', 'Reports', 'Know Your Status', '');

});
