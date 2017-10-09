using Prism.Mvvm;

namespace ToDo.Model
{
    public class ToDoItem : BindableBase
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value); }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set { SetProperty(ref _Text, value); }
        }

        private bool _IsFinished;
        public bool IsFinished
        {
            get { return _IsFinished; }
            set { SetProperty(ref _IsFinished, value); }
        }

        private int _GroupId;
        public int GroupId
        {
            get { return _GroupId; }
            set { SetProperty(ref _GroupId, value); }
        }
        private GroupToDoItem _GroupName;
        public GroupToDoItem GroupName
        {
            get { return _GroupName; }
            set { SetProperty(ref _GroupName, value); }
        }
    }
}
