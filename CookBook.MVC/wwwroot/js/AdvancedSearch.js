var ingList = [];
var askBeforeReload = true;

$(document).ready(function () {
    $("#searchlistIngridients").select2({
        theme: 'bootstrap-5'
    });
    FillData("INGR",$("#searchlistIngridients"), null, "", "");//populate datalist with ingridients names

    searchNameChange();//ustawia eventy on na nameSearch

    $("#categoryBadge").attr("hidden", true);
    $("#ingBadge").attr("hidden", true);
    $("#otherBadge").attr("hidden", true);
    $("#ingSearchList").attr("hidden", true);

    const inSearchMode = $("#inSearchMode").val();
    const ingString = $("#ingridientList").val();
    const catString = $("#choosenCategories").val();
    const otherString = $("#otherFilters").val();
    const searchPhrase = $("#searchPhrase").val();
    if (inSearchMode == "1") {
        if (catString != null && catString.length > 0) setCategorySearch(catString);
        if (ingString != null && ingString.length > 0) recreateIngList(ingString);
        if (otherString != null && otherString.length > 0) setOtherSearch(otherString);
        if (searchPhrase.length > 0) $("#nameInput").val(searchPhrase).trigger('change');
        
        const currentMode = $("#searchMode").val();
        switch (currentMode) {
            case "0": $("#allRecipeRadio").attr('checked', true).trigger('change'); break;
            case "1": $("#allRadio").attr('checked', true).trigger('change'); break;
            case "2": $("#onlyRadio").attr('checked', true).trigger('change'); break;
            default: break;
        }
        searchRecipes();
    }

    hideClearAllButton(ingList.length ? false : true);

    window.onbeforeunload = function () {
        if (ingList.length > 0 && askBeforeReload) {
            return true;
        } else {
            return void (0);
        }
    };
});

//metoda do ukrycia/pokazania przycisku do usuwania wszystkich sk³adnikó z listy
function hideClearAllButton(b) {
    $("#clearList").attr("hidden", b);
}

//metoda dodaj¹ca sk³adnik do wyszukania
const addToSearchList = () => {
    const ingVal = $("#searchlistIngridients").val();
    const ingToAdd = (ingVal == null) ? null : $.trim(ingVal.toLowerCase());
    if (ingVal != null) {
        if (ingList.includes(ingToAdd)) {
            toastr.options = {
                "closeButton": true
            }
            toastr["warning"]("Sk&#322;adnik jest ju&#380; na li&#347;cie");
            return;
        }
    }
   
    $.ajax({
        url: `/search/addIngToList/?ingridient=${ingToAdd}`,
        type: 'get',
        success: function () {
            ingList.push(ingToAdd);
            hideClearAllButton(!ingList.length);
            const container = $("#searchListTable");
            RenderSearchList(ingToAdd, container);
        },
        error: function (xhr) {
            toastr.options = {
                "closeButton": true
            }
            toastr["warning"](xhr.responseText)
        }
    })
}

//metoda wywo³uj¹ca wyszukiwanie
const searchRecipes = () => {
    //wyszukiwanie po nazwie
    var searchPhrase = $("#nameInput").val();
    searchPhrase = (searchPhrase == null) ? "" : searchPhrase;

    var catString = "";
    //zapisujemy do tablicy wszystkie wybrane kategorie i tworzymy z nich string
    const selectedCategories = $.map($('input[name="categories"]:checked'),function (c) { return c.value; });
    for (i = 0; i < selectedCategories.length; i++) {
        catString += selectedCategories[i];
        if (i < selectedCategories.length - 1)
            catString += ";";
    }

    //tworzymy string ze sk³adnikami mode;ing1;ing2...
    //jeœli nie wyszukujemy po sk³adnikach to string=0
    const selectedMode = $('input[name="searchType"]:checked').val();
    if (selectedMode != 0 && ingList.length == 0) {
        toastr.options = {
            "closeButton": true
        }
        toastr["warning"]("Dodaj sk&#322;adniki do wyszukania.")
        return;
    }
    askBeforeReload = false;
    var ingString = selectedMode;
    if (selectedMode != 0) {
        for (i = 0; i < ingList.length; i++) {
            ingString += ";" + ingList[i];
        }
    }

    //tworzymy string z dodatkowymi filtrami np. true;false;false... (true jeœli zaznaczony)
    //kolejnoœæ: isVege;
    var otherString = "";
    otherString += $('#onlyVege').is(":checked");

    var queryString = "searchName=" + searchPhrase;
    queryString += "&categories=" + catString;
    queryString += "&ings=" + ingString;
    queryString += "&others=" + otherString;

    var url = "/Recipe/AdvancedSearch?" + queryString;

    window.history.replaceState(null, null, url);

    $.ajax({
        url: `/recipe/search/advanced`,
        type: 'get',
        data: { 'searchName': searchPhrase, 'categories': catString, 'ings': ingString, 'others': otherString },
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data) {
            var container = $("#advSearchResults");
            container.empty();
            $("#searchInfo").empty();
            if (data.length == 0) {
                $("#searchInfo").append(`<div class="btn btn-outline-primary mb-2 disabled col-12">Brak przepis&#243;w spe&#322;niaj&#261;cych podane kryteria</div>`);
            } else if (data.length == 500) {
                $("#searchInfo").append(`<div class="btn btn-outline-primary mb-2 disabled col-12">Zbyt wiele przepis&#243;w spe&#322;niaj&#261;cych podane kryteria. Wy&#347;wietlam pierwszych 500.</div>`);
                RenderRecipeCard(data, container);
            } else {
                $("#searchInfo").append(`<div class="btn btn-secondary mb-2 disabled col-12" id="searchInfo">Znalezione przepisy: ` + data.length + `</div>`);
                RenderRecipeCard(data, container);
            }
        },
        error: function (xhr) {
            toastr["error"](xhr.responseText);
        }
    })
}

//usuwanie pojedynczego s³adnika
const deleteFromSearchList = (container) => {
    const row = container.parentNode.parentNode;
    row.parentNode.removeChild(row);
    const ing = $.trim(row.children[0].innerText);
    const index = ingList.indexOf(ing);
    ingList.splice(index, 1);
    hideClearAllButton(!ingList.length);
}

//czyszczenie ca³ej listy
const clearIngList = () => {
    ingList = [];
    $("#searchListTable tr").remove();
    hideClearAllButton(true);
}

//liczy iloœæ zaznaczonych kategorii i uaktualnia badge kategorii
const categoryCheckChange = () => {
    const checkCount = document.querySelectorAll('input[type="checkbox"][name="categories"]:checked').length;
    $("#categoryBadge").html(`<strong>` + checkCount + `</strong>`);
    $("#categoryBadge").attr("hidden", checkCount == 0);
}

//ustawia badge wyszukiwania po sk³adnikach oraz ukrywanie dodawania sk³adników do listy
const changeMode = () => {
    const selectedMode = $('input[name="searchType"]:checked').val();
    $("#ingBadge").empty();
    $("#ingBadge").append(`<i class="bi bi-check-lg"></i>`); 
    $("#ingBadge").attr("hidden", selectedMode == 0);
    $("#ingSearchList").attr("hidden", selectedMode == 0);
    if (selectedMode == 0) {
        askBeforeReload = false;
        $("#searchlistIngridients").val("0").change();
    } else {
        askBeforeReload = true;
    }
}

//ustawia badge wyszukiania po nazwie, wywo³ywane w document.ready
const searchNameChange = () => {
    $("#nameInput").on('change keydown paste input', function () {
        const searchPhrase = $("#nameInput").val();
        if (searchPhrase != null && searchPhrase.length > 0) {
            $("#nameBadge").html(`<i class="bi bi-check-lg"></i>`);
        } else {
            $("#nameBadge").html("");
        }
    });
}

//liczy iloœæ zaznaczonych dodatkowych fitrów
const othersCheckChange = () => {
    const checkCount = document.querySelectorAll('input[type="checkbox"][name="otherFilters"]:checked').length;
    $("#otherBadge").html(`<strong>` + checkCount + `</strong>`);
    $("#otherBadge").attr("hidden", checkCount == 0);
}

//ukrywa/odkrywa pozosta³e filtry (efekt akordeonu)
const collapseFilters = (container) => {
    const aid = container.id;
    if (aid == "heading-ings") {
        $('#categoryCollapse').collapse("hide");
        $('#otherCollapse').collapse("hide");
        $('#nameCollapse').collapse("hide");
    } else if (aid == "heading-categories") {
        $('#ingCollapse').collapse("hide");
        $('#otherCollapse').collapse("hide");
        $('#nameCollapse').collapse("hide");
    } else if (aid == "heading-others") {
        $('#ingCollapse').collapse("hide");
        $('#categoryCollapse').collapse("hide");
        $('#nameCollapse').collapse("hide");
    } else if (aid == "heading-name") {
        $('#ingCollapse').collapse("hide");
        $('#categoryCollapse').collapse("hide");
        $('#otherCollapse').collapse("hide");
    }
}

//odtwarzanie listy sk³adników do wyszukania
const recreateIngList = (ingString) => {
    const moreThanOneIng = ingString.includes(";");
    const currentList = moreThanOneIng ? ingString.split(";") : ingString;
    if (currentList != null && ingString.length > 0) {
        if (moreThanOneIng)
            ingList = currentList;
        else
            ingList[0] = currentList;
        const container = $("#searchListTable");
        for (i = 0; i < ingList.length; i++) {
            RenderSearchList(ingList[i], container);
        }
    }
}

//tworzymy tablicê zaznaczonych nazw kategorii i ustawiamy wyszukiwanie
const setCategorySearch = (catString) => {
    const moreThanOneCat = catString.includes(";");
    const catList = moreThanOneCat ? catString.split(";") : catString;
    $('input[type="checkbox"][name="categories"]').each(function (index, obj) {
        if (catList.indexOf($(this).val()) >= 0) {
            $(this).attr('checked', true).trigger('change');
        }
    });
}

//tworzymy tablicê pozosta³ych filtrów i ustawiamy wyszukiwanie
const setOtherSearch = (otherString) => {
    const moreThanOneOther = otherString.includes(";");
    const otherList = moreThanOneOther ? otherString.split(";") : otherString;
    if (otherList == "true")
        $("#onlyVege").attr('checked', true).trigger('change');
}