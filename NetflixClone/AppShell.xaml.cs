
using NetflixClone.Pages;

namespace NetflixClone;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(DanhMuc),typeof(DanhMuc));
    }
}
