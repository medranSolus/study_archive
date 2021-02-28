namespace MarekMachlinskiLab4Zad1.Repositories
{
    /// <summary>
    /// Klasa repozytorium o type identyfikatorów string
    /// </summary>
    /// <typeparam name="T">Klasa składowana w repozytorium</typeparam>
    public class RWRepositoryString<T> : RWRepository<T> where T : class
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="newContext">Obiekt AppContext</param>
        public RWRepositoryString(Tables.AppContext newContext) : base(newContext) { }

        /// <summary>
        /// Zwraca rekord po jego Id
        /// </summary>
        /// <param name="id">Id rekordu</param>
        /// <returns>Dany rekord</returns>
        public T GetById(string id)
        {
            return context.Set<T>().Find(id);
        }

        /// <summary>
        /// Aktualizuje dany rekord
        /// </summary>
        /// <param name="type">Obiekt rekordu</param>
        /// <param name="id">Identyfikator obiektu</param>
        public void Update(T type, string id)
        {
            var item = context.Set<T>().Find(id);
            if (item != null)
            {
                item = type;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Sprawdza czy repozytorium zawiera dany element
        /// </summary>
        /// <param name="id">Identyfikator obiektu</param>
        /// <returns>True jeśli zawiera element</returns>
        public bool Contains(string id)
        {
            if (context.Set<T>().Find(id) != null)
                return true;
            return false;
        }
    }
}
