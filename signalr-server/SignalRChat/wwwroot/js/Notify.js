//self-hosted
let connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

// General
connection.on("Notification", function (message) {
    $('#mensajes').append('<p class="alert alert-info">' + message + '<p>')
});

// Only Server
connection.on("Disconnect", function (message) {
    $('#mensajes').append('<p class="alert alert-danger">' + message + '<p>')
});

connection.on("Connect", function (message) {
    $('#mensajes').append('<p class="alert alert-success">' + message + '<p>')
});

// Conecta
connection.start()
    .then(function () {
        console.log("Servidor iniciado");
    })
    .catch(function (err) {
        return console.error(err.toString());
    });



$('#btn-sendMessage').click(function (event) {
    let message = $('#url_input').val();
    // Send with SignalR
    connection.invoke("SendNotify", message)
        .then(function () {
            console.log("Enviado");
        })
        .catch(function (err) {
            //error print
            return console.error(err.toString());
        });
});


$('#btn-sendIDS').click(function (event) {

    // Send with SignalR
    connection.invoke("SendIds")
        .then(function () {
            console.log("Enviado");
        })
        .catch(function (err) {
            //error print
            return console.error(err.toString());
        });
});