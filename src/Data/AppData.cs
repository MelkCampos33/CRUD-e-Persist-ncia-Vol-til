using Microsoft.EntityFrameworkCore;
using EleicaoApi.Models;

namespace EleicaoApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Candidato> Candidatos => Set<Candidato>();
}