using System.ComponentModel.DataAnnotations;

namespace EinnahmeAusgabe.Models
{
    public class Kategorie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bitte einen Namen eingeben.")]
        [StringLength(100, ErrorMessage = "Der Name darf maximal 100 Zeichen lang sein.")]
        public string Name { get; set; } = string.Empty;

        public string? Farbe { get; set; }

        public ICollection<Transaktion> Transaktionen { get; set; }
            = new List<Transaktion>();
    }
}