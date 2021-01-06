using senai.gerir.api.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.gerir.api.Interfaces
{
    interface ITarefaRepositorio
    {
        Tarefa Cadastrar(Tarefa tarefa);

        List<Tarefa> ListarTodos(Guid IdUsuario);

        Tarefa AlteraStatus(Guid IdTarefa);

        void Remover(Guid IdTarefa);

        Tarefa Editar(Tarefa tarefa);

        Tarefa BuscarPorId(Guid IdTarefa);
    }
}
