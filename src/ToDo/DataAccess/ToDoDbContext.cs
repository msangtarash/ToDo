using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using ToDo.Model;
using Xamarin.Forms;

namespace ToDo.DataAccess
{
    public class ToDoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbFileName = "toDoDb-V1.db";

            if (Device.RuntimePlatform != Device.UWP)
                dbFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbFileName);

            optionsBuilder.UseSqlite($"Filename={dbFileName}");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>()
                .HasOne(toDo => toDo.Group)
                .WithMany(toDoGroup => toDoGroup.ToDoItems)
                .HasForeignKey(toDo => toDo.GroupId)
                .IsRequired(required: false);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ToDoItem> ToDoItems { get; set; }

        public virtual DbSet<ToDoGroup> ToDoGroups { get; set; }
    }
}
