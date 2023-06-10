const DeleteUnit = () => {
    const ingName = $("#currentUnitName").val();
    $.ajax({
        url: `/unit/delete/${ingName}`,
        type: 'delete',
        success: function () {
            $("#deleteUnitModal").modal('hide');
            location.reload();
            toastr["success"]("Jednostka zosta&#322;a usuni&#281;ta");
        },
        error: function (xhr) {
            toastr.options = {
                "closeButton": true
            }
            toastr["error"](xhr.responseText)
        }
    })
}

const ShowDeleteUnitModal = (name) => {
    $("#currentUnitName").val(name);
    $("#deleteUnitModal").modal('show');
    $("#confMessage").html("Czy na pewno chcesz usun&#261;&#263; jednostk&#281;: <br />'" + name + "'?");
}