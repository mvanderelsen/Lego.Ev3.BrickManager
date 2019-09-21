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
            comboBox.SelectedIndex = (int) UserSettings.Mode;
            labelDisclaimer.Text = "Use with great care!\nDeleting or renaming firmware files lead\nto brick malfunction.";
            labelDisclaimer.Visible = UserSettings.Mode == Mode.ADVANCED;
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

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mode mode = (Mode)comboBox.SelectedItem;
            labelDisclaimer.Visible = mode == Mode.ADVANCED;
        }
    }
}
