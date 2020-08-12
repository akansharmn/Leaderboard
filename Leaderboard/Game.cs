using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.data;
using Common.Data;
using KafkaConsumer;
using Newtonsoft.Json;

namespace Leaderboard
{
    public class Game
    {
        public WinBoard leaderboard;

        public CheckPointKeep checkPointKeep;

        public Consumer<string, string> consumer;

        private string topicName;
        private int ranks;

        HubManager hubManager;

        public Game()
        {
            this.hubManager = new HubManager();
            this.topicName = "leaderboard-new";
            this.consumer = new Consumer<string, string>("group", "localhost:9092", this.topicName);
           
            this.ranks = 10;
            leaderboard = new WinBoard(ranks);
            
        }

        public async Task Start()
        {
            await hubManager.Initilize();
            Console.WriteLine("Initialized hubmanager");
            await consumer.StartConsuming(KafkaRecordProcessor);
        }
        public async Task KafkaRecordProcessor(string key, string value)
        {
            try
            {
                var participant = JsonConvert.DeserializeObject<Participant>(value);
                Console.WriteLine($"Received a participant {participant}");
                await PostParticipantInfo(participant);
            } catch(Exception ex)
            {
                Console.WriteLine($"Got exception {ex}");
            }
        }

        public async Task PostParticipantInfo(Participant updatedParticipant)
        {
            Console.WriteLine($"PArticpant is being updated in heap ... {updatedParticipant}");
            if (updatedParticipant != null)
            {
                // checkPointKeep.UpdateParticipant(updatedParticipant);
                if (leaderboard.UpdateParticipant(updatedParticipant))
                {
                    Console.WriteLine("The board was changes... Calling web socket server");
                    await hubManager.UpdateTopPerformers(GetTopParticipants);
                    Console.WriteLine("Participant data was posted");
                }
            }
        }

        public List<Performer> GetTopParticipants => leaderboard.GetTopParticipants();

        public List<Participant> GetLastParticipants(int checkPoint)
        {
            return checkPointKeep.GetLastParticipants(checkPoint);
        }
    }
}
