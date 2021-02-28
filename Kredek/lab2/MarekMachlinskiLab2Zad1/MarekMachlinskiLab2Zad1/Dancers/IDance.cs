namespace MarekMachlinskiLab2Zad1
{
    /// <summary>
    /// Interfejs umożliwiający przeciwnikom poruszanie i otrzymywanie obrażeń
    /// </summary>
    interface IDance
    {
        /// <summary>
        /// Metoda losująca kierunek, w którym przeciwnik będzie się poruszać
        /// </summary>
        /// <param name="pictureBox">Graficzna reprezentacja przeciwnika</param>
        /// <param name="area">Obszar, po którym można się poruszać</param>
        void Dance(ref System.Windows.Forms.PictureBox pictureBox, System.Windows.Forms.Integration.ElementHost area);

        /// <summary>
        /// Metoda umożliwiająca otrzymywanie obrażeń przez przeciwników
        /// </summary>
        /// <returns>Zwraca true jeśli przeciwnik zginął</returns>
        bool Die();
    }
}
