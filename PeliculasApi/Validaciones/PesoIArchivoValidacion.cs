using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PeliculasApi.Validaciones
{
    public class PesoIArchivoValidacion : ValidationAttribute
    {
        private readonly int pesoMaximo;

        public PesoIArchivoValidacion(int PesoMaximo)
        {
            pesoMaximo = PesoMaximo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) { return ValidationResult.Success; }

            IFormFile formFile= value as IFormFile;
            if (formFile == null) { return ValidationResult.Success; }

            if (formFile.Length > pesoMaximo * 1024 * 1024) 
            {
                return new ValidationResult($"El peso del archivo no puede ser mayor a {pesoMaximo}mb");
            }

            return ValidationResult.Success;
        }
    }
}
