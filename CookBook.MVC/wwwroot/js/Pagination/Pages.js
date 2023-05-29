const ChangePageSize = (obj, controller, action, sortOrder, search) => {
    window.location.href = "/" + controller + action + "?page=1&pageSize=" + obj.value
        + "&sortOrder=" + sortOrder + "&search=" + search;
}