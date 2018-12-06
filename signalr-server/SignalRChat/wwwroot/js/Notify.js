//self-hosted
let connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

connection.on("Notification", function (message) {
    //console.log("Recibido");
    console.log(message);
});

connection.start()
    .then(function () {
        console.log("Servidor iniciado");
    })
    .catch(function (err) {
        return console.error(err.toString());
    });



$('#printButton').click(function (event) {
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