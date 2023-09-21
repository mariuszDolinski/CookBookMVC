const DeleteItem = (code) => {
    const itemName = $("#currentItemName").val();
    ExecuteDeleteAjax(code, itemName);
}

const ShowDeleteModal = (name, code) => {
    $("#currentItemName").val(name);
    $("#deleteItemModal").modal('show');
    const message = GetModalMessage(name, code);
    $("#confMessage").html(message);
}


//metody do wywo³ywnia ajaxa dla konkretnego kontrolera
const ExecuteDeleteAjax = (code, name) => {
    switch (code) {
        case "INGR": ExecuteDeleteIngAjax(name); break;
        case "UNIT": ExecuteDeleteUnitAjax(name); break;
        default: break;
    }

}

const ExecuteDeleteIngAjax = (name) => {
    $.ajax({
        url: `/ingridient/delete/${name}`,
        type: 'delete',
        success: function () {
            $("#deleteIngridientModal").modal('hide');
            location.reload();
        },
        error: function (xhr) {
            toastr.options = {
                "closeButton": true
            }
            toastr["error"](xhr.responseText)
        }
    })
}

const ExecuteDeleteUnitAjax = (name) => {
    $.ajax({
        url: `/unit/delete/${name}`,
        type: 'delete',
        success: function () {
            $("#deleteUnitModal").modal('hide');
            location.reload();
        },
        error: function (xhr) {
            toastr.options = {
                "closeButton": true
            }
            toastr["error"](xhr.responseText)
        }
    })
}

const GetModalMessage = (name, code) => {
    switch (code) {
        case "INGR": return "Czy na pewno chcesz usun&#261;&#263; sk&#322;adnik: <br />'" + name + "'?"
        case "UNIT": return "Czy na pewno chcesz usun&#261;&#263; jednostk&#281;: <br />'" + name + "'?"
        default: break;
    }
}
