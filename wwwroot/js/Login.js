$(function () {
    $('#LoginForm').on('submit', function (e) {
        e.preventDefault();

        login();
    })
})

function login() {
    const username = $('#LoginUsername').val();
    const password = $('#LoginPassword').val();

    let dto = {
        Username: username,
        Password: password
    };

    let urll = '/api/auth/login';

    $.ajax({
        method: 'POST',
        url: urll,
        data: JSON.stringify(dto),
        contentType: 'application/json',
        success: function (data) {
            toastr.success("Login Successful");
            window.location.href = '/home/index';
        },
        error: function (data) {
            showError("Error logging in", data);
        }
    })
}