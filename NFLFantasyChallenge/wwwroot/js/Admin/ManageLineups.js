$(function () {
    getUserInfo(); 

    $('#RefreshTableBtn').on('click', function (e) {
        e.preventDefault();

        getUserInfo();
    });

    $('#NameSearch').on('input', function (e) {       
        searchBalanceNames();
    })
})

let Users = [];

function getUserInfo() {
    let urll = '/api/admin/GetUserManageInfo';

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            Users = data;
            fillUserBalances(data);
        },
        error: function (data) {
            showError("Error getting balances", data);
        }
    })
}

function fillUserBalances(users) {
    let rowTemplate = $('#ManageBalanceRowTemplate').clone(false).contents();
    let tableBody = $('#ManageBalanceTableBody');

    tableBody.empty();

    users.forEach(user => {
        let newRow = rowTemplate.clone(false);
        newRow.attr('id', user.UserId);
        newRow.find('.balance-name').text(user.Name);
        newRow.find('.balance-amount').val(user.Balance);

        newRow.find('.update-balance-btn').on('click', function () {
            updateBalance(user.UserId)
        })

        tableBody.append(newRow);

        if (user.LineupStatus == "Creation") {
            newRow.find('.still-creating').show();
        } else if (user.LineupStatus == "Approval") {
            newRow.find('.needs-approval').show();
            newRow.find('.approve-lineup-btn').on('click', function () {
                getUserLineupToApprove(user.UserId);
            });
        } else if (user.LineupStatus == "Payment") {
            newRow.find('.awaiting-payment').show();
        } else if (user.LineupStatus == "Finalized") {
            newRow.find('.already-approved').show();
        }

        tableBody.append(newRow);
    })
}

function updateBalance(userId) {
    let row = $('#' + userId);

    let dto = {
        UserId: userId,
        Name: row.find('.balance-name').text(),
        Balance: row.find('.balance-amount').val()
    };

    let urll = '/api/admin/UpdateUserBalance';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(dto),
        contentType: 'application/json',
        success: function (data) {
            toastr.success("Balance Updated");
            getUserInfo();
        },
        error: function (data) {
            showError("Error updating balance", data);
        }
    })
}

function searchBalanceNames() {
    let searchText = $('#NameSearch').val()?.toLowerCase().trim() || '';
    let filteredBalances = Users.filter(f => f.Name.toLowerCase().includes(searchText));

    fillUserBalances(filteredBalances);
}

function getUserLineupToApprove(userId) {
    let urll = '/Admin/GetLineupToApprove?UserId=' + userId;

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            $('#ApproveLineupModalBody').html(data);

            // the below two functions still need tested, I only added the 
            // code the api and the front end calls, didn't test
            $('#ApproveLineupBtn').off('click');
            $('#ApproveLineupBtn').on('click', function (e) {
                approveLineup(userId);
            });

            $('#DenyLineupBtn').off('click');
            $('#DenyLineupBtn').on('click', function (e) {
                denyLineup(userId);
            })

            $('#ApproveLineupModal').modal('show');
        },
        error: function (data) {
            showError("Error getting lineup", data);
        }
    })
}

function approveLineup(userId) {
    let urll = '/api/Admin/ApproveLineup';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(userId),
        contentType: 'application/json',
        success: function (data) {
            toastr.success('Lineup approved');
            $('#ApproveLineupModal').modal('hide');
            getUserInfo();
        },
        error: function (data) {
            showError("Error getting lineup", data);
        }
    })
}

function denyLineup(userId) {
    let urll = '/api/Admin/DenyLineup';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(userId),
        contentType: 'application/json',
        success: function (data) {
            toastr.success('Lineup denied');
            $('#ApproveLineupModal').modal('hide');
            getUserInfo();
        },
        error: function (data) {
            showError("Error getting lineup", data);
        }
    })
}            