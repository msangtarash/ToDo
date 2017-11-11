using Android.App;
using Android.Content.PM;
using Android.OS;
using Autofac;
using ImageCircle.Forms.Plugin.Droid;
using Prism.Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ToDo.Droid
{
    [Activity(Label = "ToDo", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            MaterialIcons.FormsPlugin.iOS.MaterialIconControls.Init();

            ImageCircleRenderer.Init();

            LoadApplication(new App(new ToDoInitializer()));
        }
    }

    public class ToDoInitializer : IPlatformInitializer
    {
        public void RegisterTypes(ContainerBuilder container)
        {

        }
    }
}

