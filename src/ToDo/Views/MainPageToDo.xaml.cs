using ToDo.ViewModels;
using Xamarin.Forms;

namespace ToDo.Views
{
    public partial class MainPageToDo : MasterDetailPage
    {
        public MainPageToDo()
        {
            InitializeComponent();

            Master = new MainPageToDoMaster { BindingContext = new MasterViewModel() };

            Detail = new NavigationPage(new MainPageToDoDetail { BindingContext = new MainViewModel() });
        }
    }
}