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
        public void AddParticipant(String name, String sponsor, String team, String seed)
       {
            tournoi.AddParticipant(name, sponsor, team, 0, Convert.ToInt32(seed));
       }
         
        public ActionResult Brackets()
        {
            return View();
        }

    }
}