using FormsPlugin.Iconize;
using FormsPlugin.Iconize.Droid;
using ToDo.Views;
using ToDo.Droid.Renderes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ToDoIconButton), typeof(ToDoIconButtonRenderer))]

namespace ToDo.Droid.Renderes
{
    public class ToDoIconButtonRenderer : IconButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.StateListAnimator = null;
            }
        }
    }
}