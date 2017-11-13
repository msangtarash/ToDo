using System.Linq;
using Xamarin.Forms;

namespace ToDo.Effects
{
    public class HideUnderlineEffect : RoutingEffect
    {
        public HideUnderlineEffect()
            : base($"ToDo.{nameof(HideUnderlineEffect)}")
        {
        }

        public static readonly BindableProperty HideUnderlineProperty = BindableProperty.CreateAttached(
                propertyName: "HideUnderline",
                returnType: typeof(bool?),
                declaringType: typeof(Element),
                defaultValue: null,
                propertyChanged: OnHideUnderlineChanged);

        public static bool? GetHideUnderline(BindableObject view)
        {
            return (bool?)view.GetValue(HideUnderlineProperty);
        }

        public static void OnHideUnderlineChanged(BindableObject view, object oldValue, object newValue)
        {
            view.SetValue(HideUnderlineProperty, newValue);

            Element element = (Element)view;

            if (((bool)newValue) == true)
                element.Effects.Add(new HideUnderlineEffect());
            else
            {
                HideUnderlineEffect effect = element.Effects.OfType<HideUnderlineEffect>().FirstOrDefault();

                if (effect != null)
                    element.Effects.Remove(effect);
            }
        }
    }
}
