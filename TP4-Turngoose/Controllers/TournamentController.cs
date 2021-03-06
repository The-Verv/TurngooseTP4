﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP4_Turngoose.Models;

namespace TP4_Turngoose.Controllers
{
    public class TournamentController : Controller
    {
        static TournamentModel tournoi = new TournamentModel();
        // GET: Tournament
        public ActionResult Index()
        {
            ViewData["tournoi"] = tournoi;
            return View();
        }

       [HttpPost]
        public void AddParticipant(String name, String sponsor, String team, String seed)
       {
           if (sponsor.Trim() == "")
               sponsor = "no sponsor";
           if (team.Trim() == "")
               team = "no team";
           if (seed.Trim() == "")
               seed = "0";

            tournoi.AddParticipant(name, sponsor, team, 0, int.Parse(seed));
       }
         
        public void Brackets(String adminName, String tournamentName, String date, String type, String seed)
        {
            tournoi.Administrator = adminName;
            tournoi.TournamentName = tournamentName;
            tournoi.TournamentDate = date;
            if (type == "Single Elimination")
            {
                tournoi.DoubleElimination = false;
            }
            else{
                tournoi.DoubleElimination = true;
            }
            if (seed == "true"){
                tournoi.RandomizeSeed();
            }

            ViewData["tournoi"] = tournoi;
        }

    }
}