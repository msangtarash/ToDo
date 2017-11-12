using ToDo.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(HideEntryUnderlineEffect), nameof(HideEntryUnderlineEffect))]

namespace ToDo.Droid.Effects
{
    public class HideEntryUnderlineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            Control.Background = null;
        }

        protected override void OnDetached()
        {

        }
    }
}