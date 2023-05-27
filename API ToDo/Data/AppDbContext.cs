using Microsoft.EntityFrameworkCore;
using MyToDo.Models;

namespace MyToDo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=mytodo;user=root;password=12345");
        }
    }
}