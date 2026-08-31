namespace EinnahmeAusgabe.Models
{
    public class StatistikViewModel
    {
        public DateTime? DatumVon { get; set; }
        public DateTime? DatumBis { get; set; }

        public decimal SummeEinnahmen { get; set; }
        public decimal SummeAusgaben { get; set; }

        public decimal DurchschnittEinnahmen { get; set; }
        public decimal DurchschnittAusgaben { get; set; }

        public List<StatistikEintrag> TopEinnahmen { get; set; } = new();
        public List<StatistikEintrag> TopAusgaben { get; set; } = new();
    }

    public class StatistikEintrag
    {
        public string Bezeichnung { get; set; } = string.Empty;
        public decimal Betrag { get; set; }
    }
}