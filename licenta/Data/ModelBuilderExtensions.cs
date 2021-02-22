/*using licenta.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionCategory>()
                .HasKey(sc => new { sc.QuestionId, sc.CategoryId });

            modelBuilder.Entity<QuestionCategory>()
                    .HasOne<Question>(sc => sc.Question)
                    .WithMany(s => s.QuestionCategory)
                    .HasForeignKey(sc => sc.QuestionId);


            modelBuilder.Entity<QuestionCategory>()
                .HasOne<Category>(sc => sc.Category)
                .WithMany(s => s.QuestionCategory)
                .HasForeignKey(sc => sc.CategoryId);

            modelBuilder.Entity<Category>().HasData(
                   new Category { Id =1, Name = "A" }
*//*                   new Category {  Name = "B" },
                   new Category {  Name = "BE" },
                   new Category {  Name = "C" },
                   new Category {  Name = "CE" },
                   new Category {  Name = "D" }*//*
                   );
        }
    }
}
*/