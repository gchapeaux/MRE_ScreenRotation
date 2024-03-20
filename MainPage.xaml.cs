namespace MRE_ScreenRotation
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            ModeBtn.Text = "Click me to query current orientation";
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("/ResponsivePage");
            //Navigation.PushAsync(new ResponsivePage());
        }
        private void ModeBtn_Clicked(object sender, EventArgs e)
        {
            ModeBtn.Text = DeviceDisplay.Current.MainDisplayInfo.Orientation.ToString();
        }
    }

}
