using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarekMachlinskiLab4Zad1.Tables
{
    /// <summary>
    /// Klasa przedstawiająca użytkownika
    /// </summary>
    public class User
    {
        /// <summary>
        /// Nazwa użytkownika
        /// </summary>
        [Key]
        public string Login { get; set; }

        /// <summary>
        /// Stopień wtajemniczenia użytkownika
        /// </summary>
        [ForeignKey("RankId")]
        public virtual Rank Rank { get; set; }

        /// <summary>
        /// Identyfikator obiektu Rank
        /// </summary>
        [Required]
        public int RankId { get; set; }

        /// <summary>
        /// Klan użytkownika
        /// </summary>
        [ForeignKey("ClanName")]
        public virtual Clan Clan { get; set; }

        /// <summary>
        /// Identyfikator obiektu Clan
        /// </summary>
        [Required]
        public string ClanName { get; set; }

        /// <summary>
        /// Hasło użytkownika
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public User()
        {
            Login = Password = ClanName = "none";
            RankId = 0;
        }

        /// <summary>
        /// Konstruktor inicjalizujący pola
        /// </summary>
        /// <param name="name">Nazwa użytkownika</param>
        /// <param name="newPassword">Hasło użytkownika</param>
        /// <param name="window">Główne okno z repozytoriami</param>
        public User(string name, string newPassword, FormMainWindow window)
        {
            Login = name;
            Password = newPassword;
            ClanName = "No Clan";
            RankId = 1;
            Clan = window.Clans.GetById(ClanName);
            Rank = window.Ranks.GetById(RankId);
        }
    }
}
