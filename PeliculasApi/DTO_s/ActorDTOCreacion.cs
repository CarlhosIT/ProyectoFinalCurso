using Microsoft.AspNetCore.Http;
using PeliculasApi.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTO_s
{
    public class ActorDTOCreacion: ActorPatchDTO
    {
        [PesoIArchivoValidacion(PesoMaximo: 4)]
        //[TipoArchivoValidacion(GrupoTipoEnum.Imagen)]
        public IFormFile Imagen { get; set; }
    }
}
