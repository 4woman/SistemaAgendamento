using Microsoft.EntityFrameworkCore;
using AgendamentoSistema.Models;

namespace AgendamentoSistema.Data
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais do modelo, se necessário
            // Exemplo: Definir chaves compostas, índices, etc.

            // Configurar o relacionamento entre Agendamento e Usuario (Cliente)
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Cliente)
                .WithMany(u => u.Agendamentos)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar exclusão em cascata se um usuário tiver agendamentos

            // Configurar o relacionamento entre Agendamento e Servico
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Servico)
                .WithMany(s => s.Agendamentos)
                .HasForeignKey(a => a.ServicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar o relacionamento entre Agendamento e Profissional
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Profissional)
                .WithMany(p => p.Agendamentos)
                .HasForeignKey(a => a.ProfissionalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar o relacionamento entre Horario e Profissional
            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Profissional)
                .WithMany(p => p.HorariosDisponiveis)
                .HasForeignKey(h => h.ProfissionalId)
                .OnDelete(DeleteBehavior.Cascade); // Se um profissional for removido, seus horários também são

            // Configurar a precisão da coluna Preco em Servico
            modelBuilder.Entity<Servico>()
                .Property(s => s.Preco)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
