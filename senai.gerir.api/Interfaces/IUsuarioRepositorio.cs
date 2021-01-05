using senai.gerir.api.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.gerir.api.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario Cadastrar(Usuario usuario);

        Usuario Logar(string email, string senha);

        Usuario Editar(Usuario usuario);

        void Remover(Guid Id);

        Usuario BuscarPorId(Guid id);
    }
}
