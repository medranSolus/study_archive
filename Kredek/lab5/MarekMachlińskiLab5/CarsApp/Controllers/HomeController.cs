using CarsApp.Models;
using System.Web.Mvc;
using System.Linq;

namespace CarsApp.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Method returns Homepage action view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method returns Exercise 1 action view
        /// </summary>
        /// <returns></returns>
        public ActionResult Exercise1()
        {
            return View();
        }

        /// <summary>
        /// Method returns Exercise 2 action view
        /// </summary>
        /// <returns></returns>
        public ActionResult Exercise2()
        {
            return View("Currencies");
        }

        /// <summary>
        /// Method returns Exercise 3 action view
        /// </summary>
        /// <returns></returns>
        public ActionResult Exercise3()
        {
            using (DbContext.DatabaseContext context = new DbContext.DatabaseContext())
            {
                var cars = context.Car.ToList();
                return View(cars);
            }
        }

        /// <summary>
        /// Method returns Exercise 4 action view
        /// </summary>
        /// <returns></returns>
        public ActionResult Exercise4()
        {
            return View();
        }

        /// <summary>
        /// Method returns Exercise 5 action view
        /// </summary>
        /// <returns></returns>
        public ActionResult Exercise5()
        {
            return View();
        }

        /// <summary>
        /// Method returns SubmitContactForm view with greetings for the sender.
        /// </summary>
        /// <param name="contactForm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitContactForm(ContactForm contactForm)
        {
            return View(contactForm);
        }
    }
}