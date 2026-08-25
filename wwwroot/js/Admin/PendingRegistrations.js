$(function () {
    getPendingRegistrations();
})

function getPendingRegistrations() {
    let urll = '/api/admin/GetPendingRegistrations';

    $.ajax({
        method: 'GET',
        url: urll,
        success: function (data) {
            fillPendingRegistrations(data);
        },
        error: function (data) {
            showError("Error getting pending registrations", data);
        }
    })
}

function fillPendingRegistrations(registrations) {
    let rowTemplate = $('#RegistrationRowTemplate').contents();

    let tableBody = $('#PendingRegistrationTableBody');
    tableBody.empty();

    registrations.forEach(registration => {
        let newRow = rowTemplate.clone(false);
        
        newRow.find('.pending-fullname').text(registration.FullName);
        newRow.find('.pending-username').text(registration.Username);
        newRow.find('.pending-email').text(registration.Email);
        newRow.find('.pending-creationdate').text(registration.DisplayCreationDate);

        newRow.find('.pending-approve-btn').on('click', function () {
            approvePendingRegistration(registration.PendingRegistrationId, registration.FullName);
        });

        newRow.find('.pending-deny-btn').on('click', function () {
            denyPendingRegistration(registration.PendingRegistrationId, registration.FullName);
        });

        tableBody.append(newRow);
    });
}

function approvePendingRegistration(registrationId, fullName) {
    let message = "Are you sure you want to approve the registration for "
        + fullName + "? The account will be made and cannot be undone.";

    if (!confirm(message)) {
        return;
    }

    let urll = '/api/Admin/ApprovePendingRegistration';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(registrationId),
        contentType: 'application/json',
        success: function (data) {
            toastr.success('Registration complete');
            getPendingRegistrations();
        },
        error: function (data) {
            showError("Error completing registration", data);
        }
    })
}

function denyPendingRegistration(registrationId, fullName) {
    let message = "Are you sure you want to deny the registration for "
        + fullName + "? This cannot be undone."

    if (!confirm(message)) {
        return;
    }

    let urll = '/api/Admin/DenyPendingRegistration';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(registrationId),
        contentType: 'application/json',
        success: function (data) {
            toastr.success("Regstration denied");
            getPendingRegistrations();
        },
        error: function (data) {
            showError("Error denying registration", data);
        }
    })
}