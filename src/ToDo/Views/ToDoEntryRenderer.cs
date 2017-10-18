using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(ToDoEntryRenderer))]

namespace ToDo.Views
{
     public class ToDoEntryRenderer :EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = null;
            }
        }

    }
}
