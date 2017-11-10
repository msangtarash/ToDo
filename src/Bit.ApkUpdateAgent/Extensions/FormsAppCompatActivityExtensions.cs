using Bit.ApkUpdateAgent;
using System;
using System.Reflection;

namespace Xamarin.Forms.Platform.Android
{
    public static class FormsAppCompatActivityExtensions
    {
        public static void LoadApplication(this BitFormsAppCompatActivity activity, Application app)
        {
            if (activity == null)
                throw new ArgumentNullException(nameof(activity));

            if (app == null)
                throw new ArgumentNullException(nameof(app));

            activity.GetType().GetTypeInfo().GetMethod("LoadApplication", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(activity, new object[] { app });
        }
    }
}