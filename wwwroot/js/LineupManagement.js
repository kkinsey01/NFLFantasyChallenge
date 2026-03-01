$(function () {    
    getUserLineup();

    $('#SearchPlayerNames').on('input', function (e) {
        e.preventDefault();

        updateSearch();
    })

    $('#TeamSearchFilter').on('change', function (e) {
        e.preventDefault();

        updateSearch();
    })

    $('#LockLineup').on('click', function (e) {
        e.preventDefault();
        
        lockLineup();
    });

    $('#CloseViewModalBtn').on('click', function (e) {
        e.preventDefault();

        clearView();
    })
})

let positionGroups = [];
let isLineupLocked = false;
let playerAddList = [];

function getUserLineup() {
    var urll = '/api/web/GetUserLineup';    

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            $('#UserFullName').text(data.FullName);
            positionGroups = data.PositionGroups;
            isLineupLocked = data.IsLocked;
            fillLineup();
        },
        error: function (data) {
            showError("Error getting lineup", data);
        }
    })
}

function fillLineup() {
    let positionTemplate = $('#PositionTemplate').clone(false).contents();
    let playerContainerTemplate = $('#PlayerContainerTemplate').clone(false).contents();
    let playerTemplate = $('#PlayerTemplate').clone(false).contents();

    let container = $('#LineupBody');  
    container.empty();

    positionGroups.forEach(position => {
        const wrapper = $('<div></div>');
        
        let newPosition = positionTemplate.clone(false);
        newPosition.find('.player-position').text(position.PositionName);

        wrapper.append(newPosition);

        let playerContainer = playerContainerTemplate.clone(true);

        position.Players.forEach(player => {
            let newRow = playerTemplate.clone(true);

            // lineup locked, so info should be static
            if (isLineupLocked) {
                newRow.find('.player-name').val(player.PlayerName);
                newRow.find('.player-team').text(player.Team);
                newRow.find('.add-player-btn').addClass('d-none');
                newRow.find('.remove-player-btn').addClass('d-none');
                newRow.find('.view-player-btn').on('click', function (e) {
                    viewPlayer(player.PlayerId);
                })

                playerContainer.append(newRow);
                return;
            }

            // no view button here, lineup is still pending
            newRow.find('.view-player-btn').addClass('d-none');

            let playerName = player.PlayerName;

            // player not filled in yet
            if (playerName == undefined || playerName == "") {
                newRow.find('.player-name').val('<Empty Slot>');
                newRow.find('.remove-player-btn').addClass('d-none');

                newRow.find('.add-player-btn').on('click', function (data) {
                    getPlayerList(position.PositionName);
                });
                playerContainer.append(newRow);
                return;
            }

            // filled out player, but still modifiable at this point
            newRow.find('.player-name').val(playerName);                    
            newRow.find('.player-team').text(player.Team);
            newRow.find('.add-player-btn').addClass('d-none');
            newRow.find('.remove-player-btn').on('click', function (data) {
                removePlayerFromLineup(player.PlayerId);
            });                       
            playerContainer.append(newRow);
        })

        wrapper.append(playerContainer);

        wrapper.append($('<hr />'));

        container.append(wrapper);
    })
}

function getPlayerList(position) {
    let searchFilter = {
        Position: position
    };

    let urll = '/api/web/GetPlayerOptions' + formatUrlParams(searchFilter);

    $('#SearchPlayerNames').val();
    $('#TeamSearchFilter').val();

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            playerAddList = data;
            fillTeamSearchDropdown(data);
            fillAddPlayerList(data);
        },
        error: function (data) {
            showError("Error getting player list", data);
        }
    })
}             

function fillTeamSearchDropdown(players) {    
    let optionTemplate = $('#OptionTemplate').contents();
    let teamSelect = $('#TeamSearchFilter');

    teamSelect.empty();

    const distinctTeamsSorted = [...new Set(players.map(p => p.Team))]
        .sort((a, b) => a.localeCompare(b));

    let emptyOption = optionTemplate.clone(false);
    emptyOption.val('');
    emptyOption.text('--');
    teamSelect.append(emptyOption);

    distinctTeamsSorted.forEach(team => {
        let newOption = optionTemplate.clone(false);

        newOption.val(team);
        newOption.text(team);

        teamSelect.append(newOption);
    })
}

function fillAddPlayerList(players) {
    let addPlayerTemplate = $('#AddPlayerTemplate').contents();
    let body = $('#AddPlayerOptionBody');

    body.empty();

    players.forEach(player => {
        let newRow = addPlayerTemplate.clone(false);
        newRow.find('.add-player-name').text(player.DisplayName);

        newRow.find('.confirm-add-player-btn').on('click', function (data) {
            addPlayerToLineup(player.PlayerID);
        })
        body.append(newRow);
    });

    $('#AddPlayerModal').modal('show');
}

function updateSearch() {
    let nameSearch = $('#SearchPlayerNames').val().toLowerCase();

    let filteredPlayers = playerAddList;

    if (nameSearch != '') {
        filteredPlayers = filteredPlayers.filter(f => f.PlayerName.toLowerCase().includes(nameSearch));
    }

    let teamSelect = $('#TeamSearchFilter').val();
    if (teamSelect != '') {
        filteredPlayers = filteredPlayers.filter(f => f.Team == teamSelect);
    }

    fillTeamSearchDropdown(filteredPlayers);
    fillAddPlayerList(filteredPlayers);
}

function addPlayerToLineup(playerId) {
    let urll = '/api/web/AddPlayerToLineup';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(playerId),
        contentType: 'application/json',
        success: function (data) {
            toastr.success("Player Added");
            $('#AddPlayerModal').modal('hide');
            getUserLineup();
        },
        error: function (data) {
            showError("Error Adding Player", data);
        }
    });
}

function removePlayerFromLineup(playerId) {
    let urll = '/api/web/RemovePlayerFromLineup';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(playerId),
        contentType: 'application/json',
        success: function (data) {
            toastr.success('Player Removed');
            getUserLineup();
        },
        error: function (data) {
            showError("Error Removing Player", data);
        }
    })
}

function lockLineup() {
    if (!confirm("Are you sure you want to lock your lineup? This action cannot be undone.")) {
        return;
    }

    let urll = '/api/web/LockLineup';

    $.ajax({
        method: 'POST',
        url: urll,
        success: function (data) {
            toastr.success("Lineup Locked");
            getUserLineup();
        },
        error: function (data) {
            showError("Error Locking Lineup", data);
        }
    })
}      

function viewPlayer(playerId) {
    let query = {
        PlayerId: playerId
    }

    let urll = '/api/web/GetIndividualScores' + formatUrlParams(query);

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            fillView(data);
        },
        error: function (data) {
            showError("Error getting player info", data)
        }
    });
}

function fillView(data) {
    $('#ViewPlayerName').text(data.PlayerName);
    $('#ViewPlayerTeam').text(data.Team);
    $('#ViewPlayerWildCardScore').text(data.WildCardScore);
    $('#ViewPlayerDivisionalScore').text(data.DivisionalScore);
    $('#ViewPlayerConferenceScore').text(data.ConferenceScore);
    $('#ViewPlayerSuperBowlScore').text(data.SuperBowlScore);
    $('#ViewPlayerTotalScore').text(data.TotalScore);

    $('#ViewPlayerModal').modal('show');
}

function clearView() {
    $('#ViewPlayerName').text('');
    $('#ViewPlayerTeam').text('');
    $('#ViewPlayerWildCardScore').text('');
    $('#ViewPlayerDivisionalScore').text('');
    $('#ViewPlayerConferenceScore').text('');
    $('#ViewPlayerSuperBowlScore').text('');
    $('#ViewPlayerTotalScore').text('');

    $('#ViewPlayerModal').modal('hide');
}