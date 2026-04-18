$(function () {
    getBalances(); 

    $('#RefreshTableBtn').on('click', function (e) {
        e.preventDefault();

        getBalances();
    })
})

function getBalances() {
    let urll = '/api/admin/GetUserBalances';

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            fillUserBalances(data);
        },
        error: function (data) {
            showError("Error getting balances", data);
        }
    })
}

function fillUserBalances(balances) {
    let rowTemplate = $('#ManageBalanceRowTemplate').contents();
    let tableBody = $('#ManageBalanceTableBody');

    tableBody.empty();

    balances.forEach(balance => {
        let newRow = rowTemplate.clone(false);
        newRow.attr('id', balance.UserId);
        newRow.find('.balance-name').text(balance.Name);
        newRow.find('.balance-amount').val(balance.Amount);

        newRow.find('.update-balance-btn').on('click', function () {
            updateBalance(balance.UserId)
        })

        tableBody.append(newRow);
    })
}

function updateBalance(userId) {
    let row = $('#' + userId);

    let dto = {
        UserId: userId,
        Name: row.find('.balance-name').text(),
        Amount: row.find('.balance-amount').val()
    };

    let urll = '/api/admin/UpdateUserBalance';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(dto),
        contentType: 'application/json',
        success: function (data) {
            toastr.success("Balance Updated");
        },
        error: function (data) {
            showError("Error updating balance", data);
        }
    })
}