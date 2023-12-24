using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindSwipe.Models;

namespace MindSwipe.Data
{
    public class MindSwipeContext : DbContext
    {
        public MindSwipeContext (DbContextOptions<MindSwipeContext> options)
            : base(options)
        {
        }

        public DbSet<MindSwipe.Models.Card> Card { get; set; } = default!;

        public DbSet<MindSwipe.Models.Deck>? Deck { get; set; }

        public DbSet<MindSwipe.Models.Users>? Users { get; set; }

        public DbSet<MindSwipe.Models.Result>? Result { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>()
                .HasIndex(u => u.Username)
                .IsUnique();

            builder.Entity<Deck>()
                .HasMany(d => d.Cards)
                .WithOne(c => c.Deck)
                .HasForeignKey(c => c.DeckId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<MindSwipe.Models.CardUploadModel>? CardUploadModel { get; set; }
    }
}
