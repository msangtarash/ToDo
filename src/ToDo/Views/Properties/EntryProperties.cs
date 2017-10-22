using Xamarin.Forms;

namespace ToDo.Views.Properties
{
    public static class EntryProperties
    {
        public static readonly BindableProperty HideUnderlineProperty =
            BindableProperty.CreateAttached(
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
        }
    }
}