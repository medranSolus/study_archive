using System.Collections.Generic;

namespace MarekMachlinskiLab4.Repositories
{
    /// <summary>
    /// Interfejs pozwalający tworzyć repozytorium z dowolnych obietków
    /// </summary>
    /// <typeparam name="T">Obiekt dziedziczący po klasie Entity</typeparam>
    interface IRWRepository<T> where T : Models.Entity
    {
        /// <summary>
        /// Zwraca wszystkie elementy repozytorium
        /// </summary>
        /// <returns>Lita elementów w repozytorium</returns>
        List<T> GetAll();
        /// <summary>
        /// Zwraca element repozytorium
        /// </summary>
        /// <param name="id">Id elementu</param>
        /// <returns>Obiekt z repozytorium</returns>
        T GetById(int id);
        /// <summary>
        /// Usuwa element repozytorium
        /// </summary>
        /// <param name="entity">Element do usunięcia</param>
        void Delete(T entity);
        /// <summary>
        /// Aktualizuje element repozytorium
        /// </summary>
        /// <param name="entity">Element do zaktualizowanias</param>
        void Update(T entity);
        /// <summary>
        /// Tworzy obiekt w repozytorium
        /// </summary>
        /// <param name="entity">Obiekt do stworzenia</param>
        void Create(T entity);
    }
}
