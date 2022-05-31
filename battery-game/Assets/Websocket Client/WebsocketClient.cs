using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using UnityEngine;

public class WebsocketClient : MonoBehaviour
{
    WebSocket websocket;
    int port = 3002;
    // Start is called before the first frame update
    void Start()
    {
        websocket = new WebSocket("ws://localhost:" + port);
        websocket.OnMessage += (sender, e) =>
        {
            Debug.Log("Message received from " + ((WebSocket)sender).Url + ", Data: " + e.Data);
        };
        websocket.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (websocket == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            websocket.Send("Hello");
        }
    }
}