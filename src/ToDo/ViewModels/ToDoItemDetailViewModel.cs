﻿using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo.DataAccess;

namespace ToDo.ViewModels
{
    public class ToDoItemDetailViewModel : BindableBase, INavigatedAware, IDestructible
    {
        private readonly ToDoDbContext _dbContext;

        private int _toDoItemId;

        private string _ToDoItemName;
        public virtual string ToDoItemName
        {
            get { return _ToDoItemName; }
            set { SetProperty(ref _ToDoItemName, value); }
        }


        public virtual async void OnNavigatedTo(NavigationParameters navigationParams)
        {
            _toDoItemId = navigationParams.GetValue<int>("toDoItemId");

            ToDoItemName = (await _dbContext.ToDoItems.FindAsync(_toDoItemId))?.Text;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {
            _dbContext.Dispose();
        }

        public ToDoItemDetailViewModel()
        {
            _dbContext = new ToDoDbContext();
        }
    }
}