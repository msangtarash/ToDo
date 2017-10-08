using Prism.Mvvm;

namespace ToDo.Model
{
    public class GroupToDoItem : BindableBase
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value); }
        }

        private string _GroupName;
        public string GroupName
        {
            get { return _GroupName; }
            set { SetProperty(ref _GroupName, value); }
        }

        //private ObservableCollection<ToDoItem> _ToDoItems;
        //public ObservableCollection<ToDoItem> ToDoItems
        //{
        //    get { return _ToDoItems; }
        //    set { Set(ref _ToDoItems, value); }
        //}
    }
}
