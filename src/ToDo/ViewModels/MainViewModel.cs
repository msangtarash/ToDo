using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;
using ToDo.DataAccess;
using ToDo.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToDo.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly ToDoDbContext dbContext;

        public Command<bool> LoadToDoItems { get; set; }

        public Command<ToDoItem> ToggleToDoItemIsFinished { get; set; }

        public Command AddToDoItem { get; set; }

        public Command<ToDoItem> DeleteTodoItem { get; set; }

        private ObservableCollection<ToDoItem> _ToDoItems;

        public ObservableCollection<ToDoItem> ToDoItems
        {
            get { return _ToDoItems; }
            set { SetProperty(ref _ToDoItems, value); }
        }

        private bool _IsBusy;

        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                if (SetProperty(ref _IsBusy, value))
                    AddToDoItem.ChangeCanExecute();
            }
        }

        private string _NewToDoText;

        public string NewToDoText
        {
            get { return _NewToDoText; }
            set
            {
                if (SetProperty(ref _NewToDoText, value))
                    AddToDoItem.ChangeCanExecute();
            }
        }

        private IQueryable<ToDoItem> GetToDoItemsQuery(IQueryable<ToDoItem> toDoItemsBaseQuery, bool loadAll)
        {
            if (loadAll == false)
                toDoItemsBaseQuery = toDoItemsBaseQuery.Where(toDo => toDo.IsFinished == false);
            else
                toDoItemsBaseQuery = toDoItemsBaseQuery.OrderBy(toDo => toDo.IsFinished == true);

            return toDoItemsBaseQuery;
        }

        public MainViewModel()
        {
            dbContext = new ToDoDbContext();

            LoadToDoItems = new Command<bool>(async (loadAll) =>
            {
                try
                {
                    IsBusy = true;

                    await Task.Delay(1000);

                    await dbContext.Database.EnsureCreatedAsync();

                    await GetToDoItemsQuery(dbContext.ToDoItems, loadAll).LoadAsync();

                    ToDoItems = new ObservableCollection<ToDoItem>(GetToDoItemsQuery(dbContext.ToDoItems.Local.AsQueryable(), loadAll));
                }
                finally
                {
                    IsBusy = false;
                }
            });

            ToggleToDoItemIsFinished = new Command<ToDoItem>(async (toDoItem) =>
            {
                if (toDoItem == null)
                    return;

                try
                {
                    IsBusy = true;

                    await Task.Delay(1000);

                    toDoItem.IsFinished = !toDoItem.IsFinished;

                    await dbContext.SaveChangesAsync();
                }
                finally
                {
                    IsBusy = false;
                }
            });

            AddToDoItem = new Command(async () =>
            {
                try
                {
                    IsBusy = true;

                    await Task.Delay(1000);

                    ToDoItem toDoItem = new ToDoItem { IsFinished = false, Text = NewToDoText };

                    await dbContext.ToDoItems.AddAsync(toDoItem);

                    await dbContext.SaveChangesAsync();

                    ToDoItems.Add(toDoItem);

                    NewToDoText = "";
                }
                finally
                {
                    IsBusy = false;
                }
            }, () => !IsBusy && !string.IsNullOrEmpty(NewToDoText));

            DeleteTodoItem = new Command<ToDoItem>(async (toDoItem) =>
            {
                try
                {
                    IsBusy = true;
                    dbContext.Remove(toDoItem);
                    await dbContext.SaveChangesAsync();
                    ToDoItems.Remove(toDoItem);
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }
    }
}
