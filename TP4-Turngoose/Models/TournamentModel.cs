using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP4_Turngoose.Models
{
    public class TournamentModel
    {
        public string TournamentName { get; set; }
        public DateTime TournamentDate { get; set; }
        public Boolean DoubleElimination { get; set; }
        public string Administrator { get; set; }
        public List<ParticipantModel> WinnerParticipants { get; set; }
        public List<ParticipantModel> LoserParticipants { get; set; }

        public TournamentModel() : this("", new DateTime(), false, "", new List<ParticipantModel>(), new List<ParticipantModel>()) { }

        public TournamentModel(string tournamentName, DateTime tournamentDate, Boolean doubleElimination, string administrator,
            List<ParticipantModel> winnerParticipants, List<ParticipantModel> loserParticipants)
        {
            TournamentName = tournamentName;
            TournamentDate = tournamentDate;
            DoubleElimination = doubleElimination;
            Administrator = administrator;
            WinnerParticipants = winnerParticipants;
            LoserParticipants = loserParticipants;
        }

        public void AddParticipant(string name = "", string sponsor = "", string team = "", int score = 0, int seed = 0)
        {
            WinnerParticipants.Add(new ParticipantModel(WinnerParticipants.Count, name, sponsor, team, score, seed));
        }

        public void AddParticipantLoserBracket(int id)
        {
            LoserParticipants.Add(WinnerParticipants.Single(x => x.ID == id));
        }

        public void RandomizeSeed()
        {
            Random rand = new Random();
            foreach (ParticipantModel p in WinnerParticipants)
            {
                p.Seed = rand.Next();
            }
        }

        public void SortByID()
        {
            WinnerParticipants.OrderBy(x => x.ID);
        }

        public void SortBySeed()
        {
            WinnerParticipants.OrderByDescending(x => x.Seed);
            WinnerParticipants = SortSeeds(WinnerParticipants);
        }

        private List<ParticipantModel> SortSeeds(List<ParticipantModel> participants)
        {
            if(participants.Count > 3) 
            {
                List<ParticipantModel> list1 = new List<ParticipantModel>();
                List<ParticipantModel> list2 = new List<ParticipantModel>();
                foreach (ParticipantModel p in participants)
                {
                    if (list1.Count <= list2.Count)
                        list1.Add(p);
                    else
                        list2.Add(p);
                }
                list1 = SortSeeds(list1);
                list2 = SortSeeds(list2);
                list1.AddRange(list2);
                return list1;
            }
            return participants;
        }
    }

    
}