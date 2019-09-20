using System;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class UserSettingsDialog : Form
    {
        public UserSettingsDialog()
        {
            InitializeComponent();
            comboBox.Items.Add(Mode.BASIC);
            comboBox.Items.Add(Mode.ADVANCED);
            comboBox.Items.Add(Mode.EXPERT);
            comboBox.SelectedIndex = (int) UserSettings.Mode;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Mode mode = (Mode)comboBox.SelectedItem;
            if (mode != UserSettings.Mode)
            {
                UserSettings.Mode = mode;
                UserSettings.Save();
                DialogResult = DialogResult.OK;
            }
            else DialogResult = DialogResult.Cancel;
        }
    }
}
