using Common.data;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Leaderboard
{
    public class HubManager : IDisposable
    {
        HubConnection hubConnection;
       // IHubProxy hubProxy;

        public  HubManager()
        {
            var ar = new int[] { };
          
            var baseUrl = "http://localhost:5000/TopPerformersHub";

            //this.hubConnection = new HubConnection(baseUrl);
            //this.hubProxy = this.hubConnection.CreateHubProxy("TopPerformersHub");
            this.hubConnection = new HubConnectionBuilder().WithUrl(baseUrl)
                                              .Build();


            //this.hubConnection.Start().ContinueWith(task =>
            //{
            //    if (task.IsFaulted)
            //    {
            //        Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
            //    }
            //    else
            //    {
            //        Console.WriteLine(task.Status);
            //    }
            //}).Wait();


            //hubProxy.Invoke<string>("RegisterAsSender").ContinueWith(task =>
            //{
            //    if (task.IsFaulted)
            //    {
            //        Console.WriteLine("There was an error calling send: {0}",
            //                          task.Exception.GetBaseException());
            //    }
            //    else
            //    {
            //        Console.WriteLine(task.Result);
            //    }
            //}).Wait();

        }

        public async Task Initilize()
        {
            Console.WriteLine("Initializing Hub...");
            await this.hubConnection.StartAsync();
            Console.WriteLine("Connection established");
            //await hubProxy.Invoke<string>("RegisterAsSender");
            await this.hubConnection.InvokeAsync<string>("RegisterAsSender");
            Console.WriteLine("Registered as Sender");

        }

        public async void Dispose()
        {
            await this.hubConnection.StopAsync();
        }

        public async Task UpdateTopPerformers(IList<Performer> performers)
        {
            //await hubProxy.Invoke<string>("UpdateTopPerformers", performers);
            string serializedPerformers = JsonConvert.SerializeObject(performers);
            Console.WriteLine($"Updating top performers {serializedPerformers}");
            await this.hubConnection.InvokeAsync<string>("UpdateTopPerformers", performers);
            Console.WriteLine("Updated receiver with top performers");
        }
    }
}
