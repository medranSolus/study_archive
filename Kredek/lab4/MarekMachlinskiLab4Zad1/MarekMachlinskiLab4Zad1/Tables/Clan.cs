using System.ComponentModel.DataAnnotations;
using System;

namespace MarekMachlinskiLab4Zad1.Tables
{
    /// <summary>
    /// Klan
    /// </summary>
    public class Clan
    {
        /// <summary>
        /// Nazwa klanu
        /// </summary>
        [Key]
        public string Name { get; set; }

        /// <summary>
        /// Data utworzenia klanu
        /// </summary>
        [Required]
        public DateTime CreationDate { get; set; }
    }
}
