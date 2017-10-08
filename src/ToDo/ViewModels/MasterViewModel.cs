using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using ToDo;
using ToDo.DataAccess;
using ToDo.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Prism.Mvvm;

namespace ToDo.ViewModels
{
    public class MasterViewModel : BindableBase
    {
        private readonly ToDoDbContext dbContext = new ToDoDbContext();
        public DelegateCommand LoadGroupToDoItems { get; set; }
        public DelegateCommand AddGroupToDoItem { get; set; }
        public DelegateCommand<GroupToDoItem> DeleteGroupTodoItem { get; set; }

        public DelegateCommand<GroupToDoItem> OpenDetailPage { get; set; }
        //new DelegateCommand OpenDetailPage => new DelegateCommand(Navigate);

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
            set { SetProperty(ref _IsBusy, value); }
        }

        private string _NewGroupToDoItem;
        public string NewGroupToDoItem
        {
            get { return _NewGroupToDoItem; }
            set { SetProperty(ref _NewGroupToDoItem, value); }
        }
        private string _ChangingTitle;
        public string ChangingTitle
        {
            get { return _ChangingTitle; }
            set { SetProperty(ref _ChangingTitle, value); }
        }

        public MasterViewModel()
        {
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

            //OpenDetailPage = new DelegateCommand<GroupToDoItem>((groupToDoItem) =>
            //{
            //    ToDo.Views.MainView mainView = new ToDo.Views.MainView
            //    {
            //        Title = groupToDoItem.GroupName
            //    };

            //    mainView.FindByName<Label>("XXXX").Text = groupToDoItem.GroupName;

            //    ((MasterDetailPage)App.Current.MainPage).Detail = mainView;
            //});


            DeleteGroupTodoItem = new DelegateCommand<GroupToDoItem>(async (grouptoDoItem) =>
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
