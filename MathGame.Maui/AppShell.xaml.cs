using MathGame.Maui.Views;

namespace MathGame.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("Menu", typeof(Menu));
        Routing.RegisterRoute("Game", typeof(Game));
    }
}
