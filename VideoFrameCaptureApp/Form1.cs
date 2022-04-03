using System.Diagnostics;
using VideoFrameCaptureLibrary;

namespace VideoFrameCaptureApp
{
    public partial class Form1 : Form
    {
        private VideoFrameReader frameReader;
        private bool hasBeenInitialized;
        
        // Capture 10 frames per second.
        // Note: The lowest cost tier of Azure Cognitive Services facial recognition only
        // supports up to 10 calls per second
        private static readonly uint FPS = 10;

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
            frameReader = new VideoFrameReader(FPS);
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

            await frameReader.StartCapture();

            if (btnSender != null)
            {
                btnSender.Enabled = true;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await frameReader.StopCapture();
        }
    }
}