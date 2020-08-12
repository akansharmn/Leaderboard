using Common.data;
using Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leaderboard
{
    public class ParticipantEquater : IEqualityComparer<Participant>
    {
        public bool Equals(Participant x, Participant y)
        {
            return x.id.Equals(y.id);
        }

        public int GetHashCode(Participant obj)
        {
            return obj.id.GetHashCode();
        }
    }

    public class WinBoard
    {
        private int ranks;
        private MinHeap<Participant> board;

        public WinBoard(int ranks)
        { 
            this.ranks = ranks;
            this.board = new MinHeap<Participant>(ranks, new ParticipantEquater());
        }

        public List<Performer> GetTopParticipants()
        {
            var topParticipants = board.GetElements().ToList();
            topParticipants.Sort();
            var performers = topParticipants.Select(
                x => new Performer
                {
                    name = x.name,
                    checkpoint = x.CheckPoint.Value,
                    timeElapsed = x.CheckPointTime.Value.Subtract(x.StartTime.Value).TotalSeconds,
                    age = DateTime.Today.Year - x.DateOfBirth.Year
                }).ToList();
           
            return performers;
        }

        public bool UpdateParticipant(Participant participant)
        {
            return board.Add(participant);
        }
    }
}
