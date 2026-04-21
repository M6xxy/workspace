using StarterApp.ViewModels;
using StarterApp.Views;

namespace StarterApp;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel viewModel)
	{	
		BindingContext = viewModel;
		InitializeComponent();

        //Add pages to shell
        Routing.RegisterRoute(nameof(ItemsListPage), typeof(ItemsListPage));
    }
}
