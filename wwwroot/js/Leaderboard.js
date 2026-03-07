$(function () {
    getLeaderboard();
});

function getLeaderboard() {
    let urll = '/api/web/GetLeaderboard';

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            fillLeaderboard(data);
        },
        error: function (data) {
            showError("Error Getting Leaderboard Info", data);
        }
    })
}

function fillLeaderboard(rankings) {
    let tableBody = $('#LeaderboardTableBody');
    let template = $('#LeaderboardRowTemplate').contents();

    tableBody.empty();

    rankings.forEach((rank, index) => {
        let newRow = template.clone(false);

        newRow.find('.rank').text(rank.Rank);
        newRow.find('.user-full-name').text(rank.UserFullName);
        newRow.find('.total-score').text(rank.TotalScore);

        if (index == 0) {
            newRow.addClass('gold-background');
        } else if (index == 1) {
            newRow.addClass('silver-background');
        } else if (index == 2) {
            newRow.addClass('bronze-background');
        }

        tableBody.append(newRow);
    })    
}