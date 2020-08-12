using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data
{
    public class Participant : IComparable<Participant>
    {
        public string name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid id { get; set; }

        public int? CheckPoint { get;  set; }

        public DateTimeOffset? StartTime { get;  set; }

        public DateTimeOffset? CheckPointTime { get;  set; }

        public Participant(string name, DateTime dateOfBirth)
        {
            this.name = name;
            this.DateOfBirth = dateOfBirth;
            this.id = Guid.NewGuid();
        }

        public bool UpdateCheckPoint(DataPoint data)
        {
            if(data.participantId == this.id )
            {
                if(this.CheckPoint == null)
                {
                    this.CheckPoint = data.checkPoint;
                    this.CheckPointTime = data.checkPointTime;
                    this.StartTime = data.checkPointTime;
                    return true;
                } else if (data.checkPoint > this.CheckPoint)
                {
                    this.CheckPoint = data.checkPoint;
                    this.CheckPointTime = data.checkPointTime;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public int CompareTo(Participant other)
        {
            if (this.CheckPoint == null && other.CheckPoint == null)
                return 0;
            if (this.CheckPoint == null)
                return -1;
            if (other.CheckPoint == null)
                return 1;

            if (this.CheckPoint != other.CheckPoint)
            {
                return this.CheckPoint.Value.CompareTo(other.CheckPoint.Value);
            }
            else
            {
                return (other.CheckPointTime.Value - other.StartTime.Value).CompareTo(this.CheckPointTime.Value - this.StartTime.Value);
            }
        }
    }
}
