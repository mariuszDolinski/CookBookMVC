//populate datalists with ingridients and units names
const FillData = (iContainer, uContainer, ing, un) => {

    $.ajax({
        url: `/recipe/getDatalists`,
        type: 'get',
        success: function (data) {
            if (iContainer != null) {
                iContainer.empty();
                if (ing == "") {
                    iContainer.append(`<option value="0" selected disabled="true">Wybierz składnik</option>`);
                    for (const item of data.ingridients)
                        iContainer.append(`<option value="${item}">${item}</option>`);
                }
                else {
                    iContainer.append(`<option value="0" disabled="true">Wybierz składnik</option>`);
                    for (const item of data.ingridients) {
                        if (item == ing) {
                            iContainer.append(`<option value="${item}" selected>${item}</option>`);
                        } else {
                            iContainer.append(`<option value="${item}">${item}</option>`);
                        }
                    }
                }
            }              
            if (uContainer != null) {
                uContainer.empty();
                if (un == "") {
                    uContainer.append(`<option value="0" selected disabled="true">Wybierz jednostkę</option>`);
                    for (const item of data.units)
                        uContainer.append(`<option value="${item}">${item}</option>`);
                }
                else {
                uContainer.append(`<option value="0" disabled="true">Wybierz jednostkę</option>`);
                for (const item of data.units)
                    if (item == un) {
                        uContainer.append(`<option value="${item}" selected>${item}</option>`);
                    } else {
                        uContainer.append(`<option value="${item}">${item}</option>`);
                    }
                }
            }    
        },
        error: function () {
            toastr["error"]("Lista składników lub jednostek nie mogła zostać wczytana")
        }
    })
}

const RenderRecipeIngridients = (data, container) => {
    container.empty();
    if (container.attr('name') == "details") {
        for (const item of data) {
            container.append(
                `<i class="bi bi-basket">  &nbsp; ${item.description}</i>`)
        }
    }
    if (container.attr('name') == "edit") {
        for (const item of data) {
            container.append(
                `<tr id="row-${item.id}">
                    <td class="align-middle">${item.description}</td>
                    <td>
                        <a class="btn btn-outline-primary btn-sm" onclick="ShowEditModal(${item.id},'${item.description}','${item.amount}','${item.ingridient}','${item.unit}')"><i class="bi bi-pencil"></i></a>
                        <a class="btn btn-outline-danger btn-sm" onclick="ConfirmDeleteModal(${item.id},'${item.description}')"><i class="bi bi-trash"></i></a>
                    </td>
                </tr>`
            )
        }
    }
}

const RenderSearchList = (data, container) => {
    container.append(
        `<tr>
            <td><i class="bi bi-cart-check"></i> ${data}</td>
            <td><a class="btn btn-outline-danger btn-sm" onclick="deleteFromSearchList(this)"><i class="bi bi-trash"></i></a></td>
        </tr>`
    );
}

const LoadRecipeIngridients = () => {
    const container = $("#ingridientsList");
    const recipeId = container.data("recipeId");

    $.ajax({
        url: `/recipe/${recipeId}/recipeIngridients`,
        type: 'get',
        success: function (data) {
            if (data.length > 0) {
                RenderRecipeIngridients(data, container)
            } else {
                container.html("Brak składników")
            }
        },
        error: function () {
            toastr["error"]("Coś poszło nie tak")
        }
    })
}