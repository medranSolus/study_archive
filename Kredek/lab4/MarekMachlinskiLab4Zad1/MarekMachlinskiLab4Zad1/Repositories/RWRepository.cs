using System.Collections.Generic;
using System.Linq;

namespace MarekMachlinskiLab4Zad1.Repositories
{
    /// <summary>
    /// Klasa abstrakcyjna repozytorium składującego dany typ obiektów
    /// </summary>
    /// <typeparam name="T">Klasa składowana w tablicy</typeparam>
    public abstract class RWRepository<T> where T : class
    {
        /// <summary>
        /// Obiekt AppContext
        /// </summary>
        protected readonly Tables.AppContext context;

        /// <summary>
        /// Kontruktor
        /// </summary>
        /// <param name="newContext">Obiekt AppContext</param>
        public RWRepository(Tables.AppContext newContext)
        {
            context = newContext;
        }

        /// <summary>
        /// Tworzy nowy rekord
        /// </summary>
        /// <param name="type">Nowy obiekt rekordu</param>
        public void Create(T type)
        {
            context.Set<T>().Add(type);
            context.SaveChanges();
        }

        /// <summary>
        /// Usuwa dany rekord
        /// </summary>
        /// <param name="type">Rekord do usunięcia</param>
        public void Delete(T type)
        {
            context.Set<T>().Remove(type);
            context.SaveChanges();
        }

        /// <summary>
        /// Zwraca wszystkie rekordy
        /// </summary>
        /// <returns>Wszystkie rekordy</returns>
        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
    }
}
