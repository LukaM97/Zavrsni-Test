using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZavrsniTest.Models.DTO
{
    public class OglasFilterDTO
    {
        [Required]
        [Range(10000, 300000)]
        public int Najmanje { get; set; }
        [Required]
        [Range(10000,300000)]
        public int Najvise { get; set; }
    }
}
