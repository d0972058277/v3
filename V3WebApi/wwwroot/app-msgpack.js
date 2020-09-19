var msgPackSettings = {
    "url": "https://localhost:5001/api/Condition/v3.0-patch0/FakeDefined?number=2",
    "dataType": "arraybuffer",
    "method": "GET",
    "headers": {
        "accept": "application/x-msgpack"
    }
};

$.ajax(msgPackSettings).done(function (response) {
    var bytes = new Uint8Array(response);
    var obj = msgpack.deserialize(bytes);
    document.getElementById("json").innerHTML = JSON.stringify(obj, null, 4);
});