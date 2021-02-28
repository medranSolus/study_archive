using System.Web.Http;

namespace MarekMachlinskiLab6Zad1.Controllers
{
    /// <summary>
    /// Kontroler umożliwiający usunięcie obiektu
    /// </summary>
    public class StarcraftDeleteController : ApiController
    {
        /// <summary>
        /// Usuwa wskazany obiekt (metoda DELETE nie jest dozwolona ze względów bezpieczeństwa, więc ta metoda ją zastępuje)
        /// </summary>
        /// <param name="id">Identyfikator obiektu</param>
        /// <returns>Prawda jeśli się powiodło</returns>
        // POST api/<controller>/5
        public void Post(int id)
        {
            using (var context = new Database.StarContext())
            {
                var toDelete = context.Units.Find(id);
                if (toDelete != null)
                {
                    context.Set<Database.Unit>().Remove(toDelete);
                    context.SaveChanges();
                }
            }
        }
    }
}