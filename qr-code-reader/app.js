/*------------------------------EXPRESS-JS------------------------------*/
const express = require("express");
const app = express();

app.get("/", (request, response) => {
    response.sendFile(__dirname + "/index.html");
});

app.use(express.static(__dirname + "/public"));

/*------------------------------HTTP-SERVER------------------------------*/
const http = require("http").createServer(app);
const port = 3001;

http.listen(port, () => {
    console.log(`[QR CODE READER STARTED AT PORT ${port}]`);
});
/*------------------------------PUSHER------------------------------*/
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
/*------------------------------------------------------------*/
