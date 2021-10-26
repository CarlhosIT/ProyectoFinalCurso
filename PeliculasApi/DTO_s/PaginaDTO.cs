using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTO_s
{
    public class PaginaDTO
    {
        public int pagina { get; set; } = 1;
        private int registroPorPagina = 10;
        private int registroMaximoPaginas = 50;

        public int RegistroPorPagina
            {
                get => registroPorPagina;
                set{
                        registroPorPagina=(value >registroMaximoPaginas)? registroMaximoPaginas:value;
    }
            }

    }
}
