using FormsPlugin.Iconize;
using System.Linq;
using Xamarin.Forms;

namespace ToDo.Effects
{
    public class RemoveIconButtonShadowEffect : RoutingEffect
    {
        public RemoveIconButtonShadowEffect()
            : base($"ToDo.{nameof(RemoveIconButtonShadowEffect)}")
        {
        }

        public static readonly BindableProperty RemoveIconButtonShadowProperty = BindableProperty.CreateAttached(
                propertyName: "RemoveIconButtonShadow",
                returnType: typeof(bool?),
                declaringType: typeof(IconButton),
                defaultValue: null,
                propertyChanged: OnRemoveIconButtonShadowChanged);

        public static bool? GetRemoveIconButtonShadow(BindableObject view)
        {
            return (bool?)view.GetValue(RemoveIconButtonShadowProperty);
        }

        public static void OnRemoveIconButtonShadowChanged(BindableObject view, object oldValue, object newValue)
        {
            view.SetValue(RemoveIconButtonShadowProperty, newValue);

            IconButton iconButton = (IconButton)view;

            if (((bool)newValue) == true)
                iconButton.Effects.Add(new RemoveIconButtonShadowEffect());
        }
    }
}
