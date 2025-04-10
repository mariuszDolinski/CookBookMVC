const ShowDetailsModal = (name, createTime, author) => {
    $("#itemName").html('𝗡𝗮𝘇𝘄𝗮: ' + name);
    $("#itemCreateDate").html('𝗗𝗮𝘁𝗮 𝘂𝘁𝘄𝗼𝗿𝘇𝗲𝗻𝗶𝗮: ' + createTime);
    $("#itemCreateBy").html('𝗔𝘂𝘁𝗼𝗿: ' + author);
    $("#detailsItemModal").modal('show');
}