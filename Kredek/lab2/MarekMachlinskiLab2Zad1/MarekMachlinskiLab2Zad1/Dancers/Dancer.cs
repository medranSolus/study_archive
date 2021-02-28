using System;

namespace MarekMachlinskiLab2Zad1
{
    /// <summary>
    /// Ogólna klasa tancerza, po której dziedziczą wszyscy domyślni przeciwnicy
    /// </summary>
    abstract class Dancer : IDance
    {
        /// <summary>
        /// Obiekt do losowania kierunku
        /// </summary>
        protected Random random = new Random();
        /// <summary>
        /// Prędkość poruszania się przeciwnika
        /// </summary>
        protected int speed;
        /// <summary>
        /// Życie przeciwnika
        /// </summary>
        protected int life;
        /// <summary>
        /// Właściwość określająca ile punktów gracz otrzyma za danego przeciwnika
        /// </summary>
        public int Points { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="newSpeed">Prędkość przeciwnika</param>
        /// <param name="points">Ilość punktów za przeciwnika</param>
        /// <param name="newLife">Życie przeciwnika</param>
        public Dancer(int newSpeed, int points, int newLife)
        {
            speed = newSpeed;
            Points = points;
            life = newLife;
        }

        /// <summary>
        /// Abstrakcyjna metoda umożliwiająca poruszanie się
        /// </summary>
        /// <param name="pictureBox">Graficzna reprezentacja przeciwnika</param>
        /// <param name="area">Obszar, po którym można się poruszać</param>
        public abstract void Dance(ref System.Windows.Forms.PictureBox pictureBox, System.Windows.Forms.Integration.ElementHost area);

        /// <summary>
        /// Metoda umożliwiająca otrzymywanie obrażeń przez przeciwników
        /// </summary>
        /// <returns>Zwraca true jeśli przeciwnik zginął</returns>
        public virtual bool Die()
        {
            --life;
            if (life < 1)
                return true;
            return false;
        }
    }
}
