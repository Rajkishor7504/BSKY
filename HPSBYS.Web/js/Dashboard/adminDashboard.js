
$(document).ready(function () {
    var d = new Date();
    var currentDate = d.getDate();
    var currMonth = d.getMonth();
    var currYear = d.getFullYear();
    var startDate = new Date(currYear, currMonth, currentDate);
    var endDate = new Date(currYear, currMonth, currentDate);
    $("#chartFromdt").val(moment(startDate).format("DD-MMM-YY"));
    $("#chartTodt").val(moment(endDate).format("DD-MMM-YY"));

    getStates();
    searchDashboardStats();
    $('#diplayExpiredNotic').hide();
    $('#diplayEmpanelmentNotic').hide();

    if ('@Session["moustatus"]' == 'true' && '@Session["empanelmentstatus_flag"]' == '1' && '@Session["is_block_active"]' == '0') {
        $('#diplayExpiredNotic').show();
        $('#diplayEmpanelmentNotic').show();
        $('#diplayNotic').hide();
    }
    else if ('@Session["moustatus"]' == 'true' && '@Session["empanelmentstatus_flag"]' == '0' && '@Session["is_block_active"]' == '0' ) {
        $('#diplayExpiredNotic').show();
        $('#diplayEmpanelmentNotic').hide();
        $('#diplayNotic').hide();
    }
    else if ('@Session["moustatus"]' == 'false' && '@Session["empanelmentstatus_flag"]' == '1' && '@Session["is_block_active"]' == '0') {
        $('#diplayExpiredNotic').hide();
        $('#diplayEmpanelmentNotic').show();
        $('#diplayNotic').hide();
    }
    else if ('@Session["mounoticeflag"]' == '1' && '@Session["moustatus"]' == 'false' && '@Session["mou_enddate"]' != null) {
        $('#diplayNotic').show();
    }
    else {
        $('#diplayNotic').hide();
        $('#NoticeDiv').hide();
    }
});

function searchDashboardStats() {
    var fromDate = new Date($('#chartFromdt').val());
    var toDate = new Date($('#chartTodt').val());
    if (fromDate > toDate) {
        swal({
            text: "From date should not be greater than To date",
            confirmButtonColor: "#36A865",
            type: "error",
            confirmButtonText: "OK",
            closeOnConfirm: false
        }).then(function () {
            $("#chartFromdt").focus();
        });
    }
    else {
        getDashboardStats();
    }
}

function getDashboardStats() {
    var param = {
        "FromDate": $('#chartFromdt').val(), "ToDate": $('#chartTodt').val(),
        "StateCode": $('#ddState').val(), "DistrictCode": $('#ddDistrict').val(), "HospitalCode": $('#ddHospital').val()
    };
    param = JSON.stringify(param);
    $.ajax({
        url: ServiceURL + "/api/Report/adminDashboardStats",
        type: "POST",
        data: param,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loader").show();
        },
        success: function (response) {
            bindPatientStats(response.patientStats);
            bindProcedureStats(response.procedureStats);
            bindPreAuthStats(response.preAuthStats);
            //bindOutboundcallStats(response.outboundCallStats);
            //bindRefAuthStats(response.referralAuthStats);
            bindAuthModeStats(response.authModeStats);
            bindOverrideCodeStats(response.overrideCodeStats)
        },
        complete: function () {
            $("#loader").hide();
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
}

function bindPatientStats(data) {
    console.log(data);
    $("#divPatBlocked").html(data.total_Admission);
    $("#divPatOngoing").html(data.total_Ongoing);
    $("#divPatDischarge").html(data.total_Discharge);

    var graphData = constructGraphData(data.patientStatsMonthwise);
    var patBlockedMonthwise = graphData[0];
    var patOngoingTreatmentMonthwise = graphData[1];
    var patDischargeMonthwise = graphData[2];

    Highcharts.chart('coloumn-bar', {
        colors: [ '#3D5656', '#68B984', '#FED049' ],
        chart: { type: 'column' },
        legend: { enabled: false },
        credits: { enabled: false },
        title: { text: '' },
        xAxis: {
            categories: [ 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec' ],
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: { text: '' }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: { pointPadding: 0.2, borderWidth: 0 }
        },
        series: [{
            name: 'Total Patient Blocked',
            data: patBlockedMonthwise //[25, 35, 45, 55, 65, 75, 30, 40, 50, 60, 70, 80 ]
        },
        {
            name: 'Ongoing Treatment',
            data: patOngoingTreatmentMonthwise //[83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3]
        },
        {
            name: 'Discharge',
            data: patDischargeMonthwise //[42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8, 51.1]
        }]
    });
}

function constructGraphData(data) {
    var mJan = data.find(o => $.trim(o.month).toUpperCase() == 'JANUARY');
    var mFeb = data.find(o => $.trim(o.month).toUpperCase() == 'FEBRUARY');
    var mMar = data.find(o => $.trim(o.month).toUpperCase() == 'MARCH');
    var mApr = data.find(o => $.trim(o.month).toUpperCase() == 'APRIL');
    var mMay = data.find(o => $.trim(o.month).toUpperCase() == 'MAY');
    var mJun = data.find(o => $.trim(o.month).toUpperCase() == 'JUNE');
    var mJul = data.find(o => $.trim(o.month).toUpperCase() == 'JULY');
    var mAug = data.find(o => $.trim(o.month).toUpperCase() == 'AUGUST');
    var mSep = data.find(o => $.trim(o.month).toUpperCase() == 'SEPTEMBER');
    var mOct = data.find(o => $.trim(o.month).toUpperCase() == 'OCTOBER');
    var mNov = data.find(o => $.trim(o.month).toUpperCase() == 'NOVEMBER');
    var mDec = data.find(o => $.trim(o.month).toUpperCase() == 'DECEMBER');

    var patBlockedArr = [
        mJan == null ? 0 : parseInt(mJan.total_Admission_Monthwise), mFeb == null ? 0 : parseInt(mFeb.total_Admission_Monthwise),
        mMar == null ? 0 : parseInt(mMar.total_Admission_Monthwise), mApr == null ? 0 : parseInt(mApr.total_Admission_Monthwise),
        mMay == null ? 0 : parseInt(mMay.total_Admission_Monthwise), mJun == null ? 0 : parseInt(mJun.total_Admission_Monthwise),
        mJul == null ? 0 : parseInt(mJul.total_Admission_Monthwise), mAug == null ? 0 : parseInt(mAug.total_Admission_Monthwise),
        mSep == null ? 0 : parseInt(mSep.total_Admission_Monthwise), mOct == null ? 0 : parseInt(mOct.total_Admission_Monthwise),
        mNov == null ? 0 : parseInt(mNov.total_Admission_Monthwise), mDec == null ? 0 : parseInt(mDec.total_Admission_Monthwise),
    ];
    var patOngoingArr = [
        mJan == null ? 0 : parseInt(mJan.total_Ongoing_Monthwise), mFeb == null ? 0 : parseInt(mFeb.total_Ongoing_Monthwise),
        mMar == null ? 0 : parseInt(mMar.total_Ongoing_Monthwise), mApr == null ? 0 : parseInt(mApr.total_Ongoing_Monthwise),
        mMay == null ? 0 : parseInt(mMay.total_Ongoing_Monthwise), mJun == null ? 0 : parseInt(mJun.total_Ongoing_Monthwise),
        mJul == null ? 0 : parseInt(mJul.total_Ongoing_Monthwise), mAug == null ? 0 : parseInt(mAug.total_Ongoing_Monthwise),
        mSep == null ? 0 : parseInt(mSep.total_Ongoing_Monthwise), mOct == null ? 0 : parseInt(mOct.total_Ongoing_Monthwise),
        mNov == null ? 0 : parseInt(mNov.total_Ongoing_Monthwise), mDec == null ? 0 : parseInt(mDec.total_Ongoing_Monthwise),
    ];
    var patDischargeArr = [
        mJan == null ? 0 : parseInt(mJan.total_Discharge_Monthwise), mFeb == null ? 0 : parseInt(mFeb.total_Discharge_Monthwise),
        mMar == null ? 0 : parseInt(mMar.total_Discharge_Monthwise), mApr == null ? 0 : parseInt(mApr.total_Discharge_Monthwise),
        mMay == null ? 0 : parseInt(mMay.total_Discharge_Monthwise), mJun == null ? 0 : parseInt(mJun.total_Discharge_Monthwise),
        mJul == null ? 0 : parseInt(mJul.total_Discharge_Monthwise), mAug == null ? 0 : parseInt(mAug.total_Discharge_Monthwise),
        mSep == null ? 0 : parseInt(mSep.total_Discharge_Monthwise), mOct == null ? 0 : parseInt(mOct.total_Discharge_Monthwise),
        mNov == null ? 0 : parseInt(mNov.total_Discharge_Monthwise), mDec == null ? 0 : parseInt(mDec.total_Discharge_Monthwise),
    ];

    var patGraphArr = [patBlockedArr, patOngoingArr, patDischargeArr];
    return patGraphArr;
}

function bindProcedureStats(data) {
    $("#divProcBlocked").html(data.total_Block == null ? 0 : data.total_Block);
    $("#divProcUnblocked").html(data.total_Unblock == null ? 0 : data.total_Unblock);
    $("#divProcDischarge").html(data.total_Discharge == null ? 0 : data.total_Discharge);

    var ProcTotal = parseInt(data.total_Block) + parseInt(data.total_Unblock)
        + parseInt(data.total_Discharge);
    var ProcBlockedPerc = (parseInt(data.total_Block) / ProcTotal) * 100;
    var ProcUnblockedPerc = (parseInt(data.total_Unblock) / ProcTotal) * 100;
    var ProcDischargePerc = (parseInt(data.total_Discharge) / ProcTotal) * 100;

    Highcharts.chart('pie', {
        colors: ['#f2bd56 ', '#6084b0', '#68B984'],
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie',
            margin: 0,
            width: 250,
            height: 200
        },
        credits: {
            enabled: false,
        },
        title: {
            text: '',
            align: 'center',
            verticalAlign: 'middle'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>{point.percentage:.1f} %',
                    distance: -50,
                    color: '#fff',
                    filter: {
                        property: 'percentage',
                        operator: '>',
                        value: 3
                    }
                }
            }
        },
        series: [{
            name: '',
            data: [
                { name: '', y: ProcDischargePerc },
                { name: '', y: ProcBlockedPerc },
                { name: '', y: ProcUnblockedPerc }
            ]
        }]
    });
}

function bindPreAuthStats(data) {
    $("#lblPreAuthResponse").html(data.total_Response == null ? 0 : data.total_Response);
    $("#lblPreAuthApprovedAmt").html('&#8377;' + data.total_Approved_Amount);
    $("#divPreAuthRejectCase").html(data.total_Reject == null ? 0 : data.total_Reject);
    $("#divPreAuthApproved").html(data.total_Approve == null ? 0 : data.total_Approve);
    $("#divPreAuthAutoApproved").html(data.total_AutoApprove == null ? 0 : data.total_AutoApprove);
    $("#spnPreAuthBlocked").html("<b>" + data.total_Blocked + '</b>');
    $("#spnPreAuthPending").html("<b>" + data.total_Pending + '</b>');
    $("#spnPreAuthExpired").html("<b>" + data.total_Expired + '</b>');
    $("#lblPreAuthResponseAwaited").html(data.response_Awaited == null ? 0 : data.response_Awaited);
    $("#lblPreAuthPendingAmount > span").html('&#8377;' + data.pending_Amount);
    $("#divPreAuthFreshCase").html(data.fresh_Cases == null ? 0 : data.fresh_Cases);
    $("#divPreAuthRevertCase").html(data.reverted_Cases == null ? 0 : data.reverted_Cases);

    var preAuthTotal = parseInt(data.total_Blocked) + parseInt(data.total_Pending)
        + parseInt(data.total_Expired);
    var preAuthBlockedPerc = (parseInt(data.total_Blocked) / preAuthTotal) * 100;
    var preAuthPendingPerc = (parseInt(data.total_Pending) / preAuthTotal) * 100;
    var preAuthExpiredPerc = (parseInt(data.total_Expired) / preAuthTotal) * 100;
    $("#divPreAuthBlockedProgress").css("width", (preAuthBlockedPerc + "%"));
    $("#divPreAuthPendingProgress").css("width", (preAuthPendingPerc + "%"));
    $("#divPreAuthExpiredProgress").css("width", (preAuthExpiredPerc + "%"));

    var preAuthCaseTotal = parseInt(data.fresh_Cases) + parseInt(data.reverted_Cases);
    var preAuthFreshCasePerc = (parseInt(data.fresh_Cases) / preAuthCaseTotal) * 100;
    var preAuthRevertedCasePerc = (parseInt(data.reverted_Cases) / preAuthCaseTotal) * 100;

    //donut circle chart
    // Create the chart
    chart = new Highcharts.Chart({
        colors: [
            '#FF7070', '#FED049'
        ],
        chart: {
            renderTo: 'donut-circle',
            type: 'pie',
            width: 150,
            height: 130
        },
        legend: { enabled: false },
        credits: {
            enabled: false,
        },
        title: {
            text: ''
        },
        yAxis: {
            title: {
                text: ''
            }
        },
        plotOptions: {
            pie: {
                shadow: false,
                point: {
                    events: {
                        distance: 0,
                        mouseOver: function (e) {
                            this.originalRadius = this.graphic.r;
                            this.graphic.animate({
                                r: this.originalRadius * 1.07
                            }, 200);
                        },
                        mouseOut: function (e) {
                            this.graphic.animate({
                                r: this.originalRadius
                            }, 200);
                        }
                    }
                }
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.point.name + '</b>: ' + this.y + ' %';
            }
        },
        series: [{
            name: 'Data',
            data: [
                ["Reverted", preAuthRevertedCasePerc],
                ["Fresh", preAuthFreshCasePerc]
            ],
            size: '100%',
            innerSize: '80%',
            showInLegend: true,
            dataLabels: {
                enabled: false
            },
            states: {
                hover: {
                    halo: false
                }
            },
        }]
    });
    var circleradius = chart;
    //console.log(chart);
    var test = chart.options.series[0].innerSize * chart.Height
    //console.log(test);
    // Render the circle
    //donut circle chart -->
}

function bindRefAuthStats(data) {
    $("#lblRefAuthActionByDc").html(data.dc_Action);
    $("#divRefAuthPerc").html(data.auth_Percent + '%');
    $("#divRefAuthAuthenticate").html(data.authenticate);
    $("#divRefAuthNotAuthenticate").html(data.not_Authenticate);
    $("#divRefAuthAutoAuthenticate").html(data.auto_Authenticate);
    $("#divRefAuthProgress").css("width", (data.auth_Percent + "%"));
}

function bindAuthModeStats(data) {
    $("#divAuthModePos").html(data.pos == null ? 0 : data.pos);
    $("#divAuthModeIris").html(data.iris == null ? 0 : data.iris);
    $("#divAuthModeOtp").html(data.otp == null ? 0 : data.otp);
    $("#divAuthModeOverride").html(data.override == null ? 0 : data.override);
}

function bindOverrideCodeStats(data) {
    $("#divOvdTotalRequest").html(data.total_Request == null ? 0 : data.total_Request);
    $("#divOvdTotalApprove").html(data.total_Approve == null ? 0 : data.total_Approve);
    $("#divOvdTotalReject").html(data.total_Reject == null ? 0 : data.total_Reject);
    $("#divOvdTotalPending").html(data.total_Pending == null ? 0 : data.total_Pending);
}

function bindOutboundcallStats(data) {
    $("#lblObcTotCallConn").html(data.totalCallConnected == null ? 0 : data.totalCallConnected);
    $("#divObcYes").html(data.yes == null ? 0 : data.yes);
    $("#divObcNo").html(data.no == null ? 0 : data.no);
    $("#spnObcYesPerc").html((data.per_Yes == null ? 0 : data.per_Yes) + '%');
    $("#spnObcNoPerc").html((data.per_Yes == null ? 0 : data.per_No) + '%');
    $("#lblObcPositiveRes").html((data.per_Yes == null ? 0 : data.per_Positive) + '%');
    $("#lblObcNegetiveRes").html((data.per_Yes == null ? 0 : data.per_Negetive) + '%');
    $("#divObcSupport").html('<b>' + (data.support == null ? 0 : data.support) + '</b>');
    $("#divObcBehaviour").html('<b>' + (data.behaviour == null ? 0 : data.behaviour) + '</b>');
    $("#divObcBribe").html('<b>' + (data.bribe == null ? 0 : data.bribe) + '</b>');
    $("#lblObcTotCallNotCon").html(data.totalCallNotConnected == null ? 0 : data.totalCallNotConnected);
    $("#divObcFreshCase").html(data.freshCase == null ? 0 : data.freshCase);
    $("#divObcNotConnected").html(data.notConnected == null ? 0 : data.notConnected);
    $("#spnObcFreshCasePerc").html((data.per_FreshCase == null ? 0 : data.per_FreshCase) + '%');
    $("#spnObcNotConnectedPerc").html((data.per_NotConnected == null ? 0 : data.per_NotConnected) + '%');

    $("#divObcYesProgress").css("width", (data.per_Yes + "%"));
    $("#divObcNoProgress").css("width", (data.per_No + "%"));
    $("#divObcFreshCaseProgress").css("width", (data.per_FreshCase + "%"));
    $("#divObcNotConnectedProgress").css("width", (data.per_NotConnected + "%"));

    var obcResponeTotal = parseInt(data.support) + parseInt(data.behaviour) + parseInt(data.bribe);
    var obcSupport = (parseInt(data.support) / obcResponeTotal) * 100;
    var obcBehaviour = (parseInt(data.behaviour) / obcResponeTotal) * 100;
    var obcBribe = (parseInt(data.bribe) / obcResponeTotal) * 100;

    // Build the chart
    Highcharts.chart('pie2', {
        colors: ['#f2bd56', '#1c4f8e', '#36a865'],
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie',
            margin: 0,
            width: 140,
            height: 120
        },
        credits: {
            enabled: false,
        },
        title: {
            text: '',
            align: 'center',
            verticalAlign: 'middle'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false,
                    format: '<b>{point.name}</b>{point.percentage:.1f} %',
                    distance: -50,
                    filter: {
                        property: 'percentage',
                        operator: '>',
                        value: 3
                    }
                }
            }
        },
        series: [{
            name: '',
            data: [
                { name: 'Support', y: obcSupport },
                { name: 'Behaviour', y: obcBehaviour },
                { name: 'Bribe', y: obcBribe }
            ]
        }]
    });
}

// Pie Chart Start
var pieColors = (function () {
    var colors = [],
        base = Highcharts.getOptions().colors[0],
        i;

    for (i = 0; i < 10; i += 1) {
        // Start out with a darkened base color (negative brighten), and end
        // up with a much brighter color
        colors.push(Highcharts.color(base).brighten((i - 3) / 7).get());
    }
    return colors;
}());

var pieColors = (function () {
    var colors = [],
        base = Highcharts.getOptions().colors[0], i;
    for (i = 0; i < 10; i += 1) {
        // Start out with a darkened base color (negative brighten), and end
        // up with a much brighter color
        colors.push(Highcharts.color(base).brighten((i - 3) / 7).get());
    }
    return colors;
}());

// Pie Chart End

// Auto Marquee Start
(function ($) {
    $.fn.autoscroll = function (options) {
        var settings = $.extend({}, $.fn.autoscroll.defaults, options);
        return this.each(function () {
            var $this = $(this);
            if ($this.length > 0 &&
                $this[0].scrollHeight > $this[0].clientHeight) {
                var scrollTimer,
                    scrollTop = 0;

                function scrollList() {
                    var itemHeight = $this.children().eq(1).outerHeight(true);
                    scrollTop++;
                    if (scrollTop >= itemHeight) {
                        $this.scrollTop(0).children().eq(0).appendTo($this);
                        scrollTop = 0;
                    } else {
                        $this.scrollTop(scrollTop);
                    }
                }

                $this.hover(function () {
                    clearInterval(scrollTimer);
                    $this.css("overflow-y", "auto");
                    if (settings.hideScrollbar) {
                        $this.addClass("hide-scrollbar");
                    }
                    if ($.type(settings.handlerIn) === "function") {
                        settings.handlerIn();
                    }
                }, function () {
                    $this.css("overflow-y", "hidden");
                    scrollTimer = setInterval(function () {
                        scrollList();
                    }, settings.interval);
                    if ($.type(settings.handlerOut) === "function") {
                        settings.handlerOut();
                    }
                }).trigger("mouseleave");
            }
        });
    }
    $.fn.autoscroll.defaults = {
        interval: 25,
        hideScrollbar: true,
        handlerIn: null,
        handlerOut: null

    };
    $(function () {
        $("[data-autoscroll]").autoscroll();
    });
})(jQuery);


// Body content Ends here
// Dashboard animate script

$(document).ready(function () {
    var i = 1;
    var class_length = $(".dash_sec").length;
    setInterval(function () {
        if (i <= class_length) {
            $(".dash_sec").removeClass("zoombox");
            $("#zoom_" + i).addClass("zoombox");
        } else {
            $(".dash_sec").removeClass("zoombox");
            $("#zoom_" + i).addClass("zoombox");
            i = 0;
        }
        //  $("#zoom_"+i).addClass("  ");
        i++;
    }, 2200);
})

// Sticky icon hover set

$(".sticky-icon a").mouseenter(function () {
    $(".sticky-icon").css("z-index", "2");
}).mouseleave(function () {
    $(".sticky-icon").css("z-index", "inherit");
});

// Sticky icon hover set