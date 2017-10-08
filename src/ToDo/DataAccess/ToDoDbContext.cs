﻿using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using ToDo.Model;

namespace ToDo.DataAccess
{
    public class ToDoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "toDoDb3.db")}");

            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<ToDoItem> ToDoItems { get; set; }

        public virtual DbSet<GroupToDoItem> GroupToDoItems { get; set; }
    }
}
