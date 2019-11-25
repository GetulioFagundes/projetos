using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Context;

namespace Teste.Models
{
    public class UsuarioService : IUsuarioService
    {
        private AppDtContext _context;
        public UsuarioService(AppDtContext context)
        {
            _context = context;
        }

        public Usuario Authenticate(string usuario, string password)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
                return null;
            var user = _context.Usuario.SingleOrDefault(x => x.GetUsuario() == usuario);

            if (usuario == null)
                return null;
            if (!VerifyPasswordHash(password, user.PassHash, user.PassSalt))
                return null;

            return user;
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuario;
        }
        public Usuario GetById(int id)
        {
            return _context.Usuario.Find(id);
        }

        public Usuario Create(Usuario usuario, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Informe o password");
            if (_context.Usuario.Any(x => x.GetUsuario() == usuario.GetUsuario()))
                throw new Exception("Usuáiro já cadastrado");

            byte[] passHash, passSalt;
            CreatePasswordHash(password, out passHash, out passSalt);
            usuario.PassHash = passHash;
            usuario.PassSalt = passSalt;
            _context.Usuario.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public void Update(Usuario usuarioParam, string password = null)
        {
            var user = _context.Usuario.Find(usuarioParam.Codigo);

            if (user == null)
                throw new Exception("Não existe usuario");
            if(usuarioParam.GetUsuario() != user.GetUsuario())
            {
                if (_context.Usuario.Any(x => x.GetUsuario() == usuarioParam.GetUsuario()))
                    throw new Exception("Usuario " + usuarioParam + " ja existe");
            }

            // update user propriedades
            user.PrimeiroNome = usuarioParam.PrimeiroNome;
            user.UltimoNome = usuarioParam.UltimoNome;
            user.SetUsuario(usuarioParam.GetUsuario());

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PassHash = passwordHash;
                user.PassSalt = passwordSalt;
            }

            _context.Usuario.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Usuario.Find(id);
            if (user != null)
            {
                _context.Usuario.Remove(user);
                _context.SaveChanges();
            }
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Necessario informar o passwords", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Tamanho excedido", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Tamanho excedido", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
