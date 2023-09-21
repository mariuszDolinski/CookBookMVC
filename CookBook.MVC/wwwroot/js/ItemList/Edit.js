const ShowEditModal = (name) => {
    $("#currentItemName").val(name);
    $("#newItemName").val(name);
    $("#editItemModal").modal('show');
}

//funkcja z onclick na buttonie
const EditItem = (code) => {
    const oldName = $("#currentItemName").val();
    ExecuteEditAjax(code, oldName);
}

const ExecuteEditAjax = (code, oldName) => {
    const newName = $("#newItemName").val();
    if (oldName == newName) {
        $("#editItemModal").modal('hide');
        return;
    }
    const result = ValidteNewName(newName);
    if (result.length > 0) {
        toastr.options = {
            "closeButton": true
        }
        toastr["warning"](result);
        return;
    }
    switch (code) {
        case "INGR": ExecuteEditItemAjax(oldName, newName, "ingridient"); break;
        case "UNIT": ExecuteEditItemAjax(oldName, newName, "unit"); break;
        case "CATG": ExecuteEditItemAjax(oldName, newName, "recipeCategory"); break;
        default: break;
    }
}

//wywo³nie akcje Edit w danym kontrolerze poprzez ajax
const ExecuteEditItemAjax = (oldName, newName, controller) => {
    const names = oldName + ";" + newName;
    $.ajax({
        url: `/${controller}/edit/${names}`,
        type: 'put',
        success: function () {
            $("#editItemModal").modal('hide');
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

//walidacja nowej nazwy
const ValidteNewName = (name) => {
    if (name == null || name.length == 0) {
        return "Podaj now&#261; nazw&#281;";
    }
    else if (name.length < 3) {
        return "Nazwa musi posiada&#263; co najmniej 3 znaki";
    }
    else if (name.length > 50) {
        return "Nazwa mo&#380;e posiada&#263; maksymalnie 50 znak&#243;w";
    }
    else if (name.includes(";")) {
        return "Nazwa zawiera niedozwolony znak";
    }
    else {
        return "";
    }
}