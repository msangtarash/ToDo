using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDo.DataAccess;
using ToDo.Model;

namespace ToDo.ViewModels
{
    public class ToDoGroupsViewModel : BindableBase, IDestructible
    {
        private readonly ToDoDbContext _dbContext;
        private readonly INavigationService _navigationService;

        public DelegateCommand LoadToDoGroups { get; set; }
        public DelegateCommand AddToDoGroup { get; set; }
        public DelegateCommand<ToDoGroup> DeleteToDoGroup { get; set; }
        public DelegateCommand<ToDoGroup> OpenToDoItems { get; set; }

        private ObservableCollection<ToDoGroup> _ToDoGroups;
        public ObservableCollection<ToDoGroup> ToDoGroups
        {
            get => _ToDoGroups;
            set => SetProperty(ref _ToDoGroups, value);
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value);
        }

        private string _NewToDoGroupName;
        public string NewToDoGroupName
        {
            get => _NewToDoGroupName;
            set => SetProperty(ref _NewToDoGroupName, value);
        }

        private string _CountToDoItems;
        public virtual string CountToDoItems
        {
            get => _CountToDoItems;
            set => SetProperty(ref _CountToDoItems, value);
        }

        public ToDoGroupsViewModel(INavigationService navigationService, ToDoDbContext dbContext)
        {
            _navigationService = navigationService;

            _dbContext = dbContext;

            LoadToDoGroups = new DelegateCommand(async () =>
            {
                try
                {
                    IsBusy = true;

                    await _dbContext.Database.EnsureCreatedAsync();

                    await _dbContext.ToDoGroups.LoadAsync();

                    ToDoGroups = new ObservableCollection<ToDoGroup>(_dbContext.ToDoGroups.Local);
                }
                finally
                {
                    IsBusy = false;
                }
            });

            AddToDoGroup = new DelegateCommand(async () =>
            {
                try
                {
                    IsBusy = true;

                    ToDoGroup toDoGroup = new ToDoGroup { Name = NewToDoGroupName, CreatedDateTime = DateTimeOffset.UtcNow };

                    await _dbContext.ToDoGroups.AddAsync(toDoGroup);

                    await _dbContext.SaveChangesAsync();

                    ToDoGroups.Add(toDoGroup);

                    NewToDoGroupName = "";
                }
                finally
                {
                    IsBusy = false;
                }
            }, () => !IsBusy && !string.IsNullOrEmpty(NewToDoGroupName));

            AddToDoGroup.ObservesProperty(() => IsBusy);
            AddToDoGroup.ObservesProperty(() => NewToDoGroupName);

            DeleteToDoGroup = new DelegateCommand<ToDoGroup>(async toDoGroup =>
            {
                try
                {
                    IsBusy = true;
                    _dbContext.Entry(toDoGroup).State = EntityState.Deleted;
                    await _dbContext.SaveChangesAsync();
                    ToDoGroups.Remove(toDoGroup);
                }
                finally
                {
                    IsBusy = false;
                }
            });

            OpenToDoItems = new DelegateCommand<ToDoGroup>(async (toDoGroup) =>
            {
                await navigationService.NavigateAsync("Nav/ToDoItems", new Dictionary<string, object>
                {
                    { "toDoGroupId", toDoGroup.Id }
                }.ToNavParams());
            });
        }

        public virtual void Destroy()
        {
            _dbContext.Dispose();
        }
    }
}
