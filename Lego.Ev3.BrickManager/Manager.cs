using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Configuration;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class Manager : Form
    {
        public static Brick Brick { get; private set; }

        private delegate Task InvokeHandler();

        public Manager()
        {
            InitializeComponent();

            //init the brick
            BrickOptions options = new();
            options.DisablePowerUpSelfTest();
            options.DisableEventMonitor();
            Brick = new Brick(options);

            //load the user settings
            UserSettings.Initialize();
        }

        public async Task Connect()
        {
            bool isConnected = await Brick.Connect();

            if (isConnected)
            {
                if (InvokeRequired)
                {
                    Invoke(new InvokeHandler(Initialize));
                }
                else await Initialize();

            }
            else
            {
                //show dialog and reconnect on OK
                using (ConnectDialog connectDialog = new())
                {
                    if (connectDialog.ShowDialog() == DialogResult.OK) await Connect();
                    else Close();
                }
            }
        }

        private async Task Initialize()
        {
            await explorerWindow.Initialize();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            explorerWindow.InitView();
        }

        public async Task Disconnect()
        {
            await Brick.Disconnect().ConfigureAwait(false);
        }

        private async void Manager_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            await Connect();
        }

        private async void Manager_FormClosing(object sender, FormClosingEventArgs e)
        {
            await Disconnect();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (UserSettingsDialog dialog = new UserSettingsDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    UseWaitCursor = true;
                    await explorerWindow.Initialize();
                    UseWaitCursor = false;
                }
            }
        }

        private async void DeviceManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DeviceManager deviceManager = new DeviceManager())
            {
                Task task = deviceManager.Initialize();
                deviceManager.ShowDialog();
                await task;
            }
        }
    }
}
