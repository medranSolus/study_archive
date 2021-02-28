using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MarekMachlinskiLab6Zad1.Controllers
{
    /// <summary>
    /// Umożliwia operacje na bazie danych
    /// </summary>
    public class StarcraftController : ApiController
    {
        /// <summary>
        /// Pobiera wszystkie obiekty klasy Unit z bazy danych
        /// </summary>
        /// <returns>Pobrane obiekty</returns>
        // GET api/<controller>
        public IEnumerable<Models.UnitModel> Get()
        {
            using (var context = new Database.StarContext())
            {
                return context.Units.Select(unit => new Models.UnitModel() { Id = unit.Id, Name = unit.Name, FactionName = unit.FactionName, PhotoLink = unit.PhotoLink }).ToList();
            }
        }

        /// <summary>
        /// Znajduje obiekt w bazie danych
        /// </summary>
        /// <param name="id">Identyfikator obiektu</param>
        /// <returns>Znaleziony obiekt lub wartość null w przypadku niepowodzenia</returns>
        // GET api/<controller>/5
        public Models.UnitModel Get(int id)
        {
            using (var context = new Database.StarContext())
            {
                var unit = context.Units.Find(id);
                if (unit != null)
                    return new Models.UnitModel() { Id = unit.Id, Name = unit.Name, FactionName = unit.FactionName, PhotoLink = unit.PhotoLink };
                return null;
            }
        }

        /// <summary>
        /// Tworzy nowy obiekt w bazie danych
        /// </summary>
        /// <param name="name">Nazwa obiektu</param>
        /// <param name="factionName">Nazwa frakcji, do której należy</param>
        /// <param name="photoLink">Opcjonalny link do grafiki z obiektem</param>
        /// <returns>Prawda jeśli się powiodło</returns>
        // POST api/<controller>
        public void Post([FromBody]Models.UnitModel newUnit)
        {
            if (newUnit.Name != null && newUnit.FactionName != null)
            {
                using (var context = new Database.StarContext())
                {
                    if (context.Factions.Find(newUnit.FactionName) != null)
                    {
                        context.Set<Database.Unit>().Add(new Database.Unit() { Name = newUnit.Name, FactionName = newUnit.FactionName, PhotoLink = newUnit.PhotoLink });
                        context.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Zmienia dane obiektu w bazie danych (metoda PUT jest niedozwolona, więc ta metoda pozwala na obejście tego zakazu stworzonego z przyczyn bezpieczeństwa)
        /// </summary>
        /// <param name="id">Identyfikator obiektu</param>
        /// <param name="name">Nazwa obiektu</param>
        /// <param name="factionName">Nazwa frakcji, do której należy</param>
        /// <param name="photoLink">Link do grafiki z obiektem</param>
        /// <returns>Prawda jeśli się powiodło</returns>
        // POST api/<controller>/5
        public void Post(int id, [FromBody]Models.UnitModel newValues)
        {
            using (var context = new Database.StarContext())
            {
                var item = context.Set<Database.Unit>().Find(id);
                if (item != null && newValues != null)
                {
                    if (newValues.FactionName != null)
                    {
                        if (context.Factions.Find(newValues.FactionName) != null)
                            item.FactionName = newValues.FactionName;
                        else
                            return;
                    }
                    if (newValues.Name != null)
                        item.Name = newValues.Name;
                    if (newValues.PhotoLink != null)
                        item.PhotoLink = newValues.PhotoLink;
                    context.SaveChanges();
                }
            }
        }
    }
}