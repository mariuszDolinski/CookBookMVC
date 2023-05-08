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
                toastr["warning"](xhr.responseText, "Z&#322;e dane!")
            }
        })
    });
});