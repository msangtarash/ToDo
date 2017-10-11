using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using ToDo.DataAccess;
using ToDo.Model;

namespace ToDo.ViewModels
{
    public class ToDoListViewModel : BindableBase, IDestructible, INavigatedAware
    {
        private readonly ToDoDbContext _dbContext;

        private int _toDoGroupId;

        public virtual DelegateCommand<bool?> LoadToDoItems { get; set; }

        public virtual DelegateCommand<ToDoItem> ToggleToDoItemIsFinished { get; set; }

        public virtual DelegateCommand AddToDoItem { get; set; }

        public virtual DelegateCommand<ToDoItem> DeleteTodoItem { get; set; }

        private ObservableCollection<ToDoItem> _ToDoItems;

        public virtual ObservableCollection<ToDoItem> ToDoItems
        {
            get { return _ToDoItems; }
            set { SetProperty(ref _ToDoItems, value); }
        }

        private bool _IsBusy;
        public virtual bool IsBusy
        {
            get { return _IsBusy; }
            set { SetProperty(ref _IsBusy, value); }
        }

        private string _NewToDoText;

        public virtual string NewToDoText
        {
            get { return _NewToDoText; }
            set { SetProperty(ref _NewToDoText, value); }
        }

        private IQueryable<ToDoItem> GetToDoItemsQuery(IQueryable<ToDoItem> toDoItemsBaseQuery, bool loadAll)
        {
            toDoItemsBaseQuery = toDoItemsBaseQuery.Where(todo => todo.GroupId == _toDoGroupId);

            if (loadAll == false)
                toDoItemsBaseQuery = toDoItemsBaseQuery.Where(toDo => toDo.IsFinished == false);
            else
                toDoItemsBaseQuery = toDoItemsBaseQuery.OrderBy(toDo => toDo.IsFinished == true);

            return toDoItemsBaseQuery;
        }

        public virtual void Destroy()
        {
            _dbContext.Dispose();
        }


        public virtual void OnNavigatedTo(NavigationParameters navigationParams)
        {
            _toDoGroupId = navigationParams.GetValue<int>("toDoGroupId");

            LoadToDoItems.Execute(false);
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public ToDoListViewModel()
        {
            _dbContext = new ToDoDbContext();

            LoadToDoItems = new DelegateCommand<bool?>(async (loadAll) =>
            {
                try
                {
                    IsBusy = true;

                    await _dbContext.Database.EnsureCreatedAsync();

                    await GetToDoItemsQuery(_dbContext.ToDoItems, loadAll.Value).LoadAsync();

                    ToDoItems = new ObservableCollection<ToDoItem>(GetToDoItemsQuery(_dbContext.ToDoItems.Local.AsQueryable(), loadAll.Value));
                }
                finally
                {
                    IsBusy = false;
                }
            }, (loadAll) => loadAll.HasValue && !IsBusy);

            LoadToDoItems.ObservesProperty(() => IsBusy);

            ToggleToDoItemIsFinished = new DelegateCommand<ToDoItem>(async (toDoItem) =>
            {
                try
                {
                    IsBusy = true;

                    toDoItem.IsFinished = !toDoItem.IsFinished;

                    await _dbContext.SaveChangesAsync();
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

                    ToDoItem toDoItem = new ToDoItem { IsFinished = false, Text = NewToDoText, GroupId = _toDoGroupId };

                    await _dbContext.ToDoItems.AddAsync(toDoItem);

                    await _dbContext.SaveChangesAsync();

                    ToDoItems.Add(toDoItem);

                    NewToDoText = "";
                }
                finally
                {
                    IsBusy = false;
                }
            }, () => !IsBusy && !string.IsNullOrEmpty(NewToDoText));

            AddToDoItem.ObservesProperty(() => IsBusy);
            AddToDoItem.ObservesProperty(() => NewToDoText);

            DeleteTodoItem = new DelegateCommand<ToDoItem>(async (toDoItem) =>
            {
                try
                {
                    IsBusy = true;
                    _dbContext.Remove(toDoItem);
                    await _dbContext.SaveChangesAsync();
                    ToDoItems.Remove(toDoItem);
                }
                finally
                {
                    IsBusy = false;
                }

            }, (toDoItem) => !IsBusy);

            DeleteTodoItem.ObservesProperty(() => IsBusy);
        }
    }
}
