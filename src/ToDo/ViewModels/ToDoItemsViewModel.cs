﻿using Humanizer;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ToDo.DataAccess;
using ToDo.Model;
using Xamarin.Forms;

namespace ToDo.ViewModels
{
    public class ToDoItemsViewModel : BindableBase, IDestructible, INavigatedAware
    {
        private readonly ToDoDbContext _dbContext;

        private readonly INavigationService _navigationService;

        private int? _toDoGroupId;

        public virtual DelegateCommand LoadToDoItems { get; set; }

        public virtual DelegateCommand<ToDoItem> ToggleToDoItemIsFinished { get; set; }

        public virtual DelegateCommand AddToDoItem { get; set; }

        public virtual DelegateCommand<ToDoItem> NavigateToDetail { get; set; }

        public virtual DelegateCommand<ToDoItem> DeleteToDoItem { get; set; }

        private ObservableCollection<ToDoItem> _ToDoItems;

        public virtual ObservableCollection<ToDoItem> ToDoItems
        {
            get => _ToDoItems;
            set => SetProperty(ref _ToDoItems, value);
        }

        private bool _IsBusy;
        public virtual bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value);
        }

        private string _NewToDoText;

        public virtual string NewToDoText
        {
            get => _NewToDoText;
            set => SetProperty(ref _NewToDoText, value);
        }

        private string _GroupName;
        public virtual string GroupName
        {
            get => _GroupName;
            set => SetProperty(ref _GroupName, value);
        }

        private bool _LoadAll;
        public virtual bool LoadAll
        {
            get => _LoadAll;
            set => SetProperty(ref _LoadAll, value);
        }

        private IQueryable<ToDoItem> GetToDoItemsQuery(IQueryable<ToDoItem> toDoItemsBaseQuery)
        {
            toDoItemsBaseQuery = _toDoGroupId.HasValue ? toDoItemsBaseQuery.Where(toDo => toDo.GroupId == _toDoGroupId) : toDoItemsBaseQuery.Where(toDo => toDo.ShowInMyDay == true || toDo.GroupId== null);

            if (LoadAll == false)
                toDoItemsBaseQuery = toDoItemsBaseQuery.Where(toDo => toDo.IsFinished == false);
            else
                toDoItemsBaseQuery = toDoItemsBaseQuery.OrderBy(toDo => toDo.IsFinished == true);

            return toDoItemsBaseQuery;
        }

        public virtual void Destroy()
        {
            _dbContext.Dispose();
        }

        public virtual async void OnNavigatedTo(NavigationParameters navigationParams)
        {
            if (navigationParams.GetNavigationMode() == NavigationMode.Back)
                return;

            navigationParams.TryGetValue("toDoGroupId", out int? toDoGroupId);

            _toDoGroupId = toDoGroupId;

            LoadToDoItems.Execute();

            GroupName = _toDoGroupId.HasValue ? (await _dbContext.ToDoGroups.FindAsync(_toDoGroupId))?.Name : "My day";
        }

            public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public ToDoItemsViewModel(INavigationService navigationService, ToDoDbContext dbContext)
        {
            _dbContext = dbContext;

            _navigationService = navigationService;

            MessagingCenter.Subscribe<ToDoItem>(this, "ToDoItemRemoved", async (toDoItem) =>
            {
                DeleteToDoItem.Execute(await _dbContext.ToDoItems.FindAsync(toDoItem.Id));
            });

            LoadToDoItems = new DelegateCommand(async () =>
            {
                try
                {
                    IsBusy = true;

                    await _dbContext.Database.EnsureCreatedAsync();

                    await GetToDoItemsQuery(_dbContext.ToDoItems).LoadAsync();

                    ToDoItems = new ObservableCollection<ToDoItem>(GetToDoItemsQuery(_dbContext.ToDoItems.Local.AsQueryable()));
                }
                finally
                {
                    IsBusy = false;
                }
            }, () => !IsBusy);

            LoadToDoItems.ObservesProperty(() => IsBusy);

            ToggleToDoItemIsFinished = new DelegateCommand<ToDoItem>(async (toDoItem) =>
            {

                try
                {
                    IsBusy = true;

                    toDoItem.IsFinished = !toDoItem.IsFinished;

                    if (toDoItem.IsFinished == true)
                    {
                        toDoItem.EndedDateTime = DateTimeOffset.UtcNow;
                    }

                    await _dbContext.SaveChangesAsync();

                    if (LoadAll == false && toDoItem.IsFinished == true)
                    {
                        ToDoItems.Remove(toDoItem);
                    }
                }
                catch
                {
                    await _dbContext.Entry(toDoItem).ReloadAsync();
                    throw;
                }
                finally
                {
                    IsBusy = false;
                }

            }, (toDoItem) => !IsBusy);

            ToggleToDoItemIsFinished.ObservesProperty(() => IsBusy);

            AddToDoItem = new DelegateCommand(async () =>
            {
                try
                {
                    IsBusy = true;

                    ToDoItem toDoItem = new ToDoItem { IsFinished = false, Text = NewToDoText, GroupId = _toDoGroupId, CreatedDateTime = DateTimeOffset.UtcNow };

                    await _dbContext.ToDoItems.AddAsync(toDoItem);

                    await _dbContext.SaveChangesAsync();

                    ToDoItems.Add(toDoItem);

                    MessagingCenter.Send(toDoItem, "ToDoItemAdded");

                    NewToDoText = "";
                }
                finally
                {
                    IsBusy = false;
                }
            }, () => !IsBusy && !string.IsNullOrEmpty(NewToDoText));

            AddToDoItem.ObservesProperty(() => IsBusy);
            AddToDoItem.ObservesProperty(() => NewToDoText);

            DeleteToDoItem = new DelegateCommand<ToDoItem>(async (toDoItem) =>
            {
                try
                {
                    IsBusy = true;
                    _dbContext.Remove(toDoItem);
                    await _dbContext.SaveChangesAsync();
                    MessagingCenter.Send(toDoItem, "ToDoItemDeleted");
                    ToDoItems.Remove(toDoItem);
                }
                finally
                {
                    IsBusy = false;
                }

            }, (toDoItem) => !IsBusy);

            DeleteToDoItem.ObservesProperty(() => IsBusy);

            NavigateToDetail = new DelegateCommand<ToDoItem>(async (toDoItem) =>
            {
                await navigationService.NavigateAsync("ToDoItemDetail", new Dictionary<string, object>
                {
                    { "toDoItemId", toDoItem.Id }
                }.ToNavParams());
            });
        }
    }
}
