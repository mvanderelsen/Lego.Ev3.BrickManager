using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class TextFileEditor : Form
    {
        private const char LINE = '\n';
        public TextFile TextFile { get; private set; }

        private string originalFileName;

        private bool TextHasChanged = false;

        public bool RefreshDirectory = false;

        public Directory Directory { get; set; }
        public TextFileEditor()
        {
            InitializeComponent();
        }


        public async Task<bool> Save()
        {
            errorProvider.Clear();
            if (string.IsNullOrWhiteSpace(textBoxFileName.Text))
            {
                errorProvider.SetError(textBoxFileName, "Filename is required");
                return false;
            }

            string fileName = System.IO.Path.GetFileNameWithoutExtension(textBoxFileName.Text.Trim());
            bool isNewName = !fileName.Equals(originalFileName, StringComparison.InvariantCultureIgnoreCase);

            if (!TextHasChanged && !isNewName) return true;

            //textbox returns lines as /n .. weird should ne /r/n
            //so hack work around
            string[] lines = richTextBox.Lines;

            string text = "-";
            if (lines?.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                int lineCount = lines.Length;
                for (int i = 0; i < lineCount; i++)
                {
                    if (i + 1 < lineCount) sb.AppendLine(lines[i]);
                    else sb.Append(lines[i]);
                }
                text = sb.ToString();
            }

            byte[] data = FileConverter.TexttoRTF(text);

            

            if (TextFile != null)
            {
                await BrickExplorer.UploadFile(data, Directory.Path, $"{fileName}.rtf");
                if (isNewName)
                {
                    await TextFile.Delete();
                }
            }
            else
            {
                await BrickExplorer.UploadFile(data, Directory.Path, $"{fileName}.rtf");
            }

            TextHasChanged = false;
            RefreshDirectory = true;
            return true;
        }


        private async void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Save();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public async Task Initialize(TextFile textFile = null) 
        {
            TextFile = textFile;
            if (TextFile != null)
            {
                originalFileName = System.IO.Path.GetFileNameWithoutExtension(TextFile.FileName);
                Text = TextFile.FileName;
                textBoxFileName.Text = originalFileName;
                richTextBox.Text = await TextFile.DownloadAsString();
                SetStatus();
                richTextBox.Focus();
            }
            else textBoxFileName.Focus();

            richTextBox.TextChanged += RichTextBox_TextChanged;
        }

        private async void TextFileEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TextHasChanged)
            {
                e.Cancel = true;
                bool saved = true;

                if (MessageBox.Show("The file has changed. Do you want to save changes?", "File save dialog", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    saved = await Save();
                }

                if (saved)
                {
                    TextHasChanged = false;
                    e.Cancel = false;
                    Close();
                }
            }
        }


       

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            TextHasChanged = true;
            SetStatus();
        }

        private void SetStatus()
        {
            string text = richTextBox.Text;
            int lines = text.Split(LINE).Length;
            text = text.Replace($"{LINE}", "");
            int chars = text.ToCharArray().Length;
            toolStripStatusLabel.Text = string.Format("{0} lines {1} chars", lines, chars);

        }
    }
}
