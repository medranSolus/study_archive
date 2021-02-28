using System.Web.Mvc;

namespace MarekMachlinskiLab6Zad1.Controllers
{
    /// <summary>
    /// Główny kontroler serwisu
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Wyświetla stronę serwisu
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Starcraft Units";

            return View();
        }
    }
}
