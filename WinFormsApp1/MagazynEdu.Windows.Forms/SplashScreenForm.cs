namespace MagazynEdu.Windows.Forms.MagazynEdu.Windows.Forms
{
    public partial class SplashScreenForm : Form
    {
        public SplashScreenForm()
        {
            InitializeComponent();
        }

        public void CloseSplashScreen()
        {
            this.Close();
        }

        public async Task SimulateLoadingOperationAsync()
        {
            // Symuluj operację ładowania trwającą 3 sekundy
            await Task.Delay(15000);
        }
    }
}