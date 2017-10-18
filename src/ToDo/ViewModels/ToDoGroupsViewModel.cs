using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDo.DataAccess;
using ToDo.Model;

namespace ToDo.ViewModels
{
    public class ToDoGroupsViewModel : BindableBase
    {
        private readonly ToDoDbContext dbContext;
        private readonly INavigationService _NavigationService;

        public DelegateCommand LoadToDoGroups { get; set; }
        public DelegateCommand AddToDoGroup { get; set; }
        public DelegateCommand<ToDoGroup> DeleteToDoGroup { get; set; }
        public DelegateCommand<ToDoGroup> OpenToDoItems { get; set; }

        private ObservableCollection<ToDoGroup> _ToDoGroups;
        public ObservableCollection<ToDoGroup> ToDoGroups
        {
            get { return _ToDoGroups; }
            set { SetProperty(ref _ToDoGroups, value); }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetProperty(ref _IsBusy, value); }
        }

        private string _NewToDoGroupName;
        public string NewToDoGroupName
        {
            get { return _NewToDoGroupName; }
            set { SetProperty(ref _NewToDoGroupName, value); }
        }

        public ToDoGroupsViewModel(INavigationService navigationService)
        {
            _NavigationService = navigationService;

            dbContext = new ToDoDbContext();

            LoadToDoGroups = new DelegateCommand(async () =>
            {
                try
                {
                    IsBusy = true;

                    await dbContext.Database.EnsureCreatedAsync();

                    await dbContext.ToDoGroups.LoadAsync();

                    ToDoGroups = new ObservableCollection<ToDoGroup>(dbContext.ToDoGroups.Local);
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

                    ToDoGroup toDoGroup = new ToDoGroup { Name = NewToDoGroupName };

                    await dbContext.ToDoGroups.AddAsync(toDoGroup);

                    await dbContext.SaveChangesAsync();

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
                    dbContext.Entry(toDoGroup).State = EntityState.Deleted;
                    await dbContext.SaveChangesAsync();
                    ToDoGroups.Remove(toDoGroup);
                }
                finally
                {
                    IsBusy = false;
                }
            });

            OpenToDoItems = new DelegateCommand<ToDoGroup>(async (toDoGroup) =>
            {
                await navigationService.NavigateAsync("ToDoList", new Dictionary<string, object>
                {
                    { "toDoGroupId", toDoGroup.Id }
                }.ToNavParams());
            });
        }
    }
}
