namespace MarekMachlinskiLab2Zad1
{
    /// <summary>
    /// Klasa bossa pokazującego się na koniec poziomu
    /// </summary>
    class BossDancer : Dancer
    {
        /// <summary>
        /// Funkcja odpowiedzialna za tworzenie przeciwników
        /// </summary>
        Create createEnemy;
        /// <summary>
        /// Obiekt paska postępu życia bossa na ekranie
        /// </summary>
        System.Windows.Forms.ProgressBar lifeLeft;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="newSpeed">Prędkość przeciwnika</param>
        /// <param name="points">Ilość punktów za przeciwnika</param>
        /// <param name="newLife">Życie przeciwnika</param>
        /// <param name="enemyCreation">Funkcja odpowiedzialna za tworzenie przeciwników</param>
        /// <param name="lifeBar">Obiekt paska postępu życia bossa na ekranie</param>
        public BossDancer(int speed, int points, int life, Create enemyCreation, ref System.Windows.Forms.ProgressBar lifeBar) : base(speed, points, life)
        {
            createEnemy = enemyCreation;
            lifeLeft = lifeBar;
        }

        /// <summary>
        /// Przesłonięta metoda umożliwiająca poruszanie się i tworzenie pomniejszych przeciwników
        /// </summary>
        /// <param name="pictureBox">Graficzna reprezentacja przeciwnika</param>
        /// <param name="area">Obszar, po którym można się poruszać</param>
        public override void Dance(ref System.Windows.Forms.PictureBox enemy, System.Windows.Forms.Integration.ElementHost area)
        {
            for (int i = 0; i < speed; ++i)
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
            int canCreate = random.Next(0, 50);
            if (canCreate == 0)
                createEnemy("pictureBoxBossCompanion");
        }

        /// <summary>
        /// Przesłonięta metoda odpowiedzialna za obrażenia otrzymywanie przez bossa i kontrolująca pasek jego życia na ekranie
        /// </summary>
        /// <returns>Zwraca true jeśli boss zginął</returns>
        public override bool Die()
        {
            --life;
            --lifeLeft.Value;
            if (life < 1)
                return true;
            return false;
        }
    }
}
