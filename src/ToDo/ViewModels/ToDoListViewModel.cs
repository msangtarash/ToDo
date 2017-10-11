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
    public class ToDoListViewModel : BindableBase, IDestructible
    {
        private readonly ToDoDbContext _dbContext;

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

        public ToDoListViewModel()
        {
            _dbContext = new ToDoDbContext();

            LoadToDoItems = new DelegateCommand<bool?>(async (loadAll) =>
            {
                try
                {
                    IsBusy = true;

                    //await Task.Delay(1000);

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
                if (toDoItem == null)
                    return;

                try
                {
                    IsBusy = true;

                    //await Task.Delay(1000);

                    toDoItem.IsFinished = !toDoItem.IsFinished;

                    await _dbContext.SaveChangesAsync();
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

                    //await Task.Delay(1000);

                    ToDoItem toDoItem = new ToDoItem { IsFinished = false, Text = NewToDoText };

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
