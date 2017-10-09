using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ToDo.DataAccess;
using ToDo.Model;
using Xamarin.Forms;

namespace ToDo.ViewModels
{
    public class MasterViewModel : BindableBase
    {
        private readonly ToDoDbContext dbContext;

        public DelegateCommand LoadGroupToDoItems { get; set; }

        public DelegateCommand AddGroupToDoItem { get; set; }
         
        public DelegateCommand<GroupToDoItem> DeleteGroupTodoItem { get; set; }
        public DelegateCommand<GroupToDoItem> OpenDetailPage { get; set; }

        private ObservableCollection<GroupToDoItem> _GroupToDoItems;
        public ObservableCollection<GroupToDoItem> GroupToDoItems
        {
            get { return _GroupToDoItems; }
            set { SetProperty(ref _GroupToDoItems, value); }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                SetProperty(ref _IsBusy, value);
                //if (SetProperty(ref _IsBusy, value))
                //    AddGroupToDoItem.ChangeCanExecute();
            }
        }

        private string _NewGroupToDoItem;
        public string NewGroupToDoItem
        {
            get { return _NewGroupToDoItem; }
            set
            {
                SetProperty(ref _NewGroupToDoItem, value);
                //    AddGroupToDoItem.ChangeCanExecute();
            }
        }

        private string _ChangingTitle;
        public string ChangingTitle
        {
            get { return _ChangingTitle; }
            set { SetProperty(ref _ChangingTitle, value); }
        }

        public MasterViewModel()
        {
            dbContext = new ToDoDbContext();

            LoadGroupToDoItems = new DelegateCommand(async () =>
            {
                try
                {
                    IsBusy = true;

                    await Task.Delay(1000);

                    await dbContext.Database.EnsureCreatedAsync();

                    GroupToDoItems = new ObservableCollection<GroupToDoItem>(await dbContext.GroupToDoItems.ToListAsync());
                }
                finally
                {
                    IsBusy = false;
                }
            });


            AddGroupToDoItem = new DelegateCommand(async () =>
            {
                try
                {
                    IsBusy = true;

                    await Task.Delay(1000);

                    GroupToDoItem groupTodo = new GroupToDoItem { GroupName = NewGroupToDoItem };

                    await dbContext.GroupToDoItems.AddAsync(groupTodo);

                    await dbContext.SaveChangesAsync();

                    GroupToDoItems.Add(groupTodo);

                    NewGroupToDoItem = "";

                }
                finally
                {
                    IsBusy = false;
                }
            });



            DeleteGroupTodoItem = new DelegateCommand<GroupToDoItem>(async grouptoDoItem =>
            {
                try
                {
                    IsBusy = true;
                    dbContext.Entry(grouptoDoItem).State = EntityState.Deleted;
                    await dbContext.SaveChangesAsync();
                    GroupToDoItems.Remove(grouptoDoItem);
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }
    }
}
