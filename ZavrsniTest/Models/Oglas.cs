using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZavrsniTest.Models
{
    public class Oglas
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2),MaxLength(100)]
        public string Naslov { get; set; }
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Tip { get; set; }
        [Range(1910,2022)]
        public int GodinaIzgradnje { get; set; }
        [Required]
        [Range(10000,300000)]
        public int CenaNekretnine { get; set; }
        public Agencija Agencija { get; set; }
        public int AgencijaId { get; set; }
    }
}
