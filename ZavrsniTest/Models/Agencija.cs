using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZavrsniTest.Models
{
    public class Agencija
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(119)]
        public string Naziv { get; set; }
        [Required]
        [Range(1980,2021)]
        public int GodinaOsnivanja { get; set; }
    }
}
