using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ContaCorrente> ContaCorrentes { get; set; }
        public DbSet<ContaPoupanca> ContaPoupancas { get; set; }
        public DbSet<Transacao> Transacaos{ get; set; }
       
    }
}
