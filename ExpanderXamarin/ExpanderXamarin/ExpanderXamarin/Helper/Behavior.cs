using Syncfusion.XForms.Expander;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExpanderXamarin
{
    public class Behavior : Behavior<StackLayout>
    {
        StackLayout stackLayout;
        SfExpander expander;
        Button button;
        bool flag;
        protected override void OnAttachedTo(StackLayout bindable)
        {
            stackLayout = bindable;
            stackLayout.ChildAdded += StackLayout_ChildAdded; ;
        }
        private void StackLayout_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is SfExpander)
            {
                //Method 1 : Get SfExpander reference using StackLayout.ChildAdded Event
                expander = e.Element as SfExpander;
            }
            else if (e.Element is Button)
            {
                button = e.Element as Button;
                button.Clicked += Button_Clicked;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Method 2 : Get SfExpander reference using FindByName
            expander = stackLayout.FindByName<SfExpander>("secondExpander");
            if(!flag)
            {
                App.Current.MainPage.DisplayAlert("Information", "Second Expander instance is obtained and Expanded/Collapsed", "Ok");
                flag = true;
            }
            if (expander.IsExpanded)
            {
                expander.IsExpanded = false;
            }
            else
            {
                expander.IsExpanded = true;
            }
        }

        protected override void OnDetachingFrom(StackLayout bindable)
        {
            button.Clicked -= Button_Clicked;
            stackLayout.ChildAdded -= StackLayout_ChildAdded;
            expander = null;
            button = null;
            stackLayout = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
