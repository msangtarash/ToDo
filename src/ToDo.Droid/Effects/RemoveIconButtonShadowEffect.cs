using ToDo.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(RemoveIconButtonShadowEffect), nameof(RemoveIconButtonShadowEffect))]

namespace ToDo.Droid.Effects
{
    public class RemoveIconButtonShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            Control.StateListAnimator = null;
        }

        protected override void OnDetached()
        {
            
        }
    }
}