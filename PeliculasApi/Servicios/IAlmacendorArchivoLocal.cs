using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Servicios
{
    public class IAlmacendorArchivoLocal : IAlmacenadorArchivos
    {
        public IAlmacendorArchivoLocal()
        {
        }

        public Task BorrarArchivo(string ruta, string Contenedor)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditarArchivo(byte[] Contenido, string Extension, string Contenedor, string ContentType)
        {
            throw new NotImplementedException();
        }

        public Task<string> GuardarArchivo(byte[] Contenido, string Extension, string Contenedor, string ContentType)
        {
            throw new NotImplementedException();
        }
    }
}
