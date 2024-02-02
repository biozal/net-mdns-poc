using CommunityToolkit.Mvvm.ComponentModel;

namespace Advertiser;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        MainViewModel viewModel = new ();
        BindingContext = viewModel; 
        
        InitializeComponent();
    }
}