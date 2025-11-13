using Microsoft.EntityFrameworkCore;
using ASLCaixaEletronico.Models;

namespace ASLCaixaEletronico.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<Usuario> Usuarios { get; set; } = null!;
    }
}
