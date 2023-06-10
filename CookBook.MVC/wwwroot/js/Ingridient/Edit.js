const ShowEditIngModal = (name) => {
    $("#currentIngName").val(name);
    $("#newIngName").val(name);
    $("#editIngridientModal").modal('show');
}

$("#editIngridientModal form").submit(function (event) {
    event.preventDefault();
    const form = $(this);
    const oldName = $("#currentIngName").val();

    $.ajax({
        url: `/ingridient/edit/${oldName}`,
        type: 'put',
        data: form.serialize(),
        success: function () {
            toastr["success"]("Sk&#322;adnik zosta&#322; zmieniony");
            $("#editIngridientModal").modal('hide');
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