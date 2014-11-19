using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP4_Turngoose.Models
{
    public class Tournament
    {
        private string TournamentName { get; set; }
        private DateTime TournamentDate { get; set; }
        private Boolean DoubleElimination { get; set; }
        private string Administrator { get; set; }
        private List<Participant> WinnerParticipants { get; set; }
        private List<Participant> LoserParticipants { get; set; }

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
    }

    public class Participant
    {
        private int ID { get; set; }
        private string Name { get; set; }
        private string Sponsor { get; set; }
        private string Team { get; set; }
        private int Score { get; set; }
        private int Seed { get; set; }

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