namespace Browser;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        MainViewModel viewModel = new();
        BindingContext = viewModel;
        InitializeComponent();
    }
}