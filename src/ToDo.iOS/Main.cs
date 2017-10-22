using SQLitePCL;
using System;
using UIKit;

namespace ToDo.iOS
{
    public class Application
    {
        static Application()
        {
#if DEBUG
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif
        }

        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            SQLitePCL.raw.SetProvider(new SQLite3Provider_sqlite3());

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, nameof(AppDelegate));
        }

#if DEBUG
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

        }
#endif
    }
}
