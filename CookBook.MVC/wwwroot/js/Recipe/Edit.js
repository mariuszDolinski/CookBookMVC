$(document).ready(function () {
    FillData()
    LoadRecipeIngridients()

    $("#createRecipeIngridientModal form").submit(function (event) {
        event.preventDefault();
        const form = $(this);

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),
            success: function () {
                toastr["success"]("Sk&#322;adnik zosta&#322; dodany do przepisu");
                $("#amountId").val("")
                $("#ingId").val("")
                $("#unitId").val("")
                $("#descId").val("")
                LoadRecipeIngridients()
            },
            error: function (xhr) {
                toastr.options = {
                    "closeButton": true
                }
                toastr["warning"](xhr.responseText)
            }
        })
    });
});

const DeleteIng = (ingId) => {
    $.ajax({
        url: `/recipeIngridient/${ingId}`,
        type: 'delete',
        success: function () {
            toastr["success"]("Sk&#322;adnik zosta&#322; usuni&#281;ty");
            $("#row-" + ingId).remove();
        },
        error: function () {
            toastr["error"]("Coœ posz³o nie tak. Operacja zakoñczona niepowodzeniem.")
        }
    })
}

const ShowEditModal = (ingId) => {
    $("#createRecipeIngridientModal").modal('show');
}