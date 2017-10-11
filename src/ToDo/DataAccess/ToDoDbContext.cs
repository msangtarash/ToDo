using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using ToDo.Model;

namespace ToDo.DataAccess
{
    public class ToDoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "toDoDb-V1.db")}");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>()
                .HasOne(toDo => toDo.Group)
                .WithMany(toDoGroup => toDoGroup.ToDoItems)
                .HasForeignKey(toDo => toDo.GroupId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ToDoItem> ToDoItems { get; set; }

        public virtual DbSet<ToDoGroup> ToDoGroups { get; set; }
    }
}
