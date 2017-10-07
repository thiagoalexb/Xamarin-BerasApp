using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace BerasApp.Behaviors
{
    public class ListViewBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: "Command",
                returnType: typeof(ICommand),
                declaringType: typeof(ListViewBehavior));


        private int maxRange = 0;

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemAppearing += Bindable_ItemAppearing;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            var lv = sender as ListView;
            BindingContext = lv?.BindingContext;
        }

        private void Bindable_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var lv = sender as ListView;
            var items = lv.ItemsSource as IList;
            if (items != null && e.Item == items[items.Count - 1])
            {
                Command.Execute(null);
                maxRange = items.Count;
            }
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemAppearing -= Bindable_ItemAppearing;
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
        }
    }
}
