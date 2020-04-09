# How to access a named Expander inside a XAML DataTemplate in Xamarin.Forms (SfExpander)?

You can access the named [SfExpander](https://help.syncfusion.com/xamarin/expander/getting-started?) defined inside [DataTemplate](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/templates/data-templates/) of [PopupLayout](https://help.syncfusion.com/xamarin/popup/overview?) by using [Behavior](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/creating).

You can also refer the following article.

https://www.syncfusion.com/kb/11374/how-to-access-a-named-expander-inside-a-xaml-datatemplate-in-xamarin-forms-sfexpander

**XAML**

In PopupLayout’s [PopupView](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfPopupLayout.XForms~Syncfusion.XForms.PopupLayout.PopupView.html?), behaviour added to parent of Expander.

``` xml
<sfPopup:SfPopupLayout x:Name="popupLayout">
    <sfPopup:SfPopupLayout.Behaviors>
        <local:PopupBehavior/>
    </sfPopup:SfPopupLayout.Behaviors>
    <sfPopup:SfPopupLayout.PopupView>
        <sfPopup:PopupView WidthRequest="400" HeightRequest="400" ShowFooter="False">
            <sfPopup:PopupView.ContentTemplate>
                <DataTemplate>
                    <ScrollView BackgroundColor="#EDF2F5" Grid.Row="1">                                   
                            <StackLayout>
                            <StackLayout.Behaviors>
                                <local:Behavior/>
                            </StackLayout.Behaviors>
                            <Button Text="Expand/Collapse Second Expander" x:Name="expanderButton" BackgroundColor="LightGray"/>
                            <syncfusion:SfExpander x:Name="firstExpander">
                                    <syncfusion:SfExpander.Header>
                                        ...
                                    </syncfusion:SfExpander.Header>
                                    <syncfusion:SfExpander.Content>
                                        ...
                                    </syncfusion:SfExpander.Content>
                                </syncfusion:SfExpander>
 
                                <syncfusion:SfExpander x:Name="secondExpander">
                                    <syncfusion:SfExpander.Header>
                                        ...
                                    </syncfusion:SfExpander.Header>
                                    <syncfusion:SfExpander.Content>
                                        ...
                                    </syncfusion:SfExpander.Content>
                                </syncfusion:SfExpander>
                            </StackLayout>
                        </ScrollView>
                </DataTemplate>
            </sfPopup:PopupView.ContentTemplate>
        </sfPopup:PopupView>
    </sfPopup:SfPopupLayout.PopupView>
    <sfPopup:SfPopupLayout.Content>
        <Grid x:Name="popupGrid">
            <Grid.RowDefinitions>
                <RowDefinition Height="50"/>
                <RowDefinition Height="*"/>
            </Grid.RowDefinitions>
            <Button x:Name="ShowPopup" Text="Bring Popup" />
        </Grid>
    </sfPopup:SfPopupLayout.Content>
</sfPopup:SfPopupLayout>
```

**C#**

In ChildAdded event you will get the instance of Expander.

``` c#
public class Behavior : Behavior<StackLayout>
{
    StackLayout stackLayout;
    SfExpander expander;
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
    }
 
    protected override void OnDetachingFrom(StackLayout bindable)
    {
        stackLayout.ChildAdded -= StackLayout_ChildAdded;
        expander = null;
        stackLayout = null;
        base.OnDetachingFrom(bindable);
    }
}
```

**C#**

You can also get the Expander using [FindByName](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.element.findbyname?view=xamarin-forms) method from Parent element.

``` c#
public class Behavior : Behavior<StackLayout>
{
    StackLayout stackLayout;
    SfExpander expander;
    Button button;
    protected override void OnAttachedTo(StackLayout bindable)
    {
        stackLayout = bindable;
        stackLayout.ChildAdded += StackLayout_ChildAdded; ;
    }
    private void StackLayout_ChildAdded(object sender, ElementEventArgs e)
    {
        if (e.Element is Button)
        {
            button = e.Element as Button;
            button.Clicked += Button_Clicked;
        }
    }
 
    private void Button_Clicked(object sender, EventArgs e)
    {
        //Method 2 : Get SfExpander reference using FindByName
        expander = stackLayout.FindByName<SfExpander>("secondExpander");
        App.Current.MainPage.DisplayAlert("Information", "Second Expander instance is obtained and Expanded/Collapsed", "Ok");
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
```
