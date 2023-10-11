using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DutyScheduleBuilderWPF.Entities;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DutyScheduleBuilderWPF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Kitchen> Kitchens { get; set; }
        public DbSet<Student> Students { get; set; }


        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string logFilePath = "log_file.sql";

            optionsBuilder.UseSqlite("Data Source=database.db");
            optionsBuilder.LogTo(output => File.AppendAllText(logFilePath, output + Environment.NewLine), LogLevel.Information);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Создаем этажи
            for (int i = 1; i <= 12; i++)
            {
                var floor = new Floor { Id = i };
                modelBuilder.Entity<Floor>().HasData(floor);
                switch (i)
                {
                    case 1:
                        modelBuilder.Entity<Kitchen>().HasData(
                            new Kitchen { Id = i * 100 + 3, FloorId = i } 
                        );
                        break;
                    case 2:
                        modelBuilder.Entity<Kitchen>().HasData(
                            new Kitchen { Id = i * 100 + 2, FloorId = i },
                            new Kitchen { Id = i * 100 + 9, FloorId = i },
                            new Kitchen { Id = i * 100 + 11, FloorId = i }
                        );
                        break;
                    case int n when n > 2 && n < 10:
                        modelBuilder.Entity<Kitchen>().HasData(
                            new Kitchen { Id = i * 100 + 2, FloorId = i },
                            new Kitchen { Id = i * 100 + 5, FloorId = i },
                            new Kitchen { Id = i * 100 + 9, FloorId = i },
                            new Kitchen { Id = i * 100 + 11, FloorId = i }
                        );
                        break;
                    case int n when n > 9 && n <= 12:
                        modelBuilder.Entity<Kitchen>().HasData(
                            new Kitchen { Id = i * 100 + 3, FloorId = i },
                            new Kitchen { Id = i * 100 + 5, FloorId = i }
                        );
                        break;
                }
            }
        }

    }
}
