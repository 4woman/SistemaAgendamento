using System.Collections.Generic;
using SistemaAgendamento.Models;

namespace SistemaAgendamento.Data
{
    public class ApplicationDbContext
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Servico> Servicos { get; set; }
            public DbSet<Profissional> Profissionais { get; set; }
            public DbSet<Horario> Horarios { get; set; }
            public DbSet<Agendamento> Agendamentos { get; set; }

        }
    }
}
