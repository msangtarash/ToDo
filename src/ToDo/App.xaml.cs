using Autofac;
using Prism.Autofac;
using ToDo.DataAccess;
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

            NavigationService.NavigateAsync("ToDoGroups/Nav/ToDoItems");
        }

        protected override void RegisterTypes()
        {
            Builder.RegisterTypeForNavigation<NavigationPage>("Nav");
            Builder.RegisterTypeForNavigation<ToDoGroupsView, ToDoGroupsViewModel>("ToDoGroups");
            Builder.RegisterTypeForNavigation<ToDoItemsView, ToDoItemsViewModel>("ToDoItems");
            Builder.RegisterTypeForNavigation<ToDoItemDetailView, ToDoItemDetailViewModel>("ToDoItemDetail");
            Builder.RegisterTypeForNavigation<SearchView, SearchViewModel>("Search");
            Builder.RegisterType<ToDoDbContext>();
        }
    }
}
