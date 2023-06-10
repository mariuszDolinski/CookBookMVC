const ShowEditUnitModal = (name) => {
    $("#currentUnitName").val(name);
    $("#newUnitName").val(name);
    $("#editUnitModal").modal('show');
}

$("#editUnitModal form").submit(function (event) {
    event.preventDefault();
    const form = $(this);
    const oldName = $("#currentUnitName").val();

    $.ajax({
        url: `/unit/edit/${oldName}`,
        type: 'put',
        data: form.serialize(),
        success: function () {
            toastr["success"]("Jednostka zosta&#322;a zmieniona");
            $("#editUnitModal").modal('hide');
            location.reload();
        },
        error: function (xhr) {
            toastr.options = {
                "closeButton": true
            }
            toastr["error"](xhr.responseText)
        }
    })
});