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
/*------------------------------------------------------------*/