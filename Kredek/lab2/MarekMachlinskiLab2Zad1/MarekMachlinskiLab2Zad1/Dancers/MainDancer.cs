namespace MarekMachlinskiLab2Zad1
{
    /// <summary>
    /// Klasa podstawowego tancerza pojawiającego się na ekranie
    /// </summary>
    class MainDancer : Dancer
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="newSpeed">Prędkość przeciwnika</param>
        /// <param name="points">Ilość punktów za przeciwnika</param>
        /// <param name="newLife">Życie przeciwnika</param>
        public MainDancer(int speed, int points, int life) : base(speed, points, life) { }

        /// <summary>
        /// Przesłonięta metoda umożliwiająca poruszanie się
        /// </summary>
        /// <param name="pictureBox">Graficzna reprezentacja przeciwnika</param>
        /// <param name="area">Obszar, po którym można się poruszać</param>
        public override void Dance(ref System.Windows.Forms.PictureBox enemy, System.Windows.Forms.Integration.ElementHost area)
        {
            System.Drawing.Point newLocation = new System.Drawing.Point
            {
                X = random.Next(-1 * speed, speed) + enemy.Location.X,
                Y = random.Next(-1 * speed, speed) + enemy.Location.Y
            };
            if (newLocation.X < 0 || newLocation.X > area.Size.Width - enemy.Size.Width)
                newLocation.X = enemy.Location.X;
            if (newLocation.Y < 0 || newLocation.Y > area.Size.Height - enemy.Size.Height)
                newLocation.Y = enemy.Location.Y;
            enemy.Location = newLocation;
            enemy.Update();
        }
    }
}
