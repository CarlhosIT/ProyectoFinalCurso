using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Servicios
{
    public interface IAlmacenadorArchivos
    {
        Task<String> GuardarArchivo(byte[] Contenido,String Extension,String Contenedor,String ContentType);
        Task<String> EditarArchivo(byte[] Contenido, String Extension, String Contenedor, String ContentType);
        Task BorrarArchivo(String ruta, String Contenedor);
    }
}
