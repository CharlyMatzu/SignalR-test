
setConnection();


/**
 * 
 * @param {String} hostUrl 
 * @param {boolean} retry 
 */
function setConnection(){

    // setting up
    let connection = new signalR.HubConnectionBuilder()
        .withUrl( 'http://localhost:1934/NotificationHub')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    // Connect
    connection.start()
        .then( function () {
            // console.log("Connected");
        })
        .catch(function ( err ) {
            receiver( "Error: " + err.toString() );
            console.error( err.toString() +" - "+ host );
        });

    //Listen notifications
    connection.on("Notification", ( message ) => {
        receiver(message);
    });
}

