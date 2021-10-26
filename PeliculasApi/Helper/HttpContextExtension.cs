using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace PeliculasApi.Helper
{
    public static class HttpContextExtension
    {
        public async static Task InsertarParametros<T>(this HttpContext Httpcontext, IQueryable<T> queryable,
            int cantidadRegistrosPag) 
        {
            double cantidad =await queryable.CountAsync();
            double cantidadPaginas = Math.Ceiling(cantidad/cantidadRegistrosPag);
            Httpcontext.Response.Headers.Add("cantidadPaginas",cantidadPaginas.ToString());

        }
    }
}
