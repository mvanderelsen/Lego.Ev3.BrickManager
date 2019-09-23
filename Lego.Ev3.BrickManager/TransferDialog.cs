using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class TransferDialog : Form
    {
        public TransferDialog()
        {
            InitializeComponent();

            labelAction.Text = "";
            labelContent.Text = "";
        }


        public async Task UploadDirectory(string directoryPath, string path)
        {
            await UploadDirectoryRecursive(directoryPath, path);
            Close();
        }

        private async Task UploadDirectoryRecursive(string directoryPath, string path)
        {
            labelAction.Text = "Uploading...";
            labelContent.Text = path;

            System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);
            string brickPath = $"{directoryPath}{directoryInfo.Name}/";

            await FileExplorer.CreateDirectory(brickPath);

            foreach (System.IO.FileInfo file in directoryInfo.GetFiles())
            {
                if (FileExplorer.IsRobotFile(file.FullName))
                {
                    labelContent.Text = file.FullName;
                    await FileExplorer.UploadFile(file.FullName, brickPath, file.Name);
                }
            }

            if (UserSettings.Mode != Mode.BASIC)
            {
                foreach (System.IO.DirectoryInfo subDir in directoryInfo.GetDirectories())
                {
                    await UploadDirectoryRecursive(brickPath, subDir.FullName);
                }
            }
        }

        public async Task UpLoadFile(Directory directory, string[] paths)
        {
            labelAction.Text = "Uploading...";
            foreach (string path in paths)
            {
                labelContent.Text = path;
                if (FileExplorer.IsRobotFile(path)) await directory.UploadFile(path);
            }
            Close();
        }



        public async Task Download(File file, string path)
        {
            labelAction.Text = "Downloading...";
            labelContent.Text = file.Path;
            byte[] data = await file.Download();
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create))
            {
                if (data != null) fs.Write(data, 0, data.Length);
            }
            Close();
        }


        public async Task Download(Directory directory, string path)
        {
            if (UserSettings.Mode == Mode.BASIC) await DownloadBasic(directory, path);
            else await DownloadAdvanced(directory, path);
            Close();
        }

        private async Task DownloadBasic(Directory directory, string toLocalPath)
        {
            labelAction.Text = "Downloading...";
            labelContent.Text = directory.Path;
            string localPath = System.IO.Path.Combine(toLocalPath, directory.Name);
            switch (directory.Path)
            {
                case FileExplorer.ROOT_PATH:
                    {
                        localPath = System.IO.Path.Combine(toLocalPath, Manager.Brick.Name);
                        break;
                    }
                case FileExplorer.PROJECTS_PATH:
                    {
                        localPath = System.IO.Path.Combine(toLocalPath, "Drive");
                        break;
                    }
                case FileExplorer.SDCARD_PATH:
                    {
                        localPath = System.IO.Path.Combine(toLocalPath, "SDCard");
                        break;
                    }
            }

            System.IO.Directory.CreateDirectory(localPath);

            if (directory.Path != FileExplorer.ROOT_PATH)
            {
                File[] files = await FileExplorer.GetFiles(directory.Path);
                foreach (File file in files)
                {
                    labelContent.Text = file.Path;
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
            }

            switch (directory.Path)
            {
                case FileExplorer.ROOT_PATH:
                    {
                        string drivePath = System.IO.Path.Combine(localPath, "Drive");
                        System.IO.Directory.CreateDirectory(drivePath);
                        foreach (Directory dir in await Manager.Brick.Drive.GetDirectories())
                        {
                            await DownloadBasic(dir, drivePath);
                        }
                        if (Manager.Brick.SDCard != null)
                        {
                            string sdPath = System.IO.Path.Combine(localPath, "SDCard");
                            System.IO.Directory.CreateDirectory(sdPath);
                            foreach (Directory dir in await Manager.Brick.SDCard.GetDirectories())
                            {
                                await DownloadBasic(dir, sdPath);
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
            labelAction.Text = "Downloading...";
            labelContent.Text = directory.Path;
            bool rootDownload = directory.Path == FileExplorer.ROOT_PATH;
            string localPath = (rootDownload) ? System.IO.Path.Combine(toLocalPath, Manager.Brick.Name) : System.IO.Path.Combine(toLocalPath, directory.Name);
            System.IO.Directory.CreateDirectory(localPath);

            File[] files = await FileExplorer.GetFiles(directory.Path);
            foreach (File file in files)
            {
                labelContent.Text = file.Path;
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
    }
}
