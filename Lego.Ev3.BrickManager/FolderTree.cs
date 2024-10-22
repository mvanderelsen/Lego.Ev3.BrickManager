﻿using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class FolderTree : UserControl
    {
        public event OnDirectoryAction DirectoryAction;

        public FolderTree()
        {
            InitializeComponent();

            tree.ImageList = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(20, 20)
            };
            tree.ImageList.Images.Add(Properties.Resources.Brick);
            tree.ImageList.Images.Add(Properties.Resources.Folder);
            tree.ImageList.Images.Add(Properties.Resources.Disk);
            tree.ImageList.Images.Add(Properties.Resources.SdCard);
        }

        public async Task Initialize(Directory root)
        {
            await BuildTree(root);
        }

        public void InitView()
        {
            tree.Nodes[0].Expand();
        }

        #region tree
        private async Task BuildTree(Directory directory)
        {
            tree.BeginUpdate();
            tree.Nodes.Clear();
            TreeNode root = new TreeNode(Manager.Brick.Name)
            {
                Tag = directory,
                ImageIndex = 0,
                SelectedImageIndex = 0
            };
            tree.Nodes.Add(root);

            switch (UserSettings.Mode)
            {
                case Mode.BASIC:
                    {
                        await BasicTree(root);
                        break;
                    }
                case Mode.ADVANCED:
                    {
                        await AdvancedTree(root);
                        break;
                    }
                default: throw new NotImplementedException(nameof(Mode));
            }

            tree.Nodes[0].Expand();
            tree.EndUpdate();
        }

        private async Task BasicTree(TreeNode treeNode)
        {
            Directory[] directories;
            TreeNode node;

            node = new TreeNode("Drive")
            {
                Tag = await BrickExplorer.GetDirectory(BrickExplorer.PROJECTS_PATH),
                ImageIndex = 2,
                SelectedImageIndex = 2
            };
            directories = await Manager.Brick.Drive.GetDirectories();
            Append(node, directories);
            treeNode.Nodes.Add(node);

            if (Manager.Brick.SDCard != null)
            {
                node = new TreeNode("SD Card")
                {
                    Tag = await BrickExplorer.GetDirectory(BrickExplorer.SDCARD_PATH),
                    ImageIndex = 3,
                    SelectedImageIndex = 3
                };
                directories = await Manager.Brick.SDCard.GetDirectories();
                Append(node, directories);
                treeNode.Nodes.Add(node);
            }
        }

        private async Task AdvancedTree(TreeNode parent)
        {
            Directory[] directories = await BrickExplorer.GetDirectories(((Directory)parent.Tag).Path);
            foreach (Directory directory in directories)
            {
                TreeNode node = ConvertToNode(directory);
                parent.Nodes.Add(node);
                await AdvancedTree(node);
            }
        }

        private void Append(TreeNode parent, Directory[] directories)
        {
            if (directories == null) return;
            foreach (Directory directory in directories)
            {
                TreeNode node = ConvertToNode(directory);
                parent.Nodes.Add(node);
            }
        }

        private TreeNode ConvertToNode(Directory directory)
        {
            TreeNode node = new TreeNode(directory.Name)
            {
                Tag = directory,
                ImageIndex = 1,
                SelectedImageIndex = 1
            };
            return node;
        }
        #endregion


        private void Tree_MouseDown(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo info = tree.HitTest(e.Location);
            if (info != null && info.Node != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    DirectoryAction?.Invoke(this, (Directory)info.Node.Tag, ActionType.OPEN);
                }
            }
        }


        public async Task Refresh(Directory directory)
        {
            tree.BeginUpdate();

            TreeNode node = Find(directory);
            if (node != null)
            {
                Directory[] directories;
                switch (UserSettings.Mode)
                {
                    case Mode.BASIC:
                        {
                            switch (directory.Path)
                            {
                                case BrickExplorer.PROJECTS_PATH:
                                    {
                                        directories = await Manager.Brick.Drive.GetDirectories();
                                        break;
                                    }
                                case BrickExplorer.SDCARD_PATH:
                                    {
                                        directories = await Manager.Brick.SDCard.GetDirectories();
                                        break;
                                    }
                                default:
                                    {
                                        //Should never get here
                                        throw new InvalidOperationException(nameof(tree));
                                    }
                            }
                            break;
                        }
                    default:
                        {
                            directories = await BrickExplorer.GetDirectories(directory.Path);
                            break;
                        }

                }

                node.Nodes.Clear();
                foreach (Directory dir in directories)
                {
                    node.Nodes.Add(ConvertToNode(dir));
                }
                
                //int nodeCount = node.Nodes.Count;
                //int ni = 0;
                //for (int i = 0; i < dirs.Length; i++)
                //{
                //    if (i < nodeCount)
                //    {
                //        if (((Directory)node.Nodes[ni].Tag).Path != dirs[i].Path)
                //        {
                //            node.Nodes.Insert(i, ConvertToNode(dirs[i]));
                //            break;
                //        }
                //    }
                //    else
                //    {
                //        node.Nodes.Add(ConvertToNode(dirs[i]));
                //    }
                //    ni++;
                //}
            }
            tree.EndUpdate();

        }

        private TreeNode Find(Directory directory)
        {
            return FindNode(tree.Nodes[0], directory.Path);
        }

        private TreeNode FindNode(TreeNode tree, string path)
        {
            foreach (TreeNode node in tree.Nodes)
            {
                if (((Directory)node.Tag).Path == path)
                {
                    return node;
                }
                else FindNode(node, path);
            }
            return null;
        }
    }
}
