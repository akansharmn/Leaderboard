using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BoardAPI.Models;
using BoardAPI.Util;
using Common.Data;
using KafkaProducer;
 using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private IMemoryCache cache;
        private static string topicName = "leaderboard-new";

        private static Producer<string, string> producer = new Producer<string, string>("localhost:9092", topicName);
        private static ParticipantManager participantManager = new ParticipantManager();
        public GameController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        /// <summary>
        /// Creates a sample game data. It can be used to create a body for createGame endpoint.
        /// </summary>
        /// <param name="sampleGameInfo">It has details of number of participants, ranks and checkpoints</param>
        /// <returns></returns>
        [HttpPost("sampleGame")]
        public ActionResult<List<ParticipantInfo>> GetSampleGameData([FromBody]SampleGameInfo sampleGameInfo)
        {
            var participantInfo = new List<ParticipantInfo>();
            for (int i = 0; i < sampleGameInfo.participants; i++)
            {
                participantInfo.Add(
                    new ParticipantInfo { name = "participant" + i, dob = DateTime.Today.AddYears(-25 + i) });
            }

            return participantInfo;
        }

        /// <summary>
        /// This gives a sample dataendpoint. This can be used to call postDataPoint endpoint.
        /// </summary>
        /// <returns>Sample data point</returns>
        [HttpGet("sampleDatapoint")]
        public ActionResult<DataPoint> GetSampleDatapoint()
        {
            var participantList = participantManager.GetParticipants();
            if (participantList != null)
            {
                var data = new DataPoint
                {
                    participantId = participantList[0].id,
                    checkPoint = 1,
                    checkPointTime = DateTime.Now.AddMinutes(2)
                };
                return data;
            }
            else
            {
                return Problem("No participant has been added yet");
            }
        }

        /// <summary>
        /// It creates a game and returns it. If a game is already created, it returns error
        /// </summary>
        /// <param name="createGame">It contains details of participants, checkpoints and ranks</param>
        /// <returns>Details of the gae created</returns>
        [HttpPost("createGame")]
        public ActionResult<GameInfo> PostGame([FromBody] List<ParticipantInfo> participantInfo)
        {
            participantManager.AddParticipants(participantInfo);
            return Ok();

        }

       
        /// <summary>
        /// Post a dataPoint which denotes the crossing of a checkpoint by a participant and the time.
        /// </summary>
        /// <param name="dataPoint">Contains participant id, checkpoint crossed lat and the timestamp associated with it</param>
        /// <returns>The status of operation</returns>
        [HttpPost("datapoint")]
        public ActionResult Post([FromBody] DataPoint dataPoint)
        {
            var participant = participantManager.UpdateParticipant(dataPoint);
            var serializedParticipant = JsonConvert.SerializeObject(participant);

            Console.WriteLine($"Datapoint received and serialized. Pushing to topic: {serializedParticipant}");
            producer.Ingest(Guid.NewGuid().ToString(), serializedParticipant);
            return Ok();      
        }

        /// <summary>
        /// Returns the participants list registered for the game
        /// </summary>
        /// <returns>List of participants</returns>
        [HttpGet("participants")]
        public ActionResult<List<Participant>> GetParticipants()
        {
            var participantList = participantManager.GetParticipants();
            if (participantList == null || participantList.Count == 0)
                return NotFound("No participant has been registered yet");

            return Ok(participantList);
        }
    }
}
