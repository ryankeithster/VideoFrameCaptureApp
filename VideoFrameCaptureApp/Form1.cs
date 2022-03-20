using System.Diagnostics;
using VideoFrameCaptureLibrary;

namespace VideoFrameCaptureApp
{
    public partial class Form1 : Form
    {
        private VideoFrameReader frameReader;
        private bool hasBeenInitialized;

        public static async Task<Form1> BuildFormAsync()
        {
            Form1 form = new Form1();
            await form.frameReader.InitializeAsync();
            return form;
        }

        public Task IntializeAsync()
        {
            return frameReader.InitializeAsync();
        }

        public Form1()
        {
            InitializeComponent();
            frameReader = new VideoFrameReader();
            hasBeenInitialized = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Button? btnSender = sender as Button;
            if (btnSender != null)
            {
                btnSender.Enabled = false;
            }

            if (!hasBeenInitialized)
            {
                await frameReader.InitializeAsync();
                Debug.WriteLine("Initialization finished");

                hasBeenInitialized = true;
            }
            Debug.WriteLine("Beginning capture");

            frameReader.StartCapture();

            if (btnSender != null)
            {
                btnSender.Enabled = true;
            }
        }
    }
}