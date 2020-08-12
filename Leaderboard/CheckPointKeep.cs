using Common.Data;
using System;
using System.Collections.Generic;

namespace Leaderboard
{
    public class CheckPointKeep
    {
        private int ranks;
        private int checkPoints;

        public CheckPointKeep(int ranks, int checkPoints)
        {
            this.ranks = ranks;
            this.checkPoints = checkPoints;
        }

        internal List<Participant> GetLastParticipants(int checkPoint)
        {
            throw new NotImplementedException();
        }

        internal void UpdateParticipant(Participant participant)
        {
            throw new NotImplementedException();
        }
    }
}