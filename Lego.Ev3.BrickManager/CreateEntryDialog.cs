using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class CreateEntryDialog : Form
    {
        public string EntryName { get { return textBoxName.Text.Trim(); } }

        private readonly EntryType _entryType;

        private readonly FileType _fileType;

        private readonly Directory _directory;

        public CreateEntryDialog(Directory current, EntryType type, FileType? fileType)
        {
            InitializeComponent();
            _entryType = type;
            _directory = current;
            if (fileType.HasValue)
            {
                _fileType = fileType.Value;
                Text = "Create file";
            }
            textBoxName.Select();
            textBoxName.Validating += TextBoxName_Validating;
        }

        private void TextBoxName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool isValid = false;
            switch (_entryType)
            {
                case EntryType.DIRECTORY:
                    {
                        
                        try
                        {
                            string name = textBoxName.Text.Trim();
                            if (UserSettings.Mode == Mode.BASIC && _directory.Path == BrickExplorer.PROJECTS_PATH)
                            {
                                isValid = !FileSystem.IsReservedDirectoryName(name);
                            }
                            else isValid = Regex.IsMatch(name, "[ -~]");
                        }
                        catch
                        {
                        }
                        break;
                    }
            }

            e.Cancel = !isValid;
        }
    }
}
