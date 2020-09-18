var jsonSettings = {
    "url": "https://localhost:5001/api/Page/v3.0-patch0/Admin/Home",
    "dataType": "json",
    "method": "GET",
    "headers": {
        "accept": "application/json"
    }
};

$.ajax(jsonSettings).done(function (response) {
    document.getElementById("json").innerHTML = JSON.stringify(response, null, 4);
});