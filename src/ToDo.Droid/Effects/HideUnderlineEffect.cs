using Android.Graphics.Drawables;
using ToDo.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(HideUnderlineEffect), nameof(HideUnderlineEffect))]

namespace ToDo.Droid.Effects
{
    public class HideUnderlineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Element is SearchBar)
            {
                int plateId = Control.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
                Android.Views.View plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
            else if (Element is Entry)
            {
                Control.Background = null;
            }
        }

        protected override void OnDetached()
        {

        }
    }
}