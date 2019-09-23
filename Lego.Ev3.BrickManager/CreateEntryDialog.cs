using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System;
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
                            FileExplorer.GetValidateDirectoryName(name);
                            if (UserSettings.Mode == Mode.BASIC && _directory.Path == FileExplorer.PROJECTS_PATH)
                            {
                                isValid = !FileSystem.IsReservedDirectoryName(name);
                            }
                            else isValid = true;
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
