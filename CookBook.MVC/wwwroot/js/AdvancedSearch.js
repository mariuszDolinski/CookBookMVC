var ingList = [];
var askBeforeReload = true;

//$(function () {
//    $('[data-bs-toggle="popover"]').popover()
//})

$(document).ready(function () {
    $("#searchlistIngridients").select2({
        theme: 'bootstrap-5'
    });
    FillData($("#searchlistIngridients"), null, "", "");//populate datalist with ingridients names

    enableInputName();
    $("#categorySearch").click(enableInputName);

    $("#categoryBadge").attr("hidden", true);
    $("#ingBadge").attr("hidden", true);
    $("#otherBadge").attr("hidden", true);
    $("#ingSearchList").attr("hidden", true);

    const currentString = $("#ingridientList").val();
    const moreThanOneIng = currentString.includes(";");
    const currentList = moreThanOneIng ? currentString.split(";") : currentString;
    if (currentList != null && currentString.length > 0) {
        if (moreThanOneIng)
            ingList = currentList;
        else
            ingList[0] = currentList;
        const container = $("#searchListTable");
        for (i = 0; i < ingList.length; i++) {
            RenderSearchList(ingList[i], container);
        }
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

function enableInputName() {
    if (this.checked) {
        $("#selectedCategories").removeAttr("disabled");
    } else {
        $("#selectedCategories").attr("disabled", true);
    }
}

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

    var queryString = "categories=" + catString;
    queryString += "&ings=" + ingString;
    queryString += "&others=" + otherString;

    $.ajax({
        url: `/recipe/search/advanced`,
        type: 'get',
        data: {'categories': catString, 'ings': ingString, 'others': otherString },
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data) {
            console.log(data.length);
            var container = $("#advSearchResults");
            container.empty();
            if (data.length == 0) {
                container.append(`<div>Brak przepisów spe³niaj¹cych podane kryteria</div>`)
            } else {
                for (const item of data) {
                    container.append(`<div>${item.name}</div>`)
                }
            }
        },
        error: function (xhr) {
            toastr["error"](xhr.responseText);
        }
    })
    //window.location.href = "/recipe/search/advanced?" + queryString; 
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
    $("#categoryBadge").text(checkCount);
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

//liczy iloœæ zaznaczonych dodatkowych fitrów
const othersCheckChange = () => {
    const checkCount = document.querySelectorAll('input[type="checkbox"][name="otherFilters"]:checked').length;
    $("#otherBadge").text(checkCount);
    $("#otherBadge").attr("hidden", checkCount == 0);
}

//ukrywa/odkrywa pozosta³e filtry (efekt akordeonu)
const collapseFilters = (container) => {
    const aid = container.id;
    if (aid == "heading-ings") {
        $('#categoryCollapse').collapse("hide");
        $('#otherCollapse').collapse("hide");
    } else if (aid == "heading-categories") {
        $('#ingCollapse').collapse("hide");
        $('#otherCollapse').collapse("hide");
    } else if (aid == "heading-others") {
        $('#ingCollapse').collapse("hide");
        $('#categoryCollapse').collapse("hide");
    }  
}