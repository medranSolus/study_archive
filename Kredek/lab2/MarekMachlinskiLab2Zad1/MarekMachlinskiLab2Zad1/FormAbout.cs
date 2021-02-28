using System;
using System.Windows.Forms;

namespace MarekMachlinskiLab2Zad1
{
    /// <summary>
    /// Okno opowiadające o grze
    /// </summary>
    public partial class FormAbout : Form
    {
        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();
            labelAbout.Text = $"Witaj!{Environment.NewLine}{Environment.NewLine}" +
                $"Twoim zadaniem jest zdobycie jak największej liczby punktów poprzez zwalczenie innych tancerzy na parkiecie.{Environment.NewLine}" +
                $"Jako Król Disco musisz im udowodnić, że jesteś niepodzielnym panem i władcą całego świata Boogie!{Environment.NewLine}" +
                $"Atakuj ich swoimi tanecznymi krokami, gdy tylko pojawią Ci się przed oczami i zmieść się w czasie!{Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"Użycie klas:{Environment.NewLine}Interfejs IDance udostępnia metody do poruszania przeciwników i zadawania im obrażeń.{Environment.NewLine}" +
                $"Klasa abstrakcyjna Dancer implementuje ten interfejs umożliwiając przesłonięcie metody Die,{Environment.NewLine}" +
                $"oraz nakazująca implementację metody abstrakcyjnej Dance.{Environment.NewLine}" +
                $"Po tej klasie dziedziczą klasy MainDancer oraz BossDancer.{Environment.NewLine}" +
                $"Obydwie przesłaniają metodę Dance implementując ją w inny sposób.{Environment.NewLine}" +
                $"Klasa BossDancer dodatkowo przesłania metodę Die w celu kontroli życia bossa na ekranie gracza.{Environment.NewLine}" +
                $"Klasa Enemy zawiera w sobie graficzą reprezentację przeciwnika{Environment.NewLine}" +
                $"oraz jego logikę w postaci pola klasy Dancer przechowującego klasy MainDancer lub BossDancer.{Environment.NewLine} {Environment.NewLine}";
        }
    }
}
