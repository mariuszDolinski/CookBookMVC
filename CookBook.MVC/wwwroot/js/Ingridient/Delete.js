const DeleteIngridient = () => {
    const ingName = $("#currentIngName").val();
    $.ajax({
        url: `/ingridient/delete/${ingName}`,
        type: 'delete',
        success: function () {
            $("#deleteIngridientModal").modal('hide');
            location.reload();
            toastr["success"]("Sk&#322;adnik zosta&#322; usuni&#281;ty");
        },
        error: function (xhr) {
            toastr.options = {
                "closeButton": true
            }
            toastr["error"](xhr.responseText)
        }
    })
}

const ShowDeleteIngModal = (name) => {
    $("#currentIngName").val(name);
    $("#deleteIngridientModal").modal('show');
    $("#confMessage").html("Czy na pewno chcesz usun&#261;&#263; sk&#322;adnik: <br />'" + name + "'?");
}