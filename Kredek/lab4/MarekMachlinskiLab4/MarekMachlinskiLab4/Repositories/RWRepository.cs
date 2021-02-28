using System.Collections.Generic;
using System.Linq;

namespace MarekMachlinskiLab4.Repositories
{
    /// <summary>
    /// Klasa repozytorium dla dowolnych danych
    /// </summary>
    /// <typeparam name="T">Klasa dziedzicząca po klasie Enitity</typeparam>
    public class RWRepository<T> : IRWRepository<T> where T : Models.Entity
    {
        /// <summary>
        /// Obiekt AppContext
        /// </summary>
        private readonly Models.AppContext context;

        /// <summary>
        /// Kontruktor
        /// </summary>
        /// <param name="newContext">Obiekt AppContext</param>
        public RWRepository(Models.AppContext newContext)
        {
            context = newContext;
        }

        /// <summary>
        /// Tworzy obiekt w repozytorium
        /// </summary>
        /// <param name="entity">Obiekt do stworzenia</param>
        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Usuwa element repozytorium
        /// </summary>
        /// <param name="entity">Element do usunięcia</param>
        public  void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Aktualizuje element repozytorium
        /// </summary>
        /// <param name="entity">Element do zaktualizowanias</param>
        public void Update(T entity)
        {
            context.Set<T>().Remove(entity);
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Zwraca wszystkie elementy repozytorium
        /// </summary>
        /// <returns>Lita elementów w repozytorium</returns>
        public List<T> GetAll()
        {
            return context.Set<T>().ToList<T>();
        }

        /// <summary>
        /// Zwraca element repozytorium
        /// </summary>
        /// <param name="id">Id elementu</param>
        /// <returns>Obiekt z repozytorium</returns>
        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
    }
}
