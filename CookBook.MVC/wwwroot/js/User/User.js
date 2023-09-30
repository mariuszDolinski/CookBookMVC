$(document).ready(function () {
    const searchRole = $("#searchRoleInput").val();
    $("#selectedRole").val(searchRole).change();
});

//wywo³anie akcji zmiany ról przez ajax
const EditUserRoles = (allRoles) => {
    const userName = $("#selectedUserName").val();
    const selectedRoles = GetChoosenRoles(allRoles);
    const parameters = userName + ";" + selectedRoles;
    $.ajax({
        url: `/user/editRoles/${parameters}`,
        type: 'put',
        success: function () {
            $("#editRolesModal").modal('hide');
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
//wyœwietla modal do zmiany ról
const ShowEditRolesModal = (userName, userRoles, allRoles) => {
    $("#selectedUserName").val(userName);
    $("#editRolesModal").modal('show');
    UncheckedRoles(allRoles);
    if (userRoles != "") {
        const roles = userRoles.split(";");
        for (i = 0; i < roles.length; i++) {
            const roleId = "role" + roles[i] + "Check"
            $(`#${roleId}`).prop('checked', true);
        }
    }
}
//odznacza wszystkie checkboxy w modalu ze zmian¹ ról
const UncheckedRoles = (allRoles) => {
    const roles = allRoles.split(";");
    for (i = 0; i < roles.length; i++) {
        const roleId = "role" + roles[i] + "Check"
        $(`#${roleId}`).prop('checked', false);
    }
}

const GetChoosenRoles = (allRoles) => {
    var newRoles = "";
    const roles = allRoles.split(";");
    for (i = 0; i < roles.length; i++) {
        const roleId = "role" + roles[i] + "Check"
        if ($('#' + roleId).is(":checked")) {
            newRoles += roles[i];
            newRoles += ";"
        }          
    }
    return newRoles.slice(0, -1);
}