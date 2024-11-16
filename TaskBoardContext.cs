using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication
{
    public class TaskBoardContext : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {          
            // Путь к базе данных
            optionsBuilder.UseSqlite("Data Source=taskboard.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Дополнительные настройки модели, если необходимо
        }
    }
}
