using Lego.Ev3.Framework;

namespace Lego.Ev3.BrickManager
{
    public class Entry
    { 
        public EntryType Type { get; set; }

        public bool IsPlayable { get; set; }

        public bool IsOpenEnabled { get; set; }

        public Entry(string path, EntryType type)
        {
            Type = type;
            switch(type)
            {
                case EntryType.FILE:
                    {
                        FileType fileType = GetFileType(path);
                        switch (fileType)
                        {
                            case FileType.SoundFile:
                                {
                                    IsPlayable = true;
                                    IsOpenEnabled = true;
                                    break;
                                }
                            case FileType.TextFile:
                                {
                                    IsOpenEnabled = true;
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        public static explicit operator Entry(File file)
        {
            return new Entry(file.Path, EntryType.FILE);
        }

        public static explicit operator Entry(Directory directory)
        {
            return new Entry(directory.Path, EntryType.DIRECTORY);
        }

        public FileType GetFileType(string path)
        {
            string extension = System.IO.Path.GetExtension(path);
            switch (extension)
            {
                case ".rsf": return FileType.SoundFile;
                case ".rtf": return FileType.TextFile;
                default: return FileType.SystemFile;
            }
        }

    }




    public enum EntryType
    {
        FILE,
        DIRECTORY
    }
}
