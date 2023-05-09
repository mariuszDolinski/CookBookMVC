﻿//populate datalists with ingridients and units names
const FillData = () => {
    const containerIngridient = $("#datalistIngridients")
    const containerUnit = $("#datalistUnits")

    $.ajax({
        url: `/recipe/getDatalists`,
        type: 'get',
        success: function (data) {
            containerIngridient.empty();
            containerUnit.empty();
            for (const item of data.ingridients)
                containerIngridient.append(`<option>${item}</option>`);
            for (const item of data.units)
                containerUnit.append(`<option>${item}</option>`);
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
                `<i class="bi bi-basket">  ${item.description}</i>`)
        }
    }
    if (container.attr('name') == "edit") {
        for (const item of data) {
            container.append(
                `<tr id="row-${item.id}">
                    <td>${item.description}</td>
                    <td>
                        <a role="button" class="btn-outline-danger btn-sm" onclick="DeleteIng(${item.id})"><i class="bi bi-trash"></i></a>
                    </td>
                </tr>`
            )
        }
        //link do wyświetlania moadala
        //<a role="button" class="btn-outline-primary btn-sm" onclick="ShowEditModal(${item.id})"><i class="bi bi-pencil-square"></i></a>
    }
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