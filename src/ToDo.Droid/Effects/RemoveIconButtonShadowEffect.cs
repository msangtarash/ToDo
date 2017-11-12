using Android.Animation;
using ToDo.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(RemoveIconButtonShadowEffect), nameof(RemoveIconButtonShadowEffect))]

namespace ToDo.Droid.Effects
{
    public class RemoveIconButtonShadowEffect : PlatformEffect
    {
        private StateListAnimator _originalStateListAnimator = null;

        protected override void OnAttached()
        {
            _originalStateListAnimator = Control.StateListAnimator;
            Control.StateListAnimator = null;
        }

        protected override void OnDetached()
        {
            Control.StateListAnimator = _originalStateListAnimator;
        }
    }
}