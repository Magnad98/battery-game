using System;
using System.Threading.Tasks;
using PusherClient;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class PusherManager : MonoBehaviour
{
    // A mutation of https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
    public static PusherManager instance = null;
    private Pusher _pusher;
    private Channel _channel;
    private const string APP_KEY = "1f05233fe6658e7cb61e";
    private const string APP_CLUSTER = "eu";

    async Task Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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
            JObject json2 = JObject.Parse(JObject.Parse(data)["data"].ToString());

            int NineVolt, D, C, AA, AAA, Cell;
            Int32.TryParse(json2["NineVolt"].ToString(), out NineVolt);
            Int32.TryParse(json2["D"].ToString(), out D);
            Int32.TryParse(json2["C"].ToString(), out C);
            Int32.TryParse(json2["AA"].ToString(), out AA);
            Int32.TryParse(json2["AAA"].ToString(), out AAA);
            Int32.TryParse(json2["Cell"].ToString(), out Cell);

            if (AA >= 3 && AAA >= 2)
            {
                Debug.Log("Congratulations! You've unlocked Level 6");
                // Add Level 6 permissions
            }
            else if (NineVolt + D + C + AA + AAA + Cell >= 10)
            {
                Debug.Log("Congratulations! You've unlocked Level 7");
                // Add Level 7 permissions
            }
            else
            {
                Debug.Log("We're happy that you help us recycle!\n But unfortunately your betteries weren't enough to unlock a new level!");
            }
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
