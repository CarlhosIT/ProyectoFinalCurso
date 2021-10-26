using PeliculasApi.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Helper
{
    public static class QueryableExtension
    {
        public static IQueryable<T> Paginacion<T>(this IQueryable<T> queryable, PaginaDTO paginaDTO)
        {
            return queryable.Skip((paginaDTO.pagina - 1) * paginaDTO.RegistroPorPagina).
                Take(paginaDTO.RegistroPorPagina);
        }
    }
}
