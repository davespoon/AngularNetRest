using DimkasBoardGames.Models;
using Microsoft.EntityFrameworkCore;

namespace DimkasBoardGames
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<BoardGame>().HasOne(game => game.Feedback).WithOne(f => f.BoardGame)
//                .HasForeignKey<Feedback>(f => f.BoardGameRef);
//        }


        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<BoardGameGenre> BoardGameGenres { get; set; }
    }
}