$(function () {
    $('#UsersTable').hide();
    getRoles();    
})

let roles = [];

function getRoles() {
    let urll = '/api/Admin/GetRoles';

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            roles = data;
            getUsers();
        },
        error: function (data) {
            showError("Error getting Roles", data);
        }
    })
}

function getUsers() {
    let urll = '/api/Admin/GetUsers';

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            fillUsers(data);
        },
        error: function (data) {
            showError("Error getting Users", data);
        }
    })
}

function fillUsers(users) {
    let tableBody = $('#UsersTableBody');

    tableBody.empty();

    let rowTemplate = $('#UserRowTemplate').contents();
    let optionTemplate = $('#OptionTemplate').contents();

    $('#UsersTable').hide();
    users.forEach(user => {
        let newRow = rowTemplate.clone(false);

        newRow.attr('id', user.UserID);
        newRow.find('.user-name').val(user.UserName);
        newRow.find('.password').val(user.Password);
        newRow.find('.user-full-name').val(user.FullName);
        newRow.find('.phone-number').val(user.PhoneNumber);
        newRow.find('.user-balance').val(user.Balance);

        roles.forEach(role => {
            let newOption = optionTemplate.clone(false);
            newOption.text(role.RoleName);
            newOption.val(role.RoleID);

            newRow.find('.user-role').append(newOption);
        });

        newRow.find('.user-role').val(user.RoleID);

        newRow.find('.update-user').on('click', function (e) {
            e.preventDefault();

            updateUser(user.UserID);
        })

        newRow.find('.remove-user').on('click', function (e) {
            e.preventDefault();

            removeUser(user.UserID);
        })

        tableBody.append(newRow);
    });

    $('#UsersTable').show();
}

function updateUser(userId) {
    let $row = $('#' + userId);
    let dto = {
        UserID: userId,
        Username: $row.find('.user-name').val(),
        FullName: $row.find('.user-full-name').val(),
        PhoneNumber: $row.find('.phone-number').val(),
        Balance: parseInt($row.find('.user-balance').val()),
        RoleID: parseInt($row.find('.user-role').val()),
        RoleName: $row.find('.user-role').text(),
    };

    console.log(dto);
    console.log($row.find('.user-role').val());
    if ($row.find('.user-password').val() != '') {
        dto.Password = $row.find('.user-password');
    }

    let urll = '/api/admin/UpdateUser';

    $.ajax({
        method: 'POST',
        url: urll,
        contentType: 'application/json',
        data: JSON.stringify(dto),
        success: function (data) {
            toastr.success('User Updated');
            getUsers();
        },
        error: function (data) {
            showError("Error Updating User", data);
        }
    })
}

function removeUser(userId) {
    if (!confirm("Are you sure you want to delete the user?\nThis will also delete the user's lineups")) {
        return;
    }

    let urll = '/api/Admin/DeleteUser';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(userId),
        contentType: 'application/json',
        success: function (data) {
            toastr.success('User Deleted');
            getUsers();
        },
        error: function (data) {
            showError("Error Deleting User", data);
        }
    })
}