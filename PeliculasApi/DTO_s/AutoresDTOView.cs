using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTO_s
{
    public class AutoresDTOView
    {
        public int Id { get; set; }
        [Required]
        public String Nombre { get; set; }
    }
}
