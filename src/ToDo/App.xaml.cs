using Prism.Autofac;
using Prism.Autofac.Forms;
using ToDo.ViewModels;
using ToDo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ToDo
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("Nav/ToDoGroups");
        }
        
        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>("Nav");
            Container.RegisterTypeForNavigation<ToDoListView, ToDoListViewModel>("ToDoList");
            Container.RegisterTypeForNavigation<ToDoGroupsView, ToDoGroupsViewModel>("ToDoGroups");
        }
    }
}
