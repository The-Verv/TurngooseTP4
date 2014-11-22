using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace TP4_Turngoose.Models
{
    [Serializable]
    public class TournamentModel
    {
        const string SAVE_PATH = "/SavedTournaments/";
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

        public TournamentModel(TournamentModel tournament)
        {
            TournamentName = tournament.TournamentName;
            TournamentDate = tournament.TournamentDate;
            DoubleElimination = tournament.DoubleElimination;
            Administrator = tournament.Administrator;
            WinnerParticipants = new List<ParticipantModel>();
            WinnerParticipants.AddRange(tournament.WinnerParticipants);
            LoserParticipants = new List<ParticipantModel>(); 
            LoserParticipants.AddRange(tournament.LoserParticipants);
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
            WinnerParticipants = WinnerParticipants.OrderBy(x => x.ID).ToList();
        }

        public void SortBySeed()
        {
            WinnerParticipants = SortSeeds(WinnerParticipants.OrderByDescending(x => x.Seed).ToList());
        }

        private List<ParticipantModel> SortSeeds(List<ParticipantModel> participants)
        {
            if(participants.Count > 3) 
            {
                List<ParticipantModel> list1 = new List<ParticipantModel>();
                List<ParticipantModel> list2 = new List<ParticipantModel>();
                int count = 2;
                foreach (ParticipantModel p in participants)
                {
                    if (count < 3)
                        list1.Add(p);
                    else
                        list2.Add(p);

                    if (count == 4)
                        count = 1;
                    else
                        count++;
                }
                list1 = SortSeeds(list1);
                list2 = SortSeeds(list2);
                list1.AddRange(list2);
                return list1;
            }
            return participants;
        }

        // filename must be formatted as "xyz.bin"
        // Template from http://msdn.microsoft.com/en-us/library/ms973893.aspx
        public void Save(string fileName)
        {
            if (!Directory.Exists(SAVE_PATH))
                Directory.CreateDirectory(SAVE_PATH);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("/SavedTournaments/" + fileName,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        // filename must be formatted as "xyz.bin"
        // Template from http://msdn.microsoft.com/en-us/library/ms973893.aspx
        static public TournamentModel Load(string fileName)
        {
            if (!Directory.Exists(SAVE_PATH))
                Directory.CreateDirectory(SAVE_PATH);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("/SavedTournaments/" + fileName,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);
            TournamentModel tm = (TournamentModel)formatter.Deserialize(stream);
            stream.Close();
            return tm;
        }
    }

    public class TournamentMemento
    {
        private Stack<TournamentModel> _undoArchive = new Stack<TournamentModel>();
        private Stack<TournamentModel> _redoArchive = new Stack<TournamentModel>();

        public TournamentMemento() { }

        public void AddNewState(TournamentModel state)
        {
            _undoArchive.Push(new TournamentModel(state));
            _redoArchive.Clear();
        }

        public TournamentModel Undo()
        {
            TournamentModel state = _undoArchive.Pop();
            _redoArchive.Push(new TournamentModel(state));
            return state;
        }

        public TournamentModel Redo()
        {
            TournamentModel state = _redoArchive.Pop();
            _undoArchive.Push(new TournamentModel(state));
            return state;
        }
    }
    
}