using Microsoft.AspNetCore.Http;
using PeliculasApi.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTO_s
{
    public class ActorPatchDTO
    {
        [Required]
        [StringLength(50)]
        public String Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

       
    }
}
