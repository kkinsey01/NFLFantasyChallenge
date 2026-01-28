$(function () {
    $('#SignupForm').on('submit', function (e) {
        e.preventDefault();

        submit();
    })

    $('#SignupFormCancel').on('click', function (e) {
        e.preventDefault();

        clear();
    })
})

function submit() {

    const password = $('#SignupPassword').val();
    const confirmPassword = $('#SignupConfirmPassword').val();

    if (password !== confirmPassword) {
        toastr.error("Passwords do not match");
        return;
    }

    var userInfo = {
        FullName: $('#SignupFullName').val(),
        Username: $('#SignupUsername').val(),
        Password: $('#SignupPassword').val(),
        ConfirmPassword: $('#SignupConfirmPassword').val(),
        PhoneNumber: $('SignupPhoneNumber').val()
    };

    var urll = '/api/auth/signup';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(userInfo),
        contentType: 'application/json',
        success: function (data) {
            toastr.success("Successfully Signed Up!");
        },
        error: function (data) {
            showError("Error signing up", data);
        }
    })
}

function clear() {
    $('#SignupFullName').val("");
    $('#SignupUsername').val("");
    $('#SignupPassword').val("");
    $('#SignupConfirmPassword').val("");
    $('#SignupPhoneNumber').val("");
}
