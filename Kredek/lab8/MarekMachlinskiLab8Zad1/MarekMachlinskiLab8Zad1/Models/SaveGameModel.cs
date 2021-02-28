namespace MarekMachlinskiLab8Zad1.Models
{
    /// <summary>
    /// Model pliku zapisu do przesyłania
    /// </summary>
    public class SaveGameModel
    {
        /// <summary>
        /// Id pliku
        /// </summary>
        public int ID { get; set; } = 0;
        /// <summary>
        /// Nazwa gracza
        /// </summary>
        public string PlayerLogin { get; set; }
        /// <summary>
        /// Data zapisu
        /// </summary>
        public System.DateTime LastSaveDate { get; set; } = System.DateTime.Now;
        /// <summary>
        /// Niewykorzystane punkty
        /// </summary>
        public int PointsLeft { get; set; } = 0;
        /// <summary>
        /// Poziom ulepszeń Assemblera
        /// </summary>
        public int AssemblerLevel { get; set; } = 0;
        /// <summary>
        /// Poziom ulepszeń C
        /// </summary>
        public int CLevel { get; set; } = 0;
        /// <summary>
        /// Poziom ulepszeń C++
        /// </summary>
        public int CppLevel { get; set; } = 0;
        /// <summary>
        /// Poziom ulepszeń C#
        /// </summary>
        public int CSharpLevel { get; set; } = 0;
        /// <summary>
        /// Poziom ulepszeń F#
        /// </summary>
        public int FSharpLevel { get; set; } = 0;
        /// <summary>
        /// Poziom ulepszeń Javy
        /// </summary>
        public int JavaLevel { get; set; } = 0;
        /// <summary>
        /// Poziom ulepszeń JavaScript
        /// </summary>
        public int JavaScriptLevel { get; set; } = 0;
        /// <summary>
        /// Poziom ulepszeń PHP
        /// </summary>
        public int PHPLevel { get; set; } = 0;
        /// <summary>
        /// Poziom ulepszeń Pythona
        /// </summary>
        public int PythonLevel { get; set; } = 0;
        /// <summary>
        /// Poziom ulepszeń VIsual Basica
        /// </summary>
        public int VisualBasicLevel { get; set; } = 0;
    }
}