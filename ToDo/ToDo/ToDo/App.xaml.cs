using Prism.Unity;
using System.ComponentModel;
using ToDo.Views;
using Xamarin.Forms;



namespace ToDo
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("Nav/Main?title=Hello%20from%20Xamarin.Forms");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>("Nav");
            Container.RegisterTypeForNavigation<MainPage>("Main");
            
        }
    }
}
