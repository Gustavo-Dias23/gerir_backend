using senai.gerir.api.Contextos;
using senai.gerir.api.Dominios;
using senai.gerir.api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.gerir.api.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly GerirContext _context;

        public TarefaRepositorio()
        {
            _context = new GerirContext();
        }
        public Tarefa AlteraStatus(Guid IdTarefa)
        {
            try
            {
                var tarefaexiste = BuscarPorId(IdTarefa);
                if (tarefaexiste == null)
                    throw new Exception("Tarefa não encontrada");

                tarefaexiste.Status = !tarefaexiste.Status;

                _context.Tarefas.Update(tarefaexiste);
                _context.SaveChanges();

                return tarefaexiste;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Tarefa BuscarPorId(Guid IdTarefa)
        {
            try
            {
                var tarefa = _context.Tarefas.Find(IdTarefa);

                return tarefa;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Tarefa Cadastrar(Tarefa tarefa)
        {
            try
            {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();
                return tarefa;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Tarefa Editar(Tarefa tarefa)
        {
            try
            {
                var tarefaexiste = BuscarPorId(tarefa.Id);

                if (tarefaexiste == null)
                    throw new Exception("Tarefa não encontrada");

                tarefaexiste.Titulo = tarefa.Titulo;
                tarefaexiste.Descricao = tarefa.Descricao;

                _context.Tarefas.Update(tarefaexiste);
                _context.SaveChanges();

                return tarefaexiste;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Tarefa> ListarTodos(Guid IdUsuario)
        {
            throw new NotImplementedException();
        }

        public void Remover(Guid IdTarefa)
        {
            var tarefa = BuscarPorId(IdTarefa);

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
        }
    }
}
