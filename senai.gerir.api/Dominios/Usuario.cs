using System;
using System.Collections.Generic;

#nullable disable

namespace senai.gerir.api.Dominios
{
    public partial class Usuario
    {
        public Usuario()
        {
            Tarefas = new HashSet<Tarefa>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }

        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
