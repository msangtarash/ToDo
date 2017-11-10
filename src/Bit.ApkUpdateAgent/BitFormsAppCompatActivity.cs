using Android.Content.PM;
using Android.OS;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms.Platform.Android;

namespace Bit.ApkUpdateAgent
{
    public class BitFormsAppCompatActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Assembly currentAssembly = GetType().GetTypeInfo().Assembly;

            string currentAssemblyName = currentAssembly.GetName().Name;

            TypeInfo activityConfigurationManagerType = Type.GetType($"{currentAssemblyName}.Codes.ActivityConfigurationManager, {currentAssemblyName}.Codes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
                ?.GetTypeInfo();

            if (activityConfigurationManagerType == null)
                throw new InvalidOperationException($"{nameof(activityConfigurationManagerType)} could not be found");

            ActivityConfigurationManager activityConfigurationManager = (ActivityConfigurationManager)Activator.CreateInstance(activityConfigurationManagerType);

            activityConfigurationManager.OnAppActivityCreation(this, bundle);
        }

        protected override void Dispose(bool disposing)
        {
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;

            base.Dispose(disposing);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly currentAssembly = GetType().GetTypeInfo().Assembly;

            AssemblyName newAssemblyName = new AssemblyName(args.Name);

            string newAssemblyResourceName = currentAssembly
                .GetManifestResourceNames()
                .SingleOrDefault(res => res == $"{currentAssembly.GetName().Name}.Lib.{newAssemblyName.Name}.dll");

            if (newAssemblyResourceName != null)
            {
                /*string newAssemblyFilePath = Path.Combine(CacheDir.AbsolutePath, $"{newAssemblyName.Name}.dll");

                PackageInfo packageInfo = PackageManager.GetPackageInfo(PackageName, 0);
                int packageVersionCode = packageInfo.VersionCode;

                if (File.Exists(newAssemblyFilePath))
                    return Assembly.Load(File.ReadAllBytes(newAssemblyFilePath));
                else
                {*/
                using (Stream newAssemblyStreamFromApk = currentAssembly.GetManifestResourceStream(newAssemblyResourceName))
                {
                    byte[] buffer = new byte[16 * 1024];

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        int read;

                        while ((read = newAssemblyStreamFromApk.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            memoryStream.Write(buffer, 0, read);
                        }

                        return Assembly.Load(memoryStream.ToArray());
                    }
                }
                /*}*/
            }

            return null;
        }
    }
}