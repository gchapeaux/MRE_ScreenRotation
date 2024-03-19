namespace MRE_ScreenRotation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ResponsivePage), typeof(ResponsivePage));
        }
    }
}
