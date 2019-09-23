using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class ExplorerWindow : UserControl
    {

        public Directory CURRENT_DIRECTORY;
        public File CURRENT_FILE;

        public Directory SELECTED_DIRECTORY;
        public File SELECTED_FILE;

        private string LocalMachinePath;

        public ExplorerWindow()
        {
            InitializeComponent();
            LocalMachinePath = (string.IsNullOrWhiteSpace(Properties.Settings.Default.LocalMachinePath)) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : Properties.Settings.Default.LocalMachinePath;
            folderTree.DirectoryAction += Execute;
            directoryContentPane.DirectoryAction += Execute;
            navigationBar.DirectoryAction += Execute;
            directoryContentPane.FileAction += Execute;

            openFileDialog.Filter = "Robot Files (*.rgf, *.rbf, *.rsf, *.rdf, *.rtf, *.rpf, *.rcf, *.raf)|*.rgf;*.rbf;*.rsf;*.rdf;*.rtf;*.rpf;*.rcf;*.raf";
            openFileDialog.Multiselect = true;
            openFileDialog.FileName = "";
        }

        #region initialize
        public async Task Initialize()
        {
            Directory root = await FileExplorer.GetDirectory(FileExplorer.ROOT_PATH);
            CURRENT_DIRECTORY = root;
            SELECTED_DIRECTORY = root;
            await folderTree.Initialize(root);
            await previewPane.Initialize(root);
            navigationBar.Initialize();
            DirectoryContent content = await DirectoryContent.Get(root);
            directoryContentPane.Set(content);
            statusBar.Set(content.Info);

            if (UserSettings.Mode == Mode.BASIC)
            {
                Directory drive = await FileExplorer.GetDirectory(FileExplorer.PROJECTS_PATH);
                Execute(this, drive, ActionType.OPEN);
            }
        }

        public void InitView()
        {
            folderTree.InitView();
        }
        #endregion


        private async void Execute(object sender, Directory directory, ActionType type)
        {
            switch (type)
            {
                case ActionType.OPEN:
                    {
                        if (CURRENT_DIRECTORY.Path == directory.Path) return; // do not open again is already open
                        CURRENT_DIRECTORY = directory;
                        SELECTED_DIRECTORY = directory;
                        statusBar.SetLoading();
                        ((Manager)Parent).UseWaitCursor = true;
                        DirectoryContent content = await DirectoryContent.Get(directory);
                        previewPane.Set(directory, content.Info);
                        directoryContentPane.Set(content);
                        statusBar.Set(content.Info);
                        navigationBar.Set(directory);
                        ((Manager)Parent).UseWaitCursor = false;
                        break;
                    }
                case ActionType.SELECT:
                    {
                        SELECTED_DIRECTORY = directory;
                        previewPane.Set(directory, null);
                        break;
                    }
                case ActionType.DOWNLOAD:
                    {
                        ((Manager)Parent).UseWaitCursor = true;
                        await Download(SELECTED_DIRECTORY);
                        ((Manager)Parent).UseWaitCursor = false;
                        break;
                    }
                case ActionType.CREATE_DIRECTORY:
                    {
                        using (CreateEntryDialog dialog = new CreateEntryDialog(CURRENT_DIRECTORY, EntryType.DIRECTORY, null))
                        {
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string path = System.IO.Path.Combine(CURRENT_DIRECTORY.Path, dialog.EntryName);
                                await FileExplorer.CreateDirectory(path);
                                await folderTree.Refresh(CURRENT_DIRECTORY);
                                Execute(this, CURRENT_DIRECTORY, ActionType.REFRESH_DIRECTORY);
                            }
                        }
                        break;
                    }
                case ActionType.DELETE:
                    {
                        if (MessageBox.Show("Are you sure you want to permanently delete this directory and all it's content?", "Delete directory", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            ((Manager)Parent).UseWaitCursor = true;
                            await FileExplorer.DeleteDirectory(directory.Path, true);
                            await folderTree.Refresh(CURRENT_DIRECTORY);
                            Execute(this, CURRENT_DIRECTORY, ActionType.REFRESH_DIRECTORY);
                            ((Manager)Parent).UseWaitCursor = false;
                        }
                        break;
                    }
                case ActionType.REFRESH_DIRECTORY:
                    {
                        statusBar.SetLoading();
                        ((Manager)Parent).UseWaitCursor = true;
                        DirectoryContent content = await DirectoryContent.Get(CURRENT_DIRECTORY);
                        directoryContentPane.Set(content);
                        previewPane.Set(directory, content.Info);
                        statusBar.Set(content.Info);
                        ((Manager)Parent).UseWaitCursor = false;
                        break;
                    }
                case ActionType.UPLOAD_DIRECTORY:
                    {
                        await UploadDirectory();
                        await folderTree.Refresh(CURRENT_DIRECTORY);
                        Execute(this, CURRENT_DIRECTORY, ActionType.REFRESH_DIRECTORY);
                        break;
                    }
            }
        }


        private async void Execute(object sender, File file, ActionType type)
        {
            switch (type)
            {
                case ActionType.OPEN:
                    {
                        if (CURRENT_FILE?.Path == file.Path) return;
                        CURRENT_FILE = file;
                        SELECTED_FILE = file;
                        await previewPane.Set(file);
                        break;
                    }
                case ActionType.SELECT:
                    {
                        SELECTED_FILE = file;
                        await previewPane.Set(file);
                        break;
                    }
                case ActionType.DOWNLOAD:
                    {
                        ((Manager)Parent).UseWaitCursor = true;
                        await Download(SELECTED_FILE);
                        ((Manager)Parent).UseWaitCursor = false;
                        break;
                    }
                case ActionType.UPLOAD_FILE:
                    {
                        await UpLoadFile();
                        Execute(this, CURRENT_DIRECTORY, ActionType.REFRESH_DIRECTORY);
                        break;
                    }
                case ActionType.DELETE:
                    {
                        if (MessageBox.Show("Are you sure you want to permanently delete this file?", "Delete file", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            await FileExplorer.DeleteFile(file.Path);
                            Execute(this, CURRENT_DIRECTORY, ActionType.REFRESH_DIRECTORY);
                        }
                        break;
                    }
            }
        }

        #region upload
        private async Task UploadDirectory()
        {
            folderBrowserDialog.SelectedPath = LocalMachinePath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                LocalMachinePath = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default.LocalMachinePath = LocalMachinePath;
                Properties.Settings.Default.Save();

                using (TransferDialog dialog = new TransferDialog())
                {
                    Task task = dialog.UploadDirectory(CURRENT_DIRECTORY.Path, LocalMachinePath);
                    dialog.ShowDialog();
                    await task;
                }
            }
        }

        private async Task UpLoadFile()
        {
            openFileDialog.InitialDirectory = LocalMachinePath;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LocalMachinePath = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
                Properties.Settings.Default.LocalMachinePath = LocalMachinePath;
                Properties.Settings.Default.Save();

                using (TransferDialog dialog = new TransferDialog())
                {
                    Task task = dialog.UpLoadFile(CURRENT_DIRECTORY, openFileDialog.FileNames);
                    dialog.ShowDialog();
                    await task;
                }


            }
        }
        #endregion

        #region Download
        private async Task Download(File file)
        {
            saveFileDialog.InitialDirectory = LocalMachinePath;
            saveFileDialog.FileName = file.Name;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                LocalMachinePath = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.LocalMachinePath = LocalMachinePath;
                Properties.Settings.Default.Save();

                using (TransferDialog dialog = new TransferDialog())
                {
                    Task task = dialog.Download(file, saveFileDialog.FileName);
                    dialog.ShowDialog();
                    await task;
                }
            }
        }


        public async Task Download(Directory directory)
        {
            folderBrowserDialog.SelectedPath = LocalMachinePath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                LocalMachinePath = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default.LocalMachinePath = LocalMachinePath;
                Properties.Settings.Default.Save();
                using (TransferDialog dialog = new TransferDialog())
                {
                    Task task =  dialog.Download(directory, LocalMachinePath);
                    dialog.ShowDialog();
                    await task;
                }
            }
        }

        #endregion
    }
}
