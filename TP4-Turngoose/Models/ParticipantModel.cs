using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP4_Turngoose.Models
{
    public class ParticipantModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Sponsor { get; set; }
        public string Team { get; set; }
        public int Score { get; set; }
        public int Seed { get; set; }

        public ParticipantModel() { }

        public ParticipantModel(int id, string name = "", string sponsor = "", string team = "", int score = 0, int seed = 0)
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