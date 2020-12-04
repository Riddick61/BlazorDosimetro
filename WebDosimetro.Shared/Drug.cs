using System;

namespace WebDosimetro.Shared
{
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DoseToTake { get; set; }
        public int NoPills { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DateToEnd { get; set; }
    }
}
