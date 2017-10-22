using ToDo.Droid.Renderes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ToDo.Views.Properties;

[assembly: ExportRenderer(typeof(Entry), typeof(ToDoEntryRendere))]

namespace ToDo.Droid.Renderes
{
    public class ToDoEntryRendere : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (EntryProperties.GetHideUnderline(e.NewElement) != false)
                    Control.Background = null;
            }
        }
    }
}