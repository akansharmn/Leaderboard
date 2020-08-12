using Common.Data;
using System.Collections.Generic;

namespace BoardAPI.Models
{
    public class GameInfo
    {
        public List<Participant> participants { get; set; }

        public int ranks { get; set; }

        public int checkpoint { get; set; }
    }
}
