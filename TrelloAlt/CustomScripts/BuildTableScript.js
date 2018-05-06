$(document).ready(function () {
    $.ajax({
        url: '/ToDoActModels/BuildDashboardTable',
        success: function(result) {
            $('#tableDivElement').html(result);
        }
    });
});