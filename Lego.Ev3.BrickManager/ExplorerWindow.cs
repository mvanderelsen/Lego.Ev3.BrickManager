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
            }
        }



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

                byte[] data = await file.Download();
                using (System.IO.FileStream fs = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create))
                {
                    fs.Write(data, 0, data.Length);
                }
            }
        }


        private async Task Download(Directory directory)
        {
            folderBrowserDialog.SelectedPath = LocalMachinePath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                LocalMachinePath = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default.LocalMachinePath = LocalMachinePath;
                Properties.Settings.Default.Save();

                if (UserSettings.Mode == Mode.BASIC) await DownloadBasic(directory, LocalMachinePath);
                else await DownloadAdvanced(directory, LocalMachinePath);
            }
        }

        private async Task DownloadBasic(Directory directory, string toLocalPath)
        {

            string localPath = System.IO.Path.Combine(toLocalPath, directory.Name);
            switch (directory.Path)
            {
                case FileExplorer.ROOT_PATH:
                    {
                        System.IO.Path.Combine(toLocalPath, Manager.Brick.Name);
                        break;
                    }
                case FileExplorer.PROJECTS_PATH:
                    {
                        System.IO.Path.Combine(toLocalPath, "Drive");
                        break;
                    }
                case FileExplorer.SDCARD_PATH:
                    {
                        System.IO.Path.Combine(toLocalPath, "SDCard");
                        break;
                    }
            }

            File[] files = await FileExplorer.GetFiles(directory.Path);
            foreach (File file in files)
            {

                byte[] data = await file.Download();
                if (data != null && data.Length > 0)
                {
                    string filePath = System.IO.Path.Combine(localPath, file.Name);
                    using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                    {
                        fs.Write(data, 0, data.Length);
                    }
                }

            }

            switch (directory.Path)
            {
                case FileExplorer.ROOT_PATH:
                    {
                        foreach (Directory dir in await Manager.Brick.Drive.GetDirectories())
                        {
                            await DownloadBasic(dir, localPath);
                        }
                        if (Manager.Brick.SDCard != null)
                        {
                            foreach (Directory dir in await Manager.Brick.SDCard.GetDirectories())
                            {
                                await DownloadBasic(dir, localPath);
                            }
                        }
                        break;
                    }
                case FileExplorer.PROJECTS_PATH:
                    {
                        foreach (Directory dir in await Manager.Brick.Drive.GetDirectories())
                        {
                            await DownloadBasic(dir, localPath);
                        }
                        break;
                    }
                case FileExplorer.SDCARD_PATH:
                    {
                        foreach (Directory dir in await Manager.Brick.SDCard.GetDirectories())
                        {
                            await DownloadBasic(dir, localPath);
                        }
                        break;
                    }
            }
        }


        private async Task DownloadAdvanced(Directory directory, string toLocalPath)
        {

            bool rootDownload = directory.Path == FileExplorer.ROOT_PATH;
            string localPath = (rootDownload) ? System.IO.Path.Combine(toLocalPath, Manager.Brick.Name) : System.IO.Path.Combine(toLocalPath, directory.Name);
            System.IO.Directory.CreateDirectory(localPath);

            File[] files = await FileExplorer.GetFiles(directory.Path);
            foreach (File file in files)
            {

                byte[] data = await file.Download();
                if (data != null && data.Length > 0)
                {
                    string filePath = System.IO.Path.Combine(localPath, file.Name);
                    using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                    {
                        fs.Write(data, 0, data.Length);
                    }
                }

            }
            Directory[] subDirectories = await FileExplorer.GetDirectories(directory.Path);
            foreach (Directory subDirectory in subDirectories)
            {
                await DownloadAdvanced(subDirectory, localPath);
            }
        }

        #endregion
    }
}
