$(function () {
    $('#UpdateScoresForWeek').on('click', function () {
        updateScores();
    })
})

function updateScores() {
    var urll = '/api/admin/UpdateScoresForWeek';

    toastr.success("Updating scores...");

    $.ajax({
        url: urll,
        method: 'POST',
        data: JSON.stringify($('#WeekSelect').val()),
        contentType: 'application/json',
        success: function (data) {
            toastr.success('Scores updated');
        },
        error: function (data) {
            showError("Error getting/updating scores", data);
        }
    })
}