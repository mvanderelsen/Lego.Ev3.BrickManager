using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System.Drawing;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class DirectoryContentPane : UserControl
    {
        public event OnDirectoryAction DirectoryAction;
        public event OnFileAction FileAction;

        public DirectoryContentPane()
        {
            InitializeComponent();

            listView.SmallImageList = new ImageList
            {
                ImageSize = new Size(20, 20),
                ColorDepth = ColorDepth.Depth32Bit
            };
            listView.SmallImageList.Images.Add(Properties.Resources.Folder);
            listView.SmallImageList.Images.Add(Properties.Resources.Disk);
            listView.SmallImageList.Images.Add(Properties.Resources.SdCard);
            listView.SmallImageList.Images.Add(Properties.Resources.File);
            listView.SmallImageList.Images.Add(Properties.Resources.Rsf);
            listView.SmallImageList.Images.Add(Properties.Resources.Rgf);
            listView.SmallImageList.Images.Add(Properties.Resources.Rtf);


            listView.LargeImageList = new ImageList
            {
                ImageSize = new Size(48, 48),
                ColorDepth = ColorDepth.Depth32Bit
            };
            listView.LargeImageList.Images.Add(Properties.Resources.Folder);
            listView.LargeImageList.Images.Add(Properties.Resources.Disk);
            listView.LargeImageList.Images.Add(Properties.Resources.SdCard);
            listView.LargeImageList.Images.Add(Properties.Resources.File);
            listView.LargeImageList.Images.Add(Properties.Resources.Rsf);
            listView.LargeImageList.Images.Add(Properties.Resources.Rgf);
            listView.LargeImageList.Images.Add(Properties.Resources.Rtf);

        }


        public void Set(DirectoryContent content)
        {
            listView.BeginUpdate();
            listView.Items.Clear();

            if (UserSettings.Mode == Mode.BASIC && content.Path == FileExplorer.ROOT_PATH)
            {
                foreach (Directory directory in content.Directories)
                {
                    switch (directory.Path)
                    {
                        case FileExplorer.PROJECTS_PATH:
                            {
                                ListViewItem item = new ListViewItem("Drive")
                                {
                                    Tag = directory,
                                    ImageIndex = 1
                                };
                                item.SubItems.Add("Drive");
                                item.SubItems.Add("");
                                listView.Items.Add(item);
                                break;
                            }
                        case FileExplorer.SDCARD_PATH:
                            {
                                ListViewItem item = new ListViewItem("SD Card")
                                {
                                    Tag = directory,
                                    ImageIndex = 2
                                };
                                item.SubItems.Add("SD Card");
                                item.SubItems.Add("");
                                listView.Items.Add(item);
                                break;
                            }
                    }
                }
            }
            else
            {
                if (content.Directories != null)
                {
                    foreach (Directory directory in content.Directories)
                    {
                        ListViewItem item = new ListViewItem(directory.Name)
                        {
                            Tag = directory,
                            ImageIndex = 0
                        };
                        item.SubItems.Add("File folder");
                        item.SubItems.Add("");
                        listView.Items.Add(item);
                    }
                }
                if (content.Files != null)
                {
                    foreach (File file in content.Files)
                    {
                        //if (UseFilter && !file.Name.EndsWith(FilterValue)) continue;

                        ListViewItem item = new ListViewItem(file.Name);
                        item.Tag = file;
                        switch (file.Type)
                        {
                            case FileType.TextFile:
                                {
                                    item.ImageIndex = 6;
                                    break;
                                }
                            case FileType.GraphicFile:
                                {
                                    item.ImageIndex = 5;
                                    break;
                                }
                            case FileType.SoundFile:
                                {
                                    item.ImageIndex = 4;
                                    break;
                                }
                            default:
                                {
                                    item.ImageIndex = 3;
                                    break;
                                }
                        }

                        item.SubItems.Add(DirectoryContent.ToString(file.Type));
                        item.SubItems.Add(DirectoryContent.ToFileSize(file.Size));
                        listView.Items.Add(item);
                    }
                }
            }

            listView.EndUpdate();
        }

        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listView.HitTest(e.Location);
            Invoke(info.Item, ActionType.SELECT);
        }

        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Invoke(listView.SelectedItems[0], ActionType.OPEN);
        }


        private void Invoke(ListViewItem item, ActionType actionType)
        {
            if (item != null && item.Tag != null)
            {
                if (item.Tag is Directory)
                {
                    DirectoryAction?.Invoke(this, (Directory)item.Tag, actionType);
                }
                else if (item.Tag is File)
                {
                    FileAction?.Invoke(this, (File)item.Tag, actionType);
                }
            }
        }
    }
}