using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public partial class SignalRUnityController : MonoBehaviour
{
    public string signalRURL;
    public GameObject UIText;

    private HubConnection hubConnection = null;
    private TextController textController;

    // Start is called before the first frame update
    async void Start()
    {
        this.textController = this.UIText.GetComponent<TextController>();

        var signalRConnData = await this.GetSignalRConnDataAsync();

        await this.StartSignalRAsync(signalRConnData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async Task StartSignalRAsync(SignalRConnData signalRConnData)
    {
        if (this.hubConnection == null)
        {
            this.hubConnection = new HubConnectionBuilder()
                .WithUrl(signalRConnData.url, options => {
                    options.AccessTokenProvider = async () => signalRConnData.accessToken;
                })
                .Build();

            this.hubConnection.On<TempObject>("newMessage", (tempObject) =>
            {
                Debug.Log("Message received ...");

                this.textController.SetTempObject(tempObject);

                //this.Invoke((Action)(() =>
                //{
                //    this.textBox1.AppendText($"Message: {newMessage.sender}" +
                //                             $"- {newMessage.temperature}" +
                //                             $"- {newMessage.humidity}" +
                //                             $"- {newMessage.time}");
                //    this.textBox1.AppendText(Environment.NewLine);
                //}));
            });

            await this.hubConnection.StartAsync();

            Debug.Log("Signalr connected ...");
        }
        else
        {
            Debug.Log("Signalr already connected ...");
        }
    }

    private async Task<SignalRConnData> GetSignalRConnDataAsync()
    {
        // Read this article to find out more info about Azure Functions and SignalR Service 
        // https://docs.microsoft.com/en-us/azure/azure-signalr/signalr-quickstart-azure-functions-csharp
        var jsonResponse = await this.GetAsync(this.signalRURL);

        return JsonConvert.DeserializeObject<SignalRConnData>(jsonResponse);
    }

    private async Task<string> GetAsync(string uri)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            return await reader.ReadToEndAsync();
        }
    }
}
