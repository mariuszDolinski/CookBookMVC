$(document).ready(function () {

    $("#createlistIngridients").select2({
        width: '100%',
        dropdownParent: $('#createRecipeIngridientModal'),//poprawa dzia³ania w modalu
        theme: 'bootstrap-5'
    });
    $("#createlistUnits").select2({
        width: '100%',
        dropdownParent: $('#createRecipeIngridientModal'),
        theme: 'bootstrap-5'
    });

    $("#editlistIngridients").select2({
        width: '100%',
        dropdownParent: $('#editRecipeIngridientModal'),
        theme: 'bootstrap-5'
    });
    $("#editlistUnits").select2({
        width: '100%',
        dropdownParent: $('#editRecipeIngridientModal'),
        theme: 'bootstrap-5'
    });

    FillData(`/recipe/getDatalists`,$("#createlistIngridients"), $("#createlistUnits"),"","");
    LoadRecipeIngridients();

    $("#createRecipeIngridientModal form").submit(function (event) {
        event.preventDefault();
        const form = $(this);

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),
            success: function () {
                toastr["success"]("Sk&#322;adnik zosta&#322; dodany do przepisu");
                $("#amountId").val("");
                $("#createlistIngridients").val('0').trigger('change');
                $("#createlistUnits").val('0').trigger('change');
                $("#descId").val("");
                $("#createRecipeIngridientModal").modal('hide');
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

    $("#editRecipeIngridientModal form").submit(function (event) {
        event.preventDefault();
        const form = $(this);
        const ingId = $("#ingridientId").val();

        $.ajax({
            url: `/recipeIngridient/${ingId}`,
            type: 'put',
            data: form.serialize(),
            success: function () {
                toastr["success"]("Sk&#322;adnik zosta&#322; zmieniony");
                $("#editRecipeIngridientModal").modal('hide');
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

const DeleteIng = () => {
    const ingId = $("#ingridientId").val();
    $.ajax({
        url: `/recipeIngridient/${ingId}`,
        type: 'delete',
        success: function () {
            toastr["success"]("Sk&#322;adnik zosta&#322; usuni&#281;ty");
            $("#row-" + ingId).remove();
            $("#deleteRecipeIngridientModal").modal('hide');
        },
        error: function () {
            toastr["error"]("Coœ posz³o nie tak. Operacja zakoñczona niepowodzeniem.")
        }
    })
}

const ConfirmDeleteModal = (ingId, ingDesc) => {
    $("#ingridientId").val(ingId);
    $("#deleteRecipeIngridientModal").modal('show');
    $("#confMessage").html("Czy na pewno chcesz usun&#261;&#263; sk&#322;adnik: <br />'" + ingDesc + "'?");
}

const ShowEditModal = (id, desc, amount, ing, unit) => {
    FillData(`/recipe/getDatalists`,$("#editlistIngridients"), $("#editlistUnits"),ing,unit)
    $("#ingridientId").val(id);
    $("#amountEdit").val(amount);
    //$("#editlistIngridients").val(ing).trigger('change');
    //$("#unitEdit").val(unit);
    $("#descEdit").val(desc);
    $("#editRecipeIngridientModal").modal('show');
}