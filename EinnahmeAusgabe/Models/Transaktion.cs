using System.ComponentModel.DataAnnotations;

namespace EinnahmeAusgabe.Models
{
    public class Transaktion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bitte eine Bezeichnung eingeben.")]
        [StringLength(50, ErrorMessage = "Die Bezeichnung darf maximal 50 Zeichen lang sein.")]
        public string Bezeichnung { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bitte einen Betrag eingeben.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Der Betrag muss größer als 0 sein.")]
        public decimal Betrag { get; set; }

        public DateTime Erstellungsdatum { get; set; }

        [Required(ErrorMessage = "Bitte einen Transaktionstyp auswählen.")]
        public TransaktionTyp Typ { get; set; }

        public int? KategorieId { get; set; }

        public Kategorie? Kategorie { get; set; }
    }

    public enum TransaktionTyp
    {
        Einnahme,
        Ausgabe
    }
}