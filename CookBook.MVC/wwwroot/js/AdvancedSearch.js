var ingList = [];
var askBeforeReload = true;

$(document).ready(function () {
    $("#searchlistIngridients").select2({
        width: '100%',
    });
    FillData($("#searchlistIngridients"), null, "", "");//populate datalist with ingridients names
    //enableInputName();
    //$("#saveSearch").click(enableInputName);  
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
        $("#searchName").removeAttr("disabled");
    } else {
        $("#searchName").attr("disabled", true);
    }
}

function hideClearAllButton(b) {
    $("#clearList").attr("hidden", b);
    console.log("hidden zmieniony")
}

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

const searchRecipes = () => {
    if (ingList.length == 0) {
        toastr.options = {
            "closeButton": true
        }
        toastr["warning"]("Brak sk&#322;adnik&#243;w do wyszukania.")
        return;
    }
    askBeforeReload = false;
    var mode = $('input[name=searchType]:checked').val();
    ingList.push(mode);
    var paramString = "";
    for (i = 0; i < ingList.length; i++) {
        paramString += "args=" + ingList[i];
        if (i < ingList.length - 1)
            paramString += "&";
    }

    window.location.href = "/recipe/search/advanced?" + paramString; 
}

const deleteFromSearchList = (container) => {
    const row = container.parentNode.parentNode;
    row.parentNode.removeChild(row);
    const ing = $.trim(row.children[0].innerText);
    const index = ingList.indexOf(ing);
    ingList.splice(index, 1);
    hideClearAllButton(!ingList.length);
}

const clearIngList = () => {
    ingList = [];
    $("#searchListTable tr").remove();
    hideClearAllButton(true);
}