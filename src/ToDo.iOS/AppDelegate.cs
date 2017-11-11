using Autofac;
using FormsPlugin.Iconize.iOS;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Plugin.Iconize;
using Plugin.Iconize.Fonts;
using Prism.Autofac;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ToDo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            SQLitePCL.Batteries.Init();

            Forms.Init();

            IconControls.Init();

            Iconize.With(new FontAwesomeModule());

            ImageCircleRenderer.Init();

            LoadApplication(new App(new ToDoInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class ToDoInitializer : IPlatformInitializer
    {
        public void RegisterTypes(ContainerBuilder container)
        {
            
        }
    }
}
