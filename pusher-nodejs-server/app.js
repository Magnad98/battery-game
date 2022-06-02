const Pusher = require("pusher");

const pusher = new Pusher({
  appId: "1414405",
  key: "1f05233fe6658e7cb61e",
  secret: "26e019fe152473429ae2",
  cluster: "eu",
  useTLS: true
});

// const json = 

pusher.trigger("my-channel", "my-event", {
  message: "bruh"
});