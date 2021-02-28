namespace MarekMachlinskiLab2Zad1
{
    /// <summary>
    /// Klasa przechowująca graficzną reprezentację przeciwnika i jego typ
    /// </summary>
    class Enemy
    {
        /// <summary>
        /// Obiekt klasy abstrakcyjnej umożliwiający przechowywanie dowolnego rodzaju przeciwników
        /// </summary>
        Dancer enemyDancer;
        /// <summary>
        /// Obiekt przechowujący graficzną reprezentację przeciwnika
        /// </summary>
        public System.Windows.Forms.PictureBox Graphics { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="newGraphics">Obiekt przechowujący graficzną reprezentację przeciwnika</param>
        /// <param name="newDancer">Dowolny obiekt przeciwnika</param>
        public Enemy(System.Windows.Forms.PictureBox newGraphics, Dancer newDancer)
        {
            Graphics = newGraphics;
            enemyDancer = newDancer;
        }

        /// <summary>
        /// Metoda umożliwiająca poruszanie się przeciwnika
        /// </summary>
        /// <param name="area">Obszar poruszania się przeciwnika</param>
        public void Dance(System.Windows.Forms.Integration.ElementHost area)
        {
            System.Windows.Forms.PictureBox temporaryGraphics = Graphics;
            enemyDancer.Dance(ref temporaryGraphics, area);
            Graphics = temporaryGraphics;
        }

        /// <summary>
        /// Metoda zwiększająca liczę punktów gracza jeśli przeciwnik zginął
        /// </summary>
        /// <param name="points">Referencja do punktów gracza</param>
        /// <returns>Zwraca true jeśli przeciwnik zginął</returns>
        public bool Die(ref int points)
        {
            if (enemyDancer.Die())
            {
                points += enemyDancer.Points;
                return true;
            }
            return false;
        }
    }
}
