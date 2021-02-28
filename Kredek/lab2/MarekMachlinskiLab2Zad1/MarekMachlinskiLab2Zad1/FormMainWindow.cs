using System;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarekMachlinskiLab2Zad1
{
    /// <summary>
    /// Delegat przedstawiający funkcję do tworzenia przeciwników
    /// </summary>
    /// <param name="name">Schemat nazwy przeciwnika</param>
    delegate void Create(string name);

    /// <summary>
    /// Klasa głównego okna gry
    /// </summary>
    public partial class FormMainWindow : Form
    {
        /// <summary>
        /// Obiekt losujący miejsce pojawiania się przeciwników
        /// </summary>
        Random random = new Random();
        /// <summary>
        /// Kolekcja przechowująca klasy przeciwników
        /// </summary>
        Dictionary<string, Enemy> Enemies = new Dictionary<string, Enemy>();
        /// <summary>
        /// Okienko opisujące program
        /// </summary>
        FormAbout about = new FormAbout();
        /// <summary>
        /// Teledysk odtwarzany w tle
        /// </summary>
        MediaElement backgroundPlay = new MediaElement();
        /// <summary>
        /// Flaga informująca czy poziom się skończył
        /// </summary>
        bool isLevelEnd = false;
        /// <summary>
        /// Flaga informująca czy pojawił się boss
        /// </summary>
        bool bossEnter = false;
        /// <summary>
        /// Flaga informująca czy boss został pokonany
        /// </summary>
        bool bossDefeated = false;
        /// <summary>
        /// Flaga inicjalizująca pierszą falę nowych przeciwników
        /// </summary>
        bool firstWave = false;
        /// <summary>
        /// Flaga inicjalizująca drugą falę nowych przeciwników
        /// </summary>
        bool secondWave = false;
        /// <summary>
        /// Flaga informująca czy można dodać przeciwników do koleckji w danym momencie
        /// </summary>
        bool enemiesMove = false;
        /// <summary>
        /// Czas do nowego przeciwnika danego typu
        /// </summary>
        int timeToNewEnemy = 1500;
        /// <summary>
        /// Całkowity wynik gracza
        /// </summary>
        int finalScore = 0;
        /// <summary>
        /// Wynik gracza na danym poziomie
        /// </summary>
        int score = 0;
        /// <summary>
        /// Początek numerowania liczby przeciwników
        /// </summary>
        int elementsCount = 5;
        /// <summary>
        /// Aktualny poziom
        /// </summary>
        int level = 1;

        /// <summary>
        /// Konstruktor domyślny okna
        /// </summary>
        public FormMainWindow()
        {
            InitializeComponent();
            backgroundPlay.LoadedBehavior = MediaState.Manual;
            backgroundPlay.Source = new Uri(System.IO.Directory.GetCurrentDirectory() + @"\Resources\Level1.mp4");
            elementHostMedia.Child = backgroundPlay;
            backgroundPlay.MediaEnded += LevelEnd;
        }

        /// <summary>
        /// Kliknięcie w przycisk rozpoczynający grę
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelPlay_Click(object sender, EventArgs e)
        {
            backgroundPlay.Play();
            pictureBoxAbout.Enabled = pictureBoxAbout.Visible = labelPlay.Enabled = labelPlay.Visible = pictureBoxMenuBackground.Enabled = pictureBoxMenuBackground.Visible = false;
            labelScore.Enabled = labelScore.Visible = true;
            Width = 976;
            Height = 759;
            AddEasyEnemies();
            MoveEnemies();
        }

        /// <summary>
        /// Kliknięcie w przycisk wyświetlający informację o grze
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxAbout_Click(object sender, EventArgs e)
        {
            if (about.IsDisposed)
                about = new FormAbout();
            about.Show();
        }

        /// <summary>
        /// Funkcja tworzącego nowego łatwego przeciwnika
        /// </summary>
        /// <param name="name">Schemat nazwy przeciwnika</param>
        public async void CreateEasyEnemy(string name = "pictureBoxEasyEnemy")
        {
            PictureBox graphics = new PictureBox
            {
                BackColor = System.Drawing.Color.Transparent,
                Cursor = Cursors.Hand,
                Image = Properties.Resources.DancerEasy,
                Location = new System.Drawing.Point(random.Next(0, elementHostMedia.Size.Width - 44), random.Next(0, elementHostMedia.Size.Height - 80)),
                Size = new System.Drawing.Size(44, 80),
                SizeMode = PictureBoxSizeMode.StretchImage,
                TabStop = false,
                TabIndex = elementsCount,
                Name = name + TabIndex.ToString() + random.Next(elementsCount, Int32.MaxValue).ToString()
            };
            graphics.Click += new EventHandler(EnemyClicked);
            Controls.Add(graphics);
            graphics.BringToFront();
            graphics.Update();
            await Task.Run(() => { while (enemiesMove) ; });
            Enemies.Add(graphics.Name, new Enemy(graphics, new MainDancer(15, 1, 1)));
        }

        /// <summary>
        /// Funkcja tworzącego nowego średniego przeciwnika
        /// </summary>
        /// <param name="name">Schemat nazwy przeciwnika</param>
        public async void CreateMediumEnemy(string name = "pictureBoxMediumEnemy")
        {
            PictureBox graphics = new PictureBox
            {
                BackColor = System.Drawing.Color.Transparent,
                Cursor = Cursors.Hand,
                Image = Properties.Resources.DancerMedium,
                Location = new System.Drawing.Point(random.Next(0, elementHostMedia.Size.Width - 72), random.Next(0, elementHostMedia.Size.Height - 95)),
                Size = new System.Drawing.Size(72, 95),
                SizeMode = PictureBoxSizeMode.StretchImage,
                TabStop = false,
                TabIndex = 2 * elementsCount,
                Name = name + TabIndex.ToString() + random.Next(elementsCount, Int32.MaxValue).ToString()
            };
            graphics.Click += new EventHandler(EnemyClicked);
            Controls.Add(graphics);
            graphics.BringToFront();
            graphics.Update();
            await Task.Run(() => { while (enemiesMove) ; });
            Enemies.Add(graphics.Name, new Enemy(graphics, new MainDancer(12, 3, 2)));
        }

        /// <summary>
        /// Funkcja tworzącego nowego trudnego przeciwnika
        /// </summary>
        /// <param name="name">Schemat nazwy przeciwnika</param>
        public async void CreateHardEnemy(string name = "pictureBoxHardEnemy")
        {
            PictureBox graphics = new PictureBox
            {
                BackColor = System.Drawing.Color.Transparent,
                Cursor = Cursors.Hand,
                Image = Properties.Resources.DancerHard,
                Location = new System.Drawing.Point(random.Next(0, elementHostMedia.Size.Width - 79), random.Next(0, elementHostMedia.Size.Height - 110)),
                Size = new System.Drawing.Size(79, 110),
                SizeMode = PictureBoxSizeMode.StretchImage,
                TabStop = false,
                TabIndex = 3 * elementsCount,
                Name = name + TabIndex.ToString() + random.Next(elementsCount, Int32.MaxValue).ToString()
            };
            graphics.Click += new EventHandler(EnemyClicked);
            Controls.Add(graphics);
            graphics.BringToFront();
            graphics.Update();
            await Task.Run(() => { while (enemiesMove) ; });
            Enemies.Add(graphics.Name, new Enemy(graphics, new MainDancer(10, 5, 3)));
        }

        /// <summary>
        /// Funckja tworząca w pętli nowych łatwych przeciwników
        /// </summary>
        private async void AddEasyEnemies()
        {
            for(; !bossEnter && !isLevelEnd; ++elementsCount)
            {
                await Task.Delay(timeToNewEnemy);
                CreateEasyEnemy();
            }
        }

        /// <summary>
        /// Funckja tworząca w pętli nowych średnich przeciwników
        /// </summary>
        private async void AddMediumEnemies()
        {
            for (; !bossEnter && !isLevelEnd; ++elementsCount)
            {
                await Task.Delay(3 * timeToNewEnemy);
                CreateMediumEnemy();
            }
        }

        /// <summary>
        /// Funckja tworząca w pętli nowych trudnych przeciwników
        /// </summary>
        private async void AddHardEnemies()
        {
            for (; !bossEnter && !isLevelEnd; ++elementsCount)
            {
                await Task.Delay(5 * timeToNewEnemy);
                CreateHardEnemy();
            }
        }

        /// <summary>
        /// Funckja tworząca w pętli nowych bonusowych przeciwników w zależności od wylosowanej liczby przeciwników
        /// </summary>
        private async void AddBonusEnemies()
        {
            for (int i = 1; !isLevelEnd;)
            {
                await Task.Delay(10 * timeToNewEnemy);
                i = random.Next(0, 3);
                if (i == 0)
                {
                    PictureBox graphics = new PictureBox
                    {
                        BackColor = System.Drawing.Color.Transparent,
                        Cursor = Cursors.Hand,
                        Image = Properties.Resources.DancerBonus,
                        Location = new System.Drawing.Point(random.Next(0, elementHostMedia.Size.Width - 59), random.Next(0, elementHostMedia.Size.Height - 79)),
                        Size = new System.Drawing.Size(59, 79),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        TabStop = false,
                        TabIndex = 4 * elementsCount,
                        Name = "pictureBoxBonusEnemy" + TabIndex.ToString() + random.Next(elementsCount, Int32.MaxValue).ToString()
                    };
                    graphics.Click += new EventHandler(EnemyClicked);
                    Controls.Add(graphics);
                    graphics.BringToFront();
                    graphics.Update();
                    Enemies.Add(graphics.Name, new Enemy(graphics, new MainDancer(30, 15, 1)));
                    ++elementsCount;
                }
            }
        }

        /// <summary>
        /// Funkcja obsługująca atak na przeciwnika, rozpoczynająca nowe fale przeciwników, oraz tworząca pod koniec poziomu bossa
        /// </summary>
        /// <param name="sender">Graficzna reprezentacja przeciwnika</param>
        /// <param name="e"></param>
        private void EnemyClicked(object sender, EventArgs e)
        {
            if (sender is PictureBox graphics)
            {
                if (Enemies.TryGetValue(graphics.Name, out Enemy currentEnemy))
                {
                    if (currentEnemy.Die(ref score))
                    {
                        Controls.Remove(currentEnemy.Graphics);
                        Enemies.Remove(graphics.Name);
                        graphics.Dispose();
                        labelScore.Text = "Punkty: " + score.ToString();
                        if (progressBarBossLife.Value == 0)
                        {
                            isLevelEnd = bossDefeated = true;
                            LevelEnd(sender, e);
                        }
                        if (!firstWave && score >= 30)
                        {
                            firstWave = true;
                            AddMediumEnemies();
                            AddBonusEnemies();
                        }
                        if (!secondWave && score >= 111)
                        {
                            secondWave = true;
                            AddHardEnemies();
                        }
                        switch (level)
                        {
                            case 1:
                                {
                                    if (!bossEnter && score >= 300)
                                    {
                                        bossEnter = true;
                                        progressBarBossLife.Value = progressBarBossLife.Maximum = 15;
                                        progressBarBossLife.Enabled = progressBarBossLife.Visible = true;
                                        PictureBox graphicsBoss = new PictureBox
                                        {
                                            BackColor = System.Drawing.Color.Transparent,
                                            Cursor = Cursors.Hand,
                                            Image = Properties.Resources.DancerBoss1,
                                            Location = new System.Drawing.Point(random.Next(0, elementHostMedia.Size.Width - 241), random.Next(0, elementHostMedia.Size.Height - 207)),
                                            Size = new System.Drawing.Size(241, 207),
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            TabStop = false,
                                            TabIndex = 5 * elementsCount,
                                            Name = "pictureBoxBossEnemy" + TabIndex.ToString()
                                        };
                                        graphicsBoss.Click += new EventHandler(EnemyClicked);
                                        Controls.Add(graphicsBoss);
                                        graphicsBoss.BringToFront();
                                        graphicsBoss.Update();
                                        Enemies.Add(graphicsBoss.Name, new Enemy(graphicsBoss, new BossDancer(8, 40, 15, CreateEasyEnemy, ref progressBarBossLife)));
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (!bossEnter && score >= 400)
                                    {
                                        bossEnter = true;
                                        progressBarBossLife.Value = progressBarBossLife.Maximum = 25;
                                        progressBarBossLife.Enabled = progressBarBossLife.Visible = true;
                                        PictureBox graphicsBoss = new PictureBox
                                        {
                                            BackColor = System.Drawing.Color.Transparent,
                                            Cursor = Cursors.Hand,
                                            Image = Properties.Resources.DancerBoss2,
                                            Location = new System.Drawing.Point(random.Next(0, elementHostMedia.Size.Width - 160), random.Next(0, elementHostMedia.Size.Height - 214)),
                                            Size = new System.Drawing.Size(160, 214),
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            TabStop = false,
                                            TabIndex = 5 * elementsCount,
                                            Name = "pictureBoxBossEnemy" + TabIndex.ToString()
                                        };
                                        graphicsBoss.Click += new EventHandler(EnemyClicked);
                                        Controls.Add(graphicsBoss);
                                        graphicsBoss.BringToFront();
                                        graphicsBoss.Update();
                                        Enemies.Add(graphicsBoss.Name, new Enemy(graphicsBoss, new BossDancer(7, 100, 25, CreateMediumEnemy, ref progressBarBossLife)));
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (!bossEnter && score >= 500)
                                    {
                                        bossEnter = true;
                                        progressBarBossLife.Value = progressBarBossLife.Maximum = 35;
                                        progressBarBossLife.Enabled = progressBarBossLife.Visible = true;
                                        PictureBox graphicsBoss = new PictureBox
                                        {
                                            BackColor = System.Drawing.Color.Transparent,
                                            Cursor = Cursors.Hand,
                                            Image = Properties.Resources.DancerBoss3,
                                            Location = new System.Drawing.Point(random.Next(0, elementHostMedia.Size.Width - 225), random.Next(0, elementHostMedia.Size.Height - 224)),
                                            Size = new System.Drawing.Size(224, 224),
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            TabStop = false,
                                            TabIndex = 5 * elementsCount,
                                            Name = "pictureBoxBossEnemy" + TabIndex.ToString()
                                        };
                                        graphicsBoss.Click += new EventHandler(EnemyClicked);
                                        Controls.Add(graphicsBoss);
                                        graphicsBoss.BringToFront();
                                        graphicsBoss.Update();
                                        Enemies.Add(graphicsBoss.Name, new Enemy(graphicsBoss, new BossDancer(9, 200, 35, CreateHardEnemy, ref progressBarBossLife)));
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    if (!bossEnter && score >= 600)
                                    {
                                        bossEnter = true;
                                        progressBarBossLife.Value = progressBarBossLife.Maximum = 55;
                                        progressBarBossLife.Enabled = progressBarBossLife.Visible = true;
                                        PictureBox graphicsBoss = new PictureBox
                                        {
                                            BackColor = System.Drawing.Color.Transparent,
                                            Cursor = Cursors.Hand,
                                            Image = Properties.Resources.DancerBossFinal,
                                            Location = new System.Drawing.Point(random.Next(0, elementHostMedia.Size.Width - 160), random.Next(0, elementHostMedia.Size.Height - 249)),
                                            Size = new System.Drawing.Size(160, 249),
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            TabStop = false,
                                            TabIndex = 5 * elementsCount,
                                            Name = "pictureBoxBossEnemy" + TabIndex.ToString()
                                        };
                                        graphicsBoss.Click += new EventHandler(EnemyClicked);
                                        Controls.Add(graphicsBoss);
                                        graphicsBoss.BringToFront();
                                        graphicsBoss.Update();
                                        Enemies.Add(graphicsBoss.Name, new Enemy(graphicsBoss, new BossDancer(8, 500, 55, CreateMediumEnemy, ref progressBarBossLife)));
                                    }
                                    break;
                                }
                        }  
                    }
                }
            }
        }

        /// <summary>
        /// Funckja poruszająca przeciwnikami
        /// </summary>
        private async void MoveEnemies()
        {
            for (; !isLevelEnd;)
            {
                await Task.Delay(100);
                enemiesMove = true;
                foreach (var enemy in Enemies)
                {
                    enemy.Value.Dance(elementHostMedia);
                }
                enemiesMove = false;
            }
        }

        /// <summary>
        /// Funkcja kończąca poziom i w zależności od pokonania bossa rozpoczyna kolejny lub kończy grę
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LevelEnd(object sender, EventArgs e)
        {
            progressBarBossLife.Value = 1;
            foreach (var enemy in Enemies)
            {
                Controls.Remove(enemy.Value.Graphics);
                enemy.Value.Graphics.Dispose();
            }
            Enemies.Clear();
            labelScore.Visible = labelScore.Enabled = progressBarBossLife.Visible = progressBarBossLife.Enabled = false;
            if (bossDefeated)
            {
                finalScore = score;
                score = 0;
                ++level;
                if(level == 5)
                {
                    labelLevelEnd.Text = $"Wszyscy przeciwnicy pokonani!{Environment.NewLine}Punkty: " + finalScore.ToString();
                    labelLevelEnd.Enabled = labelLevelEnd.Visible = true;
                    await Task.Delay(9000);
                    Close();
                }
                labelLevelEnd.Enabled = labelLevelEnd.Visible = true;
                labelScore.Text = "Punkty: 0";
                await Task.Delay(5000);
                backgroundPlay.Stop();
                if (level == 2)
                    backgroundPlay.Source = new Uri(System.IO.Directory.GetCurrentDirectory() + @"\Resources\Level2.mp4");
                if (level == 3)
                    backgroundPlay.Source = new Uri(System.IO.Directory.GetCurrentDirectory() + @"\Resources\Level3.mp4");
                if (level == 4)
                    backgroundPlay.Source = new Uri(System.IO.Directory.GetCurrentDirectory() + @"\Resources\LevelFinal.mp4");
                backgroundPlay.Play();
                firstWave = secondWave = isLevelEnd = bossDefeated = bossEnter = labelLevelEnd.Enabled = labelLevelEnd.Visible = false;
                labelScore.Enabled = labelScore.Visible = true;
                AddEasyEnemies();
                if(level == 4)
                {
                    timeToNewEnemy = 3500;
                    AddBonusEnemies();
                    AddMediumEnemies();
                    AddHardEnemies();
                    secondWave = true;
                }
                MoveEnemies();
            }
            else
            {
                labelLevelEnd.Text = "Przegrana!";
                labelLevelEnd.Enabled = labelLevelEnd.Visible = true;
                await Task.Delay(5000);
                Close();
            }
        }
    }
}
