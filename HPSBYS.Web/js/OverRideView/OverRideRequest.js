

   $(document).ready(function () {
       $('#RemoveoverrideDoc').hide();
        $('#path').html('Block Override Request');

        $('#DvOverrideRequest').hide();

       $('#btnSearch').click(function () {
           if ($('#txtUrnNo').val() == '') {

               swal({
                   text: "Please enter NFSA/SFSS NO !!",
                   confirmButtonColor: "#36A865",
                   type: "error",
                   confirmButtonText: "OK",
                   closeOnConfirm: false
               }).then((result) => {
                   $('#txtUrnNo').focus();
               });


           }
           else {
               bindgrdOverrideRequest();
           }
            //if (validate('SRH')) {

        //}
    });

    $("#rdblockpackage").click(function () {
        $("#rdunblockpackage").prop("checked", false)
           $("#rddischarge").prop("checked", false)

       });
    $("#rdunblockpackage").click(function () {
        $("#rdblockpackage").prop("checked", false)
           $("#rddischarge").prop("checked", false)

       });
    $("#rddischarge").click(function () {
        $("#rdblockpackage").prop("checked", false)
           $("#rdunblockpackage").prop("checked", false)

       });

    $("#grdOverrideRequest").on('click', '.btnSelect', function () {

            // get the current row

            // //debugger; console.log(currentRow);

            var currentRow = $(this).closest("tr");

    var patientId = currentRow.find("td:eq(3)").text();

    var patientName = currentRow.find("td:eq(2)").text();

    var patientGender = currentRow.find("td:eq(5)").text();

    var patientAge = currentRow.find("td:eq(6)").text();

    var patientAadharNo = currentRow.find("td:eq(4)").text();

    var patientUrnNO = $("#hdnUrnNo").val();

    $("#lblPatientMemberId").html(patientId);

    $("#lblPatientName").html(patientName);

    $("#lblPatientGender").html(patientGender);

    $("#lblPatientAge").html(patientAge);

    $("#lblPatientAadharNo").html(patientAadharNo);

    $("#lblPatientUrnNo").html(patientUrnNO);

        });

    });

    function RemoveoverrideDoc(ele) {
        $("#uploadfile").val("");
    $('#RemoveoverrideDoc').hide();
    }
    $("#uploadfile").on("change", function (e) {
        var fileExtension = ['pdf', 'jpeg', 'jpg'];
    var filename = $('#uploadfile').val();
    var extension = filename.replace(/^.*\./, '');
    if ($.inArray(extension.toLowerCase(), fileExtension) == -1 && filename.length != 0) {
        swal({
            text: "Please upload a valid file of type pdf, jpeg, jpg !!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#uploadfile").focus();
            $("#uploadfile").val("");
        });
    return false;
        }
    var fileSize = $("#uploadfile")[0].files[0].size;
        if (fileSize > 5000141) {
        swal({
            text: "File size cannot be greater than 5MB !!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#uploadfile").focus();
            $("#uploadfile").val("");
        });
    return false;
        }
    else {
        $("#RemoveoverrideDoc").show();
        }
    });

    function OverrideRequestPop() {
        $("#MyPopupBlock").modal("show");
    }
    function bindgrdOverrideRequest() {

        $('#grdOverrideRequest').jsGrid({
            width: "100%",
            sorting: true,
            paging: true,
            pageSize: 10,
            autoload: true,
            controller: {
                loadData: function () {
                    var d = $.Deferred();
                    var params = {
                        URN: $('#txtUrnNo').val()
                    };
                    return $.ajax({
                        beforeSend: function () {
                            $("#loader").show();
                        },
                        url: ServiceURL + "/api/URN/GetFamilyMemeberList",
                        type: "GET",
                        data: params,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        error: function (error) {
                            console.log(error.statusText);
                        },
                        complete: function () {
                            $("#loader").hide();
                        },
                    }).done(function (result) {
                        $('#DvOverrideRequest').show();
                        if (result.length > 0) {
                            $("#hdnUrnNo").val(result[0].urn);
                            d.resolve($.map(result, function (item, itemIndex) {
                                return $.extend(item, { "Index": itemIndex + 1 });
                            }));
                        }
                        else {
                            $("#hdnUrnNo").val('');
                        }
                    });
                }
            },

            fields: [
                { name: "Index", title: "Sl No.", type: "number", width: "50", align: "right" },
                { name: "urn", title: "URN", type: "text", align: "right" },
                { name: "memberName", title: "Member Name", type: "text", align: "right" },
                { name: "memberID", title: "Member Id", type: "text", align: "right" },
                { name: "maskAadharnumber", title: "Aadhaar Card No", type: "text", align: "right" },
                { name: "memberGender", title: "Gender", type: "text", align: "right" },
                { name: "memberAge", title: "Age", type: "text", align: "right" },

                {
                    title: "Select Patient", type: "control", editButton: false, deleteButton: false, align: "center",
                    itemTemplate: function (value, item) {
                        return $("<button>").attr({ type: "button", class: "btn btn-outline-success btnSelect btn-sm my-2 my-sm-0", onclick: "OverrideRequestPop();" }).text("Select")
                            .on("click", function () {
                                var $row = $("#grdOverrideRequest").jsGrid("rowByItem", item);
                            });
                    },
                    headerTemplate: function () {
                        return $("<div>").prop("title", "Select Patient").text("Action");
                    }
                }
            ]
        });
    }

function submitOverrideRequestForm() {
    var hospitalcode = document.mynamespace.hospitalcode;
    var hospitalcategoryId = document.mynamespace.hospitalcategoryId;
    var MotalityDocUpload = $("#uploadfile").get(0);
    var Motalityfiles = MotalityDocUpload.files;
    var fileExtension = ['pdf', 'jpeg', 'jpg'];
    var filename = $('#uploadfile').val();
    var extension = filename.replace(/^.*\./, '');
    if (filename.length != 0) {
            var File = $("#uploadfile")[0].files[0].size;
        }
    if ($('#txtNoofdays').val() == '' || $('#txtNoofdays').val() == 0) {
        swal({
            text: "Please Enter No. Of Days. !!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then((result) => {
            $('#txtNoofdays').focus();
        });
        }
    else if (!$("#rdblockpackage").is(':checked') && !$("#rdunblockpackage").is(':checked') && !$("#rddischarge").is(':checked')) {
        swal({
            text: "Please Select Purpose of Override code  !!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        });
        }
        else if ($.inArray(extension.toLowerCase(), fileExtension) == -1 && filename.length != 0) {
        swal({
            text: "Please upload a valid file of type pdf, docx, jpeg, jpg !!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then((result) => {
            $('#uploadfile').focus();
        });
           // return sweetAlert('Alert', "Please upload a valid file of type pdf, docx, jpeg, jpg !!")
        }
        else if (File > 5242880) {
        swal({
            text: "File size cannot be greater than 5MB !!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then((result) => {
            $('#uploadfile').focus();
        });
            //return sweetAlert('Alert', "File size cannot be greater than 5MB !!")
        }
    else if ($('#txtDescription12').val() === "") {
        swal({
            text: "Please Enter Description !!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then((result) => {
            $('#txtDescription12').focus();
        });
            //return sweetAlert('Alert', 'Please Enter Description !!')
        }
    else {
        swal({
            title: "Are you sure?",
            text: " To generate the override request!",
            type: "warning",
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonColor: "#36A865",
            confirmButtonText: 'Yes'
        }).then(function (isConfirm) {
            if (isConfirm.value) {
                $('#btnSubmit').prop('disabled', true);
                var OverrideRequestData = new FormData();
                OverrideRequestData.append("MemberId", $('#lblPatientMemberId').text());
                OverrideRequestData.append("URN", $('#lblPatientUrnNo').text());
                OverrideRequestData.append("NoofDays", $('#txtNoofdays').val());
                if (Motalityfiles.length > 0) { OverrideRequestData.append("UploadDoc", Motalityfiles[0]); }
                if (Motalityfiles.length > 0) { OverrideRequestData.append("UploadDoc1", Motalityfiles[0].name); }

                OverrideRequestData.append("Description", $('#txtDescription12').val());
                if ($("#rdblockpackage").is(':checked')) {
                    OverrideRequestData.append("GENERATEDTHROUGH", $('#rdblockpackage').val());
                }
                else if ($("#rdunblockpackage").is(':checked')) {
                    OverrideRequestData.append("GENERATEDTHROUGH", $('#rdunblockpackage').val());
                }
                else {
                    OverrideRequestData.append("GENERATEDTHROUGH", $('#rddischarge').val());
                }
                OverrideRequestData.append("HospitalCode", hospitalcode,);
                OverrideRequestData.append("HospitalCategoryId", hospitalcategoryId);
    OverrideRequestData.append("ActionCode", "A");
    $.ajax({
    url: "GenerateOverrideRequest",//'@Url.Action("GenerateOverrideRequest", "BlockPackage")',
    type: "POST",
    data: OverrideRequestData,
    processData: false,
    contentType: false,
    beforeSend: function () {
        $("#loader").show();
                        },
    success: function (data) {

                            if (data[0].output == 2) {
        swal({
            title: "Override Request generated successfully!!",
            text: "Click ok to redirect override view page.",
            icon: "success",
            type: "success",
            confirmButtonColor: "#36A865",
            confirmButtonText: "Ok"
        })
            .then((result) => {
                if (result.value) {
                    window.location = "OverrideView";//'@Url.Content("~/BlockPackage/OverrideView")';
                } else if (result.dismiss === 'cancel') {
                    swal(
                        'Cancelled',
                        'Your stay here :)',
                        'error'
                    )
                }
            });;
                            }
    else if (data[0].output == 3) {
        swal({
            text: "Override Request Has Already Generated, New Request Not Allowed.!!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        })
        //swal('Alert', 'Override Request Has Already Generated, New Request Not Allowed. !!')
    }
    else {
    }
                        },
    complete: function () {
        $("#loader").hide();
                        },
    error: function (error) {
        console.log(error.statusText);
                        }
                    });
                }
            });
        }
    }



    function validate(mode) {
        if (mode == "SRH") {

            if ($('#txtUrnNo').val() == '') {

        swal({
            text: "Please enter NFSA/SFSS NO !!",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then((result) => {
            $('#txtUrnNo').focus();
        });

                
            }

    return true;

        }

    if (mode == "ORF") {
            if (!$("#rdblockpackage").is(':checked') && !$("#rdunblockpackage").is(':checked') && !$("#rddischarge").is(':checked')) {
        sweetAlert('Alert', 'Please Select Purpose of Override code  !!')
                return false;
            }
    if ($('#txtNoofdays').val() == '') {

        sweetAlert('Alert', 'Please Enter No. Of Days. !!');

    $('#txtNoofdays').focus();

    return false;

            }

    if ($('#txtDescription').val() == '') {

        sweetAlert('Alert', 'Please Enter Description !!')

                $('#txtDescription').focus();

    return false;

            }



    return true;

        }

    return false;

    }



    //function gettoken() {

    //    var token = '@Html.AntiForgeryToken()';

    //token = $(token).val();

    //return token;

    //}



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
            //loadNavigation('', 'Mlblockpackage', 'pl');
            loadNavigation('OverrideRequest', 'MtOverride', 'pl', 'Block Package', 'Override Request', '');
    });
   