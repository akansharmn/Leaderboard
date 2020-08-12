using System.Collections.Generic;


namespace BoardAPI.Models
{
    public class SampleGame
    {
        public List<ParticipantInfo> participantInfo { get; set; }

        public int ranks { get; set; }

        public int checkpoint { get; set; }
    }
}
