using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarekMachlinskiZadanie
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Ilość drewna
        /// </summary>
        long wood = 0;
        /// <summary>
        /// Wartość czasowego zwiększania się drewna
        /// </summary>
        long woodIncrement = 5;
        /// <summary>
        /// Ilość kamienia
        /// </summary>
        long stone = 0;
        /// <summary>
        /// Wartość czasowego zwiększania się kamienia
        /// </summary>
        long stoneIncrement = 5;
        /// <summary>
        /// Ilość kamienia
        /// </summary>
        long food = 0;
        /// <summary>
        /// Wartość czasowego zwiększania się jedzenia
        /// </summary>
        long foodIncrement = 5;
        /// <summary>
        /// Ilość złota
        /// </summary>
        long gold = 0;
        /// <summary>
        /// Wartość czasowego zwiększania się złota
        /// </summary>
        long goldIncrement = 5;
        /// <summary>
        /// Ilość orków
        /// </summary>
        long orks = 0;
        /// <summary>
        /// Wartość czasowego zwiększania się orków
        /// </summary>
        long orksIncrement = 5;
        /// <summary>
        /// Ilość trolli
        /// </summary>
        long trolls = 0;
        /// <summary>
        /// Wartość czasowego zwiększania się trolli
        /// </summary>
        long trollsIncrement = 5;
        /// <summary>
        /// Ilość nazguli
        /// </summary>
        int nazguls = 0;
        /// <summary>
        /// Poziom tartaku
        /// </summary>
        int sawmillLevel = 0;
        /// <summary>
        /// Poziom kopalni
        /// </summary>
        int mineLevel = 0;
        /// <summary>
        /// Poziom rzeźni
        /// </summary>
        int butcheryLevel = 0;
        /// <summary>
        /// Poziom leża orków
        /// </summary>
        int orksBreedLevel = 0;
        /// <summary>
        /// Poziom legowiska trolli
        /// </summary>
        int trollSpawnLevel = 0;
        /// <summary>
        /// Granie muzyki w tle
        /// </summary>
        SoundPlayer themeSound;
        /// <summary>
        /// Granie muzyki na zakończenie gry
        /// </summary>
        SoundPlayer victorySound;
        /// <summary>
        /// Czas rozpoczęcia gry
        /// </summary>
        DateTime startTime;
        /// <summary>
        /// Długość gry
        /// </summary>
        TimeSpan timeDuration;

        public Form1()
        {
            InitializeComponent();
            startTime = DateTime.Now;
            themeSound = new SoundPlayer(Properties.Resources.Sauron_Theme);
            themeSound.PlayLooping();
            victorySound = new SoundPlayer(Properties.Resources.Victory);
        }

        /// <summary>
        /// Bezpośrednie zwiększenie ilości drewna przez gracza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelWood_Click(object sender, EventArgs e)
        {
            ++wood;
            labelWood.Text = "Drewno: " + wood.ToString();
        }

        /// <summary>
        /// Bezpośrednie zwiększenie ilości kamienia przez gracza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelStone_Click(object sender, EventArgs e)
        {
            ++stone;
            labelStone.Text = "Kamień: " + stone.ToString();
        }

        /// <summary>
        /// Czasowe zwiększenia się ilości drewna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerWood_Tick(object sender, EventArgs e)
        {
            ++timerWood.Interval;
            if(timerWood.Interval==80)
            {
                timerWood.Interval = 1;
                wood += woodIncrement;
                if (wood < 0)
                    wood = Int64.MaxValue;
                labelWood.Text = "Drewno: " + wood.ToString();
            }
        }

        /// <summary>
        /// Czasowe zwiększenia się ilości kamienia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerStone_Tick(object sender, EventArgs e)
        {
            ++timerStone.Interval;
            if (timerStone.Interval == 120)
            {
                timerStone.Interval = 1;
                stone += stoneIncrement;
                if (stone < 0)
                    stone = Int64.MaxValue;
                labelStone.Text = "Kamień: " + stone.ToString();
            }
        }

        /// <summary>
        /// Czasowe zwiększenia się ilości jedzenia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFood_Tick(object sender, EventArgs e)
        {
            ++timerFood.Interval;
            if (timerFood.Interval == 100)
            {
                timerFood.Interval = 1;
                food += foodIncrement;
                if (food < 0)
                    food = Int64.MaxValue;
                labelFood.Text = "Jedzenie: " + food.ToString();
            }
        }

        /// <summary>
        /// Czasowe zwiększenia się ilości złota
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerGold_Tick(object sender, EventArgs e)
        {
            ++timerGold.Interval;
            if (timerGold.Interval == 180)
            {
                timerGold.Interval = 1;
                gold += goldIncrement;
                if (gold < 0)
                    gold = Int64.MaxValue;
                labelGold.Text = "Złoto: " + gold.ToString();
            }
        }

        /// <summary>
        /// Czasowe zwiększenia się ilości orków
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerOrks_Tick(object sender, EventArgs e)
        {
            ++timerOrks.Interval;
            if (timerOrks.Interval == 130)
            {
                timerOrks.Interval = 1;
                if (food >= orksBreedLevel * 50)
                {
                    food -= orksBreedLevel * 50;
                    orks += orksIncrement;
                    if (orks < 0)
                        orks = Int64.MaxValue;
                }
                else
                {
                    food = 0;
                    if (orks >= orksIncrement)
                        orks -= orksIncrement;
                    else
                        orks = 0;
                }
                labelOrks.Text = "Orkowie: " + orks.ToString();
                labelFood.Text = "Jedzenie: " + food.ToString();
            }
        }

        /// <summary>
        /// Czasowe zwiększenia się ilości trolli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTrolls_Tick(object sender, EventArgs e)
        {
            ++timerTrolls.Interval;
            if (timerTrolls.Interval == 150)
            {
                timerTrolls.Interval = 1;
                if(food >= trollSpawnLevel * 100)
                {
                    food -= trollSpawnLevel * 100;
                    trolls += trollsIncrement;
                    if (trolls < 0)
                        trolls = Int64.MaxValue;
                }
                else
                {
                    food = 0;
                    if (trolls >= trollsIncrement)
                        trolls -= trollsIncrement;
                    else
                        trolls = 0;
                }
                labelTrolls.Text = "Trolle: " + trolls.ToString();
                labelFood.Text = "Jedzenie: " + food.ToString();
            }
        }

        /// <summary>
        /// Zwiększa stałą produkcję drewna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSawmillUpgrade_Click(object sender, EventArgs e)
        {
            if (sawmillLevel < 25)
            {
                long woodCost = Int64.Parse(labelTableSawmillWood.Text);
                if (wood >= woodCost)
                {
                    wood -= woodCost;
                    labelWood.Text = "Drewno: " + wood.ToString();
                    ++sawmillLevel;
                    if (sawmillLevel == 25)
                    {
                        buttonSawmillUpgrade.Visible = buttonSawmillUpgrade.Enabled = false;
                        woodCost = 0;
                    }
                    else
                        woodCost *= 4;
                    labelTableSawmillWood.Text = woodCost.ToString();                    
                    woodIncrement *= (sawmillLevel + 1);
                    labelSawmill.Text = "Tartak, poziom " + sawmillLevel.ToString();
                    if (sawmillLevel == 1)
                        timerWood.Start();
                }
            }
        }

        /// <summary>
        /// Zwiększa stałą produkcję kamienia i złota
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMineUpgrade_Click(object sender, EventArgs e)
        {
            if (mineLevel < 25)
            {
                long woodCost = Int64.Parse(labelTableMineWood.Text), stoneCost = Int64.Parse(labelTableMineStone.Text);
                if (wood >= woodCost && stone >= stoneCost)
                {
                    wood -= woodCost;
                    labelWood.Text = "Drewno: " + wood.ToString();
                    stone -= stoneCost;
                    labelStone.Text = "Kamień: " + stone.ToString();
                    ++mineLevel;
                    if (mineLevel == 25)
                    {
                        buttonSawmillUpgrade.Visible = buttonSawmillUpgrade.Enabled = false;
                        woodCost = stoneCost = 0;
                    }
                    else
                    {
                        woodCost *= 4;
                        stoneCost *= 4;
                    }
                    labelTableMineWood.Text = woodCost.ToString();
                    labelTableMineStone.Text = stoneCost.ToString();                    
                    stoneIncrement *= (mineLevel + 1);
                    goldIncrement *= mineLevel;
                    labelMine.Text = "Kopalnia, poziom " + mineLevel.ToString();
                    if (mineLevel == 1)
                    {
                        timerStone.Start();
                        timerGold.Start();
                    }
                }
            }
        }

        /// <summary>
        /// Zwiększa stałą produkcję jedzenia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonButcheryUpgrade_Click(object sender, EventArgs e)
        {
            if (butcheryLevel < 25)
            {
                long woodCost = Int64.Parse(labelTableButcheryWood.Text), stoneCost = Int64.Parse(labelTableButcheryStone.Text);
                if (wood >= woodCost && stone >= stoneCost)
                {
                    wood -= woodCost;
                    labelWood.Text = "Drewno: " + wood.ToString();
                    stone -= stoneCost;
                    labelStone.Text = "Kamień: " + stone.ToString();
                    ++butcheryLevel;
                    if (butcheryLevel == 25)
                    {
                        buttonMineUpgrade.Visible = buttonMineUpgrade.Enabled = false;
                        woodCost = stoneCost = 0;
                    }
                    else
                    {
                        woodCost *= 5;
                        stoneCost *= 4;
                    }
                    labelTableButcheryWood.Text = woodCost.ToString();
                    labelTableButcheryStone.Text = stoneCost.ToString();
                    foodIncrement *= (butcheryLevel + 1);
                    labelButchery.Text = "Rzeźnia, poziom " + butcheryLevel.ToString();
                    if (butcheryLevel == 1)
                        timerFood.Start();
                }
            }
        }

        /// <summary>
        /// Zwiększa stałą produkcję orków
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOrksBreedUpgrade_Click(object sender, EventArgs e)
        {
            if (orksBreedLevel < 25)
            {
                long woodCost = Int64.Parse(labelTableOrksBreedWood.Text), stoneCost = Int64.Parse(labelTableOrksBreedStone.Text), foodCost = Int64.Parse(labelTableOrksBreedFood.Text), goldCost = Int64.Parse(labelTableOrksBreedGold.Text);
                if (wood >= woodCost && stone >= stoneCost && food >= foodCost && gold >= goldCost)
                {
                    wood -= woodCost;
                    labelWood.Text = "Drewno: " + wood.ToString();
                    stone -= stoneCost;
                    labelStone.Text = "Kamień: " + stone.ToString();
                    food -= foodCost;
                    labelFood.Text = "Jedzenie: " + food.ToString();
                    gold -= goldCost;
                    labelGold.Text = "Złoto: " + gold.ToString();
                    ++orksBreedLevel;
                    if (orksBreedLevel == 25)
                    {
                        buttonOrksBreedUpgrade.Visible = buttonOrksBreedUpgrade.Enabled = false;
                        woodCost = stoneCost = foodCost = goldCost = 0;
                    }
                    else
                    {
                        woodCost *= 5;
                        stoneCost *= 3;
                        foodCost *= 6;
                        goldCost *= 4;
                    }
                    labelTableOrksBreedWood.Text = woodCost.ToString();                    
                    labelTableOrksBreedStone.Text = stoneCost.ToString();                    
                    labelTableOrksBreedFood.Text = foodCost.ToString();                    
                    labelTableOrksBreedGold.Text = goldCost.ToString();
                    orksIncrement *= orksBreedLevel;
                    labelOrksBreed.Text = "Leże orków, poziom " + orksBreedLevel.ToString();
                    if (orksBreedLevel == 1)
                        timerOrks.Start();
                }
            }
        }

        /// <summary>
        /// Zwiększa stałą produkcję trolli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTrollSpawnUpgrade_Click(object sender, EventArgs e)
        {
            if (trollSpawnLevel < 25)
            {
                long woodCost = Int64.Parse(labelTableTrollSpawnWood.Text), stoneCost = Int64.Parse(labelTableTrollSpawnStone.Text), foodCost = Int64.Parse(labelTableTrollSpawnFood.Text), goldCost = Int64.Parse(labelTableTrollSpawnGold.Text), orksCost = Int64.Parse(labelTableTrollSpawnOrks.Text);
                if (wood >= woodCost && stone >= stoneCost && food >= foodCost && gold >= goldCost && orks >= orksCost)
                {
                    wood -= woodCost;
                    labelWood.Text = "Drewno: " + wood.ToString();
                    stone -= stoneCost;
                    labelStone.Text = "Kamień: " + stone.ToString();
                    food -= foodCost;
                    labelFood.Text = "Jedzenie: " + food.ToString();
                    gold -= goldCost;
                    labelGold.Text = "Złoto: " + gold.ToString();
                    orks -= orksCost;
                    labelOrks.Text = "Orkowie: " + orks.ToString();
                    ++trollSpawnLevel;
                    if (trollSpawnLevel == 25)
                    {
                        buttonTrollSpawnUpgrade.Visible = buttonTrollSpawnUpgrade.Enabled = false;
                        woodCost = stoneCost = foodCost = goldCost = orksCost = 0;
                    }
                    else
                    {
                        woodCost *= 4;
                        stoneCost *= 5;
                        foodCost *= 7;
                        goldCost *= 5;
                        orksCost *= 4;
                    }
                    labelTableTrollSpawnWood.Text = woodCost.ToString();
                    labelTableTrollSpawnStone.Text = stoneCost.ToString();
                    labelTableTrollSpawnFood.Text = foodCost.ToString();
                    labelTableTrollSpawnGold.Text = goldCost.ToString();
                    labelTableTrollSpawnOrks.Text = orksCost.ToString();
                    trollsIncrement *= trollSpawnLevel;
                    labelTrollSpawn.Text = "Legowisko trolli, poziom " + trollSpawnLevel.ToString();
                    if (trollSpawnLevel == 1)
                        timerTrolls.Start();
                }
            }
        }

        /// <summary>
        /// Zwiększa ilość nazguli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAbyssUpgrade_Click(object sender, EventArgs e)
        {
            if (nazguls < 9)
            {
                long woodCost = Int64.Parse(labelTableAbyssWood.Text), stoneCost = Int64.Parse(labelTableAbyssStone.Text), foodCost = Int64.Parse(labelTableAbyssFood.Text), goldCost = Int64.Parse(labelTableAbyssGold.Text), orksCost = Int64.Parse(labelTableAbyssOrks.Text), trollsCost = Int64.Parse(labelTableAbyssTrolls.Text);
                if (wood >= woodCost && stone >= stoneCost && food >= foodCost && gold >= goldCost && orks >= orksCost && trolls >= trollsCost)
                {
                    ++nazguls;
                    labelNazguls.Text = "Nazgule: " + nazguls.ToString();
                    if (nazguls == 1)
                        finishIncrement();
                    wood -= woodCost;
                    labelWood.Text = "Drewno: " + wood.ToString();
                    stone -= stoneCost;
                    labelStone.Text = "Kamień: " + stone.ToString();
                    food -= foodCost;
                    labelFood.Text = "Jedzenie: " + food.ToString();
                    gold -= goldCost;
                    labelGold.Text = "Złoto: " + gold.ToString();
                    orks -= orksCost;
                    labelOrks.Text = "Orkowie: " + orks.ToString();
                    trolls -= trollsCost;
                    labelTrolls.Text = "Trolle: " + trolls.ToString();
                    if (nazguls == 9)
                    {
                        buttonAbyssUpgrade.Visible = buttonAbyssUpgrade.Enabled = false;
                        woodCost = stoneCost = foodCost = goldCost = orksCost = trollsCost = 0;
                    }
                    else
                    {
                        woodCost *= 5 * nazguls;
                        stoneCost *= 6 * nazguls;
                        foodCost *= 4 * nazguls;
                        goldCost *= 5 * (nazguls + 1);
                        orksCost *= 10 * nazguls;
                        trollsCost *= 7 * nazguls;
                    }
                    labelTableAbyssWood.Text = woodCost.ToString();
                    labelTableAbyssStone.Text = stoneCost.ToString();
                    labelTableAbyssFood.Text = foodCost.ToString();
                    labelTableAbyssGold.Text = goldCost.ToString();
                    labelTableAbyssOrks.Text = orksCost.ToString();
                    labelTableAbyssTrolls.Text = trollsCost.ToString();
                }
            }
        }

        /// <summary>
        /// Funkcja do odliczania wygranej
        /// </summary>
        private async void finishIncrement()
        {
            for (int delay = 10000; progressBarFinish.Value < progressBarFinish.Maximum;)
            {
                await Task.Delay(delay);
                progressBarFinish.PerformStep();
                delay = 10000 - nazguls * 100;
                if (sawmillLevel == 25)
                    delay -= 20;
                if (mineLevel == 25)
                    delay -= 20;
                if (butcheryLevel == 25)
                    delay -= 20;
                if (orksBreedLevel == 25)
                    delay -= 20;
                if (trollSpawnLevel == 25)
                    delay -= 20;
            }
            timeDuration = TimeSpan.FromMilliseconds(DateTime.Now.Millisecond - startTime.Millisecond);
            victorySound.PlayLooping();
            labelVictory.Visible = labelVictory.Enabled = textBoxPlayerName.Visible = textBoxPlayerName.Enabled = pictureBoxVictory.Visible = pictureBoxVictory.Enabled = true;
        }

        /// <summary>
        /// Zakończenie programu i zapisanie czasu gry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelVictory_Click(object sender, EventArgs e)
        {
            if(textBoxPlayerName.Text.Length>0)
            {
                System.IO.FileInfo file = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + @"\Score\" + textBoxPlayerName.Text + ".txt");
                file.Directory.Create();
                System.IO.File.WriteAllText(file.FullName, "Gra ukończona w: " + timeDuration.Seconds.ToString() + " sec");
                Application.Exit();
            }
        }
    }
}
