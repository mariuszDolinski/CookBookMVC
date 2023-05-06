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