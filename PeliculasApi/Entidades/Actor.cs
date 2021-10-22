using PeliculasApi.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Entidades
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public String Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [PesoIArchivoValidacion(PesoMaximo: 4)]
        [TipoArchivoValidacion(GrupoTipoEnum.Imagen)]
        public String Imagen { get; set; }
    }
}
