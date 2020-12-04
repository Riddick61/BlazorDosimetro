using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDosimetro.Api.DTOs
{
    public class DrugDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DoseToTake { get; set; }
        [Required]
        public int NoPills { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime DateToEnd { get; set; }
    }
}
