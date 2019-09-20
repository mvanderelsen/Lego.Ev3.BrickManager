using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Configuration;
using System;
using System.Reflection;
using System.Threading;
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
            BrickOptions options = new BrickOptions();
            options.DisablePowerUpSelfTest();
            Brick = new Brick(options);

            //load the user settings
            UserSettings.Initialize();
        }

        public async Task Connect()
        {
            //call connect but do not start an event monitor so pass false to internal connect
            MethodInfo method = typeof(Brick).GetMethod("Connect", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(bool) }, null);
            Task<bool> task = (Task<bool>)method.Invoke(Manager.Brick, new object[] { false });
            bool isConnected = await task.ConfigureAwait(false);

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
                using (ConnectDialog connectDialog = new ConnectDialog())
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
    }
}
