using Android.Content;
using ToDo.Droid.Renderes;
using ToDo.Views.Properties;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(ToDoEntryRenderer))]

namespace ToDo.Droid.Renderes
{
    public class ToDoEntryRenderer : EntryRenderer
    {
        public ToDoEntryRenderer(Context context)
            : base(context)
        {

        }

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