const ShowDeleteModal = (name, code) => {
    $("#currentItemName").val(name);
    $("#deleteItemModal").modal('show');
    const message = GetModalMessage(name, code);
    $("#confMessage").html(message);
}

//funkcja z onclick na buttonie
const DeleteItem = (code) => {
    const itemName = $("#currentItemName").val();
    ExecuteDeleteAjax(code, itemName);
}

const ExecuteDeleteAjax = (code, name) => {
    switch (code) {
        case "INGR": ExecuteDeleteItemAjax(name, "ingridient"); break;
        case "UNIT": ExecuteDeleteItemAjax(name, "unit"); break;
        case "CATG": ExecuteDeleteItemAjax(name, "recipeCategory"); break;
        default: break;
    }
}

//wywo³nie akcje Edit w danym kontrolerze poprzez ajax
const ExecuteDeleteItemAjax = (name, controller) => {
    $.ajax({
        url: `/${controller}/delete/${name}`,
        type: 'delete',
        success: function () {
            $("#deleteItemModal").modal('hide');
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
        case "CATG": return "Czy na pewno chcesz usun&#261;&#263; kategori&#281;: <br />'" + name + "'?"
        default: break;
    }
}
