
var baseUrl = '@Url.Action("ChangePassword", "Account")';
var urlEditInfo = '@Url.Action("EditUser", "Account")';
var myChart;
var baseUrl = "https://localhost:44368/";
$('#reset_pwd_btn').click(function () {
    $('#profile_ctn').load(baseUrl + "Account/ChangePassword");
})
$("#edit_info_btn").click(function () {
    $('#profile_ctn').load(baseUrl + "Account/EditUser");
})
$("#edit_email_btn").click(function () {
    $('#profile_ctn').load(baseUrl + "Account/ChangeEmail");
})
/*$("#chart_btn").click(function () {
    $('#profile_ctn').load(baseUrl + "Account/ProfileChart");
})*/
$('#chart_btn').click(function () {

    //Placeholder for input parameters
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
        console.log(response);
        PopulateData(response);
        $('#chart_ctn').modal('show');

    }).fail(function (error) {
        console.log(error);
    });
});
function PopulateData(data) {
    console.log(data);
    var ctx = document.getElementById("myChart");
    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    myChart = new Chart(ctx, {
        type: 'pie',
        data: data,
        options: {
            responsive: false,
            legend: {
                display: true,
                position: 'right',
                labels: {
                    fontSize:15,
                }
            },
        }
    });
};
$(".chart_ctg_btn").click(function () {
    //Placeholder for input parameters
    var data = {};
    var category = $(this).attr('id');
    console.log(category);
    $.ajax({
        url: baseUrl + 'Account/ProfileChartCategory',
        type: 'GET',
        dataType: 'Json',
        contentType: 'application/json; charset=utf-8',
        data: { categoryId: category},
    }).done(function (response) {
        console.log(response);
        PopulateDataLineChart(response);

    }).fail(function (error) {
        console.log(error);
    });
});

function PopulateDataLineChart(data) {
    console.log(data);
    var ctx = document.getElementById("myChart");
    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
     myChart = new Chart(ctx, {
        type: 'line',
        data: data,
        options: {

        }
    });

}
$(".total_chart_ctg_btn").click(function () {
    //Placeholder for input parameters
    var data = {};
    var category = $(this).attr('id');
    console.log(category);
    $.ajax({
        url: baseUrl + 'Account/BarChartCategory',
        type: 'GET',
        dataType: 'Json',
        contentType: 'application/json; charset=utf-8',
        data: { categoryId: category },
    }).done(function (response) {
        console.log(response);
        PopulateDataBarChart(response);

    }).fail(function (error) {
        console.log(error);
    });
});
function PopulateDataBarChart(data) {
    console.log(data);
    var ctx = document.getElementById("myChart");
    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }
    myChart = new Chart(ctx, {
        type: 'bar',
        data: data,
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}