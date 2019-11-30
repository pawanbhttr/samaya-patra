toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-top-center",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

$(document).ready(function () {
    $('table.dataTables').DataTable();
    $('[data-toggle="tooltip"]').tooltip();
    $('input.date-picker').datepick();
});

function showMessage(message, messageType, title = "") {
    switch (messageType) {
        case "error": {
            toastr.error(message, $.isEmptyObject(title) ? messageType.toUpperCase() : title.toUpperCase());
            break;
        }
        case "info": {
            toastr.info(message, $.isEmptyObject(title) ? messageType.toUpperCase() : title.toUpperCase());
            break;
        }
        case "success": {
            toastr.success(message, $.isEmptyObject(title) ? messageType.toUpperCase() : title.toUpperCase());
            break;
        }
    }
}

function dynamicControl() {
    $('[data-toggle="tooltip"]').tooltip();
    $('input.date-picker').datepick();
}