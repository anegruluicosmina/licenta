$(document).ready(function () {
    $("#chart_btn").css("border-bottom", "2px solid #184D68");
    var data = {};
    $.ajax({
        url: baseUrl + 'Account/ProfileChart',
        type: 'POST',
        dataType: 'Json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data)
    }).done(function (response) {
        PopulateData(response);
        $('#chart_ctn').modal('show');

    }).fail(function (error) {
        console.log(error);
    });
});

var baseUrl = '@Url.Action("ChangePassword", "Account")';
var urlEditInfo = '@Url.Action("EditUser", "Account")';
var myChart;
var baseUrl = "https://localhost:44368/";

$('#reset_pwd_btn').click(function () {
    $("#chart_ctn").hide();
    $(".test_menu").hide();
    $("#profile_form_ctn").show();
    $('#profile_form_ctn').load(baseUrl + "Account/ChangePassword");
})
$("#edit_info_btn").click(function () {
    $("#chart_ctn").hide();
    $(".test_menu").hide();
    $("#profile_form_ctn").show();
    $('#profile_form_ctn').load(baseUrl + "Account/EditUser");
})
$("#edit_email_btn").click(function () {
    $("#chart_ctn").hide();
    $(".test_menu").hide();
    $("#profile_form_ctn").show();
    $('#profile_form_ctn').load(baseUrl + "Account/ChangeEmail");
})

$("#test_btn").click(function () {
    $(".test_menu").show();
    $("#chart_ctn").show();
    $("#profile_form_ctn").hide();

});

$('#chart_btn').click(function () {
    $('#chart_btn').css("border", "none");
    $(".total_chart_ctg_btn").css("border", "none");
    $(".chart_ctg_btn").css("border", "none");
    $(this).css("border-bottom", "2px solid #184D68");
    if ($(window).width() < 750) {
        $(".test_menu").hide();
    }
    var data = {};
    //Get chart data
    $.ajax({
        url: baseUrl + 'Account/ProfileChart',
        type: 'POST',
        dataType: 'Json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data)
    }).done(function (response) {
        //Populate chart data and show the modal
        PopulateData(response);
        $('#chart_ctn').modal('show');

    }).fail(function (error) {
        console.log(error);
    });
});
function PopulateData(data) {
    var ctx = document.getElementById("myChart");
    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    myChart = new Chart(ctx, {
        type: 'pie',
        data: data,
        options: {
            title: {
                display: true,
                text: 'Rezultatele totale'
            },
            legend: {
                display: true,
                position: 'bottom',
                labels: {
                    fontSize:15,
                }
            },
            responsive: true,
            maintainAspectRatio: false,
        }
    });
};
$(".chart_ctg_btn").click(function () {
    $('#chart_btn').css("border", "none");
    $(".total_chart_ctg_btn").css("border", "none");
    $(".chart_ctg_btn").css("border", "none");
    $(this).css("border-bottom", "2px solid #184D68");
    if ($(window).width() < 750) {
        $(".test_menu").hide();
    }
    var data = {};
    var category = $(this).attr('id');
    $.ajax({
        url: baseUrl + 'Account/ProfileChartCategory',
        type: 'GET',
        dataType: 'Json',
        contentType: 'application/json; charset=utf-8',
        data: { categoryId: category},
    }).done(function (response) {
        PopulateDataLineChart(response);

    }).fail(function (error) {
        console.log(error);
    });
});

function PopulateDataLineChart(data) {
    var ctx = document.getElementById("myChart");
    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
     myChart = new Chart(ctx, {
        type: 'line',
        data: data,
         options: {
             title: {
                 display: true,
                 text: 'Evolutia liniara a progresului'
             },
            responsive: true,
            maintainAspectRatio: false,
        }
    });

}
$(".total_chart_ctg_btn").click(function () {
    $('#chart_btn').css("border", "none");
    $(".chart_ctg_btn").css("border", "none");
    $(".total_chart_ctg_btn").css("border", "none");
    $(this).css("border-bottom", "2px solid #184D68");
    var windowWidth = $(window).width();
    if ($(window).width() < 750) {
        $(".test_menu").hide();
    }
    var data = {};
    var category = $(this).attr('id');
    $.ajax({
        url: baseUrl + 'Account/BarChartCategory',
        type: 'GET',
        dataType: 'Json',
        contentType: 'application/json; charset=utf-8',
        data: { categoryId: category },
    }).done(function (response) {
        PopulateDataBarChart(response);

    }).fail(function (error) {
    });
});
function PopulateDataBarChart(data) {
    var ctx = document.getElementById("myChart");
    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    myChart = new Chart(ctx, {
        type: 'bar',
        data: data,
        options: {
            title: {
                display: true,
                text: 'Rezultat totale pentru aceasta categorie'
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            legend: {
                display: false
            },
            tooltips: {
                enabled: false
            },
            responsive: true,
            maintainAspectRatio: false,
        }
    });
}
