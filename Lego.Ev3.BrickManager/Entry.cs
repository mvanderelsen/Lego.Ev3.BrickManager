using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Core;

namespace Lego.Ev3.BrickManager
{
    public class Entry
    {
        public EntryType Type { get; set; }

        public bool IsPlayable { get; set; }

        public bool IsDownloadable{ get; set; }

        public Entry(string path, EntryType type)
        {
            Type = type;
            IsPlayable = path.EndsWith(".rsf");
           
            switch (UserSettings.Mode)
            {
                case Mode.BASIC:
                    {
                        switch (path)
                        {
                            case "../prjs/BrkProg_SAVE":
                            case "../prjs/BrkDL_SAVE":
                                {
                                    IsDownloadable = false;
                                    break;
                                }
                            default:
                                {
                                    IsDownloadable = true;
                                    break;
                                }
                        }
                        break;
                    }
                case Mode.ADVANCED:
                    {
                        IsDownloadable = true;
                        break;
                    }
                case Mode.EXPERT:
                    {
                        IsDownloadable = true;
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
    }

    public enum EntryType
    {
        FILE,
        DIRECTORY
    }
}
