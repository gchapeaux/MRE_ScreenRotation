namespace MRE_ScreenRotation
{
    public partial class ResponsivePage : ContentPage
    {

        public ResponsivePage()
        {
            InitializeComponent();

            RespModeLabel.Text = (DeviceDisplay.Current.MainDisplayInfo.Orientation == DisplayOrientation.Portrait) ? "I'm in Portrait mode" : "I'm in Landscape Mode";

            DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;
        }

        int Count = 0;

        private void Current_MainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
        {
            Console.WriteLine("=*=*=*=*=*= MAIN DISPLAY INFO CHANGED =*=*=*=*=*=");
            Count++;
            EventCalledCounter.Text = "MainDisplayInfoChanged event called " + Count + " time(s).";
            RespModeLabel.Text = (e.DisplayInfo.Orientation == DisplayOrientation.Portrait) ? "I'm in Portrait mode" : "I'm in Landscape Mode";
        }

        object? SOToRestore {  get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
#if ANDROID
        SOToRestore = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.RequestedOrientation;
        Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.FullSensor;
#endif
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
#if ANDROID
        if (SOToRestore != null && SOToRestore is Android.Content.PM.ScreenOrientation SO) Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
#endif
        }

        private void ModeBtn_Clicked(object sender, EventArgs e)
        {
            ModeBtn.Text = DeviceDisplay.Current.MainDisplayInfo.Orientation.ToString();
        }
    }
}
