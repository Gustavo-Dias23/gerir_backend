using senai.gerir.api.Contextos;
using senai.gerir.api.Dominios;
using senai.gerir.api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.gerir.api.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly GerirContext _context;

        public UsuarioRepositorio()
        {
            _context = new GerirContext();
        }
        public Usuario BuscarPorId(Guid id)
        {
            try
            {
                var usuario = _context.Usuarios.Find(id);

                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Usuario Editar(Usuario usuario)
        {
            try
            {
                var usuarioexiste = BuscarPorId(usuario.Id);

                if (usuarioexiste == null)
                    throw new Exception("Usuário não encontrado");

                usuarioexiste.Nome = usuario.Nome;
                usuarioexiste.Email = usuario.Email;

                if (!string.IsNullOrEmpty(usuario.Senha))
                    usuarioexiste.Senha = usuario.Senha;

                _context.Usuarios.Update(usuarioexiste);
                _context.SaveChanges();

                return usuarioexiste;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Usuario Logar(string email, string senha)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(c => c.Email == email && c.Senha == senha);

                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Remover(Guid Id)
        {
            try
            {
                var usuario = BuscarPorId(Id);

                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
