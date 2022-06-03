/*------------------------------EXPRESS------------------------------*/
const express = require('express')
const app = express()
const PORT = 3001

app.listen(PORT, () => {
    console.log(`[QR CODE READER STARTED AT http://${getLocalIP()}:${PORT}]`)
})

app.get('/', (request, response) => {
    response.sendFile(__dirname + '/index.html')
})

app.use(express.static(__dirname + '/public'))

/*------------------------------PUSHER------------------------------*/
app.use(express.json())

app.post('/pusher', (req, res) => {
    const Pusher = require('pusher')

    const pusher = new Pusher({
        appId: '1414405',
        key: '1f05233fe6658e7cb61e',
        secret: '26e019fe152473429ae2',
        cluster: 'eu',
        useTLS: true
    })

    data = req.body

    pusher.trigger('my-channel', 'my-event', data).then(() => {
        res.status(200).send('sent pusher request succesfully')
    })
})

/*------------------------------IPCONFIG------------------------------*/
const getLocalIP = () => {
    'use strict'

    const { networkInterfaces } = require('os')

    const nets = networkInterfaces()
    const results = Object.create(null) // Or just '{}', an empty object

    for (const name of Object.keys(nets)) {
        for (const net of nets[name]) {
            // Skip over non-IPv4 and internal (i.e. 127.0.0.1) addresses
            // 'IPv4' is in Node <= 17, from 18 it's a number 4 or 6
            const familyV4Value = typeof net.family === 'string' ? 'IPv4' : 4
            if (net.family === familyV4Value && !net.internal) {
                if (!results[name]) {
                    results[name] = []
                }
                results[name].push(net.address)
            }
        }
    }
    return results['Wi-Fi'][0]
}

/*------------------------------------------------------------*/
