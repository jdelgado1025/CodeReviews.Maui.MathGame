using MathGame.Maui.Views;

namespace MathGame.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Menu), typeof(Menu));
        Routing.RegisterRoute(nameof(Game), typeof(Game));
    }
}
