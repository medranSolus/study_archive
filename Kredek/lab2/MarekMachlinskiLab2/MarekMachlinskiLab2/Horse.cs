using System;

namespace MarekMachlinskiLab2
{
    class Horse : Creature, IMovable
    {
        /// <summary>
        /// Liczba nóg konia
        /// </summary>
        public string FavouriteColor { get; set; }
        /// <summary>
        /// Imię konia
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Konstruktor parametrowy
        /// </summary>
        /// <param name="newName">Imię konia</param>
        /// <param name="favouriteColor">Ulubiony kolor</param>
        public Horse(string newName, string favouriteColor)
        {
            MaxSpeed = 42;
            Name = newName;
            FavouriteColor = favouriteColor;
        }

        /// <summary>
        /// Zwraca tekst, że coś robi
        /// </summary>
        /// <returns></returns>
        public string DoSomething()
        {
            return "Robię coś, czekaj";
        }

        /// <summary>
        /// Przeciążenie funkcji ToString
        /// </summary>
        /// <returns>Sformatowany opis obiektu</returns>
        public override string ToString()
        {
            return $"Nazywam się {Name} {FavouriteColor} i uwielbiam {FavouriteColor}.{Environment.NewLine}";
        }

        /// <summary>
        /// Metoda do chodzenia
        /// </summary>
        /// <returns></returns>
        public string Move()
        {
            return $"Chodzę, a nawet biegnę z prędkością {GetCurrentSpeed()} m/s!{Environment.NewLine}";
        }
    }
}
