using AgendamentoBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoBackend.Data
{
    public class AgendaDbContext : DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Service> Servico { get; set; }
    }
}
