using System;
using System.Threading.Tasks;
using PusherClient;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class PusherManager : MonoBehaviour
{
    GameManager gameManager;
    // A mutation of https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
    public static PusherManager instance = null;
    private Pusher _pusher;
    private Channel _channel;
    private const string APP_KEY = "1f05233fe6658e7cb61e";
    private const string APP_CLUSTER = "eu";

    async Task Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        if (instance == null)
        {
            instance = this;
        }
        // else if (instance != this)
        // {
        //     Destroy(gameObject);
        // }
        // DontDestroyOnLoad(gameObject);
        await InitialisePusher();
        Console.WriteLine("Starting");
    }

    private async Task InitialisePusher()
    {
        //Environment.SetEnvironmentVariable("PREFER_DNS_IN_ADVANCE", "true");

        if (_pusher == null && (APP_KEY != "APP_KEY") && (APP_CLUSTER != "APP_CLUSTER"))
        {
            _pusher = new Pusher(APP_KEY, new PusherOptions()
            {
                Cluster = APP_CLUSTER,
                Encrypted = true
            });

            _pusher.Error += OnPusherOnError;
            _pusher.ConnectionStateChanged += PusherOnConnectionStateChanged;
            _pusher.Connected += PusherOnConnected;
            _channel = await _pusher.SubscribeAsync("my-channel");
            _pusher.Subscribed += OnChannelOnSubscribed;
            await _pusher.ConnectAsync();
        }
        // else
        // {
        //     Debug.LogError("APP_KEY and APP_CLUSTER must be correctly set. Find how to set it at https://dashboard.pusher.com");
        // }
    }

    private void PusherOnConnected(object sender)
    {
        // Debug.Log("Connected");
        _channel.Bind("my-event", (string data) =>
        {
            JObject json = JObject.Parse(JObject.Parse(data)["data"].ToString());
            // gameManager.AddBatteries(
            //     Int32.Parse(json["NineVolt"].ToString()),
            //     Int32.Parse(json["C"].ToString()),
            //     Int32.Parse(json["D"].ToString()),
            //     Int32.Parse(json["AA"].ToString()),
            //     Int32.Parse(json["AAA"].ToString()),
            //     Int32.Parse(json["Cell"].ToString())
            // );
        });
    }

    private void PusherOnConnectionStateChanged(object sender, ConnectionState state)
    {
        // Debug.Log("Connection state changed");
    }

    private void OnPusherOnError(object s, PusherException e)
    {
        // Debug.Log("Errored");
    }

    private void OnChannelOnSubscribed(object s, Channel channel)
    {
        // Debug.Log("Listening");
    }

    async Task OnApplicationQuit()
    {
        if (_pusher != null)
        {
            await _pusher.DisconnectAsync();
        }
    }
}
