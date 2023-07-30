var ingList = [];
var askBeforeReload = true;

$(document).ready(function () {
    FillData();
    enableInputName();
    $("#saveSearch").click(enableInputName);  

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

const addToSearchList = () => {
    const ingToAdd = $("#searchIng").val();
    if (ingList.includes(ingToAdd)) {
        toastr.options = {
            "closeButton": true
        }
        toastr["error"]("Sk&#322;adnik jest ju&#380; na li&#347;cie");
        return;
    }
   
    $.ajax({
        url: `/search/addIngToList/?ingridient=${ingToAdd}`,
        type: 'get',
        success: function () {
            ingList.push(ingToAdd);
            const container = $("#searchListTable");
            RenderSearchList(ingToAdd, container);
        },
        error: function (xhr) {
            toastr.options = {
                "closeButton": true
            }
            toastr["error"](xhr.responseText)
        }
    })
}

const searchRecipes = () => {
    if (ingList.length == 0) {
        toastr.options = {
            "closeButton": true
        }
        toastr["warning"]("Brak sk³adników do wyszukania.")
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
    /*$.ajax({
        url: `/search/advanced`,
        type: 'get',
        traditional: true,
        data: { 'args': ingList },
        success: function (result) {
            window.location = result.url;
        },
        error: function (xhr) {
            toastr.options = {
                "closeButton": true
            }
            toastr["error"](xhr.responseText);
            console.log(xhr.responseText);
        }
    })*/
    
}

const deleteFromSearchList = (container) => {
    const row = container.parentNode.parentNode;
    row.parentNode.removeChild(row);
    const ing = $.trim(row.children[0].innerText);
    const index = ingList.indexOf(ing);
    ingList.splice(index, 1);
}