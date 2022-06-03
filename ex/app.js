const express = require('express')
const app = express();
const PORT = 3002

app.use(express.json())

app.listen(PORT, () => {
    console.log(`it's alive on http://localhost:${PORT}`)
})

app.post('/pusher', (req, res) => {
    const Pusher = require("pusher")

    const pusher = new Pusher({
        appId: "1414405",
        key: "1f05233fe6658e7cb61e",
        secret: "26e019fe152473429ae2",
        cluster: "eu",
        useTLS: true
    });

    data = req.body

    pusher.trigger("my-channel", "my-event", data).then(() => {
        res.status(200).send("sent pusher request succesfully")
    })
})

// app.get('/pushergen', (req, res) => {
//     const Pusher = require("pusher")

//     const pusher = new Pusher({
//         appId: "1414405",
//         key: "1f05233fe6658e7cb61e",
//         secret: "26e019fe152473429ae2",
//         cluster: "eu",
//         useTLS: true
//     });

//     data = {
//         message: "bruh"
//     }

//     pusher.trigger("my-channel", "my-event", data).then(() => {
//         res.status(200).send("sent pusher request succesfully")
//     })
// })

// app.post('/tshirt/:id', (req, res) => {
//     const { id } = req.params;
//     const { logo } = req.body;

//     if(!logo) {
//         res.status(418).send({message: 'We need a logo!'})
//     }

//     res.send({
//         tshirt: `tshirt with your ${logo} and ID ${id}`,
//     })
// })