using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Validaciones
{
    public class TipoArchivoValidacion : ValidationAttribute
    {
        private readonly string[] formatoArchivo;

        public TipoArchivoValidacion(String[] FormatoArchivo)
        {
            formatoArchivo = FormatoArchivo;
        }
        public TipoArchivoValidacion(GrupoTipoEnum grupoArchivo)
        {
            if (grupoArchivo == GrupoTipoEnum.Imagen) 
            {
                formatoArchivo = new String[] { "imagen/jpg", "imagen/png","imagen/gif"};
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) { return ValidationResult.Success; }

            IFormFile formFile = value as IFormFile;
            if (formFile == null) { return ValidationResult.Success; }

            if (!formatoArchivo.Contains(formFile.ContentType)) 
            {
                return new ValidationResult($"El archivo no es de formato {string.Join(",",formatoArchivo)}");
            }

            return ValidationResult.Success;
        }
    }
}
