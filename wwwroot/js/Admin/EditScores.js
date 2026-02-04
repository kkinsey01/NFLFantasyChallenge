$(function () {
    getDropDownInfo();

    $('#SearchPlayerListBtn').on('click', function (e) {
        e.preventDefault();

        getPlayerList();
    });

    $('#SaveChangesButton').on('click', function (e) {
        e.preventDefault();

        updatePlayerScores();
    })

    $('#SetScoresToZero').on('click', function (e) {
        e.preventDefault();

        setScoresToZero();
    })

    $('#BottomControls').hide();
});

let playerIds = [];

function getDropDownInfo() {
    let urll = '/api/admin/GetEditScoresInitial';

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            fillInDropdownInfo(data);
        },
        error: function (data) {
            showError("Error Getting Dropdown Data", data);
        }
    })
}

function fillInDropdownInfo(data) {
    let dropdownTemplate = $('#DropdownTemplate').clone(false);

    data.Teams.forEach(team => {
        let $option = $('<option>', {
            value: team,
            text: team,
            class: 'p-2 in-bg'
        });

        $('#EditPlayerTeamSelector').append($option);
    })

    data.Weeks.forEach(week => {
        let $option = $('<option>', {
            value: week,
            text: week,
            class: 'p-2 in-bg'
        });

        $('#EditPlayerWeekSelector').append($option);
    })
}

function getPlayerList() {
    let team = $('#EditPlayerTeamSelector').val();
    let week = $('#EditPlayerWeekSelector').val();    

    let dto = {
        Team: team,
        Week: week
    };

    var urll = '/api/admin/GetEditScoresPlayerList' + formatUrlParams(dto);

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            fillPlayerList(data);
            $('#BottomControls').show();
        },
        error: function (data) {
            showError("Error getting player list", data);
        }
    });
}

function fillPlayerList(positionGroup) {
    playerIds = [];

    let positionTemplate = $('#PositionTemplate').clone(false).contents();
    let playerContainerTemplate = $('#PlayerContainerTemplate').clone(false).contents();
    let playerEntryTemplate = $('#PlayerEntryTemplate').clone(false).contents();    

    let container = $('#PlayerBody');
    positionGroup.forEach(position => {
        const wrapper = $('<div></div>');

        let newPosition = positionTemplate.clone(true);
        newPosition.find('.player-position').text(position.PositionName);        

        wrapper.append(newPosition);

        let playerContainer = playerContainerTemplate.clone(true);                   

        position.Players.forEach(player => {
            let newEntry = playerEntryTemplate.clone(true);
            
            playerIds.push(player.PlayerId);

            newEntry.find('.player-name').text(player.PlayerName);
            newEntry.find('.player-score').val(player.Score);
            newEntry.find('.player-score').attr('data-player-id', player.PlayerId);

            playerContainer.append(newEntry);
        });

        wrapper.append(playerContainer);

        wrapper.append($('<hr />')); // add horizontal divider

        container.append(wrapper);
    });
}

function updatePlayerScores() {
    let players = [];

    playerIds.forEach(id => {
        let $input = $(`input[data-player-id="${id}"]`);

        players.push({
            PlayerId: id,
            NewScore: $input.val()
        });
    })

    let dto = {
        Team: $('#EditPlayerTeamSelector').val(),
        Week: $('#EditPlayerWeekSelector').val(),
        Players: players
    };

    let urll = '/api/Admin/EditPlayerScores';    

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(dto),
        contentType: 'application/json',
        success: function (data) {
            toastr.success("Scores updated");
        },
        error: function (data) {
            showError("Error updating scores", data);
        }
    })
}

function setScoresToZero() {    
    playerIds.forEach(id => {
        let $input = $(`input[data-player-id="${id}"]`);

        $input.val(0);
    });    

    toastr.warning("Scores have been set to 0. Click 'Save Changes' to apply change.");
}