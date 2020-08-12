using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data
{
    public struct DataPoint
    {
        public Guid participantId {get; set;}
        public int checkPoint { get; set; }
        public DateTimeOffset checkPointTime { get; set; } 
    }
}
