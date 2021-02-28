using System;
using System.Linq;
using System.Web.Mvc;

namespace MarekMachlinskiLab5Zad1.Controllers
{
    /// <summary>
    /// Główny kontroler serwera
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Wyświetla stronę początkową
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Wyświetla listę wymiarów
        /// </summary>
        /// <returns></returns>
        public ActionResult Planes()
        {
            return View();
        }

        /// <summary>
        /// Wyświeta tabelę graczy
        /// </summary>
        /// <returns></returns>
        public ActionResult Players()
        {
            using (Database.MagicPortalContext context = new Database.MagicPortalContext())
            {
                var players = context.Players.ToList();
                return View(players);
            }
        }

        /// <summary>
        /// Wyświetla formularz rejestracji gracza
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Dodaje gracza jeśli nie znajduje się w bazie danych i jego imię zostało podane, inaczej pojawia się komunikat o błędzie
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddPlayer(Database.Player player)
        {
            if (player.Name == null)
                return View("ErrorData");
            using (Database.MagicPortalContext context = new Database.MagicPortalContext())
            {
                if (context.Players.Find(player.Name) != null)
                    return View("ErrorData");
                player.EnlistDate = DateTime.Now;
                context.Set<Database.Player>().Add(player);
                context.SaveChanges();
                var players = context.Players.ToList();
                return View("Players", players);
            }
        }

        /// <summary>
        /// Wyświetla stronę z najdroższą kartą
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpensiveCard()
        {
            return View();
        }

        /// <summary>
        /// Wyświetla stronę z ostatnim zwycięzcą w turnieju
        /// </summary>
        /// <returns></returns>
        public ActionResult LastWinner()
        {
            return View();
        }

        /// <summary>
        /// Wyświetla stronę z opisem gry Magic
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }
    }
}