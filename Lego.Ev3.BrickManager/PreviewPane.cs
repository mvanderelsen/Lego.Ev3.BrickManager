using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class PreviewPane : UserControl
    {
        private string _brickInfoText;

        public PreviewPane()
        {
            InitializeComponent();
            Clear();
        }

        private void Clear()
        {
            labelName.Text = "";
            labelType.Text = "";
            textBoxInfo.Text = "";
            icon.Image = null;
            previewImage.Image = null;
            previewImage.Visible = false;
            previewContent.Text = "";
            previewContent.Visible = false;
        }

        public async Task Initialize(Directory root)
        {
            BrickInfo brickInfo = await Manager.Brick.Console.GetBrickInfo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Firmware: {brickInfo.Firmware.Version}");
            sb.AppendLine($"Build: {brickInfo.Firmware.Build}");
            sb.AppendLine($"OS: {brickInfo.OS.Version}");
            sb.AppendLine($"Build: {brickInfo.OS.Build}");
            sb.AppendLine($"Hardware: {brickInfo.Hardware.Version}");
            sb.AppendLine($"Version: {brickInfo.Version.Split('(')[0]}");
            sb.AppendLine($"({brickInfo.Version.Split('(')[1]}");
            _brickInfoText = sb.ToString();
            Set(root, null);
        }


        #region directory
        public void Set(Directory directory, DirectoryInfo directoryInfo)
        {
            Clear();

            switch (UserSettings.Mode)
            {
                case Mode.BASIC:
                    {
                        switch (directory.Path)
                        {
                            case FileExplorer.ROOT_PATH:
                                {
                                    SetRoot();
                                    break;
                                }
                            case FileExplorer.PROJECTS_PATH:
                                {
                                    labelName.Text = "Drive";
                                    icon.Image = new Icon(Properties.Resources.Disk, new Size(64, 64)).ToBitmap();
                                    break;
                                }
                            case FileExplorer.SDCARD_PATH:
                                {
                                    labelName.Text = "SD Card";
                                    icon.Image = new Icon(Properties.Resources.SdCard, new Size(64, 64)).ToBitmap();
                                    break;
                                }
                            default:
                                {
                                    SetDefault(directory, directoryInfo);
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        switch (directory.Path)
                        {
                            case FileExplorer.ROOT_PATH:
                                {
                                    SetRoot();
                                    break;
                                }
                            default:
                                {
                                    SetDefault(directory, directoryInfo);
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        private void SetRoot()
        {
            labelName.Text = Manager.Brick.Name;
            labelType.Text = "Lego Mindstorms";
            icon.Image = new Icon(Properties.Resources.Brick, new Size(64, 64)).ToBitmap();
            if (UserSettings.Mode != Mode.BASIC) textBoxInfo.Text = _brickInfoText;
            
        }

        private void SetDefault(Directory directory, DirectoryInfo directoryInfo)
        {
            labelName.Text = directory.Name;
            labelType.Text = "File folder";
            icon.Image = new Icon(Properties.Resources.Folder, new Size(64, 64)).ToBitmap();
            StringBuilder sb = new StringBuilder();
            if (UserSettings.Mode != Mode.BASIC) sb.AppendLine($"Path: {directory.Path}");
            if (directoryInfo != null)
            {
                if (directoryInfo.ItemCount > 0) sb.AppendLine($"Items: {directoryInfo.ItemCount}");
                if (directoryInfo.TotalByteSize > 0)
                {
                    if (UserSettings.Mode == Mode.BASIC) sb.AppendLine($"Size: {DirectoryContent.ToFileSize(directoryInfo.TotalByteSize)}");
                    else sb.AppendLine($"Size: {DirectoryContent.ToByteFileSize(directoryInfo.TotalByteSize)}");
                }
            }
            textBoxInfo.Text = sb.ToString();
        }

        #endregion

        #region file
        public async Task Set(File file)
        {
            Clear();

            labelName.Text = file.FileName;
            labelType.Text = DirectoryContent.ToString(file.Type);
            string additionalInfo = null;
            switch (file.Type)
            {
                case FileType.TextFile:
                    {
                        icon.Image = new Icon(Properties.Resources.Rtf, new Size(64, 64)).ToBitmap();
                        previewContent.Text = await ((TextFile)file).DownloadAsString();
                        previewContent.Visible = true;
                        break;
                    }
                case FileType.SoundFile:
                    {
                        icon.Image = new Icon(Properties.Resources.Rsf, new Size(64, 64)).ToBitmap();
                        break;
                    }
                case FileType.GraphicFile:
                    {
                        icon.Image = new Icon(Properties.Resources.Rgf, new Size(64, 64)).ToBitmap();
                        byte[] data = await file.Download();
                        Bitmap bitmap = FileConverter.RGFtoBitmap(data, Color.FromArgb(73, 74, 75));
                        int width = data[0];
                        int height = data[1];
                        previewImage.Image = bitmap;
                        additionalInfo = $"Dimension: {width} x {height} px";
                        previewImage.Visible = true;
                        break;
                    }
                default:
                    {
                        icon.Image = new Icon(Properties.Resources.File, new Size(64, 64)).ToBitmap();
                        break;
                    }
            }

            StringBuilder sb = new StringBuilder();
            if (UserSettings.Mode == Mode.BASIC)
            {
                sb.AppendLine($"Size: {DirectoryContent.ToFileSize(file.Size)}");
            }
            else
            {
                sb.AppendLine($"Path: {file.Path}");
                sb.AppendLine($"Size: {DirectoryContent.ToByteFileSize(file.Size)}");
                sb.AppendLine($"Md5sum: {file.MD5SUM}");
            }
            if (additionalInfo != null) sb.Append(additionalInfo);
            textBoxInfo.Text = sb.ToString();
        }
        #endregion
    }
}
