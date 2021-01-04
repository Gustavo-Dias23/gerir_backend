using System;
using System.Collections.Generic;

#nullable disable

namespace senai.gerir.api.Dominios
{
    public partial class Tarefa
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public DateTime DataEntrega { get; set; }
        public bool? Status { get; set; }
        public Guid? UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
