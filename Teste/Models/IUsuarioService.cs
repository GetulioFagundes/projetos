using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Models
{
    public interface IUsuarioService
    {
        Usuario Authenticate(string usuario, string password);
        IEnumerable<Usuario> GetAll();
        Usuario GetById(int id);
        Usuario Create(Usuario user, string password);
        void Update(Usuario user, string password = null);
        void Delete(int id);
    }
}
