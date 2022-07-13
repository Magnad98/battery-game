using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;
using PusherClient;
using UnityEngine;

public class PusherManager : MonoBehaviour
{
    GameManager gameManager;

    const string APP_KEY = "1f05233fe6658e7cb61e", APP_CLUSTER = "eu", CHANNEL = "my-channel", EVENT = "my-event";
    public static PusherManager instance;
    public Pusher pusher;
    Channel channel;

    async Task Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        instance = this;
        await InitialisePusher();
    }

    private async Task InitialisePusher()
    {
        pusher = new Pusher(APP_KEY, new PusherOptions() { Cluster = APP_CLUSTER, Encrypted = true });
        pusher.Connected += PusherOnConnected;
        channel = await pusher.SubscribeAsync(CHANNEL);
        await pusher.ConnectAsync();
    }

    private void PusherOnConnected(object sender)
    {
        channel.Bind(EVENT, (string data) =>
        {
            JObject json = JObject.Parse(JObject.Parse(data)["data"].ToString());

            gameManager.AddBatteries(
                Int32.Parse(json["NineVolt"].ToString()),
                Int32.Parse(json["C"].ToString()),
                Int32.Parse(json["D"].ToString()),
                Int32.Parse(json["AA"].ToString()),
                Int32.Parse(json["AAA"].ToString()),
                Int32.Parse(json["Cell"].ToString())
            );
        });
    }
}
