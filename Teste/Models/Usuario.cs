using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Models
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }

        private string usuario;

        public string GetUsuario()
        {
            return usuario;
        }

        public void SetUsuario(string value)
        {
            usuario = value;
        }

        public byte[] PassHash { get; set; }
        public byte[] PassSalt { get; set; }

    }
}
