$(function () {
    getLeaderboard();
});

function getLeaderboard() {
    let urll = '/api/web/GetLeaderboard';

    $('#UserStandingResult').text('');
    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            $('#UserStandingResult').text(data.UserStanding);
            fillLeaderboard(data.Scores);
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

        newRow.find('.view-lineup-btn').on('click', function (e) {
            viewLineup(rank.UserId, rank.UserFullName);
        });

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

function viewLineup(userId, fullName) {
    let urll = '/Home/ViewLineup?UserId=' + userId;

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            $('#UserFullName').text(fullName);
            $('#ViewLineupModalBody').html(data);
            $('#ViewLineupModal').modal('show');
        },
        error: function (data) {
            showError("Error getting lineup", data);
        }
    })
}