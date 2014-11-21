using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP4_Turngoose.Models;

namespace TP4_Turngoose.Controllers
{
    public class TournamentController : Controller
    {
        TournamentModel tournoi = new TournamentModel();
        // GET: Tournament
        public ActionResult Index()
        {
            ViewData["tournoi"] = tournoi;
            return View();
        }

       [HttpPost]
        public ActionResult Index(String name, String sponsor, String team, int seed)
        {
            if (sponsor == null)
                sponsor = "";
            if (team == null)
                team = "";
            if (seed == null)
                seed = 0;


            tournoi.AddParticipant(name, sponsor, team, 0, seed);
            return null;
        }

        public ActionResult Brackets()
        {
            return View();
        }

    }
}