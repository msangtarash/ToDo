using Android.Graphics.Drawables;
using ToDo.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(HideEntryUnderlineEffect), nameof(HideEntryUnderlineEffect))]

namespace ToDo.Droid.Effects
{
    public class HideEntryUnderlineEffect : PlatformEffect
    {
        private Drawable _originalBackground;

        protected override void OnAttached()
        {
            _originalBackground = Control.Background;
            Control.Background = null;
        }

        protected override void OnDetached()
        {
            Control.Background = _originalBackground;
        }
    }
}