using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Servicios
{
    public class IAlmacendorArchivoLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpCOntextAcccssor;

        public IAlmacendorArchivoLocal(IWebHostEnvironment env, IHttpContextAccessor httpCOntextAcccssor)
        {
            this.env = env;
            this.httpCOntextAcccssor = httpCOntextAcccssor;
        }

        public Task BorrarArchivo(string ruta, string Contenedor)
        {

            if (ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                var directorioArchivo = Path.Combine(env.WebRootPath, Contenedor, nombreArchivo);
                if (File.Exists(directorioArchivo)) 
                {
                    File.Delete(directorioArchivo);
                }
            }

            return Task.FromResult(0);
        }

        public async Task<string> EditarArchivo(byte[] Contenido, string Extension, string Contenedor,String ruta, string ContentType)
        {
            await BorrarArchivo(ruta,Contenedor);
            return await GuardarArchivo(Contenido,Extension,Contenedor,ContentType);
        }

        public async Task<string> GuardarArchivo(byte[] Contenido, string Extension, string Contenedor, string ContentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{Extension}";
            var Folder = Path.Combine(env.WebRootPath,Contenedor);
            if (!Directory.Exists(Folder)) 
            {
                Directory.CreateDirectory(Folder);
            }
            String ruta = Path.Combine(Folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta,Contenido);

            var urlActual = $"{httpCOntextAcccssor.HttpContext.Request.Scheme}://{httpCOntextAcccssor.HttpContext.Request.Host}";
            var urlParaBD = Path.Combine(urlActual, Contenedor, nombreArchivo).Replace("\\", "/");
            return urlParaBD;
        }
    }
}
