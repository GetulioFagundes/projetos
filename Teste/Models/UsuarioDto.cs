using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Models
{
    public class UsuarioDto
    {
        public int Codigo {get; set;}
        public string PrimeiroNome{get; set;}
        public string UltimoNome{get; set;}
        public string Usuario{get; set;}
        public string Password {get; set;}
    }
}
