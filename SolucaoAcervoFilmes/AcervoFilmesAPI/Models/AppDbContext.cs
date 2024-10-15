using Microsoft.EntityFrameworkCore;
namespace AcervoFilmesAPI.Models;

public class AppDbContext : DbContext
{
    public DbSet<Filme> Filmes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=FilmesDB.db");
    }
}