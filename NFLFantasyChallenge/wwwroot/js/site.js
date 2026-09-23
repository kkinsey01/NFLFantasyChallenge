// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showError(defaultMsg, xhr) {
    toastr.error(
        xhr?.responseJSON?.detail
        ?? xhr?.responseText
        ?? defaultMsg
    );
}

function formatUrlParams(params) {
    const qs = new URLSearchParams(params);
    const str = qs.toString();
    return str ? `?${str}` : '';
}