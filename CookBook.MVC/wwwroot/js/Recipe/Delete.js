const DeleteRecipe = (id) => {
    //const ingId = $("#ingridientId").val();
    $.ajax({
        url: `/recipe/${id}`,
        type: 'delete',
        success: function () {
            toastr["success"]("Przepis zosta&#322; usuni&#281;ty");
            $("#row-" + id).remove();
            //$("#deleteRecipeIngridientModal").modal('hide');
        },
        error: function () {
            toastr["error"]("Co� posz�o nie tak. Operacja zako�czona niepowodzeniem.")
        }
    })
}