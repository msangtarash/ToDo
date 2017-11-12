using System.Linq;
using Xamarin.Forms;

namespace ToDo.Effects
{
    public class HideEntryUnderlineEffect : RoutingEffect
    {
        public HideEntryUnderlineEffect()
            : base($"ToDo.{nameof(HideEntryUnderlineEffect)}")
        {
        }

        public static readonly BindableProperty HideUnderlineProperty = BindableProperty.CreateAttached(
                propertyName: "HideUnderline",
                returnType: typeof(bool?),
                declaringType: typeof(Entry),
                defaultValue: null,
                propertyChanged: OnHideUnderlineChanged);

        public static bool? GetHideUnderline(BindableObject view)
        {
            return (bool?)view.GetValue(HideUnderlineProperty);
        }

        public static void OnHideUnderlineChanged(BindableObject view, object oldValue, object newValue)
        {
            view.SetValue(HideUnderlineProperty, newValue);

            Entry entry = (Entry)view;

            if (((bool)newValue) == true)
                entry.Effects.Add(new HideEntryUnderlineEffect());
            else
            {
                HideEntryUnderlineEffect effect = entry.Effects.OfType<HideEntryUnderlineEffect>().FirstOrDefault();

                if (effect != null)
                    entry.Effects.Remove(effect);
            }
        }
    }
}
