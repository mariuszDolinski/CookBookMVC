//populate datalists with ingridients and units names
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
    //container.append(`<ul>`)
    for (const item of data) {
        container.append(
            `<i class="bi bi-basket">  ${item.description}</i>`)
    }
    //container.append(`</ul>`)
}

const LoadRecipeIngridients = () => {
    const container = $("#ingridientsList");
    const rrecipeId = container.data("recipeId");

    $.ajax({
        url: `/recipe/${rrecipeId}/recipeIngridients`,
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