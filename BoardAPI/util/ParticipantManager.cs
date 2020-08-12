using BoardAPI.Models;
using Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardAPI.Util
{
    public class ParticipantManager
    {
        private Dictionary<Guid, Participant> participants;
        public int Count => participants.Count;
        private List<Participant> participantList;

        public ParticipantManager()
        {
            this.participants = new Dictionary<Guid, Participant>();
            participantList = new List<Participant>();
        }
        public void AddParticipants(List<ParticipantInfo> participantsInfo)
        {

            foreach(var info in participantsInfo)
            {
                var participant = new Participant(info.name, info.dob);
                participants.Add(participant.id, participant);
                participantList.Add(participant);
            }
        }

        public Participant Search(string name, DateTime dob)
        {
            var filteredList = participantList.Where(x => x.name == name && x.DateOfBirth == dob).ToList();

            if (filteredList.Count > 0)
                return filteredList[0];
            else
                return null;

        }

        public Participant UpdateParticipant(DataPoint dataPoint)
        {
            if (participants.ContainsKey(dataPoint.participantId))
            {
                var updateResult = participants[dataPoint.participantId].UpdateCheckPoint(dataPoint);
                if (updateResult)
                    return participants[dataPoint.participantId];
                else
                    return null;
            }
            return null;
        }

        public List<Participant> GetParticipants()
        {
            return participantList;
        }
    }
}
