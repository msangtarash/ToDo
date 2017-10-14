using ToDo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(ToDoEntryRendere))]

namespace ToDo.Views
{
    /// <summary>
    ///to write a behavior 
    /// /// </summary>
    public class MagicText : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);

            entry.TextChanged += Entry_TextChanged;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;

            if (entry.Text?.Length % 3 == 0)
                entry.Text += "!";
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= Entry_TextChanged;
            base.OnDetachingFrom(entry);
        }
    }

    /// <summary>
    /// to write a custom renderer
    /// </summary>
    public class ToDoEntryRendere : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (EntryManager.GetLessUnderLineEntry(e.NewElement))
                    Control.Background = null;
            }
        }
    }

    /// <summary>
    /// to write an attach peroperty
    /// </summary>
    public static class EntryManager
    {
        public static readonly BindableProperty LessUnderLineEntryProperty =
            BindableProperty.CreateAttached(
                "LessUnderLineEntry",
                typeof(bool),
                typeof(Entry),
                false,
               propertyChanged: OnLessUnderLineEntry);

        public static bool GetLessUnderLineEntry(BindableObject view)
        {
            return (bool)view.GetValue(LessUnderLineEntryProperty);
        }

        public static void OnLessUnderLineEntry(BindableObject view, object oldValue, object newValue)
        {
            view.SetValue(LessUnderLineEntryProperty, newValue);
        }
    }
}