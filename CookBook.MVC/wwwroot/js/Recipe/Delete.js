const DeleteRecipe = () => {
    const id = $("#recipeId").val();
    $.ajax({
        url: `/recipe/${id}`,
        type: 'delete',
        success: function () {
            toastr["success"]("Przepis zosta&#322; usuni&#281;ty");
            $("#row-" + id).remove();
            $("#deleteRecipeModal").modal('hide');
        },
        error: function () {
            toastr["error"]("Co� posz�o nie tak. Operacja zako�czona niepowodzeniem.")
        }
    })
}

const ShowConfirmDeleteModal = (id, name) => {
    $("#recipeId").val(id);
    $("#deleteRecipeModal").modal('show');
    $("#confMessage").html("Czy na pewno chcesz usun&#261;&#263; przepis: <br />'" + name + "'?");
}