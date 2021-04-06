
function OpenPopup(response, title) {
    $(".modal-title").html(title);
    $("#modalHTML").html(response);
    $("#leave-model").modal("show");
}

var getDataPost = function (url, param, successCallback) {
    $.ajax({
        url: url, data: param, method: "POST",
        success: function (result) {
            successCallback(result);
        },
        error: function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        }
    });
};

function getDataGet(url, successCallback) {
    $.ajax({
        url: url, method: "GET",
        success: function (result) {
            successCallback(result);
        },
        error: function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        }
    });
}

function datePicker() {
    flatpickr(".datepicker", {
        dateFormat: "d/m/Y"
    });

    flatpickr(".timepicker", {
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
        time_24hr: true
    });

    $(".datepicker").css("background-color", '#ffffff');

    //$('.datepicker').datepicker({
    //    format: 'dd/mm/yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});
}

function getMomentDate(value) {
    return moment(value, "DD/MM/YYYY");
}

function getMomentTime(value) {
    return moment(value, "HH:II");
}

//Toastr

function Toastr(type, msg) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    toastr[type](msg);
}

//SweetBox

function deleteRecord(successCallback) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You want to delete!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        successCallback(result.value);
    })
}

function ConfirmBox(title, text, successCallback) {
    Swal.fire({
        title: title,
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
    }).then((result) => {
        successCallback(result.value);
    })
}




