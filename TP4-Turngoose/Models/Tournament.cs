using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP4_Turngoose.Models
{
    public class Tournament
    {
        public string TournamentName { get; set; }
        public DateTime TournamentDate { get; set; }
        public Boolean DoubleElimination { get; set; }
        public string Administrator { get; set; }
        public List<Participant> WinnerParticipants { get; set; }
        public List<Participant> LoserParticipants { get; set; }

        public Tournament() : this("", new DateTime(), false, "", new List<Participant>(), new List<Participant>()) { }

        public Tournament(string tournamentName, DateTime tournamentDate, Boolean doubleElimination, string administrator,
            List<Participant> winnerParticipants, List<Participant> loserParticipants)
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
            WinnerParticipants.Add(new Participant(WinnerParticipants.Count, name, sponsor, team, score, seed));
        }

        public void AddParticipantLoserBracket(int id)
        {
            LoserParticipants.Add(WinnerParticipants.Single(x => x.ID == id));
        }

        public void RandomizeSeed()
        {
            Random rand = new Random();
            foreach (Participant p in WinnerParticipants)
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

        private List<Participant> SortSeeds(List<Participant> participants)
        {
            if(participants.Count > 3) 
            {
                List<Participant> list1 = new List<Participant>();
                List<Participant> list2 = new List<Participant>();
                foreach (Participant p in participants)
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

    public class Participant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Sponsor { get; set; }
        public string Team { get; set; }
        public int Score { get; set; }
        public int Seed { get; set; }

        public Participant() { }

        public Participant(int id, string name = "", string sponsor = "", string team = "", int score = 0, int seed = 0)
        {
            ID = id;
            Name = name;
            Sponsor = sponsor;
            Team = team;
            Score = score;
            seed = 0;
        }

        public void AddVictory(int nbWins = 1)
        {
            Score += nbWins;
        }
    }
}