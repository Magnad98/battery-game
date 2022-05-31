const websocket = require('ws')
const port = 3002
const websocketServer = new websocket.Server({port: port}, () => {
    console.log(`WEBSOCKET SERVER STARTED`)
})

websocketServer.on('connection', (websocket) => {
    websocket.on('message', (data) => {
        console.log('data received %o' + data)
        websocket.send(data)
    })

})
websocketServer.on('listening', () => {
    console.log(`WEBSOCKET SERVER STARTED AT PORT ${port}]`)
})