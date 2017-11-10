using Android.App;
using Android.Content.PM;
using Android.OS;
using Bit.ApkUpdateAgent;

namespace ToDo.Droid
{
    [Activity(Label = "ToDo", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : BitFormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);
        }
    }
}

