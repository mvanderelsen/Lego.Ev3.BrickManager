using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lego.Ev3.BrickManager
{
    public class DirectoryContent
    {
        public string Path { get; private set; }

        public Directory[] Directories { get; private set; }

        public File[] Files { get; private set; }

        public DirectoryInfo Info { get; private set; }



        public static async Task<DirectoryContent> Get(Directory directory)
        {
            DirectoryContent content = new DirectoryContent
            {
                Path = directory.Path,
                Info = new DirectoryInfo(),
            };
            switch (UserSettings.Mode)
            {
                case Mode.BASIC:
                    {
                        switch (directory.Path)
                        {
                            case BrickExplorer.ROOT_PATH:
                                {
                                    List<Directory> dirs = new List<Directory>();
                                    dirs.Add(await BrickExplorer.GetDirectory(BrickExplorer.PROJECTS_PATH));
                                    if (Manager.Brick.SDCard != null)
                                    {
                                        dirs.Add(await BrickExplorer.GetDirectory(BrickExplorer.SDCARD_PATH));
                                    }
                                    content.Directories = dirs.ToArray();
                                    content.Info.ItemCount = content.Directories.Length;
                                    break;
                                }
                            case BrickExplorer.PROJECTS_PATH:
                                {
                                    content.Directories = await Manager.Brick.Drive.GetDirectories();
                                    content.Info.ItemCount = content.Directories.Length;
                                    break;
                                }
                            case BrickExplorer.SDCARD_PATH:
                                {
                                    content.Directories = await Manager.Brick.SDCard.GetDirectories();
                                    content.Info.ItemCount = content.Directories.Length;
                                    break;
                                }
                            default:
                                {
                                    //only get files
                                    content.Files = await BrickExplorer.GetFiles(directory.Path);
                                    foreach (File file in content.Files)
                                    {
                                        content.Info.TotalByteSize += file.Size;
                                    }
                                    content.Info.ItemCount = content.Files.Length;
                                    break;
                                }
                        }
                        return content;
                    }
                default:
                    {
                        Framework.Core.DirectoryContent dc = await BrickExplorer.GetDirectoryContent(directory.Path);
                        content.Directories = dc.Directories;
                        content.Files = dc.Files;
                        foreach (File file in content.Files)
                        {
                            content.Info.TotalByteSize += file.Size;
                        }
                        content.Info.ItemCount = content.Directories.Length + content.Files.Length;
                        return content;
                    }
            }
        }


        public static string ToFileSize(long size)
        {
            if (size == 0) return "0 KB";
            if (size < 1024) return "1 KB";
            int kb = (int)Math.Ceiling(size / 1024d);
            return $"{kb} KB";
        }

        public static string ToByteFileSize(long size)
        {
            if (size == 0) return "0 KB";
            if (size < 1024) return $"{size} bytes";
            if (size > 1048576) // 1024 * 1024 = Mb
            {
                double mb = Math.Ceiling(size / 1048576d);
                return $"{mb:0.0} MB";
            }
            double kb = size / 1024d;
            return $"{kb:0.0} KB";
        }

        public static string ToString(FileType type)
        {
            switch (type)
            {
                case FileType.ArchiveFile:
                    {
                        return "Archive file";
                    }
                case FileType.ByteCodeFile:
                    {
                        return "Bytecode file";
                    }
                case FileType.ConfigFile:
                    {
                        return "Config file";
                    }
                case FileType.DataLogFile:
                    {
                        return "Datalog file";
                    }
                case FileType.GraphicFile:
                    {
                        return "Graphic file";
                    }
                case FileType.ProgramFile:
                    {
                        return "Program file";
                    }
                case FileType.SoundFile:
                    {
                        return "Sound file";
                    }
                case FileType.SystemFile:
                    {
                        return "System file";
                    }
                case FileType.TextFile:
                    {
                        return "Text file";
                    }
            }
            return "";
        }
    }
}
