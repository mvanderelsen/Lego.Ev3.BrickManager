using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class NavigationBar : UserControl
    {
        public event OnDirectoryAction DirectoryAction;

        private ToolStripButton _home;
        public NavigationBar()
        {
            InitializeComponent();
            _home = new ToolStripButton
            {
                Image = new Icon(Properties.Resources.Brick, new Size(16, 16)).ToBitmap(),
                Tag = FileExplorer.ROOT_PATH,
            };
            _home.Click += ToolStripButtonClicked;
        }

        public void Initialize()
        {
            bar.Items.Clear();
            bar.Items.Add(_home);
            panel.Visible = (UserSettings.Mode != Mode.BASIC);
        }

        private async void ToolStripButtonClicked(object sender, EventArgs e)
        {
            ToolStripButton button = (ToolStripButton)sender;
            string path = button.Tag.ToString();
            Directory directory = await FileExplorer.GetDirectory(path);
            DirectoryAction?.Invoke(this, directory, ActionType.OPEN);
        }

        public void Set(Directory directory)
        {
            bar.Items.Clear();
            bar.Items.Add(_home);
            string[] directories = directory.Path.Split('/');
            StringBuilder sb = new StringBuilder();
            foreach (string name in directories)
            {
                if (string.IsNullOrEmpty(name)) continue;
                sb.Append(name);
                sb.Append("/");
                if (name == "..") continue;

                ToolStripButton button = new ToolStripButton();
                button.Image = Properties.Resources.Arrow;
                button.ImageAlign = ContentAlignment.MiddleLeft;
                button.Text = name;
                button.Tag = sb.ToString();
                button.Click += ToolStripButtonClicked;
                bar.Items.Add(button);
            }
        }
    }
}
