using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP4_Turngoose.Controllers
{
    public class TournamentController : Controller
    {
        // GET: Tournament
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Brackets()
        {
            return View();
        }

        public ActionResult AddParticipant()
        {
            return View();
        }
    }
}