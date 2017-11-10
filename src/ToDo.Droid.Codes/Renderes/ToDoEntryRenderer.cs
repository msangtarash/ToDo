using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ToDo.Views.Properties;
using Android.Content;
using ToDo.Droid.Codes.Renderes;

[assembly: ExportRenderer(typeof(Entry), typeof(ToDoEntryRenderer))]

namespace ToDo.Droid.Codes.Renderes
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