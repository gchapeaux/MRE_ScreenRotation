namespace MRE_ScreenRotation
{
    public partial class ResponsivePage : ContentPage
    {

        bool UseUserRotation = false;

        public ResponsivePage()
        {
            InitializeComponent();

            MDIOLabel.Text = "MainDisplayInfo.Orientation : " + DeviceDisplay.Current.MainDisplayInfo.Orientation.ToString();

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
            Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.FullUser;
            UseUserRotation = true;
            ROLabel.Text = (UseUserRotation) ? "RequestedOrientation : User" : "RequestedOrientation : Portrait";
            Console.WriteLine(DeviceDisplay.Current.MainDisplayInfo.Orientation.ToString());
#endif
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
#if ANDROID
        if (SOToRestore != null && SOToRestore is Android.Content.PM.ScreenOrientation SO) Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
        UseUserRotation = false;
#endif
        }

        private void ModeBtn_Clicked(object sender, EventArgs e)
        {
            MDIOLabel.Text = "MainDisplayInfo.Orientation : " + DeviceDisplay.Current.MainDisplayInfo.Orientation.ToString();
        }

        private void SwitchModeBtn_Clicked(object sender, EventArgs e)
        {
#if ANDROID
            Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.RequestedOrientation = (UseUserRotation) ? Android.Content.PM.ScreenOrientation.Portrait : Android.Content.PM.ScreenOrientation.FullUser;
            UseUserRotation = !UseUserRotation;
            ROLabel.Text = (UseUserRotation) ? "Current RequestedOrientation : FullUser" : "Current RequestedOrientation : Portrait";
#endif
        }
    }
}
