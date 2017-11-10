using Android.OS;
using Bit.ApkUpdateAgent;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ToDo.Droid.Codes
{
    public class ActivityConfigurationManager : Bit.ApkUpdateAgent.ActivityConfigurationManager
    {
        public override void OnAppActivityCreation(BitFormsAppCompatActivity activity, Bundle bundle)
        {
            base.OnAppActivityCreation(activity, bundle);

            Forms.Init(activity, bundle);

            MaterialIcons.FormsPlugin.iOS.MaterialIconControls.Init();

            activity.LoadApplication(new App(new ToDoInitializer()));
        }
    }
}